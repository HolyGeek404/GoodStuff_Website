# 🚀 GoodStuff IT Hardware Shop

Welcome to the GoodStuff IT Hardware Shop project! This solution is composed of four repositories that together provide a web application for an online IT shop.
Built with Blazor, ASP.NET WebApi and Azure Cloud.

---

## 🔎 Repositories Overview

### 1. 🌐 [GoodStuff_Website](https://github.com/HolyGeek404/GoodStuff_Website)
**Main Website**

- **Description:**  
  The GoodStuff Website is the main frontend for the IT shop. It is built using Blazor, providing a modern, interactive user interface for customers to browse, register, and purchase products.
- **Architecture**
  - Components
  - Domain models
  - SOLID services with business logic
  - Builder DP (for Http Request message)
- **Technologies:**
  - .Net 9 
  - Blazor
  - Bootstrap 4
  - Docker (for deployment)
  - JWT Token (for auth)
- **Features:**  
  - User authentication and registration
  - Product catalog display
  - Shopping cart
  - Integration with User, Order and Product APIs
- **Azure:**
  - **Key Vault:** Stores Client Secret for client credential authorization type.
  - **App Registry:** Contains basic app's setup and identity in the cloud.
  - **App Roles:** Used for authenticate to the APIs. 
---

### 2. ⚙️ [GoodStuff_UserApi](https://github.com/HolyGeek404/GoodStuff_UserApi)
**User API**

- **Description:**  
  The User API manages user data and authentication for the GoodStuff IT shop. It provides endpoints for user registration, login and profile management.
- **Architecture:**
  - Controllers
  - SOLID services
  - CQRS with MediatR (for validating request and managing domain logic)
  - Domain models
- **Technologies:**
  - .Net 9 
  - ASP.NET Core WebApi
  - Docker (for deployment)
- **Features:**  
  - Secure user authentication (JWT)
  - User profile management
  - Role-based access control
- **Azure**
  - **Key Vault:** Stores Client Secrect and connection string to the database. 

---

### 3. ⚙️ [GoodStuff_ProductApi](https://github.com/HolyGeek404/GoodStuff_ProductApi)
**Product API Backend**

- **Description:**  
  The Product API handles all product-related operations, including product listing, creation, updating, and deletion. It serves product data to the frontend and other services.
- **Technologies:**  
  - ASP.NET Core WebApi (C#)
- **Features:**  
  - CRUD operations for products
  - Product search and filtering
  - Integration with the main website frontend

---

## 📜 Architecture Diagram
![obraz](https://github.com/user-attachments/assets/5220949c-5de5-4822-bd21-6ada3fece0c2)


---

## 🚩 Getting Started

### 1. Clone the Repositories

```bash
git clone https://github.com/HolyGeek404/GoodStuff_Website.git
git clone https://github.com/HolyGeek404/GoodStuff_UserApi.git
git clone https://github.com/HolyGeek404/GoodStuff_ProductApi.git
```

### 2. Build and Run

Each repository includes instructions for building and running individually, typically using:

```bash
dotnet build
dotnet run
```

Or, using Docker (if provided):

```bash
docker build -t goodstuff-website .
docker run -p 8080:80 goodstuff-website
```

Repeat for UserApi and ProductApi.

### 3. Configuration

- Update API endpoints in the website frontend to point to the running UserApi and ProductApi services.
- Set environment variables or configuration files as needed (see individual repository READMEs for details).

---

## 🎯 Contributing

Contributions are welcome! Please see the individual repositories for guidelines on contributions, issues, and pull requests.

## 📖 License

This project is licensed under the MIT License.

---

## 👨🏻‍💻 Authors

- [HolyGeek404](https://github.com/HolyGeek404)

---

**Happy coding!**
