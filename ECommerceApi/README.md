# 🛒 E-Commerce API

A production-inspired **ASP.NET Core 8 Web API** that powers an online shopping platform. This project demonstrates modern backend development practices including JWT Authentication, Role-Based Authorization, Repository Pattern, Generic Repository, Entity Framework Core, FluentValidation, AutoMapper, Global Exception Handling, Caching, Logging, Unit Testing, and API Versioning.

This project was built to simulate how a real-world e-commerce backend is structured while following clean coding principles and scalable architecture.

---

# 🚀 Features

## Authentication & Authorization

- User Registration
- User Login
- JWT Authentication
- Role-Based Authorization
- Password Hashing using BCrypt
- Secure API Endpoints

---

## Product Management

- Create Product
- Update Product
- Delete Product
- Get Product by Id
- Get All Products
- Upload Product Images
- Product Validation

---

## Shopping Cart

- Add Product to Cart
- Update Cart Quantity
- Remove Item from Cart
- View User Cart
- Clear Cart

---

## Order Management

- Checkout
- Order Creation
- Order History
- Transaction Management
- Automatic Stock Reduction

---

## Search & Filtering

- Search Products
- Filter by Price
- Sort Products
- Pagination

---

## Security

- JWT Authentication
- Role-Based Authorization
- Password Encryption
- Global Exception Handling

---

## Performance

- In-Memory Caching
- Structured Logging
- Database Transactions

---

## Software Engineering Practices

- Repository Pattern
- Generic Repository
- Dependency Injection
- Service Layer Architecture
- AutoMapper
- FluentValidation
- DTO Pattern
- API Versioning
- Unit Testing

---

# 🛠 Technologies Used

- ASP.NET Core 8 Web API
- C#
- Entity Framework Core
- SQL Server
- AutoMapper
- FluentValidation
- JWT Authentication
- Swagger / OpenAPI
- xUnit
- Moq
- Memory Cache
- BCrypt.Net

---

# 📂 Project Structure

```
ECommerceApi
│
├── Controllers
├── Services
│   ├── Interfaces
│   └── Implementations
│
├── Repositories
│   ├── Interfaces
│   └── Implementations
│
├── Models
├── DTOs
├── Validators
├── Middleware
├── Mapping
├── Data
├── Migrations
├── Helpers
├── Extensions
├── wwwroot
├── Program.cs
└── appsettings.json
```

---

# 🏗 Architecture

This project follows a layered architecture.

```
Client
   │
   ▼
Controllers
   │
   ▼
Services
   │
   ▼
Repositories
   │
   ▼
Entity Framework Core
   │
   ▼
SQL Server
```

### Controller Layer

Receives HTTP requests and returns responses.

### Service Layer

Contains business logic.

### Repository Layer

Handles database operations.

### Data Layer

Entity Framework Core DbContext and database configurations.

---

# 🗄 Database Entities

The application currently contains the following entities:

- User
- Product
- Cart
- CartItem
- Order
- OrderItem

Relationships are implemented using Entity Framework Core navigation properties.

---

# 🔐 Authentication

The API uses JWT Bearer Authentication.

### Register

```
POST /api/Auth/register
```

### Login

```
POST /api/Auth/login
```

After logging in, copy the generated JWT token.

Click **Authorize** in Swagger and enter:

```
Bearer YOUR_TOKEN
```

---

# 📦 Product Endpoints

| Method | Endpoint | Description |
|---------|----------|-------------|
| GET | /api/v1/Product | Get all products |
| GET | /api/v1/Product/{id} | Get product by Id |
| POST | /api/v1/Product | Create product |
| PUT | /api/v1/Product/{id} | Update product |
| DELETE | /api/v1/Product/{id} | Delete product |

---

# 🛒 Cart Endpoints

| Method | Endpoint |
|---------|----------|
| GET | /api/Cart |
| POST | /api/Cart/add |
| PUT | /api/Cart/update |
| DELETE | /api/Cart/remove |
| DELETE | /api/Cart/clear |

---

# 📋 Order Endpoints

| Method | Endpoint |
|---------|----------|
| POST | /api/Order/checkout |
| GET | /api/Order/history |

---

# 📄 API Features

✔ JWT Authentication

✔ Role-Based Authorization

✔ Global Exception Handling

✔ Pagination

✔ Searching

✔ Filtering

✔ Sorting

✔ Product Image Upload

✔ AutoMapper

✔ FluentValidation

✔ Generic Repository

✔ Repository Pattern

✔ Dependency Injection

✔ Logging

✔ Memory Caching

✔ Transactions

✔ Unit Testing

✔ API Versioning

---

# 🧪 Testing

The project includes unit tests for the service layer using:

- xUnit
- Moq
- FluentAssertions

The tests verify business logic without requiring a live SQL Server database.

---

# ⚡ Logging

The application uses ASP.NET Core's built-in ILogger for structured logging.

Logging is implemented for:

- Product operations
- Authentication
- Checkout process
- Exceptions
- Important application events

---

# ⚡ Caching

The project uses IMemoryCache to improve performance by reducing unnecessary database queries for frequently accessed data.

---

# 🔄 Transactions

Database transactions are implemented during checkout to ensure:

- Orders are created successfully.
- Stock quantities are updated correctly.
- Partial failures are rolled back automatically.

---

# 🌐 API Versioning

The project supports URL-based API versioning.

Example:

```
/api/v1/Product
```

This allows future versions of the API to be introduced without breaking existing clients.

---

# ▶ Getting Started

## Clone the Repository

```bash
git clone https://github.com/YOUR_GITHUB_USERNAME/ECommerceApi.git
```

---

## Navigate to the Project

```bash
cd ECommerceApi
```

---

## Restore Packages

```bash
dotnet restore
```

---

## Configure Database

Update the connection string in:

```
appsettings.json
```

Example:

```json
"ConnectionStrings": {
  "DefaultConnection": "Your SQL Server Connection String"
}
```

---

## Configure JWT

Update:

```json
"Jwt": {
  "Key": "YourSecretKey",
  "Issuer": "YourIssuer",
  "Audience": "YourAudience"
}
```

---

## Apply Migrations

```bash
dotnet ef database update
```

---

## Run the Project

```bash
dotnet run
```

Swagger will be available at:

```
https://localhost:xxxx/swagger
```

---

# 📸 Screenshots

You can add screenshots here such as:

- Swagger UI
- SQL Server Database
- Folder Structure
- API Responses

---

# 🚀 Future Improvements

The following features can be added in future versions:

- Payment Gateway Integration
- Email Notifications
- Redis Distributed Cache
- Docker Support
- Azure Blob Storage
- Background Jobs
- Wishlist
- Product Reviews & Ratings
- Coupons & Discounts
- Admin Dashboard
- Real-Time Notifications with SignalR

---

# 💡 What I Learned

This project strengthened my understanding of:

- ASP.NET Core Web API
- Clean API Design
- Authentication & Authorization
- Entity Framework Core
- SQL Server
- Repository Pattern
- Dependency Injection
- Validation
- Exception Handling
- Logging
- Caching
- Unit Testing
- API Versioning
- RESTful API Best Practices

---

# 👨‍💻 Author

**Sunday Adegboye**

Backend Developer | ASP.NET Core | C# | SQL Server

GitHub:
https://github.com/YOUR_GITHUB_USERNAME

LinkedIn:
https://www.linkedin.com/in/YOUR_LINKEDIN_PROFILE

---

# ⭐ Support

If you found this project helpful or interesting, consider giving it a ⭐ on GitHub.