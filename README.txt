                            EVERYBEATS                                     
                    Beat Marketplace Platform                               
                                                                            
			C2C Marketplace for Music Producers & Buyers                     



-------------------------------------------------------------------------------
1. PROJECT OVERVIEW
-------------------------------------------------------------------------------

EveryBeats is a C2C (Consumer-to-Consumer) marketplace where music producers
can sell their beats, and buyers can purchase licenses (Lease or Exclusive)
with automated legal agreements and split payments.

Key Features:
- User registration and authentication (Buyer, Producer, Admin roles)
- Beat upload and management (MP3, WAV, Stems)
- Browse and search beats by genre, BPM, and price
- Multi-producer collaboration with split percentages
- Automated license agreement generation (PDF)
- Mock payment processing
- Producer dashboard with sales tracking
- Shopping cart functionality
- User ratings and reviews
- Loyalty points system
- Invoice generation and history


-------------------------------------------------------------------------------
2. TECHNOLOGY STACK
-------------------------------------------------------------------------------

Frontend:     ASP.NET Web Forms, HTML5, CSS3, JavaScript, Bootstrap
Backend:      C#, WCF Services (Windows Communication Foundation)
Database:     Microsoft SQL Server (via LINQ to SQL)
PDF Gen:      iTextSharp
IDE:          Microsoft Visual Studio 2019 / 2022
Version Ctrl: Git & GitHub
Hosting:      Localhost / IIS Express


-------------------------------------------------------------------------------
3. PROJECT STRUCTURE
-------------------------------------------------------------------------------

EveryBeats/
│
├── Backend/                          ← WCF Services & Business Logic
│   ├── Services/                     ← Service1.svc, IService1.cs
│   ├── Data/                         ← LINQ to SQL DataContext (.dbml)
│   ├── Models/                       ← DTOs (User, Beat, License, etc.)
│   └── Helpers/                      ← PasswordHasher, PDFGenerator, MockPayment
│
├── Frontend/                         ← ASP.NET Web Forms (Presentation Layer)
│   ├── Pages/                        ← Home, Login, Register, Browse, etc.
│   ├── MasterPages/                  ← Main.Master (consistent layout)
│   ├── Styles/                       ← CSS files
│   ├── Scripts/                      ← JavaScript files
│   ├── Images/                       ← Logos, icons
│   └── Service References/           ← WCF client proxies
│
├── Database/                         ← SQL scripts & backup
│   ├── Scripts/                      ← 01_Create_Tables.sql, 02_Insert_Test_Data.sql
│   └── Backup/                       ← Database backups (.bak)
│
├── Documentation/                    ← ERD, reports, presentations
│   ├── ERD.png
│   └── Project_Report.docx
│
├── Tests/                            ← Unit & Integration tests
│   ├── UnitTests/
│   └── IntegrationTests/
│
├── EveryBeats.sln                    ← Visual Studio Solution file
├── EveryBeats.csproj                 ← Project file
├── Web.config                        ← Configuration & connection strings
├── packages.config                   ← NuGet dependencies
├── .gitignore                        ← Git ignore rules
└── README.txt                        ← This file


-------------------------------------------------------------------------------
4. INSTALLATION & SETUP
-------------------------------------------------------------------------------

Prerequisites:
- Windows OS (10 or later)
- Visual Studio 2019 or 2022 (with ASP.NET and SQL Server workloads)
- SQL Server (Express, LocalDB, or Developer Edition)
- .NET Framework 4.7.2 or higher

Step 1: Clone the Repository
----------------------------------------------------------
git clone https://github.com/stoja1209/EveryBeats.git

Step 2: Open the Solution
----------------------------------------------------------
- Navigate to the project folder
- Open EveryBeats.sln in Visual Studio

Step 3: Install NuGet Packages
----------------------------------------------------------
- Open Package Manager Console
- Run: Install-Package iTextSharp

Step 4: Set Up the Database
----------------------------------------------------------
1. Open SQL Server Management Studio (SSMS)
2. Run: Database/Scripts/01_Create_Tables.sql
3. Run: Database/Scripts/02_Insert_Test_Data.sql
4. Update the connection string in:
   - Web.config (Frontend)
   (Change Data Source and Initial Catalog as needed)

Step 5: Build and Run
----------------------------------------------------------
1. Set the WCF Service project as Startup Project
2. Press F5 to run the service (leave it running)
3. Set the Frontend project as Startup Project
4. Press F5 to run the web application

Default Test Account:
- Email:    // TODO
- Password: // TODO


-------------------------------------------------------------------------------
5. HOW TO USE THE APPLICATION
-------------------------------------------------------------------------------

For Buyers:
1. Register as a Buyer
2. Browse beats on the Home or Browse page
3. Click on a beat to view details and preview the audio
4. Select a license type (Lease or Exclusive)
5. Add to Cart → Proceed to Checkout
6. Review and sign the license agreement
7. Complete the mock payment
8. Download your beat and signed agreement PDF

For Producers:
1. Register as a Producer
2. Navigate to the Dashboard
3. Click "Upload Beat" → Fill in details and upload files
4. Add collaborators with split percentages (if any)
5. View sales and earnings on the Dashboard

