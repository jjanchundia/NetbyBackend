using MediatR;
using Netby.Application.Dtos;
using Netby.Application.Services.Interfaces;
using Netby.Domain;

namespace Netby.Application.UseCases.Formulario
{
    public class ActualizarFormulario
    {
        public class ActualizarFormularioCommand : IRequest<Result<FormularioDto>>
        {
            public int Id { get; set; }
            public required string Nombre { get; set; }
            public string Descripcion { get; set; }
        }

        public class Handler : IRequestHandler<ActualizarFormularioCommand, Result<FormularioDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<FormularioDto>> Handle(ActualizarFormularioCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var formularioDb = _unitOfWork.FormularioRepository.ObtenerPorId(request.Id).Result;

                    if (formularioDb == null)
                    {
                        return Result<FormularioDto>.Failure("No se encontró Formulario para actualizar!");
                    }

                    formularioDb.Nombre = request.Nombre;
                    formularioDb.Descripcion = request.Descripcion;

                    await _unitOfWork.FormularioRepository.Modificar(formularioDb);
                    await _unitOfWork.SaveChangesAsync();

                    var FormularioDt = new FormularioDto()
                    {
                        Id = request.Id,
                        Nombre = request.Nombre,
                        Descripcion = request.Descripcion,
                    };

                    return Result<FormularioDto>.Success(new FormularioDto
                    {
                        Id = request.Id,
                        Nombre = request.Nombre,
                        Descripcion = request.Descripcion,
                    });
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error", e.Message);
                    throw;
                }
            }
        }
    }
}