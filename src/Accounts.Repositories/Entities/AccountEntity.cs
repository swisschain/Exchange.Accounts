using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Accounts.Domain.Entities.Enums;

namespace Accounts.Repositories.Entities
{
    [Table("accounts")]
    public class AccountEntity
    {
        [Key]
        [Column("id", TypeName = "varchar(36)")]
        public string Id { get; set; }

        [Required]
        [Column("broker_id", TypeName = "varchar(36)")]
        public string BrokerId { get; set; }

        [Required]
        [Column("name", TypeName = "varchar(36)")]
        public string Name { get; set; }

        [Column("is_disabled")]
        public bool IsDisabled { get; set; }

        [Column("type", TypeName = "varchar(16)")]
        public AccountType Type { get; set; }

        [Column("created", TypeName = "timestamp with time zone")]
        public DateTime Created { get; set; }

        [Column("modified", TypeName = "timestamp with time zone")]
        public DateTime Modified { get; set; }
    }
}
