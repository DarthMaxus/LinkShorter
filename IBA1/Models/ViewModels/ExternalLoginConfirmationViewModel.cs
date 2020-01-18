using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IBA1.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Адрес электронной почты")]
        public string Email { get; set; }
    }
}
