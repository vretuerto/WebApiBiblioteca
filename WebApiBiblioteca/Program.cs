using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text.Json.Serialization;
using WebApiBiblioteca;
using WebApiBiblioteca.Filtros;
using WebApiBiblioteca.Middlewares;
using WebApiBiblioteca.Servicios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Para evitar ciclos infinitos en entidades relacionadas
builder.Services.AddControllers(
    opciones => opciones.Filters.Add(typeof(FiltroDeExcepcion))
    ).AddJsonOptions(opciones => opciones.JsonSerializerOptions.ReferenceHandler =
System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// Registramos en el sistema de inyección de dependencias de la aplicación el ApplicationDbContext

builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
{
    opciones.UseSqlServer(connectionString);
    opciones.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
}
);


builder.Services.AddControllers().AddJsonOptions(opciones => opciones.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddHostedService<TareaProgramadaService>();    

// Serilog
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();
builder.Host.UseSerilog();

var app = builder.Build();

app.UseMiddleware<LogFilePathIPMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
