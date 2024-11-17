using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager.Core.DTO
{
    public class SignUpDTO
    {
        [Required(ErrorMessage = "Account Name can't be blank")]
        [RegularExpression("^(?=.*[a-zA-Z])(?=.*\\d)[^\\s]+$", ErrorMessage = "Account Name is an alphanumeric, and don't have space")]
        [Remote(action: "IsEmailAlreadyRegistered", controller: "Account", ErrorMessage = "Email is already is use")] //cai viewfeatures trc khi su dung //de kiem tra xem email da dk hay chu, thay vi nguoi dung nhan nut dk moi ktra dc thi no se tu dong kiem tra ma khong can reload trang
        public string AccountName { get; set; }

        [Required(ErrorMessage = "Person Name Can't be blank")]
        public string PersonName { get; set; }

        [Required(ErrorMessage = "Email can't be blank")]
        [EmailAddress(ErrorMessage = "Email should be in proper email address format")]
        [DataType(DataType.EmailAddress)]
        public string Email {  get; set; }

        [Required(ErrorMessage = "Phone can't be blank")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Phone number should contain numbers only")]
        [DataType(DataType.PhoneNumber)]
        public string Phone {  get; set; }

        [Required(ErrorMessage = "Password can't be blank")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password can't be blank")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirm password do not match")]
        public string ConfirmPassword { get; set; }
    }
}
