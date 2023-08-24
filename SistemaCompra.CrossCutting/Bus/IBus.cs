using System.Threading.Tasks;

namespace SistemaCompra.CrossCutting.Bus
{
    public interface IBus
    {
        Task SendCommand<T>(ICommand<T> command);
        Task RaiseEvent(IEvent @event);
    }
}
