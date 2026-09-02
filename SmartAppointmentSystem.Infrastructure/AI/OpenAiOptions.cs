namespace SmartAppointmentSystem.Infrastructure.AI;

public sealed class OpenAiOptions
{
    public const string SectionName = "OpenAI";

    public string ApiKey { get; set; } = string.Empty;

    public string Model { get; set; } = "gpt-5.6-luna";
}
