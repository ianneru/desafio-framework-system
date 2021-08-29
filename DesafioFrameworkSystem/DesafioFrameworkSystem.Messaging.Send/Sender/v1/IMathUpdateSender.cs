using DesafioFrameworkSystem.Domain.Entities;

namespace CustomerApi.Messaging.Send.Sender.v1
{
    public interface IMathUpdateSender
    {
        void SendCustomer(MathEntity customer);
    }
}