using Bot.Comandos;
using Bot.Extensions;
using Bot.GenericTypes;
using Bot.Singletons;
using ConfigurationControler.Modelos;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using MainDatabaseControler.DAO;
using MainDatabaseControler.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using static MainDatabaseControler.Modelos.ConfiguracoesServidor;

namespace Bot.Nucleo.Eventos
{
    public class MessageEvent
    {
        //MessageEvent and CommandHandler v3.5 by Takasaki
        //Dependencia do MessagemEvent
        private readonly DiaConfig config;
        private readonly ModulesConcat<GenericModule> modulesConcat;

        public MessageEvent(DiaConfig config, ModulesConcat<GenericModule> modulesConcat)
        {
            this.config = config;
            this.modulesConcat = modulesConcat;
        }

        public Task MessageReceived(SocketMessage mensagem)
        {
            CriarSessaoComandos(mensagem);
            return Task.CompletedTask;
        }

        private void CriarSessaoComandos(SocketMessage message)
        {
            new Thread(() =>
            {
                SocketUserMessage socketUserMessage = message as SocketUserMessage;
                if (socketUserMessage != null)
                {
                    CommandContext contexto = new CommandContext(SingletonClient.client, socketUserMessage);
                    ControlarMensagens(contexto);
                }
            }).Start();
        }

        private void ControlarMensagens(CommandContext contexto)
        {
            if (!contexto.User.IsBot)
            {
                CadastrarServidorUsuarioAsync(contexto);
                new Utility(contexto, null).PIEvent();
                Servidores servidores = PegarPrefixo(contexto);
                string comandoSemPrefix = null;
                if (SepararComandoPrefix(contexto, servidores, ref comandoSemPrefix))
                {
                    CallComando(comandoSemPrefix, servidores, contexto);
                }
                else
                {
                    if (IsMentionCall(contexto))
                    {
                        new Ajuda(contexto, null).MentionMessage(servidores);
                    }
                    else
                    {
                        new CustomReactions(contexto, null).TriggerACR(contexto, servidores);
                    }

                }
            }
        }

        private Servidores PegarPrefixo(CommandContext contexto)
        {
            if (!contexto.IsPrivate)
            {
                Servidores servFinal = new Servidores(contexto.Guild.Id, config.prefix.ToCharArray());
                Servidores servidores = servFinal;
                if (new ServidoresDAO().GetPrefix(ref servidores))
                {
                    servFinal = servidores;
                }
                return servFinal;
            }
            else
            {
                Servidores servidores = new Servidores(0, config.prefix.ToCharArray());
                return servidores;
            }


        }


        private bool SepararComandoPrefix(CommandContext contexto, Servidores servidor, ref string comandoSemPrefix)
        {
            int argPos = 0;
            if (contexto.Message.HasStringPrefix(new string(servidor.Prefix), ref argPos))
            {
                comandoSemPrefix = contexto.Message.Content.Substring(servidor.Prefix.Length);
                return !(comandoSemPrefix == "" || comandoSemPrefix[0] == servidor.Prefix[0]);
            }
            else
            {
                return false;
            }

        }

        private void CadastrarServidorUsuarioAsync(CommandContext context)
        {
            BotCadastro.AdicionarCadastro(context);
        }

        private object[] CriadorDoArgs(string messagemSemPrefixo, ref string comando, Servidores servidor)
        {
            string[] stringComando = messagemSemPrefixo.Split(' ');
            comando = stringComando[0];
            object[] args = new object[3];
            args[0] = new string(servidor.Prefix);
            args[1] = stringComando;
            args[2] = new List<object>();
            return args;
        }

        private void CallComando(string comando, Servidores servidor, CommandContext contexto)
        {
            string chamada = null;
            object[] args = CriadorDoArgs(comando, ref chamada, servidor);
            try
            {
                modulesConcat.AddArgs(contexto, args);
                modulesConcat.InvokeMethod(chamada);
            }
            catch (Exception e)
            {
                new Ajuda(contexto, args).MessageEventExceptions(e, servidor);
            }

        }

        private bool IsMentionCall(CommandContext contexto)
        {
            if (contexto.Message.Content == $"<@{SingletonClient.client.CurrentUser.Id}>" || contexto.Message.Content == $"<@!{SingletonClient.client.CurrentUser.Id}>")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        

        
    }
}
