using Ordering.Application.Orders.Commands.DeleteOrder;

namespace Ordering.API.Endpoints
{

    // Accept the order Id as a parameter.
    // Constructs a DeleteOrderCommand.
    // Uses MediatR to send the command to the corresponding handler.
    // Returns a success or not found response.

    // Not Used.
    // public record DeleteOrderRequest(string OrderId);

    public record DeleteOrderResponse(bool IsSuccess);

    public class DeleteOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/orders/{orderId}", async (Guid orderId, ISender sender) =>
            {
                var command = new DeleteOrderCommand(orderId);

                var result = await sender.Send(command);

                var response = result.Adapt<DeleteOrderResponse>();

                return Results.Ok(response);
            })
            .WithName("DeleteOrder")
            .Produces<DeleteOrderResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Order")
            .WithDescription("Delete Order");
        }
    }
}
