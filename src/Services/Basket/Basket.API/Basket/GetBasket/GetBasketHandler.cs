namespace Basket.API.Basket.GetBasket
{

    public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;

    public record GetBasketResult(ShoppingCart ShoppingCart);

    internal class GetBasketQueryHandler (IDocumentSession session) 
                   : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
        {

            return new GetBasketResult(new ShoppingCart("raed abu sada"));
        }
    }
}
