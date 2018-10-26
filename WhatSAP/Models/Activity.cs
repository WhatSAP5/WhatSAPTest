using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WhatSAP.Models
{
    public partial class Activity
    {
        public long ActivityId { get; set; }

        [Display(Name="Activity Name")]
        public string ActivityName { get; set; }

        public string Description { get; set; }

        [Display(Name="Category")]
        public long CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [Display(Name = "Date")]
        public DateTime ActivityDate { get; set; }

        [Display(Name = "Address")]
        public long AddressId { get; set; }
        public virtual Address Address { get; set; }

        public double Price { get; set; }

        public int Capacity { get; set; }

        public decimal Rate { get; set; }

        [Display(Name = "Client")]
        public long ClientId { get; set; }
        public virtual Client Client { get; set; }

    }
}
