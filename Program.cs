using Microsoft.EntityFrameworkCore;
using TrilhaApiDesafio.Context;

var builder = WebApplication.CreateBuilder(args);

// Configura o DbContext com banco em memória para facilitar testes
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("TarefaDb"));

// Configura os serviços MVC/WebAPI
builder.Services.AddControllers();

// Configura o Swagger para documentação
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuração do pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
