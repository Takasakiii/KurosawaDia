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
        internal string DefaultPrefix { get; private set; }
        internal PrefixConfig(string defaultprefix)
        {
            DefaultPrefix = defaultprefix;
        }

        internal async Task<int> PegarPrefixo(DiscordMessage msg)
        {
            return GetStringPrefixLength(msg, await GetPrefix(msg));
        }

        private async Task<string> GetPrefix(DiscordMessage msg)
        {
            Servidores s = await new ServidoresDAO().Get(new Servidores
            {
                ID = msg.Channel.GuildId,
            }) ?? new Servidores();

            if (string.IsNullOrEmpty(s.Nome))
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
                        Nome = $"{msg.Author.Username} #{msg.Author.Discriminator}"
                    }
                });
            }
            return s.Prefix ?? DefaultPrefix;
        }

        private int GetStringPrefixLength(DiscordMessage msg, string str, StringComparison comparisonType = StringComparison.Ordinal)
        {
            var content = msg.Content;
            if (str.Length >= content.Length)
                return -1;

            if (!content.StartsWith(str, comparisonType))
                return -1;

            return str.Length;
        }
    }
}
