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
            Embed embed = new EmbedBuilder()
                .WithTitle("Esses são os meus comandos")
                .AddField("Comandos de Utilidades:", $"`{(string)args[0]}webcam`, `{(string)args[0]}avatar`, `{(string)args[0]}emote`, `{(string)args[0]}say`, `{(string)args[0]}simg`")
                .AddField("Comandos de Ajuda:", $"`{(string)args[0]}ajuda`, `{(string)args[0]}comandos`, `{(string)args[0]}convite`, `{(string)args[0]}info`")
                .AddField("Comandos de Imagens", $"`{(string)args[0]}neko`, `{(string)args[0]}cat`, `{(string)args[0]}img`")
                .AddField("Comandos NSFW", $"`{(string)args[0]}hentai`, `{(string)args[0]}hentaibomb`")
                .AddField("Comandos Weeb", $"`{(string)args[0]}hug`, `{(string)args[0]}slap`, `{(string)args[0]}kiss`, `{(string)args[0]}punch`, `{(string)args[0]}lick`, `{(string)args[0]}cry` , `{(string)args[0]}megumin` , `{(string)args[0]}rem`")
                .AddField("Comandos de moderação", $"`{(string)args[0]}kick`")
                .WithThumbnailUrl(context.Client.CurrentUser.GetAvatarUrl(0, 2048))
                .WithImageUrl("https://i.imgur.com/ifjBm06.png")
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

        public void info(CommandContext context, object[] args)
        {
            DiscordSocketClient client = context.Client as DiscordSocketClient;
            int users = 0;
            foreach(SocketGuild servidor in client.Guilds)
            {
                users = servidor.Users.Count;
            }

            context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle("Minhas informações:")
                    .AddField("Meus convites", "[Me convidar para seu servidor](https://ayura.com.br/links/bot)\n[Meu servidor para suporte](https://ayura.com.br/dia)")
                    .AddField("Informações do bot", "**Criador:** Yummi#1375\n**Projeto:** Zuraaa!\n**Versão:** 1.0.0", true)
                    .AddField("Outras Informações:", $"**Ping:** {client.Latency}ms\n**Servidores:** {client.Guilds.Count}\n**Usuarios:** {users}", true)
                    .WithColor(Color.DarkPurple)
                .Build());
        }
    }

    //o jogo
}
