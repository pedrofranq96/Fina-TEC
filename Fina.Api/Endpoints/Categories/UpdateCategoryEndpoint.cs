using System.Security.Claims;
using Fina.Api.Common.Api;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Responses;

namespace Fina.Api.Common.Endpoints.Categories
{
    public class UpdateCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandleAsync)
                .WithName("Categories: Update")
                .WithSummary("Atualizar uma categoria.")
                .WithDescription("Update category")
                .WithOrder(2)
                .Produces<Response<Category?>>();

        private static async Task<IResult> HandleAsync(ClaimsPrincipal user, ICategoryHandler handler, UpdateCategoryRequest request, long id)
        {
            request.UserId = user.Identity?.Name ?? string.Empty;
            request.Id = id;
            var result = await handler.UpdateAsync(request);
            return result.IsSuccess 
                ? TypedResults.Ok(result) 
                : TypedResults.BadRequest(result);
        }
    }
}