using DesafioFrameworkSystem.Service.v1.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioFrameworkSystem.Api.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class MathCalcController : ControllerBase
    {
        private readonly ILogger<MathCalcController> _logger;
        private readonly IMediator _mediator;

        public MathCalcController(ILogger<MathCalcController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        /// <summary>
        /// Lista os divisores do número de entrada.
        /// </summary>
        /// <returns>Os divisores</returns>
        [HttpGet("NumerosDivisores")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<int>>> NumerosDivisores([FromQuery] GetNumeroDivisoresQuery numerosDivisoresQuery)
        {
            try
            {
                return (await _mediator.Send(numerosDivisoresQuery)).Numeros;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Lista os divisores primos do número de entrada.
        /// </summary>
        /// <returns>Os divisores</returns>
        [HttpGet("DivisoresPrimos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<int>>> DivisoresPrimos([FromQuery] GetDivisoresPrimosQuery divisoresPrimosQuery)
        {
            try
            {
                return (await _mediator.Send(divisoresPrimosQuery)).Numeros;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
