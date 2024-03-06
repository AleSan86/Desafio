using BackendAPI.Configuration.Extensions;
using Dominio.Contexto;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

//Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Backend API",
        Version = "v1",
        Description = "API desarrollada para evaluación técnica",
        Contact = new OpenApiContact
        {
            Name = "Alejandro Adrián Sandrin",
            Email = "alejandrosandrin@gmail.com",
        },
    });
});

// Configuramos la cadena de conexión
var connectionString = builder.Configuration.GetConnectionString("DbConnection");
builder.Services.AddDbContext<DatabaseContext>(x => x.UseLazyLoadingProxies().UseSqlServer(connectionString));

//Uso de Inyección de dependencias
builder.Services.SetDependencyInjection();

//Registro el uso de Automapper
builder.Services.AddAutoMapper();

//Habilito CORS
builder.Services.AddCors(options => options.AddPolicy("CorsPolicy",
builder2 =>
{
    builder2.AllowAnyMethod()
            .AllowAnyHeader()
            .WithOrigins(builder.Configuration.GetGeneralConfiguration().AllowedOrigins)
            .AllowCredentials();
}));

var app = builder.Build();

//Registro Serilog
var loggerFactory = app.Services.GetService<ILoggerFactory>();
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
loggerFactory.AddFile(builder.Configuration["Logging:LogFile:Path"].ToString());
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

app.UseCors("CorsPolicy");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.RoutePrefix = "swagger";
        c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "Bienestar API");
    });
}

app.UseCors(builder =>
{
    builder.AllowAnyOrigin();
    builder.AllowAnyMethod();
    builder.AllowAnyHeader();
    builder.WithExposedHeaders("Content-Disposition");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
