using Bot;
using Bot.Singletons;
using System;
using System.Threading;

namespace NetCoreGUI
{
    class Launcher
    {
        static void Main(string[] args)
        {
            if(args.Length == 0)
            {
                Console.Write("Digite o local da DB: ");
                SingletonConfig.localConfig = Console.ReadLine();
                new Thread(() => new Core().IniciarBot()).Start();
                Console.Write("\nO Bot foi iniciado");
            } else
            {
                SingletonConfig.localConfig = args[0];
                new Thread(() => new Core().IniciarBot()).Start();
                Console.Write("\nO Bot foi iniciado");
            }
        }
    }
}
