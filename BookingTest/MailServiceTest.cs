using API.Services;

namespace BookingTest;

public class MailServiceTest
{
    [Fact]
    public async Task SendMailSunshineTest()
    {
        var fakeEmailSender = new FakeEmailSender();
        var sut = new EmailService(FakeEmailSender);

        string recipient = "test@email.dk";
        string subject = "Bookingbekræftelse";
        string message = "Din booking er bekræftet";

        await sut.SendBookingConfirmation(
            recipient, subject, message);
        
        Assert.True(fakeEmailSender.EmailWasSent);
        Assert.Equal(recipient, fakeEmailSender.Recipient);
        Assert.Equal(subject, fakeEmailSender.Subject);
        Assert.Equal(message, fakeEmailSender.Message);


    }
}