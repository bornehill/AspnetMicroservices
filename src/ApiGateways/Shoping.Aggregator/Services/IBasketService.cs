using Shoping.Aggregator.Models;
using System.Threading.Tasks;

namespace Shoping.Aggregator.Services
{
    public interface IBasketService
    {
        Task<BasketModel> GetBasket(string userName);
    }
}
