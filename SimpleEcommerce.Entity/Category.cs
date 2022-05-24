﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEcommerce.Entity
{
    [Index("Name", Name = "UQ__Categori__737584F699AB95CE", IsUnique = true)]
    public partial class Category
    {
        public Category()
        {
            InverseUpperCategory = new HashSet<Category>();
            Products = new HashSet<Product>();
        }

        [Key]
        [Column("CategoryID")]
        public int CategoryId { get; set; }
        [StringLength(100)]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        [Column("UpperCategoryID")]
        public int? UpperCategoryId { get; set; }

        [ForeignKey("UpperCategoryId")]
        [InverseProperty("InverseUpperCategory")]
        public virtual Category? UpperCategory { get; set; }
        [InverseProperty("UpperCategory")]
        public virtual ICollection<Category> InverseUpperCategory { get; set; }
        [InverseProperty("Category")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
