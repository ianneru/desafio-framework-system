using DesafioFrameworkSystem.Api.Controllers;
using DesafioFrameworkSystem.Service.v1.Query;
using FakeItEasy;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using Xunit;

namespace DesafioFrameworkSystem.Api.Test.Controllers.v1
{
    public class MathCalcControllerTests
    {
        private readonly IMediator _mediator;
        private readonly MathCalcController _testee;

        public MathCalcControllerTests()
        {
            _mediator = A.Fake<IMediator>();

            _testee = new MathCalcController(_mediator);
        }

        [Fact]
        public async void Get_ShouldReturnNumeroDivisores()
        {
            var result = await _testee.NumerosDivisores(new GetNumeroDivisoresQuery { NumeroEntrada = 45 });

            var numerosDivisores = new int[] { 1, 3, 5, 9, 15, 45 };

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("Numeros Divisores could not be loaded")]
        public async void Get_WhenAnExceptionOccurs_NumerosDivisores_ShouldReturnBadRequest(string exceptionMessage)
        {
            A.CallTo(() => _mediator.Send(A<GetNumeroDivisoresQuery>._, default)).Throws(new Exception(exceptionMessage));

            var result = await _testee.NumerosDivisores(new GetNumeroDivisoresQuery { NumeroEntrada = 45 });

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            (result.Result as BadRequestObjectResult)?.Value.Should().Be(exceptionMessage);
        }

        [Theory]
        [InlineData("Numeros Divisores Primos could not be loaded")]
        public async void Get_WhenAnExceptionOccurs_NumerosDivisoresPrimos_ShouldReturnBadRequest(string exceptionMessage)
        {
            A.CallTo(() => _mediator.Send(A<GetDivisoresPrimosQuery>._, default)).Throws(new Exception(exceptionMessage));

            var result = await _testee.DivisoresPrimos(new GetDivisoresPrimosQuery { NumeroEntrada = 45 });

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            (result.Result as BadRequestObjectResult)?.Value.Should().Be(exceptionMessage);
        }

    }
}