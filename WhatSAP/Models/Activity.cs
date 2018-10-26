using System;
using System.Collections.Generic;

namespace WhatSAP.Models
{
    public partial class Activity
    {
        public long ActivityId { get; set; }

        public string ActivityName { get; set; }

        public string Description { get; set; }

        public long CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public DateTime ActivityDate { get; set; }

        public long AddressId { get; set; }
        public virtual Address Address { get; set; }

        public double Price { get; set; }

        public int Capacity { get; set; }

        public decimal Rate { get; set; }

        public long ClientId { get; set; }
        public virtual Client Client { get; set; }

        public string Key { get; set; }
    }
}
