using Discord;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bot.Nucleo.Extensions
{
    public static class EmbedExtensions
    {
        public static EmbedBuilder WithOkColor(this EmbedBuilder eb)
        {
            return eb.WithColor(new Color(Constants.cor));
        }


        public static EmbedBuilder WithErrorColor(this EmbedBuilder eb)
        {
            return eb.WithColor(new Color(Constants.red));
        }
    }
}
