namespace BookingTest;

public class FakeEmailSender
{
    public bool EmailWasSent { get; private set; }
    public string Recipient { get; private set; }
    public string Subject { get; private set; }
    public string Message { get; private set; }

    public Task SendEmail(
        string recipient,
        string subject,
        string message)
    {
        EmailWasSent = true;
        Recipient = recipient;
        Subject = subject;
        Message = message;

        return Task.CompletedTask;
    }
}