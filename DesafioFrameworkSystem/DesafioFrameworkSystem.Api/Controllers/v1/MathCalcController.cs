using DesafioFrameworkSystem.Service.v1.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioFrameworkSystem.Api.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class MathCalcController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MathCalcController( IMediator mediator)
        {
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
                var resultado = await _mediator.Send(numerosDivisoresQuery);

                return resultado.Numeros;
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
                var resultado =  await _mediator.Send(divisoresPrimosQuery);

                return resultado.Numeros;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
