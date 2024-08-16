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
//builder.Services.AddScoped<IRequestHandler<ObtenerTiposPermisos.ObtenerTiposPermisosRequest, Result<List<TipoPermisoDto>>>, ObtenerTiposPermisos.Handler>();
builder.Services.AddScoped<IRequestHandler<AgregarCampo.CrearCamposCommand, Result<CamposDtos>>, AgregarCampo.Handler>();
//builder.Services.AddScoped<IRequestHandler<ModificarTipoPermiso.ModificarTipoPermisoCommand, Result<TipoPermisoDto>>, ModificarTipoPermiso.Handler>();

builder.Services.AddScoped<IRequestHandler<ObtenerFormularios.ObtenerFormulariosRequest, Result<List<FormularioDto>>>, ObtenerFormularios.Handler>();
builder.Services.AddScoped<IRequestHandler<AgregarFormulario.CrearFormularioCommand, Result<FormularioDto>>, AgregarFormulario.Handler>();
//builder.Services.AddScoped<IRequestHandler<ObtenerPermiso.ObtenerPermisoRequest, Result<List<PermisoDto>>>, ObtenerPermiso.Handler>();
//builder.Services.AddScoped<IRequestHandler<ObtenerPermisoPorId.ObtenerPermisoPorIdRequest, Result<PermisoDto>>, ObtenerPermisoPorId.Handler>();
//builder.Services.AddScoped<IRequestHandler<SolicitarPermiso.SolicitarPermisoCommand, Result<PermisoDto>>, SolicitarPermiso.Handler>();
//builder.Services.AddScoped<IRequestHandler<ModificarPermiso.ModificarPermisoCommand, Result<PermisoDto>>, ModificarPermiso.Handler>();

builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

var app = builder.Build();

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
