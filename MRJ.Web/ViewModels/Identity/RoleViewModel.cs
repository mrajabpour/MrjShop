using System.ComponentModel.DataAnnotations;

namespace MRJ.Web.ViewModels.Identity
{
    public class RoleViewModel
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "RoleName")]
        public string Name { get; set; }
    }
}