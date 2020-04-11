using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using KurosawaCore.Extensions;
using KurosawaCore.Extensions.JsonEmbedExtension;
using KurosawaCore.Models.Atributes;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KurosawaCore.Modulos
{
    [Modulo("Utilidade", "🛠")]
    [Description("Este módulo possui coisas uteis pro seu dia a dia.")]
    public class Utilidades
    {
        [Command("videochamada")]
        [Aliases("webcam", "chamadadevideo")]
        [Description("Comando permite abrir uma chamada com Video de forma alternativa no servidor")]
        public async Task VideoChamada(CommandContext ctx)
        {
            if (ctx.Channel.IsPrivate)
                throw new Exception();
            DiscordEmbedBuilder builder = new DiscordEmbedBuilder
            {
                Color = DiscordColor.Green,
                Description = $"Para acessar o compartilhamento de tela basta [clicar aqui](https://discordapp.com/channels/{ctx.Guild.Id}/{ctx.Member.VoiceState.Channel.Id}) :grinning:"
            };
            await ctx.RespondAsync(embed: builder.Build());

        }

        [Command("emoji")]
        [Aliases("emote", "emogi")]
        [Description("Almenta o tamanho de um emote, e tambem permite você pegar a url do mesmo")]
        public async Task Emoji(CommandContext ctx, [Description("Emoji que você deseja visualizar")][RemainingText]DiscordEmoji emoji)
        {
            if (ctx.Channel.IsPrivate || emoji == null)
                throw new Exception();
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

        [Command("serverimage")]
        [Aliases("simg")]
        [Description("Mostrar a imagem do servidor")]
        public async Task ServerImage(CommandContext ctx)
        {
            if (ctx.Channel.IsPrivate || ctx.Guild.IconUrl == null)
                throw new Exception();

            DiscordEmbedBuilder eb = new DiscordEmbedBuilder
            {
                Title = ctx.Guild.Name,
                Description = $"[Link Direto]({new ServerIconExtension().Get(ctx.Guild)})",
                ImageUrl = new ServerIconExtension().Get(ctx.Guild),
                Color = DiscordColor.Green
            };
            await ctx.RespondAsync(embed: eb);
        }

        [Command("sugestao")]
        [Description("Envie sugestões para nós")]
        public async Task Sugestao(CommandContext ctx, [Description("A sua sugestão")][RemainingText]string mensagem)
        {
            await ControladorDeSugestao(ctx, mensagem, "Sugestão");
        }

        [Command("bug")]
        [Description("Nos reporte um bug")]
        public async Task Bug(CommandContext ctx, [Description("O bug")][RemainingText]string mensagem)
        {
            await ControladorDeSugestao(ctx, mensagem, "Bug");
        }

        private async Task ControladorDeSugestao(CommandContext ctx, string mensagem, string tipo)
        {
            if (mensagem == "") 
                throw new Exception();

            DiscordChannel channel = await ctx.Client.GetChannelAsync(556598669500088320);

            DiscordEmbedBuilder eb = new DiscordEmbedBuilder
            {
                Title = $"Nova sugestão de: {ctx.User.Username}#{ctx.User.Discriminator}",
                Color = DiscordColor.Green
            };
            eb.AddField($"{tipo}: ", mensagem);
            eb.AddField("Servidor: ", (ctx.Guild == null) ? "Privado" : ctx.Guild.Name);
            eb.AddField("Mais informações: ", $"Channel: {ctx.Channel.Id}\n UserId: {ctx.User.Id}");

            await channel.SendMessageAsync(embed: eb);

            await ctx.RespondAsync(embed: new DiscordEmbedBuilder
            {
                Description = $"**{ctx.User.Username}#{ctx.User.Discriminator}**, obrigada! Vou usa-lá para melhorarmos ❤",
                Color = DiscordColor.Green
            });
        }

        [Command("whatsify")]
        [Aliases("copipasta", "zapironga")]
        [Description("Converte um texto com emoji do discord para emoji universais")]
        public async Task Whatsify(CommandContext ctx, [Description("Texto que deseja converter")][RemainingText]string mensagem)
        {
            if (string.IsNullOrEmpty(mensagem)) 
                throw new Exception();

            DiscordEmbedBuilder eb = new DiscordEmbedBuilder
            {
                Description = $"```{mensagem}```",
                Color = DiscordColor.Green
            };
            await ctx.RespondAsync(embed: eb);
        }

        [Command("say")]
        [Description("Cria uma mensagem no servidor")]
        [RequirePermissions(DSharpPlus.Permissions.ManageMessages & DSharpPlus.Permissions.Administrator)]
        [RequireUserPermissions(DSharpPlus.Permissions.ManageMessages & DSharpPlus.Permissions.Administrator)]
        public async Task Say(CommandContext ctx, [Description("Mensagem para converter na mensagem")][RemainingText]string mensagem)
        {
            if (ctx.Channel.IsPrivate && mensagem != "") 
                throw new Exception();

            await new StringVariablesExtension(ctx.Member, ctx.Guild).SendMessage(ctx.Message.Channel, mensagem);
            await ctx.Message.DeleteAsync();
        }
    }
}
