using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.CommandsNext.Entities;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;

namespace KurosawaCore.Configuracoes
{
    internal sealed class HelpConfig : IHelpFormatter
    {
        private readonly Discord​Embed​Builder EmbedBuilder;

        public HelpConfig()
        {
            EmbedBuilder = new DiscordEmbedBuilder
            {
                Title = "Eu ouvi alguem pedindo ajuda?? Não se preocupe Dia a seu dispor :yum:",
                Color = DiscordColor.Purple,
                Description = "Para mais informações sobre um modulo ou comando digite: `help {nomeModulo/Comando}`, que eu informarei mais sobre ele :smiley:",
                ImageUrl = "https://i.imgur.com/mQVFSrP.gif"
            };
        }

        public CommandHelpMessage Build()
        {
            return new CommandHelpMessage(embed: EmbedBuilder.Build());
        }

        public IHelpFormatter WithAliases(IEnumerable<string> aliases)
        {
            string temp = "";
            foreach (string aliase in aliases)
            {
                temp += $" - {aliase}\n";
            }
            EmbedBuilder.AddField("Outros jeitos que eu entendo:", temp);
            EmbedBuilder.WithImageUrl("https://i.imgur.com/AUpMkBP.jpg");
            return this;
        }

        public IHelpFormatter WithArguments(IEnumerable<CommandArgument> arguments)
        {
            string temp = "";
            foreach (CommandArgument argumento in arguments)
            {
                string tipo = "";
                if (argumento.IsOptional)
                    tipo = $" = {{ {argumento.DefaultValue} }}";
                temp += $"`[{argumento.Name}]: {argumento.Type.Name}{tipo}` - {argumento.Description}\n";
            }
            EmbedBuilder.AddField("Metodos de chamada:", temp);
            EmbedBuilder.WithFooter("[] Nome do Argumento e {} valor padrão do mesmo");
            EmbedBuilder.WithImageUrl("https://i.imgur.com/vg0z9yT.jpg");
            return this;
        }

        public IHelpFormatter WithCommandName(string name)
        {
            EmbedBuilder.WithTitle($"Mais informações para {char.ToUpper(name[0])}{name.Substring(1)}:");
            EmbedBuilder.WithImageUrl("https://i.imgur.com/TK7zmb8.jpg");
            return this;
        }

        public IHelpFormatter WithDescription(string description)
        {
            EmbedBuilder.WithDescription(description);
            EmbedBuilder.WithImageUrl("https://i.imgur.com/hiu0Vh0.jpg");
            return this;
        }

        public IHelpFormatter WithGroupExecutable()
        {
            throw new NotImplementedException();
        }

        public IHelpFormatter WithSubcommands(IEnumerable<Command> subcommands)
        {
            string comandos = "";
            foreach (Command temp in subcommands)
            {
                comandos += $" - {temp.Name}\n";
            }
            EmbedBuilder.AddField("Comandos:", comandos);
            EmbedBuilder.WithImageUrl("https://i.imgur.com/cQqTUl1.png");
            return this;
        }
    }
}
