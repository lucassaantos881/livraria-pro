using LivrariaApi.Data;
using LivrariaApi.Services;
using Microsoft.EntityFrameworkCore;
using LivrariaApi.Middleware;
using Serilog;

//Configura o Serilog globalmente para a aplicação
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()                        //mostra no console
    .WriteTo.File("Logs/log-.txt",            //salva em arquivo, com nome log-2024-06-01.txt, por exemplo
        rollingInterval: RollingInterval.Day) //um arquivo novo por dia
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Registra o LivrariaContext como um serviço, para que ele possa ser injetado em outros lugares da aplicação
//adiciona o contexto como serviço
//usando SqlLite
//busca o endereço appsetings.json, onde tem a string de conexão com o banco de dados
//especificamente a chave "DefaultConnection"
builder.Services.AddDbContext<LivrariaContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Host.UseSerilog(); // Configura o Serilog como o logger para a aplicação

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
