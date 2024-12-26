using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMoney.Models;

public class Account
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal InitialBalance { get; set; } = 0;
    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal CurrentBalance { get; set; } = 0;
    public string? Color { get; set; }
    public DateTime CreatedAt { get; set; } 
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public virtual User? User { get; set; }
    public virtual AccountType? AccountType { get; set; }
    public virtual Currency? Currency { get; set; }

    ICollection<Record> records { get; set; }
}
