using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Linq;

namespace Bot.Nucleo.Modulos
{
    public class Utility : Owner.Owner
    {
        public void avatar(CommandContext context, object[] args)
        {
            IUser user;
            try
            {
                if (context.Message.MentionedUserIds.Count != 0)
                {
                    user = context.Client.GetUserAsync(context.Message.MentionedUserIds.ElementAt(0)).GetAwaiter().GetResult();
                }
                else
                {
                    string[] comando = (string[])args[1]; //¯\_(ツ)_/¯
                    user = context.Client.GetUserAsync(Convert.ToUInt64(comando[1])).GetAwaiter().GetResult();
                }
            }
            catch
            {
                user = context.User;
            }

            if (user == null)
            {
                user = context.User; // avaliação desnesessaria 
            }
            string avatarUrl = user.GetAvatarUrl(0, 2048) ?? user.GetDefaultAvatarUrl();

            context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                .WithColor(Color.DarkPurple)
                .WithAuthor($"{user}")
                .WithDescription($"[Link Direto]({avatarUrl})")
                .WithImageUrl(avatarUrl)
            .Build());
        }

        public void webcam(CommandContext context, object[] args)
        {
            SocketGuildUser usr = context.User as SocketGuildUser;

            if (!context.IsPrivate && usr.VoiceChannel != null)
            {
                context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithColor(Color.DarkPurple)
                        .WithTitle($"Canal: {usr.VoiceChannel.Name}")
                        .WithDescription($"[clique aqui](https://discordapp.com/channels/{context.Guild.Id}/{usr.VoiceChannel.Id}) para poder compartilhar sua tela ou ligar sua webcam")
                .Build());
            }
            else
            {
                context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithColor(Color.Red)
                        .WithDescription("você precisa estar em um canal de voz e em um servidor para usar esse comando")
                .Build());
            }
        }

        public void emote(CommandContext context, object[] args)
        {
            string[] comando = (string[])args[1];
            try
            {
                Emote emote = Emote.Parse(comando[1]);
                context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                         .WithTitle(emote.Name)
                         .WithUrl(emote.Url)
                         .WithImageUrl(emote.Url)
                         .WithColor(Color.DarkPurple)
                     .Build());
            }
            catch (ArgumentException)
            {

                context.Channel.SendMessageAsync($"favor enfiar: {comando[1]} no cu do meu dono, agradecida");
            }
            catch (Exception e)
            {
                context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithDescription($"**{context.User}** o emoji que você tentou usar é inválido {e}")
                    .AddField("Uso do comando: ", $"`{(string)args[0]}emote emoji`")
                    .AddField("Exemplo: ", $"`{(string)args[0]}emote :kanna:`")
                    .WithColor(Color.Red)
                .Build());
            }

        }

        public void say(CommandContext context, object[] args)
        { 
            if(!context.IsPrivate)
            {
                string[] comando = (string[])args[1];
                string msg = string.Join(" ", comando, 1, (comando.Length - 1));


                if (msg != "")
                {
                    IGuildUser user = context.User as IGuildUser;
                    if (user.GuildPermissions.ManageMessages)
                    {
                        context.Message.DeleteAsync().GetAwaiter().GetResult();
                    }

                    context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithDescription(msg)
                            .WithColor(Color.DarkPurple)
                        .Build());
                }
                else
                {
                    context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithTitle("Você precisa de me falar uma mensagem")
                            .AddField("Uso do comando:", $"`{(string)args[0]}say <mensagem>`")
                            .AddField("Uso do comando:", $"`{(string)args[0]}say @Thhrag#2527 sai do facebook`")
                            .WithColor(Color.Red)
                        .Build());
                }
            } else
            {
                context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithDescription("você so pode usar esse comando em servidores")
                        .WithColor(Color.Red)
                    .Build());
            }
        }

        //nome bem top
        public void simg(CommandContext context, object[] args)
        {
            if(!context.IsPrivate)
            {
                string url = $"{context.Guild.IconUrl}?size=2048";
                context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithTitle(context.Guild.Name)
                        .WithDescription($"[Link Direto]({url})")
                        .WithImageUrl(url)
                        .WithColor(Color.DarkPurple)
                    .Build());
            } else
            {
                context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithDescription($"**{context.User}** esse comando so pode ser usado em servidores")
                        .WithColor(Color.Red)
                    .Build());
            }
        }

        public void bigtext(CommandContext context, object[] args)
        {
            string[] comando = (string[])args[1];
            string msg = string.Join(" ", comando, 1, (comando.Length -1));

            char[] aa = msg.ToCharArray();

            string txt = "";
            for(int i=0; i< aa.Length; i++)
            {
                if (aa[i].ToString() == " ")
                {
                    txt += " ";
                } else
                {
                    txt += $":regional_indicator_{aa[i]}:";
                }
            }
            context.Channel.SendMessageAsync(txt.Substring(0, txt.Length - 22));
        }

        public void sugestao(CommandContext context, object[] args)
        {
            string[] comando = (string[])args[1];
            string msg = string.Join(" ", comando, 1, (comando.Length - 1));

            if(msg != "")
            {
                IMessageChannel canal = context.Client.GetChannelAsync(556598669500088320).GetAwaiter().GetResult() as IMessageChannel;

                canal.SendMessageAsync(embed: new EmbedBuilder()
                        .WithTitle($"Nova sugestão de: {context.User}")
                        .AddField("Sugestão: ", msg)
                        .AddField("Servidor: ", context.Guild.Name)
                        .WithColor(Color.DarkPurple)
                    .Build());

                context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithDescription($"**{context.User}** sua sugestão foi enviada a o meu servidor")
                        .WithColor(Color.DarkPurple)
                    .Build());
            } else
            {
                context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithTitle("Você precisa me falara uma sugestão")
                        .AddField("Uso: ", $"`{args[0]}sugestao <sugestão>`")
                        .AddField("Exemplo: ", $"`{args[0]}sugestao fazer com que o bot ficasse mais tempo on`")
                        .WithColor(Color.Red)
                    .Build());
            }
        }
    }
}
