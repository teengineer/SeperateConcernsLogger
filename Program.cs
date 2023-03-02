global using FastEndpoints;
using Autofac;
using FastEndpoints.Swagger;
using NpgsqlTypes;
using SeperateConcernsLogger;
using SeperateConcernsLogger.Utilities;
using Serilog;
using Serilog.Context;
using Serilog.Sinks.PostgreSQL;

var builder = WebApplication.CreateBuilder();
var config = builder.Configuration;
builder.Services.AddFastEndpoints();
builder.Services.AddSwaggerDoc();



#region Serilog configuration
string tableName = "logs";

IDictionary<string, ColumnWriterBase> columnOptions = new Dictionary<string, ColumnWriterBase>
{
    { "level", new LevelColumnWriter(true, NpgsqlDbType.Varchar) },
    { "created_date", new TimestampColumnWriter(NpgsqlDbType.TimestampTz) },
    { "exception", new ExceptionColumnWriter(NpgsqlDbType.Text) },
    { "properties", new PropertiesColumnWriter(NpgsqlDbType.Jsonb) },
};

Log.Logger = new LoggerConfiguration()
    .WriteTo.PostgreSQL(config.GetConnectionString("DbConnection"), tableName, columnOptions, useCopy: true, batchSizeLimit: 40, period: new TimeSpan(0, 0, 10), formatProvider: null)
      .Enrich.With(new ThreadIdEnricher())
    .CreateLogger();
builder.Logging.ClearProviders();
Serilog.Debugging.SelfLog.Enable(Console.Error);
//builder.Host.UseSerilog(); //Bu satir sistem mesajlarinin da yakalanip loga yazilmasini saglar
#endregion

var app = builder.Build();
//app.UseSerilogRequestLogging();
app.UseAuthorization();
app.UseFastEndpoints();
app.UseSwaggerGen();
app.Run();
