# WebsiteStatusApp
Build a .NET Worker Service, checks my website status

- Serilog [Docs](https://serilog.net/)

# Website Status Checking Service

- A worker service runs all the time with no input.

- on Linux these are Deamons
- could also run on Azure

- a Worker is able to test and see it in a console, when installed the console is hidden and runs as a service

## How to see this running

- Can see the service in windows in `Task Manager` and the `Services` tab.

## How to Install Worker Service

1. Use Powershell and SC (Service Control Manager)

  - Run Powershell as an Admin

  - Example
  ```powershell
  sc create {NAME} binpath="{PATH TO EXE}" start= auto
  ```
  
  - My command
  ```powershell
  sc create WebsiteStatusCory binpath="C:\Users\Cory\Documents\_Code\WebsiteStatusApp\WebsiteStatus\bin\Release\WebsiteStatus.exe" start= auto
  ```
  
  > Will output a message service started successfully

  - Start it now
  ```powershell
  sc start WebsiteStatusCory
  ```
  > Using the Name used to create it

## How to Uninstall Service

1. Using Powershell as a Admin

  - Run the delete in powershell
  ```powershell
  sc delete WebsiteStatusCory
  ```

> if an error occurrs on delete, you may need to stop the service first.
