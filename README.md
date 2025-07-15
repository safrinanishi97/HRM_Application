# HRM Management System

A full-featured Human Resource Management (HRM) system built using a 3-layered architecture with .NET 9 Web API and Angular frontend(planned).

---

## Project Architecture

### Backend (.NET 9 Web API)

- **3-Layered Architecture**

  - **Presentation Layer**: API Controllers
  - **Application Layer (BLL)**: Interfaces & Services
  - **Data Access Layer (DAL)**: Repository Interfaces & Implementations

- **Technology Stack**:
  - ASP.NET Core 9 Web API
  - Entity Framework Core (Database First)
  - SQL Server
  - Dependency Injection
  - Layered Class Libraries:
    - `HRMApiApp.Models`
    - `HRMApiApp.DTO`
    - `HRMApiApp.ViewModels`
    - `HRMApiApp.DAL`
    - `HRMApiApp.BLL`

## Key Features (Backend)

- Employee CRUD with Master-Detail Support
- Clean DTO and Entity Mapping (Separation of Concerns)
- Pagination/Filtering Ready Structure
- Service and Repository Layering
- GET Controllers for All Base/Lookup Tables
  Used to populate dropdowns dynamically in the frontend (Department, Designation, Gender, etc.)

---

## Planned Features (To Be Implemented)

- Pagination and Filtering Support for List APIs

- Image Upload Support (e.g., Employee photo)

- JWT Authentication and Role-based Authorization

- Angular + Kendo UI Frontend with Dynamic Dropdown Binding

## Getting Started

### Prerequisites

- [.NET SDK 9 Preview](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- Visual Studio 2022 / VS Code
- SQL Server
- Angular CLI

### Clone the Repository

```bash
git clone https://github.com/safrinanishi97/HRM_Application.git
```

### Contributing

Feel free to fork and raise pull requests. If you find bugs, open an issue with detailed explanation.
