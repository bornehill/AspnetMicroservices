using Microsoft.AspNetCore.Mvc;
using Shoping.Aggregator.Models;
using Shoping.Aggregator.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Shoping.Aggregator.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ShoppingController : ControllerBase
    {
        [HttpGet("{userName}", Name = "GetShopping")]
        [ProducesResponseType(typeof(ShoppingModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingModel>> GetShopping([FromRoute] string userName, [FromServices] IBasketService basketService
            , [FromServices] ICatalogService catalogService, [FromServices] IOrderService orderService)
        {
            var basket = await basketService.GetBasket(userName);

            foreach (var item in basket.Items)
            {
                var product = await catalogService.GetCatalog(item.ProductId);

                // set additional product fields onto basket item
                item.ProductName = product.Name;
                item.Category = product.Category;
                item.Summary = product.Summary;
                item.Description = product.Description;
                item.ImageFile = product.ImageFile;
            }

            var orders = await orderService.GetOrdersByUserName(userName);

            var shoppingModel = new ShoppingModel
            {
                UserName = userName,
                BasketWithProducts = basket,
                Orders = orders
            };

            return Ok(shoppingModel);
        }
    }
}
