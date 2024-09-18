using Fina.Api.Data;
using Fina.Api.Handlers;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Responses;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x => {
    x.CustomSchemaIds(n => n.FullName);
});

var cnnStr = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
builder.Services.AddDbContext<AppDbContext>(
    x => 
    {
        x.UseSqlServer(cnnStr);
    }
);

builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();





app.MapPost("/v1/categories", 
    async (CreateCategoryRequest request, ICategoryHandler handler) => 
    await handler.CreateAsync(request))
           .WithName("Categories: Create Category")
           .WithSummary("Criar uma nova categoria.")
           .Produces<Response<Category?>>();


app.MapPut("/v1/categories/{id}", 
    async(long id,UpdateCategoryRequest request, ICategoryHandler handler) => 
        {
            request.Id = id;
            return await handler.UpdateAsync(request);
        }).WithName("Categories: Update Category")
          .WithSummary("Atualizar uma categoria.")
          .Produces<Response<Category?>>();

app.MapDelete("/v1/categories/{id}", 
    async(long id,ICategoryHandler handler) => 
        {
            var request = new DeleteCategoryRequest
            {
                Id = id,
            };
            return await handler.DeleteAsync(request);
        }).WithName("Categories: Delete Category")
          .WithSummary("Remover uma categoria.")
          .Produces<Response<Category?>>();



app.MapGet("/v1/categories/{id}", 
    async (long id, ICategoryHandler handler) => 
        {
            var request = new GetByIdCategoryRequest
            {
                Id = id,
                UserId = "teste@balta.io"

            };
            return await handler.GetByIdAsync(request);
        }).WithName("Categories: Get By Id Category")
          .WithSummary("Obter categoria por Id.")
          .Produces<Response<Category?>>();


app.MapGet("/v1/categories", 
    async (ICategoryHandler handler) => 
        {
            var request = new GetAllCategoriesRequest
            {                
                UserId = "teste@balta.io"
            };
            return await handler.GetAllAsync(request);
        }).WithName("Categories: Get all Categories")
          .WithSummary("Obter todas categorias por usuário.")
          .Produces<PagedResponse<List<Category>?>>();

app.Run();













