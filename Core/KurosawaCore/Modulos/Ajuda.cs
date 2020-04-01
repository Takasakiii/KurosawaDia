using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using KurosawaCore.Constants;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DSharpPlus.Entities.DiscordEmbedBuilder;

namespace KurosawaCore.Modulos
{
    public class Ajuda
    {
        [Command("ajuda")]
        [Aliases("help")]
        [Description("Com esse comando eu posso te fornecer informações como se comunicar comigo e as tarefas que realiso.")]
        public async Task AjudaCmd(CommandContext ctx, [Description("Comando que você precisa de ajuda")]params string[] comando)
        {
            await ctx.Client.GetCommandsNext().DefaultHelpAsync(ctx, comando);
        }

        [Command("sobre")]
        [Aliases("apresentacao", "apresentação")]
        [Description("Digamos que tudo que precisar saber de mim você pode ver aqui :heart:")]
        public async Task SobreMim(CommandContext ctx)
        {
            DiscordEmbedBuilder eb = new DiscordEmbedBuilder
            {
                Title = "Será um enorme prazer te ajudar :yum:",
                Description = "Eu me chamo Kurosawa Dia, sou presidente do conselho de classe, idol e também ajudo as pessoas com algumas coisinhas no Discord :wink:\nSe você usar `ajuda` no chat vai aparecer tudo que eu posso fazer atualmente(isso não é demais: grin:)\nSério estou muito ansiosa para passar um tempo com você e também te ajudar XD\nSe você tem ideias de mais coisas que eu possa fazer por favor mande uma sugestão com o `sugestao`\nSe você quer saber mais sobre mim, me convidar para seu servidor, ou até entrar em meu servidor de suporte use o comando `info`\n\nE como a Mari fala Let's Go!!",
                ImageUrl = "https://i.imgur.com/PC5QDiX.png",
                Footer = new EmbedFooter
                {
                    IconUrl = "http://i.imgur.com/Cm8grM4.png",
                    Text = "Kurosawa Dia é um projeto feito com amor e carinho pelos seus desenvolvedores!"
                },
                Color = DiscordColor.Purple
            };
            await ctx.RespondAsync(embed: eb.Build());
        }

        [Command("info")]
        [Aliases("convite", "ping")]
        [Description("Contem informações de suporte e algumas coisinhas pessoais")]
        public async Task Info(CommandContext ctx)
        {
            DiscordEmbedBuilder builder = new DiscordEmbedBuilder
            {
                ThumbnailUrl = "https://i.imgur.com/L8PxTrT.jpg",
                Title = "**Dia's Book:**",
                Description = "Espero que não faça nada estranho com minhas informações, to zuando kkkkkk :stuck_out_tongue_closed_eyes:",
                ImageUrl = "https://i.imgur.com/qGb6xtG.jpg",
                Color = DiscordColor.Purple
            };
            ulong users = 0;
            foreach (KeyValuePair<ulong, DiscordGuild> guild in ctx.Client.Guilds)
            {
                users += (ulong)guild.Value.MemberCount;
            }
            builder.AddField("Sobre mim:", "__Nome__: Kurosawa Dia (Dia - Chan)\n__Aniversário__: 1° de Janeiro(Quero presentes)\n__Ocupação__: Estudante e Traficante / Idol nas horas vagas");
            builder.AddField("As pessoas que fazem tudo isso ser possivel:", "Takasaki#7072\nYummi#2708\nLuckShiba#0001\n\nE é claro você que acredita em meu potencial:orange_heart:");
            builder.AddField("Informações chatas:", $"[Me adicione em seu Servidor]({ InfoImportante.conviteDia})\n[Entre no meu servidor para dar suporte ao projeto]({ InfoImportante.conviteServer})\n[Vote em mim no DiscordBotList para que eu possa ajudar mais pessoas]({ InfoImportante.topgg})");
            builder.AddField("Informações Chatas:", $"__Ping__: {ctx.Client.Ping}\n__Servidores__: {ctx.Client.Guilds.Count}\n__Usuarios__: {users}\n__Versão__: {InfoImportante.VersaoNumb} ({InfoImportante.VersaoName})");
            await ctx.RespondAsync(embed: builder.Build());
        }
    }
}
