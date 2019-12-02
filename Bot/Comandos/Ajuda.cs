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
        private PermissoesServidores Permissao = PermissoesServidores.Normal; 
        public Ajuda (CommandContext contexto, params object[] args) : base (contexto, args)
        {
            VerificarPermissao().Wait();
        }

        private async Task VerificarPermissao()
        {
            ulong id = 0;
            if (!Contexto.IsPrivate)
            {
                id = Contexto.Guild.Id;
            }

            Tuple<bool, Servidores> servidor = await new ServidoresDAO().GetPermissoesAsync(new Servidores(id));
            if (servidor.Item1)
            {
                Permissao = servidor.Item2.Permissoes;
            }
        }

        public async Task convite()
        {
            await info();
        }

        public async Task help()
        {
            await ajuda();
        }

        public async Task ajuda()
        {
            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                .WithColor(Color.DarkPurple)
                .WithTitle(await StringCatch.GetStringAsync("ajudaTitle", "Será um enorme prazer te ajudar 😋"))
                .WithDescription(await StringCatch.GetStringAsync("ajudaDesctiption", "Eu me chamo Kurosawa Dia, sou presidente do conselho de classe, idol e também ajudo as pessoas com algumas coisinhas no Discord 😉\n"
                + "Se você usar `{0}comandos` no chat vai aparecer tudo que eu posso fazer atualmente (isso não é demais 😁)\n"
                + "Sério estou muito ansiosa para passar um tempo com você e também te ajudar XD\n"
                + "Se você tem ideias de mais coisas que eu possa fazer por favor mande uma sugestão com o `{0}sugestao`\n\n"
                + "E como a Mari fala Let's Go!!", PrefixoServidor))
                .WithFooter(await StringCatch.GetStringAsync("ajudaProjeto", "Kurosawa Dia é um projeto feito com amor e carinho pelos seus desenvolvedores!"), await StringCatch.GetStringAsync("ajudaImg", "https://i.imgur.com/Cm8grM4.png"))
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
                    await helpajuda();
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
                    if (Permissao == PermissoesServidores.ServidorPika)
                    {
                        await especial();
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

        public async Task info()
        {
            DiscordShardedClient client = Contexto.Client as DiscordShardedClient;
            int users = 0;
            foreach (SocketGuild servidor in client.Guilds)
            {
                users += servidor.Users.Count;
            }
            


            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle(await StringCatch.GetStringAsync("infoTxt", "Dia's Book:"))
                    .WithDescription(await StringCatch.GetStringAsync("infoDescription", "Espero que não faça nada estranho com minhas informações, to zuando kkkkkk 😝"))
                    .AddField(await StringCatch.GetStringAsync("infoBot", "**Sobre mim:**"), await StringCatch.GetStringAsync("infoInfos", "__Nome:__ Kurosawa Dia (Dia - Chan)\n__Aniversário:__ 1° de Janeiro (Quero presentes)\n__Ocupação:__ Estudante e Traficante/Idol nas horas vagas"), false)
                    .AddField(await StringCatch.GetStringAsync("infoDeveloperTitle", "**As pessoas que fazem tudo isso ser possivel:**"), await StringCatch.GetStringAsync("infoDeveloperDesc", "Takasaki#7072\nYummi#1375\nLuckShiba#0001\n\nE é claro você que acredita em meu potencial🧡"), false)
                    .AddField(await StringCatch.GetStringAsync("infoConvites", "**Quer me ajudar????**"), await StringCatch.GetStringAsync("infoConvites", "[Me adicione em seu Servidor]({0})\n[Entre no meu servidor para dar suporte ao projeto]({1})\n[Vote em mim no DiscordBotList para que eu possa ajudar mais pessoas](https://kurosawa.zuraaa.com/paginas/redirecionar.html?pg=topgg)", InfoImportante.conviteDia, InfoImportante.conviteServer))
                    .AddField(await StringCatch.GetStringAsync("infoOutras", "**Informações chatas:**"), await StringCatch.GetStringAsync("infoOutrasInfos", "__Ping:__ {0}ms\n__Servidores:__ {1}\n__Usuários:__ {2}\n__Versão:__ {3}  ({4})", client.Latency, client.Guilds.Count, users, InfoImportante.VersaoNumb, InfoImportante.VersaoName), false)
                    .WithThumbnailUrl("https://i.imgur.com/ppXRHTi.jpg")
                    .WithImageUrl("https://i.imgur.com/qGb6xtG.jpg")
                    .WithColor(Color.DarkPurple)
                .Build());

        }

        private async Task modulos()
        {
            string modulos = await StringCatch.GetStringAsync("modulosString", ":one: ❓ Ajuda;\n:two: 🛠 Utilidade;\n:three: ⚖ Moderação;\n:four: 🔞 NSFW;\n:five: ❤ Weeb;\n:six: 🖼 Imagens;\n:seven: 💬 Reações Customizadas;\n:eight: ⚙ Configurações");

            if (Permissao.Equals(PermissoesServidores.ServidorPika))
            {
                modulos += await StringCatch.GetStringAsync("modulosStringEspecial", "\n:nine: 🌟 Especiais.");
            }
            else
            {
                modulos += ".";
            }

            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle(await StringCatch.GetStringAsync("cmdsAtacar", "Comandos atacaaaaar 😁"))
                    .WithDescription(await StringCatch.GetStringAsync("cmdsNavegar", "Para ver os comandos de cada módulo é so usar: `{0}{1} módulo`, exemplo: `{0}{1} utilidade`", PrefixoServidor, Comando[0]))
                    .AddField(await StringCatch.GetStringAsync("cmdsModulos", "Módulos:"), await StringCatch.GetStringAsync("cmdsModulosLista", modulos))
                    .WithImageUrl(await StringCatch.GetStringAsync("cmdsImg", "https://i.imgur.com/mQVFSrP.gif"))
                    .WithColor(Color.DarkPurple)
                .Build());
        }
        private async Task helpajuda()
        {
            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle(await StringCatch.GetStringAsync("helpModulo", "Módulo Ajuda (❓)"))
                    .WithDescription(await StringCatch.GetStringAsync("helpInfo", "Este módulo tem comandos para te ajudar na ultilização do bot. \n\nNão tenha medo eles não mordem 😉"))
                    .WithColor(Color.DarkPurple)
                    .AddField(await StringCatch.GetStringAsync("helpCmdsTxt", "Comandos:"), await StringCatch.GetStringAsync("helpCmds", "`{0}ajuda`, `{0}comandos`, `{0}info`", PrefixoServidor))
                    .WithImageUrl(await StringCatch.GetStringAsync("helpImg", "https://i.imgur.com/XQTVJu9.jpg"))
                .Build());
        }
        private async Task utilidade()
        {
           await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle(await StringCatch.GetStringAsync("utilidadeModulo", "Módulo Utilidade (🛠)"))
                    .WithDescription(await StringCatch.GetStringAsync("utilidadeInfo", "Este módulo possui coisas uteis pro seu dia a dia. \n\nAaaaaaa eles são tão legais ☺"))
                    .WithColor(Color.DarkPurple)
                    .AddField(await StringCatch.GetStringAsync("utilidadeCmdsTxt", "Comandos:"), await StringCatch.GetStringAsync("utiliidadeCmds", "`{0}videochamada`, `{0}avatar`, `{0}emoji`, `{0}say`, `{0}simg`, `{0}sugestao`, {0}bug, `{0}perfil`", PrefixoServidor))
                    .WithImageUrl(await StringCatch.GetStringAsync("utilidadeImg", "https://i.imgur.com/TK7zmb8.jpg"))
                .Build());
        }
        private async Task moderacao()
        {
            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle(await StringCatch.GetStringAsync("moderacaoModulo", "Módulo Moderação (⚖)"))
                    .WithDescription(await StringCatch.GetStringAsync("moderacaoInfo", "Este módulo possui coisas para te ajudar a moderar seu servidor. \n\nSó não seja malvado com seus amigos 😣"))
                    .WithColor(Color.DarkPurple)
                    .AddField(await StringCatch.GetStringAsync("moderacaoCmdsTxt", "Comandos:"), await StringCatch.GetStringAsync("moderacaoCmds", "`{0}kick`, `{0}ban`, `{0}softban`", PrefixoServidor))
                    .WithImageUrl(await StringCatch.GetStringAsync("moderacaoImg", "https://i.imgur.com/hiu0Vh0.jpg"))
                .Build());

        }
        private async Task nsfw()
        {
            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle(await StringCatch.GetStringAsync("nsfwModulo", "Módulo NSFW (🔞)"))
                    .WithDescription(await StringCatch.GetStringAsync("nsfwInfo", "Este módulo possui coisas para você dar orgulho para sua família. \n\nTenho medo dessas coisas 😣"))
                    .WithColor(Color.DarkPurple)
                    .AddField(await StringCatch.GetStringAsync("nsfwCmdsTxt", "Comandos:"), await StringCatch.GetStringAsync("nsfwCmds", "`{0}hentai`, `{0}hentaibomb`, `{0}anal`", PrefixoServidor))
                    .WithImageUrl(await StringCatch.GetStringAsync("nsfwImg", "https://i.imgur.com/iGQ3SI8.png"))
                .Build());
        }
        private async Task weeb()
        {
            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle(await StringCatch.GetStringAsync("weebModulo", "Modulo Weeb (❤)"))
                    .WithDescription(await StringCatch.GetStringAsync("weebInfo", "Este módulo é o mais amoroso de todos.  \n\nUse ele para distribuir o amor para seus amigos ❤"))
                    .WithColor(Color.DarkPurple)
                    .AddField(await StringCatch.GetStringAsync("weebCmdsTxt", "Comandos:"), await StringCatch.GetStringAsync("weebCmds", "`{0}hug`, `{0}slap`, `{0}kiss`, `{0}punch`, `{0}lick`, `{0}cry`, `{0}megumin`, `{0}rem`, `{0}dance`, `{0}pat`, `{0}fuck`, `{0}owoify`", PrefixoServidor))
                    .WithImageUrl(await StringCatch.GetStringAsync("weebImg", "https://i.imgur.com/FmCmErd.png"))
                .Build());
        }
        private async Task img()
        {
            string cmds = await StringCatch.GetStringAsync("imgCmdsNormais", "`{0}cat`, `{0}dog`,`{0}magikavatar`, `{0}magik`", PrefixoServidor);
            if (Permissao == PermissoesServidores.LolisEdition || Permissao == PermissoesServidores.ServidorPika)
            {
                cmds = await StringCatch.GetStringAsync("imgCmdsLolis", "`{0}cat`, `{0}dog`,`{0}magikavatar`, `{0}magik`, `{0}loli`, `{0}lolibomb`", PrefixoServidor);
            }
            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle(await StringCatch.GetStringAsync("imgModulo", "Módulo Imagem (🖼)"))
                    .WithDescription(await StringCatch.GetStringAsync("imgInfo", "Este módulo possui imagens fofinhas para agraciar seu computador.  \n\nKawaiii ❤❤❤"))
                    .WithColor(Color.DarkPurple)
                    .AddField(await StringCatch.GetStringAsync("imgCmdsTxt", "Comandos:"), cmds)
                    .WithImageUrl(await StringCatch.GetStringAsync("imgsImg", "https://i.imgur.com/cQqTUl1.png"))
                .Build());

        }
        private async Task customReaction()
        {
            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle(await StringCatch.GetStringAsync("acrModulo", "Módulo Reações Customizadas (💬)"))
                    .WithDescription(await StringCatch.GetStringAsync("acrInfo", "Este módulo possui comandos para você controlar as minhas Reações Customizadas. \n\nEu adoro usar elas para me divertir com vocês 😂"))
                    .WithColor(Color.DarkPurple)
                    .AddField(await StringCatch.GetStringAsync("acrCmdsTxt", "Comandos:"), await StringCatch.GetStringAsync("acrCmds", "`{0}acr`, `{0}dcr`, `{0}lcr`", PrefixoServidor))
                    .WithImageUrl(await StringCatch.GetStringAsync("acrImg", "https://i.imgur.com/AUpMkBP.jpg"))
                .Build());

        }
        private async Task configuracoes()
        {
            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle(await StringCatch.GetStringAsync("configsModulo", "Módulo Configurações (⚙)"))
                    .WithDescription(await StringCatch.GetStringAsync("ConfigsInfo", "Em configurações você define preferencias de como agirei em seu servidor. \n\nTenho certeza que podemos ficar mais íntimos assim 😄"))
                    .WithColor(Color.DarkPurple)
                    .AddField(await StringCatch.GetStringAsync("configsCmdsTxt", "Comandos:"), await StringCatch.GetStringAsync("configsCmds", "`{0}setprefix`, `{0}piconf`, `{0}welcomech`, `{0}byech`, `{0}picargo`, `{0}welcomemsg`, `{0}byemsg`, `{0}erromsg`", PrefixoServidor))
                    .WithImageUrl(await StringCatch.GetStringAsync("configsImg", "https://i.imgur.com/vg0z9yT.jpg"))
                .Build());
        }
        private async Task especial()
        {
            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle(await StringCatch.GetStringAsync("especialModulo", "Módulo Especiais (🌟)"))
                    .WithDescription(await StringCatch.GetStringAsync("especialInfo", "Só falo uma coisa, isso é exclusivo, e você pode ter o prazer de acessar. Não é todo mundo que tem essa chance, então aproveite."))
                    .WithColor(Color.DarkPurple)
                    .AddField(await StringCatch.GetStringAsync("especialCmdsTxt", "Comandos:"), await StringCatch.GetStringAsync("especialCmds", "`{0}insult`, `{0}criarinsulto`, `{0}fuckadd`", PrefixoServidor))
                    .WithImageUrl(await StringCatch.GetStringAsync("especialImg", "https://i.imgur.com/bQGUGbB.gif"))
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
                            .WithDescription(await StringCatch.GetStringAsync("msgEventNotFoundCommand", " **{0}**, esse comando não foi encontrado. Use `{1}comandos` para ver os meus comandos.", Contexto.User.ToString(), new string(servidor.Prefix)))
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
                .WithDescription(await StringCatch.GetStringAsync("msgEventPrefixInform", "Oii {0}, meu prefixo é `{1}`. Se quiser ver os meus comandos é so usar `{1}comandos`!", Contexto.User.Username, new string(servidores.Prefix)))
                .WithColor(Color.DarkPurple)
                .Build());
        }
    }
}
