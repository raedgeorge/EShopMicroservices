using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
    public class DiscountService (DiscountContext dbContext, ILogger<DiscountService> logger)
        : DiscountProtoService.DiscountProtoServiceBase
    {

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext
                                .Coupons
                                .FirstOrDefaultAsync(c => c.ProductName.Equals(request.ProductName));

            if (coupon is null)
                coupon =  new Coupon() { ProductName = request.ProductName, Amount = 0, Description = "No Discount" };

            logger.LogInformation("Discount is retrieved for ProductName : {productName}, Amount: {amount}",
                                  coupon.ProductName, coupon.Amount);


            return coupon.Adapt<CouponModel>();
        }


        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {

            var coupon = request.Coupon.Adapt<Coupon>();

            if (coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object."));

            await dbContext.AddAsync(coupon);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Discount is created successfully. ProductName : {productName}", coupon.ProductName);

            return coupon.Adapt<CouponModel>();
        }


        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {

            if (request.Coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request"));

            var coupon = await dbContext.Coupons.FirstOrDefaultAsync(c => c.Id == request.Coupon.Id) ??
                   throw new RpcException(new Status(StatusCode.InvalidArgument, "Coupon not found"));

            coupon.Amount = request.Coupon.Amount;
            coupon.ProductName = request.Coupon.ProductName;
            coupon.Description = request.Coupon.Description;

            dbContext.Update(coupon);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Discount is updated successfully. ProductName : {productName}", coupon.ProductName);

            return coupon.Adapt<CouponModel>();
        }


        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
           var coupon = await dbContext.Coupons.FirstOrDefaultAsync(c => c.ProductName.Equals(request.ProductName)) ??
                                   throw new RpcException(new Status(StatusCode.NotFound, "Invalid argument"));


            dbContext.Coupons.Remove(coupon);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Discount is deleted successfully. ProductName : {productName}", coupon.ProductName);

            return new DeleteDiscountResponse { Success  = true };
        }

    }
}
