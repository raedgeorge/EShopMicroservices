﻿namespace Basket.API.Basket.GetBasket
{

    public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;

    public record GetBasketResult(ShoppingCart ShoppingCart);

    internal class GetBasketQueryHandler (IBasketRepository repository) 
                   : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
        {

            var basket = await repository.GetBaksket(query.UserName, cancellationToken);

            return new GetBasketResult(basket);
        }
    }
}
