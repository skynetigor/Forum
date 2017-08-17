using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Forum.WEB.Models
{

    public class AccountSettingsViewModel
    {
        const string Req = "Обязательно для заполнения";
        const string NOT_EQUAL = "Пароли должны быть равны!";

        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = Req)]
        [Display(Name = "Текущий пароль")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = Req)]
        [Display(Name = "Новый пароль")]
        public string FirstPassword { get; set; }

        [Required(ErrorMessage = Req)]
        [Compare("FirstPassword", ErrorMessage = NOT_EQUAL)]
        [Display(Name = "Повторите пароль")]
        public string SecondPassword { get; set; }
    }
}