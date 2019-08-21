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
using static MainDatabaseControler.Modelos.ConfiguracoesServidor;
using static MainDatabaseControler.Modelos.Servidores;

namespace Bot.Comandos
{
    public class Ajuda : GenericModule
    {
        public Ajuda (CommandContext contexto, object[] args) : base (contexto, args)
        {

        }

        

        public void ajuda()
        {
            contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                .WithColor(Color.DarkPurple)
                .WithTitle(StringCatch.GetString("ajudaTitle", "Sera um enorme prazer te ajudar 😋"))
                .WithDescription(StringCatch.GetString("ajudaDesctiption", "Eu me chamo Kurosawa Dia, sou presidente do conselho de classe, idol e tambem ajudo as pessoas com algumas coisinhas no discord 😉\n"
                + "Se você usar `{0}comandos` no chat vai aparecer tudo que eu posso fazer atualmente (isso não é demais 😁)\n"
                + "Serio estou muito ansiosa para passar um tempo com você e tambem te ajudar XD\n"
                + "Se você tem ideias de mais coisas que possa fazer por favor mande uma sugestao com o `{0}sugestao`\n\n"
                + "E como a Mari fala Let's Go!!", (string)args[0]))
                .WithFooter(StringCatch.GetString("ajudaProjeto", "Kurosawa Dia é um projeto feito com amor e carinho financiado pelo Zuraaa!"), StringCatch.GetString("ajudaImg", "https://i.imgur.com/Cm8grM4.png"))
                .WithImageUrl("https://i.imgur.com/PC5QDiX.png")

                .Build()
                );

        }

        public void comandos()
        {
            string[] comando = (string[])args[1];
            string msg = string.Join(" ", comando, 1, (comando.Length - 1));

            if (!string.IsNullOrEmpty(msg))
            {
                switch (msg.ToLowerInvariant())
                {
                    case "ajuda":
                        help();
                        break;
                    case "utilidade":
                        utilidade();
                        break;
                    case "moderacao":
                    case "moderação":
                        moderacao();
                        break;
                    case "nsfw":
                        nsfw();
                        break;
                    case "weeb":
                        weeb();
                        break;
                    case "imagens":
                        img();
                        break;
                    case "reações customizadas":
                    case "reacoes customizadas":
                        customReaction();
                        break;
                    case "configurações":
                    case "configuracoes":
                        configuracoes();
                        break;
                    case "especiais":
                        if (!contexto.IsPrivate)
                        {
                            Servidores servidor = new Servidores(contexto.Guild.Id);
                            if (new ServidoresDAO().GetPermissoes(ref servidor))
                            {
                                if (servidor.Permissoes == PermissoesServidores.ServidorPika)
                                {
                                    especial();
                                }
                                else
                                {
                                    modulos();
                                }
                            }
                        }
                        else
                        {
                            modulos();
                        }
                        break;
                    default:
                        modulos();
                        break;
                }

            }
            else
            {
                modulos();
            }
        }

