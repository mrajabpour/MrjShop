using System.ComponentModel.DataAnnotations;

namespace MRJ.Web.ViewModels.Identity
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}