using System.Diagnostics.CodeAnalysis;

namespace ChatbotProject.Common.Domain.Models.Settings;

[ExcludeFromCodeCoverage]
public record MongoDbSettings
{
    public string Url { get; set; } = null!;
    public string Database { get; set; } = null!;
}