using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarSolicitacaoCompra;
using System;

namespace SistemaCompra.API.SolicitacaoCompra
{
    [Route("[controller]")]
    public class SolicitacaoCompraController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SolicitacaoCompraController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CriarSolicitacaoCompra([FromBody] RegistrarCompraCommand registrarCompraCommand)
        {
            try {
                _mediator.Send(registrarCompraCommand);
                return StatusCode(StatusCodes.Status201Created);

            }catch (Exception ex) 
            { 
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message); 
            }

        }
    }
}
