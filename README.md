# ABInBev-Employees
This is the API to manage employees for AB InBev company.

# Requirements
To run this application is required that you have Docker installed and running on your machine.

# Running the application
Download the source code, open the solution file with Visual Studio, rebuild the application and run it with "docker compose"

# Usage
You can use Swagger to test this API. In order to authenticate, do a request to POST Authentication, with the email and password contained in appsettings.json (AdminUser section). With this token you can configure the "Authorize" in Swagger and then create a new Employee so that you can login in the application. Each user is going to be associated with a Identity User, and then you will be able to authenticate with Employee's credentials.

# Author 
maub-dev
