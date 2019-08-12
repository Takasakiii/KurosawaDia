using Bot.Extensions;
using Discord;
using Discord.Commands;
using MainDatabaseControler.DAO;
using MainDatabaseControler.Modelos;
using static MainDatabaseControler.Modelos.Canais;
using static MainDatabaseControler.Modelos.ConfiguracoesServidor;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Bot.Comandos
{
    public class Configuracoes : CustomReactions
    {
        //setar as perm
        public void setprefix(CommandContext context, object[] args)
        {
            new BotCadastro((CommandContext cmdContext, object[] cmdArgs) =>
            {
                if (!cmdContext.IsPrivate)
                {
                    string[] comando = (string[])cmdArgs[1];
                    string msg = string.Join(" ", comando, 1, (comando.Length - 1));

                    if (msg != "")
                    {
                        IUserMessage message = cmdContext.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                .WithDescription(StringCatch.GetString("setprefixCtz", "**{0}** você quer mudar o prefixo?", cmdContext.User))
                                .WithFooter(StringCatch.GetString("setprefixIgnorar", "se não apenas ignore essa mensagem"))
                                .WithColor(Color.DarkPurple)
                            .Build()).GetAwaiter().GetResult();

                        Emoji emoji = new Emoji("✅");
                        message.AddReactionAsync(emoji);

                        ReactionControler reaction = new ReactionControler();
                        reaction.GetReaction(message, emoji, cmdContext.User, new ReturnMethod((CommandContext contexto, object[] argumentos) =>
                        {
                            Servidores servidor = new Servidores(cmdContext.Guild.Id, msg.ToCharArray());

                            new ServidoresDAO().SetServidorPrefix(ref servidor);

                            message.DeleteAsync();
                            cmdContext.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                    .WithDescription(StringCatch.GetString("setperfixAlterado", "**{0}** o prefixo do servidor foi alterado de: `{1}` para: `{2}`", cmdContext.User.ToString(), (string)cmdArgs[0], new string(servidor.Prefix)))
                                    .WithColor(Color.DarkPurple)
                                .Build());
                        }, cmdContext, cmdArgs));
                    }
                    else
                    {
                        cmdContext.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                .WithDescription(StringCatch.GetString("setprefixFalarPrefixo", "**{0}** você precisa me falar um prefixo", cmdContext.User.ToString()))
                                .AddField(StringCatch.GetString("usoCmd", "Uso do Comando:"), StringCatch.GetString("usoSetprefix", "`{0}setprefix <prefixo>`", (string)cmdArgs[0]))
                                .AddField(StringCatch.GetString("exemploCmd", "Exemplo: "), StringCatch.GetString("exemploCmd", "`{0}setprefix !`", (string)cmdArgs[0]))
                                .WithColor(Color.Red)
                            .Build());
                    }

                }
                else
                {
                    cmdContext.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithDescription(StringCatch.GetString("setprefixDm", "Esse comando so pode ser usado em servidores"))
                            .WithColor(Color.Red)
                        .Build());
                }
            }, context, args).EsperarOkDb();
        }

        //setar as perm
        public void pirole(CommandContext context, object[] args)
        {
            new BotCadastro((CommandContext cmdContext, object[] cmdArgs) =>
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithColor(Color.DarkPurple);

                if (!cmdContext.IsPrivate)
                {
                    embed.WithColor(Color.Purple);
                    embed.WithTitle(StringCatch.GetString("xproleSetTitle", "**Configuração dos Pontos de Interação**"));
                    embed.WithDescription(StringCatch.GetString("xproleSetDesc1", "Você deseja ligar os pontos de interação??(eles servem para medir a interação dos seus membros e setar cargos automaticamente)"));
                    embed.AddField(StringCatch.GetString("xptoleSetF1", "Opções Validas:"), StringCatch.GetString("xproleSetF1Desc", "s - Sim / Ligar\nn - Não / Desligar"));
                    IMessage pergunta = cmdContext.Channel.SendMessageAsync(embed: embed.Build()).GetAwaiter().GetResult();
                    SubCommandControler sub = new SubCommandControler();
                    IMessage msgresposta = sub.GetCommand(pergunta, cmdContext.User);
                    if (msgresposta != null)
                    {
                        bool ativado;
                        double rate = 2;
                        string msg = "";
                        if (msgresposta.Content == "s" || msgresposta.Content == "n")
                        {

                            if (msgresposta.Content == "s")
                            {
                                ativado = true;
                                embed.WithDescription(StringCatch.GetString("xproleSetDesc2", "Qual é o multiplicador de Pontos de Interação que deseja usar (esse multiplicador determina como sera medido a interação dos membros) [recomendamos o multiplicador 2]"));
                                embed.Fields.Clear();
                                embed.AddField(StringCatch.GetString("xptoleSetF1", "Opções Validas:"), StringCatch.GetString("xproleSet2F1Desc", "Qualquer numero a partir de 1,0"));
                                pergunta = cmdContext.Channel.SendMessageAsync(embed: embed.Build()).GetAwaiter().GetResult();
                                sub = new SubCommandControler();
                                msgresposta = sub.GetCommand(pergunta, cmdContext.User);
                                if (double.TryParse(msgresposta.Content, out rate))
                                {
                                    if (rate >= 1)
                                    {
                                        embed.WithDescription(StringCatch.GetString("xproleSetDesc3", "Digite a messagem que você quer que eu mostre quando alguem conseguir um Ponto de Interação, se você não deseja ter uma mensagem apenas digite `%desativar%`"));
                                        embed.Fields.Clear();
                                        embed.AddField(StringCatch.GetString("xptoleSetF1", "Opções Validas:"), StringCatch.GetString("xproleSet3F1Desc", "Qualquer tipo de texto, podendo usar até Embeds compativel com a Nadeko Bot e variaveis como %user% e %pontos%"));
                                        pergunta = cmdContext.Channel.SendMessageAsync(embed: embed.Build()).GetAwaiter().GetResult();
                                        sub = new SubCommandControler();
                                        msgresposta = sub.GetCommand(pergunta, cmdContext.User);
                                        msg = msgresposta.Content;
                                    }
                                    else
                                    {
                                        RotaFail(cmdContext);
                                    }
                                }
                                else
                                {
                                    RotaFail(cmdContext);
                                }
                            }
                            else
                            {
                                ativado = false;
                            }
                            PI pimodel = new PI(ativado, rate, (msg == "%desativar%") ? "" : msg);
                            if (new ConfiguracoesServidorDAO().SalvarPIConfig(new ConfiguracoesServidor(new Servidores(cmdContext.Guild.Id), pimodel)))
                            {
                                cmdContext.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                    .WithColor(Color.Green)
                                    .WithTitle(StringCatch.GetString("xproleSetTitleOK", "Ok, farei tudo conforme o pedido 😃"))
                                    .Build());
                            }
                            else
                            {
                                cmdContext.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                    .WithColor(Color.Red)
                                    .WithTitle(StringCatch.GetString("xproleSetTitleFail", "Desculpe mas ouve um problema ao tentar salvar suas preferencias, se for urgente contate meus criadores que eles vão te dar todo o suporte 😔"))
                                    .Build());
                            }
                        }
                        else
                        {
                            RotaFail(cmdContext);
                        }

                    }
                }
                else
                {
                    embed.WithDescription(StringCatch.GetString("xproleDm", "Esse comando só pode ser usado em servidores"));
                    embed.WithColor(Color.Red);
                    cmdContext.Channel.SendMessageAsync(embed: embed.Build());
                }
            }, context, args).EsperarOkDb();
        }

        private void RotaFail(CommandContext contexto)
        {
            contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                .WithColor(Color.Red)
                .WithTitle(StringCatch.GetString("rotafailtitle", "Desculpe, mas você terá que me falar um valor dentro do **Opções Validas**, se não eu não poderei te ajudar 😔"))
                .Build());
        }

        //setar as perm
        public void welcomech(CommandContext context, object[] args)
        {
            if (!context.IsPrivate)
            {
                string id = "";
                string[] comando = (string[])args[1];
                string msg = string.Join(" ", comando, 1, (comando.Length - 1));

                foreach (char letra in msg)
                {
                    if (ulong.TryParse(letra.ToString(), out ulong result))
                    {
                        id += result;
                    }
                }
                IChannel canal = null;
                try
                {
                    canal = context.Guild.GetChannelAsync(Convert.ToUInt64(id)).GetAwaiter().GetResult();
                }
                catch
                {
                    canal = context.Channel;
                }

                if (canal != null)
                {
                    Canais canalModel = new Canais(canal.Id, new Servidores(context.Guild.Id), TiposCanais.bemvindoCh, canal.Name);
                    if (new CanaisDAO().AddCh(canalModel))
                    {
                        context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                .WithDescription(StringCatch.GetString("welcomechOk", "**{0}** as mensagens de boas-vindas serão enviadas no canal: `#{1}`", context.User.ToString(), canalModel.NomeCanal))
                                .WithColor(Color.DarkPurple)
                             .Build());
                    }
                    else
                    {
                        context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                .WithDescription(StringCatch.GetString("welcomechNSetado", "**{0}** eu não consegui definir esse canal para mandar as boas-vindas", context.User.ToString()))
                                .WithColor(Color.Red)
                            .Build());
                    }
                }
                else
                {
                    context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithDescription(StringCatch.GetString("welcomechSemCanal", "**{0}** eu não encontrei esse canal no servidor", context.User.ToString()))
                            .WithColor(Color.Red)
                        .Build());
                }
            }
            else
            {
                context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithDescription(StringCatch.GetString("welcomechDm", "Esse comando só pode ser usado em servidores"))
                        .WithColor(Color.Red)
                    .Build());
            }
        }

        //setar as perm
        public void addPICargo (CommandContext contexto, object[] args)
        {
            string[] comandoargs = (string[])args[1];
            string prefix = (string)args[0];
            EmbedBuilder msgErro = new EmbedBuilder()
                .WithColor(Color.Red)
                .AddField(StringCatch.GetString("addpicargoErrMsgUsageFtitle", "Uso do comando:"), StringCatch.GetString("addpicargoErrMsgUsageFcontent", "`{0}addPICargo QuantidadeDePIRequerido NomeCargo`", prefix))
                .AddField(StringCatch.GetString("addpicargoErrMsgExempleFtitle", "Exemplo do comando:"), StringCatch.GetString("addpicargoErrMsgExempleFcontent", "`{0}addPICargo 3 Membros`", prefix));

            if(comandoargs.Length > 2)
            {
                string nomerole = string.Join(" ", comandoargs, 2, comandoargs.Length - 2);
                List<IRole> cargos = contexto.Guild.Roles.ToList();
                ulong id;
                IRole cargoSelecionado = null;
                if (ulong.TryParse(nomerole, out id))
                {
                    cargoSelecionado = cargos.Find(x => x.Id == id);
                }
                else
                {
                    cargoSelecionado = cargos.Find(x => x.Name == nomerole);
                }

                if(cargoSelecionado == null)
                {
                    msgErro.WithTitle(StringCatch.GetString("addpicargoErrTitleRoleNotFind", "**{0}**, o cargo não pode ser encontrado, por favor verifique se você digitou o nome/id do cargo corretamente.", contexto.User.Username));
                    contexto.Channel.SendMessageAsync(embed: msgErro.Build());
                }
                else
                {
                    long requesito;
                    if (long.TryParse(comandoargs[1], out requesito) && requesito > 0)
                    {
                        Servidores servidor = new Servidores(contexto.Guild.Id, contexto.Guild.Name);
                        Cargos cargoCadastro = new Cargos(Cargos.Tipos_Cargos.XpRole, Convert.ToUInt64(cargoSelecionado.Id), cargoSelecionado.Name, requesito, servidor);
                        CargosDAO dao = new CargosDAO();
                        CargosDAO.Operacao operacaoRetorno = dao.AdicionarAtualizarCargo(cargoCadastro);
                        if(operacaoRetorno != CargosDAO.Operacao.Incompleta)
                        {
                            contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                .WithColor(Color.Green)
                                .WithTitle(StringCatch.GetString("addpicargofoi", "**{0}**, o cargo `{1}` foi {2} com sucesso 😃", contexto.User.Username, cargoSelecionado.Name, (operacaoRetorno == CargosDAO.Operacao.Insert) ? StringCatch.GetString("addpicargoAdicionar", "adicionado") : StringCatch.GetString("addpicargoAtualizado", "atualizado")))
                                .Build());
                        }
                        else
                        {
                            msgErro.WithTitle(StringCatch.GetString("addpicargoNFAdd", "Desculpe mas não consegui adicionar o cargo 😔", contexto.User.Username));
                            msgErro.Fields.Clear();
                            contexto.Channel.SendMessageAsync(embed: msgErro.Build());
                        }
                    }
                    else
                    {
                        msgErro.WithTitle(StringCatch.GetString("addpicargoErrTitlerequesito", "**{0}**, a quantidade de PI está invalida, por favor digite somente numero inteiros e maior que 0.", contexto.User.Username));
                        contexto.Channel.SendMessageAsync(embed: msgErro.Build());
                    }
                }
            }
            else
            {
                msgErro.WithTitle(StringCatch.GetString("addpicargoErrTitleLess2", "**{0}**, você precisa adicionar enviar os parametros do comando.", contexto.User.Username));
                contexto.Channel.SendMessageAsync(embed: msgErro.Build());
            }
        }

    }
}
