# Customer Data Import Tool

## Overview

This ASP.NET MVC-based tool enables users to seamlessly import customer data from Excel files (`.xlsx`) into a relational database. With an intuitive mapping interface, users can align spreadsheet columns to database fields dynamically.

### Features
- Upload customer data via Excel (`.xlsx`)
- Interactive mapping of Excel headers to DB fields
- Field validation and detailed error handling
- Secure and reliable import mechanism
- Supports SQL Server and MySQL backends

### Technologies
- **Framework:** ASP.NET MVC
- **Backend:** SQL Server / MySQL
- **File Format:** `.xlsx`
- **Package:** EPPlus.Core (for Excel parsing)

---

## How It Works

### 1. **Upload Excel File**
Users visit the upload page to select and submit an Excel file containing customer data.

### 2. **Column Mapping**
An interactive mapping interface allows users to associate Excel headers with database column names.

### 3. **Data Import**
On clicking **Import**, the system validates the mapped data and stores it in the selected database.

---

## Project Structure

```
├── Controllers
│   ├── UploadController.cs
│   └── MappingController.cs
├── Models
│   ├── Customer.cs
│   ├── CustomerColumnMapping.cs
│   └── CustomerDbContext.cs
├── Views
│   ├── Upload
│   │   └── Index.cshtml
│   ├── Mapping
│   │   └── MapColumns.cshtml
├── Services
│   └── ExcelProcessingService.cs
```

---

## NuGet Package Requirement

Install EPPlus.Core to handle Excel file operations:

EPPlus parses `.xlsx` files without requiring Excel to be installed.

Install EntityFramework -Version 6.5.1

---

## Database Setup (SQL Server)

Entity Framework Code First Migrations are used to automatically generate the required table.

### Steps to Configure

1. **Verify SQL Server Is Running**
   Use SQL Server Express or Standard. Confirm access via SSMS.

2. **Update Connection String**
   In `Web.config`, replace with:
   ```xml
   <add name="CustomerDb" connectionString="Data Source=YOUR_SERVER_NAME;Initial Catalog=ExcelImport;Integrated Security=True;" providerName="System.Data.SqlClient" />
   ```

3. **Automatic Table Creation**
   Upon first run, EF creates the `Customers` table based on your `Customer` model — no manual SQL scripting needed!

4. **Confirm Setup**
   Open SSMS → Connect to your DB → Run:
   ```sql
   SELECT * FROM Customers;
   ```
   If results appear, import is successful!
