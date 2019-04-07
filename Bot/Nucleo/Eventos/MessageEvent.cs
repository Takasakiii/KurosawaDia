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

        public object CommandContex { get; private set; } // organização da classe ta uma porra
        //de vez de perder meia hora criando comandos o pente fino seria melhor

        public async Task MessageRecived(SocketMessage mensagem)
        {
            var lastClassCommand = new Utility();

            SocketUserMessage mensagemTratada = mensagem as SocketUserMessage;
            CommandContext commandContex = new CommandContext(client, mensagemTratada); //1000% facepalm

            if(!mensagem.Author.IsBot)
            {
               //otimização grave pode ser resolvida com um poquinho de logica e mover itens


                int argPos = 0;
                //variavel putamente desnecessaria
                bool err = true;
                bool cmd = false;
                if(mensagemTratada.HasStringPrefix(new string (config.prefix), ref argPos))
                {
                    string messageSemPrefix = mensagem.Content.Substring(config.prefix.Length);

                    string[] comando;
                    try
                    {
                        //DBconfig dBconfig = new DBconfig(1);
                        //dBconfig = new DbConfigDAO().Carregar(dBconfig);

                        //await VerificarACR(commandContex.Message.Content, commandContex, dBconfig);

                        comando = messageSemPrefix.Split(' ');
                        MethodInfo metodo = lastClassCommand.GetType().GetMethod(comando[0]);
                        object instanced = lastClassCommand;
                        object[] parametros = new object[2];
                        parametros[0] = commandContex;
                        object[] args = new object[2];
                        args[0] = new string(config.prefix);
                        args[1] = comando;
                        parametros[1] = args;

                        //variavel inutil
                        string[] cmdargs = (string[])args[1];
                        //isso n deveria estar aqui
                        if (cmdargs[0] != "")
                        {
                            cmd = true;
                        }
                        else
                        {
                            cmd = false;
                        }
                        err = false;

                        metodo.Invoke(instanced, parametros);

                    }
                    catch 
                    {
                        //if desnecessario, try catch todo errado
                        if(err == false && cmd == true)
                        {
                            err = true;
                        }
                    }

                    if(err)
                    {
                        await mensagem.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithDescription($"**{mensagem.Author}** esse comando não existe use `{new string(config.prefix)}comandos` para ver os comandos")
                             .WithColor(Color.DarkPurple)
                         .Build());
                    }
                }
            }
        }

        //private async Task VerificarACR(string msg, CommandContext context, DBconfig dBconfig)
        //{
        //    BotRespostas botRespostas = new BotRespostas();
        //    botRespostas.pergunta = msg;
        //    botRespostas.id = Convert.ToInt64(context.Guild.Id);
        //    BotRespostasDAO dao = new BotRespostasDAO(dBconfig);
        //    botRespostas = dao.Responder(botRespostas);
        //    await context.Channel.SendMessageAsync(botRespostas.resposta);
        //    return;
        //}
    }
}
