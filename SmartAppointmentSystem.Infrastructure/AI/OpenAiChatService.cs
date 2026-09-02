using Microsoft.Extensions.Options;
using OpenAI.Responses;
using SmartAppointmentSystem.Business.Contracts;

namespace SmartAppointmentSystem.Infrastructure.AI;

#pragma warning disable OPENAI001

public sealed class OpenAiChatService : IAiChatService
{
    private readonly ResponsesClient _client;
    private readonly string _model;

    public OpenAiChatService(IOptions<OpenAiOptions> options)
    {
        ArgumentNullException.ThrowIfNull(options);

        OpenAiOptions settings = options.Value;
        _client = new ResponsesClient(settings.ApiKey);
        _model = settings.Model;
    }

    public async Task<string> ChatAsync(
        string prompt,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(prompt);

        CreateResponseOptions request = new()
        {
            Model = _model
        };
        request.InputItems.Add(ResponseItem.CreateUserMessageItem(prompt));

        ResponseResult response = await _client.CreateResponseAsync(
            request,
            cancellationToken);

        return response.GetOutputText();
    }
}

#pragma warning restore OPENAI001
