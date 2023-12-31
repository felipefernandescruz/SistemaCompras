﻿using SistemaCompra.Domain.Core;
using SistemaCompra.Domain.Core.Model;
using SistemaCompra.Domain.ProdutoAggregate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaCompra.Domain.SolicitacaoCompraAggregate
{
    public class SolicitacaoCompra : Entity
    {
        public UsuarioSolicitante UsuarioSolicitante { get; private set; }
        public NomeFornecedor NomeFornecedor { get; private set; }
        public IList<Item> Itens { get; private set; }
        public DateTime Data { get; private set; }
        public Money TotalGeral { get; private set; }
        public Situacao Situacao { get; private set; }
        public CondicaoPagamento CondicaoPagamento { get; private set; }

        private SolicitacaoCompra() { }

        public SolicitacaoCompra(string usuarioSolicitante, string nomeFornecedor)
        {
            Id = Guid.NewGuid();
            UsuarioSolicitante = new UsuarioSolicitante(usuarioSolicitante);
            NomeFornecedor = new NomeFornecedor(nomeFornecedor);
            Data = DateTime.Now;
            Situacao = Situacao.Solicitado;
        }

        public void AdicionarItem(Produto produto, int qtde)
        {
            Itens.Add(new Item(produto, qtde));
        }

        public void RegistrarCompra(IEnumerable<Item> itens)
        {
            if (itens is null || itens.Any() is false)
                throw new BusinessRuleException("A solicitação de compra deve possuir itens!");

            Itens = itens.ToList();
            CalcularTotalGeral();
        }

        public void CalcularTotalGeral()
        {
            TotalGeral = new Money(Itens.Sum(x => x.Subtotal.Value));

            if (TotalGeral is null || TotalGeral.Value == 0)
                throw new BusinessRuleException("O valor total da compra precisam ser maiores do que 0");

            DefinirCondicaoPagamento();
        }
        private void DefinirCondicaoPagamento()
        {
            CondicaoPagamento = new CondicaoPagamento(PrazoPagamento(TotalGeral.Value));
        }

        private static int PrazoPagamento(decimal valorTotal)
        {
            if (valorTotal > 50000) return 30;

            return default;
        }
    }
}
