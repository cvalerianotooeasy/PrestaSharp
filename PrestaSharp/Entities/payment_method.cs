using Bukimedia.PrestaSharp.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Bukimedia.PrestaSharp.Entities
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using J = Newtonsoft.Json.JsonPropertyAttribute;
    using R = Newtonsoft.Json.Required;
    using N = Newtonsoft.Json.NullValueHandling;

    public partial class PaymentMethods : PrestaShopEntity, IPrestaShopFactoryEntity
    {
        [J("id", NullValueHandling = N.Ignore)] public long? id { get; set; }
        [J("code", NullValueHandling = N.Ignore)] public long? Code { get; set; }
        [J("status", NullValueHandling = N.Ignore)] public string Status { get; set; }
        [J("message", NullValueHandling = N.Ignore)] public string Message { get; set; }
        [J("data", NullValueHandling = N.Ignore)] public List<PaymentMethod> Data { get; set; }
      
    }

    public partial class PaymentMethod
    {
        [J("id_module", NullValueHandling = N.Ignore)] [JsonConverter(typeof(ParseStringConverter))] public long? IdModule { get; set; }
        [J("id_hook", NullValueHandling = N.Ignore)] [JsonConverter(typeof(ParseStringConverter))] public long? IdHook { get; set; }
        [J("name", NullValueHandling = N.Ignore)] public string Name { get; set; }
        [J("position", NullValueHandling = N.Ignore)] [JsonConverter(typeof(ParseStringConverter))] public long? Position { get; set; }
    }

    public partial class PaymentMethods
    {
        public static PaymentMethods FromJson(string json) => JsonConvert.DeserializeObject<PaymentMethods>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this PaymentMethods self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }

}
