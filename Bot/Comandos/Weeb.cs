using Bot.Extensions;
using Discord.Commands;

namespace Bot.Comandos
{
    public class Weeb : Moderacao
    {
        WeebExtensions weeb = new WeebExtensions();

        public void hug(CommandContext context, object[] args)
        {
            weeb.WeebCmd(true, "hug", "abraçando o(a)", context, args).GetAwaiter().GetResult();
        }

        public void kiss(CommandContext context, object[] args)
        {
            weeb.WeebCmd(true, "kiss", "beijando o(a)", context, args).GetAwaiter().GetResult();
        }

        public void slap(CommandContext context, object[] args)
        {
            weeb.WeebCmd(true, "slap", "dando um tapa no(a)", context, args).GetAwaiter().GetResult();
        }

        public void punch(CommandContext context, object[] args)
        {
            weeb.WeebCmd(true, "punch", "dando um soco no(a)", context, args).GetAwaiter().GetResult();
        }

        public void lick(CommandContext context, object[] args)
        {
            weeb.WeebCmd(true, "lick", "lambendo o(a)", context, args).GetAwaiter().GetResult();
        }

        public void cry(CommandContext context, object[] args)
        {
            weeb.WeebCmd(false, "cry", $"{context.User.Username} esta chorando", context, args).GetAwaiter().GetResult();
        }

        public void megumin(CommandContext context, object[] args)
        {
            weeb.WeebCmd(false, "megumin", "Megumin ❤", context, args).GetAwaiter().GetResult();
        }

        public void rem(CommandContext context, object[] args)
        {
            weeb.WeebCmd(false, "rem", "Rem ❤", context, args).GetAwaiter().GetResult();
        }

        public void pat(CommandContext context, object[] args)
        {
            weeb.WeebCmd(true, "pat", "fazendo carinho no(a)", context, args).GetAwaiter().GetResult();

        }

        //decepção = decepção + 3000%;
    }
}
