# DotnetMacTest

This repository is created to test some basic features of .NET and SQL Server on Apple Silicon.

---

## Prerequisites

Before you begin, ensure you have the following installed on your Mac:

- [.NET SDK](https://dotnet.microsoft.com/download/dotnet) (version 8.0 or higher, but not 9.0)
- [Docker Desktop for Mac](https://www.docker.com/products/docker-desktop/) (Apple Silicon compatible)
- [Visual Studio Code](https://code.visualstudio.com/) or [Rider](https://www.jetbrains.com/rider/)
- [Azure Data Studio](https://azure.microsoft.com/services/developer-tools/data-studio/)

---

## Setup

### 1. Run Docker Desktop

Make sure Docker Desktop is up and running.
- **Enable Virtualization Framework**: In Docker Desktop, go to **Settings > General** and enable **Use Virtualization Framework**.
- **Enable x86/amd64 Emulation**: Go to **Settings > Features in Development** and enable **Use Rosetta for x86/amd64 Emulation…** (Note: This option may no longer be experimental).

### 2. Start the Docker Containers

From the root directory of this repository, run the following command to start the necessary containers:

```bash
docker-compose up -d
```

This will start the database and any other required services as defined in docker-compose.yml.

### 3. Connect to the Database
   Open Azure Data Studio and connect to the database. The connection details (such as host, port, username, and password) can be found in the docker-compose.yml file.

### 4. Start the Application
   Depending on your preference, you can start the application in one of the following ways:

- Using the CLI:

```bash
dotnet watch
```
Run this command from the folder containing the API project.

- Using an IDE: Open the project in Visual Studio Code or Rider, then start the application from there.

### Usage
1. Seed the Database: Click on "Seed Data" in the application interface to populate the database with sample data.
2. Export Data:
   - Click on Export Excel to download data as an Excel file.
   - Click on Export PDF to download data as a PDF file.
   - Click on Export Word to download data as a Word document.