using MediatR;
using SistemaCompra.Domain.SolicitacaoCompraAggregate;
using SistemaCompra.Infra.Data.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SolicitacaoCompraAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;
using ProdutoAgg = SistemaCompra.Domain.ProdutoAggregate;
using SistemaCompra.Infra.Data.SolicitacaoCompra;

namespace SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarSolicitacaoCompra
{
    public class RegistrarCompraCommandHandler : CommandHandler, IRequestHandler<RegistrarCompraCommand, bool>
    {
        private readonly SolicitacaoCompraAgg.ISolicitacaoCompraRepository _solicitacaoCompraRepository;

        public RegistrarCompraCommandHandler(SolicitacaoCompraAgg.ISolicitacaoCompraRepository solicitacaoCompraRepository, IUnitOfWork uow, IMediator mediator) : base(uow, mediator)
        {
            _solicitacaoCompraRepository = solicitacaoCompraRepository;
        }

        public Task<bool> Handle(RegistrarCompraCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var solicitacaoCompra = SolicitacaoCompraToRepositoryObject(request);

                var ItemList = ItemListToRepositoryObject(request.ListaItem);
                solicitacaoCompra.RegistrarCompra(ItemList);

                _solicitacaoCompraRepository.RegistrarCompra(solicitacaoCompra);

                Commit();
                PublishEvents(solicitacaoCompra.Events);

                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
