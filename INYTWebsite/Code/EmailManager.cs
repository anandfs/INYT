using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using INYTWebsite.Models;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace INYTWebsite.Code
{
    public class EmailManager : IEmailManager
    {
        private AppSettings _appSettings;

        public EmailManager(IOptions<AppSettings> settings)
        {
            _appSettings = settings.Value;
        }

        public async Task<Response> SendEmail(EmailInfo Email)
        {
            try
            {
                string apikey2 = _appSettings.SendGridAPI.apiKey;
                string apikey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
                var client = new SendGridClient(apikey2, _appSettings.SendGridAPI.clientURL);
                var msg = new SendGridMessage();
                msg.SetFrom(new EmailAddress(Email.FromAddress, (!String.IsNullOrEmpty(Email.FromDisplayName)) ? Email.FromDisplayName : null));
                msg.AddTo(new EmailAddress(Email.ToAddress));
                msg.Subject = Email.Subject;


                msg.SetTemplateId(_appSettings.SendGridAPI.serviceproviderauthorisationTemplate);
                var dynamicTemplateData = new MessageCenterTemplate
                {
                    message_text = Email.Body,
                    reply_link = Email.emailReplyLink,
                    subject = Email.Subject
                };
                msg.SetTemplateData(dynamicTemplateData);

                if (Email.emailType == "INYT_WelcomeCustomerEmail")
                {
                    msg.SetTemplateId("d-2c3fef273b5546d5b0166f4184f86b92");
                    dynamicTemplateData = new MessageCenterTemplate
                    {
                        firstName = Email.FromDisplayFirstName,
                        lastName = Email.FromDisplayLastName,
                        companyName = Email.FromDisplayCompanyName,
                        job_position = Email.FromDisplayJobPosition,
                        message_count = 1,
                        message_text = Email.Body,
                        reply_link = Email.emailReplyLink,
                        subject = Email.Subject
                    };
                    msg.SetTemplateData(dynamicTemplateData);
                }
                //else
                //{
                //    msg.SetTemplateId("d-33203bde393c4a1bad54ccb476fab339");
                //    var dynamicTemplateData = new MessageCenterTemplate
                //    {
                //        message_text = Email.Body,
                //        reply_link = Email.emailReplyLink,
                //        subject = Email.Subject
                //    };
                //    msg.SetTemplateData(dynamicTemplateData);
                //}

                if (!string.IsNullOrEmpty(Email.calendarToAttach))
                {
                    var calendarBytes = Encoding.UTF8.GetBytes(Email.calendarToAttach.ToString());
                    MemoryStream ms = new MemoryStream(calendarBytes);
                    SendGrid.Helpers.Mail.Attachment calendarAttachment = new SendGrid.Helpers.Mail.Attachment();
                    calendarAttachment.Content = Convert.ToBase64String(calendarBytes);
                    calendarAttachment.Type = "text/calendar";
                    calendarAttachment.Filename = "calendar.ics";
                    msg.Attachments = new List<SendGrid.Helpers.Mail.Attachment>() { calendarAttachment };
                }


                var response = await client.SendEmailAsync(msg);

                return response;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void SendEmail2(string email, string sub, string body)
        {

            
            MailMessage mail = new MailMessage();
            mail.Subject = sub;
            mail.From = new MailAddress("noreply@mecure.org");
            mail.To.Add(email);
            mail.Body = body;
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient("plesk3500.is.cc", 587);
            smtp.EnableSsl = false;
            NetworkCredential netCre = new NetworkCredential("noreply@mecure.org", "Ew1h%q70");
            smtp.Credentials = netCre;
            smtp.Send(mail);
        }



        public bool SendErrorEmail(string errorText)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.sendgrid.net", Convert.ToInt32(587));
                NetworkCredential credentials = new NetworkCredential("apikey", _appSettings.SendGridAPI.apiKey);
                SmtpServer.Credentials = credentials;
                mail.From = new MailAddress("donotreply@inyt.com");
                mail.To.Add("anand@futuresolutionsltd.com");
                mail.Subject = "Error logged on site";
                mail.Body = errorText;
                mail.IsBodyHtml = true;
                if (Convert.ToBoolean(_appSettings.SendErrorMessage))
                    SmtpServer.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
