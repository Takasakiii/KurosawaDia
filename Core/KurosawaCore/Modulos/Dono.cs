using DataBaseController.Abstractions;
using DataBaseController.DAOs;
using DataBaseController.Modelos;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using KurosawaCore.Extensions;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KurosawaCore.Modulos
{
    [Group("Owner")]
    [Hidden]
    [Description("kkkk o cara eh owner q corno")]
    public class Dono
    {

        [Command("exec")]
        [Description("shiba gay")]
        public async Task Eval(CommandContext ctx, [Description("comandos de hacker")][RemainingText]string comando)
        {
            if (string.IsNullOrEmpty(comando) || await BotPermissions.CheckAdm(ctx.User) != TiposAdms.Dono)
                throw new Exception();
            string comandoformatado = comando.Replace("```", "");
            await ctx.TriggerTypingAsync();
            DiscordEmbedBuilder eb = new DiscordEmbedBuilder
            {
                Title = "Result:",
            };


            try
            {
                ScriptOptions escopo = ScriptOptions.Default;
                escopo = escopo.WithImports("System", "System.Collections.Generic", "System.Linq", "System.Text", "System.Threading.Tasks", "DSharpPlus", "DSharpPlus.CommandsNext");
                escopo = escopo.WithReferences(AppDomain.CurrentDomain.GetAssemblies().Where(xa => !xa.IsDynamic && !string.IsNullOrWhiteSpace(xa.Location)));

                Script<object> code = CSharpScript.Create(comandoformatado, escopo, typeof(CommandContext));
                code.Compile();
                ScriptState<object> resultado = await code.RunAsync(ctx);
                if (resultado != null && !string.IsNullOrWhiteSpace(resultado.ReturnValue.ToString()))
                    await ctx.RespondAsync(embed: eb.WithDescription($"```{resultado.ReturnValue}```").WithColor(DiscordColor.Cyan).Build());
            }
            catch (Exception e)
            {
                await ctx.RespondAsync(embed: eb.WithDescription($"```{e.Message}```").WithColor(DiscordColor.Red).Build());
            }
        }

        [Command("sudo")]
        [Description("shiba putinha")]
        public async Task Sudo(CommandContext ctx, [Description("Amiguxo")] DiscordUser user, [Description("Comandos de hacker")][RemainingText] string cmd)
        {
            if (ctx.Channel.IsPrivate || string.IsNullOrEmpty(cmd) || (byte)await BotPermissions.CheckAdm(ctx.User) < (byte)TiposAdms.Adm)
                throw new Exception();
            await ctx.CommandsNext.SudoAsync(user, ctx.Channel, cmd);
        }

        [Command("setespecial")]
        [Description("shiba furro")]
        public async Task SetEspecial(CommandContext ctx, [Description("O fdp")]DiscordGuild guild, [Description("\nInal = 0\nNor = 1\nLoli = 2\nPikachu = 3")]int tipo = 0)
        {
            if (ctx.Channel.IsPrivate || await BotPermissions.CheckAdm(ctx.User) != TiposAdms.Dono)
                throw new Exception();

            await new ServidoresDAO().Atualizar(new Servidores
            {
                ID = guild.Id,
                Espercial = (TiposServidores)tipo
            });
        }
    }
}
