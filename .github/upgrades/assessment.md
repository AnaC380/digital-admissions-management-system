# Projects and dependencies analysis

This document provides a comprehensive overview of the projects and their dependencies in the context of upgrading to .NETCoreApp,Version=v10.0.

## Table of Contents

- [Executive Summary](#executive-Summary)
  - [Highlevel Metrics](#highlevel-metrics)
  - [Projects Compatibility](#projects-compatibility)
  - [Package Compatibility](#package-compatibility)
  - [API Compatibility](#api-compatibility)
- [Aggregate NuGet packages details](#aggregate-nuget-packages-details)
- [Top API Migration Challenges](#top-api-migration-challenges)
  - [Technologies and Features](#technologies-and-features)
  - [Most Frequent API Issues](#most-frequent-api-issues)
- [Projects Relationship Graph](#projects-relationship-graph)
- [Project Details](#project-details)

  - [DAMS.Api\DAMS.Api.csproj](#damsapidamsapicsproj)
  - [DAMS.Application\DAMS.Application.csproj](#damsapplicationdamsapplicationcsproj)
  - [DAMS.Domain\DAMS.Domain.csproj](#damsdomaindamsdomaincsproj)
  - [DAMS.Infrastructure\DAMS.Infrastructure.csproj](#damsinfrastructuredamsinfrastructurecsproj)
  - [DAMS.Tests\DAMS.Tests.csproj](#damstestsdamstestscsproj)


## Executive Summary

### Highlevel Metrics

| Metric | Count | Status |
| :--- | :---: | :--- |
| Total Projects | 5 | 0 require upgrade |
| Total NuGet Packages | 13 | All compatible |
| Total Code Files | 15 |  |
| Total Code Files with Incidents | 0 |  |
| Total Lines of Code | 786 |  |
| Total Number of Issues | 0 |  |
| Estimated LOC to modify | 0+ | at least 0,0% of codebase |

### Projects Compatibility

| Project | Target Framework | Difficulty | Package Issues | API Issues | Est. LOC Impact | Description |
| :--- | :---: | :---: | :---: | :---: | :---: | :--- |
| [DAMS.Api\DAMS.Api.csproj](#damsapidamsapicsproj) | net10.0 | ✅ None | 0 | 0 |  | AspNetCore, Sdk Style = True |
| [DAMS.Application\DAMS.Application.csproj](#damsapplicationdamsapplicationcsproj) | net10.0 | ✅ None | 0 | 0 |  | ClassLibrary, Sdk Style = True |
| [DAMS.Domain\DAMS.Domain.csproj](#damsdomaindamsdomaincsproj) | net10.0 | ✅ None | 0 | 0 |  | ClassLibrary, Sdk Style = True |
| [DAMS.Infrastructure\DAMS.Infrastructure.csproj](#damsinfrastructuredamsinfrastructurecsproj) | net10.0 | ✅ None | 0 | 0 |  | ClassLibrary, Sdk Style = True |
| [DAMS.Tests\DAMS.Tests.csproj](#damstestsdamstestscsproj) | net10.0 | ✅ None | 0 | 0 |  | DotNetCoreApp, Sdk Style = True |

### Package Compatibility

| Status | Count | Percentage |
| :--- | :---: | :---: |
| ✅ Compatible | 13 | 100,0% |
| ⚠️ Incompatible | 0 | 0,0% |
| 🔄 Upgrade Recommended | 0 | 0,0% |
| ***Total NuGet Packages*** | ***13*** | ***100%*** |

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 0 | High - Require code changes |
| 🟡 Source Incompatible | 0 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 0 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 0 |  |
| ***Total APIs Analyzed*** | ***0*** |  |

## Aggregate NuGet packages details

| Package | Current Version | Suggested Version | Projects | Description |
| :--- | :---: | :---: | :--- | :--- |
| coverlet.collector | 6.0.4 |  | [DAMS.Tests.csproj](#damstestsdamstestscsproj) | ✅Compatible |
| Microsoft.AspNetCore.OpenApi | 10.0.1 |  | [DAMS.Api.csproj](#damsapidamsapicsproj) | ✅Compatible |
| Microsoft.EntityFrameworkCore | 10.0.1 |  | [DAMS.Infrastructure.csproj](#damsinfrastructuredamsinfrastructurecsproj) | ✅Compatible |
| Microsoft.EntityFrameworkCore.Design | 10.0.1 |  | [DAMS.Api.csproj](#damsapidamsapicsproj) | ✅Compatible |
| Microsoft.EntityFrameworkCore.SqlServer | 10.0.1 |  | [DAMS.Infrastructure.csproj](#damsinfrastructuredamsinfrastructurecsproj) | ✅Compatible |
| Microsoft.EntityFrameworkCore.Tools | 10.0.1 |  | [DAMS.Infrastructure.csproj](#damsinfrastructuredamsinfrastructurecsproj) | ✅Compatible |
| Microsoft.Extensions.Configuration | 10.0.2 |  | [DAMS.Infrastructure.csproj](#damsinfrastructuredamsinfrastructurecsproj) | ✅Compatible |
| Microsoft.Extensions.Configuration.FileExtensions | 10.0.2 |  | [DAMS.Infrastructure.csproj](#damsinfrastructuredamsinfrastructurecsproj) | ✅Compatible |
| Microsoft.Extensions.Configuration.Json | 10.0.2 |  | [DAMS.Infrastructure.csproj](#damsinfrastructuredamsinfrastructurecsproj) | ✅Compatible |
| Microsoft.NET.Test.Sdk | 17.14.1 |  | [DAMS.Tests.csproj](#damstestsdamstestscsproj) | ✅Compatible |
| Swashbuckle.AspNetCore | 10.1.5 |  | [DAMS.Api.csproj](#damsapidamsapicsproj) | ✅Compatible |
| xunit | 2.9.3 |  | [DAMS.Tests.csproj](#damstestsdamstestscsproj) | ✅Compatible |
| xunit.runner.visualstudio | 3.1.4 |  | [DAMS.Tests.csproj](#damstestsdamstestscsproj) | ✅Compatible |

## Top API Migration Challenges

### Technologies and Features

| Technology | Issues | Percentage | Migration Path |
| :--- | :---: | :---: | :--- |

### Most Frequent API Issues

| API | Count | Percentage | Category |
| :--- | :---: | :---: | :--- |

## Projects Relationship Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart LR
    P1["<b>📦&nbsp;DAMS.Api.csproj</b><br/><small>net10.0</small>"]
    P2["<b>📦&nbsp;DAMS.Application.csproj</b><br/><small>net10.0</small>"]
    P3["<b>📦&nbsp;DAMS.Domain.csproj</b><br/><small>net10.0</small>"]
    P4["<b>📦&nbsp;DAMS.Infrastructure.csproj</b><br/><small>net10.0</small>"]
    P5["<b>📦&nbsp;DAMS.Tests.csproj</b><br/><small>net10.0</small>"]
    P1 --> P4
    P1 --> P2
    P2 --> P3
    P4 --> P3
    click P1 "#damsapidamsapicsproj"
    click P2 "#damsapplicationdamsapplicationcsproj"
    click P3 "#damsdomaindamsdomaincsproj"
    click P4 "#damsinfrastructuredamsinfrastructurecsproj"
    click P5 "#damstestsdamstestscsproj"

```

## Project Details

<a id="damsapidamsapicsproj"></a>
### DAMS.Api\DAMS.Api.csproj

#### Project Info

- **Current Target Framework:** net10.0✅
- **SDK-style**: True
- **Project Kind:** AspNetCore
- **Dependencies**: 2
- **Dependants**: 0
- **Number of Files**: 50
- **Lines of Code**: 152
- **Estimated LOC to modify**: 0+ (at least 0,0% of the project)

#### Dependency Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart TB
    subgraph current["DAMS.Api.csproj"]
        MAIN["<b>📦&nbsp;DAMS.Api.csproj</b><br/><small>net10.0</small>"]
        click MAIN "#damsapidamsapicsproj"
    end
    subgraph downstream["Dependencies (2"]
        P4["<b>📦&nbsp;DAMS.Infrastructure.csproj</b><br/><small>net10.0</small>"]
        P2["<b>📦&nbsp;DAMS.Application.csproj</b><br/><small>net10.0</small>"]
        click P4 "#damsinfrastructuredamsinfrastructurecsproj"
        click P2 "#damsapplicationdamsapplicationcsproj"
    end
    MAIN --> P4
    MAIN --> P2

```

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 0 | High - Require code changes |
| 🟡 Source Incompatible | 0 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 0 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 0 |  |
| ***Total APIs Analyzed*** | ***0*** |  |

<a id="damsapplicationdamsapplicationcsproj"></a>
### DAMS.Application\DAMS.Application.csproj

#### Project Info

- **Current Target Framework:** net10.0✅
- **SDK-style**: True
- **Project Kind:** ClassLibrary
- **Dependencies**: 1
- **Dependants**: 1
- **Number of Files**: 1
- **Lines of Code**: 11
- **Estimated LOC to modify**: 0+ (at least 0,0% of the project)

#### Dependency Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart TB
    subgraph upstream["Dependants (1)"]
        P1["<b>📦&nbsp;DAMS.Api.csproj</b><br/><small>net10.0</small>"]
        click P1 "#damsapidamsapicsproj"
    end
    subgraph current["DAMS.Application.csproj"]
        MAIN["<b>📦&nbsp;DAMS.Application.csproj</b><br/><small>net10.0</small>"]
        click MAIN "#damsapplicationdamsapplicationcsproj"
    end
    subgraph downstream["Dependencies (1"]
        P3["<b>📦&nbsp;DAMS.Domain.csproj</b><br/><small>net10.0</small>"]
        click P3 "#damsdomaindamsdomaincsproj"
    end
    P1 --> MAIN
    MAIN --> P3

```

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 0 | High - Require code changes |
| 🟡 Source Incompatible | 0 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 0 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 0 |  |
| ***Total APIs Analyzed*** | ***0*** |  |

<a id="damsdomaindamsdomaincsproj"></a>
### DAMS.Domain\DAMS.Domain.csproj

#### Project Info

- **Current Target Framework:** net10.0✅
- **SDK-style**: True
- **Project Kind:** ClassLibrary
- **Dependencies**: 0
- **Dependants**: 2
- **Number of Files**: 4
- **Lines of Code**: 137
- **Estimated LOC to modify**: 0+ (at least 0,0% of the project)

#### Dependency Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart TB
    subgraph upstream["Dependants (2)"]
        P2["<b>📦&nbsp;DAMS.Application.csproj</b><br/><small>net10.0</small>"]
        P4["<b>📦&nbsp;DAMS.Infrastructure.csproj</b><br/><small>net10.0</small>"]
        click P2 "#damsapplicationdamsapplicationcsproj"
        click P4 "#damsinfrastructuredamsinfrastructurecsproj"
    end
    subgraph current["DAMS.Domain.csproj"]
        MAIN["<b>📦&nbsp;DAMS.Domain.csproj</b><br/><small>net10.0</small>"]
        click MAIN "#damsdomaindamsdomaincsproj"
    end
    P2 --> MAIN
    P4 --> MAIN

```

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 0 | High - Require code changes |
| 🟡 Source Incompatible | 0 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 0 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 0 |  |
| ***Total APIs Analyzed*** | ***0*** |  |

<a id="damsinfrastructuredamsinfrastructurecsproj"></a>
### DAMS.Infrastructure\DAMS.Infrastructure.csproj

#### Project Info

- **Current Target Framework:** net10.0✅
- **SDK-style**: True
- **Project Kind:** ClassLibrary
- **Dependencies**: 1
- **Dependants**: 1
- **Number of Files**: 7
- **Lines of Code**: 476
- **Estimated LOC to modify**: 0+ (at least 0,0% of the project)

#### Dependency Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart TB
    subgraph upstream["Dependants (1)"]
        P1["<b>📦&nbsp;DAMS.Api.csproj</b><br/><small>net10.0</small>"]
        click P1 "#damsapidamsapicsproj"
    end
    subgraph current["DAMS.Infrastructure.csproj"]
        MAIN["<b>📦&nbsp;DAMS.Infrastructure.csproj</b><br/><small>net10.0</small>"]
        click MAIN "#damsinfrastructuredamsinfrastructurecsproj"
    end
    subgraph downstream["Dependencies (1"]
        P3["<b>📦&nbsp;DAMS.Domain.csproj</b><br/><small>net10.0</small>"]
        click P3 "#damsdomaindamsdomaincsproj"
    end
    P1 --> MAIN
    MAIN --> P3

```

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 0 | High - Require code changes |
| 🟡 Source Incompatible | 0 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 0 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 0 |  |
| ***Total APIs Analyzed*** | ***0*** |  |

<a id="damstestsdamstestscsproj"></a>
### DAMS.Tests\DAMS.Tests.csproj

#### Project Info

- **Current Target Framework:** net10.0✅
- **SDK-style**: True
- **Project Kind:** DotNetCoreApp
- **Dependencies**: 0
- **Dependants**: 0
- **Number of Files**: 3
- **Lines of Code**: 10
- **Estimated LOC to modify**: 0+ (at least 0,0% of the project)

#### Dependency Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart TB
    subgraph current["DAMS.Tests.csproj"]
        MAIN["<b>📦&nbsp;DAMS.Tests.csproj</b><br/><small>net10.0</small>"]
        click MAIN "#damstestsdamstestscsproj"
    end

```

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 0 | High - Require code changes |
| 🟡 Source Incompatible | 0 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 0 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 0 |  |
| ***Total APIs Analyzed*** | ***0*** |  |

