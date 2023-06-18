namespace AcerPro.Application.Jobs.ACL;

public class EmailNotifierService : INotifierService
{
    public Task NotifyAsync(string address)
    {
        Console.WriteLine($"Email has been sent to {address}");
        return Task.CompletedTask;
    }
}
