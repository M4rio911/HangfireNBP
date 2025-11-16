using HangfireNBP.Application.DTO;
using HangfireNBP.Application.Interfaces;
using System.Globalization;
using System.Xml.Linq;

namespace HangfireNBP.Application.Parsing;
public class NbpXmlParser : INbpXmlParser
{
    public ParsedExchangeRateTable Parse(string xml)
    {
        var doc = XDocument.Parse(xml);

        var table = doc.Descendants("ExchangeRatesTable").First();

        var tableCode = table.Element("Table")!.Value;
        var fileNumber = table.Element("No")!.Value;
        var effectiveDate = DateTime.Parse(table.Element("EffectiveDate")!.Value);

        var rates = table
            .Descendants("Rate")
            .Select(r => new ParsedExchangeRate
            {
                Name = r.Element("Currency")!.Value,
                Code = r.Element("Code")!.Value,
                Mid = decimal.Parse(r.Element("Mid")!.Value, CultureInfo.InvariantCulture)
            })
            .ToList();

        return new ParsedExchangeRateTable
        {
            TableCode = tableCode,
            FileNumber = fileNumber,
            EffectiveDate = effectiveDate,
            Rates = rates
        };
    }
}
