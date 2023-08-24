using MediatR;
using System.Collections.Generic;

namespace SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarSolicitacaoCompra
{
    public class RegistrarCompraCommand : IRequest<bool>
    {
        public string UsuarioSolicitante { get; set; }

        public string NomeFornecedor { get; set; }

        public IEnumerable<ItemCommand> ListaItem { get; set; }
    }
}
