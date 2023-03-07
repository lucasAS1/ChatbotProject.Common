using ChatbotProject.Common.Domain.Models.Requests;
using ChatbotProject.Common.Domain.Models.Responses;

namespace ChatbotProject.Common.Domain.Interfaces.Services.MessageBroker;

public interface IMessageBrokerService
{
    public Task<MessageResponse> SendMessageAsync(MessageRequest request);
}