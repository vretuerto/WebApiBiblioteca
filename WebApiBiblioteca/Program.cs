using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using System.Text.Json.Serialization;
using WebApiBiblioteca;
using WebApiBiblioteca.Filtros;
using WebApiBiblioteca.Hubs;
using WebApiBiblioteca.Middlewares;
using WebApiBiblioteca.Servicios;
using WebApiBiblioteca.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Para evitar ciclos infinitos en entidades relacionadas
builder.Services.AddControllers(opciones =>
{
    opciones.Filters.Add(typeof(FiltroDeExcepcion));
    opciones.Conventions.Add(new SwaggerVersion());
}
).AddJsonOptions(opciones => opciones.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{}
                    }
                });

});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPIAlmacen", Version = "v1" });
    c.SwaggerDoc("v2", new OpenApiInfo { Title = "WebAPIAlmacen", Version = "v2" });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// Registramos en el sistema de inyecci?n de dependencias de la aplicaci?n el ApplicationDbContext

builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
{
    opciones.UseSqlServer(connectionString);
    opciones.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
}
);


builder.Services.AddControllers().AddJsonOptions(opciones => opciones.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

//builder.Services.AddHostedService<TareaProgramadaService>();


builder.Services.AddDataProtection();
builder.Services.AddTransient<HashService>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(opciones => opciones.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = false,
                   ValidateAudience = false,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(
                     Encoding.UTF8.GetBytes(builder.Configuration["ClaveJWT"])),
                   ClockSkew = TimeSpan.Zero
               });


// Serilog
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();
builder.Host.UseSerilog();


builder.Services.AddSignalR();


var app = builder.Build();

app.MapHub<EncuestaHub>("/encuestaHub");


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
