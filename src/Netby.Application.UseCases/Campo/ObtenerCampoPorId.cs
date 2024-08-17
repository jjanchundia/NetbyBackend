using MediatR;
using Netby.Application.Dtos;
using Netby.Application.Services.Interfaces;
using Netby.Domain;

namespace Netby.Application.UseCases.Campo
{
    public class ObtenerCampoPorId
    {
        public class ObtenercampoPorIdRequest : IRequest<Result<CamposDtos>>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<ObtenercampoPorIdRequest, Result<CamposDtos>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<CamposDtos>> Handle(ObtenercampoPorIdRequest request, CancellationToken cancellationToken)
            {
                var campo = await _unitOfWork.CampoRepository.ObtenerPorId(request.Id);

                var formulario = await _unitOfWork.FormularioRepository.ObtenerPorId(campo.FormularioId);

                var CamposDtos = new CamposDtos
                {
                    Id = campo.Id,
                    FormularioId = campo.FormularioId,
                    NombreCampo = campo.NombreCampo,
                    TipoCampo = campo.TipoCampo,
                    EsRequerido = campo.EsRequerido,
                    FormularioNombre = formulario.Nombre,
                };

                return Result<CamposDtos>.Success(CamposDtos);
            }
        }
    }
}