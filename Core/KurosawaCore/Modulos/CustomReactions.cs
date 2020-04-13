using DataBaseController.DAOs;
using DataBaseController.Modelos;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using KurosawaCore.Models.Atributes;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model = DataBaseController.Modelos.CustomReactions;

namespace KurosawaCore.Modulos
{
    [Modulo("Reações Customizadas", "💬")]
    [Description("Este módulo possui comandos para você controlar as minhas Reações Customizadas")]
    public class CustomReactions
    {
        [Command("adicionarcr")]
        [Aliases("acr")]
        [Description("Adiciona uma Reação Customizada ao Servidor. (Observação: para usar o mesmo precisa ser administrador ou ter um cargo chamado Ajudante de Idol)")]
        public async Task AdicionarCR(CommandContext ctx, [Description("\"pergunta\" | \"resposta\" da custom reaction.")][RemainingText] string args)
        {
            await CAcr(ctx, args);
        }

        [Command("adicionarespecialcr")]
        [Aliases("aecr")]
        [Description("Adiciona uma Reação Customizada Especial ao Servidor. (Observação: para usar o mesmo precisa ser administrador ou ter um cargo chamado Ajudante de Idol)")]
        public async Task AdicionarECR(CommandContext ctx, [Description("\"pergunta\" | \"resposta\" da custom reaction.")][RemainingText] string args)
        {
            await CAcr(ctx, args, true);
        }

        [Command("listcr")]
        [Aliases("lcr")]
        [Description("Lista as Custom Reactions ou Pesquisa uma Custom Reaction Especifica")]
        public async Task ListCR(CommandContext ctx, [Description("Objeto de pesquisa ou pagina")][RemainingText] params string[] pesquisa)
        {
            if (pesquisa.Length == 0)
                pesquisa = new string[] { "", "1" };

            if (!uint.TryParse(pesquisa.Last(), out uint page))
                page = 1;
            else
                pesquisa = pesquisa[..^1];

            Model[] crs = await new CustomReactionsDAO().GetPage(new Model
            {
                Servidor = new Servidores
                {
                    ID = ctx.Guild.Id
                },
                Trigger = string.Join(" " , pesquisa)
            }, page);

            string description = $"**Página {page}**\n```md\n";

            if (crs?.Length != 0)
            {
                foreach (Model modelo in crs)
                {
                    string trigger = modelo.Trigger;
                    if (trigger.Length > 20) 
                        trigger = trigger.Substring(0, 17) + "...";
                    if (modelo.Modo)
                        trigger = $"*{trigger}*";
                    
                    description += string.Format("{0, -7}{1}\n", modelo.Cod.ToString() + '.', trigger);
                }

                await ctx.RespondAsync(embed: new DiscordEmbedBuilder() {
                    Color = DiscordColor.Green,
                    Description = description + "```",
                    Title = "Lista das Reações Customizadas"
                }.WithFooter("As Reações Customizadas marcadas *assim* são as especiais."));
            }
            else
            {
                await ctx.RespondAsync(embed: new DiscordEmbedBuilder() {
                    Color = DiscordColor.Green,
                    Title = "Não encontrei nenhuma Reação Customizada."
                });
            }
        }

        private async Task CAcr(CommandContext ctx, string args, bool modo = false)
        {
            if (string.IsNullOrEmpty(args) || ctx.Channel.IsPrivate || !args.Contains("|") || (!ctx.Member.IsOwner && ctx.Member.Roles.Where(x => x.Name == "Ajudante de Idol").Count() == 0 && ctx.Member.PermissionsIn(ctx.Channel) != Permissions.Administrator))
                throw new Exception();
            string[] split = args.Split("|");
            if (split.Length < 2)
                throw new Exception();
            await new CustomReactionsDAO().Adicionar(new Model
            {
                Modo = modo,
                Resposta = split[1].TrimStart(),
                Trigger = split[0].TrimEnd(),
                Servidor = new Servidores
                {
                    ID = ctx.Guild.Id
                }
            });
            await ctx.RespondAsync(embed: new DiscordEmbedBuilder
            {
                Title = "Custom Reaction adicionada com sucesso 😃",
                Color = DiscordColor.Orange
            });
        }
    }
}
