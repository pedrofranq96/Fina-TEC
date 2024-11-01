using Fina.Api.Common.Api;
using Fina.Api.Common.Endpoints.Categories;
using Fina.Api.Endpoints.Identity;
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
            
            endpoints.MapGroup("v1/Categories")
                    .WithTags("Categories")
                    .RequireAuthorization()
                    .MapEndpoint<CreateCategoryEndpoint>()
                    .MapEndpoint<UpdateCategoryEndpoint>()
                    .MapEndpoint<DeleteCategoryEndpoint>()
                    .MapEndpoint<GetCategoryByIdEndpoint>()
                    .MapEndpoint<GetAllCategoriesEndpoint>();

            endpoints.MapGroup("v1/Transations")
                    .WithTags("Transations")
                    .RequireAuthorization()
                    .MapEndpoint<CreateTransactionEndpoint>()
                    .MapEndpoint<UpdateTransactionEndpoint>()
                    .MapEndpoint<DeleteTransactionEndpoint>()
                    .MapEndpoint<GetTransactionByIdEndpoint>()
                    .MapEndpoint<GetTransactionsByPeriodEndpoint>();
        }

        private static IEndpointRouteBuilder MapEndpoint<TEndponint>(this IEndpointRouteBuilder app) where TEndponint: IEndpoint
        {
            TEndponint.Map(app);
            return app;
        }
    }
}