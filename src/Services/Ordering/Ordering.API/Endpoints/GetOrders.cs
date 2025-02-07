﻿using BuildingBlocks.Pagination;
using Ordering.Application.Orders.Queries.GetOrders;

namespace Ordering.API.Endpoints
{

    // Accept pagination parameters.
    // Constructs a GetOrdersQuery with the pagination params.
    // Retrieves the data and returns it in a paginated format.

    // public record GetOrdersRequest(PaginationRequest PaginationRequest);

    public record GetOrdersResponse(PaginatedResult<OrderDto> Orders);

    public class GetOrders : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders", async ([AsParameters] PaginationRequest request, ISender sender) =>
            {
                var result = await sender.Send(new GetOrdersQuery(request));

                var response = result.Adapt<GetOrdersResponse>();

                return Results.Ok(response);
            })
            .WithName("GetOrders")
            .Produces<GetOrdersByCustomerResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Orders")
            .WithDescription("Get Orders");
        }
    }
}
