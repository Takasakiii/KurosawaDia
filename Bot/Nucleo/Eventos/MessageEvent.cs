using Bot.Comandos;
using Bot.DataBase.ConfigDB.Modelos;
using Bot.DataBase.MainDB.DAO;
using Bot.DataBase.MainDB.Modelos;
using Bot.Extensions;
using Bot.Singletons;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
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

        public Task MessageRecived(SocketMessage mensagem)
        {
            SocketUserMessage mensagemTratada = mensagem as SocketUserMessage;
            CommandContext commandContex = new CommandContext(SingletonClient.client, mensagemTratada);

            if (!mensagem.Author.IsBot)
            {
                new Thread(() =>
                {
                    char[] prefix = config.prefix;

                    if (!commandContex.IsPrivate)
                    {
                        Servidores serv = new Servidores(commandContex.Guild.Id);
                        char[] tmp = new ServidoresDAO().GetPrefix(serv);

                        if (tmp != null)
                        {
                            prefix = tmp;
                        }

                        ACRs acrTmp = new ACRs();
                        acrTmp.SetTrigger(commandContex.Message.Content, new Servidores(commandContex.Guild.Id));

                        ACRs acr = new ACRsDAO().ResponderAcr(acrTmp);
                        if (acr.resposta != null)
                        {
                            new EmbedControl().SendMessage(commandContex, acr.resposta);
                        }
                    }

                    int argPos = 0;
                    if (mensagemTratada.HasStringPrefix(new string(prefix), ref argPos))
                    {
                        string messageSemPrefix = mensagem.Content.Substring(prefix.Length);

                        if (messageSemPrefix != "" && messageSemPrefix[0] != prefix[0])
                        {
                            try
                            {
                                if (!commandContex.IsPrivate)
                                {
                                    new Thread(() =>
                                    {
                                        Servidores servi = new Servidores(commandContex.Guild.Id, commandContex.Guild.Name);
                                        Usuarios usuario = new Usuarios();
                                        usuario.SetUsuario(commandContex.User.Id, commandContex.User.ToString());

                                        new ServidoresDAO().inserirServidorUsuario(servi, usuario);
                                    }).Start();
                                }

                                string[] comando = messageSemPrefix.Split(' ');
                                var lastClassCommand = new CustomReactions();
                                MethodInfo metodo = lastClassCommand.GetType().GetMethod(comando[0]);
                                object instanced = lastClassCommand;
                                object[] parametros = new object[2];
                                parametros[0] = commandContex;
                                object[] args = new object[3];
                                args[0] = new string(prefix);
                                args[1] = comando;
                                args[2] = new List<object>(); //addons memory
                                parametros[1] = args;

                                metodo.Invoke(instanced, parametros);
                            }
                            catch (Exception e)
                            {
                                if (e is NullReferenceException || e is AmbiguousMatchException)
                                {
                                    commandContex.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                                .WithDescription($"**{commandContex.User}** comando não encontrado use `{new string(prefix)}comandos` para ver os meus comandos")
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
                        commandContex.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                .WithDescription($"Oii {commandContex.User.Username} meu prefixo é: `{new string(prefix)}` se quiser ver meus comando é so usar: `{new string(prefix)}comandos`")
                                .WithColor(Color.DarkPurple)
                            .Build());
                    }

                }).Start();
                
            }
            return Task.CompletedTask;
        }
    }
}
