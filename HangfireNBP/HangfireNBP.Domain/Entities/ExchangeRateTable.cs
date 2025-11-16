using HangfireNBP.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Entities;
public class ExchangeRateTable : BaseEntity
{
    [Key]
    public long Id { get; set; }

    [Required, MaxLength(10)]
    public string TableCode { get; set; }

    [Required, MaxLength(50)]
    public string FileNumber { get; set; }

    [Required]
    public DateTime EffectiveDate { get; set; }

    public ICollection<ExchangeRate> ExchangeRates { get; set; } = new List<ExchangeRate>();
}