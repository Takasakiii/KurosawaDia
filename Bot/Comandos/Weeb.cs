using Bot.Extensions;
using Discord.Commands;

namespace Bot.Comandos
{
    public class Weeb
    {
        public void hug(CommandContext context, object[] args)
        {
            new WeebExtensions().WeebCmd("hug", "abraçando o(a)", context, args).GetAwaiter().GetResult();
        }

        public void kiss(CommandContext context, object[] args)
        {
            new WeebExtensions().WeebCmd("kiss", "beijando o(a)", context, args).GetAwaiter().GetResult();
        }

        public void slap(CommandContext context, object[] args)
        {
            new WeebExtensions().WeebCmd("slap", "dando um tapa no(a)", context, args).GetAwaiter().GetResult();
        }

        public void punch(CommandContext context, object[] args)
        {
            new WeebExtensions().WeebCmd("punch", "dando um soco no(a)", context, args).GetAwaiter().GetResult();
        }

        public void lick(CommandContext context, object[] args)
        {
            new WeebExtensions().WeebCmd("lick", "lambendo o(a)", context, args).GetAwaiter().GetResult();
        }
    }
}
