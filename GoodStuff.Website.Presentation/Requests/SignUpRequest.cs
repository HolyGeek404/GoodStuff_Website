using System.ComponentModel.DataAnnotations;
using GoodStuff.Website.Presentation.Requests.Validators;

namespace GoodStuff.Website.Presentation.Requests;

public class SignUpRequest
{
    [Required(ErrorMessage = "You have to provide your Name")]
    [NameValidation(ErrorMessage = "Name can't contains numbers and special characters.")]
    public string Name { get; set; }


    [Required(ErrorMessage = "You have to provide your Surname")]
    [NameValidation(ErrorMessage = "Surname can't contains numbers and special characters.")]
    public string Surname { get; set; }


    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    public string Email { get; set; }


    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    [PasswordValidation(ErrorMessage =
        "Password must contains one: upercase, numer, special character and be at least 8 long.")]
    public string Password { get; set; }

    [Required]
    [Compare("Password", ErrorMessage = "The passwords does not match.")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }


    [CheckboxValidation(ErrorMessage = "This term is required")]
    public bool AcceptTerms { get; set; }

    [CheckboxValidation(ErrorMessage = "This term is required")]
    public bool AcceptEmailSpam { get; set; }

    [CheckboxValidation(ErrorMessage = "This term is required")]
    public bool AcceptSellingUserData { get; set; }
}