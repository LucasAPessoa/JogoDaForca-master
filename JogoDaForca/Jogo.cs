using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exibir;
using NAudio.Wave;

namespace JogoDoMeEnforca
{
    internal class Jogo
    {
        internal class PalavraComDica
        {
            public string Palavra { get; set; }
            public string Dica { get; set; }

            public PalavraComDica(string palavra, string dica)
            {
                Palavra = palavra;
                Dica = dica;
            }
        }


        public static List<PalavraComDica> palavrasFaceis { get; private set; } = new List<PalavraComDica>
                {
                    new PalavraComDica("calopsita", "ave"),
                    new PalavraComDica("cachorro", "animal"),
                    new PalavraComDica("banana", "fruta"),
                    new PalavraComDica("maça", "fruta"),
                    new PalavraComDica("olho", "parte do corpo"),
                    new PalavraComDica("azul", "cor"),
                    new PalavraComDica("matematica", "materia de exatas")
                };
        public static List<PalavraComDica> palavrasMedias { get; private set; } = new List<PalavraComDica>
                {
                    new PalavraComDica("psicologo", "profissao"),
                    new PalavraComDica("golf", "esporte"),
                    new PalavraComDica("pitaya", "fruta"),
                    new PalavraComDica("banoffe", "doce"),
                    new PalavraComDica("intestino", "parte do corpo"),
                    new PalavraComDica("paralelepipedo", "forma geometrica"),

                };

        public static List<PalavraComDica> palavrasDificeis { get; private set; } = new List<PalavraComDica>
                {
                   new PalavraComDica("pneumoultramicroscopicossilicovulcanoconiótico",""),
                   new PalavraComDica("otorrinolaringologista",""),
                   new PalavraComDica("inconstitucionalissimamente",""),
                   new PalavraComDica("HIPOPOTAMONSTROSESQUIPEDALIOFOBIA","")
                };




        public static void SelecionarDificuldade(int dificuldade)
        {

            switch (dificuldade)
            {

                case 1:
                    Console.WriteLine("Dificuldade fácil escolhida.");
                    EscolherPalavra(palavrasFaceis);
                    break;
                case 2:
                    Console.WriteLine("Dificuldade média escolhida.");
                    EscolherPalavra(palavrasMedias);
                    break;
                case 3:
                    Console.WriteLine("Dificuldade difícil escolhida.");
                    EscolherPalavra(palavrasDificeis);
                    break;
            }
        }



        public static void EscolherPalavra(List<PalavraComDica> palavras)
        {
            Random random = new Random();
            PalavraComDica palavraAtual = palavras[random.Next(palavras.Count)];
            string palavraSecreta = palavraAtual.Palavra.ToUpper();
            string dica = palavraAtual.Dica;

            Jogar(palavraSecreta, dica);
        }

