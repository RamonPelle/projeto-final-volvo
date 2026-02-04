var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

// TODO: Verificar o que cada método faz e quais são opcionais
app.UseHttpsRedirection();
app.UseAuthorization();


app.MapControllers();
app.Run();