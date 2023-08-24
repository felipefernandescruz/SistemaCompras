using MediatR;

namespace SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarSolicitacaoCompra
{
    public class ProdutoCommand : IRequest<bool>
    {
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
    }
}
