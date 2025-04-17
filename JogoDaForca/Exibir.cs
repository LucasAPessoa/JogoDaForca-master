using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using JogoDoMeEnforca;
using Player;
using Helper;
using System.IO;

namespace Exibir
{

    public class TelaInicial
    {
        public static void Menu()
        {
            string titulo = @"
 ███████╗░█████╗░██████╗░░█████╗░░█████╗░
 ██╔════╝██╔══██╗██╔══██╗██╔══██╗██╔══██╗
 █████╗░░██║░░██║██████╔╝██║░░╚═╝███████║
 ██╔══╝░░██║░░██║██╔══██╗██║░░██╗██╔══██║
 ██║░░░░░╚█████╔╝██║░░██║╚█████╔╝██║░░██║
╚═╝░░░░░░╚════╝░╚═╝░░╚═╝░╚════╝░╚═╝░░╚═╝";

          
            bool continuar = true;
            while (continuar)
            {
                Console.Clear();
                    LoopingPlayer.StartLoop("C:\\Users\\paulo\\Desktop\\JogoDaForca-master\\JogoDaForca\\Audio\\Menu Mario.mp3");
                ConsoleWriteCenter(titulo);
                Console.WriteLine();

                ExibirOpcoesMenu();

                
                Console.Write("Digite o número da opção: ");
                string opcao = Console.ReadLine();

                string resultado = MenuHelper.ProcessarOpcao(opcao);

                switch (resultado)
                {
                    case "Facil":
                        LoopingPlayer.StopLoop();
                        Jogo.SelecionarDificuldade(1);
                        Thread.Sleep(3000);
                        break;
                    case "Medio":
                        LoopingPlayer.StopLoop();
                        Jogo.SelecionarDificuldade(2);
                        Thread.Sleep(3000);
                        break;
                    case "Dificil":
                        LoopingPlayer.StopLoop();
                        Jogo.SelecionarDificuldade(3);
                        Thread.Sleep(3000);
                        break;
                    case "Sair":
                        Console.Clear();
                        LoopingPlayer.StopLoop();
                        Console.WriteLine("Saindo...");
                        continuar = false;
                        break;
                    case "ErroVazio":
                        Console.WriteLine("Você precisa digitar uma opção!");
                        Thread.Sleep(1000);
                        break;
                    case "ErroInvalido":
                        Console.WriteLine("Opção inválida! Tente novamente.");
                        Thread.Sleep(1000);
                        break;
                }


            }
        }


        public static void ConsoleWriteCenter(string texto)
        {
            string[] linhas = texto.Split("\n", StringSplitOptions.None);

            foreach (var linha in linhas)
            {
                int larguraConsole = Console.WindowWidth;
                int espacos = (larguraConsole - linha.Length) / 2;
                string textoCentralizado = new string(' ', Math.Max(0, espacos)) + linha;
                Console.WriteLine(textoCentralizado);
            }
        }

        public static void ExibirOpcoesMenu()
        {
            string[] opcoesMenu = {
            "1. Fácil",
            "2. Médio",
            "3. Difícil",
            "4. Sair"
        };

            
            int alinhamento = 54;  

            foreach (var opcao in opcoesMenu)
            {
                
                Console.WriteLine(new string(' ', alinhamento) + opcao);
            }
        }
        public static void ExibirAlinhadoForca(string mensagem)
        {
            int alinhamento = 40;
            Console.WriteLine(new string(' ', alinhamento) + mensagem);
        }
       
    }
}
