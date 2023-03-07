using ChatbotProject.Common.Domain.Interfaces.Services.MessageBroker;
using ChatbotProject.Common.Domain.Models.Requests;
using RabbitMQ.Client.Core.DependencyInjection.MessageHandlers;
using RabbitMQ.Client.Core.DependencyInjection.Models;
using static System.Text.Encoding;
using static Newtonsoft.Json.JsonConvert;

namespace TelegramBroker.Application.HostedServices;

public class MessageBrokerHostedService : IAsyncMessageHandler
{
    private readonly IMessageBrokerService _messageBrokerService;

    public MessageBrokerHostedService(IMessageBrokerService messageBrokerService)
    {
        _messageBrokerService = messageBrokerService;
    }

    public async Task Handle(MessageHandlingContext context, string matchingRoute)
    {
        var message = UTF8.GetString(context.Message.Body.ToArray());
        var messageRequest = DeserializeObject<MessageRequest>(message);
        
        await _messageBrokerService.SendMessageAsync(messageRequest);
    }
}