using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WhatSAP.Models
{
    public partial class Payment
    {
        public Payment()
        {
            Booking = new HashSet<Booking>();
        }

        public long PaymentId { get; set; }

        [Required]
        public bool PaymentState { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime PaymentDate { get; set; }

        public ICollection<Booking> Booking { get; set; }
    }
}
