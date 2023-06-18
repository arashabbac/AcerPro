namespace AcerPro.Application.Jobs.ACL;

public interface INotifierService
{
    Task NotifyAsync(string address);
}
