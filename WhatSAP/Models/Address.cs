using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WhatSAP.Models
{
    public partial class Address
    {
        public Address()
        {
            Activity = new HashSet<Activity>();
        }

        public long AddressId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Address 1")]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "Address 1 must be between 10 and 100 characters long!")]
        public string Address1 { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Address 2")]
        public string Address2 { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "City must be between 5 and 50 characters long!")]
        public string City { get; set; }

        [Required]
        public string Province { get; set; }

        [Required]
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }

        public ICollection<Activity> Activity { get; set; }
    }
}
