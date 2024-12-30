﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyMoney.Models
{
    public class Record
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime? Date { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; } = 0;
        public string Description { get; set; } = string.Empty;
        public string Type { get; set; }
        public DateTime CreatedAt { get; set; } 
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public virtual User? User { get; set; }
        public int FromAccountId { get; set; }
        public virtual Account FromAccount { get; set; }

        public int? ToAccountId { get; set; }
        public virtual Account? ToAccount { get; set; }

        // Relationship with Category
        public int CategoryId { get; set; }

        public Category Category { get; set; }

    }
}

