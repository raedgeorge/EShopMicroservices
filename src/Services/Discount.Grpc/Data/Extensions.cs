﻿using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data
{
    public static class Extensions
    {
        public static IApplicationBuilder UseMigration(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            using var dbContext = scope.ServiceProvider.GetRequiredService<DiscountContext>();

            if (dbContext.Database.GetPendingMigrations().Any())
            {
                dbContext.Database.MigrateAsync();
            }

            return app;
        }
    }
}
