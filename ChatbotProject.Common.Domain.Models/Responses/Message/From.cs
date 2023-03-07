using System.Diagnostics.CodeAnalysis;

namespace ChatbotProject.Common.Domain.Models.Responses;

[ExcludeFromCodeCoverage]
public class From
{
    public long id { get; set; }
    public bool is_bot { get; set; }
    public string first_name { get; set; } = null!;
    public string username { get; set; } = null!;
}