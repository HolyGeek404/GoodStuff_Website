using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GoodStuff_Blazor.Components.User
{
    public class SingUpModel
    {
        [Required(ErrorMessage = "You have to provide your Name")]
        [MinLength(3)]
        public string Name { get; set; }


        [Required(ErrorMessage = "You have to provide your Surname")]
        public string Surname { get; set; }


        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }


        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
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
}
