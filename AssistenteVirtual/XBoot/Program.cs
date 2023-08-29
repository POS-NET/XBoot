namespace AssistenteVirtual
{
    class Program
    {
        static void Main(string[] args)
        {
            string nomeUsuario = Saudacao();

            Console.WriteLine($"Olá, {nomeUsuario}!");
            Console.WriteLine("O que você deseja fazer? Digite o número da opção para continuar.\n");

            Acoes(nomeUsuario);
        }

        static string Saudacao()
        {
            DateTime horaAtual = DateTime.Now;
            string periodo = (horaAtual.Hour >= 6 && horaAtual.Hour < 12) ? "Bom dia!" :
                             (horaAtual.Hour >= 12 && horaAtual.Hour < 18) ? "Boa tarde!" :
                             "Boa noite!";

            Console.WriteLine($"{periodo} Sou um assistente virtual x-boot.");
            Console.WriteLine("Primeira coisa, como você se chama?");
            return Console.ReadLine();
        }

        static void Acoes(string nomeUsuario)
        {
            Console.WriteLine("\n1. Excluir todos os arquivos da pasta de download.");
            Console.WriteLine("2. Limpar lixeira.");
            string opcao = Console.ReadLine();
            Console.WriteLine();

            switch (opcao)
            {
                case "1":
                    ExcluirTodosArquivosDownload();
                    Console.WriteLine($"{nomeUsuario}, todos os arquivos da sua pasta de download foram deletados.");
                    Console.WriteLine("Qual é a próxima atividade que você quer executar?\n");
                    Acoes(nomeUsuario);
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }

        static void ExcluirTodosArquivosDownload()
        {
            string localPasta = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";

            if (System.IO.Directory.Exists(localPasta))
            {
                string[] arquivos = System.IO.Directory.GetFiles(localPasta);

                foreach (string arquivo in arquivos)
                {
                    System.IO.File.Delete(arquivo);
                }
            }
            else
            {
                Console.WriteLine("A pasta Downloads não foi encontrada.");
            }
        }
    }
}