        public void convite()
        {
            contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle(StringCatch.GetString("conviteTxt", "Aqui estão meus convites: "))
                    .WithDescription(StringCatch.GetString("conviteConvites", "[Me convide para o seu servidor](https://ayura.com.br/links/bot)\n[Entre no meu servidor](https://ayura.com.br/dia)")) //shrug
                    .WithColor(Color.DarkPurple)
             .Build());
        }

        public void info()
        {
            DiscordSocketClient client = contexto.Client as DiscordSocketClient;
            int users = 0;
            foreach (SocketGuild servidor in client.Guilds)
            {
                users += servidor.Users.Count;
            }


            _ = contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle(StringCatch.GetString("infoTxt", "Dia's Book:"))
                    .WithDescription(StringCatch.GetString("infoDescription", "Espero que não faça nada estranho com minhas informações, to zuando kkkkkk 😝"))
                    .AddField(StringCatch.GetString("infoBot", "**Sobre mim:**"), StringCatch.GetString("infoInfos", "__Nome:__ Kurosawa Dia (Dia - Chan)\n__Aniversario:__ 01 de Janeiro (Quero Presentes)\n__Ocupação:__ Estudante e Traficante/Idol nas horas vagas"), false)
                    .AddField(StringCatch.GetString("infoDeveloperTitle", "**As pessoas/grupos que fazem tudo isso ser possivel:**"), StringCatch.GetString("infoDeveloperDesc", "Zuraaa!\nTakasaki#7072\nYummi#1375\n\nE é claro você que acredita em meu potencial🧡"), false)
                    .AddField(StringCatch.GetString("infoConvites", "**Quer me ajudar????**"), StringCatch.GetString("infoConvites", "[Adicione-me em seu Servidor](https://ayura.com.br/links/bot)\n[Entre em meu servidor para dar suporte ao projeto](https://ayura.com.br/dia)\n[Vote em mim no DiscordBotList para que possa ajudar mais pessoas](https://discordbots.org/bot/389917977862078484/vote)"))
                    .AddField(StringCatch.GetString("infoOutras", "**Informações chatas:**"), StringCatch.GetString("infoOutrasInfos", "__Ping:__ {0}ms\n__Servidores:__ {1}\n__Usuarios:__ {2}\n__Versão:__ 1.2.2  (Cinnamon Smooth)", client.Latency, client.Guilds.Count, users), false)
                    .WithThumbnailUrl("https://i.imgur.com/ppXRHTi.jpg")
                    .WithImageUrl("https://i.imgur.com/qGb6xtG.jpg")
                    .WithColor(Color.DarkPurple)
                .Build());

        }

        private void modulos()
        {
            string modulos = StringCatch.GetString("modulosString", "❓ Ajuda;\n🛠 Utilidade;\n⚖ Moderação;\n🔞 NSFW;\n❤ Weeb;\n🖼 Imagens;\n💬 Reações Customizadas;\n⚙ Configurações.");

            if (!contexto.IsPrivate)
            {
                Servidores servidor = new Servidores(contexto.Guild.Id);
                if (new ServidoresDAO().GetPermissoes(ref servidor))
                {
                    if (servidor.Permissoes == PermissoesServidores.ServidorPika)
                    {
                        modulos = StringCatch.GetString("modulosStringEspecial", "❓ Ajuda;\n🛠 Utilidade;\n⚖ Moderação;\n🔞 NSFW;\n❤ Weeb;\n🖼 Imagens;\n💬 Reações Customizadas;\n⚙ Configurações;\n🌟 Especiais.");
                    }
                }
            }

            contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle(StringCatch.GetString("cmdsAtacar", "Comandos atacaaaaar 😁"))
                    .WithDescription(StringCatch.GetString("cmdsNavegar", "Para ver os comandos de cada modulo é so usar: `{0}{1} modulo`, exemplo: `{0}{1} utilidade`", (string)args[0], ((string[])args[1])[0]))
                    .AddField(StringCatch.GetString("cmdsModulos", "Modulos:"), StringCatch.GetString("cmdsModulosLista", modulos))
                    .WithImageUrl(StringCatch.GetString("cmdsImg", "https://i.imgur.com/mQVFSrP.gif"))
                    .WithColor(Color.DarkPurple)
                .Build());
        }
        private void help()
        {
            contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle(StringCatch.GetString("helpModulo", "Modulo Ajuda (❓)"))
                    .WithDescription(StringCatch.GetString("helpInfo", "Esse modulo tem comandos para te ajudar na ultilização do bot. \n\nNão tenha medo eles não mordem 😉"))
                    .WithColor(Color.DarkPurple)
                    .AddField(StringCatch.GetString("helpCmdsTxt", "Comandos:"), StringCatch.GetString("helpCmds", "`{0}ajuda`, `{0}comandos`, `{0}info`, `{0}convite`", (string)args[0]))
                    .WithImageUrl(StringCatch.GetString("helpImg", "https://i.imgur.com/XQTVJu9.jpg"))
                .Build());
        }
        private void utilidade()
        {
           contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle(StringCatch.GetString("utilidadeModulo", "Modulo Utilidade (🛠)"))
                    .WithDescription(StringCatch.GetString("utilidadeInfo", "Esse modulo possui coisas uteis pro seu dia a dia. \n\nAaaaaaa eles são tão legais ☺"))
                    .WithColor(Color.DarkPurple)
                    .AddField(StringCatch.GetString("utilidadeCmdsTxt", "Comandos:"), StringCatch.GetString("utiliidadeCmds", "`{0}videochamada`, `{0}avatar`, `{0}emoji`, `{0}say`, `{0}simg`, `{0}sugestao`, `{0}perfil`", (string)args[0]))
                    .WithImageUrl(StringCatch.GetString("utilidadeImg", "https://i.imgur.com/TK7zmb8.jpg"))
                .Build());
        }
        private void moderacao()
        {
            contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle(StringCatch.GetString("moderacaoModulo", "Modulo Moderação (⚖)"))
                    .WithDescription(StringCatch.GetString("moderacaoInfo", "Esse modulo possui coisas para te ajudar moderar seu servidor. \n\nSó não seja malvado com seus amigos 😣"))
                    .WithColor(Color.DarkPurple)
                    .AddField(StringCatch.GetString("moderacaoCmdsTxt", "Comandos:"), StringCatch.GetString("modercaoCmds", "`{0}kick`, `{0}ban`, `{0}softban`", (string)args[0]))
                    .WithImageUrl(StringCatch.GetString("moderacaoImg", "https://i.imgur.com/hiu0Vh0.jpg"))
                .Build());

        }
        private void nsfw()
        {
            contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle(StringCatch.GetString("nsfwModulo", "Modulo NSFW (🔞)"))
                    .WithDescription(StringCatch.GetString("nsfwInfo", "Esse modulo possui coias para você dar orgulho para sua família. \n\nTenho medo dessas coisa 😣"))
                    .WithColor(Color.DarkPurple)
                    .AddField(StringCatch.GetString("nsfwCmdsTxt", "Comandos:"), StringCatch.GetString("nsfwCmds", "`{0}hentai`, `{0}hentaibomb`, `{0}anal`", (string)args[0]))
                    .WithImageUrl(StringCatch.GetString("nsfwImg", "https://i.imgur.com/iGQ3SI8.png"))
                .Build());
        }
        private void weeb()
        {
            contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle(StringCatch.GetString("weebModulo", "Modulo Weeb (❤)"))
                    .WithDescription(StringCatch.GetString("weebInfo", "Esse modulo é o mais amoroso de todos.  \n\nUse ele para distribuir o amor para seus amigos ❤"))
                    .WithColor(Color.DarkPurple)
                    .AddField(StringCatch.GetString("weebCmdsTxt", "Comandos:"), StringCatch.GetString("weebCmds", "`{0}hug`, `{0}slap`, `{0}kiss`, `{0}punch`, `{0}lick`, `{0}cry`, `{0}megumin`, `{0}rem`, `{0}dance`, `{0}pat`", (string)args[0]))
                    .WithImageUrl(StringCatch.GetString("weebImg", "https://i.imgur.com/FmCmErd.png"))
                .Build());
        }
        private void img()
        {
            contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle(StringCatch.GetString("imgModulo", "Modulo Imagem (🖼)"))
                    .WithDescription(StringCatch.GetString("imgInfo", "Esse modulopossui imagens fofinhas para agraciar seu computador.  \n\nKawaiii ❤❤❤"))
                    .WithColor(Color.DarkPurple)
                    .AddField(StringCatch.GetString("imgCmdsTxt", "Comandos:"), StringCatch.GetString("imgCmds", "`{0}cat`, `{0}dog`,`{0}magikavatar`, `{0}magik`, `{0}fuck`", (string)args[0]))
                    .WithImageUrl(StringCatch.GetString("imgsImg", "https://i.imgur.com/cQqTUl1.png"))
                .Build());

        }
        private void customReaction()
        {
            contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle(StringCatch.GetString("acrModulo", "Modulo Reações Customizadas (💬)"))
                    .WithDescription(StringCatch.GetString("acrInfo", "Esse modulo possui comandos para você controlar as minhas Reações Customizadas. \n\nEu adoro usar elas para me divertir com vocês 😂"))
                    .WithColor(Color.DarkPurple)
                    .AddField(StringCatch.GetString("acrCmdsTxt", "Comandos:"), StringCatch.GetString("acrCmds", "`{0}acr`, `{0}dcr`, `{0}lcr`", (string)args[0]))
                    .WithImageUrl(StringCatch.GetString("acrImg", "https://i.imgur.com/AUpMkBP.jpg"))
                .Build());

        }
        private void configuracoes()
        {
            contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle(StringCatch.GetString("configsModulo", "Modulo Configurações (⚙)"))
                    .WithDescription(StringCatch.GetString("ConfigsInfo", "Em configurações você define preferencias de como agirei em seu servidor. \n\nTenho certeza que podemos ficar mais intimos assim 😄"))
                    .WithColor(Color.DarkPurple)
                    .AddField(StringCatch.GetString("configsCmdsTxt", "Comandos:"), StringCatch.GetString("configsCmds", "`{0}setprefix`, `{0}piconf`, `{0}welcomech`, `{0}byech`, `{0}picargo`, `{0}welcomemsg`, `{0}byemsg`, `{0}erromsg`", (string)args[0]))
                    .WithImageUrl(StringCatch.GetString("configsImg", "https://i.imgur.com/vVBOIB2.gif"))
                .Build());
        }
        private void especial()
        {
            contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle(StringCatch.GetString("especialModulo", "Modulo Especiais (🌟)"))
                    .WithDescription(StringCatch.GetString("especialInfo", "Só falo uma coisa, isso é exclusivo, e você pode ter o prazer de acessar, não é todo mundo que tem essa chance então aproveite."))
                    .WithColor(Color.DarkPurple)
                    .AddField(StringCatch.GetString("especialCmdsTxt", "Comandos:"), StringCatch.GetString("especialCmds", "`{0}insult`, `{0}criarinsulto`, `{0}fuckadd`", (string)args[0]))
                    .WithImageUrl(StringCatch.GetString("especialImg", "https://i.imgur.com/bQGUGbB.gif"))
                .Build());
        }

        public void MessageEventExceptions(Exception e, Servidores servidor)
        {
            if (e is NullReferenceException)
            {
                bool erroMsg = true;
                if (!contexto.IsPrivate)
                {
                    ConfiguracoesServidor configuracoes = new ConfiguracoesServidor(new Servidores(contexto.Guild.Id), new ErroMsg());
                    if(new ConfiguracoesServidorDAO().GetErrorMsg(ref configuracoes))
                    {
                        erroMsg = configuracoes.erroMsg.erroMsg;
                    }
                }

                if (erroMsg)
                {
                    contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithDescription(StringCatch.GetString("msgEventNotFoundCommand", " **{0}** comando não encontrado use `{1}comandos` para ver os meus comandos", contexto.User.ToString(), new string(servidor.Prefix)))
                            .WithColor(Color.DarkPurple)
                        .Build());
                }
            }
            else
            {
                MethodInfo metodo = SingletonLogs.tipo.GetMethod("Log");
                object[] parms = new object[1];
                parms[0] = e.ToString();
                metodo.Invoke(SingletonLogs.instanced, parms);
            }
        }

        public void MentionMessage(Servidores servidores)
        {
            contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                .WithDescription(StringCatch.GetString("msgEventPrefixInform", "Oii {0} meu prefixo é: `{1}` se quiser ver meus comando é so usar: `{1}comandos`", contexto.User.Username, new string(servidores.Prefix)))
                .WithColor(Color.DarkPurple)
                .Build());
        }
    }


}
