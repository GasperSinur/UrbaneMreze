using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace UrbaneMreze.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "E-pošta")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Koda")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Zapomni si ta brskalnik?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "E-pošta")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "E-pošta")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Geslo")]
        public string Password { get; set; }

        [Display(Name = "Zapomni si?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-pošta")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vnesite Uporabniško ime.")]
        [Remote("IsUserNameUnique", "Account", ErrorMessage = "Izbrano Uporabniško ime je že zasedeno.")]
        [Display(Name = "Uporabniško ime")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} mora biti dolgo vsaj {2} znakov.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Geslo")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potrdi geslo")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Geslo in Potrdi geslo se ne ujemata.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-pošta")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} mora biti dolgo vsaj {2} znakov.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Geslo")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potrdi geslo")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Geslo in Potrdi geslo se ne ujemata.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-pošta")]
        public string Email { get; set; }
    }
}
