using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectDIgitization.Entities.Entities
{
    public class Users
    {
        [Key]
        public Guid Id { get; set; }

        [Required, MaxLength(50)]
        public string UserId { get; set; } = null!;

        [Required, MaxLength(100)]
        public string UserName { get; set; } = null!;

        [ForeignKey(nameof(UserType))]
        public Guid UserTypeId { get; set; }

        public UserTypes? UserType { get; set; }

        [MaxLength(50)]
        public string? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        [MaxLength(50)]
        public string? LastModifiedBy { get; set; }

        public DateTime? LastModifiedOn { get; set; }
    }
}
