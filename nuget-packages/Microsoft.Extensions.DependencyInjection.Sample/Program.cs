using Microsoft.Extensions.DependencyInjection;

ServiceCollection services = new();

services.AddSingleton<IConsoleService, ConsoleService>();

ServiceProvider serviceProvider = services.BuildServiceProvider();

IConsoleService consoleService = serviceProvider.GetRequiredService<IConsoleService>();

consoleService.Success("This is a success message!");

consoleService.Error("This is a error message!");

consoleService.Warning("This is a warning message!");

