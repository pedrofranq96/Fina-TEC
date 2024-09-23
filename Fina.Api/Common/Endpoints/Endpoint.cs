using Fina.Api.Common.Api;
using Fina.Api.Common.Endpoints.Categories;

namespace Fina.Api.Common.Endpoints
{
    public static class Endpoint
    {
        public static void MapEndpoints(this WebApplication app)
        {
            var endpoints = app.MapGroup("");

            endpoints.MapGroup("v1/Categories")
                    .WithTags("Categories")
                    //.RequireAuthorization()
                    .MapEndpoint<CreateCategoryEndpoint>()
                    .MapEndpoint<UpdateCategoryEndpoint>()
                    .MapEndpoint<DeleteCategoryEndpoint>()
                    .MapEndpoint<GetCategoryByIdEndpoint>()
                    .MapEndpoint<GetAllCategoriesEndpoint>();
        }

        private static IEndpointRouteBuilder MapEndpoint<TEndponint>(this IEndpointRouteBuilder app) where TEndponint: IEndpoint
        {
            TEndponint.Map(app);
            return app;
        }
    }
}