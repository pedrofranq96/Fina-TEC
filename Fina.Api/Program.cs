

using Fina.Api.Data;
using Fina.Core.Models;
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

builder.Services.AddTransient<Handler>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("/v1/categories", 
    (Request request, Handler handler) => handler.Handle(request)).WithName("Categories: Create")
                                                                  .WithSummary("Cria uma nova categoria")
                                                                  .Produces<Response>();

app.Run();








public class Request {
    public string Title { get; set; } = string.Empty;
    public string Description  { get; set; } = string.Empty;
}


public class Response{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
}


public class Handler(AppDbContext context) {
    public Response Handle(Request request)
    {
        var category = new Category
        {
            Title = request.Title,
            Description = request.Description
        };

        context.Categories.Add(category);
        context.SaveChanges();
        return new Response
        {
            Id = category.Id,
            Title = request.Title
        };
    }
}




