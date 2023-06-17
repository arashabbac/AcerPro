using AcerPro.Common;
using System.ComponentModel.DataAnnotations;

namespace AcerPro.Presentation.Client.ViewModels;

public class AddTargetAppFormViewModel
{
    public int? Id { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required")]
    public string Name { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "UrlAddress is required")]
    [RegularExpression(Constants.Regex.Url, ErrorMessage = "UrlAddress is invalid")]
    public string UrlAddress { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "MonitoringIntervalInSeconds is required")]
    public int? MonitoringIntervalInSeconds { get; set; }
}
