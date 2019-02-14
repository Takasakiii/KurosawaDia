using Discord;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bot.Nucleo.Extensions
{
    public static class Extensions
    {
        public static EmbedBuilder WithOkColor(this EmbedBuilder eb) =>
            eb.WithColor(new Color(Constants.cor));


        public static EmbedBuilder WithErrorColor(this EmbedBuilder eb) =>
            eb.WithColor(new Color(Constants.red));
    
     
    }
}
