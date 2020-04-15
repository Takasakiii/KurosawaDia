using DataBaseController.Abstractions;
using DataBaseController.DAOs;
using DataBaseController.Modelos;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using KurosawaCore.Extensions;
using KurosawaCore.Models.Atributes;
using System;
using System.Text;
using System.Threading.Tasks;

namespace KurosawaCore.Modulos
{
    [Modulo("Weeb", "❤")]
    [Description("Este módulo é o mais amoroso de todos.")]
    public class Weeb
    {
        [Command("hug")]
        [Aliases("abraço", "abraco")]
        [Description("Dê um abraço em si mesmo(a) ou em seu amiguinho.")]
        public async Task Hug(CommandContext ctx, [Description("Usuário que você deseja abraçar.")][RemainingText]DiscordUser usuario = null)
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
        [Description("Dê um beijo em si mesmo(a) ou em seu amiguinho.")]
        public async Task Kiss(CommandContext ctx, [Description("Usuário que você deseja beijar.")][RemainingText]DiscordUser usuario = null)
        {
            await ctx.RespondAsync(embed: await new WeebExtension
            {
                SelfMsg = "está se beijando",
                UsuarioDestino = usuario,
                Author = ctx.User
            }.GetWeeb("kiss", "está beijando"));
        }

        [Command("slap")]
        [Aliases("bater")]
        [Description("Dê um tapa em si mesmo(a) ou em seu amiguinho.")]
        public async Task Slap(CommandContext ctx, [Description("Usuário que você deseja bater.")][RemainingText]DiscordUser usuario = null)
        {
            await ctx.RespondAsync(embed: await new WeebExtension
            {
                SelfMsg = "está se batendo",
                UsuarioDestino = usuario,
                Author = ctx.User
            }.GetWeeb("slap", "está dando um tapa em"));
        }

        [Command("punch")]
        [Aliases("socar")]
        [Description("Dê um soco em si mesmo ou em seu amiguinho.")]
        public async Task Punch(CommandContext ctx, [Description("Usuário que você deseja socar.")][RemainingText]DiscordUser usuario = null)
        {
            await ctx.RespondAsync(embed: await new WeebExtension
            {
                SelfMsg = "está se socando",
                UsuarioDestino = usuario,
                Author = ctx.User
            }.GetWeeb("punch", "está socando"));
        }

        [Command("lick")]
        [Aliases("lamber")]
        [Description("Dê uma lambida em si mesmo ou em seu amiguinho!")]
        public async Task Lick(CommandContext ctx, [Description("Usuário que você deseja lamber.")][RemainingText]DiscordUser usuario = null)
        {
            await ctx.RespondAsync(embed: await new WeebExtension
            {
                SelfMsg = "está se lambendo",
                UsuarioDestino = usuario,
                Author = ctx.User
            }.GetWeeb("lick", "está lambendo"));
        }

        [Command("cry")]
        [Aliases("chorar")]
        [Description("Chore sozinho(a) ou junto com alguém!")]
        public async Task Cry(CommandContext ctx, [Description("Usuário que você deseja chorar junto com ele.")][RemainingText]DiscordUser usuario = null)
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
        [Description("Faça carinho em seu amiguinho (a não ser que você esteja carente).")]
        public async Task Pat(CommandContext ctx, [Description("Usuário que você deseja acariciar.")][RemainingText]DiscordUser usuario = null)
        {
            await ctx.RespondAsync(embed: await new WeebExtension
            {
                SelfMsg = "está carente",
                UsuarioDestino = usuario,
                Author = ctx.User
            }.GetWeeb("pat", "está fazendo carinho em"));
        }

        [Command("dance")]
        [Aliases("dancar")]
        [Description("Dance sozinho(a) ou com o seu amiguinho.")]
        public async Task Dance(CommandContext ctx, [Description("Usuário que você deseja dançar junto com ele.")][RemainingText]DiscordUser usuario = null)
        {
            await ctx.RespondAsync(embed: await new WeebExtension
            {
                SelfMsg = "começou a dançar com a vassoura",
                UsuarioDestino = usuario,
                Author = ctx.User
            }.GetWeeb("dance", "começou a dançar com"));
        }

