using Fina.Core.Models;
using Fina.Core.Requests.Orders;
using Fina.Core.Responses;

namespace Fina.Core.Handlers;

public interface IProductHandler
{
    Task<PagedResponse<List<Product>?>> GetAllAsync(GetAllProductsRequest request);
    Task<Response<Product?>> GetBySlugAsync(GetProductBySlugRequest request);
    
}