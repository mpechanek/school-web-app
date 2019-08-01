using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TNPW2_video_2_ASP_MVC.Models
{
    public class UserRegister
    {
        [Required(ErrorMessage = "Prosím zadejte Vaše jméno.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Prosím zadejte Vaše příjmení.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Prosím vyplňte login.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Heslo musíte vyplnit.")]
        [MinLength(5, ErrorMessage = "Heslo musí obsahovat minimálně pět znaků.")]
        [MaxLength(30)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Toto pole je povinné..")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Hesla se neshodují.")]
        public string PasswordRepeat { get; set; }

    }
}