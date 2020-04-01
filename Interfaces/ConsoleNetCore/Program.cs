using ConfigController.DAOs;
using ConfigController.EntityConfiguration;
using ConfigController.Models;
using KurosawaCore;
using System;
using System.Drawing;
using System.Threading.Tasks;
using AConsole = Colorful.Console;

namespace ConsoleNetCore
{
    public class Program
    {
        private BaseConfig Config { get; set; }

        public static async Task Main(string[] args)
        {
            Program programa = new Program();
            await programa.LigarBot();
        }

        private async Task CriarDB()
        {

            if (await new KurosawaConfigContext().Database.EnsureCreatedAsync())
            {
                Config = new BaseConfig();
                Console.Write("Token Bot: ");
                Config.Token = Console.ReadLine();
                Console.Write("ID Dono: ");
                Config.IdDono = ulong.Parse(Console.ReadLine());
                Console.Write("Prefixo: ");
                Config.Prefixo = Console.ReadLine();
                await new BaseConfigDAO().Adicionar(Config);
            }
            else
            {
                Config = await new BaseConfigDAO().Ler();
            }
        }

        private async Task LigarBot()
        {
            AConsole.WriteAscii("Kurosawa Dia <3", Color.DarkMagenta);
            await CriarDB();
            Kurosawa kud = new Kurosawa(Config);
            await kud.Iniciar();
        }
    }
}
