using MediatR;
using Netby.Application.Dtos;
using Netby.Application.Services.Interfaces;
using c = Netby.Domain;
using Netby.Domain;

namespace Netby.Application.UseCases.Campo
{
    public class AgregarCampo
    {
        public class CrearCamposCommand : IRequest<Result<CamposDtos>>
        {
            //public int Id { get; set; }
            public int FormularioId { get; set; }
            public string NombreCampo { get; set; }
            public string TipoCampo { get; set; }
            public bool EsRequerido { get; set; }
        }

        public class Handler : IRequestHandler<CrearCamposCommand, Result<CamposDtos>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<CamposDtos>> Handle(CrearCamposCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var campo = new c.Campo
                    {
                        FormularioId = request.FormularioId,
                        NombreCampo = request.NombreCampo,
                        TipoCampo = request.TipoCampo,
                        EsRequerido = request.EsRequerido,
                    };

                    await _unitOfWork.CampoRepository.Crear(campo);
                    await _unitOfWork.SaveChangesAsync();

                    var idC = campo.Id;

                    return Result<CamposDtos>.Success(new CamposDtos
                    {
                        Id = idC,
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