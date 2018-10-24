using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WhatSAP.Models
{
    public partial class Booking
    {
        public long BookingId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime BookingDate { get; set; }

        [Required]
        public int NumberOfPeople { get; set; }

        [Required]
        public double Total { get; set; }

        public long CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public long ClientId { get; set; }
        public virtual Client Client { get; set; }

        public long ActivityId { get; set; }
        public virtual Activity Activity { get; set; }

        public long PaymentId { get; set; }
        public virtual Payment Payment { get; set; }
    }
}
