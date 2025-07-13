using System.Xml.Linq;

namespace NetworkConnectivityChallenge
{
    public class Network
    {
        // usado array de tuplas aqui para poder definir um tamanho máximo do array
        // https://stackoverflow.com/questions/20490884/how-to-create-an-array-of-tuples
        public Tuple<int, int>[] Elements { get; set; }      

        public Network(int numberOfElements)
        {
            Elements = new Tuple<int, int>[numberOfElements];
        }

        public void Connect(int i)
        {
            Console.WriteLine(String.Concat(i + 1, "ª conexão"));
            
            Elements[i] = GetTuplaByInput();
        }

        public bool Query()
        {
            Tuple<int, int> elementoNovo = GetTuplaByInput();

            bool achouConexao = false;

            while (!achouConexao)
            {
                foreach (Tuple<int, int> element in Elements)
                {
                    //ligação direta
                    if (elementoNovo == element || (elementoNovo.Item2 == element.Item1 && elementoNovo.Item2 == element.Item1))
                        achouConexao = true;
                }

                // filtro pra buscar se algum dos números da tupla recebida está na lista de conexões
                Tuple<int, int>[] variavel = Elements.Where(e => elementoNovo.Item2 == e.Item1 || elementoNovo.Item2 == e.Item1).ToArray();

                bool achouUmIgual = false;
                Tuple<int, int> seiSim = elementoNovo;

                Tuple<int, int> tupla = null;



                while (tupla != elementoNovo || (tupla.Item1 == elementoNovo.Item1 || tupla.Item2 == elementoNovo.Item2))
                {
                    for (int i = 0; i < Elements.Length; i++)
                    {
                        if (elementoNovo.Item1 == Elements[i].Item2 || elementoNovo.Item1 == Elements[i].Item1 || seiSim.Item2 == Elements[i].Item2)                        
                            seiSim = Elements[i];                                              
                    }

                    Console.WriteLine(seiSim.ToString());
                }
            }

            return false;
        }

        private static Tuple<int, int> GetTuplaByInput()
        {
            Console.WriteLine("Primeiro");
            int item1 = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Segundo");
            int item2 = Convert.ToInt32(Console.ReadLine());

            Tuple<int, int> elementoNovo = Tuple.Create(item1, item2);
            return elementoNovo;
        }
    }
}