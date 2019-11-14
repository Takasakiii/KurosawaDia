using Bot.Extensions;
using Bot.GenericTypes;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Threading;

namespace Bot.Comandos
{
    public class Moderacao : GenericModule
    {
        public Moderacao(CommandContext contexto, object[] args) : base (contexto, args)
        {
                /*
             *  pra que server
             *  tantos codigos
             *  se a vida
             *  não é programada
             *  e as melhores coisas
             *  não tem logica
            */
            /*
             *  Pra que serve
             *  o dinheiro 
             *  se a melhores coisas
             *  são feitas de graça
             *      - Takasaki 2019 
             *      (so vc pegar mina kkkkkkkkkkkkk ou ter amigos XD)
             */
            /* 
             * 
             * (essa eh minha filosofia não en nada você que cria o significado de tudo)
             * 
             */
             //Quem inventa é o inventor, segue o esperado pq vc não é inventor caralho
        }

        private void moderacao(int tipo) 
        {
            if (!Contexto.IsPrivate)
            {
                new Thread(() =>
                {
                    string[] comando = (string[])args[1];
                    string msg = string.Join(" ", comando, 1, (comando.Length - 1));

                    Tuple<IUser, string> getUser = new Extensions.UserExtensions().GetUser(Contexto.Guild.GetUsersAsync().GetAwaiter().GetResult(), msg);

                    if (getUser.Item1 != null)
                    {
                        SocketGuildUser user = getUser.Item1 as SocketGuildUser;
                        SocketGuildUser author = Contexto.User as SocketGuildUser;
                        SocketGuildUser bot = Contexto.Guild.GetUserAsync(Contexto.Client.CurrentUser.Id).GetAwaiter().GetResult() as SocketGuildUser;

                        EmbedBuilder embed = new EmbedBuilder();
                        embed.WithColor(Color.Red);

                        bool perm = false, botPerm = false;
                        switch (tipo)
                        {
                            case 1:
                                perm = author.GuildPermissions.KickMembers;
                                botPerm = bot.GuildPermissions.KickMembers;
                                embed.WithDescription(StringCatch.GetString("kickPerm", "**{0}** você não pode expulsar esse usuario", Contexto.User.ToString()));
                                break;
                            case 2:
                            case 3:
                                perm = author.GuildPermissions.BanMembers;
                                botPerm = bot.GuildPermissions.BanMembers;
                                embed.WithDescription(StringCatch.GetString("banPerm", "**{0}** você não pode banir esse usuario", Contexto.User.ToString()));
                                break;
                        }

                        if (author.Hierarchy > user.Hierarchy && user != bot && bot.Hierarchy > user.Hierarchy && perm && botPerm)
                        {
                            string motivo = getUser.Item2;
                            if (motivo.Length > 1024)
                            {
                                motivo = motivo.Substring(0, 1024);
                            }

                            IDMChannel privado = user.GetOrCreateDMChannelAsync().GetAwaiter().GetResult();

                            EmbedBuilder embedo = new EmbedBuilder();
                            embedo.WithColor(Color.DarkPurple);

                            switch (tipo)
                            {
                                case 1:
                                    if(motivo == "")
                                    {
                                        privado.SendMessageAsync(embed: new EmbedBuilder()
                                                .WithDescription(StringCatch.GetString("kickExpulso", "você foi expulso do servidor: **{0}**", Contexto.Guild.Name))
                                                .WithColor(Color.DarkPurple)
                                            .Build());
                                        motivo = StringCatch.GetString("kickModerador", "Moderador: {0}", Contexto.User);
                                    }
                                    else
                                    {
                                        privado.SendMessageAsync(embed: new EmbedBuilder()
                                                .WithDescription(StringCatch.GetString("kickExpulso", "você foi expulso do servidor: **{0}**", Contexto.Guild.Name))
                                                .AddField(StringCatch.GetString("kickMotivo", "Motivo:"), motivo)
                                                .WithColor(Color.DarkPurple)
                                            .Build());
                                        motivo = StringCatch.GetString("kickModeradorMotivo", "Moderador: {0} || Motivo: {1}", Contexto.User.ToString(), motivo);
                                    }

                                    if (motivo.Length > 512)
                                    {
                                        motivo = motivo.Substring(0, 512);
                                    }

                                    Thread.Sleep(1000);

                                    user.KickAsync(motivo);

                                    embedo.WithDescription(StringCatch.GetString("kickTxt", "**{0}** o membro {1} foi expulso do servidor", Contexto.User.ToString(), user.Mention));
                                    Contexto.Channel.SendMessageAsync(embed: embedo.Build());
                                    break;
                                case 2:
                                    if (motivo == "")
                                    {
                                        privado.SendMessageAsync(embed: new EmbedBuilder()
                                                .WithDescription(StringCatch.GetString("banBanido", "você foi banido do servidor: **{0}**", Contexto.Guild.Name))
                                                .WithColor(Color.DarkPurple)
                                            .Build());
                                        motivo = StringCatch.GetString("banModerador", "Moderador: {0}", Contexto.User.ToString());
                                    }
                                    else
                                    {
                                        privado.SendMessageAsync(embed: new EmbedBuilder()
                                                .WithDescription(StringCatch.GetString("banBanido", "você foi banido do servidor: **{0}**", Contexto.Guild.Name))
                                                .AddField(StringCatch.GetString("banMotivo", "Motivo:"), motivo)
                                                .WithColor(Color.DarkPurple)
                                            .Build());
                                        motivo = StringCatch.GetString("banModeradorMotivo", "Moderador: {0} || Motivo: {1}", Contexto.User.ToString(), motivo);
                                    }

                                    if (motivo.Length > 512)
                                    {
                                        motivo = motivo.Substring(0, 512);
                                    }

                                    Thread.Sleep(1000);

                                    user.BanAsync(7, motivo);

                                    embedo.WithDescription(StringCatch.GetString("banMembroBanido", "**{0}** o membro {1} foi banido do servidor", Contexto.User.ToString(), user.Mention));
                                    Contexto.Channel.SendMessageAsync(embed: embedo.Build());
                                    break;
                                case 3:
                                    if (motivo == "")
                                    {
                                        privado.SendMessageAsync(embed: new EmbedBuilder()
                                                .WithDescription(StringCatch.GetString("softbanExpulso", "você foi banido temporariamente do servidor: **{0}**", Contexto.Guild.Name))
                                                .WithColor(Color.DarkPurple)
                                            .Build());
                                        motivo = StringCatch.GetString("softbanModerador", "Moderador: {0}", Contexto.User.ToString());
                                    }
                                    else
                                    {
                                        privado.SendMessageAsync(embed: new EmbedBuilder()
                                                .WithDescription(StringCatch.GetString("softbanExpulso", "você foi banido temporariamente do servidor: **{0}**", Contexto.Guild.Name))
                                                .AddField("Motivo:", motivo)
                                                .WithColor(Color.DarkPurple)
                                            .Build());
                                        motivo = StringCatch.GetString("softbanModeradorMotivo", "Moderador: {0} || Motivo: {1}", Contexto.User.ToString(), motivo);
                                    }

                                    if (motivo.Length > 512)
                                    {
                                        motivo = motivo.Substring(0, 512);
                                    }

                                    Thread.Sleep(1000);

                                    user.BanAsync(7, motivo);
                                    Contexto.Guild.RemoveBanAsync(user);

                                    embedo.WithDescription(StringCatch.GetString("softbanMembroExpulso", "**{0}** o membro {1} foi banido temporariamente do servidor", Contexto.User.ToString(), user.Mention));
                                    Contexto.Channel.SendMessageAsync(embed: embedo.Build());
                                    break;
                            }

                        }
                        else
                        {
                            Contexto.Channel.SendMessageAsync(embed: embed.Build());
                        }
                    }
                    else
                    {
                        EmbedBuilder usoEmbed = new EmbedBuilder();
                        usoEmbed.WithColor(Color.Red);
                        usoEmbed.WithDescription(StringCatch.GetString("moderacaoMembro", "**{0}** você precisa mencionar um membro", Contexto.User.ToString()));

                        switch (tipo)
                        {
                            case 1:
                                usoEmbed.AddField(StringCatch.GetString("usosComando", "Usos do Comando: "), StringCatch.GetString("kickUsos", "`{0}kick @membro motivo`\n`{0}kick <id membro> motivo`", (string)args[0]));
                                usoEmbed.AddField(StringCatch.GetString("exemplo", "Exemplos: "), StringCatch.GetString("kickExemplos", "`{0}kick @Takasaki#7072 abre o servidor`\n`{0}kick 274289097689006080 abre o servidor`", (string)args[0]));
                                break;
                            case 2:
                                usoEmbed.AddField(StringCatch.GetString("usosComando", "Usos do Comando: "), StringCatch.GetString("banUsos", "`{0}ban @membro motivo`\n`{0}ban <id membro> motivo`", (string)args[0]));
                                usoEmbed.AddField(StringCatch.GetString("exemplo", "Exemplos: "), StringCatch.GetString("banExemplos", "`{0}ban @Thhrag#2527 vai pra escola`\n`{0}ban 240860729027198977 vai pra escola`", (string)args[0]));
                                break;
                            case 3:
                                usoEmbed.AddField(StringCatch.GetString("usosComando", "Usos do Comando: "), StringCatch.GetString("softbanUsos", "`{0}softban @membro motivo`\n`{0}softban <id membro> motivo`", (string)args[0]));
                                usoEmbed.AddField(StringCatch.GetString("exemplo", "Exemplos: "), StringCatch.GetString("softbanExemplos", "`{0}softban @Sakurako Oomuro#5964 muito trap larissinha`\n`{0}softban 234097420898664448 muito trap larissinha`", (string)args[0]));
                                break;
                        }

                        Contexto.Channel.SendMessageAsync(embed: usoEmbed.Build());
                    }

                }).Start();

            }
            else
            {
                Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithDescription(StringCatch.GetString("moderacaoDm", "Você so pode usar esse comando em servidores"))
                        .WithColor(Color.Red)
                    .Build());
            }
        }

        public void kick()
        {
            moderacao(1); 
        }
        public void ban()
        {
            moderacao(2); 
        }
        public void softban()
        {
            moderacao(3);
        }
    }
}




