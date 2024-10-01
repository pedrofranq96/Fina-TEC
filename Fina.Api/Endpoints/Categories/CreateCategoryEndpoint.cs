
using Fina.Api.Common.Api;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Responses;

namespace Fina.Api.Common.Endpoints.Categories
{
    public class CreateCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
            .WithName("Categories: Create")
            .WithSummary("Criar uma nova categoria.")
            .WithDescription("Create a category")
            .WithOrder(1)
            .Produces<Response<Category?>>();
        
        private static async Task<IResult> HandleAsync(ICategoryHandler handler, CreateCategoryRequest request)
        {
            request.UserId = "teste@balta.io";
            var result = await handler.CreateAsync(request);
            
            return result.IsSuccess 
                ? TypedResults.Created($"/{result.Data?.Id}", result) 
                : TypedResults.BadRequest(result);
        }
    }
}