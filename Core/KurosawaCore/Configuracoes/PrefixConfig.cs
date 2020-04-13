using DataBaseController.DAOs;
using DataBaseController.Modelos;
using DSharpPlus.Entities;
using KurosawaCore.Extensions.JsonEmbedExtension;
using System;
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
            int tamanho = GetStringPrefixLength(msg, await GetPrefix(msg));
            if (tamanho != -1)
                return tamanho;
            else
                return await GetCR(msg);
        }



        private async Task<int> GetCR(DiscordMessage msg)
        {
            if (msg.Channel.IsPrivate)
                return -1;
            CustomReactions cr = await new CustomReactionsDAO().Get(new CustomReactions
            {
                Servidor = new Servidores
                {
                    ID = msg.Channel.Guild.Id
                },
                Trigger = msg.Content
            });

            if (cr != null)
                await new JsonEmbedExtension().SendMessage(msg.Channel, cr.Resposta);
            return -1;
        }

        private async Task<string> GetPrefix(DiscordMessage msg)
        {
            if (!msg.Channel.IsPrivate)
            {
                Servidores s = await new ServidoresDAO().Get(new Servidores
                {
                    ID = msg.Channel.GuildId,
                }) ?? new Servidores();

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
                return s.Prefix ?? DefaultPrefix;
            }
            return DefaultPrefix;
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
