using LivrariaApi.Data;
using LivrariaApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Registra o LivrariaContext como um serviço, para que ele possa ser injetado em outros lugares da aplicação
//adiciona o contexto como serviço
//usando SqlLite
//busca o endereço appsetings.json, onde tem a string de conexão com o banco de dados
//especificamente a chave "DefaultConnection"
builder.Services.AddDbContext<LivrariaContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ILivroService, LivroService>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
