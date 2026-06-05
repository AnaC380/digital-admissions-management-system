# 🎓 DAMS — Digital Admissions Management System

Sistema de gerenciamento digital de admissões para instituições de ensino, desenvolvido com **.NET 10** e **Clean Architecture**.

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat-square&logo=dotnet)
![SQL Server](https://img.shields.io/badge/SQL%20Server-2022-CC2927?style=flat-square&logo=microsoftsqlserver)
![Docker](https://img.shields.io/badge/Docker-Compose-2496ED?style=flat-square&logo=docker)
![Azure](https://img.shields.io/badge/Azure-SQL%20Database-0078D4?style=flat-square&logo=microsoftazure)
![CI/CD](https://img.shields.io/badge/CI%2FCD-GitHub%20Actions-2088FF?style=flat-square&logo=githubactions)
![JWT](https://img.shields.io/badge/Auth-JWT%20Bearer-000000?style=flat-square&logo=jsonwebtokens)
![License](https://img.shields.io/badge/License-MIT-green?style=flat-square)

---

## 📋 Sobre o Projeto

O DAMS é uma API REST para gerenciar processos de admissão de candidatos em instituições de ensino. O sistema permite controle completo sobre candidatos, documentos e usuários envolvidos no processo seletivo, com rastreabilidade, auditoria em todas as operações e autenticação segura via JWT.

O projeto foi desenvolvido com foco em **operação de ambientes produtivos**, incluindo scripts de DBA, automação PowerShell, pipeline de CI/CD e infraestrutura provisionada no Azure.

---

## ✨ Funcionalidades

- ✅ Autenticação e autorização com JWT Bearer
- ✅ Gerenciamento de candidatos e admissões
- ✅ Controle de documentos por admissão
- ✅ Sistema de usuários com perfis e permissões
- ✅ Rastreamento de status das admissões
- ✅ Auditoria completa com timestamps
- ✅ Scripts de operação de banco de dados (backup, health check, performance)
- ✅ Automação de rotinas via PowerShell
- ✅ Pipeline CI/CD com GitHub Actions
- ✅ Banco de dados provisionado no Azure SQL Database
- 📖 Documentação interativa via OpenAPI

---

## 🏗️ Arquitetura

O projeto segue os princípios de **Clean Architecture** e **Domain-Driven Design (DDD)**:

```
digital-admissions-management-system/
├── .github/
│   └── workflows/
│       └── ci.yml                    # Pipeline CI/CD — build, migrations, testes
└── backend/
    ├── DAMS.Api/                     # Camada de entrada — Controllers e endpoints
    │   ├── Controllers/
    │   │   ├── AdmissionsController.cs
    │   │   └── AuthController.cs     # Endpoints de registro e login JWT
    │   ├── Properties/
    │   │   └── launchSettings.json   # Configurações de execução local
    │   ├── appsettings.json          # Connection string e configurações JWT
    │   ├── DAMS.Api.http             # HTTP Client para testes dos endpoints
    │   └── Program.cs                # Pipeline da aplicação
    ├── DAMS.Application/             # Casos de uso e lógica de aplicação
    │   ├── DTOs/
    │   │   ├── Auth/
    │   │   │   └── AuthDtos.cs       # RegisterRequest, LoginRequest, AuthResponse
    │   │   └── AdmissionDto.cs
    │   └── Interfaces/
    │       └── ITokenService.cs      # Contrato do serviço JWT
    ├── DAMS.Domain/                  # Entidades e regras de negócio
    │   └── Entities/
    │       ├── User.cs               # Entidade com PasswordHash (BCrypt)
    │       ├── Admission.cs
    │       └── Document.cs
    ├── DAMS.Infrastructure/          # Persistência e serviços externos
    │   ├── Persistence/
    │   │   └── DamsDbContext.cs
    │   ├── Migrations/               # Migrations EF Core (versionamento do schema)
    │   └── Services/
    │       └── TokenService.cs       # Geração do JWT (HMAC-SHA256)
    ├── DAMS.Tests/                   # Testes unitários
    └── scripts/
        ├── sql/
        │   └── dams-ops.sql          # Health check, backup, performance, índices
        └── powershell/
            └── dams-ops.ps1          # Automação: HealthCheck, Backup, CheckContainer
```

### Princípios Aplicados

- **Separation of Concerns** — cada camada tem responsabilidade única
- **Dependency Inversion** — dependências apontam para abstrações
- **SOLID Principles** — código limpo e manutenível
- **Repository Pattern** — abstração de acesso a dados

---

## 🔐 Autenticação JWT

A API utiliza **JWT Bearer Token** com algoritmo **HMAC-SHA256** para autenticação. O fluxo é:

```
POST /api/auth/register  →  cria usuário com senha hasheada (BCrypt)  →  retorna token
POST /api/auth/login     →  valida credenciais  →  retorna token (expira em 8h)
GET  /api/admissions     →  requer header: Authorization: Bearer {token}
```

### Estrutura do Token

```json
{
  "sub": "guid-do-usuario",
  "email": "usuario@email.com",
  "name": "Nome do Usuário",
  "role": "Admin",
  "jti": "guid-unico-do-token",
  "exp": 1234567890,
  "iss": "DAMS.Api",
  "aud": "DAMS.Client"
}
```

---

## 🗄️ Modelo de Dados

### Entidades Principais

**User**
```
Id:            Guid (PK)
Name:          string
Email:         string
Role:          string
PasswordHash:  string  ← BCrypt hash
```

**Admission**
```
Id:               Guid (PK)
CandidateName:    string
Status:           int (enum)
CreatedAt:        DateTime
CreatedByUserId:  Guid (FK → Users)
Documents:        ICollection<Document>
```

**Document**
```
Id:           Guid (PK)
AdmissionId:  Guid (FK → Admissions)
FileName:     string
FilePath:     string
FileType:     string
UploadedAt:   DateTime
```

### Relacionamentos

- `Admission 1:N Documents` — uma admissão pode ter múltiplos documentos
- `User 1:N Admissions` — um usuário pode criar múltiplas admissões
- Deleção em cascata configurada via EF Core

---

## 🚀 Tecnologias Utilizadas

| Tecnologia | Versão | Uso |
|---|---|---|
| .NET | 10.0 | Framework principal |
| ASP.NET Core | 10.0 | API REST |
| Entity Framework Core | 10.0 | ORM e migrations |
| SQL Server | 2022 | Banco de dados (Docker e Azure) |
| BCrypt.Net-Next | 4.2.0 | Hash de senhas |
| JwtBearer | 10.0.8 | Autenticação JWT |
| xUnit | 2.9.3 | Testes unitários |
| Docker | — | Containerização do SQL Server |
| GitHub Actions | — | CI/CD |
| Microsoft Azure | — | SQL Database em nuvem |

---

## ☁️ Infraestrutura Azure

O banco de dados está provisionado no **Azure SQL Database**:

| Recurso | Nome | Região | Status |
|---|---|---|---|
| Resource Group | `rg-dams` | Brazil South | ✅ Provisionado |
| SQL Server | `dams-sqlserver.database.windows.net` | Brazil South | ✅ Provisionado |
| SQL Database | `DAMS_DB` | Brazil South | ✅ Provisionado |
| App Service | `dams-api` | — | ⏳ Roadmap |

### Connection String Azure

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=dams-sqlserver.database.windows.net;Database=DAMS_DB;User Id=<usuario>;Password=<senha>;TrustServerCertificate=True"
}
```

> ⚠️ Nunca exponha credenciais em repositórios públicos. Use variáveis de ambiente ou Azure Key Vault em produção.

---

## 📦 Pré-requisitos

- .NET SDK 10.0
- Docker Desktop
- Git
- `dotnet-ef`

```bash
dotnet tool install --global dotnet-ef
```

---

## ⚙️ Configuração e Instalação

### 1. Clone o repositório

```bash
git clone https://github.com/AnaC380/digital-admissions-management-system.git
cd digital-admissions-management-system/backend
```

### 2. Configure o `appsettings.json`

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=DAMS_DB;User Id=sa;Password=<sua-senha>;TrustServerCertificate=True"
  },
  "JwtSettings": {
    "SecretKey": "<chave-secreta-minimo-32-caracteres>",
    "Issuer": "DAMS.Api",
    "Audience": "DAMS.Client",
    "ExpiresInHours": "8"
  }
}
```

### 3. Suba o banco de dados

```bash
docker-compose up -d
```

Aguarde ~30 segundos para o SQL Server inicializar.

### 4. Aplique as migrations

```bash
dotnet ef database update --project DAMS.Infrastructure --startup-project DAMS.Api
```

### 5. Execute o projeto

```bash
cd DAMS.Api
dotnet run
```

A API estará disponível em:

- 🌐 `http://localhost:5219`
- 📖 `http://localhost:5219/openapi/v1.json` ← Documentação OpenAPI

---

## 📖 Documentação da API

### Autenticação

| Método | Rota | Descrição | Auth |
|---|---|---|---|
| POST | `/api/auth/register` | Registra novo usuário | ❌ |
| POST | `/api/auth/login` | Autentica e retorna token JWT | ❌ |

### Admissões

| Método | Rota | Descrição | Auth |
|---|---|---|---|
| GET | `/api/admissions` | Lista todas as admissões | ✅ |
| GET | `/api/admissions/{id}` | Busca admissão por ID (GUID) | ✅ |

### Testando com o HTTP Client integrado

O arquivo `DAMS.Api/DAMS.Api.http` permite testar os endpoints diretamente no Visual Studio:

1. Crie o arquivo `DAMS.Api/http-client.env.json` (ignorado pelo `.gitignore`):
```json
{
  "dev": {
    "token": "<token-retornado-pelo-login>"
  }
}
```

2. Selecione o ambiente **dev** no canto superior direito do arquivo `.http`
3. Execute `### Login` e copie o token retornado
4. Cole o token no `http-client.env.json`
5. Execute os endpoints protegidos

### Testando via curl

```bash
# Registrar usuário
curl -X POST http://localhost:5219/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{"name":"Nome","email":"email@exemplo.com","password":"Senha@123","role":"Admin"}'

# Login
curl -X POST http://localhost:5219/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"email":"email@exemplo.com","password":"Senha@123"}'

# Listar admissões (requer token)
curl http://localhost:5219/api/admissions \
  -H "Authorization: Bearer <token>"
```

---

## 🐳 Docker

```bash
# Subir o SQL Server
docker-compose up -d

# Verificar status
docker ps

# Verificar logs
docker logs dams-sqlserver

# Conectar via terminal
docker exec -it dams-sqlserver /opt/mssql-tools18/bin/sqlcmd \
  -S localhost -U sa -P "<senha>" -C

# Parar o container
docker-compose down
```

---

## 🛠️ Scripts de Operação (DBA)

### Health Check e Backup — SQL

```bash
# Via SSMS ou Azure Data Studio
# Abrir: scripts/sql/dams-ops.sql

# Via terminal (Docker)
docker exec -it dams-sqlserver /opt/mssql-tools18/bin/sqlcmd \
  -S localhost -U sa -P "<senha>" -C \
  -i /scripts/sql/dams-ops.sql
```

O script `dams-ops.sql` executa:
- Health check do banco (status, tamanho de tabelas, conexões ativas)
- Backup Full com timestamp automático
- Monitoramento de queries custosas
- Verificação de fragmentação de índices

### Automação PowerShell

```powershell
# Health check completo
.\scripts\powershell\dams-ops.ps1 -Action HealthCheck

# Backup com limpeza automática (> 7 dias)
.\scripts\powershell\dams-ops.ps1 -Action Backup

# Verificar container Docker
.\scripts\powershell\dams-ops.ps1 -Action CheckContainer

# Executar tudo
.\scripts\powershell\dams-ops.ps1 -Action All
```

---

## 🔍 Explorando o Banco de Dados

### Via SSMS ou Azure Data Studio

```
Server: localhost,1433               (local Docker)
        dams-sqlserver.database.windows.net  (Azure)
Authentication: SQL Login
Database: DAMS_DB
```

### Via Terminal

```bash
# Ver tabelas
sqlcmd -S localhost,1433 -U sa -P "<senha>" -C \
  -Q "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE'"

# Ver usuários cadastrados
sqlcmd -S localhost,1433 -U sa -P "<senha>" -C \
  -Q "SELECT Id, Name, Email, Role FROM Users"
```

---

## 🧪 Testes

```bash
# Executar todos os testes
dotnet test

# Com cobertura de código
dotnet test /p:CollectCoverage=true
```

---

## 📊 Migrations

```bash
# Criar nova migration
dotnet ef migrations add NomeDaMigration \
  --project DAMS.Infrastructure --startup-project DAMS.Api

# Aplicar migrations
dotnet ef database update \
  --project DAMS.Infrastructure --startup-project DAMS.Api

# Reverter última migration
dotnet ef migrations remove \
  --project DAMS.Infrastructure --startup-project DAMS.Api

# Ver histórico
dotnet ef migrations list \
  --project DAMS.Infrastructure --startup-project DAMS.Api
```

---

## 🔁 CI/CD — GitHub Actions

O pipeline `.github/workflows/ci.yml` executa automaticamente a cada push/PR na branch `main`:

1. Sobe SQL Server 2022 como service container
2. Restaura dependências e compila a solution
3. Aplica migrations automaticamente
4. Executa todos os testes com relatório de cobertura
5. Valida o `docker-compose.yml`

---

## 📝 Roadmap

- [x] Configuração inicial do projeto
- [x] Implementação das entidades de domínio
- [x] Configuração do Entity Framework Core
- [x] Migrations iniciais
- [x] Implementação dos Controllers
- [x] Documentação da API (OpenAPI)
- [x] Autenticação e Autorização (JWT)
- [x] Scripts de operação de banco de dados (DBA)
- [x] Automação de rotinas operacionais (PowerShell)
- [x] CI/CD com GitHub Actions
- [x] Infraestrutura Azure (SQL Database)
- [ ] POST /api/admissions — criação de admissões
- [ ] Upload de documentos
- [ ] Paginação e filtros nos endpoints
- [ ] Testes de integração
- [ ] Deploy da API em Azure App Service
- [ ] Frontend em React

---

## 🤝 Contribuindo

1. Fork o projeto
2. Crie uma branch: `git checkout -b feature/MinhaFeature`
3. Commit: `git commit -m 'feat: adiciona MinhaFeature'`
4. Push: `git push origin feature/MinhaFeature`
5. Abra um Pull Request

| Prefixo | Uso |
|---|---|
| `feat:` | Nova funcionalidade |
| `fix:` | Correção de bug |
| `docs:` | Documentação |
| `refactor:` | Refatoração |
| `test:` | Testes |
| `chore:` | Manutenção |
| `ops:` | Scripts de operação/infra |

---

## 📄 Licença

Este projeto está sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.

---

## 👩‍💻 Autora

**Ana Carolina**

[![GitHub](https://img.shields.io/badge/GitHub-AnaC380-181717?style=flat-square&logo=github)](https://github.com/AnaC380)

---

⭐ Se este projeto foi útil, considere dar uma estrela no repositório!

*Desenvolvido com foco em operação de ambientes produtivos, infraestrutura em nuvem e boas práticas de DevOps.*