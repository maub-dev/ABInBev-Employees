# ABInBev-Employees
This is the API to manage employees for AB InBev company.

# Requirements
To run this application is required that you have Docker installed and running on your machine.

# Running the application
Download the source code, open the solution file with Visual Studio, rebuild the application and run it with "docker compose"

# Usage
You can use Swagger to test this API. In order to authenticate, do a request to POST Authentication, with the email and password contained in appsettings.json (AdminUser section). With this token you can configure the "Authorize" in Swagger and then create a new Employee so that you can login in the application. Each user is going to be associated with a Identity User, and then you will be able to authenticate with Employee's credentials.
Example of first Employee:
{
  "firstName": "John",
  "lastName": "Doe",
  "email": "john.doe@test.com",
  "documentNumber": "111111111",
  "phone1": "11 6565656",
  "phone2": "11 1611666",
  "password": "String1!",
  "birthDate": "2000-01-01",
  "role": 3
}

# Logging
The logging for this application is running in a container created through the docker-compose. You can access the logs openning your browser in http://localhost:5342/ (you can check the port number in docker-compose, seq_log service). To login you should use the "admin" user and the password that is also contained in docker-compose, seq_log>environment>SEQ_PASSWORD.

# Author 
maub-dev
