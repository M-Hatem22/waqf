using System.ComponentModel.DataAnnotations;

namespace BackEgyVision.Models
{
    public class LoginModel
    {
        [Required(ErrorMessageResourceName = "EmailRequired", ErrorMessageResourceType = typeof(Resources.Models_LoginModel))]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessageResourceName = "PasswordRequired", ErrorMessageResourceType = typeof(Resources.Models_LoginModel))]
        [Display(Name = "PasswordDisplay")]
        public string Password { get; set; }
        public string UserName { get; set; }
        public bool IsPostBack { get; set; }
        public string Error { get; set; }

    }
}
