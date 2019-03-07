using Discord;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bot.Nucleo.Extensions
{
    public static class EmbedExtensions //static class???? for thissssssss??? ?? ? tu ta usando crack??
    {
        public static EmbedBuilder WithOkColor(this EmbedBuilder eb) //this vai tomar no cuck
        {
            return eb.WithColor(new Color(Constants.cor));
        }


        public static EmbedBuilder WithErrorColor(this EmbedBuilder eb) //this ai ja n vai dar pra passar
        {
            return eb.WithColor(new Color(Constants.red));
        }
    }
}
