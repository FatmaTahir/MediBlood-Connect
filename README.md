# 🩸Blood Donation Management System

A modern **ASP.NET MVC web application** developed to streamline the blood donation process by connecting **Blood Donors and Patients ** on one platform. The system helps manage blood requests, donation requests, donor availability, stock alerts, and request approvals efficiently.

---

## 🚀 Project Overview

The **Blood Donation Management System** is designed to reduce delays in blood availability during emergencies. It provides a secure and user-friendly environment where:

- **Patients** can request blood easily.
- **Donors** can manage their profile and availability.
- **Admins** can monitor requests, donors, and blood stock.

---

## 🛠️ Technologies Used

### Backend
- ASP.NET Core MVC  
- C#  
- Dapper 

### Frontend
- Bootstrap  
- HTML5  
- CSS3  
- JavaScript  
- AJAX  

### Authentication & Security
- ASP.NET Identity  
- Policy-Based Authorization
- Role-Based Authorization

### Real-Time Features
- SignalR  

### Testing
- Integration Testing  

### Database
- SQL Server  

---

## ✨ Key Features

---

## 👤 Donor Module

Registered donors can:

- Login securely
- Edit profile
- Update contact details
- Change availability status
- Submit donation request
- View donation request status
  

---

## 🩺 Patient Module

Patients can:

- Register/Login
- Submit blood requests
- Enter required units
- Select request emergency status from dropdown
- View request approval status
- After approval, view available donors
- Access donor contact details

---

## 🛡️ Admin Module

Admin has complete control to:

- Approve blood requests
- Delete requests
- View all blood requests
- View donation requests
- Filter requests by blood type
- View donors by:
  - Available
  - Not Available
- Monitor low blood stock alerts

---

## ⚡ Advanced Features

### 🔄 AJAX Integration
Used for faster page updates without full reloads.

### 📡 SignalR
Used for real-time notifications and instant updates.

### 🔐 Identity + Policy Authorization
Secure authentication with role-based access control.

### 🧪 Integration Tests
Includes testing project for validating system functionality.

---

## 📂 Project Structure

```bash
BloodDonationManagementSystem/
│── Controllers/
│── Models/
│── Views/
│── Services/
│── Repositories/
│── Interfaces/
│── Hubs/
│── Migrations/
│── Areas/Identity/
│── wwwroot/
│── Tests/
└── README.md
