using System.Security.Claims;
using Fina.Api.Common.Api;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Orders;
using Fina.Core.Responses;

namespace Fina.Api.Endpoints.Orders;

public class PayOrderEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/{id}/pay", HandleAsync).WithName("Orders: Pay an order").WithSummary("Pagar um pedido")
            .WithDescription("Pagar um pedido").WithOrder(3).Produces<Response<Order?>>();

    private static async Task<IResult> HandleAsync(IOrderHandler handler, long id, PayOrderRequest request,
        ClaimsPrincipal user)
    {
        request.Id = id;
        request.UserId = user.Identity!.Name ?? string.Empty;

        var result = await handler.PayAsync(request);
        
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}