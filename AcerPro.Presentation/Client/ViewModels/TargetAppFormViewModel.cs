using AcerPro.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AcerPro.Presentation.Client.ViewModels;

public class TargetAppFormViewModel
{
    public int? Id { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required")]
    public string Name { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "UrlAddress is required")]
    [RegularExpression(Constants.Regex.Url, ErrorMessage = "UrlAddress is invalid")]
    public string UrlAddress { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "MonitoringIntervalInSeconds is required")]
    public int? MonitoringIntervalInSeconds { get; set; }

    public List<NotifierFormViewModel> Notifiers { get; set; }
}


public class NotifierFormViewModel
{
    public int TargetAppId { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "Address is required")]
    public string Address { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "NotifierType is required")]
    public NotifierTypeViewModel? NotifierType { get; set; }
}

public class NotifierViewModel
{
    public int Id { get; set; }
    public string Address { get; set; }

    public NotifierTypeViewModel? NotifierType { get; set; }
}

public enum NotifierTypeViewModel
{
    Email = 1,
    SMS = 2,
    Call = 3,
}