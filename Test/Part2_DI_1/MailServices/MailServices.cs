using ConfigServices;
using LogServices;

namespace MailServices;

public class MailServices : IMailServices
{
    private readonly ILogProvider log;
    private readonly IConfigServices config;

    public MailServices(ILogProvider log, IConfigServices config)
    {
        this.log = log;
        this.config = config;
    }
    
    public void Send(string title, string to, string body)
    {
        this.log.LogInfo($"Preparing to send mail to {to}");
        string smtpServer = this.config.GetValue("SmtpServer");
        string username = this.config.GetValue("UserName");
        string password = this.config.GetValue("Password");
        
        this.log.LogInfo($"Using SMTP server {smtpServer} with user {username}");
        
        Console.WriteLine($"Sending mail to {to} with title {title} and body {body}");
        
        this.log.LogInfo($"Mail sent to {to}");
    }
}