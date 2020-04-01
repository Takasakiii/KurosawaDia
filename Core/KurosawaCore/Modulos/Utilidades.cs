using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using KurosawaCore.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KurosawaCore.Modulos
{
    public class Utilidades
    {
        [Command("videochamada")]
        [Aliases("webcam", "chamadadevideo")]
        [Description("Comando permite abrir uma chamada com Video de forma alternativa no servidor")]
        public async Task VideoChamada(CommandContext ctx)
        {
            if (!ctx.Channel.IsPrivate)
            {
                DiscordEmbedBuilder builder = new DiscordEmbedBuilder
                {
                    Color = DiscordColor.Green,
                    Description = $"Para acessar o compartilhamento de tela basta [clicar aqui](https://discordapp.com/channels/{ctx.Guild.Id}/{ctx.Member.VoiceState.Channel.Id}) :grinning:"
                };
                await ctx.RespondAsync(embed: builder.Build());
            }
        }

        [Command("emoji")]
        [Aliases("emote", "emogi")]
        [Description("Almenta o tamanho de um emote, e tambem permite você pegar a url do mesmo")]
        public async Task Emoji(CommandContext ctx, [Description("Emoji que você deseja visualizar")][RemainingText]DiscordEmoji emoji)
        {
            if (!ctx.Channel.IsPrivate)
            {
                DiscordEmojiExtension ex = new DiscordEmojiExtension(emoji);
                string url = await ex.GetUrl();
                DiscordEmbedBuilder eb = new DiscordEmbedBuilder
                {
                    Color = DiscordColor.Green,
                    Title = ex.Emoji.GetDiscordName().Replace(":", ""),
                    Description = $"[Link Direto]({url})",
                    ImageUrl = url
                };
                await ctx.RespondAsync(embed: eb.Build());
            }
        }

        [Command("avatar")]
        [Aliases("uimg")]
        [Description("Mostra a imagem de  perfil um usuario")]
        public async Task Avatar(CommandContext ctx, [Description("Usuario da pessoa que deseja pegar o avatar")][RemainingText]DiscordUser alvo = null)
        {
            string[] frases;
            if (alvo == null)
                alvo = ctx.User;
            if (alvo == ctx.Client.CurrentUser)
                frases = ArrayExtension.CriarArray("Ownt, que amor, você realmente quer me ver 😍", "Assim você me deixa sem jeito 😊");
            else
                frases = ArrayExtension.CriarArray("Nossa, que avatar bonito, agora sei porque você queria vê-lo 🤣", "Vocês são realmente criativos para avatares 😂", "Com avatar assim seria um disperdicio não se tornar idol 😃", "Talvez se você pusesse um filtro ficaria melhor... 🤐");
            int rnd = new Random().Next(0, frases.Length);
            DiscordEmbedBuilder eb = new DiscordEmbedBuilder
            {
                Title = frases[rnd],
                Description = $"{alvo.Username}#{alvo.Discriminator}\n[Link Direto]({alvo.AvatarUrl})",
                ImageUrl = alvo.AvatarUrl,
                Color = DiscordColor.Green
            };
            await ctx.RespondAsync(embed: eb.Build());
        }
    }
}
