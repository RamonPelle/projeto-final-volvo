using Microsoft.EntityFrameworkCore;
using TechStore.Data;
using TechStore.Services.api;
using TechStore.Repository.api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using System.Diagnostics.Contracts;
using Microsoft.OpenApi.Models;
using System.Reflection;
using TechStore.Middlewares;
using AutoMapper;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using TechStore.Services;

var builder = WebApplication.CreateBuilder(args);

// Controllers e Swagger
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

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    options.ReportApiVersions = true;
});
// DI
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<CategoriaRepository>();
builder.Services.AddScoped<CategoriaService>();
builder.Services.AddScoped<ProdutoRepository>();
builder.Services.AddScoped<ProdutoService>();
builder.Services.AddScoped<PedidoRepository>();
builder.Services.AddScoped<PedidoService>();
builder.Services.AddScoped<ItemPedidoRepository>();
builder.Services.AddScoped<ItemPedidoService>();
builder.Services.AddScoped<ClienteRepository>();
builder.Services.AddScoped<SenhaService>();
builder.Services.AddScoped<ClienteService>();

// DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<TechStoreContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

// Configuração de cultura para resolver problema de validação de decimal
var supportedCultures = new[] { new CultureInfo("en-US") };
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en-US"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

// Swagger
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
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Pipeline HTTP
app.UseHttpsRedirection();

// Middleware de exceções
app.UseMiddleware<TratamentoExcecoesMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
