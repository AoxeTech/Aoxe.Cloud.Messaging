using Azure;
using Azure.Messaging.EventGrid;

namespace Aoxe.Azure.EventGrid;

public class EventGridPublisher
{
    private readonly EventGridPublisherClient _client;

    public EventGridPublisher(string topicEndpoint, string topicKey)
    {
        var credential = new AzureKeyCredential(topicKey);
        _client = new EventGridPublisherClient(new Uri(topicEndpoint), credential);
    }

    public async Task PublishEventAsync(string subject, string eventType, object data)
    {
        var eventGridEvent = new EventGridEvent(subject, eventType, "1.0", data);
        await _client.SendEventAsync(eventGridEvent);
        Console.WriteLine("Event published successfully.");
    }
}
