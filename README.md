````markdown
# EducationalConsulting

A comprehensive educational consulting website built with **ASP.NET Core MVC**. This project provides
a platform for educational consulting services, featuring article management, user authentication,
 and package-based content delivery.

---

# 📖 About The Project

EducationalConsulting is a web application designed for educational consulting services. It allows administrators
 to manage articles and content while providing users with access
to educational materials based on their subscription packages.

> 🚧 **Current Status:** Under Active Development

**Current Progress:** **~60% Complete**

## ✅ Completed Features

- Full Article Management System (CRUD)
- Secure Admin Panel Login
- Article Category Management
- CKEditor 4 Integration
- Image & Video Upload
- Responsive RTL Design
- Dynamic Sidebar
- Pagination
- Blog-style Articles

## ⏳ Planned Features

- User Authentication
- Package Subscription System
- User Dashboard
- Payment Gateway
- Comment System
- User Management

---

# 🏗️ Project Architecture

| Layer | Responsibility |
|--------|---------------|
| **Repository** | Database access |
| **Service** | Business logic |
| **Controller** | Request handling |
| **View** | UI (Razor Views) |
| **DTOs** | Data transfer |

## Architecture Flow

```text
Controller
     │
     ▼
Service
     │
     ▼
Repository
     │
     ▼
Database

Views ◄──────── DTOs ───────► Models
```

---

# 📂 Project Structure

```text
EducationalConsulting
│
├── Controllers
│   ├── AdminController.cs
│   ├── ArticlesController.cs
│   ├── HomeController.cs
│   └── UploadController.cs
│
├── Data
│   ├── ApplicationDbContext.cs
│   ├── IArticleRepository.cs
│   ├── ICategoryRepository.cs
│   ├── ArticleRepository.cs
│   └── CategoryRepository.cs
│
├── DTOs
│   ├── AdminArticlesViewModel.cs
│   ├── ArticleCreateDto.cs
│   ├── ArticleDetailDto.cs
│   ├── ArticleListDto.cs
│   ├── ArticleUpdateDto.cs
│   ├── CategoryDto.cs
│   ├── LoginDto.cs
│   ├── PaginationDto.cs
│   └── ServiceResult.cs
│
├── Models
│   ├── Article.cs
│   ├── Category.cs
│   └── ErrorViewModel.cs
│
├── Services
│   ├── IAdminService.cs
│   ├── IArticleService.cs
│   ├── IAuthService.cs
│   ├── IFileService.cs
│   ├── AdminService.cs
│   ├── ArticleService.cs
│   ├── AuthService.cs
│   └── FileService.cs
│
├── Views
│   ├── Admin
│   │   ├── Articles.cshtml
│   │   ├── CreateArticle.cshtml
│   │   ├── EditArticle.cshtml
│   │   ├── Index.cshtml
│   │   └── Login.cshtml
│   │
│   ├── Articles
│   │   ├── Details.cshtml
│   │   └── ListByCategory.cshtml
│   │
│   ├── Home
│   │   ├── Index.cshtml
│   │   └── ...
│   │
│   └── Shared
│       ├── _Layout.cshtml
│       ├── _Header.cshtml
│       ├── _Footer.cshtml
│       ├── _SidebarArticles.cshtml
│       └── ...
│
├── Components
│   └── ImportantArticlesViewComponent.cs
│
├── wwwroot
│   ├── css
│   ├── js
│   ├── images
│   ├── ckeditor4
│   └── uploads
│       └── articles
│           ├── images
│           └── videos
│
├── Program.cs
├── appsettings.json
└── EducationalConsulting.csproj
```

---

# 🎯 Key Features

## 📄 Article Management

- Create
- Read
- Update
- Delete
- Category Management
- Rich Text Editor
- Image Upload
- Video Upload

---

## 👨‍💼 Admin Panel

- Session-based Authentication
- Manage Articles
- Manage Categories
- Active / Inactive Status

---

## 📚 Content Display

- Pagination
- Category Filtering
- Article Details
- Latest Articles Sidebar
- Responsive RTL Layout

---

## 📤 File Upload

- Image Upload
- Video Upload (100MB)
- Secure Storage

```
wwwroot/uploads/
```

---

