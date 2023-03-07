using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using ChatbotProject.Common.Domain.Interfaces.Services.MessageBroker;
using ChatbotProject.Common.Domain.Models.Requests;
using ChatbotProject.Common.Domain.Models.Responses;
using Moq;
using RabbitMQ.Client.Core.DependencyInjection.Models;
using RabbitMQ.Client.Events;
using TelegramBroker.Application.HostedServices;
using Xunit;

namespace ChatbotProject.Common.Application.Tests;

public class MessageBrokerHostedServiceHandlerTests
{
    private readonly Mock<IMessageBrokerService> _messageBrokerServiceMockServiceMock;
    private readonly IFixture _fixture;

    public MessageBrokerHostedServiceHandlerTests()
    {
        _fixture = new Fixture();
        _messageBrokerServiceMockServiceMock = new Mock<IMessageBrokerService>();
       
        _fixture.Customize(new AutoMoqCustomization(){ConfigureMembers = true});
    }
    
    private void ConfigureMocks()
    {
        _messageBrokerServiceMockServiceMock
            .Setup(x => x.SendMessageAsync(It.IsAny<MessageRequest>()))
            .ReturnsAsync(_fixture.Create<MessageResponse>());
    }
    
    
    [Fact]
    public async Task ShouldProperlyHandleMessageReceivingEvent()
    {
        ConfigureMocks();
        var argsMock = _fixture
            .Build<BasicDeliverEventArgs>()
            .With(x => x.Body, Encoding.ASCII.GetBytes("{\"Chat\":\"teste\",\"chatId\":\"1234364\"}"))
            .WithAutoProperties()
            .Create();
        
        var aut = new MessageBrokerHostedService(_messageBrokerServiceMockServiceMock.Object);
        var messageHandlingContext = new MessageHandlingContext(argsMock, x => x.Exchange = "testexchange", false);
        
        await aut.Handle(messageHandlingContext, "test-route");
        Assert.Equal(messageHandlingContext.Message.Exchange, argsMock.Exchange);
    }
}