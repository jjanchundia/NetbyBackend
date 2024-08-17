using MediatR;
using Microsoft.EntityFrameworkCore;
using Netby.Application.Dtos;
using Netby.Application.Services.Interfaces;
using Netby.Application.Services.Repository;
using Netby.Application.UseCases.Campo;
using Netby.Application.UseCases.Formulario;
using Netby.Domain;
using Netby.Infraestucture.Persistence;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Agregamos politicas CORS para uso de endpoint localmente
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionDB")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddScoped<IRequestHandler<ObtenerCampos.ObtenerCamposRequest, Result<List<CamposDtos>>>, ObtenerCampos.Handler>();
builder.Services.AddScoped<IRequestHandler<ObtenerCampoPorId.ObtenercampoPorIdRequest, Result<CamposDtos>>, ObtenerCampoPorId.Handler>();
builder.Services.AddScoped<IRequestHandler<AgregarCampo.CrearCamposCommand, Result<CamposDtos>>, AgregarCampo.Handler>();
builder.Services.AddScoped<IRequestHandler<ActualizarCampo.ActualizarCampoCommand, Result<CamposDtos>>, ActualizarCampo.Handler>();
builder.Services.AddScoped<IRequestHandler<EliminarCampo.EliminarCampoCommand, Result<string>>, EliminarCampo.Handler>();
builder.Services.AddScoped<IRequestHandler<ObtenerFormularios.ObtenerFormulariosRequest, Result<List<FormularioDto>>>, ObtenerFormularios.Handler>();
builder.Services.AddScoped<IRequestHandler<AgregarFormulario.CrearFormularioCommand, Result<FormularioDto>>, AgregarFormulario.Handler>();
builder.Services.AddScoped<IRequestHandler<ObtenerFormularioPorId.ObtenerFormularioPorIdRequest, Result<FormularioDto>>, ObtenerFormularioPorId.Handler>();
builder.Services.AddScoped<IRequestHandler<ActualizarFormulario.ActualizarFormularioCommand, Result<FormularioDto>>, ActualizarFormulario.Handler>();
builder.Services.AddScoped<IRequestHandler<EliminarFormulario.EliminarFormularioCommand, Result<string>>, EliminarFormulario.Handler>();

builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Aplica la política CORS a todas las solicitudes
app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
