using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using BrainWare.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Legacy API starts with Initial Caps.
builder.Services.AddControllers()
    .AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);
builder.Services.AddDbContext<BrainWareContext>(
    x => x.UseLazyLoadingProxies()
            .UseSqlServer(builder.Configuration.GetConnectionString("BrainWareConnectionString")));

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
