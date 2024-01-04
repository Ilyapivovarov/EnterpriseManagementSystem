namespace EnterpriseManagementSystem.Contracts.JsonConverters;

public sealed class PasswordJsonConverter : JsonConverter<Password>
{
    public override Password Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => Password.Parse(reader.GetString());

    public override void Write(Utf8JsonWriter writer, Password value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.Value);
}