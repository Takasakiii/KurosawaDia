using DataBaseController.Abstractions;
using DataBaseController.DAOs;
using DataBaseController.Modelos;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using KurosawaCore.Extensions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KurosawaCore.Events
{
    internal class UserGuildExit
    {
        internal UserGuildExit(ref DiscordClient client)
        {
            client.GuildMemberRemoved += Client_GuildMemberRemoved;
        }

        private Task Client_GuildMemberRemoved(GuildMemberRemoveEventArgs e)
        {
            new Thread(Session).Start(e);
            return Task.CompletedTask;
        }

        private async void Session(object note)
        {
            GuildMemberRemoveEventArgs e = (GuildMemberRemoveEventArgs)note;

            try
            {
                Canais canalSaida = await new CanaisDAO().Get(new Canais
                {
                    Servidor = new Servidores
                    {
                        ID = e.Guild.Id
                    },
                    TipoCanal = TiposCanais.Sair
                });
                if (canalSaida != null)
                {
                    ConfiguracoesServidores config = await new ConfiguracoesServidoresDAO().Get(new ConfiguracoesServidores
                    {
                        Servidor = new Servidores
                        {
                            ID = e.Guild.Id
                        },
                        Configuracoes = TiposConfiguracoes.SaidaMsg
                    });
                    if (config != null)
                    {
                        DiscordChannel canal = e.Guild.GetChannel(canalSaida.ID);
                        await new StringVariablesExtension(e.Member, e.Guild).SendMessage(canal, config.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                e.Client.DebugLogger.LogMessage(LogLevel.Info, "Kurosawa Dia - Event", ex.Message, DateTime.Now);
            }


        }
    }
}
