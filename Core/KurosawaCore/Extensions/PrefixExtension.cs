using DataBaseController.DAOs;
using DataBaseController.Modelos;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KurosawaCore.Extensions
{
    internal class PrefixExtension
    {

        internal static string DefaultPrefix { get; set; }
        internal Servidores Servidor { get; private set; }
        private static List<Servidores> Buffer;

        static PrefixExtension()
        {
            Buffer = new List<Servidores>();
        }

        private PrefixExtension() { }

        internal static async Task<string> GetPrefix(DiscordMessage msg)
        {
            if (!msg.Channel.IsPrivate)
            {
                Servidores servidores = await new Usuarios_ServidoresDAO().Add(new Servidores_Usuarios
                {
                    Servidor = new Servidores
                    {
                        ID = msg.Channel.GuildId,
                        Nome = msg.Channel.Guild.Name
                    },
                    Usuario = new Usuarios
                    {
                        ID = msg.Author.Id,
                        Nome = $"{msg.Author.Username} #{msg.Author.Discriminator}"
                    }
                });

                Buffer.Add(servidores);
                return servidores.Prefix ?? DefaultPrefix;
            }
            return DefaultPrefix;
        }

        internal static Servidores GetServidor(DiscordGuild guild)
        {
            Servidores achado = Buffer.Find(x => x.ID == guild.Id);
            Buffer.Remove(achado);
            return achado;
        }
    }
}
