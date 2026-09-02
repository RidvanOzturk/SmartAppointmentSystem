using System.ComponentModel.DataAnnotations;

namespace SmartAppointmentSystem.Api.Models;

public sealed class AiChatRequestModel
{
    [Required]
    [MinLength(1)]
    [MaxLength(4000)]
    public string Prompt { get; init; } = string.Empty;
}
