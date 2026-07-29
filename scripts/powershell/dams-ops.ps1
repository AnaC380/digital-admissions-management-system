param(
    [ValidateSet("HealthCheck","Backup","CheckContainer","All")]
    [string]$Action = "All",
    [string]$SqlServer    = "localhost,1433",
    [string]$SqlUser      = "sa",
    [Parameter(Mandatory=$true)]
    [string]$SqlPassword,
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