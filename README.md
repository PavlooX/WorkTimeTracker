# About

ASP.NET Core Web API for employee working hours tracking application. 

Made in Microsoft Visual Studio 2022 using .NET (v8.0.4) and Entity Framework Core (v8.0.4). Prepared to be used with Microsoft SQL Server.

Consists of controllers for: 

* **Employee**
    * Create Employee
    
    * Get Employee
    * Get all Employees
    * Update Employee
    * Delete Employee

* **TrackLog**
    * Start time tracking
    
    * End time tracking

* **Report**
    * Get Report
    
    * Get all Reports

<br>

# Installation

1. Clone the repository:
   ```
   https://github.com/PavlooX/WorkTimeTracker.git
   ```

2. Inside Microsoft Visual Studio right click on "Solution "WorkTimeTracker"" and press "Restore NuGet Packages".

3. Right click on "Solution "WorkTimeTracker"" and press "Build Solution".

4. In Microsoft SQL Server Management Studio create a new database, e.g.:
   ```
   WorkTimeTracker
   ```

5. Right click on the created database and open properties. Under "Database" copy the "Name" property, e.g. "WorkTimeTracker". That will be {DATABASENAME}. 

6. Under "Database" from "Owner" property, which is in format "pcname\user", copy the "pcname" part. That will be {PCNAME}.

7. Inside project folder open "appsettings.json" and change the parameters {PCNAME} and {DATABASENAME} with the parameters retrieved from the previous steps.
   ```
   "Data Source={PCNAME}\\SQLEXPRESS;Initial Catalog={DATABASENAME};Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
   ```
   ```
   "Data Source=MyPC\\SQLEXPRESS;Initial Catalog=WorkTimeTracker;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
   ```

8. Inside Microsoft Visual Studio from View > Other Windows open Package Manager Console. Write the following line to initiate the newly created database.
   ```
   Update-Database
   ```

9. Installation complete. API is ready for use.

<br>


# Usage


## EmployeeController


### CreateAsync(...)

*Creates new Employee.*

Checks if:
* FirstName and LastName fields are set and in the [1, 50] characters count range


### GetAsync(...)

*Displays Employee by given employeeId, with its belonging TrackLogs.*

Checks if:
* employeeId is integer type
* Employee exists by given employeeId 


### GetAllAsync(...)

*Displays all Employees with its belonging TrackLogs. Employees can be searched (by FirstName or LastName), sorted (by Id, FirstName, LastName or WorkingHours) and ordered (by Ascending or Descending). Default state is all Employees sorted by Id with Ascending order.*

Checks if:
* Search field, if set, is less than 100 characters


### UpdateAsync(...)

*Updates Employee by given employeeId. Fields updated are FirstName, LastName and LastUpdate.*

Checks if:
* employeeId is integer type
* Employee exists by given employeeId 
* FirstName and LastName fields are set and in the [1, 50] characters count range
  

### DeleteAsync(...)

*Deletes Employee by given employeeId, with its belonging TrackLogs.*

Checks if:
* employeeId is integer type
* Employee exists by given employeeId 


<br>


## TrackLogController


### StartAsync(...)

*Starts time tracking for Employee by given employeeId. Updates Employee LastUpdate field.*

Checks if:
* StartTime field is set
* employeeId is integer type
* Employee exists by given employeeId 
* active TrackLog already exists
* new StartTime is later than the latest TrackLog EndTime (if latest TrackLog exists)


### EndAsync(...)

*Ends time tracking for Employee by given employeeId. Updates Employee LastUpdate field.*

Checks if:
* EndTime field is set
* employeeId is integer type
* Employee exists by given employeeId 
* there is an active TrackLog
* EndTime is later than StartTime


<br>


## ReportController


### GetAsync(...)

*Displays Employee by given employeeId, with its belonging TrackLogs in range [FromTime, ToTime].*

Checks if:
* FromTime and ToTime fields are set
* employeeId is integer type
* Employee exists by given employeeId 


### GetAllAsync(...)

*Displays all Employees with its belonging TrackLogs in range [FromTime, ToTime]. Employees can be searched (by FirstName or LastName), sorted (by Id, FirstName, LastName or WorkingHours) and ordered (by Ascending or Descending). Default state is all Employees sorted by WorkingHours with Descending order.*

Checks if:
* FromTime and ToTime fields are set
* Search field, if set, is less than 100 characters


<br>
<br>
