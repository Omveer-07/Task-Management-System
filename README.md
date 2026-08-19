# Task Management System

A web-based **Task Management System** built with ASP.NET Core MVC to manage departments, projects, tasks, users, deadlines, and task progress.

## Features

* User registration and login
* Role-based access control
* Admin and Employee dashboards
* Department management
* Project management
* Task creation and management
* Task priority and status
* Due-date tracking
* Employee task access
* MySQL database integration
* Entity Framework Core migrations

## User Roles

### Admin

* Dashboard
* Manage departments
* Manage projects
* Manage tasks
* Manage users/employees

### Employee

* Dashboard
* View and manage assigned tasks

## Technology Stack

* C#
* ASP.NET Core MVC
* .NET
* Entity Framework Core
* ASP.NET Core Identity
* MySQL
* Razor Views
* HTML / CSS / Bootstrap
* Git & GitHub

## Project Structure

```text
TaskManagementSystem/
│
├── Controllers/
├── Models/
├── Views/
├── Data/
├── Areas/
│   └── Identity/
├── Migrations/
├── wwwroot/
├── Program.cs
├── appsettings.json
└── TaskManagementSystem.csproj
```

## Database Structure

```text
Department
    │
    └── Project
           │
           └── Task

User
    │
    └── Assigned Tasks
```

## Getting Started

### Clone the Repository

```bash
git clone <repository-url>
cd TaskManagementSystem
```

### Configure Database

Update the MySQL connection string in `appsettings.json`.

```json
"ConnectionStrings": {
  "DefaultConnection": "server=localhost;port=3306;database=TaskManagementSystem;user=root;password=YOUR_PASSWORD;"
}
```

### Restore and Run

```bash
dotnet restore
dotnet ef database update
dotnet run
```

Open the URL shown in the terminal.

## Development Workflow

```text
Model → DbContext → Migration → Database
                         ↓
                    Controller
                         ↓
                       View
                         ↓
                      Testing
```

Create a migration after changing models:

```bash
dotnet ef migrations add MigrationName
dotnet ef database update
```

## Future Improvements

* Task comments and progress updates
* File attachments
* Notifications and reminders
* Search and filtering
* Dashboard analytics
* Department-wise reports
* Employee workload reports
* Overdue task alerts

## Author

**Omveer Singh**
B.Tech — ICFAI University, Jaipur

## License

This project is developed for educational and project purposes.
