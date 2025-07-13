using NetworkConnectivityChallenge;

Console.WriteLine("Tamanho da network que será criada");
int size = Convert.ToInt32(Console.ReadLine());

Network network = new Network(size);

Console.WriteLine("Definir conexões");

for (int i = 0; i < network.Elements.Length; i++)
{   
    network.Connect(i);
}

while (true)
{
    network.Query();
}