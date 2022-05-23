using ClientA.Models;
using System.Threading.Tasks;

namespace ClientA.BL.Interfaces
{
    public interface IRabbitMqService
    {
        Task PublishCarAsync(Car car);
    }
}
