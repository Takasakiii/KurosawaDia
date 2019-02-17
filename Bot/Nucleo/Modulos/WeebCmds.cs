using Bot.Nucleo.Extensions;
using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Weeb.net;

namespace Bot.Nucleo.Modulos
{
    public class WeebCmds
    {
        CommandContext context;

        public WeebCmds(CommandContext context)
        {
            this.context = context;
        }

        public async Task Hug(WeebClient weebClient)
        {
            var img = await weebClient.GetRandomAsync("hug", new string[] { }, FileType.Gif, false, NsfwSearch.False);

            EmbedBuilder builder = new EmbedBuilder()
                .WithImageUrl(img.Url)
                .WithOkColor();
            Embed embed = builder.Build();

            await context.Channel.SendMessageAsync("", embed: embed).ConfigureAwait(false);
        }

        public async Task Weeb(WeebClient weebClient, string[] comando)
        {
            try
            {
                if (comando[1] == "t" || comando[1] == "tipos")
                {
                    Weeb.net.Data.TypesData tipos = await weebClient.GetTypesAsync();
                    string[] tiposArr = tipos.Types.ToArray();
                    string txt = "";

                    for (int i = 0; i < tiposArr.Length; i++)
                    {
                        txt += $"`{tiposArr[i]}`, ";
                    }

                    EmbedBuilder builder = new EmbedBuilder()
                        .WithTitle($"{context.User} Esses são os tipos de gifs:")
                        .WithDescription(txt)
                        .WithFooter("Use 'weeb g <tipo>  |  para pegar o gif")
                        .WithOkColor();
                    Embed embed = builder.Build();
                    await context.Channel.SendMessageAsync("", embed: embed).ConfigureAwait(false);
                }
                else if (comando[1] == "g" || comando[1] == "get")
                {
                    try
                    {
                        var img = await weebClient.GetRandomAsync(comando[2], new string[] { }, FileType.Gif, false, NsfwSearch.False);

                        EmbedBuilder builder = new EmbedBuilder()
                            .WithTitle(img.BaseType)
                            .WithUrl(img.Url)
                            .WithImageUrl(img.Url)
                            .WithOkColor();
                        Embed embed = builder.Build();

                        await context.Channel.SendMessageAsync("", embed: embed).ConfigureAwait(false);
                    }
                    catch
                    {
                        EmbedBuilder builder = new EmbedBuilder()
                            .WithDescription($"**{context.User}** você não me disse o tipo de gif ou esse tipo não existe")
                            .WithErrorColor();
                        Embed embed = builder.Build();

                        await context.Channel.SendMessageAsync("", embed: embed).ConfigureAwait(false);
                    }
                }
            } catch {
                EmbedBuilder builder = new EmbedBuilder()
                    .WithTitle($"{context.User}")
                    .WithDescription("`'weeb t` para ver os tipos,\n`'weeb g <tipo>` para pegar o gif do tipo")
                    .WithErrorColor();
                Embed embed = builder.Build();

                await context.Channel.SendMessageAsync("", embed: embed).ConfigureAwait(false);
            }
        }
    }
}
