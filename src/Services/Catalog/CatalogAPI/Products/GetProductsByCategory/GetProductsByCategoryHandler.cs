
namespace CatalogAPI.Products.GetProductsByCategory
{

    public record GetProductsByCategoryQuery(string Category) : IQuery<GetPrdouctsByCategoryResult>;

    public record GetPrdouctsByCategoryResult(IEnumerable<Product> Products);


    internal class GetProductsByCategoryQueryHandler(IDocumentSession session) :
            IQueryHandler<GetProductsByCategoryQuery, GetPrdouctsByCategoryResult>
    {

        public async Task<GetPrdouctsByCategoryResult> Handle(GetProductsByCategoryQuery query, 
                                                          CancellationToken cancellationToken)
        {

            var products = await session.Query<Product>()
                                        .Where(p => p.Category.Contains(query.Category))
                                        .ToListAsync();

            return new GetPrdouctsByCategoryResult(products);
        }
    }
}
