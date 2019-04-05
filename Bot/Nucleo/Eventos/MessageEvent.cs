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

        public object CommandContex { get; private set; }

        public async Task MessageRecived(SocketMessage mensagem)
        {
            var lastClassCommand = new Utility();

            SocketUserMessage mensagemTratada = mensagem as SocketUserMessage;
            CommandContext commandContex = new CommandContext(client, mensagemTratada);

            if(!mensagem.Author.IsBot)
            {
               

                int argPos = 0;
                if(mensagemTratada.HasStringPrefix(new string (config.prefix), ref argPos))
                {
                    string messageSemPrefix = mensagem.Content.Substring(config.prefix.Length);

                    try
                    {
                        //DBconfig dBconfig = new DBconfig(1);
                        //dBconfig = new DbConfigDAO().Carregar(dBconfig);

                        //await VerificarACR(commandContex.Message.Content, commandContex, dBconfig);

                        string[] comando = messageSemPrefix.Split(' ');
                        MethodInfo metodo = lastClassCommand.GetType().GetMethod(comando[0]);
                        object instanced = lastClassCommand;
                        object[] parametros = new object[2];
                        parametros[0] = commandContex;
                        object[] args = new object[2];
                        args[0] = new string(config.prefix);
                        args[1] = comando;
                        parametros[1] = args;

                        metodo.Invoke(instanced, parametros);
                    } catch {
                        await mensagem.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                .WithDescription($"**{mensagem.Author}** esse comando não existe use `{new string(config.prefix)}comandos` para ver os comandos")
                                .WithColor(Color.DarkPurple)
                            .Build());
                    }
                }
            }
        }

        private async Task VerificarACR(string msg, CommandContext context, DBconfig dBconfig)
        {
            BotRespostas botRespostas = new BotRespostas();
            botRespostas.pergunta = msg;
            botRespostas.id = Convert.ToInt64(context.Guild.Id);
            BotRespostasDAO dao = new BotRespostasDAO(dBconfig);
            botRespostas = dao.Responder(botRespostas);
            await context.Channel.SendMessageAsync(botRespostas.resposta);
            return;
        }
    }
}
