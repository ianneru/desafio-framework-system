using DesafioFrameworkSystem.Application;
using DesafioFrameworkSystem.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DesafioFrameworkSystem.Service.v1.Query
{
    public class GetDivisoresPrimosQueryHandler : IRequestHandler<GetDivisoresPrimosQuery, MathEntity>
    {
        public GetDivisoresPrimosQueryHandler()
        {
        }

        public async Task<MathEntity> Handle(GetDivisoresPrimosQuery request, CancellationToken cancellationToken)
        {
            return new MathEntity
            {
                Numeros = new MathCalcApplication(request.NumeroEntrada).DivisoresPrimos
            };
        }
    }
}