## 🔒 Security

- Session Authentication
- HTTPS
- Password Hashing *(Planned)*
- Role-based Authorization *(Planned)*

---

# 🛠️ Technologies Used

## Backend

- ASP.NET Core MVC (.NET 8)
- C#
- Entity Framework Core
- SQL Server (LocalDB)
- Repository Pattern
- Service Layer
- Dependency Injection

---

## Frontend

- Bootstrap 5 RTL
- jQuery
- Bootstrap JS
- CKEditor 4
- Font Awesome
- Vazir Font

---

## Development Tools

- Visual Studio 2022
- Git
- GitHub
- Swagger
- Postman

---

# 💡 Design Decisions

## Why store `Content` as string?

The article body is stored as HTML.

Advantages:

- Supports rich formatting
- Easy CKEditor integration
- Images & videos
- Simple rendering using:

```cshtml
@Html.Raw(Model.Content)
```

---

## Why UploadController?

A dedicated controller:

- Better separation of concerns
- Cleaner AdminController
- Easier maintenance
- Reusable upload logic

---

## Why Repository + Service Pattern?

Repository

- Database operations only

Service

- Business logic

Controller

- Request handling

Benefits:

- Clean Architecture
- Easy Testing
- Better Maintainability
- Scalability

---

# 🚀 Getting Started

## Prerequisites

- .NET 8 SDK
- SQL Server (LocalDB)
- Visual Studio 2022

---

## Clone Repository

```bash
git clone https://github.com/MasoudMiri/EducationalConsulting.git
```

---

## Restore Packages

```bash
dotnet restore
```

---

## Configure Database

Update **appsettings.json**

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=EducationalConsultingDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
}
```

---

## Apply Migrations

```bash
dotnet ef database update
```

---

## Run

```bash
dotnet run
```

Visit:

```
https://localhost:xxxx/
```

---

# 🔑 Admin Login

| Username | Password |
|-----------|----------|
| admin | 123456 |

---

# 🗄️ Database Schema

## Articles

| Column | Type | Description |
|--------|------|-------------|
| Id | int | Primary Key |
| Title | nvarchar | Article Title |
| Summary | nvarchar | Short Description |
| Content | nvarchar | HTML Content |
| ImageUrl | nvarchar | Cover Image |
| IsActive | bit | Status |
| ViewCount | int | Views |
| CreateDate | datetime | Created |
| LastModifiedDate | datetime | Updated |
| CategoryId | int | Foreign Key |

---

## Categories

| Column | Type | Description |
|--------|------|-------------|
| Id | int | Primary Key |
| Name | nvarchar | Category Name |
| IsActive | bit | Status |
| CreateDate | datetime | Created |

---

## Relationship

```
Category (1)
      │
      │
      ▼
Articles (Many)
```

---

# ✅ Features Checklist

| Feature | Status |
|---------|--------|
| Article CRUD | ✅ |
| Categories | ✅ |
| Admin Panel | ✅ |
| CKEditor | ✅ |
| Image Upload | ✅ |
| Video Upload | ✅ |
| Pagination | ✅ |
| Sidebar | ✅ |
| Responsive | ✅ |
| RTL | ✅ |
| Authentication | ⏳ |
| Package System | ⏳ |
| User Dashboard | ⏳ |
| Comments | ⏳ |

---

# 📷 Screenshots

Screenshots will be added after the project is completed.

---

# 🤝 Contributing

Contributions are welcome.

```bash
Fork the repository

Create a new branch
git checkout -b feature/AmazingFeature

Commit changes
git commit -m "Add AmazingFeature"

Push branch
git push origin feature/AmazingFeature

Open a Pull Request
```

---

# 📬 Contact

**Masoud Miri**

📧 masoudmiri7667@gmail.com

GitHub

https://github.com/MasoudMiri

LinkedIn

https://linkedin.com

---

# 🙏 Acknowledgments

- ASP.NET Core Documentation
- Bootstrap RTL
- CKEditor Team
- Font Awesome

---

# ⭐ Support

If you found this project useful, please consider giving it a ⭐ on GitHub.

---

# 📝 License

This project is intended for educational and portfolio purposes.

---

<div align="center">

Made with by **Masoud Miri**

</div>
````
