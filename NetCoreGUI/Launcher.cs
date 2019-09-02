using Bot;
using Bot.Extensions;
using Bot.Singletons;
using System;
using System.Drawing;
using System.Threading;
using Console = Colorful.Console;

namespace NetCoreGUI
{
    class Launcher
    {
        static void Main(string[] args)
        {
            Console.WriteAscii("Kurosawa Dia <3", Color.DarkMagenta);
            new Thread(() => new Core().IniciarBot()).Start();
            LogEmiter.SetMetodoLog(new Launcher().Log);
            
        }

        public void Log(LogEmiter.TipoLog logType, string e)
        {
            System.Console.ForegroundColor = logType.CorNoConsole;
            Console.Write($"\n{e}");
        }
    }
}
