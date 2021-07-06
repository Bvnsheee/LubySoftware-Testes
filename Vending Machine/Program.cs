using System;
using System.Globalization;

namespace Vending_Machine
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * Alguns métodos de remover produtos e buscar produtos, como também as vendas, foram feitos para buscar pelo código por ser um aplicativo por console.
             * Algumas funções foram criadas, mas não foram implementadas por não terem sido pedidas na tarefa, como a de buscar e a de remover por exemplo.
             * Três produtos já foram adicionados ao estoque para teste, mas há como criar novos produtos.
             * 
             * --INFORMAÇÕES DE COMO UTILIZAR--
             * 
             * Existem 2 modos, operário e cliente, para acessar cada modo, estará na frente [] entre colchetes o valor chave de acesso, é só digitar o numero correspondente
             * 
             * operário cria novos produtos no sistema, verifica o estoque e visualiza o histórico de vendas(como também deveria remover e procurar produtos existentes, funções não implementadas)
             * 
             * para continuar com as operações aparecerá uma pergunta se deseja continuar [s/n] s para sim e n para não, mas qualquer outro valor diferente de S ou s será lido como não.
             * 
             * cliente pode fazer compras e visualizar o estoque.
             * 
             * As compras são feitas pelo código do produto, produto 1 = coca, então antes de comprar visualize os produtos ou o estoque para saber seus respectivos códigos.
             * 
             * OBSERVAÇÃO: Os valores de entrada que são doubles devem vir seguidos de ponto, por exemplo 2.5 para preço, estou usando CultureInfo.InvarianteCulture,
             *             no entanto os dados de saída estão com vírgula.
             * 
             */

            Estoque estoque = new Estoque();
            HistoricoDeVendas historicoVendas = new HistoricoDeVendas();
            Produto coca = new Produto("Coca", 3.5, 2);
            Produto pepsi = new Produto("Pepsi", 2.9, 3);
            Produto guarana = new Produto("Guarana", 2.4, 5);
            estoque.AdicionaProduto(coca);
            estoque.AdicionaProduto(pepsi);
            estoque.AdicionaProduto(guarana);

            int opcao;
            char resposta;

            bool programa = true;
            while (programa)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("Modo Operario[0]");
                    Console.WriteLine("Modo Cliente[1]");
                    opcao = int.Parse(Console.ReadLine());

                    if (opcao == 0)
                    {
                        bool modoOperario = true;
                        while (modoOperario)
                        {
                            try
                            {
                                Console.Clear();
                                Console.WriteLine("CONTROLE DE ESTOQUE");
                                Console.WriteLine("Novo produto[0]");
                                Console.WriteLine("Visualizar estoque[1]");
                                Console.WriteLine("Visualizar histórico de vendas[2]");

                                opcao = int.Parse(Console.ReadLine());
                                if (opcao == 0)
                                {
                                    bool cadastrandoProduto = true;
                                    while (cadastrandoProduto)
                                    {
                                        try
                                        {
                                            Console.Clear();
                                            Console.WriteLine("NOVO PRODUTO");
                                            Console.Write("Nome: ");
                                            string nome = Console.ReadLine();
                                            Console.Write("Preço: ");
                                            double preco = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                                            Console.Write("Quantidade: ");
                                            int quantidade = int.Parse(Console.ReadLine());
                                            if (estoque.AdicionaProduto(new Produto(nome, preco, quantidade)))
                                            {
                                                Console.WriteLine("Produto CADASTRADO");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Produto JÁ EXISTENTE");
                                            }

                                            Console.WriteLine("----------------------------------");
                                            Console.WriteLine("Continuar cadastrando produtos? [s/n] ...");
                                            resposta = char.Parse(Console.ReadLine());
                                            if (resposta.ToString().ToLower() != "s")
                                            {
                                                cadastrandoProduto = false;
                                            }
                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine(e.Message);
                                            Console.WriteLine("----------------------------------");
                                            Console.WriteLine("Erro - Cadastro NÃO feito");
                                            Console.WriteLine("Pressione qualquer botão para continuar...");
                                            Console.ReadLine();
                                        }
                                    }
                                }
                                if (opcao == 1)
                                {
                                    Console.Clear();
                                    estoque.VisualizarEstoque();
                                    Console.WriteLine("----------------------------------");
                                    Console.WriteLine("Pressione qualquer botão para continuar...");
                                    Console.ReadLine();
                                }
                                if (opcao == 2)
                                {
                                    Console.Clear();
                                    historicoVendas.VisualizarVendas();
                                    Console.WriteLine("----------------------------------");
                                    Console.WriteLine("Pressione qualquer botão para continuar...");
                                    Console.ReadLine();
                                }

                                Console.WriteLine("Continuar no Modo Operário? [s/n]");
                                resposta = char.Parse(Console.ReadLine());
                                if (resposta.ToString().ToLower() != "s")
                                {
                                    modoOperario = false;
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }
                    }

                    if (opcao == 1)
                    {
                        Console.Clear();
                        bool modoCliente = true;
                        while (modoCliente)
                        {
                            try
                            {
                                Console.Clear();
                                Console.WriteLine("Comprar produto[0]");
                                Console.WriteLine("Visualizar produtos[1]");
                                opcao = int.Parse(Console.ReadLine());
                                if (opcao == 0)
                                {
                                    bool comprando = true;
                                    Venda venda = new Venda(estoque, historicoVendas);
                                    while (comprando)
                                    {
                                        try
                                        {
                                            Console.Clear();
                                            Console.Write("Codigo produto: ");
                                            int codigo = int.Parse(Console.ReadLine());
                                            if (venda.AdicionarProdutoNaVenda(codigo))
                                            {
                                                Console.WriteLine("VENDIDO");
                                            }
                                            else
                                            {
                                                Console.WriteLine("FORA DE ESTOQUE");
                                            }
                                            Console.WriteLine("Continuar comprando? [s/n]");
                                            resposta = char.Parse(Console.ReadLine());
                                            if (resposta.ToString().ToLower() != "s")
                                            {
                                                bool pagando = true;
                                                while (pagando)
                                                {
                                                    try
                                                    {
                                                        Console.Clear();
                                                        venda.VisualizarProdutosNaVenda();
                                                        Console.WriteLine("----------------------------------");
                                                        Console.WriteLine("Valor total: R$ " + venda.ValoresAPagar().ToString("F2"));
                                                        Console.Write("Pagamento: R$ ");
                                                        double pagamento = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                                                        if (venda.ConclusaoDaVenda(pagamento))
                                                        {
                                                            Console.WriteLine("Venda concluída, troco: R$ " + venda.Troco.ToString("F2"));
                                                            Console.WriteLine("Pressione qualquer botão para continuar...");
                                                            Console.ReadLine();
                                                            pagando = false;
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("Falta R$ " + venda.ValorFaltante.ToString("F2"));
                                                            Console.WriteLine("----------------------------------");
                                                            Console.WriteLine("Pressione qualquer botão para continuar...");
                                                            Console.ReadLine();
                                                        }
                                                    }
                                                    catch (Exception e)
                                                    {
                                                        Console.WriteLine(e.Message);
                                                        Console.WriteLine("----------------------------------");
                                                        Console.WriteLine("Erro - Pagamento NÃO foi concluido");
                                                        Console.WriteLine("Pressione qualquer botão para continuar...");
                                                        Console.ReadLine();

                                                    }
                                                }
                                                comprando = false;
                                            }
                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine(e.Message);
                                             Console.WriteLine("----------------------------------");
                                            Console.WriteLine("Erro - Venda NÃO foi concluida");
                                            Console.WriteLine("Pressione qualquer botão para continuar...");
                                            Console.ReadLine();

                                        }
                                    }
                                }
                                if (opcao == 1)
                                {
                                    Console.Clear();
                                    estoque.VisualizarEstoque();
                                    Console.WriteLine("Pressione qualquer botao para continuar...");
                                    Console.ReadLine();
                                }


                                Console.WriteLine("Continuar no Modo Cliente? [s/n]");
                                resposta = char.Parse(Console.ReadLine());
                                if (resposta.ToString().ToLower() != "s")
                                {
                                    modoCliente = false;
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

        }

    }
}
