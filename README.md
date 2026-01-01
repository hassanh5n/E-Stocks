# E-Stocks - Smart Trading & Portfolio Management

> **A comprehensive web-based platform for simulating stock market trading, mutual fund investments, and future contracts management.**

![Project Status](https://img.shields.io/badge/status-active-success.svg)
![.NET Core](https://img.shields.io/badge/.NET%20Core-6.0-purple)
![MySQL](https://img.shields.io/badge/database-MySQL-blue)

---

## Introduction

**E-Stocks** is a financial technology application designed to simulate a real world trading environment. It provides users with tools to manage their financial portfolios, trade stocks in real-time, invest in mutual funds, and engage in future contract trading. Built with **ASP.NET Core MVC** and **MySQL**, it offers a secure, scalable, and responsive experience for financial enthusiasts and learners.

---

## Key Features

### User Management & Security
*   **Secure Authentication**: Role-based login and registration system using Cookie Authentication.
*   **Profile Management**: Update personal details and manage account settings.

### Stock Market Trading
*   **Real-Time Data**: Integration with stock data services for up-to-date market information.
*   **Spot Trading**: Buy and sell stocks instantly at current market rates.
*   **Watchlist**: Track favorite stocks and monitor their performance.

### Investment Portfolio
*   **Digital Wallet**: Integrated wallet system to deposit, withdraw, and track funds.
*   **Transaction History**: Comprehensive log of all financial activities (Deposits, Withdrawals, Trades).
*   **Portfolio Dashboard**: Visual overview of asset allocation, current holdings, and overall net worth.

### Advanced Financial Instruments
*   **Mutual Funds**: Browse and invest in various mutual funds managed by professionals.
*   **Future Contracts**: Engage in derivatives trading with future contracts capabilities.
*   **Dividends**: Automated dividend distribution tracking for held stocks.

---

## Technology Stack

*   **Framework**: [ASP.NET Core 6 MVC](https://dotnet.microsoft.com/apps/aspnet/mvc)
*   **Language**: C# 10
*   **Database**: [MySQL](https://www.mysql.com/)
*   **ORM**: [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
*   **Frontend**: Razor Views (CSHTML), HTML5, CSS3, JavaScript (Bootstrap 5)
*   **Development Tools**: Visual Studio 2022 / VS Code

---

## Installation & Setup

Follow these steps to get the project running on your local machine.

### Prerequisites
*   [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) or later.
*   [MySQL Server](https://dev.mysql.com/downloads/mysql/) installed and running.
*   An IDE like **Visual Studio 2022** or **Visual Studio Code**.

### 1. Clone the Repository
```bash
git clone https://github.com/hassanh5n/E-Stocks.git
cd estocks
```

### 2. Configure Database Connection
Update the connection string in **`appsettings.Development.json`** (or `appsettings.json`) to match your local MySQL configuration:

```json
"ConnectionStrings": {
  "DefaultConnection": "server=localhost;user=root;password=yourpassword;database=estocksdb"
}
```

> **Note**: Also check `Data/EstocksDbContextFactory.cs` if you are using design-time tools that require a specific configuration.

### 3. Database Migration
Apply the migrations to create the database schema:

```bash
dotnet ef database update
```

### 4. Build and Run
Run the application using the .NET CLI:

```bash
dotnet run
```

The application should now be accessible at `https://localhost:7197` (or the port specified in your launch logs).

---

## DevOps & Deployment 🚀

### 🚀 Quick Deploy with Docker (Recommended)
The easiest way to run E-Stocks is using Docker Compose. This automatically sets up both the **ASP.NET Core Application** and the **MySQL Database**.

1.  **Clone the Repository**:
    ```bash
    git clone https://github.com/hassanh5n/E-Stocks.git
    cd estocks
    ```

2.  **Spin up the Environment**:
    ```bash
    docker-compose up -d
    ```

3.  **Access the Application**:
    Naviate to `http://localhost:5130` in your browser. 

*Note: The system includes a built-in health check and retry logic, so the app will wait for the database to be fully initialized before starting.*

---

E-Stocks is fully containerized and features a professional CI/CD pipeline for automated cloud deployment.

### 🐳 Containerization
The application uses **Docker** and **Docker Compose** to orchestrate the web server and the MySQL database.
*   **Multi-Stage Builds**: The `Dockerfile` uses a build stage (SDK) and a runtime stage (ASP.NET Runtime) to ensure the final production image is lightweight and secure.
*   **Database Resilience**: Implemented a custom C# retry mechanism in `Program.cs` to handle race conditions during container startup (ensuring the app waits for MySQL to be ready).
*   **Auto-Migration**: The database schema is automatically provisioned on startup using Entity Framework's `EnsureCreated()`.

### 🔄 CI/CD Pipeline (GitHub Actions)
A full CI/CD pipeline is configured in `.github/workflows/ci.yml`:
1.  **Continuous Integration**: On every push to `main`, GitHub Actions triggers a build to verify code integrity.
2.  **Continuous Delivery**: Upon a successful build, the Docker image is automatically pushed to **Docker Hub** (`hassanh5n/estocks`).
3.  **Continuous Deployment**: The pipeline then securely SSHs into an **AWS EC2** instance, pulls the latest image, and restarts the services using Docker Compose.

### ☁️ Cloud Infrastructure
*   **Host**: AWS EC2 (Ubuntu 24.04 LTS).
*   **Orchestration**: Docker Compose.
*   **Security**: Secured via GitHub Secrets and AWS Security Groups.

---

## Project Structure

```bash
estocks
 ├── Controllers   # Handles user requests and application logic (Stocks, Funds, Wallet, etc.)
 ├── Models        # Database entities (User, Stock, Order, Transaction, etc.)
 ├── Views         # Razor pages for UI rendering
 ├── Data          # DbContext and database configuration
 ├── Services      # External services (e.g., StockDataService)
 ├── wwwroot       # Static assets (CSS, JS, Images)
 ├── Program.cs    # Application entry point and DI configuration
 └── appsettings.json # Configuration settings
```

---

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

1.  Fork the project
2.  Create your feature branch (`git checkout -b feature/AmazingFeature`)
3.  Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4.  Push to the branch (`git push origin feature/AmazingFeature`)
5.  Open a Pull Request

---

## License

This project is licensed under the [MIT License](LICENSE).

---
