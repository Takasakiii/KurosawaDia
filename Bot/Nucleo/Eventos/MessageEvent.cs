using Bot.Configs.DAO;
using Bot.Configs.Modelos;
using Bot.DataBase.DAO;
using Bot.Modelos;
using Bot.Modelos.Objetos;
using Bot.Nucleo.Modulos;
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

        public MessageEvent(DiscordSocketClient client, AyuraConfig config)
        {
            this.client = client;
            this.config = config;
        }

        public async Task MessageRecived(SocketMessage mensagem)
        {
            var lastClassCommand = new Utility();

            SocketUserMessage mensagemTratada = mensagem as SocketUserMessage;
            CommandContext commandContex = new CommandContext(client, mensagemTratada); 

            if(!mensagem.Author.IsBot)
            {
                int argPos = 0;
                if(mensagemTratada.HasStringPrefix(new string(config.prefix), ref argPos))
                {
                    string messageSemPrefix = mensagem.Content.Substring(config.prefix.Length);

                    if(messageSemPrefix != "")
                    {
                        string[] comando;
                        try
                        {
                            comando = messageSemPrefix.Split(' ');
                            MethodInfo metodo = lastClassCommand.GetType().GetMethod(comando[0]);
                            object instanced = lastClassCommand;
                            object[] parametros = new object[2];
                            parametros[0] = commandContex;
                            object[] args = new object[2];
                            args[0] = new string(config.prefix);
                            args[1] = comando;
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
