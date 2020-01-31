using BlazorGUI.Core.Extensions;
using Bot.Extensions;
using ConfigurationControler.Factory;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;

namespace BlazorGUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            LogEmiter.SetMetodoLog(new LogHub().Log);

            if (ConnectionFactory.VerificarDB())
            {
                new Thread(async () =>
                {
                    Bot.Core core = new Bot.Core();
                    await core.CriarClienteAsync();
                }).Start();

                CreateHostBuilder(args).Build().Run();
            }
            else
            {
                Console.WriteLine("Meu caro a configDia está ausente :(");
            }


        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
