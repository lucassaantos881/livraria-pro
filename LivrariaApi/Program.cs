using LivrariaApi.Data;
using LivrariaApi.Services;
using Microsoft.EntityFrameworkCore;
using LivrariaApi.Middleware;

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
builder.Services.AddScoped<IPedidoService, PedidoService>();

builder.Services.AddControllers()
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters
    .Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
});

builder.Services.AddCors(options =>
{

    options.AddPolicy("BlazorPolicy", policy =>
    {
        policy.WithOrigins("https://localhost:7202") // Porta do Blazor WebAssembly
              .AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();


    });
});


var app = builder.Build();

app.UseCors("BlazorPolicy");

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
