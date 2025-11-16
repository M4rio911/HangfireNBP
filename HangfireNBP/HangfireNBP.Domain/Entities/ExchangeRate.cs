using Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HangfireNBP.Domain.Entities;

public class ExchangeRate : BaseEntity
{
    [Key]
    public long Id { get; set; }

    [Required]
    public long ExchangeRateTableId { get; set; }

    [ForeignKey(nameof(ExchangeRateTableId))]
    [JsonIgnore]
    public ExchangeRateTable ExchangeRateTable { get; set; }

    [Required, MaxLength(50)]
    public string Name { get; set; } 

    [Required, MaxLength(10)]
    public string Code { get; set; }

    [Column(TypeName = "decimal(12,10)")]
    public decimal Mid { get; set; }
}