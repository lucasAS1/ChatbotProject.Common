using Newtonsoft.Json;

namespace ChatbotProject.Common.Domain.Models.Requests;

public enum InteractiveMessageType
{
    [JsonProperty("Button")]
    Button,
    [JsonProperty("Menu")]
    Menu
}