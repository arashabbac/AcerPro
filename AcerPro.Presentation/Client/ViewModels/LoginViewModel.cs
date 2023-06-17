using AcerPro.Common;
using System.ComponentModel.DataAnnotations;

namespace AcerPro.Presentation.Client.ViewModels;

public class LoginViewModel
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required")]
    [RegularExpression(Constants.Regex.Email, ErrorMessage = "Email is invalid")]
    public string Email { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
    public string Password { get; set; }
}
