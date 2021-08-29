using DesafioFrameworkSystem.Application;
using DesafioFrameworkSystem.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DesafioFrameworkSystem.Service.v1.Query
{
    public class GetNumeroDivisoresQueryHandler : IRequestHandler<GetNumeroDivisoresQuery, MathEntity>
    {
        public GetNumeroDivisoresQueryHandler()
        {
        }

        public async Task<MathEntity> Handle(GetNumeroDivisoresQuery request, CancellationToken cancellationToken)
        {
            return new MathEntity
            {
                Numeros = new MathCalcApplication(request.NumeroEntrada).NumeroDivisores
            };
        }
    }
}
