# ASP.NET MVC CAPTCHA Project

This is a simple ASP.NET MVC application that includes CAPTCHA functionality for form validation and bot protection. The project uses Entity Framework with existing migrations.

## Getting Started

1. Restore NuGet packages:
   ```bash
   dotnet restore
   ```

2. Update your database connection string:

   Edit the `appsettings.json` and `appsettings.Development.json` (or your configuration file) to include your **local MySQL** connection string. Example:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "server=localhost;port=3306;database=your_db_name;user=your_username;password=your_password;"
   }
   ```

3. Apply the existing migrations:

   ```bash
   dotnet ef database update
   ```

4. Run the project:

   ```bash
   dotnet run
   ```
