using DataBaseController.DAOs;
using DataBaseController.Modelos;
using DSharpPlus.Entities;
using KurosawaCore.Extensions;
using KurosawaCore.Extensions.JsonEmbedExtension;
using System;
using System.Threading.Tasks;

namespace KurosawaCore.Configuracoes
{
    internal sealed class PrefixConfig
    {
        internal PrefixConfig(string defaultprefix)
        {
            PrefixExtension.DefaultPrefix = defaultprefix;
        }

        internal async Task<int> PegarPrefixo(DiscordMessage msg)
        {
            int tamanho = GetStringPrefixLength(msg, await PrefixExtension.GetPrefix(msg));
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



        private int GetStringPrefixLength(DiscordMessage msg, string str, StringComparison comparisonType = StringComparison.Ordinal)
        {
            string content = msg.Content ?? "";
            if (str.Length >= content.Length)
                return -1;

            if (!content.StartsWith(str, comparisonType))
                return -1;

            return str.Length;
        }
    }
}
