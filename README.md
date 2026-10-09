# 🩸 MediBlood Connect – Full Stack Blood Donation & Management System

![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-.NET%208-purple?logo=dotnet)
![Azure](https://img.shields.io/badge/Azure-App%20Service-blue?logo=microsoftazure)
![SQL Server](https://img.shields.io/badge/Azure%20SQL-Database-red?logo=microsoftsqlserver)
![License](https://img.shields.io/badge/Status-Live%20In%20Production-success)

MediBlood Connect is a mission-critical full-stack web application designed to connect blood donors directly with patients in real-time. It provides donors, patients, and administrators with an intuitive interface to manage requests, donor availability, and request approvals.

The application is built using **ASP.NET Core MVC**, **Entity Framework Core**, **Dapper**, and **Azure SQL Database**, deployed live on **Azure App Service**.

---
# 📸 Preview

<img width="941" height="442" alt="image" src="https://github.com/user-attachments/assets/e37a830b-5ffa-4de5-a2cf-17af4c39f206" />
<img width="941" height="444" alt="image" src="https://github.com/user-attachments/assets/344d7ecc-2f2f-461d-af5e-24c3c46560d1" />


```
Donar Portal

```
<img width="956" height="445" alt="image" src="https://github.com/user-attachments/assets/e3ef85f1-3783-4ec0-9f23-0172881ab93a" />
<img width="952" height="445" alt="image" src="https://github.com/user-attachments/assets/83941bbb-0bb4-4b86-b166-9da562b9cd12" />
<img width="954" height="445" alt="image" src="https://github.com/user-attachments/assets/92d8d467-d347-4741-9d6b-8107820fff99" />
<img width="956" height="445" alt="image" src="https://github.com/user-attachments/assets/7bce2bad-5339-48aa-9823-4770c3750919" />

```
Recipient Portal

```
<img width="951" height="444" alt="Screenshot 2026-10-09 210425" src="https://github.com/user-attachments/assets/fa26ffc4-2a59-496b-a514-44e8f66497fd" />
<img width="953" height="443" alt="image" src="https://github.com/user-attachments/assets/8db55ca7-363b-4d68-9976-cac6e1663173" />
<img width="955" height="440" alt="image" src="https://github.com/user-attachments/assets/44c67be0-f2c7-4e73-8e0b-eaaa6826bb50" />

```
Admin Portal

```
<img width="953" height="443" alt="Screenshot 2026-10-09 211002" src="https://github.com/user-attachments/assets/bd884e4f-03ec-43f0-a5b4-d92ad818dbee" />
<img width="955" height="442" alt="image" src="https://github.com/user-attachments/assets/e6076e33-3962-4af0-84fa-73bdc26ce3cc" />
<img width="954" height="442" alt="image" src="https://github.com/user-attachments/assets/e34fba87-2726-4498-a4e4-a42a348ff302" />
<img width="951" height="442" alt="image" src="https://github.com/user-attachments/assets/55721b04-dbad-47bd-9c6e-ee0efaff5a84" />

#  Live Application

- **Production App:** [https://medibloodconnect-acafg2hrgjakbsfz.swedencentral-01.azurewebsites.net](https://medibloodconnect-acafg2hrgjakbsfz.swedencentral-01.azurewebsites.net)
- **Deployment Platform:** Azure App Service (Sweden Central)
- **Database Engine:** Azure SQL Database (`MediBlooddb`)

---

# ✨ Features

##  Donor Features

- User Registration & Authentication (ASP.NET Core Identity)
- Donor profile management (Blood Group, Age, Contact)
- Toggle donor availability (`Available` / `Unavailable`)
- Submit blood donation offers
- View past donation request status

---

##  Patient Features

- Submit urgent blood requests specifying units required, urgency level, and medical reason
- Search and filter active matching donors by blood group
- Track status of submitted requests (`Pending`, `Approved`, `Rejected`)
- Direct contact details access for active donors

---

##  Admin Features

- Real-time dashboard metrics (Total Donors, Active Requests, Approved Requests)
- View and filter all blood requests by blood group and status
- View and filter donation requests
- Approve or reject patient blood requests
- Approve or reject donor requests

---

#  Tech Stack

## Architecture & Frameworks

- ASP.NET Core 8.0 MVC
- C#
- Razor Views
- Bootstrap 5
- HTML5 / CSS3 / JavaScript

---

## Data Access & Security

- Entity Framework Core (Identity schema & relational tracking)
- Dapper (High-performance micro-ORM for read/write repository queries)
- Azure SQL Server
- ASP.NET Core Identity
- Role-based Authorization (`Admin`, `Donor`, `Patient`)

---

# 🎨 UI & UX Design

MediBlood Connect features a medical-themed, responsive user interface designed for urgency and ease of navigation:

- Medical Red & Crimson accent themes
- Clean dashboard layouts
- Role-specific navigation bars
- Instant feedback alerts for request updates
- Mobile-responsive layout for quick access on mobile devices

---

#  Backend Concepts Implemented

✔ MVC Design Pattern

✔ Repository Pattern (Separation of Data Access)

✔ Dual ORM Integration (EF Core + Dapper)

✔ Azure SQL Database Integration

✔ Identity Management & Policy , Claim and Role-Based Authorization

✔ Custom Dapper Repositories (`DonorRepository`, `PatientRepository`, `AdminRepository`)

✔ Dynamic Firewall & Connection String Configuration

✔ Dependency Injection & Service Registration

✔ Exception Handling & Logging Middleware

✔ Parameterized SQL Queries (SQL Injection Prevention)

✔ SignalR for live notifications

---

# 🔐 Authentication & Roles

- ASP.NET Core Identity Integration
- Role & Policy-Based Access Control :
  - **Donor**: Manages availability & donation logs
  - **Patient**: Submits requests & views matching donors
  - **Admin**: System-wide approval authority and request oversight

---

# 🗄 Database

Azure SQL Server (`MediBlooddb`)

### Main Tables

- `AspNetUsers` (Extended with BloodGroup, Availability, Contact, Age)
- `AspNetRoles` / `AspNetUserRoles`
- `BloodRequest`
- `DonationRequest`

---



# ⚙️ Installation & Local Setup

## 1. Clone Repository

```bash
git clone [https://github.com/FatmaTahir/MediBlood-Connect.git](https://github.com/FatmaTahir/MediBlood-Connect.git)
cd MediBlood-Connect/BloodDonationMangementSystem
