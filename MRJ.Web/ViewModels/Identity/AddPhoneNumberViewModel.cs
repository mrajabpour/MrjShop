using System.ComponentModel.DataAnnotations;

namespace MRJ.Web.ViewModels.Identity
{
    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }
}