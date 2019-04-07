using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Bot.Comandos
{
    public class Ajuda : Image
    {
        public void ajuda(CommandContext context, object[] args)
        {
            context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                .WithColor(Color.DarkPurple)
                .WithDescription($"Oii {context.User} você pode usar `{(string)args[0]}comandos` para ver os comandos que eu tenho <:hehe:555914678866280448>")
                .WithFooter("Um projeto by Zuraaa!", "https://i.imgur.com/Cm8grM4.png")
                .Build());
        }

        public void comandos(CommandContext context, object[] args)
        {
            string prefix = (string)args[0];
            Embed embed = new EmbedBuilder()
                .WithTitle("Esses são os meus comandos")
                .AddField("Comandos de Utilidades:", $"`{prefix}webcam`, `{prefix}avatar`, `{prefix}emote`, `{prefix}say`")
                .AddField("Comandos de Ajuda:", $"`{prefix}ajuda`, `{prefix}comandos`, `{prefix}convite`")
                .AddField("Comandos de Imagens", $"`{prefix}neko`, `{prefix}cat`, `{prefix}img`")
                .AddField("Comandos NSFW", $"`{prefix}hentai`")
                .AddField("Comandos Weeb", $"`{prefix}hug`, `{prefix}slap`, `{prefix}kiss`, `{prefix}punch`, `{prefix}lick`, `{prefix}cry`")
                .AddField("Comandos de moderação", $"`{prefix}kick`")
                .WithThumbnailUrl(context.Client.CurrentUser.GetAvatarUrl(0, 2048))
                .WithColor(Color.DarkPurple)
                .Build();

            if(!context.IsPrivate)
            {
                try
                {
                    SocketGuildUser user = context.User as SocketGuildUser;
                    IDMChannel prive = user.GetOrCreateDMChannelAsync().GetAwaiter().GetResult();
                    context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithColor(Color.DarkPurple)
                        .WithDescription($"**{context.User}** eu enviarei a lista dos meus comandos no seu privado 😜")
                       .Build());
                    prive.SendMessageAsync(embed: embed);
                } catch
                {
                    context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithDescription($"**{context.User}** eu não consegui enviar a lista dos meus comandos no seu privado 😔")
                        .WithColor(Color.DarkPurple)
                     .Build());
                }
            } else
            {
                context.Channel.SendMessageAsync(embed: embed);
            }
        }

        public void convite(CommandContext context, object[] args)
        {
            string msg = "Aqui esta meu convite, Espero que goste de mim :3\nhttps://ayura.com.br/links/bot\n\nse não se importar eu se tiver algum problema, você pode entrar no meu servidor de suporte (ou não so meu)\nhttps://discord.gg/JuWpeNY";
            try
            {
                if (!context.IsPrivate)
                {
                    IDMChannel privado = context.User.GetOrCreateDMChannelAsync().GetAwaiter().GetResult();
                    privado.SendMessageAsync(msg);

                    context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithDescription($"**{context.User}** eu enviei os meus convites no seu privado")
                            .WithColor(Color.DarkPurple)
                        .Build());
                }
                else
                {
                    context.Channel.SendMessageAsync(msg);
                }
            }
            catch
            {
                context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithDescription($"**{context.User}** eu não consegui enviar os meu convites no seu privado 😔")
                    .WithColor(Color.DarkPurple)
                .Build());
            }
        }
    }
}
