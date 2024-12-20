using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using OrderService.Kafka;

namespace OrderService.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class OrderController : ControllerBase
	{
		private readonly IKafkaProducer _kafkaProducer;
		public OrderController(IKafkaProducer kafkaProducer) {
			_kafkaProducer = kafkaProducer;
		}


		[HttpPost]
		public async Task<ActionResult<string>> PostOrder(string orderName, string orderId)
		{
			try
			{
				await _kafkaProducer.ProduceAsync("order-topic", new Message<string, string>
				{
					Key = orderId,
					Value = orderName
				});
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

			return orderName;
		}
	}
}
