using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace HotelApp.Blazor.MudBlazor.Pages
{
    public partial class TestUI
    {
        bool success;
        RequiredInputs newGuest = new RequiredInputs();

        public class RequiredInputs
        {
            [Required]
            [StringLength(8, ErrorMessage = "Name length can't be more than 8.")]
            public string FirstName { get; set; }

            [Required]
            [StringLength(8, ErrorMessage = "Name length can't be more than 8.")]
            public string LastName { get; set; }
        }

        private void OnValidSubmit(EditContext context)
        {
            success = true;
            StateHasChanged();
        }
    }
}
