# MediBlood Connect 🩸

**MediBlood Connect** is a full-stack ASP.NET Core MVC application built to streamline blood donation workflows, enable real-time matching between patients and active donors, and manage blood requests efficiently.

---

## 🚀 Live Demo
- **App URL:** [https://medibloodconnect-acafg2hrgjakbsfz.swedencentral-01.azurewebsites.net](https://medibloodconnect-acafg2hrgjakbsfz.swedencentral-01.azurewebsites.net)
- **Deployment Platform:** Azure App Service (Sweden Central)
- **Database Engine:** Azure SQL Database (`MediBlooddb`)

---

## 🛠️ Tech Stack

- **Backend:** C# / .NET 8.0 ASP.NET Core MVC
- **Authentication & Security:** ASP.NET Core Identity (Role-based Authorization)
- **Data Access Layer:** 
  - **Entity Framework Core:** Identity Schema, Migrations, and Relational Mapping
  - **Dapper:** High-performance direct SQL queries for Donor, Patient, and Admin Repositories
- **Database:** Azure SQL Database / Microsoft SQL Server
- **Frontend:** Razor Views, HTML5, CSS3, JavaScript, Bootstrap

---

## ✨ Features

### 👤 Donor Module
- Register as an active donor with blood group and contact details.
- Toggle availability status (`Available` / `Unavailable`).
- Track and respond to blood donation requests.

### 🏥 Patient Module
- Submit urgent blood requests specifying units required, urgency level, and hospital details.
- Search and filter active donors matching requested blood groups.
- Track status of submitted requests (`Pending`, `Approved`, `Rejected`).

### 🛡️ Admin Dashboard
- Comprehensive metrics overview (Total Donors, Active Requests, Approved Requests).
- Filter and search donation requests by blood group and status.
- Single-click approval/rejection workflows for donation and blood requests.

---

## ⚙️ Local Development Setup

1. **Clone the Repository:**
   ```bash
   git clone [https://github.com/YOUR_USERNAME/MediBlood-Connect.git](https://github.com/YOUR_USERNAME/MediBlood-Connect.git)
   cd MediBlood-Connect
