# ABInBev-Employees
This is a project to manage employees for AB InBev company.

# First run
Open the command line and run the following command to create the database container for the application:
docker run -v ~/docker --name ABInBevEmployees -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Passw0rd1!" -p 5434:1433 -d mcr.microsoft.com/mssql/server:2022-latest

Open the solution in the Visual Studio, in the appsettings.json adjust the ConnectionStrings.EmployeesConnection to "Server=localhost,5434;Database=EmployeesDb;Trusted_Connection=false;MultipleActiveResultSets=true;Encrypt=false;user id=sa;password=Passw0rd1!;", and then run the migration for EmployeeDbContext and AuthenticationDbContext with the following commands:
dotnet ef database update --project ABInBev.Employees.Data --startup-project ABInBev.Employees -v --context EmployeeDbContext
dotnet ef database update --project ABInBev.Employees.Data --startup-project ABInBev.Employees -v --context AuthenticationDbContext

Then in the appsettings.json adjust the ConnectionStrings.EmployeesConnection to "Server=host.docker.internal,5434;Database=EmployeesDb;Trusted_Connection=false;MultipleActiveResultSets=true;Encrypt=false;user id=sa;password=Passw0rd1!;", with that the application is going to connect to the database through Docker internal network. The first time you run the application the admin user is going to be created based in the values for the AdminUser section in appsettings.json.

# Usage
You can use Swagger to test this API. In order to authenticate do a request to POST Authentication, with the email and password contained in appsettings.json (AdminUser section). With this token you can create a new Employee, which is going to be associated with a Identity User, and then you will be able to authenticate with Employee's credentials.

# Author 
maub-dev
