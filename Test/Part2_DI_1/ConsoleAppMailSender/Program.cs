using ConfigServices;
using LogServices;
using MailServices;
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

using (var sp = services.BuildServiceProvider())
{
    var mailServices = sp.GetRequiredService<IMailServices>();
    mailServices.Send("Hello", "trump@usa.gov", "hello trump");
}

Console.Read();
