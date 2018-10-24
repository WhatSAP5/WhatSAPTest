using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WhatSAP.Models
{
    public partial class Comment
    {
        public long CommentId { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(300, MinimumLength = 10, ErrorMessage = "Comment must be between 10 and 300 characters long!")]
        public string Body { get; set; }

        [Required]
        public decimal Rate { get; set; }

        [Required]
        public long CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        [Required]
        public long ActivityId { get; set; }
        public virtual Activity Activity { get; set; }
    }
}
