using System.Security.Claims;
using Fina.Api.Common.Api;
using Fina.Core.Handlers;
using Fina.Core.Models.Reports;
using Fina.Core.Requests.Reports;
using Fina.Core.Responses;

namespace Fina.Api.Endpoints.Reports;

public class GetIncomesAndExpensesEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/incomes-expenses", HandleAsync).Produces<Response<List<IncomesAndExpenses>?>>();

    private static async Task<IResult> HandleAsync(ClaimsPrincipal user, IReportHandler handler)
    {
        var request = new GetIncomesAndExpensesRequest { UserId = user.Identity?.Name ?? string.Empty };
        var result = await handler.GetIncomesAndExpensesReportAsync(request);
        return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
    }
}