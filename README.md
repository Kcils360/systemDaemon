# systemDaemon
---------------------------------

## a Service Daemon application

The service daemon consists of a single C# console project that runs in the background on a desktop or server. 
The interval that the program waits between action executions can be set to any interval in seconds, currently it 
performs a `TimeCheck` action every hour, looking for a match for 2300 hours.  At 2300, the `TimeCheck` function
calls the Action to be executed.


---------------------------------

## Tools Used
Microsoft Visual Studio Community 2019 (Version 16.5.0)

- C#
- .Net Core

#### Code sources...
- [Create a daemon](https://www.wintellect.com/creating-a-daemon-with-net-core-part-1/)
- [Schedule a task](https://medium.com/@NitinManju/a-simple-scheduled-task-using-c-and-net-c9d3230769ea)

---------------------------------

## Recent Updates

#### V 1.0
*Initial Launch of the application* - 14 Jul 2020

---------------------------

## Getting Started

Clone this repository to your local machine.
```
$ git clone https://github.com/Corliss-Dukes/syncdaemon
```
Once downloaded, you can either use the dotnet CLI utilities or Visual Studio 2019 (or greater) to build the 
application. The solution file is located in the syncdaemon subdirectory at the root of the repository.
```
cd syncdaemon/syncdaemon
dotnet build
```
The dotnet tools will automatically restore any dependencies. Before running the application, you will need to supply 
an `appsettings.json` file in the same directory as the solution, which will be used to pass any secret information
to other functions in the application.

```
{
  "someSecret": "xxxx...",
  "someArray": [ "xxxx...", "xxxx..." ]
}
```

Next, add to the application whatever functions need to be executed on a recurring schedule.  At this point the 
application can be run. Options for running and debugging the application using IIS Express or Kestrel are provided 
within Visual Studio. From the command line, the following will start an instance of the Kestrel server to host the application:
```
cd syncdaemon/syncdaemon
dotnet run --Daemon:DaemonName="Sync Daemon"
```

To quit the application using the command line:
```
 Ctrl + c
```

---------------------------------

## Change Log

------------------------------

## Author
Gregory Dukes <br>
www.gregorydukes.com
