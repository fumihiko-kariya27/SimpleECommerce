using System.ComponentModel.DataAnnotations;

namespace SimpleECommerce.Controllers.Auth
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "{0}は必須です")]
        [EmailAddress]
        [Display(Name = "メールアドレス")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "{0}は必須です")]
        [Display(Name = "パスワード")]
        public string Password { get; set; } = string.Empty;
    }
}
