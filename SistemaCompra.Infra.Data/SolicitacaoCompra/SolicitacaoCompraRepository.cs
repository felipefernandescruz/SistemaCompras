﻿using SistemaCompra.Domain.SolicitacaoCompraAggregate;
using SolicitacaoCompraAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;

namespace SistemaCompra.Infra.Data.SolicitacaoCompra
{

    public class SolicitacaoCompraRepository : ISolicitacaoCompraRepository
    {
        private readonly SistemaCompraContext _context;

        public SolicitacaoCompraRepository(SistemaCompraContext context)
        {
            _context = context;
        }
        public void RegistrarCompra(Domain.SolicitacaoCompraAggregate.SolicitacaoCompra entity)
        {
            _context.Set<SolicitacaoCompraAgg.SolicitacaoCompra>().Add(entity);
        }
    }
}
