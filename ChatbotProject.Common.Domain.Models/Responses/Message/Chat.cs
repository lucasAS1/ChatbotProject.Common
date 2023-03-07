using System.Diagnostics.CodeAnalysis;

namespace ChatbotProject.Common.Domain.Models.Responses;

[ExcludeFromCodeCoverage]
public class Chat
{
    public int id { get; set; }
    public string first_name { get; init; } = null!;
    public string last_name { get; init; } = null!;
    public string username { get; init; } = null!;
    public string type { get; init; } = null!;
}