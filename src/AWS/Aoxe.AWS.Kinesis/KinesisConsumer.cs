using System;
using System.Text;
using System.Threading.Tasks;
using Amazon.Kinesis;
using Amazon.Kinesis.Model;

namespace Aoxe.AWS.Kinesis;

public class KinesisConsumer
{
    private readonly AmazonKinesisClient _kinesisClient;
    private readonly string _streamName;

    public KinesisConsumer(string streamName, string region)
    {
        _kinesisClient = new AmazonKinesisClient(Amazon.RegionEndpoint.GetBySystemName(region));
        _streamName = streamName;
    }

    public async Task ConsumeMessagesAsync()
    {
        var shardIteratorRequest = new GetShardIteratorRequest
        {
            StreamName = _streamName,
            ShardId = "shardId-000000000000",
            ShardIteratorType = ShardIteratorType.TRIM_HORIZON
        };

        var shardIteratorResponse = await _kinesisClient.GetShardIteratorAsync(
            shardIteratorRequest
        );
        var shardIterator = shardIteratorResponse.ShardIterator;

        while (true)
        {
            var getRecordsRequest = new GetRecordsRequest
            {
                ShardIterator = shardIterator,
                Limit = 100
            };

            var getRecordsResponse = await _kinesisClient.GetRecordsAsync(getRecordsRequest);
            foreach (var record in getRecordsResponse.Records)
            {
                var message = Encoding.UTF8.GetString(record.Data.ToArray());
                Console.WriteLine($"Received message: {message}");
            }

            shardIterator = getRecordsResponse.NextShardIterator;
            await Task.Delay(1000); // Wait for a second before fetching new records
        }
    }
}
