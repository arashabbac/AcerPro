using AcerPro.Presentation.Client.ViewModels;

namespace AcerPro.Presentation.Client;

public static class Extensions
{
    public static string GetNotifierColor(this NotifierTypeViewModel? notifier)
    {
        return notifier switch
        {
            NotifierTypeViewModel.Email => "green",
            NotifierTypeViewModel.SMS => "magenta",
            NotifierTypeViewModel.Call => "geekblue",
            _ => "",
        };
    }
}
