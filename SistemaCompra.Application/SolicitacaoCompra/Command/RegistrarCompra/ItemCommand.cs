using MediatR;

namespace SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra
{
    public class ItemCommand : IRequest<bool>
    {
        public ProdutoCommand Produto { get; set; }

        public int Quantidade { get; set; }
    }
}
