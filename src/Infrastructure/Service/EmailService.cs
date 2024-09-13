using System.Net.Mail;
using Interface;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Service;

public class EmailService : IEmailService
{
    private readonly SmtpClient _smtpClient;
    private readonly ILogger<EmailService> _logger;

    public EmailService(ILogger<EmailService> logger, SmtpClient smtpClient)
    {
        _logger = logger;
        _smtpClient = smtpClient;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var mailMessage = new MailMessage
        {
            From = new MailAddress("care@yogihosting.com"),
            Subject = "Confirm your email",
            IsBodyHtml = true,
            Body = htmlMessage
        };

        mailMessage.To.Add(new MailAddress(email));

        try
        {
            await _smtpClient.SendMailAsync(mailMessage);
        }
        catch (Exception ex)
        {
            _logger.LogError("Unable to send the confirmatin email with the {@Exception} to the {@EmailAddress}", ex, email);
        }
    }
}
