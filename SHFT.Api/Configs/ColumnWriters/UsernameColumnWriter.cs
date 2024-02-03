using NpgsqlTypes;
using Serilog.Events;
using Serilog.Sinks.PostgreSQL;

namespace SHFT.API.Configs.ColumnWriters;

public class UsernameColumnWriter : ColumnWriterBase
{
    public UsernameColumnWriter() : base(NpgsqlDbType.Varchar)
    {
    }

    public override object GetValue(LogEvent logEvent, IFormatProvider formatProvider = null)
    {
        var (username, value) = logEvent.Properties.FirstOrDefault(c => c.Key == "Username");
        return value?.ToString();
    }
}