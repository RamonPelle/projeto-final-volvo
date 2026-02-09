using Microsoft.EntityFrameworkCore;
using TechStore.Data;
using TechStore.Services.api;
using TechStore.Repository.api;
using TechStore.Middlewares;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Controllers e Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DI
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<CategoriaRepository>();
builder.Services.AddScoped<CategoriaService>();
builder.Services.AddScoped<ProdutoRepository>();
builder.Services.AddScoped<ProdutoService>();
builder.Services.AddScoped<PedidoRepository>();
builder.Services.AddScoped<PedidoService>();
builder.Services.AddScoped<ItemPedidoRepository>();

// DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<TechStoreContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

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
