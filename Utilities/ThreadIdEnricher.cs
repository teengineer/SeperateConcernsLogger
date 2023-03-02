using Serilog.Context;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.PostgreSQL;

namespace SeperateConcernsLogger.Utilities;
public class ThreadIdEnricher : ILogEventEnricher
{
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(
            "user_id", 1));

        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(
            "created_date", DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")));
    }
}

