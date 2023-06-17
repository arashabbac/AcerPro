using System.Collections.Generic;

namespace AcerPro.Presentation.Client.ViewModels;

public class TargetAppViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string UrlAddress { get; set; }
    public int MonitoringIntervalInSeconds { get; set; }

    public List<NotifierFormViewModel> Notifiers { get; set; }
}
