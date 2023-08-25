using MediatR;
using SistemaCompra.Domain.SolicitacaoCompraAggregate;
using SistemaCompra.Infra.Data.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ProdutoAgg = SistemaCompra.Domain.ProdutoAggregate;
using SolicitacaoCompraAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;

namespace SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra
{
    public class RegistrarCompraCommandHandler : CommandHandler, IRequestHandler<RegistrarCompraCommand, bool>
    {
        private readonly ISolicitacaoCompraRepository _solicitacaoCompraRepository;

        public RegistrarCompraCommandHandler(ISolicitacaoCompraRepository solicitacaoCompraRepository, IUnitOfWork uow, IMediator mediator) : base(uow, mediator)
        {
            _solicitacaoCompraRepository = solicitacaoCompraRepository;
        }

        public Task<bool> Handle(RegistrarCompraCommand request, CancellationToken cancellationToken)
        {
            var solicitacaoCompra = SolicitacaoCompraToRepositoryObject(request);

            var ItemList = ItemListToRepositoryObject(request.ListaItem);
            solicitacaoCompra.RegistrarCompra(ItemList);

            _solicitacaoCompraRepository.RegistrarCompra(solicitacaoCompra);

            Commit();
            PublishEvents(solicitacaoCompra.Events);

            return Task.FromResult(true);
        }

        private SolicitacaoCompraAgg.SolicitacaoCompra SolicitacaoCompraToRepositoryObject(RegistrarCompraCommand request)
        {
            return new SolicitacaoCompraAgg.SolicitacaoCompra(request.UsuarioSolicitante, request.NomeFornecedor);
        }

        private IEnumerable<Item> ItemListToRepositoryObject(IEnumerable<ItemCommand> ListaItem)
        {
            if (ListaItem is null)
                return Enumerable.Empty<Item>();

            return ListaItem.Select(item => new Item(ProductToRepositoryObject(item.Produto), item.Quantidade)).ToList();
        }

        private ProdutoAgg.Produto ProductToRepositoryObject(ProdutoCommand productCommand)
        {
            return new ProdutoAgg.Produto(productCommand.Nome, productCommand.Descricao, productCommand.Categoria, productCommand.Preco);
        }
    }
}
