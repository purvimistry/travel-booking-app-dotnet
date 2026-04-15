# ✈️ Travel Booking App (.NET 8)

A full-stack **Travel Booking Application** built using **.NET 8** following **Clean Architecture principles**.
This project demonstrates real-world enterprise patterns like **Repository Pattern, Service Layer, and API integration**.

---

## 🚀 Features

* 🔍 Hotel search & filtering (City, Price, Stars)
* 📅 Booking management (Create, Edit, Delete)
* 👤 Customer management (demo data)
* 💱 Currency conversion (INR → USD using external API)
* ⚡ Real-time UI updates using AJAX
* 🏗️ Clean Architecture implementation

---

## 🛠️ Tech Stack

* ASP.NET Core MVC
* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* jQuery / AJAX

---

## 🏗️ Architecture

This project follows **Clean Architecture**:

* **Domain** → Core entities & business rules
* **Application** → Business logic, DTOs, Services
* **Infrastructure** → Database access (EF Core)
* **API** → REST endpoints
* **Web** → MVC UI

---

## ⚙️ Setup Instructions

### 1. Clone the repository

git clone https://github.com/purvimistry/travel-booking-app-dotnet.git

### 2. Configure Database

Update connection string in `appsettings.json`

### 3. Run migrations

dotnet ef database update

### 4. Add API Key

Add your ExchangeRate API key:

```
"ExchangeRateApi": {
  "Key": "YOUR_API_KEY"
}
```

### 5. Run the project

dotnet run

---

## 📝 Notes

* API keys are removed for security
* Static data is used for demo purposes
* Designed for learning & showcasing architecture skills

---

## 🎯 Key Concepts Demonstrated

* Clean Architecture
* Repository Pattern
* Dependency Injection
* DTO & ViewModel separation
* API Integration using HttpClient

---

## 👩‍💻 Author

**Purvi Mistry**
