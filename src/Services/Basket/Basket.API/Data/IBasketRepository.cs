﻿namespace Basket.API.Data
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetBaksket(string username, CancellationToken cancellationToken = default);

        Task<ShoppingCart> StoreBasket(ShoppingCart shoppingCart, CancellationToken cancellationToken = default);

        Task<bool> DeleteBasket(string username, CancellationToken cancellationToken = default);
    }
}
