namespace StringUtils
{
    public static class StringUtil
    {
        // https://learn.microsoft.com/pt-br/dotnet/csharp/programming-guide/classes-and-structs/constants
        private const int TAMANHO_LINHA =  80;

        // PadCenter string
        // https://stackoverflow.com/questions/17590528/pad-left-pad-right-pad-center-string
        public static string PadCenter(string source)
        {
            int spaces = TAMANHO_LINHA - source.Length;
            int padLeft = spaces / 2 + source.Length;
            return source.PadLeft(padLeft).PadRight(TAMANHO_LINHA);
        }

        public static void CentralizarOpcao(string source)
        {
            Console.WriteLine(String.Concat("|", StringUtil.PadCenter(source), "|"));
        }

    }
}