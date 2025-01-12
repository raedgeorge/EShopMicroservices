using MediatR;

namespace CatalogAPI.Products.CreateProduct
{

    // data needed to create a Product
    public record CreateProductCommand(
                                        string Name, 
                                        List<string> Category,
                                        string Descriptino,
                                        string ImageFile,
                                        decimal Price
                                       ) : IRequest<CreateProductResult>;


    // create Product resut -> return created Product Id
    public record CreateProductResult(Guid Id);


    // create product command handler
    internal class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
    {

        public Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            // Buisness logic to create a Product
            throw new NotImplementedException();
        }
    }
}
