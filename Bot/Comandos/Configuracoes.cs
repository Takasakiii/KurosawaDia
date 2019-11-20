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
                    

                    if (comando.Length > 1)
                    {
                        string msg = comando[1];
                        IUserMessage message = await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                .WithDescription(await StringCatch.GetStringAsync("setprefixCtz", "**{0}** você quer mudar o prefixo?", Contexto.User))
                                .WithFooter(await StringCatch.GetStringAsync("setprefixIgnorar", "se não apenas ignore essa mensagem"))
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
                                    .WithDescription(await StringCatch.GetStringAsync("setperfixAlterado", "**{0}** o prefixo do servidor foi alterado de: `{1}` para: `{2}`", Contexto.User.Username, PrefixoServidor, new string(servidor.Prefix)))
                                    .WithColor(Color.DarkPurple)
                                .Build());
                        }));
                    }
                    else
                    {
                        await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                .WithDescription(await StringCatch.GetStringAsync("setprefixFalarPrefixo", "**{0}** você precisa me falar um prefixo", Contexto.User.Username))
                                .AddField(await StringCatch.GetStringAsync("usoCmd", "Uso do Comando:"), await StringCatch.GetStringAsync("usoSetprefix", "`{0}setprefix <prefixo>`", PrefixoServidor))
                                .AddField(await StringCatch.GetStringAsync("exemploCmd", "Exemplo: "), await StringCatch.GetStringAsync("exemploCmd", "`{0}setprefix !`", PrefixoServidor))
                                .WithColor(Color.Red)
                            .Build());
                    }
                }
                else
                {
                    await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithDescription(await StringCatch.GetStringAsync("setprefixSemPerm", "**{0}**, você precisa de permissão de Gerenciar Servidor para poder usar esse comando 😔", Contexto.User.Username))
                            .WithColor(Color.Red)
                        .Build()); ;
                }

            }
            else
            {
                await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithDescription(await StringCatch.GetStringAsync("setprefixDm", "Esse comando so pode ser usado em servidores"))
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
                        embed.WithTitle(await StringCatch.GetStringAsync("xproleSetTitle", "**Configuração dos Pontos de Interação**"));
                        embed.WithDescription(await StringCatch.GetStringAsync("xproleSetDesc1", "Você deseja ligar os pontos de interação??(eles servem para medir a interação dos seus membros e setar cargos automaticamente)"));
                        embed.AddField(await StringCatch.GetStringAsync("xptoleSetF1", "Opções Validas:"), await StringCatch.GetStringAsync("xproleSetF1Desc", "s - Sim / Ligar\nn - Não / Desligar"));
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
                                    embed.WithDescription(await StringCatch.GetStringAsync("xproleSetDesc2", "Qual é o multiplicador de Pontos de Interação que deseja usar (esse multiplicador determina como sera medido a interação dos membros) [recomendamos o multiplicador 2]"));
                                    embed.Fields.Clear();
                                    embed.AddField(await StringCatch.GetStringAsync("xptoleSetF1", "Opções Validas:"), await StringCatch.GetStringAsync("xproleSet2F1Desc", "Qualquer numero a partir de 1.0"));
                                    pergunta = await Contexto.Channel.SendMessageAsync(embed: embed.Build());
                                    sub = new SubCommandControler();
                                    msgresposta = await sub.GetCommand(pergunta, Contexto.User);
                                    if (msgresposta != null && double.TryParse(msgresposta.Content, out rate))
                                    {
                                        if (rate > 1)
                                        {
                                            embed.WithDescription(await StringCatch.GetStringAsync("xproleSetDesc3", "Digite a messagem que você quer que eu mostre quando alguem conseguir um Ponto de Interação, se você não deseja ter uma mensagem apenas digite `%desativar%`"));
                                            embed.Fields.Clear();
                                            embed.AddField(await StringCatch.GetStringAsync("xptoleSetF1", "Opções Validas:"), await StringCatch.GetStringAsync("xproleSet3F1Desc", "Qualquer tipo de texto, podendo usar até Embeds compativel com a Nadeko Bot e variaveis como %user% e %pontos%"));
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
                                        .WithTitle(await StringCatch.GetStringAsync("xproleSetTitleOK", "Ok, farei tudo conforme o pedido 😃"))
                                        .Build());
                                }
                                else
                                {
                                    await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                        .WithColor(Color.Red)
                                        .WithTitle(await StringCatch.GetStringAsync("xproleSetTitleFail", "Desculpe mas ouve um problema ao tentar salvar suas preferencias, se for urgente contate meus criadores que eles vão te dar todo o suporte 😔"))
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
                            .WithTitle(await StringCatch.GetStringAsync("xproleCargosFailCheck", "**{0}**, o bot precisa da permissão de gerenciar cargos para executar esse comando 😔", Contexto.User.Username))
                            .Build());
                    }
                }
                else
                {
                    await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithColor(Color.Red)
                        .WithTitle(await StringCatch.GetStringAsync("msgErroConfigPermission", "**{0}**, você precisa de permissão de Administrador para poder executar esse comando 😔", Contexto.User.Username))
                        .Build());
                }
            }
            else
            {
                await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithColor(Color.Red)
                    .WithTitle(await StringCatch.GetStringAsync("xprolePrivateErro", "Desculpe, mas você só pode dar esse comando em um servidor"))
                    .Build());
            }
        }

        private async Task TimeOut()
        {
            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                .WithColor(Color.Red)
                .WithTitle(await StringCatch.GetStringAsync("timeoutFailTitle", "**{0}**, Tempo acabou 😶", Contexto.User.Username))
                .Build());
            return;
        }

        private async Task RotaFail()
        {
            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                .WithColor(Color.Red)
                .WithTitle(await StringCatch.GetStringAsync("rotafailtitle", "Desculpe, mas você terá que me falar um valor dentro do **Opções Validas**, se não eu não poderei te ajudar 😔"))
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
                                    .WithDescription(await StringCatch.GetStringAsync("welcomechOk", "**{0}** as mensagens de boas-vindas serão enviadas no canal: `#{1}`", Contexto.User.Username, canalModel.NomeCanal))
                                    .WithColor(Color.DarkPurple)
                                 .Build());
                        }
                        else
                        {
                            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                    .WithDescription(await StringCatch.GetStringAsync("welcomechNSetado", "**{0}** eu não consegui definir esse canal para mandar as boas-vindas", Contexto.User.Username))
                                    .WithColor(Color.Red)
                                .Build());
                        }
                    }
                    else
                    {
                        await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                .WithDescription(await StringCatch.GetStringAsync("welcomechSemCanal", "**{0}** eu não encontrei esse canal no servidor", Contexto.User.Username))
                                .WithColor(Color.Red)
                            .Build());
                    }
                }
                else
                {
                    await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithDescription(await StringCatch.GetStringAsync("welcomechSemPerm", "**{0}**, você precisa de permissão de Administrador para poder executar esse comando 😔", Contexto.User.Username))
                            .WithColor(Color.Red)
                        .Build());
                }
            }
            else
            {
                await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithDescription(await StringCatch.GetStringAsync("welcomechDm", "Esse comando só pode ser usado em servidores"))
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
                                    .WithDescription(await StringCatch.GetStringAsync("welcomechOk", "**{0}** as mensagens de saida serão enviadas no canal: `#{1}`", Contexto.User.Username, canalModel.NomeCanal))
                                    .WithColor(Color.DarkPurple)
                                 .Build());
                        }
                        else
                        {
                            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                    .WithDescription(await StringCatch.GetStringAsync("welcomechNSetado", "**{0}** eu não consegui definir esse canal para mandar as mensagens de saida", Contexto.User.Username))
                                    .WithColor(Color.Red)
                                .Build());
                        }
                    }
                    else
                    {
                       await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                .WithDescription(await StringCatch.GetStringAsync("welcomechSemCanal", "**{0}** eu não encontrei esse canal no servidor", Contexto.User.Username))
                                .WithColor(Color.Red)
                            .Build());
                    }
                }
                else
                {
                    await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithDescription(await StringCatch.GetStringAsync("welcomechSemPerm", "**{0}**, você precisa de permissão de Administrador para poder executar esse comando 😔", Contexto.User.Username))
                            .WithColor(Color.Red)
                        .Build());
                }
            }
            else
            {
                await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithDescription(await StringCatch.GetStringAsync("welcomechDm", "Esse comando só pode ser usado em servidores"))
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
                        .AddField(await StringCatch.GetStringAsync("addpicargoErrMsgUsageFtitle", "Uso do comando:"), await StringCatch.GetStringAsync("addpicargoErrMsgUsageFcontent", "`{0}picargo [QuantidadeDePIRequerido se o valor for menor ou igual a 0 o mesmo será removido] NomeCargo`", prefix))
                        .AddField(await StringCatch.GetStringAsync("addpicargoErrMsgExempleFtitle", "Exemplo do comando:"), await StringCatch.GetStringAsync("addpicargoErrMsgExempleFcontent", "`{0}piCargo 3 Membros`", prefix));

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
                            msgErro.WithTitle(await StringCatch.GetStringAsync("addpicargoErrTitleRoleNotFind", "**{0}**, o cargo não pode ser encontrado, por favor verifique se você digitou o nome/id do cargo corretamente.", Contexto.User.Username));
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
                                        .WithTitle(await StringCatch.GetStringAsync("addpicargofoi", "**{0}**, o cargo `{1}` foi {2} com sucesso 😃", Contexto.User.Username, cargoSelecionado.Name, (operacaoRetorno == CargosDAO.Operacao.Insert) ? StringCatch.GetStringAsync("addpicargoAdicionar", "adicionado") : (operacaoRetorno == CargosDAO.Operacao.Update) ? StringCatch.GetStringAsync("addpicargoAtualizado", "atualizado") : StringCatch.GetStringAsync("addpicargoDeletado", "removido")))
                                        .Build());
                                }
                                else
                                {
                                    msgErro.WithTitle(await StringCatch.GetStringAsync("addpicargoNFAdd", "Desculpe mas não consegui adicionar o cargo 😔", Contexto.User.Username));
                                    msgErro.Fields.Clear();
                                    await Contexto.Channel.SendMessageAsync(embed: msgErro.Build());
                                }
                            }
                            else
                            {
                                msgErro.WithTitle(await StringCatch.GetStringAsync("addpicargoErrTitlerequesito", "**{0}**, a quantidade de PI está invalida, por favor digite somente numero inteiros.", Contexto.User.Username));
                                await Contexto.Channel.SendMessageAsync(embed: msgErro.Build());
                            }
                        }
                    }
                    else
                    {
                        msgErro.WithTitle(await StringCatch.GetStringAsync("addpicargoErrTitleLess2", "**{0}**, você precisa adicionar enviar os parametros do comando.", Contexto.User.Username));
                        await Contexto.Channel.SendMessageAsync(embed: msgErro.Build());
                    }
                }
                else
                {
                    await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithColor(Color.Red)
                        .WithTitle(await StringCatch.GetStringAsync("msgErroConfigPermission", "**{0}**, você precisa de permissão de Administrador para poder executar esse comando 😔", Contexto.User.Username))
                        .Build());
                }
            }
            else
            {
                await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithColor(Color.Red)
                        .WithTitle(await StringCatch.GetStringAsync("xproleCargosFailCheck", "Esse comando so pode ser execultado em Servidores"))
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
                               .WithTitle(await StringCatch.GetStringAsync("welcomemsgTitle1", "Configurar a mensagem de boas-vindas"))
                               .WithDescription(await StringCatch.GetStringAsync("welcomemsgDesc1", "Você quer ligar a mensagem de boas vindas no seu servidor?"))
                               .AddField(await StringCatch.GetStringAsync("welcomemmsgOpcsValidasTitle1", "Opções Validas:"), await StringCatch.GetStringAsync("welcomemmsgOpcsValidas1", "s - Sim / Ligar\nn - Não / Desligar"))
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
                                    .WithTitle(await StringCatch.GetStringAsync("welcomemsgTitle2", "Configurar a mensagem de boas-vindas"))
                                    .WithDescription(await StringCatch.GetStringAsync("welcomemsgDesc2", "Digite a mensagem que você quer que eu mostre quando alguem entrar no servidor, se você não quer ter uma mensagem digite: ``%desativar%``"))
                                    .AddField(await StringCatch.GetStringAsync("welcomemmsgOpcValidasTitle2", "Opções Validas:"), await StringCatch.GetStringAsync("welcomemsgOpcsValidas2", "Qualquer tipo de texto, podendo usar até Embeds compativel com a Nadeko Bot e variaveis como %user%"))
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
                                .WithTitle(await StringCatch.GetStringAsync("welcomemsgSetOk", "Ok, farei tudo conforme o pedido 😃"))
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
                            .WithDescription(await StringCatch.GetStringAsync("welcomemsgSemPerm", "**{0}**, você precisa de permissão de Administrador para poder usar esse comando 😔", Contexto.User.Username))
                            .WithColor(Color.Red)
                        .Build());
                }
            }
            else
            {
                await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithDescription(await StringCatch.GetStringAsync("welcomemsgDm", "Esse comando só pode ser usado em servidores"))
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
                                .WithTitle(await StringCatch.GetStringAsync("byemsgTitle1", "Configurar a mensagem de saida"))
                                .WithDescription(await StringCatch.GetStringAsync("byemsgDesc1", "Você quer ligar a mensagem de quando alguem sai do servidor?"))
                                .AddField(await StringCatch.GetStringAsync("byeMsgOpcsValidasTitle1", "Opções Validas:"), await StringCatch.GetStringAsync("byemsgOpcsValidas1", "s - Sim / Ligar\nn - Não / Desligar"))
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
                                    .WithTitle(await StringCatch.GetStringAsync("byemsgTitle2", "Configurar a mensagem de saida"))
                                    .WithDescription(await StringCatch.GetStringAsync("byemsgDesc2", "Digite a mensagem que você quer que eu mostre quando alguem sai do servidor, se você não quer ter uma mensagem digite: ``%desativar%``"))
                                    .AddField(await StringCatch.GetStringAsync("byeMsgOpcsValidasTitle2", "Opções Validas:"), await StringCatch.GetStringAsync("byemsgOpcsValidas2", "Qualquer tipo de texto, podendo usar até Embeds compativel com a Nadeko Bot e variaveis como %user%"))
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
                                .WithTitle(await StringCatch.GetStringAsync("byemsgSetOk", "Ok, farei tudo conforme o pedido 😃"))
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
                            .WithDescription(await StringCatch.GetStringAsync("welcomemsgSemPerm", "**{0}**, você precisa de permissão de Administrador para poder usar esse comando 😔", Contexto.User.Username))
                            .WithColor(Color.Red)
                        .Build());
                }
            }
            else
            {
                await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithDescription(await StringCatch.GetStringAsync("welcomemsgDm", "Esse comando só pode ser usado em servidores"))
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
                                .WithTitle(await StringCatch.GetStringAsync("erromsgTitle1", "Configurar a mensagem de erro"))
                                .WithDescription(await StringCatch.GetStringAsync("erromsgDesc1", "Você quer que eu envia uma mensagem de erro quando alguem tenta usar algum comando que eu não tenho?"))
                                .AddField(await StringCatch.GetStringAsync("erromsgOpcsValidasTitle1", "Opções Validas:"), await StringCatch.GetStringAsync("erromsgOpcsValidas1", "s - Sim / Ligar\nn - Não / Desligar"))
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
                                .WithTitle(await StringCatch.GetStringAsync("erromsgSetOk", "Ok, farei tudo conforme o pedido 😃"))
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
                            .WithDescription(await StringCatch.GetStringAsync("erromsgSemPerm", "**{0}**, você precisa de permissão de Administrador para poder usar esse comando 😔", Contexto.User.Username))
                            .WithColor(Color.Red)
                        .Build());
                }
            }
            else
            {
                await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithDescription(await StringCatch.GetStringAsync("erromsgDM", "Esse comando só pode ser usado em servidores"))
                        .WithColor(Color.Red)
                    .Build());
            }
        }

    }
}
