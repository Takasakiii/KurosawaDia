using Bot.Comandos;
using Bot.Extensions;
using Bot.GenericTypes;
using Bot.Singletons;
using ConfigurationControler.Modelos;
using Discord.Commands;
using Discord.WebSocket;
using MainDatabaseControler.DAO;
using MainDatabaseControler.Modelos;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Bot.Nucleo.Eventos
{
    //Classe responsavel pelo tratamento do evento MessageReceived do Discord.net (Recebimento e tratamento das Mensagens)
    public class MessageEvent
    {
        //MessageEvent and CommandHandler v3.5 by Takasaki
        
        //Dependencias do MessagemEvent
        //  - DiaConfig contem informações como prefix padrão do bot
        private readonly DiaConfig config;
        //  - ModulesConcat contem as informações de todos os modulos e comandos do bot
        //      -GenericModule é a classe que representa o tipo de modulo usado nos comandos
        private readonly ModulesConcat<GenericModule> modulesConcat;

        //Construtor da classe MessageEvent, ele requer para fins de dependencia um objeto da DiaConfig e um objeto de ModulesConcat<GenericModule>
        public MessageEvent(DiaConfig config, ModulesConcat<GenericModule> modulesConcat)
        {
            this.config = config;
            this.modulesConcat = modulesConcat;
        }

        //Metodo responsavel por receber e cuidar do MessageEvent do Discord.Net
        public Task MessageReceived(SocketMessage mensagem)
        {
            CriarSessaoComandos(mensagem);
            return Task.CompletedTask;
        }

        //Metodo interno que cria uma thread para que o bot não trave o Handler do bot
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

        //Metodo interno responsavel por integrar todos os modulos extras (SubEventos) a fim de que cada mensagem receba o seu devido tratamento e caminho pelo bot
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

        //Metodo interno para pegar as informações do prefixo do servidor especifico
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

        //Metodo interno responsavel por separar o prefixo do comando
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

        //Metodo interno responsavel pelo cadastramento de um usuario/servidor na db do bot
        private void CadastrarServidorUsuarioAsync(CommandContext context)
        {
            BotCadastro.AdicionarCadastro(context);
        }

        //Metodo interno responsavel por separar o comando e criar o args que vai ser enviado pros modulos
        //  - 0: prefixo do servidor/bot
        //  - 1: array de string cada um contendo uma posição do comando enviado separado por " "(espaço);
        //  - 2: List responsavel por passamento de args extras para uso de Extenções 
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
        
        //Metodo interno resposavel por chamar o ModulesConcat e chamar o comando especificado
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

        //Metodo interno para avaliar se a mensagem é uma mensão ao bot
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
