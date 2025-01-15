
namespace CatalogAPI.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;

    public record DeleteProductResult(bool IsSuccess);


    internal class DeleteProductCommandHandler (IDocumentSession session)
        : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, 
                                               CancellationToken cancellationToken)
        {
            var productFromDB = await session.LoadAsync<Product>(command.Id, cancellationToken);

            if (productFromDB is null) throw new ProductNotFoundException("Product not found!");

            session.Delete(productFromDB);
            await session.SaveChangesAsync(cancellationToken);

            return new DeleteProductResult(true);
        }
    }
}
