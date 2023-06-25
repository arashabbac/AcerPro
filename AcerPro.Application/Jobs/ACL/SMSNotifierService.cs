namespace AcerPro.Application.Jobs.ACL;

public class SMSNotifierService : INotifierService
{
    public Task NotifyAsync(string address)
    {
        Console.WriteLine($"SMS has been sent to {address}");
        return Task.CompletedTask;
    }
}
