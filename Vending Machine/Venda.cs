using System;
using System.Collections.Generic;
using System.Text;

namespace Vending_Machine
{
    class Venda
    {
        private static int Auto_Incremento;
        public int CodigoVenda { get; private set; }
        public List<Produto> ProdutosVenda { get; private set; }
        public HistoricoDeVendas HistoricoDeVendas { get; private set; }
        public Estoque Estoque { get; private set; }
        public double Troco { get; private set; }
        public double ValorAPagar { get; private set; }
        public double ValorFaltante { get; private set; }
        private List<double> ValoresInseridosNoPagamento { get; set; }

        public Venda(Estoque estoque, HistoricoDeVendas historicoDeVendas)
        {
            this.ProdutosVenda = new List<Produto>();
            this.Estoque = estoque;
            this.HistoricoDeVendas = historicoDeVendas;
            this.Troco = 0;
            this.ValorAPagar = 0;
            this.ValorFaltante = 0;
            ValoresInseridosNoPagamento = new List<double>();
            this.CodigoVenda = 100 + NovoCodigo();
        }

        //Método privado de Venda, gera o código da venda
        private int NovoCodigo()
        {
            Auto_Incremento++;
            return CodigoVenda = CodigoVenda + Auto_Incremento;
        }

        //Adiciona produto a lista de produtos que serão vendidos por meio do código do produto
        public bool AdicionarProdutoNaVenda(int codigo)
        {
            foreach (Produto p in Estoque.GetProdutos())
            {
                if (codigo == p.GetCodigoProduto() && p.QuantidadeEstoque > 0)
                {
                    ProdutosVenda.Add(p);
                    p.AlteraQuantidadeEstoque(-1);
                    return true;
                }
            }
            return false;
        }

        //Remove os produtos da lista de vendas por meio do código do produto, não foi implementado no programa.
        public bool RemoveProdutoNaVenda(int codigo)
        {
            foreach (Produto p in Estoque.GetProdutos())
            {
                if (codigo == p.GetCodigoProduto())
                {
                    ProdutosVenda.Remove(p);
                    p.AlteraQuantidadeEstoque(1);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        //Visualiza os produtos na venda, nome e o valor.
        public void VisualizarProdutosNaVenda()
        {
            foreach (Produto p in ProdutosVenda)
            {
                Console.WriteLine(p.NomeDoProduto + ", R$" + p.ValorDoProduto.ToString("F2"));
            }
        }

        //Esse método retorna o valor que deve ser pago quando não foi pago o valor completo da venda.
        public double ValoresAPagar()
        {
            ValorAPagar = 0;
            ValorAPagar = ValorTotalDaVenda(); //Valor total da venda.
            foreach(double valor in ValoresInseridosNoPagamento)
            {
                ValorAPagar -= valor; //Valor total menos cada valor já pago antes.
            }
            return ValorAPagar;
        }

        //Valor total da venda, soma do valor de todos os produtos.
        public double ValorTotalDaVenda()
        {
            double sum = 0;
            foreach (Produto p in ProdutosVenda)
            {
                sum += p.ValorDoProduto;
            }
            return sum;
        }

        //Retorna verdadeiro para quando a venda foi paga completamente, e falso caso falta pagar. Conclusão da venda.
        public bool ConclusaoDaVenda(double pagamento)
        {
            ValoresInseridosNoPagamento.Add(pagamento);
            ValorFaltante = ValorAPagar - pagamento;
            if (ValorFaltante == 0)
            {
                HistoricoDeVendas.AdicionaVenda(this);
                return true;
            }
            else if (ValorFaltante < 0)
            {
                Troco = ValorFaltante * -1;
                HistoricoDeVendas.AdicionaVenda(this);
                return true;
            }
            else
            {
                return false;
            }
        }

        //Sobrescrita toString() retorna as informações da venda, código e valor total.
        public override string ToString()
        {
            return "Codigo venda: " + CodigoVenda
                + "\nValor total: R$ " + this.ValorTotalDaVenda().ToString("F2");
        }
    }
}
