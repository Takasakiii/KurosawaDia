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
using static Bot.Extensions.LogEmiter.TipoLog;

namespace Bot.Nucleo.Eventos
{
    //Classe responsavel pelo tratamento do evento MessageReceived do Discord.net (Recebimento e tratamento das Mensagens)
    public class MessageEvent
    {
        //MessageEvent and CommandHandler v3.5 by Takasaki
        
        //Dependencias do MessagemEvent
        //  - DiaConfig contem informações como prefix padrão do bot
        private readonly DiaConfig Config;
        //  - ModulesConcat contem as informações de todos os modulos e comandos do bot
        //      -GenericModule é a classe que representa o tipo de modulo usado nos comandos
        private readonly ModulesConcat<GenericModule> ModulesConcat;

        //Construtor da classe MessageEvent, ele requer para fins de dependencia um objeto da DiaConfig e um objeto de ModulesConcat<GenericModule>
        public MessageEvent(DiaConfig config, ModulesConcat<GenericModule> modulesConcat)
        {
            Config = config;
            ModulesConcat = modulesConcat;
        }

        //Metodo responsavel por receber e cuidar do MessageEvent do Discord.Net
        public async Task MessageReceived(SocketMessage mensagem)
        {
            await Task.Run(() => {
                CriarSessaoComandos(mensagem);
            });
        }

        //Metodo interno que cria uma thread para que o bot não trave o Handler do bot
        private void CriarSessaoComandos(SocketMessage message)
        {
            new Thread(async () =>
            {
                CommandContext contexto = null;
                try
                {
                    SocketUserMessage socketUserMessage = message as SocketUserMessage;
                    if (socketUserMessage != null)
                    {
                        contexto = new CommandContext(SingletonClient.client, socketUserMessage);
                        await ControlarMensagens(contexto);
                    }
                }
                catch(Discord.Net.HttpException e)
                {
                    await LogEmiter.EnviarLogAsync(TipoCor.Erro, $"Erro do servidor: {contexto.Guild.Id}\n\n{e.ToString()}");
                }

                catch (Exception e)
                {
                    await LogEmiter.EnviarLogAsync(e);
                }
            }).Start();
        }

        //Metodo interno responsavel por integrar todos os modulos extras (SubEventos) a fim de que cada mensagem receba o seu devido tratamento e caminho pelo bot
        private async Task ControlarMensagens(CommandContext contexto)
        {
            if (!contexto.User.IsBot)
            {
                await CadastrarServidorUsuarioAsync(contexto);
                await new Utility(contexto:contexto).PIEvent();
                Servidores servidores = await PegarPrefixo(contexto);
                string comandoSemPrefix = null;
                if (SepararComandoPrefix(contexto, servidores, ref comandoSemPrefix))
                {
                    await CallComando(comandoSemPrefix, servidores, contexto);
                }
                else
                {
                    if (IsMentionCall(contexto))
                    {
                        await new Ajuda(contexto, null, null).MentionMessage(servidores);
                    }
                    else
                    {
                        await new CustomReactions(contexto, null, null).TriggerACR(contexto, servidores);
                    }

                }
            }
        }

        //Metodo interno para pegar as informações do prefixo do servidor especifico
        private async Task<Servidores> PegarPrefixo(CommandContext contexto)
        {
            if (!contexto.IsPrivate)
            {
                Servidores servFinal = new Servidores(contexto.Guild.Id, Config.prefix.ToCharArray());
                Servidores servidores = servFinal;
                Tuple<bool, Servidores> serv = await new ServidoresDAO().GetPrefixAsync(servidores);
                if (serv.Item1)
                {
                    servFinal = serv.Item2;
                }
                return servFinal;
            }
            else
            {
                Servidores servidores = new Servidores(0, Config.prefix.ToCharArray());
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
        private async Task CadastrarServidorUsuarioAsync(CommandContext context)
        {
            if (!context.IsPrivate)
            {
                await new Servidores_UsuariosDAO().inserirServidorUsuarioAsync(new Servidores_Usuarios(new Servidores(context.Guild.Id, context.Guild.Name), new Usuarios(context.User.Id, context.User.ToString())));
            }
        }

        //Metodo interno responsavel por separar o comando e criar o args que vai ser enviado pros modulos
        //  - 0: prefixo do servidor/bot
        //  - 1: array de string cada um contendo uma posição do comando enviado separado por " "(espaço);
        private object[] CriadorDoArgs(string messagemSemPrefixo, ref string comando, Servidores servidor)
        {
            string[] stringComando = messagemSemPrefixo.Split(' ');
            comando = stringComando[0];
            object[] args = new object[2];
            args[0] = new string(servidor.Prefix);
            args[1] = stringComando;
            return args;
        }
        
        //Metodo interno resposavel por chamar o ModulesConcat e chamar o comando especificado
        private async Task CallComando(string comando, Servidores servidor, CommandContext contexto)
        {
            string chamada = null;
            object[] args = CriadorDoArgs(comando, ref chamada, servidor);
            try
            {
                ModulesConcat.AddArgs(contexto, args[0], args[1], new ErrorExtension(contexto, chamada, (string)args[0]));
                await (Task)ModulesConcat.InvokeMethod(chamada);
            }
            catch (Exception e)
            {
                await new Ajuda(contexto, (string)args[0], (string[])args[1]).MessageEventExceptions(e, servidor);
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
