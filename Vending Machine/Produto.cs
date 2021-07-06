using System;
using System.Globalization;
using System.Collections.Generic;
using System.Text;

namespace Vending_Machine
{
    class Produto
    {
        public static int Auto_Incremento {get; private set;}
        public int CodigoProduto { get; private set; }
        public string NomeDoProduto { get; private set; }
        public double ValorDoProduto { get; private set; }
        public int QuantidadeEstoque { get; private set; }

        public Produto(string nomeDoProduto, double valorDoProduto, int quantidadeEstoque)
        {
            this.NomeDoProduto = nomeDoProduto;
            this.ValorDoProduto = valorDoProduto;
            this.QuantidadeEstoque = quantidadeEstoque;
            this.CodigoProduto = NovoCodigo();
        }

        //Método para alterar a quantidade dos produtos no estoque, podendo aumentar ou diminuir, não foi implementado no programa.
        public int AlteraQuantidadeEstoque(int quantidade)
        {
            return QuantidadeEstoque += quantidade;
        }


        //Retorna o código do produto, utilizado para reconhecer o produto.
        public int GetCodigoProduto()
        {
            return CodigoProduto;
        }

        //Método privado da classe, apenas gera um código sequencial para os produtos criados.
        private int NovoCodigo()
        {
            Auto_Incremento++;
            return CodigoProduto + Auto_Incremento;
        }

        //Sobrescrita toString retorna as informações do produto.
        public override string ToString()
        {
            return "Codigo Produto: " + CodigoProduto
                + "\n"
                + "Produto: "  + NomeDoProduto 
                + "\n"
                + "Preço: R$" + ValorDoProduto.ToString("F2", CultureInfo.InvariantCulture)
                + "\n"
                + "Estoque: " + QuantidadeEstoque;
        }
    }
}
