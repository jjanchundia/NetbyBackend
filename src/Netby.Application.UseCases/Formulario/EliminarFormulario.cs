using MediatR;
using Netby.Application.Services.Interfaces;
using Netby.Domain;
using Netby.Infraestucture.Persistence;

namespace Netby.Application.UseCases.Formulario
{
    public class EliminarFormulario
    {

        public class EliminarFormularioCommand: IRequest<Result<string>>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<EliminarFormularioCommand, Result<string>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<string>> Handle(EliminarFormularioCommand request, CancellationToken cancellationToken)
            {
                var formulario = await _unitOfWork.FormularioRepository.ObtenerPorId(request.Id);

                if (formulario == null)
                {
                    // El formulario no fue encontrado, puedes manejar esta situación de acuerdo a tus necesidades
                    return Result<string>.Failure("No se encontró formulario para eliminar!");
                }

                await _unitOfWork.FormularioRepository.Elimninar(formulario);
                await _unitOfWork.SaveChangesAsync();

                return Result<string>.Success("Formulario eliminado correctamente!");
            }
        }
    }
}