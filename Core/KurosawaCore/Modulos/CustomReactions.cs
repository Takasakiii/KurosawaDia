using DataBaseController.DAOs;
using DataBaseController.Modelos;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using KurosawaCore.Extensions;
using KurosawaCore.Models.Atributes;
using System;
using System.Linq;
using System.Threading.Tasks;
using Model = DataBaseController.Modelos.CustomReactions;

namespace KurosawaCore.Modulos
{
    [Modulo("Reações Customizadas", "💬")]
    [Description("Este módulo possui comandos para você controlar as minhas Reações Customizadas.")]
    public class CustomReactions
    {
        [Command("adicionarrc")]
        [Aliases("acr", "arc", "adicionarcr")]
        [Description("Adiciona uma Reação Customizada ao servidor.\n\n(Observação: para usar o mesmo, você precisa ser administrador ou ter um cargo chamado Ajudante de Idol.)")]
        public async Task AdicionarCR(CommandContext ctx, [Description("\"pergunta\" | \"resposta\" da Reação Customizada.")][RemainingText] string args)
        {
            await CAcr(ctx, args);
        }

        [Command("adicionarrcespecial")]
        [Aliases("aecr", "arce")]
        [Description("Adiciona uma Reação Customizada Especial ao servidor.\n\n(Observação: para usar o mesmo, você precisa ser administrador ou ter um cargo chamado Ajudante de Idol.)")]
        public async Task AdicionarECR(CommandContext ctx, [Description("\"pergunta\" | \"resposta\" da Reação Customizada.")][RemainingText] string args)
        {
            await CAcr(ctx, args, true);
        }

        [Command("listrc")]
        [Aliases("lcr", "listarrc", "listcr")]
        [Description("Lista as Reações Customizadas ou pesquisa uma Reação Customizada específica.")]
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
                Trigger = string.Join(" ", pesquisa)
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

                await ctx.RespondAsync(embed: new DiscordEmbedBuilder()
                {
                    Color = DiscordColor.Orange,
                    Description = description + "```",
                    Title = "Lista das Reações Customizadas"
                }.WithFooter("As Reações Customizadas marcadas *assim* são as especiais."));
            }
            else
            {
                await ctx.RespondAsync(embed: new DiscordEmbedBuilder()
                {
                    Color = DiscordColor.Orange,
                    Title = "Não encontrei nenhuma Reação Customizada."
                });
            }
        }

        private async Task CAcr(CommandContext ctx, string args, bool modo = false)
        {
            if (string.IsNullOrEmpty(args) || ctx.Channel.IsPrivate || !args.Contains("|") || (ctx.Member.Roles.Where(x => x.Name == "Ajudante de Idol").Count() == 0 && !ctx.HasPermissions(Permissions.ManageGuild)))
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
            Console.WriteLine($"Comando cr feito por {ctx.Message.Author.Username}#{ctx.Message.Author.Discriminator} contendo {split[1]}");
        }


        [Command("deletecr")]
        [Aliases("dcr")]
        [Description("Remove uma Reação Customizada especifica\n\n(Observação: você precisa da permissão de administrador para poder usar esse comando.)")]
        public async Task Dcr(CommandContext ctx, [Description("Codigo da Reação Customizada")] uint codigo)
        {
            if ((ctx.Member.Roles.Where(x => x.Name == "Ajudante de Idol").Count() != 0 || ctx.HasPermissions()) && await new CustomReactionsDAO().Delete(new Model
            {
                Cod = codigo,
                Servidor = new Servidores
                {
                    ID = ctx.Guild.Id
                }
            }) != 0)
                await ctx.RespondAsync(embed: new DiscordEmbedBuilder
                {
                    Title = "Reação foi removida com sucesso 😃",
                    Color = DiscordColor.Orange
                });
            else
                throw new Exception();
        }
    }
}
