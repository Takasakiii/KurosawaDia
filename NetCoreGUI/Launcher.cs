using Bot;
using Bot.Extensions;
using Bot.Singletons;
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using Console = Colorful.Console;

namespace NetCoreGUI
{
    class Launcher
    {
        static void Main(string[] args)
        {
            Console.Write("", Color.White);
            Console.WriteAscii("Kurosawa Dia <3", Color.DarkMagenta);
            Task.Run(async () =>
            {
                await new Core().CriarClienteAsync();
            });
            
            LogEmiter.SetMetodoLog(new Launcher().Log);
            
        }

        public void Log(LogEmiter.TipoLog logType, string e)
        {
            System.Console.ForegroundColor = logType.CorNoConsole;
            Console.Write($"\n{e}");
        }
    }
}
