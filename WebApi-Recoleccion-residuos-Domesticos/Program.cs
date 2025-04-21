using Microsoft.EntityFrameworkCore;
using Recoleccion.AccesoDatos.Data;
using Recoleccion.AccesoDatos.Data.Repository.IRepository;
using Recoleccion.AccesoDatos.Data.Repository;


var builder = WebApplication.CreateBuilder(args);

// Obtenempos la cadena de conexion desde el archivo appsettings.json
var connectionString = builder.Configuration.GetConnectionString("CadenaSQL");

// Configuramos el DbContext para usar SQL Server
builder.Services.AddDbContext<RecoleccionResiduosContext>(options => 
{
    options.UseSqlServer(connectionString);
});

// Registramos servicios antes de construir la aplicacion.
builder.Services.AddScoped<IContenedorTrabajo, ContenedorTrabajo>();

// Configuramos los CORS.
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTodo", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});


// Agregamos controladores y configuramos Swagger.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var app = builder.Build();

// Configuramos el entorno de desarrollo. - Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("PermitirTodo");
app.UseAuthorization();
app.MapControllers();

app.Run();