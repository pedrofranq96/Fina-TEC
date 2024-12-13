using Fina.Api.Common.Api;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Orders;
using Fina.Core.Responses;

namespace Fina.Api.Endpoints.Orders;

public class GetVoucherByNumberEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{number}", HandleAsync).WithName("Get: Voucher by number").WithName("Obtem cupom voucher")
            .WithDescription("Obtém cupom voucher").WithOrder(1).Produces<Response<Voucher?>>();

    private static async Task<IResult> HandleAsync(IVoucherHandler handler, string number)
    {
        var request = new GetVoucherByNumberRequest{ Number = number };
        
        var result = await handler.GetByNumberAsync(request);

        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}