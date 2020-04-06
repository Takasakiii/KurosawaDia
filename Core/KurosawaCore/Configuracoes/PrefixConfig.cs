using DataBaseController.DAOs;
using DataBaseController.Modelos;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KurosawaCore.Configuracoes
{
    internal sealed class PrefixConfig
    {
        internal async Task<int> PegarPrefixo(DiscordMessage msg)
        {
            await new Usuarios_ServidoresDAO().Add(new Servidores_Usuarios
            {
                Servidor = new Servidores
                {
                    ID = msg.Channel.GuildId,
                    Nome = msg.Channel.Guild.Name
                },
                Usuario = new Usuarios
                {
                    ID = msg.Author.Id,
                    Nome = $"{msg.Author.Username}#{msg.Author.Discriminator}"
                }
            });
            return 0;
        }
    }
}
