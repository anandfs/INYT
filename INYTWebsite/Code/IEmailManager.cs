using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INYTWebsite.Code
{
    public interface IEmailManager
    {
        Task<Response> SendEmail(EmailInfo Email);
        bool SendErrorEmail(String errorText);
    }
}
