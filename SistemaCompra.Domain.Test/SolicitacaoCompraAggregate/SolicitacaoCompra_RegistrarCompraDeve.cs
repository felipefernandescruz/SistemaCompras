using SistemaCompra.Domain.Core;
using SistemaCompra.Domain.ProdutoAggregate;
using SistemaCompra.Domain.SolicitacaoCompraAggregate;
using System.Collections.Generic;
using Xunit;

namespace SistemaCompra.Domain.Test.SolicitacaoCompraAggregate
{
    public class SolicitacaoCompra_RegistrarCompraDeve
    {
        [Fact]
        public void DefinirPrazo30DiasAoComprarMais50mil()
        {
            //Dado
            var solicitacao = new SolicitacaoCompra("rodrigoasth", "rodrigoasth");
            var itens = new List<Item>();
            var produto = new Produto("Cedro", "Transversal 3/3", Categoria.Madeira.ToString(), 1001);
            itens.Add(new Item(produto, 50));

            //Quando
            solicitacao.RegistrarCompra(itens);

            //Então
            Assert.Equal(30, solicitacao.CondicaoPagamento.Valor);
        }

        [Fact]
        public void NotificarErroQuandoNaoInformarItensCompra()
        {
            //Dado
            var solicitacao = new SolicitacaoCompra("rodrigoasth", "rodrigoasth");
            var itens = new List<Item>();

            //Quando 
            var ex = Assert.Throws<BusinessRuleException>(() => solicitacao.RegistrarCompra(itens));

            //Então
            Assert.Equal("A solicitação de compra deve possuir itens!", ex.Message);
        }

        [Fact]
        public void NotificarErroQuandoTotalGeralMaiorQueZero()
        {
            //Dado
            var solicitacao = new SolicitacaoCompra("rodrigoasth", "rodrigoasth");
            var itens = new List<Item>();
            var produto = new Produto("Cedro", "Transversal 3/3", Categoria.Madeira.ToString(), 0);
            itens.Add(new Item(produto, 50));

            //Quando
            var ex = Assert.Throws<BusinessRuleException>(() => solicitacao.RegistrarCompra(itens));

            //Então
            Assert.Equal("O valor total da compra precisam ser maiores do que 0", ex.Message);
        }

        [Theory]
        [InlineData("r")]
        [InlineData("ro")]
        [InlineData("rod")]
        [InlineData("rodr")]
        public void NotificarErroPorNomeUsuarioMenorQueCincoCaracters(string usuarioSolicitante)
        {
            //Quando
            var ex = Assert.Throws<BusinessRuleException>(() => new SolicitacaoCompra(usuarioSolicitante, "João Teste Fornecedor"));

            //Então
            Assert.Equal("Nome de usuário deve possuir pelo menos 5 caracteres.", ex.Message);
        }

        [Theory]
        [InlineData("r")]
        [InlineData("ro")]
        [InlineData("rod")]
        [InlineData("rodr")]
        [InlineData("rodri")]
        [InlineData("rodrig")]
        [InlineData("rodrigo")]
        [InlineData("rodrigoa")]
        [InlineData("rodrigoas")]
        public void NotificarErroPorNomeFornecedorMenorQueCincoCaracters(string nomeFornecedor)
        {
            //Quando
            var ex = Assert.Throws<BusinessRuleException>(() => new SolicitacaoCompra("Felipe Fernandes", nomeFornecedor));

            //Então
            Assert.Equal("Nome de fornecedor deve ter pelo menos 10 caracteres.", ex.Message);
        }
    }
}
