﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager.Core.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Email can't be blank")]
        public string? AccountName { get; set; }

        [Required(ErrorMessage ="Password can't be blank")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

    }
}
