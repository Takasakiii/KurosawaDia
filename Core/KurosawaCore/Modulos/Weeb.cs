using DataBaseController.Abstractions;
using DataBaseController.DAOs;
using DataBaseController.Modelos;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using KurosawaCore.Extensions;
using KurosawaCore.Models.Atributes;
using System.Threading.Tasks;

namespace KurosawaCore.Modulos
{
    [Modulo("Weeb", "❤")]
    [Description("Este módulo é o mais amoroso de todos.")]
    public class Weeb
    {
        [Command("hug")]
        [Aliases("abraço", "abraco")]
        [Description("Abraça o seu amiguinho")]
        public async Task Hug(CommandContext ctx, [Description("Usuario que deseja abraçar")][RemainingText]DiscordUser usuario = null)
        {
            await ctx.RespondAsync(embed: await new WeebExtension
            {
                SelfMsg = "está se abraçando",
                UsuarioDestino = usuario,
                Author = ctx.User
            }.GetWeeb("hug", "está abraçando"));
        }

        [Command("kiss")]
        [Aliases("beijo")]
        [Description("Beija o amiguinho")]
        public async Task Kiss(CommandContext ctx, [Description("Usuario que deseja beijar")][RemainingText]DiscordUser usuario = null)
        {
            await ctx.RespondAsync(embed: await new WeebExtension
            {
                SelfMsg = "está beijando ele(a) mesmo",
                UsuarioDestino = usuario,
                Author = ctx.User
            }.GetWeeb("kiss", "está beijando"));
        }

        [Command("slap")]
        [Aliases("bater")]
        [Description("Bater no seu amiguinho")]
        public async Task Slap(CommandContext ctx, [Description("Usuario que deseja bater")][RemainingText]DiscordUser usuario = null)
        {
            await ctx.RespondAsync(embed: await new WeebExtension
            {
                SelfMsg = "está se batendo",
                UsuarioDestino = usuario,
                Author = ctx.User
            }.GetWeeb("slap", "está dando um tapa no(a)"));
        }

        [Command("punch")]
        [Aliases("socar")]
        [Description("Socar o amiguinho")]
        public async Task Punch(CommandContext ctx, [Description("Usuario que deseja socar")][RemainingText]DiscordUser usuario = null)
        {
            await ctx.RespondAsync(embed: await new WeebExtension
            {
                SelfMsg = "está socando ele(a) mesmo",
                UsuarioDestino = usuario,
                Author = ctx.User
            }.GetWeeb("punch", "está socando"));
        }

        [Command("lick")]
        [Aliases("lamber")]
        [Description("Lamber a gasosa")]
        public async Task Lick(CommandContext ctx, [Description("Usuario que deseja lamber")][RemainingText]DiscordUser usuario = null)
        {
            await ctx.RespondAsync(embed: await new WeebExtension
            {
                SelfMsg = "está lambendo ele(a) mesmo",
                UsuarioDestino = usuario,
                Author = ctx.User
            }.GetWeeb("lick", "está lambendo"));
        }

        [Command("cry")]
        [Aliases("chorar")]
        [Description("Chorar com o amiguinho")]
        public async Task Cry(CommandContext ctx, [Description("Usuario que deseja chorar junto")][RemainingText]DiscordUser usuario = null)
        {
            await ctx.RespondAsync(embed: await new WeebExtension
            {
                SelfMsg = "está chorando",
                UsuarioDestino = usuario,
                Author = ctx.User
            }.GetWeeb("cry", "está chorando com"));
        }

        [Command("pat")]
        [Aliases("acariciar")]
        [Description("Fazer carinho no amiguinho")]
        public async Task Pat(CommandContext ctx, [Description("Usuario que deseja acariciar")][RemainingText]DiscordUser usuario = null)
        {
            await ctx.RespondAsync(embed: await new WeebExtension
            {
                SelfMsg = "está se acariciando",
                UsuarioDestino = usuario,
                Author = ctx.User
            }.GetWeeb("pat", "está fazendo carinho no(a)"));
        }

        [Command("dance")]
        [Aliases("dancar")]
        [Description("Dançar com seu amiguinho")]
        public async Task Dance(CommandContext ctx, [Description("Usuario que deseja dançar junto")][RemainingText]DiscordUser usuario = null)
        {
            await ctx.RespondAsync(embed: await new WeebExtension
            {
                SelfMsg = "começou a dançar com a vasoura",
                UsuarioDestino = usuario,
                Author = ctx.User
            }.GetWeeb("dance", "começou a dançar com"));
        }

        [Command("megumin")]
        [Description("Mostra imagens da megumin")]
        public async Task Megumin(CommandContext ctx)
        {
            await ctx.RespondAsync(embed: await new WeebExtension
            {
                Auto = false
            }.GetWeeb("megumin", "Megumin ❤"));
        }

        [Command("rem")]
        [Description("Mostra imagens da Rem")]
        public async Task Rem(CommandContext ctx)
        {
            await ctx.RespondAsync(embed: await new WeebExtension
            {
                Auto = false
            }.GetWeeb("rem", "rem ❤"));
        }

        [Command("fuck")]
        [Description("( ͡° ͜ʖ ͡°)")]
        public async Task Fuck(CommandContext ctx, [Description("Colega")][RemainingText]DiscordUser usuario = null)
        {
            bool especial = false;
            if (ctx.Guild != null)
            {
                Servidores servidor = await new ServidoresDAO().Get(new Servidores
                {
                    ID = ctx.Guild.Id
                });
                especial = ((byte)servidor.Espercial >= (byte)TiposServidores.LolisEdition);
            }
            
            Fuck fuck = await new FuckDAO().Get(new Fuck
            {
                Explicit = especial
            });

            await ctx.RespondAsync(embed: new DiscordEmbedBuilder
            {
                Color = DiscordColor.HotPink,
                ImageUrl = fuck.Url,
                Title = (usuario == null) ? $"{ctx.User.Username} está se masturbando." : $"{ctx.User.Username} está fodendo {usuario.Username}"
            });
        }
    }
}
