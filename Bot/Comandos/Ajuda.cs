using Bot.DataBase.MainDB.Modelos;
using Bot.Extensions;
using Bot.Singletons;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Reflection;

namespace Bot.Comandos
{
    public class Ajuda
    {
        public void ajuda(CommandContext context, object[] args)
        {
            context.Channel.SendMessageAsync(embed: new EmbedBuilder()
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

        public void comandos(CommandContext context, object[] args)
        {
            try
            {
                string[] userMessage = (string[])args[1];
            }
            catch
            {
                ((IUserMessage)args[1]).DeleteAsync();
            }
            IUserMessage msg = context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithTitle(StringCatch.GetString("cmdsAtacar", "Comandos atacaaaaar 😁"))
                        .WithDescription(StringCatch.GetString("cmdsNavegar", "Use as reações para navegar pelos comandos 👍"))
                        .AddField(StringCatch.GetString("cmdsModulos", "Modulos:"), StringCatch.GetString("cmdsModulosLista", "❓ Ajuda;\n🛠 Ultilidades;\n⚖ Moderação;\n🔞 NSFW;\n❤ Weeb;\n🖼 Imagens;\n💬 Reações Customizadas;\n⚙ Configurações."))
                        .WithImageUrl(StringCatch.GetString("cmdsImg", "https://i.imgur.com/mQVFSrP.gif"))
                        .WithColor(Color.DarkPurple)
                .Build()).GetAwaiter().GetResult();

            Emoji[] menu = new Emoji[8];
            menu[0] = new Emoji("❓");
            menu[1] = new Emoji("🛠");
            menu[2] = new Emoji("⚖");
            menu[3] = new Emoji("🔞");
            menu[4] = new Emoji("❤");
            menu[5] = new Emoji("🖼");
            menu[6] = new Emoji("💬");
            menu[7] = new Emoji("⚙");

            RequestOptions opc = new RequestOptions();
            opc.RetryMode = RetryMode.AlwaysRetry;
            opc.Timeout = 129;
            msg.AddReactionsAsync(menu, opc);

            args[1] = msg;
            ReactionControler reaction = new ReactionControler();
            args[2] = reaction;
            reaction.GetReaction(msg, menu[0], context.User, new ReturnMethod(help, context, args));
            reaction.GetReaction(msg, menu[1], context.User, new ReturnMethod(utilidade, context, args));
            reaction.GetReaction(msg, menu[2], context.User, new ReturnMethod(moderacao, context, args));
            reaction.GetReaction(msg, menu[3], context.User, new ReturnMethod(nsfw, context, args));
            reaction.GetReaction(msg, menu[4], context.User, new ReturnMethod(weeb, context, args));
            reaction.GetReaction(msg, menu[5], context.User, new ReturnMethod(img, context, args));
            reaction.GetReaction(msg, menu[6], context.User, new ReturnMethod(customReaction, context, args));
            reaction.GetReaction(msg, menu[7], context.User, new ReturnMethod(configuracoes, context, args));

        }

        public void convite(CommandContext context, object[] args)
        {
            context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle(StringCatch.GetString("conviteTxt", "Aqui estão meus convites: "))
                    .WithDescription(StringCatch.GetString("conviteConvites", "[Me convide para o seu servidor](https://ayura.com.br/links/bot)\n[Entre no meu servidor](https://ayura.com.br/dia)")) //shrug
                    .WithColor(Color.DarkPurple)
             .Build());
        }

        public void info(CommandContext context, object[] args)
        {
            DiscordSocketClient client = context.Client as DiscordSocketClient;
            int users = 0;
            foreach (SocketGuild servidor in client.Guilds)
            {
                users += servidor.Users.Count;
            }


            _ = context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle(StringCatch.GetString("infoTxt", "Dia's Book:"))
                    .WithDescription(StringCatch.GetString("infoDescription", "Espero que não faça nada estranho com minhas informações, to zuando kkkkkk 😝"))
                    .AddField(StringCatch.GetString("infoBot", "**Sobre mim:**"), StringCatch.GetString("infoInfos", "__Nome:__ Kurosawa Dia (Dia - Chan)\n__Aniversario:__ 01 de Janeiro (Quero Presentes)\n__Ocupação:__ Estudante e Traficante/Idol nas horas vagas"), false)
                    .AddField(StringCatch.GetString("infoDeveloperTitle", "**As pessoas/grupos que fazem tudo isso ser possivel:**"), StringCatch.GetString("infoDeveloperDesc", "Zuraaa!\nTakasaki#7072\nYummi#1375\n\nE é claro você que acredita em meu potencial🧡"), false)
                    .AddField(StringCatch.GetString("infoConvites", "**Quer me ajudar????**"), StringCatch.GetString("infoConvites", "[Adicione-me em seu Servidor](https://ayura.com.br/links/bot)\n[Entre em meu servidor para dar suporte ao projeto](https://ayura.com.br/dia)\n[Vote em mim no DiscordBotList para que possa ajudar mais pessoas](https://discordbots.org/bot/389917977862078484/vote)"))
                    .AddField(StringCatch.GetString("infoOutras", "**Informações chatas:**"), StringCatch.GetString("infoOutrasInfos", "__Ping:__ {0}ms\n__Servidores:__ {1}\n__Usuarios:__ {2}\n__Versão:__ 1.2.1  (Mari - Chan, it's Joke)", client.Latency, client.Guilds.Count, users), false)
                    .WithThumbnailUrl("https://i.imgur.com/ppXRHTi.jpg")
                    .WithImageUrl("https://i.imgur.com/qGb6xtG.jpg")
                    .WithColor(Color.DarkPurple)
                .Build()); ;

        }

