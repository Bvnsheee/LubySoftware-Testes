using System;
using System.Collections.Generic;
using System.Text;

namespace Vending_Machine
{
    class HistoricoDeVendas
    {
        private List<Venda> Vendas = new List<Venda>();

        //Retorna a lista de vendas feitas.
        public List<Venda> GetVendas()
        {
            return Vendas;
        }

        //Visualiza as vendas caso haja venda, imprime o toString() de cada venda na lista vendas.
        public void VisualizarVendas()
        {
            if (!(Vendas.Count == 0))
            {
                foreach (Venda v in Vendas)
                {
                    Console.WriteLine(v);
                }
            }
            else
            {
                Console.WriteLine("Nenhuma venda cadastrada");
            }
        }

        //Adiciona vendas a lista vendas.
        public void AdicionaVenda(Venda venda)
        {
            Vendas.Add(venda);
        }

        //Remove venda da lista atráves do código.
        public bool RemoveVenda(int codigo)
        {
            foreach (Venda v in Vendas)
            {
                if (v.CodigoVenda == codigo)
                {
                    Vendas.Remove(v);
                    return true;
                }
            }
            return false;
        }

        //Busca uma venda já feita pelo código no histórico.
        public bool BuscarVenda(int codigo)
        {
            foreach (Venda v in Vendas)
            {
                if (v.CodigoVenda == codigo)
                {
                    return true;
                }
            }
            return false;
        }

        //Retorna o valor total das vendas realizadas no histórico.
        public double ValorTotalDasVendasRealizadas()
        {
            double sum = 0;
            foreach (Venda v in Vendas)
            {
                sum += v.ValorTotalDaVenda();
            }
            return sum;
        }
    }
}
