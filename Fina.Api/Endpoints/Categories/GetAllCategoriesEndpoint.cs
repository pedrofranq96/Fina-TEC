using System.Security.Claims;
using Fina.Api.Common.Api;
using Fina.Core;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Fina.Api.Common.Endpoints.Categories
{
    public class GetAllCategoriesEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
                .WithName("Categories: GetAllCategories")
                .WithSummary("Buscar diversas categoria.")
                .WithDescription("Get all categories.")
                .WithOrder(5)
                .Produces<PagedResponse<List<Category>?>>();


        private static async Task<IResult> HandleAsync(ClaimsPrincipal user, ICategoryHandler handler, [FromQuery]int pagedNumber = Configuration.DefaultPageNumber, [FromQuery]int pageSize = Configuration.DefaultPageSize)
        {
            var request = new GetAllCategoriesRequest
            {
                UserId = user.Identity?.Name ?? string.Empty,
                PageNumber = pagedNumber,
                PageSize = pageSize
            };
            var result = await handler.GetAllAsync(request);
            return result.IsSuccess 
                ? TypedResults.Ok(result) 
                : TypedResults.BadRequest(result);
        }
    }
}