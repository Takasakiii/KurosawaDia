using Bot.Extensions;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;

namespace Bot.Comandos
{
    public class Moderacao
    {
        /*
            pra que server
            tantos codigos
            se a vida
            não é programada
            e as melhores coisas
            não tem logica
        */
        private void moderacao(int tipo, CommandContext context, object[] args)
        {
            new Thread(() =>
            {
                if (!context.IsPrivate)
                {
                    string[] msg = new string[4];
                    switch (tipo)
                    {
                        case 1:
                            msg[0] = $"você foi expulso do servidor **{context.Guild.Name}**";
                            msg[1] = $"expulsar";
                            msg[2] = "kick @Takasaki#7072 abre o servidor";
                            msg[3] = "kick";
                            break;
                        case 2:
                            msg[0] = $"você foi banido do servidor **{context.Guild.Name}**";
                            msg[1] = $"banir";
                            msg[2] = "ban @Thhrag#2527 vai pra escola";
                            msg[3] = "ban";
                            break;
                        case 3:
                            msg[0] = $"você foi banido temporariamente do servidor **{context.Guild.Name}**";
                            msg[1] = $"banir";
                            msg[2] = "softban @Sakurako Oomuro#5964 muito trap larissinha";
                            msg[3] = "softban";
                            break;
                    }
                    System.Tuple<bool, IUser> getUser = new Extensions.UserExtensions().GetUserAsync(context, args);
                    if (getUser.Item1 && getUser.Item2 != null)
                    {
                        SocketGuildUser user = getUser.Item2 as SocketGuildUser;
                        SocketGuildUser ContextUser = context.User as SocketGuildUser;

                        if ((ContextUser.GuildPermissions.KickMembers && tipo == 1) || (ContextUser.GuildPermissions.BanMembers && tipo == 2 || tipo == 3))
                        {
                            string[] comando = (string[])args[1];
                            SocketGuildUser bot = context.Guild.GetUserAsync(context.Client.CurrentUser.Id).GetAwaiter().GetResult() as SocketGuildUser;
                            if (ContextUser.Hierarchy > user.Hierarchy && user != bot && bot.Hierarchy > user.Hierarchy)
                            {
                                string motivo = "";

                                for (int i = 2; i < comando.Length; i++)
                                {
                                    motivo += $"{comando[i]} ";
                                }

                                IDMChannel privado = user.GetOrCreateDMChannelAsync().GetAwaiter().GetResult();

                                string log = "";
                                if (motivo != "")
                                {
                                    log = $"Moderador: {context.User}  ||  Motivo: {motivo}";
                                    privado.SendMessageAsync(embed: new EmbedBuilder()
                                                .WithTitle(msg[0])
                                                .AddField("Motivo", motivo)
                                                .WithColor(Color.Red)
                                            .Build());
                                }
                                else
                                {
                                    log = $"Moderador: {context.User}";
                                    privado.SendMessageAsync(embed: new EmbedBuilder()
                                                .WithTitle(msg[0])
                                                .WithColor(Color.Red)
                                            .Build());
                                }

                                Thread.Sleep(1000);

                                switch (tipo)
                                {
                                    case 1:
                                        user.KickAsync(log).GetAwaiter().GetResult();
                                        context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                                .WithDescription($"**{context.User}** o membro {user.Mention} foi expulso")
                                                .WithColor(Color.DarkPurple)
                                            .Build());
                                        break;
                                    case 2:
                                        user.BanAsync(7, log).GetAwaiter().GetResult();
                                        context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                                .WithDescription($"**{context.User}** o membro {user.Mention} foi banido")
                                                .WithColor(Color.DarkPurple)
                                            .Build());
                                        break;
                                    case 3:
                                        user.BanAsync(7, log).GetAwaiter().GetResult();
                                        context.Guild.RemoveBanAsync(user);
                                        context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                                .WithDescription($"**{context.User}** o membro {user.Mention} foi banido temporariamente")
                                                .WithColor(Color.DarkPurple)
                                            .Build());
                                        break;
                                }
                            }
                            else
                            {
                                context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                    .WithDescription($"**{context.User}** você não pode {msg[1]} esse membro")
                                    .WithColor(Color.Red)
                                .Build());
                            }
                        }
                        else
                        {
                            context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                .WithDescription($"**{context.User}** você não tem permissão para {msg[1]} esse membro")
                                .WithColor(Color.Red)
                            .Build());
                        }
                    }
                    else
                    {
                        context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithDescription($"**{context.User}** você precisa mencionar quem você quer {msg[1]}")
                            .AddField("Uso do Comando: ", $"`{(string)args[0]}{msg[3]} @membro motivo`")
                            .AddField("Exemplo: ", $"`{(string)args[0]}{msg[2]}`")
                            .WithColor(Color.Red)
                        .Build());
                    }
                }
                else
                {
                    context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithDescription($"**{context.User}** esse comando so pode ser usado em servidores")
                            .WithColor(Color.Red)
                        .Build());
                }

            }).Start();
        }

        public void kick(CommandContext context, object[] args)
        {
            moderacao(1, context, args);
        }
        public void ban(CommandContext context, object[] args)
        {
            moderacao(2, context, args);
        }
        public void softban(CommandContext context, object[] args)
        {
            moderacao(3, context, args);
        }

        public void unban(CommandContext context, object[] args)
        {

        }
    }
}


