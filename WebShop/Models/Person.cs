﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Models
{
    public partial class Person
    {
        public Person()
        {
            PurchaseOrders = new HashSet<PurchaseOrder>();
        }

        public string PersonId { get; set; }
        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(30)]
        public string LastName { get; set; }
        [Required]
        [StringLength(30)]
        public string Country { get; set; }
        [Required]
        [StringLength(30)]
        public string City { get; set; }
        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [InverseProperty("Person")]
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
    }
}