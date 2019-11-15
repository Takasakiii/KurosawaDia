using Bot.Extensions;
using Bot.GenericTypes;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using MainDatabaseControler.DAO;
using MainDatabaseControler.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MainDatabaseControler.Modelos.Canais;
using static MainDatabaseControler.Modelos.ConfiguracoesServidor;

namespace Bot.Comandos
{
    public class Configuracoes : GenericModule
    {
        public Configuracoes(CommandContext contexto, string prefixo, string[] comando) : base(contexto, prefixo, comando)
        { 

        }
        


        public async Task setprefix()
        {
            if (!Contexto.IsPrivate)
            {
                SocketGuildUser userGuild = Contexto.User as SocketGuildUser;
                if (userGuild.GuildPermissions.ManageGuild)
                {
                    string[] comando = Comando;
                    string msg = comando[1];

                    if (msg != "")
                    {
                        IUserMessage message = await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                .WithDescription(await StringCatch.GetString("setprefixCtz", "**{0}** você quer mudar o prefixo?", Contexto.User))
                                .WithFooter(await StringCatch.GetString("setprefixIgnorar", "se não apenas ignore essa mensagem"))
                                .WithColor(Color.DarkPurple)
                            .Build());

                        Emoji emoji = new Emoji("✅");
                        await message.AddReactionAsync(emoji);

                        ReactionControler reaction = new ReactionControler();
                        reaction.GetReaction(message, emoji, Contexto.User, new ReturnMethod(async () =>
                        {
                            Servidores servidor = new Servidores(Contexto.Guild.Id, msg.ToCharArray());

                            Tuple<bool, Servidores> res = await new ServidoresDAO().SetServidorPrefixAsync(servidor);
                            servidor = res.Item2;
                            await message.DeleteAsync();
                            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                    .WithDescription(await StringCatch.GetString("setperfixAlterado", "**{0}** o prefixo do servidor foi alterado de: `{1}` para: `{2}`", Contexto.User.Username, PrefixoServidor, new string(servidor.Prefix)))
                                    .WithColor(Color.DarkPurple)
                                .Build());
                        }));
                    }
                    else
                    {
                        await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                .WithDescription(await StringCatch.GetString("setprefixFalarPrefixo", "**{0}** você precisa me falar um prefixo", Contexto.User.Username))
                                .AddField(await StringCatch.GetString("usoCmd", "Uso do Comando:"), await StringCatch.GetString("usoSetprefix", "`{0}setprefix <prefixo>`", PrefixoServidor))
                                .AddField(await StringCatch.GetString("exemploCmd", "Exemplo: "), await StringCatch.GetString("exemploCmd", "`{0}setprefix !`", PrefixoServidor))
                                .WithColor(Color.Red)
                            .Build());
                    }
                }
                else
                {
                    await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithDescription(await StringCatch.GetString("setprefixSemPerm", "**{0}**, você precisa de permissão de Gerenciar Servidor para poder usar esse comando 😔", Contexto.User.Username))
                            .WithColor(Color.Red)
                        .Build()); ;
                }

            }
            else
            {
                await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithDescription(await StringCatch.GetString("setprefixDm", "Esse comando so pode ser usado em servidores"))
                        .WithColor(Color.Red)
                    .Build());
            }
        }

        public async Task piconf()
        {
            if (!Contexto.IsPrivate)
            {
                SocketGuildUser usuarioinGuild = Contexto.User as SocketGuildUser;
                if (usuarioinGuild.GuildPermissions.Administrator)
                {
                    SocketGuildUser botRepresentacao = await Contexto.Guild.GetCurrentUserAsync() as SocketGuildUser;
                    if (botRepresentacao.GuildPermissions.ManageRoles)
                    {
                        EmbedBuilder embed = new EmbedBuilder();
                        embed.WithColor(Color.DarkPurple);
                        embed.WithColor(Color.Purple);
                        embed.WithTitle(await StringCatch.GetString("xproleSetTitle", "**Configuração dos Pontos de Interação**"));
                        embed.WithDescription(await StringCatch.GetString("xproleSetDesc1", "Você deseja ligar os pontos de interação??(eles servem para medir a interação dos seus membros e setar cargos automaticamente)"));
                        embed.AddField(await StringCatch.GetString("xptoleSetF1", "Opções Validas:"), await StringCatch.GetString("xproleSetF1Desc", "s - Sim / Ligar\nn - Não / Desligar"));
                        IMessage pergunta = await Contexto.Channel.SendMessageAsync(embed: embed.Build());
                        SubCommandControler sub = new SubCommandControler();
                        IMessage msgresposta = await sub.GetCommand(pergunta, Contexto.User);
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
                                    embed.WithDescription(await StringCatch.GetString("xproleSetDesc2", "Qual é o multiplicador de Pontos de Interação que deseja usar (esse multiplicador determina como sera medido a interação dos membros) [recomendamos o multiplicador 2]"));
                                    embed.Fields.Clear();
                                    embed.AddField(await StringCatch.GetString("xptoleSetF1", "Opções Validas:"), await StringCatch.GetString("xproleSet2F1Desc", "Qualquer numero a partir de 1.0"));
                                    pergunta = await Contexto.Channel.SendMessageAsync(embed: embed.Build());
                                    sub = new SubCommandControler();
                                    msgresposta = await sub.GetCommand(pergunta, Contexto.User);
                                    if (msgresposta != null && double.TryParse(msgresposta.Content, out rate))
                                    {
                                        if (rate > 1)
                                        {
                                            embed.WithDescription(await StringCatch.GetString("xproleSetDesc3", "Digite a messagem que você quer que eu mostre quando alguem conseguir um Ponto de Interação, se você não deseja ter uma mensagem apenas digite `%desativar%`"));
                                            embed.Fields.Clear();
                                            embed.AddField(await StringCatch.GetString("xptoleSetF1", "Opções Validas:"), await StringCatch.GetString("xproleSet3F1Desc", "Qualquer tipo de texto, podendo usar até Embeds compativel com a Nadeko Bot e variaveis como %user% e %pontos%"));
                                            pergunta = await Contexto.Channel.SendMessageAsync(embed: embed.Build());
                                            sub = new SubCommandControler();
                                            msgresposta = await sub.GetCommand(pergunta, Contexto.User);
                                            msg = msgresposta.Content;
                                        }
                                        else
                                        {
                                            await RotaFail();
                                        }
                                    }
                                    else
                                    {
                                        await RotaFail();
                                    }
                                }
                                else
                                {
                                    ativado = false;
                                }
                                PI pimodel = new PI(ativado, rate, (msg == "%desativar%") ? "" : msg);
                                if (await new ConfiguracoesServidorDAO().SalvarPIConfigAsync(new ConfiguracoesServidor(new Servidores(Contexto.Guild.Id), pimodel)))
                                {
                                    await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                        .WithColor(Color.Green)
                                        .WithTitle(await StringCatch.GetString("xproleSetTitleOK", "Ok, farei tudo conforme o pedido 😃"))
                                        .Build());
                                }
                                else
                                {
                                    await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                        .WithColor(Color.Red)
                                        .WithTitle(await StringCatch.GetString("xproleSetTitleFail", "Desculpe mas ouve um problema ao tentar salvar suas preferencias, se for urgente contate meus criadores que eles vão te dar todo o suporte 😔"))
                                        .Build());
                                }
                            }
                            else
                            {
                                await RotaFail();
                            }


                        }
                    }
                    else
                    {
                        await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithColor(Color.Red)
                            .WithTitle(await StringCatch.GetString("xproleCargosFailCheck", "**{0}**, o bot precisa da permissão de gerenciar cargos para executar esse comando 😔", Contexto.User.Username))
                            .Build());
                    }
                }
                else
                {
                    await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithColor(Color.Red)
                        .WithTitle(await StringCatch.GetString("msgErroConfigPermission", "**{0}**, você precisa de permissão de Administrador para poder executar esse comando 😔", Contexto.User.Username))
                        .Build());
                }
            }
            else
            {
                await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithColor(Color.Red)
                    .WithTitle(await StringCatch.GetString("xprolePrivateErro", "Desculpe, mas você só pode dar esse comando em um servidor"))
                    .Build());
            }
        }

        private async Task TimeOut()
        {
            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                .WithColor(Color.Red)
                .WithTitle(await StringCatch.GetString("timeoutFailTitle", "**{0}**, Tempo acabou 😶", Contexto.User.Username))
                .Build());
            return;
        }

        private async Task RotaFail()
        {
            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                .WithColor(Color.Red)
                .WithTitle(await StringCatch.GetString("rotafailtitle", "Desculpe, mas você terá que me falar um valor dentro do **Opções Validas**, se não eu não poderei te ajudar 😔"))
                .Build());
        }

        public async Task welcomech()
        {
            if (!Contexto.IsPrivate)
            {
                SocketGuildUser guildUser = Contexto.User as SocketGuildUser;
                if (guildUser.GuildPermissions.Administrator)
                {
                    string id = "";
                    string[] comando = Comando;
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
                        canal = await Contexto.Guild.GetChannelAsync(Convert.ToUInt64(id));
                    }
                    catch
                    {
                        canal = Contexto.Channel;
                    }

                    if (canal != null)
                    {
                        Canais canalModel = new Canais(canal.Id, new Servidores(Contexto.Guild.Id), TiposCanais.bemvindoCh, canal.Name);
                        if (await new CanaisDAO().AddChAsync(canalModel))
                        {
                            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                    .WithDescription(await StringCatch.GetString("welcomechOk", "**{0}** as mensagens de boas-vindas serão enviadas no canal: `#{1}`", Contexto.User.Username, canalModel.NomeCanal))
                                    .WithColor(Color.DarkPurple)
                                 .Build());
                        }
                        else
                        {
                            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                    .WithDescription(await StringCatch.GetString("welcomechNSetado", "**{0}** eu não consegui definir esse canal para mandar as boas-vindas", Contexto.User.Username))
                                    .WithColor(Color.Red)
                                .Build());
                        }
                    }
                    else
                    {
                        await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                .WithDescription(await StringCatch.GetString("welcomechSemCanal", "**{0}** eu não encontrei esse canal no servidor", Contexto.User.Username))
                                .WithColor(Color.Red)
                            .Build());
                    }
                }
                else
                {
                    await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithDescription(await StringCatch.GetString("welcomechSemPerm", "**{0}**, você precisa de permissão de Administrador para poder executar esse comando 😔", Contexto.User.Username))
                            .WithColor(Color.Red)
                        .Build());
                }
            }
            else
            {
                await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithDescription(await StringCatch.GetString("welcomechDm", "Esse comando só pode ser usado em servidores"))
                        .WithColor(Color.Red)
                    .Build());
            }
        }

        public async Task byech()
        {
            if (!Contexto.IsPrivate)
            {
                SocketGuildUser guildUser = Contexto.User as SocketGuildUser;
                if (guildUser.GuildPermissions.Administrator)
                {
                    string id = "";
                    string[] comando = Comando;
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
                        canal = await Contexto.Guild.GetChannelAsync(Convert.ToUInt64(id));
                    }
                    catch
                    {
                        canal = Contexto.Channel;
                    }

                    if (canal != null)
                    {
                        Canais canalModel = new Canais(canal.Id, new Servidores(Contexto.Guild.Id), TiposCanais.sairCh, canal.Name);
                        if (await new CanaisDAO().AddChAsync(canalModel))
                        {
                            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                    .WithDescription(await StringCatch.GetString("welcomechOk", "**{0}** as mensagens de saida serão enviadas no canal: `#{1}`", Contexto.User.Username, canalModel.NomeCanal))
                                    .WithColor(Color.DarkPurple)
                                 .Build());
                        }
                        else
                        {
                            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                    .WithDescription(await StringCatch.GetString("welcomechNSetado", "**{0}** eu não consegui definir esse canal para mandar as mensagens de saida", Contexto.User.Username))
                                    .WithColor(Color.Red)
                                .Build());
                        }
                    }
                    else
                    {
                       await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                .WithDescription(await StringCatch.GetString("welcomechSemCanal", "**{0}** eu não encontrei esse canal no servidor", Contexto.User.Username))
                                .WithColor(Color.Red)
                            .Build());
                    }
                }
                else
                {
                    await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithDescription(await StringCatch.GetString("welcomechSemPerm", "**{0}**, você precisa de permissão de Administrador para poder executar esse comando 😔", Contexto.User.Username))
                            .WithColor(Color.Red)
                        .Build());
                }
            }
            else
            {
                await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithDescription(await StringCatch.GetString("welcomechDm", "Esse comando só pode ser usado em servidores"))
                        .WithColor(Color.Red)
                    .Build());
            }
        }

        public async Task picargo()
        {
            if (!Contexto.IsPrivate)
            {
                SocketGuildUser userGuild = Contexto.User as SocketGuildUser;
                if (userGuild.GuildPermissions.Administrator)
                {
                    string[] comandoargs = Comando;
                    string prefix = PrefixoServidor;
                    EmbedBuilder msgErro = new EmbedBuilder()
                        .WithColor(Color.Red)
                        .AddField(await StringCatch.GetString("addpicargoErrMsgUsageFtitle", "Uso do comando:"), await StringCatch.GetString("addpicargoErrMsgUsageFcontent", "`{0}picargo [QuantidadeDePIRequerido se o valor for menor ou igual a 0 o mesmo será removido] NomeCargo`", prefix))
                        .AddField(await StringCatch.GetString("addpicargoErrMsgExempleFtitle", "Exemplo do comando:"), await StringCatch.GetString("addpicargoErrMsgExempleFcontent", "`{0}piCargo 3 Membros`", prefix));

                    if (comandoargs.Length > 2)
                    {
                        string nomerole = string.Join(" ", comandoargs, 2, comandoargs.Length - 2);
                        List<IRole> cargos = Contexto.Guild.Roles.ToList();
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
                            msgErro.WithTitle(await StringCatch.GetString("addpicargoErrTitleRoleNotFind", "**{0}**, o cargo não pode ser encontrado, por favor verifique se você digitou o nome/id do cargo corretamente.", Contexto.User.Username));
                            await Contexto.Channel.SendMessageAsync(embed: msgErro.Build());
                        }
                        else
                        {
                            long requesito;
                            if (long.TryParse(comandoargs[1], out requesito))
                            {
                                Servidores servidor = new Servidores(Contexto.Guild.Id, Contexto.Guild.Name);
                                Cargos cargoCadastro = new Cargos(Cargos.Tipos_Cargos.XpRole, Convert.ToUInt64(cargoSelecionado.Id), cargoSelecionado.Name, requesito, servidor);
                                CargosDAO dao = new CargosDAO();
                                CargosDAO.Operacao operacaoRetorno = await dao.AdicionarAtualizarCargoAsync(cargoCadastro);
                                if (operacaoRetorno != CargosDAO.Operacao.Incompleta)
                                {
                                    await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                        .WithColor(Color.Green)
                                        .WithTitle(await StringCatch.GetString("addpicargofoi", "**{0}**, o cargo `{1}` foi {2} com sucesso 😃", Contexto.User.Username, cargoSelecionado.Name, (operacaoRetorno == CargosDAO.Operacao.Insert) ? StringCatch.GetString("addpicargoAdicionar", "adicionado") : (operacaoRetorno == CargosDAO.Operacao.Update) ? StringCatch.GetString("addpicargoAtualizado", "atualizado") : StringCatch.GetString("addpicargoDeletado", "removido")))
                                        .Build());
                                }
                                else
                                {
                                    msgErro.WithTitle(await StringCatch.GetString("addpicargoNFAdd", "Desculpe mas não consegui adicionar o cargo 😔", Contexto.User.Username));
                                    msgErro.Fields.Clear();
                                    await Contexto.Channel.SendMessageAsync(embed: msgErro.Build());
                                }
                            }
                            else
                            {
                                msgErro.WithTitle(await StringCatch.GetString("addpicargoErrTitlerequesito", "**{0}**, a quantidade de PI está invalida, por favor digite somente numero inteiros.", Contexto.User.Username));
                                await Contexto.Channel.SendMessageAsync(embed: msgErro.Build());
                            }
                        }
                    }
                    else
                    {
                        msgErro.WithTitle(await StringCatch.GetString("addpicargoErrTitleLess2", "**{0}**, você precisa adicionar enviar os parametros do comando.", Contexto.User.Username));
                        await Contexto.Channel.SendMessageAsync(embed: msgErro.Build());
                    }
                }
                else
                {
                    await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithColor(Color.Red)
                        .WithTitle(await StringCatch.GetString("msgErroConfigPermission", "**{0}**, você precisa de permissão de Administrador para poder executar esse comando 😔", Contexto.User.Username))
                        .Build());
                }
            }
            else
            {
                await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithColor(Color.Red)
                        .WithTitle(await StringCatch.GetString("xproleCargosFailCheck", "Esse comando so pode ser execultado em Servidores"))
                        .Build());
            }


        }

        public async Task welcomemsg()
        {
            if (!Contexto.IsPrivate)
            {
                SocketGuildUser guildUser = Contexto.User as SocketGuildUser;
                if (guildUser.GuildPermissions.Administrator)
                {
                    IMessage embed = await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                               .WithTitle(await StringCatch.GetString("welcomemsgTitle1", "Configurar a mensagem de boas-vindas"))
                               .WithDescription(await StringCatch.GetString("welcomemsgDesc1", "Você quer ligar a mensagem de boas vindas no seu servidor?"))
                               .AddField(await StringCatch.GetString("welcomemmsgOpcsValidasTitle1", "Opções Validas:"), await StringCatch.GetString("welcomemmsgOpcsValidas1", "s - Sim / Ligar\nn - Não / Desligar"))
                               .WithColor(Color.DarkPurple)
                           .Build());

                    SubCommandControler sub = new SubCommandControler();
                    IMessage msgresposta = await sub.GetCommand(embed, Contexto.User);

                    if (msgresposta.Content == "s" || msgresposta.Content == "n")
                    {
                        string msg = "";
                        if (msgresposta.Content == "s")
                        {
                            embed = await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                    .WithTitle(await StringCatch.GetString("welcomemsgTitle2", "Configurar a mensagem de boas-vindas"))
                                    .WithDescription(await StringCatch.GetString("welcomemsgDesc2", "Digite a mensagem que você quer que eu mostre quando alguem entrar no servidor, se você não quer ter uma mensagem digite: ``%desativar%``"))
                                    .AddField(await StringCatch.GetString("welcomemmsgOpcValidasTitle2", "Opções Validas:"), await StringCatch.GetString("welcomemsgOpcsValidas2", "Qualquer tipo de texto, podendo usar até Embeds compativel com a Nadeko Bot e variaveis como %user%"))
                                    .WithColor(Color.DarkPurple)
                                .Build());

                            sub = new SubCommandControler();
                            msgresposta = await sub.GetCommand(embed, Contexto.User);

                            msg = msgresposta.Content;
                        }
                        else
                        {
                            msg = "%desativar%";
                        }
                        BemVindoGoodByeMsg vindoGoodByeMsg = new BemVindoGoodByeMsg().setBemvindo((msg == "%desativar%") ? "" : msg);
                        await new ConfiguracoesServidorDAO().SetWelcomeMsgAsync(new ConfiguracoesServidor(new Servidores(Contexto.Guild.Id), vindoGoodByeMsg));

                        await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                .WithColor(Color.Green)
                                .WithTitle(await StringCatch.GetString("welcomemsgSetOk", "Ok, farei tudo conforme o pedido 😃"))
                            .Build());

                    }
                    else
                    {
                        await RotaFail();
                    }

                }
                else
                {
                    await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithDescription(await StringCatch.GetString("welcomemsgSemPerm", "**{0}**, você precisa de permissão de Administrador para poder usar esse comando 😔", Contexto.User.Username))
                            .WithColor(Color.Red)
                        .Build());
                }
            }
            else
            {
                await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithDescription(await StringCatch.GetString("welcomemsgDm", "Esse comando só pode ser usado em servidores"))
                        .WithColor(Color.Red)
                    .Build());
            }
        }

        public async Task byemsg()
        {
            if (!Contexto.IsPrivate)
            {
                SocketGuildUser guildUser = Contexto.User as SocketGuildUser;
                if (guildUser.GuildPermissions.Administrator)
                {
                    IMessage embed = await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                .WithTitle(await StringCatch.GetString("byemsgTitle1", "Configurar a mensagem de saida"))
                                .WithDescription(await StringCatch.GetString("byemsgDesc1", "Você quer ligar a mensagem de quando alguem sai do servidor?"))
                                .AddField(await StringCatch.GetString("byeMsgOpcsValidasTitle1", "Opções Validas:"), await StringCatch.GetString("byemsgOpcsValidas1", "s - Sim / Ligar\nn - Não / Desligar"))
                                .WithColor(Color.DarkPurple)
                            .Build());

                    SubCommandControler sub = new SubCommandControler();
                    IMessage msgresposta = await sub.GetCommand(embed, Contexto.User);

                    if (msgresposta.Content == "s" || msgresposta.Content == "n")
                    {
                        string msg = "";
                        if (msgresposta.Content == "s")
                        {
                            embed = await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                    .WithTitle(await StringCatch.GetString("byemsgTitle2", "Configurar a mensagem de saida"))
                                    .WithDescription(await StringCatch.GetString("byemsgDesc2", "Digite a mensagem que você quer que eu mostre quando alguem sai do servidor, se você não quer ter uma mensagem digite: ``%desativar%``"))
                                    .AddField(await StringCatch.GetString("byeMsgOpcsValidasTitle2", "Opções Validas:"), await StringCatch.GetString("byemsgOpcsValidas2", "Qualquer tipo de texto, podendo usar até Embeds compativel com a Nadeko Bot e variaveis como %user%"))
                                    .WithColor(Color.DarkPurple)
                                .Build());

                            sub = new SubCommandControler();
                            msgresposta = await sub.GetCommand(embed, Contexto.User);

                            msg = msgresposta.Content;
                        }
                        else
                        {
                            msg = "%desativar%";
                        }
                        BemVindoGoodByeMsg vindoGoodByeMsg = new BemVindoGoodByeMsg().setSair((msg == "%desativar%") ? "" : msg);
                        await new ConfiguracoesServidorDAO().SetByeMsgAsync(new ConfiguracoesServidor(new Servidores(Contexto.Guild.Id), vindoGoodByeMsg));

                        await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                .WithColor(Color.Green)
                                .WithTitle(await StringCatch.GetString("byemsgSetOk", "Ok, farei tudo conforme o pedido 😃"))
                            .Build());

                    }
                    else
                    {
                        await RotaFail();
                    }

                }
                else
                {
                    await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithDescription(await StringCatch.GetString("welcomemsgSemPerm", "**{0}**, você precisa de permissão de Administrador para poder usar esse comando 😔", Contexto.User.Username))
                            .WithColor(Color.Red)
                        .Build());
                }
            }
            else
            {
                await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithDescription(await StringCatch.GetString("welcomemsgDm", "Esse comando só pode ser usado em servidores"))
                        .WithColor(Color.Red)
                    .Build());
            }
        }

        public async Task erromsg()
        {
            if (!Contexto.IsPrivate)
            {
                SocketGuildUser guildUser = Contexto.User as SocketGuildUser;
                if (guildUser.GuildPermissions.Administrator)
                {
                    IMessage embed = await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                .WithTitle(await StringCatch.GetString("erromsgTitle1", "Configurar a mensagem de erro"))
                                .WithDescription(await StringCatch.GetString("erromsgDesc1", "Você quer que eu envia uma mensagem de erro quando alguem tenta usar algum comando que eu não tenho?"))
                                .AddField(await StringCatch.GetString("erromsgOpcsValidasTitle1", "Opções Validas:"), await StringCatch.GetString("erromsgOpcsValidas1", "s - Sim / Ligar\nn - Não / Desligar"))
                                .WithColor(Color.DarkPurple)
                            .Build());

                    SubCommandControler sub = new SubCommandControler();
                    IMessage msgresposta = await sub.GetCommand(embed, Contexto.User);

                    if (msgresposta.Content == "s" || msgresposta.Content == "n")
                    {
                        bool erroMsg = false;
                        if (msgresposta.Content == "s")
                        {
                            erroMsg = true;
                        }
                        await new ConfiguracoesServidorDAO().SetErroMsgAsync(new ConfiguracoesServidor(new Servidores(Contexto.Guild.Id), new ErroMsg(erroMsg)));
                        await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                .WithColor(Color.Green)
                                .WithTitle(await StringCatch.GetString("erromsgSetOk", "Ok, farei tudo conforme o pedido 😃"))
                            .Build());

                    }
                    else
                    {
                        await RotaFail();
                    }

                }
                else
                {
                    await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithDescription(await StringCatch.GetString("erromsgSemPerm", "**{0}**, você precisa de permissão de Administrador para poder usar esse comando 😔", Contexto.User.Username))
                            .WithColor(Color.Red)
                        .Build());
                }
            }
            else
            {
                await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithDescription(await StringCatch.GetString("erromsgDM", "Esse comando só pode ser usado em servidores"))
                        .WithColor(Color.Red)
                    .Build());
            }
        }

    }
}
