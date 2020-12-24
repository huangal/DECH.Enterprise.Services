using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using DECH.Enterprise.Services.Customers.Contracts.Models;

namespace DECH.Enterprise.Services.Customers.Contracts.Converters
{
    public class CategoryJsonConverter : JsonConverter<Customer>
    {
        public override Customer Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, Customer value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }


    public class DateTimeOffsetConverter : JsonConverter<DateTimeOffset>
    {
        public override DateTimeOffset Read(ref Utf8JsonReader reader,Type typeToConvert,JsonSerializerOptions options) =>
                DateTimeOffset.ParseExact(reader.GetString(), "MM/dd/yyyy", CultureInfo.InvariantCulture);

        public override void Write(Utf8JsonWriter writer, DateTimeOffset dateTimeValue,JsonSerializerOptions options) =>
                writer.WriteStringValue(dateTimeValue.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture));
    }


    public class AccountIdConverter : JsonConverter<AccountId>
    {
        public override AccountId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {

            string id = reader.GetString();

            return new AccountId() { Value = Guid.Parse(id) };
        }

        public override void Write(Utf8JsonWriter writer, AccountId value, JsonSerializerOptions options)
        {
            if (!(value is AccountId accountId))
                throw new JsonException("Expected AccountId object value.");
            writer.WriteStringValue(accountId.Value);
        }
    }

    public class DefaultFloatConverter : JsonConverter<float>
    {
        public override float Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
        public override void Write(Utf8JsonWriter writer, float value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value);
        }
    }


    //public class AccountIdConverter : JsonConverter<Guid>
    //{
    //    public override Guid Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    //    {
    //        return Guid.Parse(reader.GetString());
    //    }

    //    public override void Write(Utf8JsonWriter writer, Guid value, JsonSerializerOptions options)
    //    {
    //        //if(!(value is AccountId accountId))
    //        //throw new JsonSerializationException("Expected AccountId object value.");

    //        // custom response 
    //        writer.WriteStringValue(value);
    //    }
    //}



    //public class ForceDefaultConverter : JsonConverter<Enum>
    //{

    //    public override Enum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    //    {
    //        throw new NotImplementedException();
    //    }
    //    public override void Write(Utf8JsonWriter writer, Enum value, JsonSerializerOptions options)
    //    {
    //        writer.WriteNumber
    //    }
    //}
}
