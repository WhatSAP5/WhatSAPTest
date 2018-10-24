using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WhatSAP.Models
{
    public partial class Administrator
    {
        public long AdminId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
