using System.Security.Claims;
using Fina.Api.Common.Endpoints;
using Fina.Api.Data;
using Fina.Api.Handlers;
using Fina.Api.Models;
using Fina.Core.Handlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme).AddIdentityCookies();
builder.Services.AddAuthorization();

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

builder.Services.AddIdentityCore<User>()
                .AddRoles<IdentityRole<long>>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddApiEndpoints();

builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();
builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();

var app = builder.Build();


app.UseAuthentication();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI();




app.MapGet("/", () => new {message = "OK"});

app.MapEndpoints();
app.MapGroup("v1/identity")
    .WithTags("Identity")
    .MapIdentityApi<User>();

app.MapGroup("v1/identity")
    .WithTags("Identity")
    .MapPost("/logout", async (SignInManager<User> signInManager) => 
    {
        await signInManager.SignOutAsync();
        return Results.Ok();
    }).RequireAuthorization();

app.MapGroup("v1/identity")
    .WithTags("Identity")
    .MapGet("/roles", (ClaimsPrincipal user) => 
    {
        if(user.Identity is null || user.Identity.IsAuthenticated == false)
            return Results.Unauthorized();

        var identity = (ClaimsIdentity)user.Identity;
        var roles = identity.FindAll(identity.RoleClaimType)
            .Select(c => new 
            {
                c.Issuer,
                c.OriginalIssuer,
                c.Type,
                c.Value,
                c.ValueType
            });
        return TypedResults.Json(roles);
    }).RequireAuthorization();

app.Run();













