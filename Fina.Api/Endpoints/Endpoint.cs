using Fina.Api.Common.Api;
using Fina.Api.Common.Endpoints.Categories;
using Fina.Api.Endpoints.Identity;
using Fina.Api.Endpoints.Orders;
using Fina.Api.Endpoints.Reports;
using Fina.Api.Endpoints.Transactions;
using Fina.Api.Models;

namespace Fina.Api.Endpoints
{
    public static class Endpoint
    {
        public static void MapEndpoints(this WebApplication app)
        {
            var endpoints = app.MapGroup("");
            
            endpoints.MapGroup("/").WithTags("Health Check").MapGet("/", () => new {message = "OK"});
            
            endpoints.MapGroup("v1/identity")
                .WithTags("Identity")
                .MapIdentityApi<User>();
                
            endpoints.MapGroup("v1/identity")
                .WithTags("Identity")
                .MapEndpoint<LogoutEndpoint>()
                .MapEndpoint<GetRolesEndpoint>();
            
            endpoints.MapGroup("v1/categories")
                    .WithTags("Categories")
                    .RequireAuthorization()
                    .MapEndpoint<CreateCategoryEndpoint>()
                    .MapEndpoint<UpdateCategoryEndpoint>()
                    .MapEndpoint<DeleteCategoryEndpoint>()
                    .MapEndpoint<GetCategoryByIdEndpoint>()
                    .MapEndpoint<GetAllCategoriesEndpoint>();

            endpoints.MapGroup("v1/transactions")
                    .WithTags("Transactions")
                    .RequireAuthorization()
                    .MapEndpoint<CreateTransactionEndpoint>()
                    .MapEndpoint<UpdateTransactionEndpoint>()
                    .MapEndpoint<DeleteTransactionEndpoint>()
                    .MapEndpoint<GetTransactionByIdEndpoint>()
                    .MapEndpoint<GetTransactionsByPeriodEndpoint>();

            endpoints.MapGroup("v1/products")
                    .WithTags("Products")
                    .RequireAuthorization()
                    .MapEndpoint<GetAllProductsEndpoint>()
                    .MapEndpoint<GetProductBySlugEndpoint>();
            
            endpoints.MapGroup("v1/vouchers")
                    .WithTags("Vouchers")
                    .RequireAuthorization()
                    .MapEndpoint<GetVoucherByNumberEndpoint>();

            endpoints.MapGroup("v1/orders")
                    .WithTags("Orders")
                    .RequireAuthorization()
                    .MapEndpoint<GetAllOrdersEndpoint>()
                    .MapEndpoint<GetOrderByNumberEndpoint>()
                    .MapEndpoint<CreateOrderEndpoint>()
                    .MapEndpoint<CancelOrderEndpoint>()
                    .MapEndpoint<PayOrderEndpoint>()
                    .MapEndpoint<RefundOrderEndpoint>();

            endpoints.MapGroup("v1/reports")
                .WithTags("Reports")
                .RequireAuthorization()
                .MapEndpoint<GetIncomesAndExpensesEndpoint>()
                .MapEndpoint<GetIncomesByCategoryEndpoint>()
                .MapEndpoint<GetExpensesByCategoryEndpoint>()
                .MapEndpoint<GetFinancialSummaryEndpoint>();
        }

        private static IEndpointRouteBuilder MapEndpoint<TEndponint>(this IEndpointRouteBuilder app) where TEndponint: IEndpoint
        {
            TEndponint.Map(app);
            return app;
        }
    }
}