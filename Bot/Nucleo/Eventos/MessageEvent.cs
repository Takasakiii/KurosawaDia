using Bot.Comandos;
using Bot.Modelos;
using Bot.Singletons;
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

        public MessageEvent(AyuraConfig config)
        {
            this.config = config;
        }

        public async Task MessageRecived(SocketMessage mensagem)
        {
            SocketUserMessage mensagemTratada = mensagem as SocketUserMessage;
            CommandContext commandContex = new CommandContext(SingletonClient.client, mensagemTratada);

            if (!mensagem.Author.IsBot)
            {
                int argPos = 0;
                if (mensagemTratada.HasStringPrefix(new string(config.prefix), ref argPos))
                {
                    string messageSemPrefix = mensagem.Content.Substring(config.prefix.Length);

                    if (messageSemPrefix != "" && messageSemPrefix[0] != config.prefix[0])
                    {
                        try
                        {
                            string[] comando = messageSemPrefix.Split(' ');
                            var lastClassCommand = new Owner();
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
                        catch (Exception e)
                        {
                            if(e is NullReferenceException || e is AmbiguousMatchException)
                            {
                                await commandContex.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                            .WithDescription($"**{commandContex.User}** comando não encontrado use `{new string(config.prefix)}comandos` para ver os meus comandos")
                                            .WithColor(Color.DarkPurple)
                                     .Build());
                            }
                            else
                            {
                                MethodInfo metodo = SingletonLogs.tipo.GetMethod("Log");
                                object[] parms = new object[1];
                                parms[0] = e.ToString();
                                metodo.Invoke(SingletonLogs.instanced, parms);
                            }
                        }
                    }
                }
                if (commandContex.Message.Content == $"<@{SingletonClient.client.CurrentUser.Id}>" || commandContex.Message.Content == $"<@!{SingletonClient.client.CurrentUser.Id}>")
                {
                    await commandContex.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithDescription($"Oii {commandContex.User.Username} meu prefixo é: `{new string(config.prefix)}` se quiser ver meus comando é so usar: `{new string(config.prefix)}comandos`")
                            .WithColor(Color.DarkPurple)
                        .Build());
                }
            }
        }
    }
}
