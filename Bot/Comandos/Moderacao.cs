using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Linq;
using System.Threading;

namespace Bot.Comandos
{
    public class Moderacao
    {
        public void kick(CommandContext context, object[] args)
        {
            new Thread(() =>
            {
                if(!context.IsPrivate)
                {
                    SocketGuildUser userGuild = context.User as SocketGuildUser;
                    if(userGuild.GuildPermissions.KickMembers)
                    {
                        string[] comando = (string[])args[1];

                        IGuildUser user;
                        try
                        {
                            if (context.Message.MentionedUserIds.Count != 0)
                            {
                                user = context.Guild.GetUserAsync(context.Message.MentionedUserIds.ElementAt(0)).GetAwaiter().GetResult();
                            }
                            else
                            {
                                user = context.Guild.GetUserAsync(Convert.ToUInt64(comando[1])).GetAwaiter().GetResult();
                            }
                        }
                        catch
                        {
                            user = null;
                        }

                        if (user != null)
                        {
                            if(user.Id != context.Client.CurrentUser.Id)
                            {
                                string motivo = "";
                                for (int i = 2; i < comando.Length; i++)
                                {
                                    motivo += comando[i] + " ";
                                }
                                IDMChannel privado = user.GetOrCreateDMChannelAsync().GetAwaiter().GetResult();

                                if (motivo != "")
                                {
                                    privado.SendMessageAsync(embed: new EmbedBuilder()
                                            .WithTitle($"você foi kickado do servidor {context.Guild.Name}")
                                            .AddField("Motivo", motivo)
                                            .WithColor(Color.Red)
                                        .Build());
                                }
                                else
                                {
                                    privado.SendMessageAsync(embed: new EmbedBuilder()
                                            .WithTitle($"você foi kickado do servidor {context.Guild.Name}")
                                            .WithColor(Color.Red)
                                        .Build());
                                }
                                Thread.Sleep(1000);

                                

                                try
                                {
                                    user.KickAsync($"Moderador: {context.User}       Motivo: {motivo}");
                                    context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                            .WithDescription($"**{context.User}** o membro {user.Mention} foi kickado")
                                            .WithColor(Color.DarkPurple)
                                        .Build());
                                } catch
                                {
                                    context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                            .WithDescription($"**{context.User}**vc disse batata")
                                            .WithColor(Color.DarkPurple)
                                        .Build());
                                }
                            } else
                            {
                                context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                        .WithDescription($"**{context.User}** por-favor não me kicke desu")
                                        .WithColor(Color.DarkPurple)
                                    .Build());
                            }
                        } else
                        {
                            context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                    .WithTitle("Você precisa marcar o usuario que deseja kickar")
                                    .AddField("Uso do comando:", $"`{(string)args[0]}kick @usuario motivo\n`")
                                    .AddField("Exemplo:", $"`{(string)args[0]}kick @Takasaki abre o servidor`")
                                    .WithColor(Color.Red)
                                .Build());
                        }
                    } else
                    {
                        context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                .WithDescription($"**{context.User}** você não tem a pdoe de kickar membros")
                                .WithColor(Color.Red)
                            .Build());
                    }
                } else
                {
                    context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithDescription("Esse comando só pode ser usado em servidores")
                            .WithColor(Color.Red)
                        .Build());
                }
            }).Start();
        }
    }
}
// n conta