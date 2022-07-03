using Microsoft.EntityFrameworkCore;
using MinimalAPIBiblioteca.Entidades;
using MinimalAPIBiblioteca.Services;

var builder = WebApplication.CreateBuilder(args);


IConfiguration configuration = new ConfigurationBuilder()
      .AddJsonFile("appsettings.json", true, true)
      .Build();


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = configuration.GetConnectionString("defaultConnection");
builder.Services.AddDbContext<BibliotecaContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<EditorialesService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/editoriales", async (EditorialesService editorialesService) =>
{
    return await editorialesService.GetEditoriales();
});

app.MapGet("/editoriales/{id}", async (EditorialesService editorialesService, int id) =>
{
    var editorial = await editorialesService.GetEditoriales(id);
    return editorial.Value != null ? Results.Ok(editorial) : Results.NotFound();
});

app.MapPost("/editoriales/{nombre}", async (EditorialesService editorialesService, string nombre) =>
{
    var editorial = await editorialesService.PostEditoriales(nombre);
    return Results.Ok(editorial);
});

app.MapPut("/editoriales/{id}/{nombre}", async (EditorialesService editorialesService, int id, string nombre) =>
{
    var editorial = await editorialesService.GetEditoriales(id);
    if (editorial.Value == null)
    {
        return Results.NotFound();
    }
    await editorialesService.PutEditoriales(id, nombre);
    return Results.NoContent();
});

app.MapDelete("/editoriales/{id}", async (EditorialesService editorialesService, int id) =>
{
    var editorial = await editorialesService.GetEditoriales(id);
    if (editorial.Value == null)
    {
        return Results.NotFound();
    }
    await editorialesService.DeleteEditoriales(id);
    return Results.Ok();
});


app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}