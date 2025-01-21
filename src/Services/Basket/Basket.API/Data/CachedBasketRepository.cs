
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Data
{
    public class CachedBasketRepository(IBasketRepository repository,
                                        IDistributedCache cache) 
                 : IBasketRepository
    {

        public async Task<ShoppingCart> GetBaksket(string username, CancellationToken cancellationToken = default)
        {

            var cachedBasket = await cache.GetStringAsync(username, cancellationToken);

            if (!string.IsNullOrEmpty(cachedBasket))
            {
                Console.WriteLine($"get basket for {username} from cache");
                return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;
            }


            var basket = await repository.GetBaksket(username, cancellationToken);

            await cache.SetStringAsync(username, 
                                       JsonSerializer.Serialize<ShoppingCart>(basket), 
                                       cancellationToken);

            return basket;
        }


        public async Task<ShoppingCart> StoreBasket(ShoppingCart shoppingCart, CancellationToken cancellationToken = default)
        {
            var basket = await repository.StoreBasket(shoppingCart, cancellationToken);

            await cache.SetStringAsync(shoppingCart.UserName,
                                       JsonSerializer.Serialize<ShoppingCart>(basket),
                                       cancellationToken);

            return basket;
        }


        public async Task<bool> DeleteBasket(string username, CancellationToken cancellationToken = default)
        {
            await repository.DeleteBasket(username, cancellationToken);

            await cache.RemoveAsync(username, cancellationToken);

            return true;
        }
    }
}
