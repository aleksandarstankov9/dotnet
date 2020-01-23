﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Models
{
    [Table("Product")]
    public partial class Product
    {
        public Product()
        {
            Items = new HashSet<Item>();
        }

        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        [Required]
        [StringLength(100)]
        public string ProductName { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }
        [StringLength(100)]
        public string ProductDescription { get; set; }

        [ForeignKey("CategoryId")]
        [InverseProperty("Products")]
        public virtual Category Category { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<Item> Items { get; set; }
    }
}