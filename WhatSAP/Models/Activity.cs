using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace WhatSAP.Models
{
    public partial class Activity
    {
        public Activity()
        {
            Booking = new HashSet<Booking>();
            Comment = new HashSet<Comment>();
        }

        public long ActivityId { get; set; }

        //private string _key;

        //public string Key
        //{
        //    get
        //    {
        //        if (_key == null)
        //        {
        //            _key = Regex.Replace(ActivityName.ToLower(), "[^a-z0-9]", "-");
        //        }
        //        return _key;
        //    }
        //    set { _key = value; }
        //}

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Activity Name")]
        public string ActivityName { get; set; }

        [Required]
        [MinLength(30, ErrorMessage = "Description must be at least 30 characters long!")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Activity Date")]
        public DateTime ActivityDate { get; set; }

        public long AddressId { get; set; }
        public virtual Address Address { get; set; }

        public double Price { get; set; }

        public int Capacity { get; set; }

        public decimal Rate { get; set; }

        [Required]
        public long ClientId { get; set; }
        public virtual Client Client { get; set; }

        public ICollection<Booking> Booking { get; set; }
        public ICollection<Comment> Comment { get; set; }
    }
}
