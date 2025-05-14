using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace GoodStuff_Blazor.Components.User.Pages
{
    public partial class SingUp
    {
        [SupplyParameterFromForm]
        private SingUpModel singUpModel { get; set; } = new SingUpModel();


        private async Task HandleValidSubmit()
        {
            var a = singUpModel.Name;
            // Here you can handle the form submission, e.g., send the data to an API
            // For now, we'll just display a message
            await Task.Delay(1000); // Simulate a delay for the API call
            Console.WriteLine("Form submitted successfully!");
        }

        private async Task HandleInvalidSubmit(EditContext context)
        {
            var a = singUpModel.Name;


        }

    }
}