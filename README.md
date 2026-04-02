# 🏨 Hotel Management System

A full-stack hotel booking and management system including a mobile application and a web-based admin panel. The system allows users to browse and book hotels, while administrators can manage hotels, rooms, and bookings efficiently.


### 🔹 Web Admin

* ASP.NET Core MVC
* Razor Pages
* Bootstrap

### 🔹 Database

* MySQL

---

## ✨ Features

### 👤 User (Mobile App)

* 🔐 User authentication (JWT)
* 🔍 Browse hotels and rooms
* 📅 Book rooms
* 📖 View booking history

### 🛠️ Admin (Web)

* 🏨 Manage hotels (CRUD)
* 🛏️ Manage rooms (CRUD)
* 📦 Manage bookings
* 👥 Manage users

### ⚙️ System

* RESTful API architecture
* Secure password encryption
* Token-based authentication (JWT)
* Clean UI/UX design
* Client-server communication via HTTP

---

## ⚙️ Setup Instructions

### 1. Clone repository

```bash
git clone
```

---


Web Admin Setup

* Open MVC project
* Make sure API base URL is correct
* Run project:

```bash
dotnet run
```



* Update API base URL in the app

---

## 🔐 Notes

* Do not commit sensitive data (connection strings, secrets)
* Use environment variables for production
* Ignore `bin/`, `obj/`, `.vs/` using `.gitignore`

---

## 💡 Highlights

* Built with **3-layer architecture** (Controller - Service - Repository)
* Full-stack development: Mobile + Backend + Web Admin
* Applied best practices for security (JWT, password hashing)
* Clean and maintainable code structure

---

## 📌 Author

* Nguyen Van Giang
* GitHub: https://github.com/Giang-ASAG

---

## 📄 License

This project is for educational purposes.
