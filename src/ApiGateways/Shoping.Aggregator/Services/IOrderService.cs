using Shoping.Aggregator.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shoping.Aggregator.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName(string userName);
    }
}
