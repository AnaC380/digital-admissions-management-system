-- =============================================================================
-- DAMS Database Operations Scripts
-- =============================================================================

-- 1. HEALTH CHECK
PRINT '========================================'
PRINT 'DAMS_DB — Health Check'
PRINT CONVERT(VARCHAR, GETDATE(), 120)
PRINT '========================================'

SELECT name AS DatabaseName, state_desc AS Status, recovery_model_desc AS RecoveryModel
FROM sys.databases WHERE name = 'DAMS_DB';

SELECT
    t.name AS TableName,
    p.rows AS RowCount,
    CAST(a.total_pages * 8 / 1024.0 AS DECIMAL(10,2)) AS TotalSizeMB
FROM sys.tables t
JOIN sys.indexes i ON t.object_id = i.object_id
JOIN sys.partitions p ON i.object_id = p.object_id AND i.index_id = p.index_id
JOIN sys.allocation_units a ON p.partition_id = a.container_id
WHERE t.is_ms_shipped = 0
GROUP BY t.name, p.rows, a.total_pages
ORDER BY TotalSizeMB DESC;

SELECT DB_NAME(dbid) AS DatabaseName, COUNT(dbid) AS ActiveConnections, loginame AS Login
FROM sys.sysprocesses WHERE dbid > 0
GROUP BY dbid, loginame ORDER BY ActiveConnections DESC;

-- 2. BACKUP FULL
DECLARE @BackupPath  NVARCHAR(500);
DECLARE @BackupFile  NVARCHAR(500);
DECLARE @Timestamp   NVARCHAR(20);
SET @Timestamp  = REPLACE(REPLACE(CONVERT(VARCHAR, GETDATE(), 120), ':', ''), ' ', '_');
SET @BackupPath = 'C:\Backups\DAMS\';
SET @BackupFile = @BackupPath + 'DAMS_DB_FULL_' + @Timestamp + '.bak';
PRINT 'Iniciando backup FULL: ' + @BackupFile;
BACKUP DATABASE DAMS_DB TO DISK = @BackupFile
WITH FORMAT, INIT, NAME = N'DAMS_DB Full Backup', STATS = 10, COMPRESSION;
PRINT 'Backup concluido: ' + @BackupFile;

-- 3. MONITORAMENTO DE PERFORMANCE
SELECT TOP 10
    qs.total_worker_time / qs.execution_count AS AvgCPU_microsec,
    qs.execution_count AS Executions,
    SUBSTRING(qt.text, (qs.statement_start_offset/2)+1,
        ((CASE qs.statement_end_offset WHEN -1 THEN DATALENGTH(qt.text)
          ELSE qs.statement_end_offset END - qs.statement_start_offset)/2)+1) AS QueryText
FROM sys.dm_exec_query_stats qs
CROSS APPLY sys.dm_exec_sql_text(qs.sql_handle) qt
WHERE qt.dbid = DB_ID('DAMS_DB')
ORDER BY AvgCPU_microsec DESC;

SELECT
    OBJECT_NAME(ips.object_id) AS TableName,
    i.name AS IndexName,
    CAST(ips.avg_fragmentation_in_percent AS DECIMAL(5,2)) AS FragmentationPct
FROM sys.dm_db_index_physical_stats(DB_ID('DAMS_DB'), NULL, NULL, NULL, 'LIMITED') ips
JOIN sys.indexes i ON ips.object_id = i.ob


@'
param(
    [ValidateSet("HealthCheck","Backup","CheckContainer","All")]
    [string]$Action = "All",
    [string]$SqlServer    = "localhost,1433",
    [string]$SqlUser      = "sa",
    [string]$SqlPassword  = "SUA_SENHA_AQUI",
    [string]$Database     = "DAMS_DB",
    [string]$BackupDir    = "C:\Backups\DAMS",
    [string]$ContainerName = "dams-sqlserver"
)

function Write-Header($msg) {
    Write-Host "`n$("="*60)" -ForegroundColor Cyan
    Write-Host "  $msg"       -ForegroundColor Cyan
    Write-Host "$("="*60)`n"  -ForegroundColor Cyan
}
function Write-Success($msg) { Write-Host "OK  $msg" -ForegroundColor Green  }
function Write-Fail($msg)    { Write-Host "ERR $msg" -ForegroundColor Red    }

function Invoke-SqlCmd($query) {
    & sqlcmd -S $SqlServer -U $SqlUser -P $SqlPassword -d $Database -C -Q $query
    return $LASTEXITCODE
}

function Invoke-HealthCheck {
    Write-Header "Health Check — DAMS_DB"
    Invoke-SqlCmd "SELECT name, state_desc, recovery_model_desc FROM sys.databases WHERE name = '$Database';"
    Invoke-SqlCmd "SELECT t.name AS TableName, p.rows AS RowCount FROM sys.tables t JOIN sys.partitions p ON t.object_id = p.object_id AND p.index_id IN (0,1) WHERE t.is_ms_shipped = 0 ORDER BY p.rows DESC;"
    Write-Success "Health Check concluido."
}

function Invoke-Backup {
    Write-Header "Backup — DAMS_DB"
    if (-not (Test-Path $BackupDir)) { New-Item -ItemType Directory -Path $BackupDir -Force | Out-Null }
    $ts   = Get-Date -Format "yyyyMMdd_HHmmss"
    $file = Join-Path $BackupDir "DAMS_DB_FULL_$ts.bak"
    $exit = Invoke-SqlCmd "BACKUP DATABASE [$Database] TO DISK = N'$file' WITH FORMAT, INIT, NAME = N'DAMS Full $ts', STATS = 20, COMPRESSION;"
    if ($exit -eq 0) {
        Write-Success "Backup criado: $file"
        Get-ChildItem $BackupDir -Filter "*.bak" | Where-Object { $_.LastWriteTime -lt (Get-Date).AddDays(-7) } | Remove-Item -Force
    } else { Write-Fail "Falha no backup." }
}

function Invoke-CheckContainer {
    Write-Header "Container — $ContainerName"
    if (-not (Get-Command "docker" -ErrorAction SilentlyContinue)) { Write-Host "Docker nao encontrado."; return }
    $status = docker inspect --format "{{.State.Status}}" $ContainerName 2>$null
    if ($LASTEXITCODE -ne 0) { Write-Fail "Container nao encontrado."; return }
    Write-Host "Status: $status"
    if ($status -eq "running") {
        docker stats $ContainerName --no-stream --format "table {{.Name}}\t{{.CPUPerc}}\t{{.MemUsage}}"
        docker logs $ContainerName --tail 10
    }
}

Write-Host "DAMS Ops — $(Get-Date -Format 'yyyy-MM-dd HH:mm:ss')" -ForegroundColor Magenta
switch ($Action) {
    "HealthCheck"    { Invoke-HealthCheck }
    "Backup"         { Invoke-Backup }
    "CheckContainer" { Invoke-CheckContainer }
    "All"            { Invoke-CheckContainer; Invoke-HealthCheck; Invoke-Backup }
}
