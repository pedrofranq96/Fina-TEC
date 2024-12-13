using Fina.Api.Common.Api;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Orders;
using Fina.Core.Responses;

namespace Fina.Api.Endpoints.Orders;

public class GetProductBySlugEndpoint: IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{slug}", HandleAsync).WithName("Products: Get by slug").WithSummary("Recupera um produto")
            .WithDescription("Recupera um produto").WithOrder(2).Produces<Response<Product?>>();

    private static async Task<IResult> HandleAsync(IProductHandler handler, string slug)
    {
        var request = new GetProductBySlugRequest
        {
            Slug = slug
        };
        var result = await handler.GetBySlugAsync(request);

        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}