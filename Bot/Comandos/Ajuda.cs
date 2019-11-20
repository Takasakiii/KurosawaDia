using Bot.Extensions;
using Bot.GenericTypes;
using Bot.Singletons;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using MainDatabaseControler.DAO;
using MainDatabaseControler.Modelos;
using System;
using System.Reflection;
using System.Threading.Tasks;
using static MainDatabaseControler.Modelos.ConfiguracoesServidor;
using static MainDatabaseControler.Modelos.Servidores;

namespace Bot.Comandos
{
    public class Ajuda : GenericModule
    {
        public Ajuda (CommandContext contexto, string prefixo, string[] comando) : base (contexto, prefixo, comando)
        {
            
        }

        

        public async Task ajuda()
        {
            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                .WithColor(Color.DarkPurple)
                .WithTitle(await StringCatch.GetString("ajudaTitle", "Sera um enorme prazer te ajudar 😋"))
                .WithDescription(await StringCatch.GetString("ajudaDesctiption", "Eu me chamo Kurosawa Dia, sou presidente do conselho de classe, idol e tambem ajudo as pessoas com algumas coisinhas no discord 😉\n"
                + "Se você usar `{0}comandos` no chat vai aparecer tudo que eu posso fazer atualmente (isso não é demais 😁)\n"
                + "Serio estou muito ansiosa para passar um tempo com você e tambem te ajudar XD\n"
                + "Se você tem ideias de mais coisas que possa fazer por favor mande uma sugestao com o `{0}sugestao`\n\n"
                + "E como a Mari fala Let's Go!!", PrefixoServidor))
                .WithFooter(await StringCatch.GetString("ajudaProjeto", "Kurosawa Dia é um projeto feito com amor e carinho financiado pelo Zuraaa!"), await StringCatch.GetString("ajudaImg", "https://i.imgur.com/Cm8grM4.png"))
                .WithImageUrl("https://i.imgur.com/PC5QDiX.png")
                .Build()
                );

        }

        enum ListaModulos { nada, ajuda, utilidade, moderacao, nsfw, weeb, imgs, cr, config, especial};
        public async Task comandos()
        {
            string[] comando = Comando;
            string msg = string.Join(" ", comando, 1, (comando.Length - 1)).ToLowerInvariant();

            if (!string.IsNullOrEmpty(msg))
            {
                ListaModulos modulo = ListaModulos.nada;
                if(int.TryParse(msg, out int result))
                {
                    modulo = (ListaModulos)result;
                }

                if(modulo ==  ListaModulos.ajuda || msg == "ajuda")
                {
                    await help();
                }
                else if (modulo == ListaModulos.utilidade || msg == "utilidade")
                {
                    await utilidade();
                }
                else if (modulo == ListaModulos.moderacao || msg == "moderação" || msg == "moderacao")
                {
                    await moderacao();
                }
                else if (modulo == ListaModulos.nsfw || msg == "nsfw")
                {
                    await nsfw();
                }
                else if (modulo == ListaModulos.weeb || msg == "weeb")
                {
                    await weeb();
                }
                else if (modulo == ListaModulos.imgs || msg == "imagens" || msg == "imgs")
                {
                    await img();
                }
                else if (modulo == ListaModulos.cr || msg == "reações customizadas" || msg == "reacoes customizadas")
                {
                    await customReaction();
                }
                else if (modulo == ListaModulos.config || msg == "configurações" || msg == "configuracoes")
                {
                    await configuracoes();
                }
                else if (modulo == ListaModulos.especial || msg == "especiais")
                {
                    if (!Contexto.IsPrivate)
                    {
                        Servidores servidor = new Servidores(Contexto.Guild.Id);
                        Tuple<bool, Servidores> res = await new ServidoresDAO().GetPermissoesAsync(servidor);
                        servidor = res.Item2;
                        if (res.Item1)
                        {
                            if (servidor.Permissoes == PermissoesServidores.ServidorPika)
                            {
                                await especial();
                            }
                            else
                            {
                                await modulos();
                            }
                        }
                    }
                    else
                    {
                        await modulos();
                    }
                }
                else
                {
                    await modulos();
                }
            }
            else
            {
                await modulos();
            }
        }

        //public async Task convite()
        //{
        //    await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
        //            .WithTitle(await StringCatch.GetString("conviteTxt", "Aqui estão meus convites: "))
        //            .WithDescription(await StringCatch.GetString("conviteConvites", "[Me convide para o seu servidor](https://ayura.com.br/links/bot)\n[Entre no meu servidor](https://ayura.com.br/dia)")) //shrug
        //            .WithColor(Color.DarkPurple)
        //     .Build());
        //}

