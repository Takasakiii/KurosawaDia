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
        internal static async Task<string> GetPrefix(DiscordMessage msg)
        {
            if (!msg.Channel.IsPrivate)
            {
                Servidores s = await new ServidoresDAO().Get(new Servidores
                {
                    ID = msg.Channel.GuildId,
                }) ?? new Servidores();

                new Usuarios_ServidoresDAO().Add(new Servidores_Usuarios
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
                return s.Prefix ?? DefaultPrefix;
            }
            return DefaultPrefix;
        }
    }
}
