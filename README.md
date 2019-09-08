# plugins

Solution provides an application that measure local environment parameters. Program was built using following technologies:
- .NET Standard 2.0 and .NET Core 2.2
- Topshelf as service hosting framework 
- Autofac as dependency container
- NLog as logging library that implements Microsoft.Extensions.Logging.Abstractions
- Reactive Extension as library that implements rich publish/subscribe pattern

Structure of application was built in that way to allow developers in easy way add a new plugins. The entry point of the application is PerformanceMonitorService.Host project that contains definition of service that has to be installed and run.
Additionally, DI container is setup as well. The initial version of application contains two example plugins that allows to collect information about Cpu usage and available RAM.
To add a new plugin developer has to only implement a strategy pattern (IMonitorPlugin). Communication between local storage and plugins is implemented using publish/subscriber pattern. It allows to separate the logic of plugins, storage and service. The whole solution is generic as much as possible to reduce developer effort to create a new plugin.

Method that should be implemented:
- Active -> Method is executed when application is started. Should create a new thread with bussines logic.
- Deactive -> Method is executed when application is stopped. All resources should be cleaned up.
- ResultStream -> Stream property that returns new value for the measured metric.

Examples of plugins can be found :
- Cpu plugin : https://github.com/mjuskiewicz/plugins/tree/master/PerformanceMonitorService.Plugins.CpuMonitorPlugin
- Ram plugin  https://github.com/mjuskiewicz/plugins/tree/master/PerformanceMonitorService.Plugins.RamMonitorPlugin

# Feature plans

Solution will be extend about REST API that consume HTTP requests. Web part will not have access to the plugins. Only available part will be Storage object. Interface segregation principle will be introduced becasue WebPart will be use only IStorageReader interface. 
