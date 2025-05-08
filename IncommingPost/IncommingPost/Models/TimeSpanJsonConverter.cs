using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace IncommingPost.Models;

public class TimeSpanJsonConverter : JsonConverter<TimeSpan>
{
    public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            string value = reader.GetString();
            
            // Если строка пустая, используем 00:00
            if (string.IsNullOrEmpty(value))
                return TimeSpan.Zero;
                
            // Стандартный парсинг TimeSpan
            if (TimeSpan.TryParse(value, out TimeSpan result))
                return result;
                
            // Для формата HH:mm
            if (value.Length == 5 && value[2] == ':')
            {
                int hours = int.Parse(value.Substring(0, 2));
                int minutes = int.Parse(value.Substring(3, 2));
                return new TimeSpan(hours, minutes, 0);
            }
        }
        
        // Для всего остального возвращаем 00:00
        return TimeSpan.Zero;
    }

    public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(@"hh\:mm"));
    }
}