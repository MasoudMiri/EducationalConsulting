````markdown
# EducationalConsulting

A comprehensive educational consulting website built with ASP.NET Core MVC. This project provides a platform for educational consulting services, featuring article management, user authentication, and package-based content delivery.

---

## 📖 About The Project

EducationalConsulting is a web application designed for educational consulting services. It allows administrators to manage articles and content while providing users with access to educational materials based on their subscription packages.

The project is currently under active development.

**Current Progress:** ~60% completed

### ✅ Completed Features

- Full article management system (CRUD)
- Admin panel with secure login
- Article categories management
- CKEditor integration with file upload (images/videos)
- Responsive RTL design with modern UI
- Dynamic sidebar with latest articles
- Pagination for article listing
- Blog-style articles with multimedia support

### ⏳ Planned Features

- User authentication system
- Package-based subscription system
- User dashboard
- Payment gateway integration
- Article commenting system
- User management in admin panel

---

## 🏗️ Project Architecture

| Layer          | Responsibility                                    |
| -------------- | ------------------------------------------------- |
| **Repository** | Data access and database operations               |
| **Service**    | Business logic and validation                     |
| **Controller** | Handle requests and return responses              |
| **View**       | Display data to users using Razor Views           |
| **DTOs**       | Data transfer between layers                     |

### Architecture Layers

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

## 🎯 Key Features

### 1️⃣ Article Management System

- Full CRUD operations
- Category-based organization
- Rich text editing with CKEditor
- Image and video upload support

### 2️⃣ Admin Panel

- Secure login system (Session-based)
- Article management interface
- Category management
- Article status (Active/Inactive)

### 3️⃣ Content Display

- Paginated article listing
- Category filtering
- Article details view
- Dynamic sidebar with latest articles
- Responsive RTL design

### 4️⃣ File Upload System

- Image upload for articles
- Video upload support (up to 100MB)
- Secure file storage in `wwwroot/uploads/`

### 5️⃣ Security Features

- Session-based authentication
- Password hashing (planned)
- Role-based access control (planned)
- HTTPS enforcement

---

## 🛠️ Technologies Used

### Backend

- **Framework:** ASP.NET Core MVC (.NET 8)
- **Language:** C#
- **ORM:** Entity Framework Core (Code-First)
- **Database:** SQL Server (LocalDB)
- **Patterns:** Repository, Service, Dependency Injection

### Frontend

- **CSS Framework:** Bootstrap 5 (RTL)
- **Fonts:** Vazir (Persian font)
- **Editor:** CKEditor 4
- **Icons:** Font Awesome
- **JavaScript:** jQuery, Bootstrap JS

### Tools & Version Control

- **Version Control:** Git & GitHub
- **IDE:** Visual Studio 2022

---

## 💡 Key Design Decisions

### 1. Why string `Content`?

Article content is stored as HTML string. This allows:
- Full formatting flexibility
- Embedding images and videos via URLs
- Easy integration with CKEditor
- Simple display with `@Html.Raw()`

### 2. Why Separate Upload Controller?

`UploadController` handles file uploads separately:
- Keeps `AdminController` clean
- Improves maintainability
- Enables reuse across the application

### 3. Why Repository & Service Layers?

- **Repository:** Data access abstraction
- **Service:** Business logic separation
- **Controller:** Request handling only
- **Benefits:** Testable, maintainable, scalable

---

## 🚀 Getting Started

### Prerequisites

- .NET 8.0 SDK
- SQL Server (LocalDB or full version)
- Visual Studio 2022 (recommended)

### Clone Repository

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

## 📸 Screenshots

### صفحه اصلی
![Homepage 1](ScreenShots/homepage1.png)
![Homepage 2](ScreenShots/homepage2.png)
![Homepage 3](ScreenShots/homepage3.png)
![Homepage 4](ScreenShots/homepage4.png)

### بخش مقالات
![Articles 1](ScreenShots/articles1.png)
![Articles 2](ScreenShots/articles2.png)
![Articles 3](ScreenShots/articles3.png)

### پنل ادمین
![Admin 1](ScreenShots/admin1.png)
![Admin 2](ScreenShots/admin2.png)
![Admin 3](ScreenShots/admin3.png)



---

# 📬 Contact

**Masoud Miri**

📧 masoudmiri7667@gmail.com

GitHub

https://github.com/MasoudMiri


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
