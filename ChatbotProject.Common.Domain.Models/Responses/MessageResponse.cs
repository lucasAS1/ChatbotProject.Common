using System.Diagnostics.CodeAnalysis;

namespace ChatbotProject.Common.Domain.Models.Responses;

[ExcludeFromCodeCoverage]
public class MessageResponse
{
    public bool ok { get; init; } = false;
    public Result result { get; init; } = null!;
}