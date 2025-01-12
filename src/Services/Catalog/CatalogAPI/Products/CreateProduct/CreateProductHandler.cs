using BuildingBlocks.CQRS;
using CatalogAPI.Models;

namespace CatalogAPI.Products.CreateProduct
{

    // data needed to create a Product
    public record CreateProductCommand(
                                        string Name, 
                                        List<string> Category,
                                        string Description,
                                        string ImageFile,
                                        decimal Price
                                       ) : ICommand<CreateProductResult>;


    // create Product resut -> return created Product Id
    public record CreateProductResult(Guid Id);


    // create product command handler
    internal class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
    {

        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            // create Product entity from command object
            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
            };

            // save to database
            // TODO

            // return CreateProductResult result
            return new CreateProductResult(Guid.NewGuid());
        }
    }
}
