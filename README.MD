Memory Monitor is a simple utility for logging system RAM usage. That's it.

# Download
You can download the utility in the [Release session](https://github.com/datvm/LukeVo.MemoryMonitor/releases).

If you want, you can clone the project and build/modify it to fit your needs. The utility is written in C#. You can build it by yourself using Microsoft's free [Visual Studio](https://visualstudio.microsoft.com/downloads/).

# Config
By default, the program log the information every 1 second on a folder named `Logs`. You can change that by opening `LukeVo.MemoryMonitor.exe.config` file and change the following settings:

    <add key="IntervalMillisecond" value="1000"/>
    <add key="OutputFolder" value="Logs"/>
    
`OutputFolder` can be set as relative path or absolute path (for example, `C:\Log\RAM` is valid).
*Note: you need to restart the program if it is already started.*

# Task Scheduler

If you want the program to start on startup, you can use [Task Scheduler](https://docs.microsoft.com/en-us/windows/desktop/taskschd/about-the-task-scheduler) for it.