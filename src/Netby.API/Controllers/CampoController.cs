using MediatR;
using Microsoft.AspNetCore.Mvc;
using Netby.Application.UseCases.Campo;
using Netby.Application.UseCases.Formulario;

namespace Netby.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampoController : Controller
    {
        //Uso del patron mediador - CQRS
        private readonly IMediator _mediator;

        public CampoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> ConsultarCampos()
        {
            var response = await _mediator.Send(new ObtenerCampos.ObtenerCamposRequest());
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> CrearCampos(AgregarCampo.CrearCamposCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpPut("actualizarFormulario")]
        public async Task<IActionResult> ActualizarFormulario(ActualizarFormulario.ActualizarFormularioCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}