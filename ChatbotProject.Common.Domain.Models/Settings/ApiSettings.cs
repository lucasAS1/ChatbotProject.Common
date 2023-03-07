using System.Diagnostics.CodeAnalysis;

namespace ChatbotProject.Common.Domain.Models.Settings;

[ExcludeFromCodeCoverage]
public class ApiSettings
{
    public RabbitMqSettings RabbitMqSettings { get; init; } = null!;
}