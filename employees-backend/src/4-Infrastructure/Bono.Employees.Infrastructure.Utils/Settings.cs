using Microsoft.Extensions.Configuration;
using System;

namespace Bono.Employees.Infrastructure.Utils
{
    public class Settings
    {        
        public string SmtpEmail { get; }
        public string EmailSender { get; }
        public string SmtpPort { get; }
        public string SmtpTls { get; }
        public string SmtpPassword { get; }
        public bool IsDevelopment { get; }

        public Settings(IConfiguration Configuration)
        {
            
            this.SmtpEmail = Configuration["Smtp:Email"];
            this.EmailSender = Configuration["Smtp:EmailSender"];
            this.SmtpPort = Configuration["Smtp:Port"];
            this.SmtpTls = Configuration["Smtp:Tls"];
            this.SmtpPassword = Configuration["Smtp:Password"];
            this.IsDevelopment = Convert.ToBoolean(Configuration["Environment"]);
        }        

    }
}
