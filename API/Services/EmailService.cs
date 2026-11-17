using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.Threading;

namespace API.Services;

public class EmailService
{
    private readonly string _emailAddress;
    private readonly string _emailPassword;

    public EmailService(string emailAddress, string emailPassword)
    {
        _emailAddress = emailAddress;
        _emailPassword = emailPassword;
    }

    // Generel metode til at sende mails
    public async Task SendEmail(
        string recipient,
        string subject,
        string message)
    {
        var email = new MimeMessage();

        email.From.Add(
            new MailboxAddress("Booking System", _emailAddress));

        email.To.Add(MailboxAddress.Parse(recipient));
        email.Subject = subject;

        email.Body = new TextPart("plain")
        {
            Text = message
        };

        using var smtp = new SmtpClient();

        await smtp.ConnectAsync(
            "smtp.gmail.com",
            587,
            SecureSocketOptions.StartTls);

        await smtp.AuthenticateAsync(
            _emailAddress,
            _emailPassword);

        await smtp.SendAsync(email, CancellationToken.None);
        await smtp.DisconnectAsync(true);
    }

    // Bookingbekræftelse
    public Task SendBookingConfirmation(string recipient)
    {
        return SendEmail(
            recipient,
            "Bookingbekræftelse",
            "Din booking af fællesrummet er blevet bekræftet.");
    }

    // Adgangskode til fællesrummet
    public Task SendAccessCode(string recipient, string accessCode)
    {
        return SendEmail(
            recipient,
            "Adgangskode til fællesrummet",
            $"Din adgangskode er: {accessCode}");
    }

    // Hjælp-mail
    public Task SendHelpEmail(
        string senderEmail,
        string subject,
        string message,
        string adminEmail)
    {
        return SendEmail(
            adminEmail,
            $"Hjælp: {subject}",
            $"Fra: {senderEmail}\n\n{message}");
    }
}