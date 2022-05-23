using ClientA.BL.Interfaces;
using ClientA.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ClientA.Controllers
{
    [ApiController]
    [Route("controller")]
    public class CarController : ControllerBase
    {
        private readonly IRabbitMqService _rabbitMq;

        public CarController(IRabbitMqService rabbitMq)
        {
            _rabbitMq = rabbitMq;
        }

        [HttpPost("Publish Car to RabbitMQ")]
        public async Task<IActionResult> ProduceCar([FromBody] Car car)
        {
            await _rabbitMq.PublishCarAsync(car);

            return Ok();
        }
    }
}
