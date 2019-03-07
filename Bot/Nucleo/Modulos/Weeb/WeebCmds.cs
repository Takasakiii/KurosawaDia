using Bot.Nucleo.Extensions;
using Bot.Nucleo.Modulos.WeebCmds;
using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Weeb.net;
using Weeb.net.Data;

namespace Bot.Nucleo.Modulos
{
    public class weebCmds
    {
        CommandContext context;
        WeebClient weebClient = new WeebGen().weebClient;

        public weebCmds(CommandContext context)
        {
            this.context = context;
        }

        public async Task Hug()
        {
            RandomData img = await weebClient.GetRandomAsync("hug", new string[] { }, FileType.Gif, false, NsfwSearch.False);

            EmbedBuilder builder = new EmbedBuilder()
                .WithImageUrl(img.Url)
                .WithOkColor();
            Embed embed = builder.Build();

            await context.Channel.SendMessageAsync("", embed: embed).ConfigureAwait(false);
        }

        public async Task Weeb(string[] comando)
        {
            try
            {
                if (comando[1] == "t" || comando[1] == "tipos")
                {
                    TypesData tipos = await weebClient.GetTypesAsync();
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
                        RandomData img = await weebClient.GetRandomAsync(comando[2], new string[] { }, FileType.Gif, false, NsfwSearch.False);

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
                        await context.SendErrorAsync($"você não me disse o tipo de gif ou esse tipo não existe");
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
//ok
