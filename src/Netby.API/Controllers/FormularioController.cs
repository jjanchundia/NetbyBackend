using MediatR;
using Microsoft.AspNetCore.Mvc;
using Netby.Application.UseCases.Formulario;

namespace Netby.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormularioController : Controller
    {
        //Uso del patron mediador - CQRS
        private readonly IMediator _mediator;

        public FormularioController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> ConsultarFormulario()
        {
            var response = await _mediator.Send(new ObtenerFormularios.ObtenerFormulariosRequest());
            return Ok(response);
        }

        [HttpGet("ObtenerFormularioPorId")]
        public async Task<IActionResult> ObtenerFormularioPorId(int id)
        {
            var response = await _mediator.Send(new ObtenerFormularioPorId.ObtenerFormularioPorIdRequest() { Id = id });
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> CrearFormulario(AgregarFormulario.CrearFormularioCommand command)
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

        [HttpDelete("eliminar/{id:int}")]
        public async Task<IActionResult> EliminarFormulario(int id)
        {
            var response = await _mediator.Send(new EliminarFormulario.EliminarFormularioCommand() { Id = id });
            return Ok(response);
        }
    }
}