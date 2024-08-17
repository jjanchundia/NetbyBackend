using MediatR;
using Microsoft.AspNetCore.Mvc;
using Netby.Application.UseCases.Campo;

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

        [HttpGet("obtenerCampoPorId")]
        public async Task<IActionResult> ObtenerCampoPorId(int id)
        {
            var response = await _mediator.Send(new ObtenerCampoPorId.ObtenercampoPorIdRequest() { Id = id });
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> CrearCampos(AgregarCampo.CrearCamposCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut("actualizarCampo")]
        public async Task<IActionResult> ActualizarCampo(ActualizarCampo.ActualizarCampoCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("eliminar/{id:int}")]
        public async Task<IActionResult> EliminarCampo(int id)
        {
            var response = await _mediator.Send(new EliminarCampo.EliminarCampoCommand() { Id = id });
            return Ok(response);
        }
    }
}