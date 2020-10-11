using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.Entities;
using KurosawaCore.Extensions;
using KurosawaCore.Extensions.MessagesExtensions;
using KurosawaCore.Models.Atributes;
using NCalc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KurosawaCore.Modulos
{
    [Modulo("Utilidade", "🛠")]
    [Description("Este módulo possui coisas úteis para o seu dia a dia.")]
    public class Utilidades
    {
        [Command("videochamada")]
        [Aliases("webcam", "chamadadevideo")]
        [Description("Permite abrir uma chamada com vídeo de forma alternativa no servidor.")]
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
        [Description("Aumenta o tamanho de um emoji e também permite você pegar a url do mesmo.")]
        public async Task Emoji(CommandContext ctx, [Description("Emoji que você deseja visualizar.")][RemainingText]string emoji)
        {
            if (ctx.Channel.IsPrivate || emoji == null)
                throw new Exception();
            DiscordEmojiExtension ex = new DiscordEmojiExtension(emoji);
            string url = ex.GetUrl();
            DiscordEmbedBuilder eb = new DiscordEmbedBuilder
            {
                Color = DiscordColor.Green,
                Title = ex.Nome,
                Description = $"[Link direto]({url})",
                ImageUrl = url
            };
            await ctx.RespondAsync(embed: eb.Build());
        }

        [Command("avatar")]
        [Aliases("uimg")]
        [Description("Mostra o avatar de um usuário.")]
        public async Task Avatar(CommandContext ctx, [Description("Usuário da pessoa que você deseja pegar o avatar.")][RemainingText]DiscordUser alvo = null)
        {
            string[] frases;
            if (alvo == null)
                alvo = ctx.User;
            if (alvo == ctx.Client.CurrentUser)
                frases = ArrayExtension.CriarArray("Ownt, que amor, você realmente quer me ver 😍", "Assim você me deixa sem jeito 😊");
            else
                frases = ArrayExtension.CriarArray("Nossa, que avatar bonito! Agora sei porque você queria vê-lo 🤣", "Vocês são realmente criativos para avatares 😂", "Com um avatar assim seria um desperdício não se tornar uma idol 😃", "Talvez se você colocasse um filtro ficaria melhor... 🤐");
            int rnd = new Random().Next(0, frases.Length);
            DiscordEmbedBuilder eb = new DiscordEmbedBuilder
            {
                Title = frases[rnd],
                Description = $"{alvo.Username}#{alvo.Discriminator}\n[Link direto]({alvo.AvatarUrl})",
                ImageUrl = alvo.AvatarUrl,
                Color = DiscordColor.Green
            };
            await ctx.RespondAsync(embed: eb.Build());
        }

        [Command("serverimage")]
        [Aliases("simg")]
        [Description("Mostra o ícone do servidor.")]
        public async Task ServerImage(CommandContext ctx)
        {
            if (ctx.Channel.IsPrivate || ctx.Guild.IconUrl == null)
                throw new Exception();

            string url = await new ServerIconExtension().Get(ctx.Guild);

            DiscordEmbedBuilder eb = new DiscordEmbedBuilder
            {
                Title = ctx.Guild.Name,
                Description = $"[Link direto]({url})",
                ImageUrl = url,
                Color = DiscordColor.Green
            };
            await ctx.RespondAsync(embed: eb);
        }

        [Command("sugestao")]
        [Description("Nos envie uma sugestão.")]
        public async Task Sugestao(CommandContext ctx, [Description("A sua sugestão.")][RemainingText]string mensagem)
        {
            await ControladorDeSugestao(ctx, mensagem, "Sugestão");
        }

        [Command("bug")]
        [Description("Nos reporte um bug.")]
        public async Task Bug(CommandContext ctx, [Description("O bug para ser reportado.")][RemainingText]string mensagem)
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
                Description = $"**{ctx.User.Username}#{ctx.User.Discriminator}**, obrigada! Vou usá-l{((tipo == "Bug") ? 'o' : 'a')} para melhorarmos ❤",
                Color = DiscordColor.Green
            });
        }

        [Command("whatsify")]
        [Aliases("copipasta", "zapironga")]
        [Description("Converte um texto com emoji do Discord para um texto com emojis universais.")]
        public async Task Whatsify(CommandContext ctx, [Description("O texto que deseja converter.")][RemainingText]string mensagem)
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
        [Description("Faz eu falar algo à sua vontade.\n\n(Observação: você precisa da permissão de gerenciar mensagens para poder usar esse comando.")]
        public async Task Say(CommandContext ctx, [Description("Mensagem para eu falar.")][RemainingText]string mensagem)
        {
            if (ctx.Message.Attachments?.Count > 0 && ctx.Message.Attachments[0].FileName.EndsWith(".txt"))
            {
                using HttpClient client = new HttpClient();
                byte[] file = await client.GetByteArrayAsync(ctx.Message.Attachments[0].Url);
                mensagem = Encoding.UTF8.GetString(file);
            }

            if (ctx.Channel.IsPrivate || mensagem == "" || !ctx.HasPermissions(Permissions.ManageMessages))
                throw new Exception();

            await new StringVariablesExtension(ctx.Member, ctx.Guild).SendMessage(ctx.Message.Channel, mensagem);
            await ctx.Message.DeleteAsync();
        }

        [Command("editsay")]
        [Aliases("esay")]
        [Description("Faz eu editar algo que eu já disse.\n\n(Observação: você precisa da permissão de gerenciar mensagens para poder usar esse comando.\nA mensagem a editar precisa ser uma mensagem feita por mim")]
        public async Task EditSay(CommandContext ctx, [Description("Mensagem a ser editada")] string message, [Description("Mensagem para eu falar.")][RemainingText] string texto)
        {
            if (ctx.Message.Attachments?.Count > 0 && ctx.Message.Attachments[0].FileName.EndsWith(".txt"))
            {
                using HttpClient client = new HttpClient();
                byte[] file = await client.GetByteArrayAsync(ctx.Message.Attachments[0].Url);
                texto = Encoding.UTF8.GetString(file);
            }

            MessageExtension msgex = new MessageExtension(ctx, message);
            if (!msgex.Success || msgex.Message.Author.Id != ctx.Client.CurrentUser.Id || msgex.Message.Channel.GuildId != ctx.Guild.Id || ctx.Channel.IsPrivate
                || texto == "" || !ctx.HasPermissions(Permissions.ManageMessages))
                throw new Exception();

            await new StringVariablesExtension(ctx.Member, ctx.Guild).ModifyMessage(msgex.Message, texto);
            await ctx.Message.DeleteAsync();

        }

        [Command("mentionrandom")]
        [Aliases("someone", "mencionaraleatorio")]
        [Description("Menciona alguém escolhido aleatóriamente do seu servidor (like @someone).\n\n(Observação: você precisa de permissão pra enviar um everyone ou here no canal para poder usar este comando).")]
        public async Task Someone(CommandContext ctx, [Description("Cargo que o usuário escolhido deva ter (caso não informado, o bot escolherá um aleatório do servidor).")][RemainingText]DiscordRole cargo = null)
        {
            if (ctx.Channel.IsPrivate || !ctx.HasPermissions(Permissions.MentionEveryone))
                throw new Exception("Comando executado no privado.");
            List<DiscordMember> membros;
            if (cargo != null)
            {
                membros = ctx.Guild.Members.Where(x => x.Roles.Contains(cargo)).ToList();
            }
            else
            {
                membros = ctx.Guild.Members.ToList();
            }
            string[] frases = { "eu te invoco!", "acorde!!!!", "vamos, o show vai começar!", "seu amigo está te chamando." };
            Random rnd = new Random();
            await ctx.RespondAsync($"🎲{membros[rnd.Next(0, membros.Count)].Mention}, {frases[rnd.Next(0, frases.Length)]}🎲");
        }

        [Command("calc")]
        [Aliases("math", "calcular")]
        [Description("Te fornece o resultado da conta solicitada")]
        public async Task Calc (CommandContext ctx, [Description("Conta matemática")][RemainingText] string conta)
        {
            if (string.IsNullOrEmpty(conta))
                throw new Exception("conta é nulo");
            await ctx.RespondAsync(embed: new DiscordEmbedBuilder { 
                Color = DiscordColor.Green,
                Description = $"Resultado: {new Expression(conta).Evaluate()}"
            });
        }
    }
}
