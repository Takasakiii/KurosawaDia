using Bot.Extensions;
using Bot.GenericTypes;
using ConfigurationControler.DAO;
using ConfigurationControler.Modelos;
using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using Weeb.net;
using Weeb.net.Data;
using TokenType = Weeb.net.TokenType;

namespace Bot.Comandos
{
    public class Weeb : GenericModule
    {
        public Weeb(CommandContext contexto, object[] args) : base(contexto, args)
        {

        }

        private void weeb(string tipo, string msg, bool auto = true)
        {
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithColor(Color.DarkPurple);

            Tuple<bool, List<ApisConfig>> apiConfig = new ApisConfigDAO().Carregar();

            WeebClient weebClient = new WeebClient();
            weebClient.Authenticate(apiConfig.Item2[0].Token, TokenType.Wolke).GetAwaiter().GetResult();
            RandomData img = weebClient.GetRandomAsync(tipo, new string[] { }, FileType.Any, false, NsfwSearch.False).GetAwaiter().GetResult();

            
        }

        public void megumin()
        {
            weeb("megumin", StringCatch.GetString("meguminTxt", "Megumin ❤"), false);
        }

    }
}
