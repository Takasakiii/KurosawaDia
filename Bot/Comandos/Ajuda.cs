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
                        .AddField(StringCatch.GetString("cmdsModulos", "Modulos:"), StringCatch.GetString("cmdsModulosLista", "❓ Ajuda;\n🛠 Ultilidades;\n⚖ Moderação;\n🔞 NSFW;\n❤ Weeb;\n🖼 Imagens;\n💬 Reações Customizadas."))
                        .WithImageUrl(StringCatch.GetString("cmdsImg", "https://i.imgur.com/mQVFSrP.gif"))
                        .WithColor(Color.DarkPurple)
                .Build()).GetAwaiter().GetResult();

            Emoji[] menu = new Emoji[7];
            menu[0] = new Emoji("❓");
            menu[1] = new Emoji("🛠");
            menu[2] = new Emoji("⚖");
            menu[3] = new Emoji("🔞");
            menu[4] = new Emoji("❤");
            menu[5] = new Emoji("🖼");
            menu[6] = new Emoji("💬");

            RequestOptions opc = new RequestOptions();
            opc.RetryMode = RetryMode.RetryTimeouts;
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
                    .AddField(StringCatch.GetString("infoConvites", "**Quer me ajudar????**"), StringCatch.GetString("infoConvites", "[Adicione-me em seu Servidor](https://ayura.com.br/links/bot)\n[Entre em meu servidor para dar suporte ao projeto](https://ayura.com.br/dia)"))
                    .AddField(StringCatch.GetString("infoOutras", "**Informações chatas:**"), StringCatch.GetString("infoOutrasInfos", "__Ping:__ {0}ms\n__Servidores:__ {1}\n__Usuarios:__ {2}", client.Latency, client.Guilds.Count, users), false)
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
                    .WithFooter(StringCatch.GetString("helpVoltar", "Voltar"), StringCatch.GetString("voltarImg", "https://i.imgur.com/iAnGwW4.png"))
                    .WithImageUrl("https://i.imgur.com/XQTVJu9.jpg")
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
                    .WithTitle("Modulo Ultilidades (🛠)")
                    .WithDescription("Esse modulo possui coisas uteis pro seu dia a dia. \n\nAaaaaaa eles são tão legais ☺")
                    .WithColor(Color.DarkPurple)
                    .AddField("Comandos:", $"`{(string)args[0]}videochamada`, `{(string)args[0]}avatar`, `{(string)args[0]}emote`, `{(string)args[0]}say`, `{(string)args[0]}simg`, `{(string)args[0]}setprefix`")
                    .WithFooter("Voltar", "https://i.imgur.com/iAnGwW4.png")
                    .WithImageUrl("https://i.imgur.com/TK7zmb8.jpg")
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
                    .WithTitle("Modulo Moderação (⚖)")
                    .WithDescription("Esse modulo possui coisas para te ajudar moderar seu servidor. \n\nSó não seja malvado com seus amigos 😣")
                    .WithColor(Color.DarkPurple)
                    .AddField("Comandos:", $"`{(string)args[0]}kick`, `{(string)args[0]}ban`, `{(string)args[0]}softban`")
                    .WithFooter("Voltar", "https://i.imgur.com/iAnGwW4.png")
                    .WithImageUrl("https://i.imgur.com/hiu0Vh0.jpg")
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
                    .WithTitle("Modulo NSFW (🔞)")
                    .WithDescription("Esse modulo possui coias para você dar orgulho para sua família. \n\nTenho medo dessas coisa 😣")
                    .WithColor(Color.DarkPurple)
                    .AddField("Comandos:", $"`{(string)args[0]}hentai`, `{(string)args[0]}hentaibomb`, `{(string)args[0]}hneko`, `{(string)args[0]}anal`")
                    .WithFooter("Voltar", "https://i.imgur.com/iAnGwW4.png")
                    .WithImageUrl("https://i.imgur.com/iGQ3SI8.png")
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
                    .WithTitle("Modulo Weeb (❤)")
                    .WithDescription("Esse modulo é o mais amoroso de todos.  \n\nUse ele para distribuir o amor para seus amigos ❤")
                    .WithColor(Color.DarkPurple)
                    .AddField("Comandos:", $"`{(string)args[0]}hug`, `{(string)args[0]}slap`, `{(string)args[0]}kiss`, `{(string)args[0]}punch`, `{(string)args[0]}lick`, `{(string)args[0]}cry`, `{(string)args[0]}megumin`, `{(string)args[0]}rem`")
                    .WithFooter("Voltar", "https://i.imgur.com/iAnGwW4.png")
                    .WithImageUrl("https://i.imgur.com/FmCmErd.png")
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
                    .WithTitle("Modulo Imagem (🖼)")
                    .WithDescription("Esse modulopossui imagens fofinhas para agraciar seu computador.  \n\nKawaiii ❤❤❤")
                    .WithColor(Color.DarkPurple)
                    .AddField("Comandos:", $"`{(string)args[0]}neko`, `{(string)args[0]}cat`, `{(string)args[0]}img`, `{(string)args[0]}magikavatar`, `{(string)args[0]}magik`")
                    .WithFooter("Voltar", "https://i.imgur.com/iAnGwW4.png")
                    .WithImageUrl("https://i.imgur.com/cQqTUl1.png")
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
                    .WithTitle("Modulo Reações Customizadas (💬)")
                    .WithDescription("Esse modulo possui comandos para você controlar as minhas Reações Customizadas. \n\nEu adoro usar elas para me divertir com vocês 😂")
                    .WithColor(Color.DarkPurple)
                    .AddField("Comandos:", $"`{(string)args[0]}acr`, `{(string)args[0]}dcr`, `{(string)args[0]}lcr`")
                    .WithFooter("Voltar", "https://i.imgur.com/iAnGwW4.png")
                    .WithImageUrl("https://i.imgur.com/AUpMkBP.jpg")
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
