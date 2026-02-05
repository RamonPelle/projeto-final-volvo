using Microsoft.EntityFrameworkCore;
using TechStore.Data;
using Microsoft.Extensions.DependencyInjection;
using TechStore.Services.api;
using TechStore.Repository.api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<CategoriaRepository>();
builder.Services.AddScoped<CategoriaService>();

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