using MediatR;
using Netby.Application.Dtos;
using Netby.Application.Services.Interfaces;
using Netby.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netby.Application.UseCases.Formulario
{
    public class ObtenerFormularioPorId
    {
        public class ObtenerFormularioPorIdRequest : IRequest<Result<FormularioDto>>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<ObtenerFormularioPorIdRequest, Result<FormularioDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<FormularioDto>> Handle(ObtenerFormularioPorIdRequest request, CancellationToken cancellationToken)
            {
                var formulario = await _unitOfWork.FormularioRepository.ObtenerPorId(request.Id);

                var FormularioDto = new FormularioDto
                {
                    Id = formulario.Id,
                    Nombre = formulario.Nombre,
                    Descripcion = formulario.Descripcion,
                };

                return Result<FormularioDto>.Success(FormularioDto);
            }
        }
    }
}