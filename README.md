Tracktor
========
![ForNeVeR/Tracktor](http://issuestats.com/github/ForNeVeR/Tracktor/badge/pr?style=flat-square) ![ForNeVeR/Tracktor](http://issuestats.com/github/ForNeVeR/Tracktor/badge/issue?style=flat-square)


Tracktor is a service for version control system monitoring.

Database setup
--------------
First you should set up the database for this service. It uses database
migration so you hopefully won't need to administer it.

1. Install PostgreSQL server anywhere.
2. Create a user, set up a database for him. An example of how it can be done:

   ```sql
create user tracktor password 'password';
create database Tracktor owner = tracktor;
```

3. Set up the connection string in `Tracktor.Service/app.config`. It should
   look like this:

   ```
Server=localhost;Port=5432;User Id=tracktor;Password=password;Database=Tracktor;
```

Service Installation
--------------------
Tracktor service supports two execution modes: as a Windows service and as an
executable. The latter is useful for debug and is not meant for production
mode.

To run the service in the debug mode, simply run the executable.

To install the service in the service mode you may use simple helper written in
PowerShell. Run `Install-Service.ps1` (you'll probably need to install
[PowerShell Community Extensions](https://pscx.codeplex.com/) and [enable script
execution](http://superuser.com/questions/106360/how-to-enable-execution-of-powershell-scripts)).

Here's how to start the service with PowerShell after its installation:

    PS> Start-Service TracktorService
