using Amazon.SQS;
using Amazon.SQS.Model;

namespace Zaabee.AWS.SQS;

public class Test
{
    public async ValueTask Demo()
    {
        var sqsClient = new AmazonSQSClient();

        Console.WriteLine($"Hello Amazon SQS! Following are some of your queues:");
        Console.WriteLine();

        // You can use await and any of the async methods to get a response.
        // Let's get the first five queues.
        var response = await sqsClient.ListQueuesAsync(new ListQueuesRequest { MaxResults = 5 });

        foreach (var queue in response.QueueUrls)
        {
            Console.WriteLine($"\tQueue Url: {queue}");
            Console.WriteLine();
        }
    }
}
