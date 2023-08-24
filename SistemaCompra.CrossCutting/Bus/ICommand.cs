using MediatR;

namespace SistemaCompra.CrossCutting.Bus
{
    public interface ICommand<T> : IRequest<T>
    {
    }
}
