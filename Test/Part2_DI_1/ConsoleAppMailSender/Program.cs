using ConfigServices;
using ConsoleAppMailSender;
using LogServices;
using MailServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

// See https://aka.ms/new-console-template for more information



Console.WriteLine("Hello, World!");

ServiceCollection services = new ServiceCollection();
// services.AddScoped<IConfigServices>(s=> new IniFileConfigServices {FilePath = "mail.ini"});
services.AddIniFileConfig("mail.ini");
services.AddScoped<IConfigServices, EnvVarConfigServices>();

services.AddLayeredConfig();

services.AddScoped<IMailServices, MailServices.MailServices>();
// services.AddScoped<ILogProvider, ConsoleLogProvider>();
services.AddConsoleLog();

// Build configuration first
ConfigurationBuilder configBuilder = new ConfigurationBuilder();
configBuilder.AddJsonFile("config.json", optional: false, reloadOnChange: true);
IConfigurationRoot configurationRoot = configBuilder.Build();

// Register IConfiguration
services.AddOptions().Configure<Configuration>(e => configurationRoot.Bind(e)); // Bind configuration to POCO

// Register IConfiguration so it can be injected
services.AddScoped<TestController>();

using (var sp = services.BuildServiceProvider())
{ 
    var mailServices = sp.GetRequiredService<IMailServices>();
    mailServices.Send("Hello", "trump@usa.gov", "hello trump");
    
    var controller = sp.GetRequiredService<TestController>();
    controller.Test();
}

Console.Read();
