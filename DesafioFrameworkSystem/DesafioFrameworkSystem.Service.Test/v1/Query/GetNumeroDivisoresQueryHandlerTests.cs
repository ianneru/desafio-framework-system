using DesafioFrameworkSystem.Service.v1.Query;
using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace DesafioFrameworkSystem.Service.Test.v1.Query
{
    public class GetNumeroDivisoresQueryHandlerTests
    {
        private readonly GetNumeroDivisoresQueryHandler _testee;
        private readonly int _numeroEntrada = 45;

        public GetNumeroDivisoresQueryHandlerTests()
        {
            _testee = new GetNumeroDivisoresQueryHandler();
        }

        [Fact]
        public async Task Handle_WithValidArray_ShouldReturnNumerosDivisores()
        {
            var result = await _testee.Handle(new GetNumeroDivisoresQuery { NumeroEntrada = _numeroEntrada }, default);
            
            int[] numerosDivisores = new int[] { 1,3,5,9,15,45 };

            result.Numeros.Should().Equal(numerosDivisores);
        }
    }


}
