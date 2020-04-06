using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using KurosawaCore.Models.Atributes;
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
            if (string.IsNullOrEmpty(comando))
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
                if (resultado != null)
                    await ctx.RespondAsync(embed: eb.WithDescription($"```{resultado.ReturnValue}```").WithColor(DiscordColor.Cyan).Build());
            }
            catch (Exception e)
            {
                await ctx.RespondAsync(embed: eb.WithDescription($"```{e.Message}```").WithColor(DiscordColor.Red).Build());
            }
        }


    }
}
