using JogoDoMeEnforca;
using Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public class MenuHelper
    {
        public static string ProcessarOpcao(string opcao)
        {
            if (string.IsNullOrWhiteSpace(opcao))
                return "ErroVazio";
            switch (opcao)
            {
                case "1":
                    LoopingPlayer.StopLoop();
                    Jogo.SelecionarDificuldade(1);
                    Thread.Sleep(3000);
                    return "1";
                    break;
                case "2":
                    LoopingPlayer.StopLoop();
                    Jogo.SelecionarDificuldade(2);
                    Thread.Sleep(3000);
                    return "2";
                    break;
                case "3":
                    LoopingPlayer.StopLoop();
                    Jogo.SelecionarDificuldade(3);
                    Thread.Sleep(3000);
                    return "3";
                    break;
                case "4":
                    Console.Clear();
                    LoopingPlayer.StopLoop();
                    Console.WriteLine("Saindo...");
                    return "Sair";
                default:
                    return "ErroInvalido";
            }
        }
    }
}
