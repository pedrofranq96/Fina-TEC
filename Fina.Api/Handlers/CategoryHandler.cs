using Fina.Api.Data;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Fina.Api.Handlers
{
    public class CategoryHandler(AppDbContext context) : ICategoryHandler
    {
        public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest request)
        {
            try
            {
                    var category = new Category
                {
                    UserId = request.UserId,
                    Title = request.Title,
                    Description = request.Description
                };
                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();

                return new Response<Category?>(category, 201, "Categoria criada com sucesso.");
            }
            catch
            {
                return new Response<Category?>(null, 500,"[FP078] Falha ao criar categoria.");
            }

        }

        public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
        {
           try
           {
                var category = await context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
                if(category == null)
                        return new Response<Category?>(null, 404,"Categoria não encontrada.");
                    
                category.Title = request.Title;
                category.Description = request.Description;

                context.Categories.Update(category);
                await context.SaveChangesAsync();

                return new Response<Category?>(category, 201, "Categoria atualizada com sucesso.");
            }
           catch
           {
                return new Response<Category?>(null, 500,"[FP079]Falha ao atualizar categoria.");           
           }

        }

        public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
        {
            try
            {
                var category = await context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
                if(category == null)
                        return new Response<Category?>(null, 404,"Categoria não encontrada.");

                context.Categories.Remove(category);
                await context.SaveChangesAsync();

                return new Response<Category?>(category,201, "Categoria removida com sucesso.");
            }catch
            {
                return new Response<Category?>(null, 500,"[FP080] Falha ao excluir categoria.");
            }

        }

        public async Task<PagedResponse<List<Category>>> GetAllAsync(GetAllCategoriesRequest request)
        {
           try
           {
                var query = context.Categories.AsNoTracking().Where(x => x.UserId == request.UserId).OrderBy(x => x.Title);

                var categories = await query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<Category>>(categories,count, request.PageNumber, request.PageSize);
           }
           catch
           {
                return new PagedResponse<List<Category>>(null, 500, "[FP082] Não foi possível consultar as categorias.");
            
           }
        }

        public async Task<Response<Category?>> GetByIdAsync(GetByIdCategoryRequest request)
        {
            try
            {
                var category = await context
                    .Categories
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if(category == null)
                {
                    return new Response<Category?>(null, 404,"Categoria não encontrada.");
                }
                else
                {
                    return new Response<Category?>(category);
                }
            }
            catch
            {
                return new Response<Category?>(null, 500, "[FP081] Não foi possível recuperar a categoria");
            }
        }


    }
}