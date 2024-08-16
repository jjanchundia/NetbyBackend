using MediatR;
using Netby.Application.Dtos;
using Netby.Application.Services.Interfaces;
using Netby.Domain;
using c = Netby.Domain;

namespace Netby.Application.UseCases.Formulario
{
    public class AgregarFormulario
    {
        public class CrearFormularioCommand : IRequest<Result<FormularioDto>>
        {
            //public int Id { get; set; }
            public string Nombre { get; set; }
            public string Descripcion { get; set; }
        }

        public class Handler : IRequestHandler<CrearFormularioCommand, Result<FormularioDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<FormularioDto>> Handle(CrearFormularioCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var formulario = new c.Formulario
                    {
                        Nombre = request.Nombre,
                        Descripcion = request.Descripcion,
                    };

                    await _unitOfWork.FormularioRepository.Crear(formulario);
                    await _unitOfWork.SaveChangesAsync();

                    var idF = formulario.Id;

                    return Result<FormularioDto>.Success(new FormularioDto
                    {
                        Id = idF,
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