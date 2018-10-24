using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WhatSAP.Models
{
    public partial class Client
    {
        public Client()
        {
            Activity = new HashSet<Activity>();
            Booking = new HashSet<Booking>();
        }

        public long ClientId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public ICollection<Activity> Activity { get; set; }
        public ICollection<Booking> Booking { get; set; }
    }
}
