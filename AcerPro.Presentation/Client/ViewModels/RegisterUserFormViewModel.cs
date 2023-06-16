﻿using System.ComponentModel.DataAnnotations;

namespace AcerPro.Presentation.Client.ViewModels;
public class RegisterUserFormViewModel
{
    public int? Id { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "FirstName is required")]
    public string Firstname { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "LastName is required")]
    public string Lastname { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required")]
    [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Email is invalid")]
    public string Email { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
    [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,40}$",
        ErrorMessage = "Password lenght must be between 8 to 40 and must contain upper case, lower case and symbol characters")]
    public string Password { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "ConfirmPassword is required")]
    [Compare(otherProperty: "Password", ErrorMessage = "ConfirmPassword is not matched with Password")]
    public string ConfirmPassword { get; set; }
}
