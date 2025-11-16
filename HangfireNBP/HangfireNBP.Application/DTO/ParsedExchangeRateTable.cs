namespace HangfireNBP.Application.DTO;
public class ParsedExchangeRateTable
{
    public string TableCode { get; set; }
    public string FileNumber { get; set; }
    public DateTime EffectiveDate { get; set; }
    public List<ParsedExchangeRate> Rates { get; set; }
}
