using DesafioFrameworkSystem.Service.v1.Query;
using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace DesafioFrameworkSystem.Service.Test.v1.Query
{
    public class GetDivisoresPrimosQueryHandlerTests
    {
        private readonly GetDivisoresPrimosQueryHandler _testee;
        private readonly int _numeroEntrada = 45;

        public GetDivisoresPrimosQueryHandlerTests()
        {
            _testee = new GetDivisoresPrimosQueryHandler();
        }

        [Fact]
        public async Task Handle_WithValidArray_ShouldReturnNumerosDivisoresPrimos()
        {
            var result = await _testee.Handle(new GetDivisoresPrimosQuery { NumeroEntrada = _numeroEntrada }, default);
            
            int[] numerosDivisoresPrimos = new int[] { 1,3,5};

            result.Numeros.Should().Equal(numerosDivisoresPrimos);
        }
    }


}
