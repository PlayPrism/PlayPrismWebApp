using System.Globalization;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using PlayPrism.Core.Settings;

namespace PlayPrism.BLL.Helpers;

public class EmailWorker
{
    private readonly AppSettings _appSettings;

    public EmailWorker(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings?.Value;
    }

    public async Task<bool> SendVerificationCodeByEmailAsync(string emailTo, string code)
    {
        try
        {
            var toMailAddress = new MailAddress(emailTo);
            var subject = "PlayPrism | Confirmation code";
            
            string htmlBody = await File.ReadAllTextAsync("email_template.html");

            htmlBody = htmlBody.Replace("{{verificationCode}}", code);

            await SendMailMessageAsync(toMailAddress, subject, htmlBody);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    
    
    public async Task<bool> SendProductItemKeyByEmailAsync(string emailTo, string productKey)
    {
        try
        {
            var random = new Random();
            var toMailAddress = new MailAddress(emailTo);
            var subject = "PlayPrism | Product Key";
            
            string htmlBody = await File.ReadAllTextAsync("email_template.html");

            htmlBody = htmlBody.Replace("{{verificationCode}}", productKey);

            await SendMailMessageAsync(toMailAddress, subject, htmlBody);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    
    

    private async Task<bool> SendMailMessageAsync(MailAddress mailAddressTo, string subject, string body)
    {
        try
        {
            var fromMailAddress = new MailAddress(_appSettings.SmtpSettings.SenderEmail);

            var message = new MailMessage(fromMailAddress, mailAddressTo);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient(_appSettings.SmtpSettings.Host, _appSettings.SmtpSettings.Port);
            smtpClient.Credentials =
                new NetworkCredential(fromMailAddress.Address, _appSettings.SmtpSettings.Password);
            smtpClient.EnableSsl = _appSettings.SmtpSettings.EnableSsl;

            await smtpClient.SendMailAsync(message);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}