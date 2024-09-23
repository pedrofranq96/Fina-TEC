using Fina.Api.Common.Api;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Responses;

namespace Fina.Api.Common.Endpoints.Categories
{
    public class GetCategoryByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
         => app.MapGet("/{id}", HandleAsync)
                .WithName("Categories: GetCategoryById")
                .WithSummary("Buscar uma categoria por Id.")
                .WithDescription("Get category by Id.")
                .WithOrder(4)
                .Produces<Response<Category?>>();


        private static async Task<IResult> HandleAsync(ICategoryHandler handler, long id)
        {
            var request = new GetByIdCategoryRequest
            {
                UserId = "teste@balta.io",
                Id = id,
            };
            var result = await handler.GetByIdAsync(request);
            return result.IsSuccess 
                ? TypedResults.Ok(result) 
                : TypedResults.BadRequest(result);
        }
    }
}