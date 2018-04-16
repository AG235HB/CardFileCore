using System.ComponentModel.DataAnnotations;

namespace CardFileCore.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name="Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Password")]
        public string Password { get; set; }

        [Display(Name ="MEMBER?!")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
