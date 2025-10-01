using System.ComponentModel.DataAnnotations;

namespace GoodStuff.Website.Domain.Models.User;

public class SignInModel
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}