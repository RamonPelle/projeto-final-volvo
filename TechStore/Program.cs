using Microsoft.EntityFrameworkCore;
using TechStore.Data;
using Microsoft.Extensions.DependencyInjection;
using TechStore.Services.api;
using TechStore.Repository.api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using System.Diagnostics.Contracts;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    options.SwaggerDoc("v1", new global::Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "TechStore API - V1",
        Version = "v1",
        Description = "Documentação da primeira versão da API",
        Contact = new global::Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Ramon & Ana",
            Email = "ramon.pelle15@gmail.com & ana@email.com"
        }
    });

});
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    options.ReportApiVersions = true;
});

builder.Services.AddScoped<CategoriaRepository>();
builder.Services.AddScoped<CategoriaService>();
builder.Services.AddScoped<ProdutoRepository>();
builder.Services.AddScoped<ProdutoService>();
builder.Services.AddScoped<PedidoRepository>();
builder.Services.AddScoped<PedidoService>();
builder.Services.AddScoped<ItemPedidoRepository>();


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<TechStoreContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = "techstore";
    });

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = "swagger";
    });
}

app.UseSwagger();
app.UseSwaggerUI();

// TODO: Verificar o que cada método faz e quais são opcionais
app.UseHttpsRedirection();
app.UseAuthorization();


app.MapControllers();
app.Run();