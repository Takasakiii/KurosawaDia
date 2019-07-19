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
            new Thread(() => new Core().IniciarBot()).Start();
            SingletonLogs.SetInstance(new Launcher(), typeof(Launcher));
        }

        public void Log(string e)
        {
            Console.Write($"\n{e}");
        }
    }
}
