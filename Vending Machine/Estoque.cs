using System;
using System.Collections.Generic;
using System.Text;

namespace Vending_Machine
{
    class Estoque
    {
        private List<Produto> Produtos = new List<Produto>();

        //Retorn a lista de produtos do estoque.
        public List<Produto> GetProdutos()
        {
            return Produtos;
        }

        //Adiciona produtos no estoque quando são criados pelo programa.
        public bool AdicionaProduto(Produto produto)
        {
            if (Produtos.Count == 0)
            {
                Produtos.Add(produto);
                return true;
            }
            else
            {
                foreach(Produto p in Produtos)
                {
                    if(p.NomeDoProduto.ToLower() == produto.NomeDoProduto.ToLower())
                    {
                        return false;
                    }
                    else
                    {
                        Produtos.Add(produto);
                        return true;
                    }
                }
            }
            return false;
        }

        //Remove produtos do estoque atráves do código do produto, não foi implementado no programa.
        public bool RemoveProduto(int codigo)
        {
            foreach (Produto p in Produtos)
            {
                if (codigo == p.CodigoProduto)
                {
                    this.Produtos.Remove(p);
                    return true;
                }
            }
            return false;
        }

        //Busca um determinado produto no estoque, não foi implementado no programa.
        public bool BuscaProduto(Produto produto)
        {
            if (Produtos.Contains(produto))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Visualiza os produtos no estoque, imprime suas informações pelo toString() do produto, nome, quantidade e preco.
        public void VisualizarEstoque()
        {
            int sum = 0;
            foreach (Produto p in Produtos)
            {
                Console.WriteLine(p);
                Console.WriteLine("---------------------");
                sum += p.QuantidadeEstoque;
            }
            Console.WriteLine("Quantidade total de produtos: " + sum);
        }
    }
}
