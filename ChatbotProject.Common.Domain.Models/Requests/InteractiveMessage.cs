using System.Diagnostics.CodeAnalysis;

namespace ChatbotProject.Common.Domain.Models.Requests;

[ExcludeFromCodeCoverage]
public class InteractiveMessage
{
    public InteractiveMessageType Type { get; set; }
    public List<string> Options { get; set; } = null!;
}