
namespace CatalogAPI.Products.GetProductById
{

    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

    public record GetProductByIdResult(Product Product);


    internal class GetProductByIdQueryHandler(IDocumentSession session, 
                                              ILogger<GetProductByIdQueryHandler> logger) 
                : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {

        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("========================================================");
            logger.LogInformation("GetProductByIdHandler.Handle called with {@query}", query);
            logger.LogInformation("========================================================");


            var product = await session.LoadAsync<Product>(query.Id, cancellationToken);

            return product is null ? throw new ProductNotFoundException("Product not found!") 
                                    : 
                                    new GetProductByIdResult(product);
        }
    }
}
