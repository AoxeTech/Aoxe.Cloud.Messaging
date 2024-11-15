using Amazon.Lambda.Core;
using Amazon.Lambda.EventBridgeEvents;

namespace Aoxe.Aws.EventBridge;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

public class EventBridgeSubscriber
{
    public void FunctionHandler(EventBridgeEvent<dynamic> eventBridgeEvent, ILambdaContext context)
    {
        Console.WriteLine($"Received event: {eventBridgeEvent.Detail}");
    }
}