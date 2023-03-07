using System.Diagnostics.CodeAnalysis;

namespace ChatbotProject.Common.Domain.Models.Responses;

[ExcludeFromCodeCoverage]
public class Result
{
    public int message_id { get; set; }
    public From from { get; set; } = null!;
    public Chat chat { get; set; } = null!;
    public int date { get; set; }
    public string text { get; set; } = null!;
}