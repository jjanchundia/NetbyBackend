using MediatR;
using Netby.Application.Dtos;
using Netby.Application.Services.Interfaces;
using Netby.Domain;

namespace Netby.Application.UseCases.Campo
{
    public class ObtenerCampos
    {
        public class ObtenerCamposRequest : IRequest<Result<List<CamposDtos>>> {}

        public class Handler : IRequestHandler<ObtenerCamposRequest, Result<List<CamposDtos>>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<List<CamposDtos>>> Handle(ObtenerCamposRequest request, CancellationToken cancellationToken)
            {
                var campos = await _unitOfWork.CampoRepository.ObtenerTodo();
                var camposDto = new List<CamposDtos>();

                foreach (var item in campos)
                {
                    //var u = await _unitOfWork.UsuarioRepository.ObtenerPorId(item.UsuarioId);
                    var CamposDtos = new CamposDtos
                    {
                        Id = item.Id,
                        FormularioId = item.FormularioId,
                        NombreCampo = item.NombreCampo,
                        TipoCampo = item.TipoCampo,
                        EsRequerido = item.EsRequerido,
                    };

                    camposDto.Add(CamposDtos);
                }

                return Result<List<CamposDtos>>.Success(camposDto);
            }
        }
    }
}