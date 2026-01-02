using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProjectDIgitization.Entities.Entities
{
    public class UserTypes
    {
        [Key]
        public Guid Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; } = null!;

        [MaxLength(50)]
        public string? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        [MaxLength(50)]
        public string? LastModifiedBy { get; set; }

        public DateTime? LastModifiedOn { get; set; }

        public ICollection<Users> Users { get; set; } = new List<Users>();
    }
}
