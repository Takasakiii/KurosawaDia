using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Extensions
{
    internal class ErrorExtension
    {
        internal struct DadosErro
        {
            public string Arg { private set; get; }
            public string Exemplo { private set; get; }

            public DadosErro(string arg, string exemplo)
            {
                Arg = arg;
                Exemplo = exemplo;
            }
        }

        private CommandContext Contexto;
        private string Comando;
        private string Autor;
        private EmbedBuilder Builder;
        private string Prefix;


        internal ErrorExtension(CommandContext contexto, string comando, string prefix)
        {
            Contexto = contexto;
            Comando = comando;
            Autor = contexto.User.ToString();
            Prefix = prefix;
            Builder = new EmbedBuilder();
        }

        internal async Task EnviarErroAsync(string fraseErro, params DadosErro[] dados)
        {
            Builder.WithColor(Color.Red);
            Builder.WithTitle($"**{Autor}**, {fraseErro}");
            string args = string.Empty;
            string exemplo = string.Empty;
            foreach(DadosErro dado in dados)
            {
                args += $"{Prefix}{Comando} {dado.Arg}\n";
                exemplo += $"{Prefix}{Comando} {dado.Exemplo}\n";
            }

            Builder.AddField((dados.Length > 1) ? await StringCatch.GetStringAsync("baseErroArgsM1", "Usos do comando:") : await StringCatch.GetStringAsync("baseErroArgs1", "Uso do comando:"), args);
            Builder.AddField((dados.Length > 1) ? await StringCatch.GetStringAsync("baseErroExM1", "Exemplos:") : await StringCatch.GetStringAsync("baseErroEx1", "Exemplo:"), exemplo);

            await Contexto.Channel.SendMessageAsync(embed: Builder.Build());
        }

        
    }
}
