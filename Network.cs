using System;
using System.Collections.Generic;
using System.Xml.Linq;
using StringUtils;

namespace NetworkConnectivityChallenge
{
    public class Network
    {
        // usar Tuple<List<int>, List<int>> ou List<Tuple<int, int>>?
        // https://stackoverflow.com/questions/4349635/tuplelistint-listint-or-a-listtupleint-int
        public List<Tuple<int, int>> Connections { get; set; }
        private int QuantidadeElementos { get; set; }

        public Network(int quantidadeElementos)
        {
            QuantidadeElementos = quantidadeElementos;
            Connections = new List<Tuple<int, int>>();
        }

        public void Connect()
        {
            try
            {
                StringUtil.Message("Nova conexão");
                Connections.Add(GetTuplaByInput());
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Concat("Erro ao registrar conexão" + ex.Message));
            }
        }

        public bool Query()
        {
            try
            {
                StringUtil.Message("Verificar conexão");

                // Obtém a origem e o destino a partir da entrada do usuário.
                Tuple<int, int> consulta = GetTuplaByInput();
                int origem = consulta.Item1;
                int destino = consulta.Item2;

                // 'ligados' armazena todos os nós alcançáveis a partir da 'origem'
                int[] ligados = new int[Connections.Count * 2];
                int contador = 0;
                ligados[0] = origem;

                // repete o processo de busca para espalhar a conexão por nós mais distantes.
                for (int rodada = 0; rodada < QuantidadeElementos; rodada++)
                {
                    // Guarda o número de nós conectados ANTES de iniciar a rodada.
                    int contadorAntesDaRodada = contador;

                    // Para cada conexão direta na nossa rede
                    foreach (Tuple<int, int> conexao in Connections)
                    {
                        // para cada nó que já sabemos que está conectado
                        for (int indiceNoAtual = 0; indiceNoAtual < contador; indiceNoAtual++)
                        {
                            int atual = ligados[indiceNoAtual];

                            // Se um lado da 'conexao' é um nó 'atual' e o outro lado ainda não foi descoberto
                            if (conexao.Item1 == atual && !Contem(ligados, conexao.Item2, contador))
                                // adicionamos o novo nó à lista de 'ligados'.
                                ligados[contador++] = conexao.Item2;

                            if (conexao.Item2 == atual && !Contem(ligados, conexao.Item1, contador))
                                ligados[contador++] = conexao.Item1;
                        }
                    }

                    // Se o contador não mudou durante esta rodada inteira significa que não há mais nós a serem descobertos
                    if (contador == contadorAntesDaRodada)
                        break;  
                }

                // Ao final, verifica se o nó 'destino' foi encontrado na lista de nós alcançáveis.
                return Contem(ligados, destino, contador);
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Concat("Erro na consulta" + ex.Message));
                return false;
            }
        }

        private static bool Contem(int[] array, int valor, int limite)
        {
            for (int indice = 0; indice < limite; indice++)
            {
                if (array[indice] == valor)
                    return true;
            }
            return false;
        }

        private static Tuple<int, int> GetTuplaByInput()
        {
            try
            {
                Console.Write("Primeiro número: ");
                int item1 = Convert.ToInt32(Console.ReadLine());
                Console.Write("Segundo número: ");
                int item2 = Convert.ToInt32(Console.ReadLine());

                return Tuple.Create(item1, item2);
            }
            catch (FormatException)
            {
                Console.WriteLine("Entrada inválida. Digite apenas números inteiros.");
                return GetTuplaByInput();
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Concat("Erro ao ler entrada" + ex.Message));
                return Tuple.Create(0, 0);
            }
        }
    }
}
