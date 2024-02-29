using System;
using System.Collections.Generic;
using System.Text;

namespace Bono.Employees.Domain.Interfaces.Business
{
    public interface IEmailManager
    {
        bool Send(string to, string subject, string body, string cc = "", string co = "", List<Entities.Attachment> attachments = null);
    }
}
