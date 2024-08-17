using MediatR;
using Netby.Application.Services.Interfaces;
using Netby.Domain;

namespace Netby.Application.UseCases.Campo
{
    public class EliminarCampo
    {
        public class EliminarCampoCommand : IRequest<Result<string>>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<EliminarCampoCommand, Result<string>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<string>> Handle(EliminarCampoCommand request, CancellationToken cancellationToken)
            {
                var campo = await _unitOfWork.CampoRepository.ObtenerPorId(request.Id);

                if (campo == null)
                {
                    return Result<string>.Failure("No se encontró campo para eliminar!");
                }

                await _unitOfWork.CampoRepository.Elimninar(campo);
                await _unitOfWork.SaveChangesAsync();

                return Result<string>.Success("campo eliminado correctamente!");
            }
        }
    }
}