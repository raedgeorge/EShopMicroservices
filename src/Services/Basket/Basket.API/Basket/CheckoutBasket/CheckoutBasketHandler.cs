﻿using BuildingBlock.Messaging.Events;
using MassTransit;

namespace Basket.API.Basket.CheckoutBasket
{

    public record CheckoutBasketCommand(BasketCheckoutDto BasketCheckoutDto) : ICommand<CheckoutBasketResult>;

    public record CheckoutBasketResult(bool IsSuccess);

    public class CheckoutBasketCommandValidator : AbstractValidator<CheckoutBasketCommand>
    {
        public CheckoutBasketCommandValidator()
        {
            RuleFor(x => x.BasketCheckoutDto).NotNull().WithMessage("BasketCheckoutDto is required");
            RuleFor(x => x.BasketCheckoutDto.UserName).NotEmpty().WithMessage("Username is required");
        }
    }


    public class CheckoutBasketCommandHandler (IBasketRepository basketRepository, 
                                               IPublishEndpoint publishEndpoint) 
           : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
    {

        public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
        {

            var basketFromDB = await basketRepository.GetBaksket(command.BasketCheckoutDto.UserName, cancellationToken);

            if (basketFromDB is null) 
                return new CheckoutBasketResult(false);

            var eventMessage = command.BasketCheckoutDto.Adapt<BasketCheckoutEvent>();
            eventMessage.TotalPrice = basketFromDB.TotalPrice;

            await publishEndpoint.Publish(eventMessage, cancellationToken);

            await basketRepository.DeleteBasket(command.BasketCheckoutDto.UserName, cancellationToken);

            return new CheckoutBasketResult(true);
        }
    }
}