        private void help(CommandContext contexto, object[] args)
        {
            ((IUserMessage)args[1]).DeleteAsync();
            ((ReactionControler)args[2]).DesligarReaction();
            IUserMessage cmds = contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle(StringCatch.GetString("helpModulo", "Modulo Ajuda (❓)"))
                    .WithDescription(StringCatch.GetString("helpInfo", "Esse modulo tem comandos para te ajudar na ultilização do bot. \n\nNão tenha medo eles não mordem 😉"))
                    .WithColor(Color.DarkPurple)
                    .AddField(StringCatch.GetString("helpCmdsTxt", "Comandos:"), StringCatch.GetString("helpCmds", "`{0}ajuda`, `{0}comandos`, `{0}info`", (string)args[0]))
                    .WithFooter(StringCatch.GetString("helpVoltar", "Voltar"), StringCatch.GetString("helpVoltarImg", "https://i.imgur.com/iAnGwW4.png"))
                    .WithImageUrl(StringCatch.GetString("helpImg", "https://i.imgur.com/XQTVJu9.jpg"))
                .Build()).GetAwaiter().GetResult();

            Emoji emoji = new Emoji("⬅");
            cmds.AddReactionAsync(emoji);
            ReactionControler reaction = new ReactionControler();
            args[1] = cmds;
            reaction.GetReaction(cmds, emoji, contexto.User, new ReturnMethod(comandos, contexto, args));
        }
        private void utilidade(CommandContext contexto, object[] args)
        {
            ((IUserMessage)args[1]).DeleteAsync();
            ((ReactionControler)args[2]).DesligarReaction();
            IUserMessage cmds = contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle(StringCatch.GetString("utilidadeModulo", "Modulo Ultilidades (🛠)"))
                    .WithDescription(StringCatch.GetString("utilidadeInfo", "Esse modulo possui coisas uteis pro seu dia a dia. \n\nAaaaaaa eles são tão legais ☺"))
                    .WithColor(Color.DarkPurple)
                    .AddField(StringCatch.GetString("utilidadeCmdsTxt", "Comandos:"), StringCatch.GetString("utiliidadeCmds", "`{0}videochamada`, `{0}avatar`, `{0}emote`, `{0}say`, `{0}simg`, `{0}setprefix`", (string)args[0]))
                    .WithFooter(StringCatch.GetString("utilidadeVoltar", "Voltar"), StringCatch.GetString("utilidadeVoltarImg", "https://i.imgur.com/iAnGwW4.png"))
                    .WithImageUrl(StringCatch.GetString("utilidadeImg", "https://i.imgur.com/TK7zmb8.jpg"))
                .Build()).GetAwaiter().GetResult();

            Emoji emoji = new Emoji("⬅");
            cmds.AddReactionAsync(emoji);
            ReactionControler reaction = new ReactionControler();
            args[1] = cmds;
            reaction.GetReaction(cmds, emoji, contexto.User, new ReturnMethod(comandos, contexto, args));
        }
        private void moderacao(CommandContext contexto, object[] args)
        {
            ((IUserMessage)args[1]).DeleteAsync();
            ((ReactionControler)args[2]).DesligarReaction();
            IUserMessage cmds = contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle(StringCatch.GetString("moderacaoModulo", "Modulo Moderação (⚖)"))
                    .WithDescription(StringCatch.GetString("moderacaoInfo", "Esse modulo possui coisas para te ajudar moderar seu servidor. \n\nSó não seja malvado com seus amigos 😣"))
                    .WithColor(Color.DarkPurple)
                    .AddField(StringCatch.GetString("moderacaoCmdsTxt", "Comandos:"), StringCatch.GetString("modercaoCmds", "`{0}kick`, `{0}ban`, `{0}softban`", (string)args[0]))
                    .WithFooter(StringCatch.GetString("moderacaoVoltar", "Voltar"), StringCatch.GetString("moderacaoVoltarImg", "https://i.imgur.com/iAnGwW4.png"))
                    .WithImageUrl(StringCatch.GetString("moderacaoImg", "https://i.imgur.com/hiu0Vh0.jpg"))
                .Build()).GetAwaiter().GetResult();

            Emoji emoji = new Emoji("⬅");
            cmds.AddReactionAsync(emoji);
            ReactionControler reaction = new ReactionControler();
            args[1] = cmds;
            reaction.GetReaction(cmds, emoji, contexto.User, new ReturnMethod(comandos, contexto, args));
        }
        private void nsfw(CommandContext contexto, object[] args)
        {
            ((IUserMessage)args[1]).DeleteAsync();
            ((ReactionControler)args[2]).DesligarReaction();
            IUserMessage cmds = contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle(StringCatch.GetString("nsfwModulo", "Modulo NSFW (🔞)"))
                    .WithDescription(StringCatch.GetString("nsfwInfo", "Esse modulo possui coias para você dar orgulho para sua família. \n\nTenho medo dessas coisa 😣"))
                    .WithColor(Color.DarkPurple)
                    .AddField(StringCatch.GetString("nsfwCmdsTxt", "Comandos:"), StringCatch.GetString("nsfwCmds", "`{0}hentai`, `{0}hentaibomb`, `{0}anal`", (string)args[0]))
                    .WithFooter(StringCatch.GetString("nsfwVoltar", "Voltar"), StringCatch.GetString("nsfwVoltarImg", "https://i.imgur.com/iAnGwW4.png"))
                    .WithImageUrl(StringCatch.GetString("nsfwImg", "https://i.imgur.com/iGQ3SI8.png"))
                .Build()).GetAwaiter().GetResult();

            Emoji emoji = new Emoji("⬅");
            cmds.AddReactionAsync(emoji);
            ReactionControler reaction = new ReactionControler();
            args[1] = cmds;
            reaction.GetReaction(cmds, emoji, contexto.User, new ReturnMethod(comandos, contexto, args));
        }
        private void weeb(CommandContext contexto, object[] args)
        {
            ((IUserMessage)args[1]).DeleteAsync();
            ((ReactionControler)args[2]).DesligarReaction();
            IUserMessage cmds = contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle(StringCatch.GetString("weebModulo", "Modulo Weeb (❤)"))
                    .WithDescription(StringCatch.GetString("weebInfo", "Esse modulo é o mais amoroso de todos.  \n\nUse ele para distribuir o amor para seus amigos ❤"))
                    .WithColor(Color.DarkPurple)
                    .AddField(StringCatch.GetString("weebCmdsTxt", "Comandos:"), StringCatch.GetString("weebCmds", "`{0}hug`, `{0}slap`, `{0}kiss`, `{0}punch`, `{0}lick`, `{0}cry`, `{0}megumin`, `{0}rem`", (string)args[0]))
                    .WithFooter(StringCatch.GetString("weebVoltar", "Voltar"), StringCatch.GetString("weebVoltarImg", "https://i.imgur.com/iAnGwW4.png"))
                    .WithImageUrl(StringCatch.GetString("weebImg", "https://i.imgur.com/FmCmErd.png"))
                .Build()).GetAwaiter().GetResult();

            Emoji emoji = new Emoji("⬅");
            cmds.AddReactionAsync(emoji);
            ReactionControler reaction = new ReactionControler();
            args[1] = cmds;
            reaction.GetReaction(cmds, emoji, contexto.User, new ReturnMethod(comandos, contexto, args));
        }
        private void img(CommandContext contexto, object[] args)
        {
            ((IUserMessage)args[1]).DeleteAsync();
            ((ReactionControler)args[2]).DesligarReaction();
            IUserMessage cmds = contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle(StringCatch.GetString("imgModulo", "Modulo Imagem (🖼)"))
                    .WithDescription(StringCatch.GetString("imgInfo", "Esse modulopossui imagens fofinhas para agraciar seu computador.  \n\nKawaiii ❤❤❤"))
                    .WithColor(Color.DarkPurple)
                    .AddField(StringCatch.GetString("imgCmdsTxt", "Comandos:"), StringCatch.GetString("imgCmds", "`{0}cat`, `{0}magikavatar`, `{0}magik`", (string)args[0]))
                    .WithFooter(StringCatch.GetString("imgVoltar", "Voltar"), StringCatch.GetString("imgsVoltarImg", "https://i.imgur.com/iAnGwW4.png"))
                    .WithImageUrl(StringCatch.GetString("imgsImg", "https://i.imgur.com/cQqTUl1.png"))
                .Build()).GetAwaiter().GetResult();

            Emoji emoji = new Emoji("⬅");
            cmds.AddReactionAsync(emoji);
            ReactionControler reaction = new ReactionControler();
            args[1] = cmds;
            reaction.GetReaction(cmds, emoji, contexto.User, new ReturnMethod(comandos, contexto, args));
        }
        private void customReaction(CommandContext contexto, object[] args)
        {
            ((IUserMessage)args[1]).DeleteAsync();
            ((ReactionControler)args[2]).DesligarReaction();
            IUserMessage cmds = contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle(StringCatch.GetString("acrModulo", "Modulo Reações Customizadas (💬)"))
                    .WithDescription(StringCatch.GetString("acrInfo", "Esse modulo possui comandos para você controlar as minhas Reações Customizadas. \n\nEu adoro usar elas para me divertir com vocês 😂"))
                    .WithColor(Color.DarkPurple)
                    .AddField(StringCatch.GetString("acrCmdsTxt", "Comandos:"), StringCatch.GetString("acrCmds", "`{0}acr`, `{0}dcr`, `{0}lcr`", (string)args[0]))
                    .WithFooter(StringCatch.GetString("acrVoltarTxt", "Voltar"), StringCatch.GetString("acrVoltarImg", "https://i.imgur.com/iAnGwW4.png"))
                    .WithImageUrl(StringCatch.GetString("acrImg", "https://i.imgur.com/AUpMkBP.jpg"))
                .Build()).GetAwaiter().GetResult();

            Emoji emoji = new Emoji("⬅");
            cmds.AddReactionAsync(emoji);
            ReactionControler reaction = new ReactionControler();
            args[1] = cmds;
            reaction.GetReaction(cmds, emoji, contexto.User, new ReturnMethod(comandos, contexto, args));
        }
        private void configuracoes(CommandContext contexto, object[] args)
        {
            ((IUserMessage)args[1]).DeleteAsync();
            ((ReactionControler)args[2]).DesligarReaction();
            IUserMessage cmds = contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle(StringCatch.GetString("configsModulo", "Modulo Configurações (⚙)"))
                    .WithDescription(StringCatch.GetString("ConfigsInfo", "Em configurações você define preferencias de como agirei em seu servidor. \n\nTenho certeza que podemos ficar mais intimos assim 😄"))
                    .WithColor(Color.DarkPurple)
                    .AddField(StringCatch.GetString("configsCmdsTxt", "Comandos:"), StringCatch.GetString("configsCmds", "`{0}setprefix`", (string)args[0]))
                    .WithFooter(StringCatch.GetString("configsVoltarTxt", "Voltar"), StringCatch.GetString("configsVoltarImg", "https://i.imgur.com/iAnGwW4.png"))
                    .WithImageUrl(StringCatch.GetString("configsImg", "https://i.imgur.com/vVBOIB2.gif"))
                .Build()).GetAwaiter().GetResult();

            Emoji emoji = new Emoji("⬅");
            cmds.AddReactionAsync(emoji);
            ReactionControler reaction = new ReactionControler();
            args[1] = cmds;
            reaction.GetReaction(cmds, emoji, contexto.User, new ReturnMethod(comandos, contexto, args));
        }

        public void MessageEventExceptions(Exception e, CommandContext contexto, Servidores servidor)
        {
            if(e is NullReferenceException || e is AmbiguousMatchException)
            {
                contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithDescription(StringCatch.GetString("msgEventNotFoundCommand", " **{0}** comando não encontrado use `{1}comandos` para ver os meus comandos", contexto.User.ToString(), new string(servidor.prefix)))
                    .WithColor(Color.DarkPurple)
                    .Build());
            }
            else
            {
                MethodInfo metodo = SingletonLogs.tipo.GetMethod("Log");
                object[] parms = new object[1];
                parms[0] = e.ToString();
                metodo.Invoke(SingletonLogs.instanced, parms);
            }
        }

        public void MentionMessage(CommandContext context, Servidores servidores)
        {
            context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                .WithDescription(StringCatch.GetString("msgEventPrefixInform", "Oii {0} meu prefixo é: `{1}` se quiser ver meus comando é so usar: `{1}comandos`", context.User.Username, new string(servidores.prefix)))
                .WithColor(Color.DarkPurple)
                .Build());
        }
    }


}
