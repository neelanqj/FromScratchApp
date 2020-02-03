using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FromScratchApp.Servies
{
    public class AuthMessageSender : IEmailSender
    {
        public AuthMessageSender()
        {
            //Options = optionsAccessor.Value;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            // Plug in your email service here to send an email.

            return Task.FromResult(0);
        }
    }
}
