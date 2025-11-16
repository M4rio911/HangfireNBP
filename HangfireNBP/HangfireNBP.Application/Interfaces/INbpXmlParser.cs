using HangfireNBP.Application.DTO;

namespace HangfireNBP.Application.Interfaces;
public interface INbpXmlParser
{
    ParsedExchangeRateTable Parse(string xml);
}

