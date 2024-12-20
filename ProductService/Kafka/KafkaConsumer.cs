using Confluent.Kafka;

namespace ProductService.Kafka
{
	public class KafkaConsumer : BackgroundService
	{
		protected override Task ExecuteAsync(CancellationToken stoppingToken)
		{

			return Task.Run(() =>
			{
				_ = ConsumeAsync("order-topic", stoppingToken);
			}, stoppingToken);
		}

		public async Task ConsumeAsync(string topic, CancellationToken stoppingToken)
		{
			try
			{
				var config = new ConsumerConfig
				{
					GroupId = "order-group",
					BootstrapServers = "localhost:9092",
					AutoOffsetReset = AutoOffsetReset.Earliest
				};
				using var consumer = new ConsumerBuilder<string, string>(config).Build();

				consumer.Subscribe(topic);

				while (!stoppingToken.IsCancellationRequested)
				{
					var consumeResult = consumer.Consume(stoppingToken);

					var order = consumeResult;

				}
				consumer.Close();
			}
			catch (Exception ex )
			{
				Console.WriteLine(ex.Message);
			}
			
		}
	}
}
