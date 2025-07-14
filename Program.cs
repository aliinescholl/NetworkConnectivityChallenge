using NetworkConnectivityChallenge;
using StringUtils;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine(new String('=', 82));
        StringUtil.CentralizarOpcao("Desafio de conectividade de rede");
        Console.WriteLine(new String('=', 82));
        int totalElementos;

        while (true)
        {
            Console.WriteLine("Digite a quantidade total de elementos: ");
            try
            {
                totalElementos = Convert.ToInt32(Console.ReadLine());
                break;
            }
            catch (FormatException)
            {
                Console.WriteLine("Valor inválido. Digite um número inteiro.");
            }
        }

        Network network = new Network(totalElementos);

        while (true)
        {
            Console.WriteLine(new String('=', 82));
            StringUtil.CentralizarOpcao("MENU");
            Console.WriteLine(new String('-', 82));
            Console.WriteLine("| 1 - Adicionar conexão".PadRight(81) + "|");
            Console.WriteLine("| 2 - Verificar conexão entre dois elementos".PadRight(81) + "|");
            Console.WriteLine("| 0 - Sair".PadRight(81) + "|");
            Console.WriteLine(new String('=', 82));
            Console.WriteLine("Escolha uma opção: ");
            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    network.Connect();
                    break;

                case "2":
                    Console.WriteLine(network.Query() ? "Conectados" : "Não conectados");
                    break;

                case "0":
                    Console.WriteLine("Encerrando o programa.");
                    return;

                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }
    }
}
