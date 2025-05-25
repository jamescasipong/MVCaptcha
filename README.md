# ASP.NET MVC CAPTCHA

This is a simple ASP.NET MVC application that includes CAPTCHA functionality for form validation and bot protection. The project uses Entity Framework with existing migrations and MySQL as the database.

## Getting Started

1. Restore NuGet packages:
   ```bash
   dotnet restore
   ```

2. Configure your local MySQL connection:

   Open the `appsettings.json` file and ensure the database section looks like this (update values if needed):

   ```json
   {
     "AppSettings": {
       "Database": {
         "ConnectionString": "server=localhost;port=3306;database=mvcaptchadb;user=root;password=your_password;",
         "MaxRetryCount": 3,
         "EnableSensitiveDataLogging": false
       },
       "Captcha": {
         "ImageFolder": "/Images/Captcha/",
         "ExpiryMinutes": 20,
         "CaptchaCount": 3
       }
     },
     "Logging": {
       "LogLevel": {
         "Default": "Information",
         "Microsoft.AspNetCore": "Warning"
       }
     },
     "AllowedHosts": "*"
   }
   ```

   Change the connection string of `appsettings.Development.json` as well to ensure it works.

3. Apply the existing migrations to create/update your MySQL database schema:

   ```bash
   dotnet ef database update
   ```

4. Run the project:

   ```bash
   dotnet run
   ```

## Notes

- Ensure that MySQL is installed and running on your machine.
- Make sure the user has the necessary privileges to access and modify the `mvcaptchadb` database.
- If you run into connection issues, verify your connection string and database server status.
