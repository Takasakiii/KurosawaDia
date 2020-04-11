using DataBaseController.Abstractions;
using DataBaseController.DAOs;
using DataBaseController.Modelos;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using KurosawaCore.Extensions.JsonEmbedExtension;
using System.Threading.Tasks;

namespace KurosawaCore.Events
{
    internal class UserGuildEnter
    {
        internal UserGuildEnter(ref DiscordClient client)
        {
            client.GuildMemberAdded += Client_GuildMemberAdded;
        }

        private async Task Client_GuildMemberAdded(GuildMemberAddEventArgs e)
        {
            Canais canalBemvindo = await new CanaisDAO().Get(new Canais
            {
                Servidor = new Servidores
                {
                    ID = e.Guild.Id
                },
                TipoCanal = TiposCanais.BemVindo
            });
            if (canalBemvindo != null)
            {
                ConfiguracoesServidores config = await new ConfiguracoesServidoresDAO().Get(new ConfiguracoesServidores
                {
                    Servidor = new Servidores
                    {
                        ID = e.Guild.Id
                    },
                    Configuracoes = TiposConfiguracoes.BemVindoMsg
                });
                if (config != null)
                {
                    DiscordChannel canal = e.Guild.GetChannel(canalBemvindo.ID);
                    await new JsonEmbedExtension().SendMessage(canal, config.Value);
                }
            }
        }
    }
}
