using Microsoft.Extensions.Options;
using OpenAI.Responses;
using SmartAppointmentSystem.Business.Contracts;
using SmartAppointmentSystem.Business.DTOs.AI;
using System.Text.Json;

namespace SmartAppointmentSystem.Infrastructure.AI;

#pragma warning disable OPENAI001

public sealed class OpenAiChatService(IOptions<OpenAiOptions> options) : IAiChatService
{
    private readonly ResponsesClient _client = new((options ?? throw new ArgumentNullException(nameof(options))).Value.ApiKey);

    private readonly string _model = options.Value.Model;

    public async Task<string> ChatAsync(string prompt, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(prompt);

        CreateResponseOptions request = new()
        {
            Model = _model
        };

        request.InputItems.Add(
            ResponseItem.CreateUserMessageItem(prompt));

        ResponseResult response = await _client.CreateResponseAsync(request, cancellationToken);

        return response.GetOutputText();
    }

    public async Task<AppointmentSearchIntent> ParseAppointmentSearchAsync(string prompt, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(prompt);

        var today = DateOnly.FromDateTime(DateTime.UtcNow.AddHours(3));

        CreateResponseOptions request = new()
        {
            Model = _model,
            Instructions = $"""
            You analyze Turkish healthcare appointment search requests.

            Today's date is {today:yyyy-MM-dd}.

            Extract only the search filters from the user's message.

            Branch should contain the requested medical branch.

            Resolve relative dates such as:
            bugün, yarın, cuma, gelecek pazartesi.

            Date must be yyyy-MM-dd.

            If the user says something like:
            "14'ten sonra"
            then AfterTime must be 14:00:00.

            If branch, date or after time is not provided, return null.

            Do not invent missing information.
            """
        };

        request.TextOptions = new ResponseTextOptions
        {
            TextFormat = ResponseTextFormat.CreateJsonSchemaFormat(
                "appointment_search",
                BinaryData.FromString("""
                {
                  "type": "object",
                  "properties": {
                    "branch": {
                      "type": ["string", "null"]
                    },
                    "date": {
                      "type": ["string", "null"]
                    },
                    "afterTime": {
                      "type": ["string", "null"]
                    }
                  },
                  "required": [
                    "branch",
                    "date",
                    "afterTime"
                  ],
                  "additionalProperties": false
                }
                """),
                jsonSchemaIsStrict: true)
        };

        request.InputItems.Add(ResponseItem.CreateUserMessageItem(prompt));

        ResponseResult response = await _client.CreateResponseAsync(request, cancellationToken);

        string json = response.GetOutputText();

        return JsonSerializer.Deserialize<AppointmentSearchIntent>(
            json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })
            ?? throw new InvalidOperationException("AI appointment search could not be parsed.");
    }
}

#pragma warning restore OPENAI001