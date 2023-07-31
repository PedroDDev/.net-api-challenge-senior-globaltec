using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Infrastructure.Converters
{
    public class DateConverter : JsonConverter<DateTime>
    {
        private readonly string _formatDate = "dd/MM/yyyy";

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            DateTime expectedDate;

            if(!DateTime.TryParseExact(reader.GetString(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out expectedDate)) throw new ArgumentException("Error while trying to convert Date. (Correct Date Format: dd/MM/yyyy)");

            return DateTime.ParseExact(reader.GetString()!, _formatDate, CultureInfo.InvariantCulture);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(_formatDate));
        }
    }
}