        public async Task info()
        {
            DiscordShardedClient client = Contexto.Client as DiscordShardedClient;
            int users = 0;
            foreach (SocketGuild servidor in client.Guilds)
            {
                users += servidor.Users.Count;
            }
            


            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle(await StringCatch.GetString("infoTxt", "Dia's Book:"))
                    .WithDescription(await StringCatch.GetString("infoDescription", "Espero que não faça nada estranho com minhas informações, to zuando kkkkkk 😝"))
                    .AddField(await StringCatch.GetString("infoBot", "**Sobre mim:**"), await StringCatch.GetString("infoInfos", "__Nome:__ Kurosawa Dia (Dia - Chan)\n__Aniversario:__ 01 de Janeiro (Quero Presentes)\n__Ocupação:__ Estudante e Traficante/Idol nas horas vagas"), false)
                    .AddField(await StringCatch.GetString("infoDeveloperTitle", "**As pessoas/grupos que fazem tudo isso ser possivel:**"), await StringCatch.GetString("infoDeveloperDesc", "Zuraaa!\nTakasaki#7072\nYummi#1375\n\nE é claro você que acredita em meu potencial🧡"), false)
                    .AddField(await StringCatch.GetString("infoConvites", "**Quer me ajudar????**"), await StringCatch.GetString("infoConvites", "[Adicione-me em seu Servidor](https://ayura.com.br/links/bot)\n[Entre em meu servidor para dar suporte ao projeto](https://ayura.com.br/dia)\n[Vote em mim no DiscordBotList para que possa ajudar mais pessoas](https://top.gg/bot/389917977862078484/vote)"))
                    .AddField(await StringCatch.GetString("infoOutras", "**Informações chatas:**"), await StringCatch.GetString("infoOutrasInfos", "__Ping:__ {0}ms\n__Servidores:__ {1}\n__Usuarios:__ {2}\n__Versão:__ 1.2.2.3  (Cinnamon Smooth - Patch 03)", client.Latency, client.Guilds.Count, users), false)
                    .WithThumbnailUrl("https://i.imgur.com/ppXRHTi.jpg")
                    .WithImageUrl("https://i.imgur.com/qGb6xtG.jpg")
                    .WithColor(Color.DarkPurple)
                .Build());

        }

        private async Task modulos()
        {
            string modulos = await StringCatch.GetString("modulosString", ":one: ❓ Ajuda;\n:two: 🛠 Utilidade;\n:three: ⚖ Moderação;\n:four: 🔞 NSFW;\n:five: ❤ Weeb;\n:six: 🖼 Imagens;\n:seven: 💬 Reações Customizadas;\n:eight: ⚙ Configurações.");

            if (!Contexto.IsPrivate)
            {
                Servidores servidor = new Servidores(Contexto.Guild.Id);
                Tuple<bool, Servidores> res = await new ServidoresDAO().GetPermissoesAsync(servidor);
                servidor = res.Item2;
                if (res.Item1)
                {
                    if (servidor.Permissoes == PermissoesServidores.ServidorPika)
                    {
                        modulos = await StringCatch.GetString("modulosStringEspecial", ":one: ❓ Ajuda;\n:two: 🛠 Utilidade;\n:three: ⚖ Moderação;\n:four: 🔞 NSFW;\n:five: ❤ Weeb;\n:six: 🖼 Imagens;\n:seven: 💬 Reações Customizadas;\n:eight: ⚙ Configurações;\n:nine: 🌟 Especiais.");
                    }
                }
            }

            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle(await StringCatch.GetString("cmdsAtacar", "Comandos atacaaaaar 😁"))
                    .WithDescription(await StringCatch.GetString("cmdsNavegar", "Para ver os comandos de cada modulo é so usar: `{0}{1} modulo`, exemplo: `{0}{1} utilidade`", PrefixoServidor, Comando[0]))
                    .AddField(await StringCatch.GetString("cmdsModulos", "Modulos:"), await StringCatch.GetString("cmdsModulosLista", modulos))
                    .WithImageUrl(await StringCatch.GetString("cmdsImg", "https://i.imgur.com/mQVFSrP.gif"))
                    .WithColor(Color.DarkPurple)
                .Build());
        }
        private async Task help()
        {
            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle(await StringCatch.GetString("helpModulo", "Modulo Ajuda (❓)"))
                    .WithDescription(await StringCatch.GetString("helpInfo", "Esse modulo tem comandos para te ajudar na ultilização do bot. \n\nNão tenha medo eles não mordem 😉"))
                    .WithColor(Color.DarkPurple)
                    .AddField(await StringCatch.GetString("helpCmdsTxt", "Comandos:"), await StringCatch.GetString("helpCmds", "`{0}ajuda`, `{0}comandos`, `{0}info`", PrefixoServidor))
                    .WithImageUrl(await StringCatch.GetString("helpImg", "https://i.imgur.com/XQTVJu9.jpg"))
                .Build());
        }
        private async Task utilidade()
        {
           await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle(await StringCatch.GetString("utilidadeModulo", "Modulo Utilidade (🛠)"))
                    .WithDescription(await StringCatch.GetString("utilidadeInfo", "Esse modulo possui coisas uteis pro seu dia a dia. \n\nAaaaaaa eles são tão legais ☺"))
                    .WithColor(Color.DarkPurple)
                    .AddField(await StringCatch.GetString("utilidadeCmdsTxt", "Comandos:"), await StringCatch.GetString("utiliidadeCmds", "`{0}videochamada`, `{0}avatar`, `{0}emoji`, `{0}say`, `{0}simg`, `{0}sugestao`, `{0}perfil`", PrefixoServidor))
                    .WithImageUrl(await StringCatch.GetString("utilidadeImg", "https://i.imgur.com/TK7zmb8.jpg"))
                .Build());
        }
        private async Task moderacao()
        {
            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle(await StringCatch.GetString("moderacaoModulo", "Modulo Moderação (⚖)"))
                    .WithDescription(await StringCatch.GetString("moderacaoInfo", "Esse modulo possui coisas para te ajudar moderar seu servidor. \n\nSó não seja malvado com seus amigos 😣"))
                    .WithColor(Color.DarkPurple)
                    .AddField(await StringCatch.GetString("moderacaoCmdsTxt", "Comandos:"), await StringCatch.GetString("modercaoCmds", "`{0}kick`, `{0}ban`, `{0}softban`", PrefixoServidor))
                    .WithImageUrl(await StringCatch.GetString("moderacaoImg", "https://i.imgur.com/hiu0Vh0.jpg"))
                .Build());

        }
        private async Task nsfw()
        {
            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle(await StringCatch.GetString("nsfwModulo", "Modulo NSFW (🔞)"))
                    .WithDescription(await StringCatch.GetString("nsfwInfo", "Esse modulo possui coias para você dar orgulho para sua família. \n\nTenho medo dessas coisa 😣"))
                    .WithColor(Color.DarkPurple)
                    .AddField(await StringCatch.GetString("nsfwCmdsTxt", "Comandos:"), await StringCatch.GetString("nsfwCmds", "`{0}hentai`, `{0}hentaibomb`, `{0}anal`", PrefixoServidor))
                    .WithImageUrl(await StringCatch.GetString("nsfwImg", "https://i.imgur.com/iGQ3SI8.png"))
                .Build());
        }
        private async Task weeb()
        {
            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle(await StringCatch.GetString("weebModulo", "Modulo Weeb (❤)"))
                    .WithDescription(await StringCatch.GetString("weebInfo", "Esse modulo é o mais amoroso de todos.  \n\nUse ele para distribuir o amor para seus amigos ❤"))
                    .WithColor(Color.DarkPurple)
                    .AddField(await StringCatch.GetString("weebCmdsTxt", "Comandos:"), await StringCatch.GetString("weebCmds", "`{0}hug`, `{0}slap`, `{0}kiss`, `{0}punch`, `{0}lick`, `{0}cry`, `{0}megumin`, `{0}rem`, `{0}dance`, `{0}pat`, `{0}fuck`", PrefixoServidor))
                    .WithImageUrl(await StringCatch.GetString("weebImg", "https://i.imgur.com/FmCmErd.png"))
                .Build());
        }
        private async Task img()
        {
            string cmds = await StringCatch.GetString("imgCmdsNormais", "`{0}cat`, `{0}dog`,`{0}magikavatar`, `{0}magik`", PrefixoServidor);
            if (!Contexto.IsPrivate)
            {
                Servidores servidor = new Servidores(Contexto.Guild.Id);
                Tuple<bool, Servidores> res = await new ServidoresDAO().GetPermissoesAsync(servidor);
                servidor = res.Item2;
                if (res.Item1)
                {
                    if (servidor.Permissoes == PermissoesServidores.LolisEdition || servidor.Permissoes == PermissoesServidores.ServidorPika)
                    {
                        cmds = await StringCatch.GetString("imgCmdsLolis", "`{0}cat`, `{0}dog`,`{0}magikavatar`, `{0}magik`, `{0}loli`", PrefixoServidor);
                    }
                }
            }
            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle(await StringCatch.GetString("imgModulo", "Modulo Imagem (🖼)"))
                    .WithDescription(await StringCatch.GetString("imgInfo", "Esse modulopossui imagens fofinhas para agraciar seu computador.  \n\nKawaiii ❤❤❤"))
                    .WithColor(Color.DarkPurple)
                    .AddField(await StringCatch.GetString("imgCmdsTxt", "Comandos:"), cmds)
                    .WithImageUrl(await StringCatch.GetString("imgsImg", "https://i.imgur.com/cQqTUl1.png"))
                .Build());

        }
        private async Task customReaction()
        {
            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle(await StringCatch.GetString("acrModulo", "Modulo Reações Customizadas (💬)"))
                    .WithDescription(await StringCatch.GetString("acrInfo", "Esse modulo possui comandos para você controlar as minhas Reações Customizadas. \n\nEu adoro usar elas para me divertir com vocês 😂"))
                    .WithColor(Color.DarkPurple)
                    .AddField(await StringCatch.GetString("acrCmdsTxt", "Comandos:"), await StringCatch.GetString("acrCmds", "`{0}acr`, `{0}dcr`, `{0}lcr`", PrefixoServidor))
                    .WithImageUrl(await StringCatch.GetString("acrImg", "https://i.imgur.com/AUpMkBP.jpg"))
                .Build());

        }
        private async Task configuracoes()
        {
            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle(await StringCatch.GetString("configsModulo", "Modulo Configurações (⚙)"))
                    .WithDescription(await StringCatch.GetString("ConfigsInfo", "Em configurações você define preferencias de como agirei em seu servidor. \n\nTenho certeza que podemos ficar mais intimos assim 😄"))
                    .WithColor(Color.DarkPurple)
                    .AddField(await StringCatch.GetString("configsCmdsTxt", "Comandos:"), await StringCatch.GetString("configsCmds", "`{0}setprefix`, `{0}piconf`, `{0}welcomech`, `{0}byech`, `{0}picargo`, `{0}welcomemsg`, `{0}byemsg`, `{0}erromsg`", PrefixoServidor))
                    .WithImageUrl(await StringCatch.GetString("configsImg", "https://i.imgur.com/vg0z9yT.jpg"))
                .Build());
        }
        private async Task especial()
        {
            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle(await StringCatch.GetString("especialModulo", "Modulo Especiais (🌟)"))
                    .WithDescription(await StringCatch.GetString("especialInfo", "Só falo uma coisa, isso é exclusivo, e você pode ter o prazer de acessar, não é todo mundo que tem essa chance então aproveite."))
                    .WithColor(Color.DarkPurple)
                    .AddField(await StringCatch.GetString("especialCmdsTxt", "Comandos:"), await StringCatch.GetString("especialCmds", "`{0}insult`, `{0}criarinsulto`, `{0}fuckadd`", PrefixoServidor))
                    .WithImageUrl(await StringCatch.GetString("especialImg", "https://i.imgur.com/bQGUGbB.gif"))
                .Build());
        }

        public async Task MessageEventExceptions(Exception e, Servidores servidor)
        {
            if (e is NullReferenceException)
            {
                bool erroMsg = true;
                if (!Contexto.IsPrivate)
                {
                    ConfiguracoesServidor configuracoes = new ConfiguracoesServidor(new Servidores(Contexto.Guild.Id), new ErroMsg());
                    Tuple<bool, ConfiguracoesServidor> res = await new ConfiguracoesServidorDAO().GetErrorMsgAsync(configuracoes);
                    configuracoes = res.Item2;
                    if (res.Item1)
                    {
                        erroMsg = configuracoes.erroMsg.erroMsg;
                    }
                }

                if (erroMsg)
                {
                    await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithDescription(await StringCatch.GetString("msgEventNotFoundCommand", " **{0}** comando não encontrado use `{1}comandos` para ver os meus comandos", Contexto.User.ToString(), new string(servidor.Prefix)))
                            .WithColor(Color.DarkPurple)
                        .Build());
                }
            }
            else
            {
                await LogEmiter.EnviarLogAsync(e);
            }
        }

        public async Task MentionMessage(Servidores servidores)
        {
            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                .WithDescription(await StringCatch.GetString("msgEventPrefixInform", "Oii {0} meu prefixo é: `{1}` se quiser ver meus comando é so usar: `{1}comandos`", Contexto.User.Username, new string(servidores.Prefix)))
                .WithColor(Color.DarkPurple)
                .Build());
        }
    }


}
