using ConfigServices;
using LogServices;

namespace MailServices;

public class MailServices : IMailServices
{
    private readonly ILogProvider log;
    // private readonly IConfigServices config;
    
    private readonly IConfigReader configReader;

    public MailServices(ILogProvider log, IConfigReader config)
    {
        this.log = log;
        // this.config = config;
        this.configReader = config;
    }
    
    public void Send(string title, string to, string body)
    {
        this.log.LogInfo($"Preparing to send mail to {to}");
        string smtpServer = this.configReader.GetValue("SmtpServer");
        string username = this.configReader.GetValue("UserName");
        string password = this.configReader.GetValue("Password");
        
        this.log.LogInfo($"Using SMTP server {smtpServer} with user {username}");
        
        Console.WriteLine($"Sending mail to {to} with title {title} and body {body}");
        
        this.log.LogInfo($"Mail sent to {to}");
    }
}