namespace AcerPro.Application.Jobs.ACL;

public class CallNotifierService : INotifierService
{
    public Task NotifyAsync(string address)
    {
        Console.WriteLine($"Call with {address} has been accomplished");
        return Task.CompletedTask;
    }
}
