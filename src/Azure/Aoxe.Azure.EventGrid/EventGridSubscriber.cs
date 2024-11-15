using System.Text.Json;
using Azure.Messaging.EventGrid;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Aoxe.Azure.EventGrid;

public static class EventGridSubscriber
{
    [Function("EventGridSubscriber")]
    public static void Run(
        [EventGridTrigger] EventGridEvent eventGridEvent,
        FunctionContext context
    )
    {
        var logger = context.GetLogger("EventGridSubscriber");
        logger.LogInformation($"Received event: {JsonSerializer.Serialize(eventGridEvent)}");
    }
}
