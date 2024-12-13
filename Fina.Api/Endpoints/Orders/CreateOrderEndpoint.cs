using System.Security.Claims;
using Fina.Api.Common.Api;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Orders;
using Fina.Core.Responses;

namespace Fina.Api.Endpoints.Orders;

public class CreateOrderEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapPost("/", HandlerAsync).WithName("Orders: Criar pedido").WithSummary("Criar pedido").WithDescription("Cria um pedido")
        .WithOrder(1).Produces<Response<Order?>>();
            

    private static async Task<IResult> HandlerAsync(IOrderHandler handler, CreateOrderRequest request, ClaimsPrincipal user)
    {
        request.UserId = user.Identity!.Name ?? string.Empty;
        var result = await handler.CreateAsync(request);

        return result.IsSuccess
            ? TypedResults.Created($"v1/orders/{result.Data?.Number}", result)
            : TypedResults.BadRequest(result);
    }
}