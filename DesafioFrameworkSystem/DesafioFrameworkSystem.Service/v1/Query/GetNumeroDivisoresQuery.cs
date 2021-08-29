using DesafioFrameworkSystem.Domain.Entities;
using MediatR;

namespace DesafioFrameworkSystem.Service.v1.Query
{
    public class GetNumeroDivisoresQuery : IRequest<MathEntity>
    {
        public int NumeroEntrada { get; set; }
    }
}
