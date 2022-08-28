using System.Text.Json;

namespace EnterpriseManagementSystem.Contracts.JsonConverters;

public sealed class EmailAddressJsonConverter : JsonConverter<EmailAddress>
{
    public override EmailAddress? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => EmailAddress.TryParse(reader.GetString());

    public override void Write(Utf8JsonWriter writer, EmailAddress value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.Value);
}