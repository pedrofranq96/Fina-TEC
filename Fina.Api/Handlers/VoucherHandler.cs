using Fina.Api.Data;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Orders;
using Fina.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Fina.Api.Handlers;

public class VoucherHandler(AppDbContext context): IVoucherHandler
{
    public async Task<Response<Voucher?>> GetByNumberAsync(GetVoucherByNumberRequest request)
    {
        try
        {
            var voucher = await context.Vouchers.AsNoTracking().FirstOrDefaultAsync(x=> x.Number == request.Number && x.IsActive == true);

            return voucher is null
                ? new Response<Voucher?>(null, 404, "Voucher não encontrado")
                : new Response<Voucher?>(voucher);
        }
        catch
        {
            return new Response<Voucher?>(null, 500, "Não foi possível recuperar o Voucher.");
        }
    }
}