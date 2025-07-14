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
        private int NumberOfElements { get; set; }

        public Network(int numberOfElements)
        {
            NumberOfElements = numberOfElements;
            Connections = new List<Tuple<int, int>>();
        }

        public void Connect()
        {
            try
            {
                Console.WriteLine(new String('-', 82));
                StringUtil.CentralizarOpcao("Nova conex�o");
                Console.WriteLine(new String('-', 82));
                Connections.Add(GetTuplaByInput());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao registrar conex�o: {ex.Message}");
            }
        }

        public bool Query()
        {
            try
            {
                Console.WriteLine(new String('-', 82));
                StringUtil.CentralizarOpcao("Verificar conex�o");
                Console.WriteLine(new String('-', 82));

                // Obt�m a origem e o destino a partir da entrada do usu�rio.
                // A valida��o de entrada (se � n�mero, se est� no intervalo) deve ser feita em GetTuplaByInput.
                Tuple<int, int> consulta = GetTuplaByInput();
                int origem = consulta.Item1;
                int destino = consulta.Item2;

                // 'ligados' armazena todos os n�s alcan��veis a partir da 'origem'
                int[] ligados = new int[Connections.Count * 2];
                int contador = 0;
                ligados[contador++] = origem;

                // repete o processo de busca para espalhar a conex�o por n�s mais distantes.
                for (int rodada = 0; rodada < NumberOfElements; rodada++)
                {
                    // Guarda o n�mero de n�s conectados ANTES de iniciar a rodada.
                    int contadorAntesDaRodada = contador;

                    // Para cada conex�o direta na nossa rede
                    foreach (var conexao in Connections)
                    {
                        // para cada n� que j� sabemos que est� conectado
                        for (int j = 0; j < contador; j++)
                        {
                            int atual = ligados[j];

                            // Se um lado da 'conexao' � um n� 'atual' e o outro lado ainda n�o foi descoberto
                            if (conexao.Item1 == atual && !Contem(ligados, conexao.Item2, contador))
                                // adicionamos o novo n� � lista de 'ligados'.
                                ligados[contador++] = conexao.Item2;

                            if (conexao.Item2 == atual && !Contem(ligados, conexao.Item1, contador))
                                ligados[contador++] = conexao.Item1;
                        }
                    }

                    // Se o contador n�o mudou durante esta rodada inteira significa que n�o h� mais n�s a serem descobertos
                    if (contador == contadorAntesDaRodada)
                        break;  
                }

                // Ao final, verifica se o n� 'destino' foi encontrado na lista de n�s alcan��veis.
                return Contem(ligados, destino, contador);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro na consulta: {ex.Message}");
                return false;
            }
        }

        private static bool Contem(int[] array, int valor, int limite)
        {
            for (int i = 0; i < limite; i++)
            {
                if (array[i] == valor)
                    return true;
            }
            return false;
        }

        private static Tuple<int, int> GetTuplaByInput()
        {
            try
            {
                Console.Write("Primeiro n�mero: ");
                int item1 = Convert.ToInt32(Console.ReadLine());
                Console.Write("Segundo n�mero: ");
                int item2 = Convert.ToInt32(Console.ReadLine());

                return Tuple.Create(item1, item2);
            }
            catch (FormatException)
            {
                Console.WriteLine("Entrada inv�lida. Digite apenas n�meros inteiros.");
                return GetTuplaByInput();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao ler entrada: {ex.Message}");
                return Tuple.Create(0, 0);
            }
        }
    }
}