        [Command("megumin")]
        [Description("Mostra uma imagem da Megumin.")]
        public async Task Megumin(CommandContext ctx)
        {
            await ctx.RespondAsync(embed: await new WeebExtension
            {
                Auto = false
            }.GetWeeb("megumin", "Megumin ❤"));
        }

        [Command("rem")]
        [Description("Mostra uma imagem da Rem.")]
        public async Task Rem(CommandContext ctx)
        {
            await ctx.RespondAsync(embed: await new WeebExtension
            {
                Auto = false
            }.GetWeeb("rem", "Rem ❤"));
        }

        [Command("fuck")]
        [Aliases("foder")]
        [Description("( ͡° ͜ʖ ͡°)")]
        public async Task Fuck(CommandContext ctx, [Description("Usuário que você quer foder.")][RemainingText]DiscordUser usuario = null)
        {
            bool especial = false;
            if (ctx.Guild != null)
            {
                Servidores servidor = await new ServidoresDAO().Get(new Servidores
                {
                    ID = ctx.Guild.Id
                });
                especial = ((byte)servidor.Especial >= (byte)TiposServidores.LolisEdition);
            }
            
            Fuck fuck = await new FuckDAO().Get(new Fuck
            {
                Explicit = especial
            });

            await ctx.RespondAsync(embed: new DiscordEmbedBuilder
            {
                Color = DiscordColor.HotPink,
                ImageUrl = fuck.Url,
                Title = (usuario == null) ? $"{ctx.User.Username} está se masturbando." : $"{ctx.User.Username} está fodendo {usuario.Username}."
            });
        }

        [Command("owoify")]
        [Aliases("furroify", "furrofy", "furrar")]
        [Description("Transforma uma frase em câncer.")]
        public async Task Owoify(CommandContext ctx, [Description("Texto para arruinar (não pode ser maior que 800 caracteres).")][RemainingText]string texto)
        {
            if (texto.Length > 800)
                throw new Exception();

            string owoifiedText = string.Empty;

            string[] faces = { "OwO", "owo", "oωo", "òωó", "°ω°", "UwU", ">w<", "^w^" };

            Random rand = new Random();
            for (int i = 0; i < texto.Length; i++)
            {
                char ch = texto[i];

                if (ch == 'r' || ch == 'l')
                    owoifiedText += 'w';
                else if (texto.Length - i != 1 && (ch == 'n' || ch == 'N'))
                {
                    char nextNormalizated = texto[i + 1].ToString().Normalize(NormalizationForm.FormD)[0];
                    char nextNormalizatedLower = nextNormalizated.ToString().ToLowerInvariant()[0];
                    switch (nextNormalizatedLower)
                    {
                        case 'a':
                        case 'e':
                        case 'i':
                        case 'o':
                        case 'u':
                            if (nextNormalizatedLower == nextNormalizated)
                                owoifiedText += $"{ch}y";
                            else
                                owoifiedText += $"{ch}Y";
                            break;
                        default:
                            owoifiedText += ch;
                            break;
                    }
                }
                else if (ch == 'R' || ch == 'R')
                    owoifiedText += 'W';
                else if (ch == '!')
                {
                    if (i != 0 && texto[i - 1] == '@')
                        owoifiedText += ch;
                    else if (!(texto.Length - i != 1 && texto[i + 1] == '!'))
                        owoifiedText += $" {faces[rand.Next(0, faces.Length)]} ";
                }
                else if (texto.Length - i > 2)
                    if (ch == 'o' && texto[i + 1] == 'v' && texto[i + 2] == 'e')
                    {
                        owoifiedText += "uv";
                        i += 2;
                    }
                    else if (ch == 'O' && texto[i + 1] == 'V' && texto[i + 2] == 'E')
                    {
                        owoifiedText += "UV";
                        i += 2;
                    }
                    else
                        owoifiedText += ch;
                else
                    owoifiedText += ch;
            }

            await ctx.RespondAsync(embed: new DiscordEmbedBuilder
            {
                Color = DiscordColor.HotPink,
                Description = owoifiedText
            });
        }
    }
}
