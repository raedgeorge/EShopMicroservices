namespace CatalogAPI.Products.UpdateProduct
{

    public record UpdateProductCommand(
                                        Guid Id,
                                        string Name,
                                        List<string> Category,
                                        string Description,
                                        string ImageFile,
                                        decimal Price
                                        ) : ICommand<UpdateProductResult>;

    public record UpdateProductResult(bool IsSuccess);



    internal class UpdateProductCommandHandler (IDocumentSession session) : 
            ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {

        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, 
                                                      CancellationToken cancellationToken)
        {

            var productFromDB = await session.LoadAsync<Product>(command.Id, cancellationToken);

            if (productFromDB is null)
            {
                throw new ProductNotFoundException("Product not found!");
            }

            productFromDB.Name = command.Name;
            productFromDB.Description = command.Description;
            productFromDB.ImageFile = command.ImageFile;
            productFromDB.Price = command.Price;
            productFromDB.Category = command.Category;

            session.Update(productFromDB);
            await session.SaveChangesAsync(cancellationToken);

            return new UpdateProductResult(true);
        }
    }
}
