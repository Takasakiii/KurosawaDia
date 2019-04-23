using Bot.Comandos;
using Bot.Modelos;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Bot.Nucleo.Eventos
{
    public class MessageEvent
    {
        private readonly AyuraConfig config;
        private readonly DiscordSocketClient client;
        private readonly ApiConfig apiConfig;

        public MessageEvent(DiscordSocketClient client, AyuraConfig config, ApiConfig apiConfig)
        {
            this.client = client;
            this.config = config;
            this.apiConfig = apiConfig;
        }

        public async Task MessageRecived(SocketMessage mensagem)
        {
            var lastClassCommand = new Utility();

            SocketUserMessage mensagemTratada = mensagem as SocketUserMessage;
            CommandContext commandContex = new CommandContext(client, mensagemTratada);

            if (!mensagem.Author.IsBot)
            {
                int argPos = 0;
                if (mensagemTratada.HasStringPrefix(new string(config.prefix), ref argPos))
                {
                    string messageSemPrefix = mensagem.Content.Substring(config.prefix.Length);

                    if (messageSemPrefix != "")
                    {
                        try
                        {
                            string[] comando = messageSemPrefix.Split(' ');
                            MethodInfo metodo = lastClassCommand.GetType().GetMethod(comando[0]);
                            object instanced = lastClassCommand;
                            object[] parametros = new object[2];
                            parametros[0] = commandContex;
                            object[] args = new object[3];
                            args[0] = new string(config.prefix);
                            args[1] = comando;
                            args[2] = apiConfig;
                            parametros[1] = args;

                            metodo.Invoke(instanced, parametros);
                        }
                        catch (NullReferenceException)
                        {
                            await commandContex.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                    .WithDescription($"**{commandContex.User}** comando não encontrado use `{new string(config.prefix)}comandos` para ver os meus comandos")
                                    .WithColor(Color.DarkPurple)
                                .Build());
                        }
                    }
                }
            }
        }
    }
}
