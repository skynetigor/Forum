using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Forum.WEB.Models
{
    public class LoginViewModel
    {
        [Display(Name = "Имя пользователя")]
        [Required]
        public string UserName { get; set; }
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
    }
}