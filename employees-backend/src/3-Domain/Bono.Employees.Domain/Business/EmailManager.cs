using Bono.Employees.Domain.Interfaces.Business;
using Bono.Employees.Infrastructure.Utils;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Bono.Employees.Domain.Business
{
    public class EmailManager : Settings, IEmailManager
    {
        public EmailManager(IConfiguration Configuration) : base(Configuration)
        {            
        }

        public bool Send(string to, string subject, string body, string cc = "", string co = "", List<Entities.Attachment> attachments = null)
        {
            try
            {
                MailMessage mailMessage = new MailMessage(this.EmailSender, to, subject, GetBody(body, to, subject));

                if (cc != string.Empty) mailMessage.CC.Add(cc);


                mailMessage.IsBodyHtml = true;

                //SMTP client
                SmtpClient sC = new SmtpClient(this.SmtpEmail, Convert.ToInt32(this.SmtpPort))
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(this.EmailSender, this.SmtpPassword),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    EnableSsl = Convert.ToBoolean(this.SmtpTls)
                };


                //Anexo
                if (attachments != null)
                {
                    foreach (var attachment in attachments)
                    {
                        MemoryStream ms = new MemoryStream(attachment.File);
                        mailMessage.Attachments.Add(new System.Net.Mail.Attachment(ms, attachment.FileName, attachment.MimeType));
                    }
                }

                sC.Send(mailMessage);


                return true;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    var message = ex.Message;
                }
                return false;
            }
        }

        public string GetBody(string Message, string User, string HeadMessage)
        {
            string message = @"
                        <div style='padding:40px 20px 36px; border-radius:8px; border:thin solid rgb(218,220,224); text-align:center'>
	                        <div style='line-height: 32px; padding-bottom: 24px; font-family: &quot;Google Sans&quot;, Roboto, RobotoDraft, Helvetica, Arial, sans-serif, serif, EmojiFont; border-bottom: thin solid rgb(218, 220, 224);'>
		                        <div style='font-size:24px'>@HeadMessage</div>
		                        <table align='center' style='margin-top:8px'>
			                        <tbody>
				                        <tr style='line-height:normal'>
					                        <td align='right' style='margin:0px; padding-right:8px; font-family:Roboto,RobotoDraft,Helvetica,Arial,sans-serif'>                        From:&nbsp;
						                        <b>@User</b>
					                        </td>
					                        <td style='margin:0px; font-family:Roboto,RobotoDraft,Helvetica,Arial,sans-serif'>                        &nbsp;</td>
				                        </tr>
			                        </tbody>
		                        </table>
	                        </div>
	                        <div style='line-height: 20px; padding-top: 20px; font-family: Roboto-Regular, Helvetica, Arial, sans-serif, serif, EmojiFont; font-size: 14px;'>
                                @Message    
		                    </div>
	                        </div>
	                        <div style='line-height: 18px; padding-top: 12px; font-family: Roboto-Regular, Helvetica, Arial, sans-serif, serif, EmojiFont; font-size: 11px;'>
		                            <div>
			                            <br>
                                        Information Technology ©2021
			                            </div>
			                            <div style='direction:ltr'>
				                            <br>
                                            &nbsp; © 2021 Vibbra
				                        </div>
			                        </div>
                        ";

            message = message.Replace("@Message", Message);
            message = message.Replace("@User", User);
            message = message.Replace("@HeadMessage", HeadMessage);

            return message;
        }
    }

}
