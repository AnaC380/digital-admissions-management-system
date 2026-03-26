\# 🎓 DAMS - Digital Admissions Management System



> Sistema de gerenciamento digital de admissões para instituições de ensino, desenvolvido com .NET 10 e Clean Architecture.




![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=for-the-badge&logo=dotnet)
![SQL Server](https://img.shields.io/badge/SQL_Server-Express-CC2927?style=for-the-badge&logo=microsoftsqlserver)
![Swagger](https://img.shields.io/badge/Swagger-OAS_3.0-85EA2D?style=for-the-badge&logo=swagger)
![License](https://img.shields.io/badge/License-MIT-green?style=for-the-badge)



\---



\## 📋 Sobre o Projeto



O \*\*DAMS\*\* é uma API REST para gerenciar processos de admissão de candidatos em instituições de ensino. O sistema permite controle completo sobre candidatos, documentos e usuários envolvidos no processo seletivo, com rastreabilidade e auditoria em todas as operações.



\---



\## ✨ Funcionalidades



\- ✅ Gerenciamento de candidatos e admissões

\- 📄 Controle de documentos por admissão

\- 👥 Sistema de usuários com perfis e permissões

\- 🔍 Rastreamento de status das admissões

\- 📊 Auditoria completa com timestamps

\- 📖 Documentação interativa via Swagger UI



\---



\## 🏗️ Arquitetura



O projeto segue os princípios de \*\*Clean Architecture\*\* e \*\*Domain-Driven Design (DDD)\*\*:



```

backend/

├── DAMS.Domain/           # Entidades e regras de negócio

├── DAMS.Application/      # Casos de uso e lógica de aplicação

├── DAMS.Infrastructure/   # Persistência e configurações do EF Core

├── DAMS.Api/              # API REST — Controllers e endpoints

└── DAMS.Tests/            # Testes unitários e de integração

```



\### 🎯 Princípios Aplicados



\- \*\*Separation of Concerns\*\* — cada camada tem responsabilidade única

\- \*\*Dependency Inversion\*\* — dependências apontam para abstrações

\- \*\*SOLID Principles\*\* — código limpo e manutenível

\- \*\*Repository Pattern\*\* — abstração de acesso a dados



\---



\## 🗄️ Modelo de Dados



\### Entidades Principais



\*\*Admission\*\*

```

Id:               Guid (PK)

CandidateName:    string

Status:           int (enum)

CreatedAt:        DateTime

CreatedByUserId:  Guid (FK → Users)

Documents:        ICollection<Document>

```



\*\*Document\*\*

```

Id:           Guid (PK)

AdmissionId:  Guid (FK → Admissions)

FileName:     string

FilePath:     string

FileType:     string

UploadedAt:   DateTime

```



\*\*User\*\*

```

Id:     Guid (PK)

Name:   string

Email:  string

Role:   string

```



\### 🔗 Relacionamentos

\- `Admission` \*\*1:N\*\* `Documents` — uma admissão pode ter múltiplos documentos

\- `User` \*\*1:N\*\* `Admissions` — um usuário pode criar múltiplas admissões

\- Deleção em cascata configurada via EF Core



\---



\## 🚀 Tecnologias Utilizadas



| Tecnologia | Versão | Uso |

|---|---|---|

| .NET | 10.0 | Framework principal |

| ASP.NET Core | 10.0 | API REST |

| Entity Framework Core | 10.0 | ORM |

| SQL Server Express | 2022+ | Banco de dados |

| Swashbuckle | 10.1.5 | Swagger UI |

| xUnit | - | Testes |



\---



\## 📦 Pré-requisitos



\- \[.NET SDK 10.0](https://dotnet.microsoft.com/download)

\- \[SQL Server Express](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads) ou \[Docker Desktop](https://www.docker.com/products/docker-desktop/)

\- \[Git](https://git-scm.com/downloads)

\- \[dotnet-ef](https://learn.microsoft.com/pt-br/ef/core/cli/dotnet) (ferramenta de migrations)



\---



\## ⚙️ Configuração e Instalação



\### 1. Clone o repositório



```bash

git clone https://github.com/AnaC380/digital-admissions-management-system.git

cd digital-admissions-management-system/backend

```



\### 2. Instale a ferramenta de migrations



```bash

dotnet tool install --global dotnet-ef

```



\### 3. Configure a string de conexão



Edite o arquivo `DAMS.Api/appsettings.json`:



\*\*Opção A — SQL Server Express (local):\*\*

```json

{

&#x20; "ConnectionStrings": {

&#x20;   "DefaultConnection": "Server=.\\\\SQLEXPRESS;Database=DAMS\_DB;Trusted\_Connection=True;TrustServerCertificate=True;Integrated Security=True;"

&#x20; }

}

```



\*\*Opção B — Docker:\*\*

```json

{

&#x20; "ConnectionStrings": {

&#x20;   "DefaultConnection": "Server=localhost,1433;Database=DAMS\_DB;User Id=sa;Password=SUA_SENHA_AQUI;TrustServerCertificate=True"

&#x20; }

}

```



\### 4. Suba o banco de dados (se usar Docker)



```bash

docker-compose up -d

```

> Aguarde \~30 segundos para o SQL Server inicializar.



\### 5. Aplique as migrations



```bash

dotnet ef database update --project DAMS.Infrastructure --startup-project DAMS.Api

```



\### 6. Execute o projeto



```bash

cd DAMS.Api

dotnet run

```



A API estará disponível em:

\- 🌐 \*\*http://localhost:5219\*\*

\- 🔒 \*\*https://localhost:7076\*\*

\- 📖 \*\*http://localhost:5219/index.html\*\* ← Swagger UI



\---



\## 📖 Documentação da API



Após executar o projeto, acesse o \*\*Swagger UI\*\*:



```

http://localhost:5219/index.html

```



\### Endpoints disponíveis



| Método | Rota | Descrição |

|---|---|---|

| `GET` | `/api/Admissions` | Lista todas as admissões |

| `GET` | `/api/Admissions/{id}` | Busca admissão por ID (GUID) |



\---



\## 🐳 Docker



\### Subir o SQL Server



```bash

docker-compose up -d

```



\### Verificar logs



```bash

docker logs dams-sqlserver

```



\### Conectar via terminal



```bash

docker exec -it dams-sqlserver /opt/mssql-tools18/bin/sqlcmd \\

&#x20; -S localhost -U sa -P "SUA_SENHA_AQUI" -C

```



\### Parar o container



```bash

docker-compose down

```



\---



\## 🧪 Testes



```bash

\# Executar todos os testes

dotnet test



\# Com cobertura de código

dotnet test /p:CollectCoverage=true

```



\---



\## 📊 Migrations



```bash

\# Criar nova migration

dotnet ef migrations add NomeDaMigration \\

&#x20; --project DAMS.Infrastructure --startup-project DAMS.Api



\# Aplicar migrations

dotnet ef database update \\

&#x20; --project DAMS.Infrastructure --startup-project DAMS.Api



\# Reverter última migration

dotnet ef migrations remove \\

&#x20; --project DAMS.Infrastructure --startup-project DAMS.Api



\# Ver histórico

dotnet ef migrations list \\

&#x20; --project DAMS.Infrastructure --startup-project DAMS.Api

```



\---



\## 🔍 Explorando o Banco de Dados



\### Via Azure Data Studio (recomendado)



1\. Baixe o \[Azure Data Studio](https://azure.microsoft.com/pt-br/products/data-studio)

2\. Conecte com:

&#x20;  - \*\*Server:\*\* `.\\SQLEXPRESS` ou `localhost,1433`

&#x20;  - \*\*Authentication:\*\* Windows ou SQL Login

&#x20;  - \*\*Database:\*\* `DAMS\_DB`



\### Via Terminal (sqlcmd)



```bash

\# Ver tabelas

sqlcmd -S .\\SQLEXPRESS -d DAMS\_DB \\

&#x20; -Q "SELECT TABLE\_NAME FROM INFORMATION\_SCHEMA.TABLES"



\# Ver admissões

sqlcmd -S .\\SQLEXPRESS -d DAMS\_DB \\

&#x20; -Q "SELECT \* FROM Admissions"



\# Ver usuários

sqlcmd -S .\\SQLEXPRESS -d DAMS\_DB \\

&#x20; -Q "SELECT \* FROM Users"

```



\---



\## 🤝 Contribuindo



Contribuições são bem-vindas! Siga estes passos:



1\. Fork o projeto

2\. Crie uma branch para sua feature

&#x20;  ```bash

&#x20;  git checkout -b feature/MinhaFeature

&#x20;  ```

3\. Commit suas mudanças

&#x20;  ```bash

&#x20;  git commit -m 'feat: adiciona MinhaFeature'

&#x20;  ```

4\. Push para a branch

&#x20;  ```bash

&#x20;  git push origin feature/MinhaFeature

&#x20;  ```

5\. Abra um \*\*Pull Request\*\*



\### Padrões de Commit



| Prefixo | Uso |

|---|---|

| `feat:` | Nova funcionalidade |

| `fix:` | Correção de bug |

| `docs:` | Documentação |

| `refactor:` | Refatoração |

| `test:` | Testes |

| `chore:` | Manutenção |



\---



\## 📝 Roadmap



\- \[x] Configuração inicial do projeto

\- \[x] Implementação das entidades de domínio

\- \[x] Configuração do Entity Framework Core

\- \[x] Migrations iniciais

\- \[x] Implementação dos Controllers

\- \[x] Documentação da API (Swagger UI)

\- \[ ] Autenticação e Autorização (JWT)

\- \[ ] Upload de documentos

\- \[ ] Paginação e filtros nos endpoints

\- \[ ] Frontend em React

\- \[ ] CI/CD com GitHub Actions

\- \[ ] Testes de integração

\- \[ ] Deploy em Azure



\---



\## 🐛 Problemas Conhecidos



Consulte as \[Issues](https://github.com/AnaC380/digital-admissions-management-system/issues) do projeto.



\---



\## 📄 Licença



Este projeto está sob a licença MIT. Veja o arquivo \[LICENSE](LICENSE) para mais detalhes.



\---



\## 👩‍💻 Autora



\*\*Ana Carolina\*\*



\[!\[GitHub](https://img.shields.io/badge/GitHub-@AnaC380-181717?style=flat\&logo=github)](https://github.com/AnaC380)

\[!\[LinkedIn](https://img.shields.io/badge/LinkedIn-Ana\_Carolina-0A66C2?style=flat\&logo=linkedin)](https://www.linkedin.com/in/ana-carolina-salles-b31a3421a/)



\---



\## 📞 Suporte



Para reportar bugs ou solicitar features, abra uma \[Issue](https://github.com/AnaC380/digital-admissions-management-system/issues).



\---



<div align="center">

&#x20; ⭐ Se este projeto foi útil, considere dar uma estrela no repositório!

&#x20; <br><br>

&#x20; Desenvolvido com ❤️ usando .NET 10 e Clean Architecture

</div>

