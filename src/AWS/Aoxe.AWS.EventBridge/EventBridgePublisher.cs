using Amazon.EventBridge;
using Amazon.EventBridge.Model;

namespace Aoxe.Aws.EventBridge;

public class EventBridgePublisher
{
    private readonly AmazonEventBridgeClient _eventBridgeClient;
    private readonly string _eventBusName;

    public EventBridgePublisher(string eventBusName, string region)
    {
        _eventBridgeClient = new AmazonEventBridgeClient(
            Amazon.RegionEndpoint.GetBySystemName(region)
        );
        _eventBusName = eventBusName;
    }

    public async Task PublishEventAsync(string detailType, string source, string detail)
    {
        var putEventsRequest = new PutEventsRequest
        {
            Entries = new List<PutEventsRequestEntry>
            {
                new PutEventsRequestEntry
                {
                    EventBusName = _eventBusName,
                    DetailType = detailType,
                    Source = source,
                    Detail = detail
                }
            }
        };

        var response = await _eventBridgeClient.PutEventsAsync(putEventsRequest);
        Console.WriteLine($"Event published with status: {response.Entries[0].EventId}");
    }
}
