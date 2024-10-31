using System;
using System.Text;
using System.Threading.Tasks;
using Amazon.Kinesis;
using Amazon.Kinesis.Model;

namespace Aoxe.AWS.Kinesis;

public class KinesisProducer
{
    private readonly AmazonKinesisClient _kinesisClient;
    private readonly string _streamName;

    public KinesisProducer(string streamName, string region)
    {
        _kinesisClient = new AmazonKinesisClient(Amazon.RegionEndpoint.GetBySystemName(region));
        _streamName = streamName;
    }

    public async Task PublishMessageAsync(string message)
    {
        var putRecordRequest = new PutRecordRequest
        {
            StreamName = _streamName,
            Data = new MemoryStream(Encoding.UTF8.GetBytes(message)),
            PartitionKey = Guid.NewGuid().ToString()
        };

        var response = await _kinesisClient.PutRecordAsync(putRecordRequest);
        Console.WriteLine($"Message published with sequence number: {response.SequenceNumber}");
    }
}
