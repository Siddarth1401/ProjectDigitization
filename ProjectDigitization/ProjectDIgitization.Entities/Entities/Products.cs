using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProjectDIgitization.Entities.Entities
{
    public class Products
    {
        [Key]
        public Guid Id { get; set; }

        [Required, MaxLength(100)]
        public string ProductName { get; set; } = null!;

        public int OverallStock { get; set; }

        public decimal Price { get; set; }

        public decimal? Discount { get; set; }

        [MaxLength(50)]
        public string? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        [MaxLength(50)]
        public string? LastModifiedBy { get; set; }

        public DateTime? LastModifiedOn { get; set; }
    }
}
