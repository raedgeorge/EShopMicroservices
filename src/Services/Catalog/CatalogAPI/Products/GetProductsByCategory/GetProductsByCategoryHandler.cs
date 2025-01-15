
namespace CatalogAPI.Products.GetProductsByCategory
{

    public record GetProductsByCategoryQuery(string Category) : IQuery<GetPrdouctsByCategoryResult>;

    public record GetPrdouctsByCategoryResult(IEnumerable<Product> Products);


    internal class GetProductsByCategoryQueryHandler(IDocumentSession session, 
                                                     ILogger<GetProductsByCategoryQueryHandler> logger) :
            IQueryHandler<GetProductsByCategoryQuery, GetPrdouctsByCategoryResult>
    {

        public async Task<GetPrdouctsByCategoryResult> Handle(GetProductsByCategoryQuery query, 
                                                          CancellationToken cancellationToken)
        {

            logger.LogInformation("====================================================================");
            logger.LogInformation("GetProductsByCategoryQueryHandler.Handle called with {@Query}", query);
            logger.LogInformation("====================================================================");

            var products = await session.Query<Product>()
                                        .Where(p => p.Category.Contains(query.Category))
                                        .ToListAsync();

            return new GetPrdouctsByCategoryResult(products);
        }
    }
}
