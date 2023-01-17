using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace HotelApp.Blazor.MudBlazor.Pages
{
    public partial class TestUI2
    {
        RegisterAccountForm model = new RegisterAccountForm();
        bool success;

        public class RegisterAccountForm
        {
            [Required]
            [StringLength(8, ErrorMessage = "Name length can't be more than 8.")]
            public string Username { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

        }

        private void OnValidSubmit(EditContext context)
        {
            success = true;
            StateHasChanged();
        }
    }
}
