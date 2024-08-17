using MediatR;
using Netby.Application.Dtos;
using Netby.Application.Services.Interfaces;
using Netby.Domain;

namespace Netby.Application.UseCases.Formulario
{
    public class ObtenerFormularios
    {
        public class ObtenerFormulariosRequest : IRequest<Result<List<FormularioDto>>> { }

        public class Handler : IRequestHandler<ObtenerFormulariosRequest, Result<List<FormularioDto>>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<List<FormularioDto>>> Handle(ObtenerFormulariosRequest request, CancellationToken cancellationToken)
            {
                var formularios = await _unitOfWork.FormularioRepository.ObtenerTodo();
                var formulariosDto = new List<FormularioDto>();

                foreach (var item in formularios)
                {
                    var FormularioDto = new FormularioDto
                    {
                        Id = item.Id,
                        Nombre = item.Nombre,
                        Descripcion = item.Descripcion,
                    };

                    formulariosDto.Add(FormularioDto);
                }

                return Result<List<FormularioDto>>.Success(formulariosDto);
            }
        }
    }
}