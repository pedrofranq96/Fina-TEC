using System.Security.Claims;
using Fina.Api.Common.Api;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Orders;
using Fina.Core.Responses;

namespace Fina.Api.Endpoints.Orders;

public class RefundOrderEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/{id}/refund", HandleAsync).WithName("Orders: Refund an order").WithSummary("Estorna um pedido")
            .WithDescription("Estorna um pedido").WithOrder(6).Produces<Response<Order?>>();

    private static async Task<IResult> HandleAsync(ClaimsPrincipal user, long id, IOrderHandler handler)
    {
        var request = new RefundOrderRequest
        {
            Id = id,
            UserId = user.Identity!.Name ?? string.Empty,
        };
        
        var result = await handler.RefundAsync(request);
        
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}