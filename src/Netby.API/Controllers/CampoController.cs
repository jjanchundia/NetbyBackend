using MediatR;
using Microsoft.AspNetCore.Mvc;
using Netby.Application.UseCases.Campo;

namespace Netby.API.Controllers
{
    public class CampoController : Controller
    {
        [Route("api/[controller]")]
        [ApiController]
        public class UsuariosController : ControllerBase
        {
            //Uso del patron mediador - CQRS
            private readonly IMediator _mediator;

            public UsuariosController(IMediator mediator)
            {
                _mediator = mediator;
            }

            [HttpGet]
            public async Task<IActionResult> ConsultarFormulario()
            {
                var response = await _mediator.Send(new ObtenerCampos.ObtenerCamposRequest());
                return Ok(response);
            }

            [HttpPost("register")]
            public async Task<IActionResult> CreateFormulario(AgregarCampo.CrearCamposCommand command)
            {
                var response = await _mediator.Send(command);

                return Ok(response);
            }
        }
    }
}