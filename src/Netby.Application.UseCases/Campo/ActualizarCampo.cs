using MediatR;
using Netby.Application.Dtos;
using Netby.Application.Services.Interfaces;
using Netby.Domain;

namespace Netby.Application.UseCases.Campo
{
    public class ActualizarCampo
    {
        public class ActualizarCampoCommand : IRequest<Result<CamposDtos>>
        {
            public int Id { get; set; }
            public int FormularioId { get; set; }
            public string NombreCampo { get; set; }
            public string TipoCampo { get; set; }
            public bool EsRequerido { get; set; }
        }

        public class Handler : IRequestHandler<ActualizarCampoCommand, Result<CamposDtos>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<CamposDtos>> Handle(ActualizarCampoCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var campoDb = _unitOfWork.CampoRepository.ObtenerPorId(request.Id).Result;

                    if (campoDb == null)
                    {
                        return Result<CamposDtos>.Failure("No se encontró Campo para actualizar!");
                    }

                    campoDb.FormularioId = request.FormularioId;
                    campoDb.NombreCampo = request.NombreCampo;
                    campoDb.TipoCampo = request.TipoCampo;
                    campoDb.EsRequerido = request.EsRequerido;

                    await _unitOfWork.CampoRepository.Modificar(campoDb);
                    await _unitOfWork.SaveChangesAsync();

                    var FormularioDt = new CamposDtos()
                    {
                        FormularioId = request.FormularioId,
                        NombreCampo = request.NombreCampo,
                        TipoCampo = request.TipoCampo,
                        EsRequerido = request.EsRequerido,
                    };

                    return Result<CamposDtos>.Success(new CamposDtos
                    {
                        Id = request.Id,
                        FormularioId = request.FormularioId,
                        NombreCampo = request.NombreCampo,
                        TipoCampo = request.TipoCampo,
                        EsRequerido = request.EsRequerido,
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