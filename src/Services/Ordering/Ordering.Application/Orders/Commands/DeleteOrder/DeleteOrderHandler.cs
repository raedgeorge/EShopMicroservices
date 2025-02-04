namespace Ordering.Application.Orders.Commands.DeleteOrder
{
    public class DeleteOrderHandler (IApplicationDbContext context) : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
    {
        public async Task<DeleteOrderResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
        {

            var orderId = OrderId.Of(command.OrderId);

            var orderFromDB = await context.Orders.FindAsync([orderId], cancellationToken: cancellationToken) ??
                                    throw new OrderNotFoundException(command.OrderId);


            context.Orders.Remove(orderFromDB);
            await context.SaveChangesAsync(cancellationToken);

            return new DeleteOrderResult(true);
        }
    }
}
