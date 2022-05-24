using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEcommerce.Entity
{
    [Index("Name", Name = "UQ__Products__737584F665D8BD99", IsUnique = true)]
    public partial class Product
    {
        [Key]
        [Column("ProductID")]
        public int ProductId { get; set; }
        [StringLength(100)]
        public string Name { get; set; } = null!;
        [Column(TypeName = "decimal(19, 4)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(6, 4)")]
        public decimal Tax { get; set; }
        [Column("CategoryID")]
        public int? CategoryId { get; set; }
        [Required]
        public bool? IsActive { get; set; }

        [ForeignKey("CategoryId")]
        [InverseProperty("Products")]
        public virtual Category? Category { get; set; }
    }
}