        public static void Jogar(string PalavraSecreta, string Dica)
        {
            char[] letrasDescobertas = new char[PalavraSecreta.Length];
            List<char> letrasIncorretas = new List<char>();

            for (int i = 0; i < letrasDescobertas.Length; i++)
            {
                letrasDescobertas[i] = '_';
            }

            int tentativasRestantes = 6;
            bool jogoTerminado = false;

            while (!jogoTerminado)
            {
                Console.Clear();
                Console.WriteLine($"Dica: {(string.IsNullOrEmpty(Dica) ? "Sem dica disponível" : Dica)}\n");
                ExibirForca(tentativasRestantes);
                Console.WriteLine("\nPalavra: " + string.Join(" ", letrasDescobertas));
                Console.WriteLine("\nLetras incorretas: " + string.Join(", ", letrasIncorretas));
                Console.WriteLine($"Tentativas restantes: {tentativasRestantes}");

                Console.Write("\nDigite uma letra: ");

                char letra;


                while (true)
                {
                    string input = (Console.ReadLine() ?? "").Trim();

                    if (input.Length == 1 && char.IsLetter(input[0]))
                    {
                        letra = char.ToUpper(input[0]);
                        break;
                    }
                    else
                    {
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        Console.Write(new string(' ', Console.WindowWidth));
                        Console.SetCursorPosition(0, Console.CursorTop);
                        Console.Write("Entrada inválida. Tente novamente: ");
                    }
                }

                if (PalavraSecreta.Contains(letra))
                {
                    for (int i = 0; i < PalavraSecreta.Length; i++)
                    {
                        if (PalavraSecreta[i] == letra)
                        {
                            letrasDescobertas[i] = letra;
                        }
                    }

                    if (!new string(letrasDescobertas).Contains('_'))
                    {
                        // 
                        string caminho = @"Audio\Tema da Vitória.mp3";
                        jogoTerminado = true;
                        Console.Clear();
                        using (var audioFile = new AudioFileReader(caminho))
                        using (var outputDevice = new WaveOutEvent())
                        {
                            outputDevice.Init(audioFile);
                            outputDevice.Play();

                            TelaInicial.ConsoleWriteCenter("\r\n █▀█ ▄▀█ █▀█ ▄▀█ █▄▄ █▀▀ █▄░█ █▀    █░█ █▀█ █▀▀ █▀▀   █▀▀ ▄▀█ █▄░█ █░█ █▀█ █░█ █\r\n█▀▀ █▀█ █▀▄ █▀█ █▄█ ██▄ █░▀█ ▄█    ▀▄▀ █▄█ █▄▄ ██▄   █▄█ █▀█ █░▀█ █▀█ █▄█ █▄█ ▄");
                            Console.WriteLine($"\nA palavra era: {PalavraSecreta}");
                            Console.WriteLine();
                            Console.WriteLine("\nPressione qualquer tecla para sair...");
                            Console.ReadKey();
                            outputDevice.Stop();
                        }

                    }
                }
                else
                {
                    if (!letrasIncorretas.Contains(letra))
                    {
                        letrasIncorretas.Add(letra);
                        tentativasRestantes--;
                    }

                    if (tentativasRestantes == 0)
                    {
                        string caminho = @"Audio\Mario game over song.mp3";
                        jogoTerminado = true;
                        Console.Clear();
                        using (var audioFile = new AudioFileReader(caminho))
                        using (var outputDevice = new WaveOutEvent())
                        {
                            outputDevice.Init(audioFile);
                            outputDevice.Play();
                            TelaInicial.ConsoleWriteCenter(@"
░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
░█▀▀ ░█▀█ ░█ ░█▀▀ ░░█▀▀ ░█▀█░█ ░█░░░░
░█▀▀ ░█▀▀ ░█ ░█ ░░░░█▀▀ ░█▀█░█ ░█ ░░░░
░▀▀▀ ░▀ ░░░▀ ░▀▀▀ ░░▀░░ ░▀░▀░▀ ░▀▀▀░░
░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░");

                            Console.WriteLine($"\nA palavra era: {PalavraSecreta}");
                            Console.WriteLine();
                            Console.WriteLine("\nPressione qualquer tecla para sair...");
                            Console.ReadKey();
                            outputDevice.Stop();
                        }
                    }
                }
            }
        }
        


        public static void ExibirForca(int tentativasRestantes)
        {
            switch (tentativasRestantes)
            {
                case 0:
                    Console.WriteLine("  =========  ");
                    Console.WriteLine(" ||       | ");
                    Console.WriteLine(" ||       O ");
                    Console.WriteLine(" ||      /|\\");
                    Console.WriteLine(" ||      / \\");
                    Console.WriteLine(" ||_________");
                    break;
                case 1:
                    Console.WriteLine("  ========  ");
                    Console.WriteLine(" ||       | ");
                    Console.WriteLine(" ||       O ");
                    Console.WriteLine(" ||      /|\\");
                    Console.WriteLine(" ||      /  ");
                    Console.WriteLine(" ||_________");
                    break;
                case 2:
                    Console.WriteLine("  ========  ");
                    Console.WriteLine(" ||       | ");
                    Console.WriteLine(" ||       O ");
                    Console.WriteLine(" ||      /|\\");
                    Console.WriteLine(" ||         ");
                    Console.WriteLine(" ||_________");
                    break;
                case 3:
                    Console.WriteLine("  ========  ");
                    Console.WriteLine(" ||       | ");
                    Console.WriteLine(" ||       O ");
                    Console.WriteLine(" ||      /| ");
                    Console.WriteLine(" ||         ");
                    Console.WriteLine(" ||_________");
                    break;
                case 4:
                    Console.WriteLine("  ========  ");
                    Console.WriteLine(" ||       | ");
                    Console.WriteLine(" ||       O ");
                    Console.WriteLine(" ||       | ");
                    Console.WriteLine(" ||         ");
                    Console.WriteLine(" ||_________");
                    break;
                case 5:
                    Console.WriteLine("  ========  ");
                    Console.WriteLine(" ||       | ");
                    Console.WriteLine(" ||       O ");
                    Console.WriteLine(" ||         ");
                    Console.WriteLine(" ||         ");
                    Console.WriteLine(" ||_________");
                    break;
                case 6:
                    Console.WriteLine("  ========  ");
                    Console.WriteLine(" ||       | ");
                    Console.WriteLine(" ||         ");
                    Console.WriteLine(" ||         ");
                    Console.WriteLine(" ||         ");
                    Console.WriteLine(" ||_________");
                    break;
            }

        }




    } 
}

