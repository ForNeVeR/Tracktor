Tracktor
========
Tracktor is a service for monitoring version control system.

Service Installation
--------------------
Currently only debug service instance mode is supported. To install service in
debug mode, run `Install-Service.ps1` (you'll probably need to install
[PowerShell Community Extensions](https://pscx.codeplex.com/) and [enable script
execution](http://superuser.com/questions/106360/how-to-enable-execution-of-powershell-scripts)).

After that you can manually start the service and debug it with Visual Studio.

Here's how to start the service with PowerShell:

    PS> Start-Service TracktorService
