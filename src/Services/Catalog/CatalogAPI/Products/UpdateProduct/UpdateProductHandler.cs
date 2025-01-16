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


    // validation class for UpdateProductCommand
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is required");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .Length(2, 64).WithMessage("Name must be between 2 and 64 characters");

            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price should be greater than 0");
        }
    }


    internal class UpdateProductCommandHandler (IDocumentSession session) : 
            ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {

        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, 
                                                      CancellationToken cancellationToken)
        {

            var productFromDB = await session.LoadAsync<Product>(command.Id, cancellationToken);

            if (productFromDB is null)
            {
                throw new ProductNotFoundException(command.Id);
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