For Admins:
1. Log in with admin credentials
2. Access the Admin Panel
3. Manage users, beats, and disputes


-------------------------------------------------------------------------------
6. DATABASE SCHEMA (Key Tables)
-------------------------------------------------------------------------------

Users          - User accounts (Buyers, Producers, Admins)
Producer       - Producer-specific data (artist name, payout details)
BankDetails    - Producer bank account information
Genre          - Music genres
Beat           - Beat metadata (title, price, file paths, genre)
License        - License types (Lease, Exclusive) with terms & price
Collaborations - Links beats to multiple producers with split %
Order          - Purchase transactions
OrderItems     - Items purchased in an order
Agreement      - Legal contract (PDF) signed by the buyer
Payouts        - Producer payouts
Splits         - Revenue distribution among collaborators
ShoppingCart   - User's cart items

ERD available in: Documentation/ERD.png


-------------------------------------------------------------------------------
7. WCF SERVICE METHODS
-------------------------------------------------------------------------------

The WCF Service exposes the following categories of methods:

User Management:       loginUser, registerUser, updateUserProfile, deactivateUser,
                       getUserByID, getAllUsers

Beat Management:       uploadBeat, updateBeat, deleteBeat, getBeatByID,
                       getAllBeats, getBeatsByProducer, getBeatsByGenre, searchBeats

License Management:    addLicense, updateLicense, deleteLicense, getLicenseByID,
                       getLicensesByBeat, isLicenseAvailable

Collaborations:        addCollaboration, updateCollaboration, removeCollaboration,
                       getCollaborationsByBeat, validateSplits

Shopping Cart:         addToCart, removeFromCart, clearCart, getCart,
                       getCartTotal, updateCartItemLicense

Orders:                createOrder, getOrderByID, getOrdersByUser,
                       processPayment, completeOrder, refundOrder

Agreements:            signAgreement, generateAgreementPDF, getAgreementByID,
                       getAgreementByOrderItem, getAgreementPDFUrl

Payouts & Splits:      createPayout, approvePayout, markPayoutPaid,
                       getPayoutsByProducer, createSplit, getSplitsByOrderItem,
                       getSplitsByProducer

Reports:               getRegisteredUsersPerDay, getTotalRevenueByDateRange,
                       getSalesByGenre, getTopSellingProducers, getMonthlySales

Invoices:              getInvoiceByOrderID, getAllInvoicesByUser

Producer Dashboard:    getTotalBeats, getTotalSales, getTotalRevenue,
                       getAverageSale, getTopBeatTitle, getSalesHistory


-------------------------------------------------------------------------------
8. COMMON ISSUES & TROUBLESHOOTING
-------------------------------------------------------------------------------

Issue: "The type or namespace name 'ServiceReference1' could not be found"
Solution: Add the service reference again:
  1. Right-click Frontend → Add Service Reference
  2. Click Discover
  3. Select Service1.svc → Click OK

Issue: "Cannot connect to database"
Solution:
  1. Check your connection string in Web.config
  2. Ensure SQL Server is running
  3. Verify the database name is correct

Issue: "Port mismatch (WCF service not found)"
Solution:
  1. Run the WCF service first (F5) and note the port number
  2. Update the endpoint address in Frontend/Web.config

Issue: "iTextSharp namespace not found"
Solution:
  1. Open Package Manager Console
  2. Run: Install-Package iTextSharp
  3. Add: using iTextSharp.text; and using iTextSharp.text.pdf;

Issue: "DateTime? does not contain a definition for Year"
Solution: Use o.OrderDate.Value.Year instead of o.OrderDate.Year


-------------------------------------------------------------------------------
9. CONTRIBUTORS
-------------------------------------------------------------------------------

Team Name: LOGIC GATES

| Name                  | Student Number | Role                                          |
|-----------------------|----------------|-----------------------------------------------|
| Surname Initials      | 20xxxxxxxx     | Team Leader / Frontend Developer              |
| Surname Initials      | 20xxxxxxxx     | Frontend Developer / UI/UX                    |
| MATHEBULA S           | 225150529      | Backend Developer (WCF Services & Database)   |
| Surname Initials      | 20xxxxxxxx     | Backend-Frontend Integration & Testing        |

Role Breakdown:
- Team Leader / Frontend Developer: Leads the team, oversees project progress, builds the user interface.
- Frontend Developer / UI/UX: Implements frontend pages, handles user experience, CSS styling, and responsive design.
- Backend Developer (WCF Services & Database): Designs the database, implements WCF service methods, manages business logic, and creates DTOs.
- Backend-Frontend Integration & Testing: Connects frontend to backend via service references, handles API calls, performs testing and debugging.


-------------------------------------------------------------------------------
10. LICENSE
-------------------------------------------------------------------------------

This project was developed for academic purposes as part of the
Informatics 2B (IFM02B2) module at the University of Johannesburg.

MIT License
Copyright (c) 2026 LOGIC GATES


-------------------------------------------------------------------------------
11. CONTACT
-------------------------------------------------------------------------------

For any questions or issues, please contact:
- Email: // TODO
- GitHub Issues: https://github.com/stoja1209/EveryBeats/issues


-------------------------------------------------------------------------------
END OF README
-------------------------------------------------------------------------------