using System.ComponentModel.DataAnnotations;

namespace GoodStuff.Website.Presentation.Requests;

public class SignInRequest
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}