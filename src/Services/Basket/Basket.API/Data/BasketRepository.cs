namespace Basket.API.Data
{
    public class BasketRepository(IDocumentSession session) : IBasketRepository
    {



        public async Task<ShoppingCart> GetBaksket(string username, CancellationToken cancellationToken = default)
        {
            var basket = await session.LoadAsync<ShoppingCart>(username, cancellationToken);

            return basket is null ? throw new BasketNotFoundException(username) : basket;
        }



        public async Task<ShoppingCart> StoreBasket(ShoppingCart shoppingCart, CancellationToken cancellationToken = default)
        {
            session.Store(shoppingCart);
            await session.SaveChangesAsync();

            return shoppingCart;
        }



        public async Task<bool> DeleteBasket(string username, CancellationToken cancellationToken = default)
        {

            var basket = await session.LoadAsync<ShoppingCart>(username, cancellationToken) ?? 
                                                throw new BasketNotFoundException(username);
            
            session.Delete(basket);
            await session.SaveChangesAsync();

            return true;

        }


    }
}
