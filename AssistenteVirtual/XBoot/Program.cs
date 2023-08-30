using System.Diagnostics;

namespace AssistenteVirtual
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string nomeUsuario = Saudacao();

            Console.WriteLine($"\nOlá, {nomeUsuario}!");
            Console.WriteLine("Qual ação você deseja executar? Por favor, insira o número da opção desejada para prosseguir..\n");

            Acoes(nomeUsuario);
        }

        private static string Saudacao()
        {
            DateTime horaAtual = DateTime.Now;
            string periodo = (horaAtual.Hour >= 6 && horaAtual.Hour < 12) ? "Bom dia!" :
                             (horaAtual.Hour >= 12 && horaAtual.Hour < 18) ? "Boa tarde!" :
                             "Boa noite!";

            Console.WriteLine($"{periodo} Sou um assistente virtual x-boot.");
            Console.WriteLine("Primeira coisa, como você se chama?");
            return Console.ReadLine();
        }

        private static void Acoes(string nomeUsuario)
        {
            Console.WriteLine("\n1. Excluir todos os arquivos da pasta de download.");
            Console.WriteLine("\n2. Excluir todas as pastas da pasta de download.");
            Console.WriteLine("\n3. Abrir Spotify.");
            string opcao = Console.ReadLine();
            Console.WriteLine();

            switch (opcao)
            {
                case "1":
                    ExcluirTodosArquivosDownload();
                    break;

                case "2":
                    ExcluirTodasPastasDownload();
                    break;

                case "3":
                    AbrirSpotify();
                    break;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }

            Console.WriteLine("Qual é a próxima atividade que você quer executar?\n");
            Acoes(nomeUsuario);
        }


        private static void ExcluirTodosArquivosDownload()
        {
            string localPasta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");

            try
            {
                if (!Directory.Exists(localPasta))
                {
                    Console.WriteLine("A pasta Downloads não foi encontrada.");
                    return;
                }

                string[] arquivos = Directory.GetFiles(localPasta);

                if (arquivos.Length == 0)
                {
                    Console.WriteLine("A pasta não possui arquivos.");
                    return;
                }

                Console.WriteLine($"Deseja excluir {arquivos.Length} arquivos da pasta Downloads? (S/N)");
                string resposta = Console.ReadLine();

                if (resposta?.Trim().ToUpper() == "S")
                {
                    foreach (string arquivo in arquivos)
                    {
                        File.Delete(arquivo);
                    }

                    Console.WriteLine($"{arquivos.Length} arquivos excluídos da pasta Downloads.");
                }
                else
                {
                    Console.WriteLine("Operação cancelada pelo usuário.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro: {ex.Message}");
            }
        }

        private static void ExcluirTodasPastasDownload()
        {
            string localPasta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");

            try
            {
                if (!Directory.Exists(localPasta))
                {
                    Console.WriteLine("A pasta Downloads não foi encontrada.");
                    return;
                }

                string[] pastas = Directory.GetDirectories(localPasta);

                if (pastas.Length == 0)
                {
                    Console.WriteLine("A pasta não possui pastas.");
                    return;
                }

                Console.WriteLine($"Deseja excluir {pastas.Length} pastas da pasta Downloads? (S/N)");
                string resposta = Console.ReadLine();

                if (resposta?.Trim().ToUpper() == "S")
                {
                    foreach (string pasta in pastas)
                    {
                        ExcluirPastaRecursivamente(pasta);
                    }

                    Console.WriteLine($"{pastas.Length} pastas excluídas da pasta Downloads.");
                }
                else
                {
                    Console.WriteLine("Operação cancelada pelo usuário.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro: {ex.Message}");
            }
        }

        private static void ExcluirPastaRecursivamente(string pasta)
        {
            foreach (string arquivo in Directory.GetFiles(pasta))
            {
                File.Delete(arquivo);
            }

            foreach (string subPasta in Directory.GetDirectories(pasta))
            {
                ExcluirPastaRecursivamente(subPasta);
            }

            Directory.Delete(pasta);
        }

        private static void AbrirSpotify()
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "spotify",
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Não foi possível abrir o Spotify: {ex.Message}");
            }
        }

    }
}