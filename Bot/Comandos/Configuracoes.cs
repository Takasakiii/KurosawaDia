using Bot.Extensions;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using MainDatabaseControler.DAO;
using MainDatabaseControler.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using static MainDatabaseControler.Modelos.Canais;
using static MainDatabaseControler.Modelos.ConfiguracoesServidor;

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
                    SocketGuildUser userGuild = context.User as SocketGuildUser;
                    if (userGuild.GuildPermissions.ManageGuild)
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
                                .WithDescription(StringCatch.GetString("setprefixSemPerm", "**{0}** você precisa da permissão ``Gerenciar Servidor`` para usar esse comando", cmdContext.User.ToString()))
                                .WithColor(Color.Red)
                            .Build()); ;
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

        public void piconf(CommandContext context, object[] args)
        {
            SocketGuildUser usuarioinGuild = context.User as SocketGuildUser;
            if (usuarioinGuild.GuildPermissions.Administrator)
            {
                SocketGuildUser botRepresentacao = context.Guild.GetCurrentUserAsync().GetAwaiter().GetResult() as SocketGuildUser;
                if (botRepresentacao.GuildPermissions.ManageRoles)
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
                                        if (msgresposta != null && double.TryParse(msgresposta.Content, out rate))
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
                                    if (rate < 1 && msg != "")
                                    {
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
                else
                {
                    context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithColor(Color.Red)
                        .WithTitle(StringCatch.GetString("xproleCargosFailCheck", "**{0}**, o bot precisa da permissão de gerenciar cargos para executar esse comando 😔", context.User.Username))
                        .Build());
                }
            }
            else
            {
                context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithColor(Color.Red)
                    .WithTitle(StringCatch.GetString("msgErroConfigPermission", "**{0}**, você precisa de permissão de Administrador para poder executar esse comando 😔"))
                    .Build());
            }
        }

        private void RotaFail(CommandContext contexto)
        {
            contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                .WithColor(Color.Red)
                .WithTitle(StringCatch.GetString("rotafailtitle", "Desculpe, mas você terá que me falar um valor dentro do **Opções Validas**, se não eu não poderei te ajudar 😔"))
                .Build());
        }

        public void welcomech(CommandContext context, object[] args)
        {
            if (!context.IsPrivate)
            {
                SocketGuildUser guildUser = context.User as SocketGuildUser;
                if (guildUser.GuildPermissions.Administrator)
                {
                    new BotCadastro((CommandContext cmdContext, object[] cmdArgs) =>
                    {
                        string id = "";
                        string[] comando = (string[])cmdArgs[1];
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
                            canal = cmdContext.Guild.GetChannelAsync(Convert.ToUInt64(id)).GetAwaiter().GetResult();
                        }
                        catch
                        {
                            canal = cmdContext.Channel;
                        }

                        if (canal != null)
                        {
                            Canais canalModel = new Canais(canal.Id, new Servidores(cmdContext.Guild.Id), TiposCanais.bemvindoCh, canal.Name);
                            if (new CanaisDAO().AddCh(canalModel))
                            {
                                cmdContext.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                        .WithDescription(StringCatch.GetString("welcomechOk", "**{0}** as mensagens de boas-vindas serão enviadas no canal: `#{1}`", cmdContext.User.ToString(), canalModel.NomeCanal))
                                        .WithColor(Color.DarkPurple)
                                     .Build());
                            }
                            else
                            {
                                cmdContext.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                        .WithDescription(StringCatch.GetString("welcomechNSetado", "**{0}** eu não consegui definir esse canal para mandar as boas-vindas", cmdContext.User.ToString()))
                                        .WithColor(Color.Red)
                                    .Build());
                            }
                        }
                        else
                        {
                            cmdContext.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                    .WithDescription(StringCatch.GetString("welcomechSemCanal", "**{0}** eu não encontrei esse canal no servidor", cmdContext.User.ToString()))
                                    .WithColor(Color.Red)
                                .Build());
                        }
                    }, context, args).EsperarOkDb();
                }
                else
                {
                    context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithDescription(StringCatch.GetString("welcomechSemPerm", "**{0}** você precisa da permissão: ``Administrador`` para usar esse comando"))
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

        public void byech(CommandContext context, object[] args)
        {
            if (!context.IsPrivate)
            {
                SocketGuildUser guildUser = context.User as SocketGuildUser;
                if (guildUser.GuildPermissions.Administrator)
                {
                    new BotCadastro((CommandContext cmdContext, object[] cmdArgs) =>
                    {
                        string id = "";
                        string[] comando = (string[])cmdArgs[1];
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
                            canal = cmdContext.Guild.GetChannelAsync(Convert.ToUInt64(id)).GetAwaiter().GetResult();
                        }
                        catch
                        {
                            canal = cmdContext.Channel;
                        }

                        if (canal != null)
                        {
                            Canais canalModel = new Canais(canal.Id, new Servidores(cmdContext.Guild.Id), TiposCanais.sairCh, canal.Name);
                            if (new CanaisDAO().AddCh(canalModel))
                            {
                                cmdContext.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                        .WithDescription(StringCatch.GetString("welcomechOk", "**{0}** as mensagens de saida serão enviadas no canal: `#{1}`", cmdContext.User.ToString(), canalModel.NomeCanal))
                                        .WithColor(Color.DarkPurple)
                                     .Build());
                            }
                            else
                            {
                                cmdContext.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                        .WithDescription(StringCatch.GetString("welcomechNSetado", "**{0}** eu não consegui definir esse canal para mandar as mensagens de saida", cmdContext.User.ToString()))
                                        .WithColor(Color.Red)
                                    .Build());
                            }
                        }
                        else
                        {
                            cmdContext.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                    .WithDescription(StringCatch.GetString("welcomechSemCanal", "**{0}** eu não encontrei esse canal no servidor", cmdContext.User.ToString()))
                                    .WithColor(Color.Red)
                                .Build());
                        }
                    }, context, args).EsperarOkDb();
                }
                else
                {
                    context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithDescription(StringCatch.GetString("welcomechSemPerm", "**{0}** você precisa da permissão: ``Administrador`` para usar esse comando"))
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

        public void picargo(CommandContext contexto, object[] args)
        {
            if (!contexto.IsPrivate)
            {
                SocketGuildUser userGuild = contexto.User as SocketGuildUser;
                if (userGuild.GuildPermissions.Administrator)
                {
                    string[] comandoargs = (string[])args[1];
                    string prefix = (string)args[0];
                    EmbedBuilder msgErro = new EmbedBuilder()
                        .WithColor(Color.Red)
                        .AddField(StringCatch.GetString("addpicargoErrMsgUsageFtitle", "Uso do comando:"), StringCatch.GetString("addpicargoErrMsgUsageFcontent", "`{0}picargo [QuantidadeDePIRequerido se o valor for menor ou igual a 0 o mesmo será removido] NomeCargo`", prefix))
                        .AddField(StringCatch.GetString("addpicargoErrMsgExempleFtitle", "Exemplo do comando:"), StringCatch.GetString("addpicargoErrMsgExempleFcontent", "`{0}piCargo 3 Membros`", prefix));

                    if (comandoargs.Length > 2)
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

                        if (cargoSelecionado == null)
                        {
                            msgErro.WithTitle(StringCatch.GetString("addpicargoErrTitleRoleNotFind", "**{0}**, o cargo não pode ser encontrado, por favor verifique se você digitou o nome/id do cargo corretamente.", contexto.User.Username));
                            contexto.Channel.SendMessageAsync(embed: msgErro.Build());
                        }
                        else
                        {
                            long requesito;
                            if (long.TryParse(comandoargs[1], out requesito))
                            {
                                new BotCadastro((CommandContext Contexto, object[] Args) =>
                                {
                                    Servidores servidor = new Servidores(contexto.Guild.Id, contexto.Guild.Name);
                                    Cargos cargoCadastro = new Cargos(Cargos.Tipos_Cargos.XpRole, Convert.ToUInt64(cargoSelecionado.Id), cargoSelecionado.Name, requesito, servidor);
                                    CargosDAO dao = new CargosDAO();
                                    CargosDAO.Operacao operacaoRetorno = dao.AdicionarAtualizarCargo(cargoCadastro);
                                    if (operacaoRetorno != CargosDAO.Operacao.Incompleta)
                                    {
                                        contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                            .WithColor(Color.Green)
                                            .WithTitle(StringCatch.GetString("addpicargofoi", "**{0}**, o cargo `{1}` foi {2} com sucesso 😃", contexto.User.Username, cargoSelecionado.Name, (operacaoRetorno == CargosDAO.Operacao.Insert) ? StringCatch.GetString("addpicargoAdicionar", "adicionado") : (operacaoRetorno == CargosDAO.Operacao.Update) ? StringCatch.GetString("addpicargoAtualizado", "atualizado") : StringCatch.GetString("addpicargoDeletado", "removido")))
                                            .Build());
                                    }
                                    else
                                    {
                                        msgErro.WithTitle(StringCatch.GetString("addpicargoNFAdd", "Desculpe mas não consegui adicionar o cargo 😔", contexto.User.Username));
                                        msgErro.Fields.Clear();
                                        contexto.Channel.SendMessageAsync(embed: msgErro.Build());
                                    }
                                }, contexto, args).EsperarOkDb();
                            }
                            else
                            {
                                msgErro.WithTitle(StringCatch.GetString("addpicargoErrTitlerequesito", "**{0}**, a quantidade de PI está invalida, por favor digite somente numero inteiros.", contexto.User.Username));
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
                else
                {
                    contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithColor(Color.Red)
                        .WithTitle(StringCatch.GetString("msgErroConfigPermission", "**{0}**, você precisa de permissão de Administrador para poder execultar esse comando 😔"))
                        .Build());
                }
            }
            else
            {
                contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithColor(Color.Red)
                        .WithTitle(StringCatch.GetString("xproleCargosFailCheck", "Esse comando so pode ser execultado em Servidores"))
                        .Build());
            }


        }

        public void welcomemsg(CommandContext context, object[] args)
        {
            if (!context.IsPrivate)
            {
                SocketGuildUser guildUser = context.User as SocketGuildUser;
                if (guildUser.GuildPermissions.Administrator)
                {
                    new BotCadastro((CommandContext cmdContext, object[] cmdArgs) =>
                    {
                        IMessage embed = cmdContext.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                .WithTitle(StringCatch.GetString("welcomemsgTitle1", "Configurar a mensagem de boas-vindas"))
                                .WithDescription(StringCatch.GetString("welcomemsgDesc1", "Você quer ligar a mensagem de boas vindas no seu servidor?"))
                                .AddField(StringCatch.GetString("welcomemmsgOpcsValidasTitle1", "Opções Validas:"), StringCatch.GetString("welcomemmsgOpcsValidas1", "s - Sim / Ligar\nn - Não / Desligar"))
                                .WithColor(Color.DarkPurple)
                            .Build()).GetAwaiter().GetResult();

                        SubCommandControler sub = new SubCommandControler();
                        IMessage msgresposta = sub.GetCommand(embed, cmdContext.User);

                        if(msgresposta.Content == "s" || msgresposta.Content == "n")
                        {
                            string msg = "";
                            if(msgresposta.Content == "s")
                            {
                                embed = cmdContext.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                        .WithTitle(StringCatch.GetString("welcomemsgTitle2", "Configurar a mensagem de boas-vindas"))
                                        .WithDescription(StringCatch.GetString("welcomemsgDesc2", "Digite a mensagem que você quer que eu mostre quando alguem entrar no servidor, se você não quer ter uma mensagem digite: ``%desativar%``"))
                                        .AddField(StringCatch.GetString("welcomemmsgOpcValidasTitle2", "Opções Validas:"), StringCatch.GetString("welcomemsgOpcsValidas2", "Qualquer tipo de texto, podendo usar até Embeds compativel com a Nadeko Bot e variaveis como %user%"))
                                        .WithColor(Color.DarkPurple)
                                    .Build()).GetAwaiter().GetResult();

                                sub = new SubCommandControler();
                                msgresposta = sub.GetCommand(embed, context.User);

                                msg = msgresposta.Content;
                            }
                            else
                            {
                                msg = "%desativar%";
                            }
                            BemVindoGoodByeMsg vindoGoodByeMsg = new BemVindoGoodByeMsg().setBemvindo((msg == "%desativar%") ? "" : msg);
                            new ConfiguracoesServidorDAO().SetWelcomeMsg(new ConfiguracoesServidor(new Servidores(cmdContext.Guild.Id), vindoGoodByeMsg));

                            cmdContext.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                    .WithColor(Color.Green)
                                    .WithTitle(StringCatch.GetString("welcomemsgSetOk", "Ok, farei tudo conforme o pedido 😃"))
                                .Build());

                        }
                        else
                        {
                            RotaFail(cmdContext);
                        }

                    }, context, args).EsperarOkDb();
                }
                else
                {
                    context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithDescription(StringCatch.GetString("welcomemsgSemPerm", "**{0}** você precisa da permissão: ``Administrador`` para usar esse comando"))
                            .WithColor(Color.Red)
                        .Build());
                }
            }
            else
            {
                context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithDescription(StringCatch.GetString("welcomemsgDm", "Esse comando só pode ser usado em servidores"))
                        .WithColor(Color.Red)
                    .Build());
            }
        }

        public void byemsg(CommandContext context, object[] args)
        {
            if (!context.IsPrivate)
            {
                SocketGuildUser guildUser = context.User as SocketGuildUser;
                if (guildUser.GuildPermissions.Administrator)
                {
                    new BotCadastro((CommandContext cmdContext, object[] cmdArgs) =>
                    {
                        IMessage embed = cmdContext.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                .WithTitle(StringCatch.GetString("byemsgTitle1", "Configurar a mensagem de saida"))
                                .WithDescription(StringCatch.GetString("byemsgDesc1", "Você quer ligar a mensagem de quando alguem sai do servidor?"))
                                .AddField(StringCatch.GetString("byeMsgOpcsValidasTitle1", "Opções Validas:"), StringCatch.GetString("byemsgOpcsValidas1", "s - Sim / Ligar\nn - Não / Desligar"))
                                .WithColor(Color.DarkPurple)
                            .Build()).GetAwaiter().GetResult();

                        SubCommandControler sub = new SubCommandControler();
                        IMessage msgresposta = sub.GetCommand(embed, cmdContext.User);

                        if (msgresposta.Content == "s" || msgresposta.Content == "n")
                        {
                            string msg = "";
                            if (msgresposta.Content == "s")
                            {
                                embed = cmdContext.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                        .WithTitle(StringCatch.GetString("byemsgTitle2", "Configurar a mensagem de saida"))
                                        .WithDescription(StringCatch.GetString("byemsgDesc2", "Digite a mensagem que você quer que eu mostre quando alguem sai do servidor, se você não quer ter uma mensagem digite: ``%desativar%``"))
                                        .AddField(StringCatch.GetString("byeMsgOpcsValidasTitle2", "Opções Validas:"), StringCatch.GetString("byemsgOpcsValidas2", "Qualquer tipo de texto, podendo usar até Embeds compativel com a Nadeko Bot e variaveis como %user%"))
                                        .WithColor(Color.DarkPurple)
                                    .Build()).GetAwaiter().GetResult();

                                sub = new SubCommandControler();
                                msgresposta = sub.GetCommand(embed, context.User);

                                msg = msgresposta.Content;
                            }
                            else
                            {
                                msg = "%desativar%";
                            }
                            BemVindoGoodByeMsg vindoGoodByeMsg = new BemVindoGoodByeMsg().setSair((msg == "%desativar%") ? "" : msg);
                            new ConfiguracoesServidorDAO().SetByeMsg(new ConfiguracoesServidor(new Servidores(cmdContext.Guild.Id), vindoGoodByeMsg));

                            cmdContext.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                    .WithColor(Color.Green)
                                    .WithTitle(StringCatch.GetString("byemsgSetOk", "Ok, farei tudo conforme o pedido 😃"))
                                .Build());

                        }
                        else
                        {
                            RotaFail(cmdContext);
                        }

                    }, context, args).EsperarOkDb();
                }
                else
                {
                    context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithDescription(StringCatch.GetString("welcomemsgSemPerm", "**{0}** você precisa da permissão: ``Administrador`` para usar esse comando"))
                            .WithColor(Color.Red)
                        .Build());
                }
            }
            else
            {
                context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithDescription(StringCatch.GetString("welcomemsgDm", "Esse comando só pode ser usado em servidores"))
                        .WithColor(Color.Red)
                    .Build());
            }
        }

        public void erromsg(CommandContext context, object[] args)
        {
            if (!context.IsPrivate)
            {
                SocketGuildUser guildUser = context.User as SocketGuildUser;
                if (guildUser.GuildPermissions.Administrator)
                {
                    new BotCadastro((CommandContext cmdContext, object[] cmdArgs) =>
                    {
                        IMessage embed = cmdContext.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                .WithTitle(StringCatch.GetString("erromsgTitle1", "Configurar a mensagem de erro"))
                                .WithDescription(StringCatch.GetString("erromsgDesc1", "Você quer que eu envia uma mensagem de erro quando alguem tenta usar algum comando que eu não tenho?"))
                                .AddField(StringCatch.GetString("erromsgOpcsValidasTitle1", "Opções Validas:"), StringCatch.GetString("erromsgOpcsValidas1", "s - Sim / Ligar\nn - Não / Desligar"))
                                .WithColor(Color.DarkPurple)
                            .Build()).GetAwaiter().GetResult();

                        SubCommandControler sub = new SubCommandControler();
                        IMessage msgresposta = sub.GetCommand(embed, cmdContext.User);

                        if (msgresposta.Content == "s" || msgresposta.Content == "n")
                        {
                            bool erroMsg = false;
                            if (msgresposta.Content == "s")
                            {
                                erroMsg = true;
                            }
                            new ConfiguracoesServidorDAO().SetErroMsg(new ConfiguracoesServidor(new Servidores(context.Guild.Id), new ErroMsg(erroMsg)));
                            cmdContext.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                    .WithColor(Color.Green)
                                    .WithTitle(StringCatch.GetString("erromsgSetOk", "Ok, farei tudo conforme o pedido 😃"))
                                .Build());

                        }
                        else
                        {
                            RotaFail(cmdContext);
                        }

                    }, context, args).EsperarOkDb();
                }
                else
                {
                    context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithDescription(StringCatch.GetString("erromsgSemPerm", "**{0}** você precisa da permissão: ``Administrador`` para usar esse comando"))
                            .WithColor(Color.Red)
                        .Build());
                }
            }
            else
            {
                context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithDescription(StringCatch.GetString("erromsgDM", "Esse comando só pode ser usado em servidores"))
                        .WithColor(Color.Red)
                    .Build());
            }
        }

    }
}
