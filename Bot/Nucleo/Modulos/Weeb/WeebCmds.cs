using Bot.Modelos;
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
        AyuraConfig config;
        WeebClient weebClient = new WeebGen().weebClient;

        public weebCmds(CommandContext context, AyuraConfig config)
        {
            this.context = context;
            this.config = config;
        }

        public async Task Hug()
        {
            RandomData img = await weebClient.GetRandomAsync("hug", new string[] { }, FileType.Gif, false, NsfwSearch.False);

            EmbedBuilder builder = new EmbedBuilder()
                .WithImageUrl(img.Url);
            Embed embed = builder.Build();

            await context.Channel.SendMessageAsync("", embed: embed).ConfigureAwait(false);

            //geneneralizar
        }

        public async Task Weeb(string[] comando)
        {
            if(context.User.Id == Convert.ToUInt64(config.ownerId))
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
                        await context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                .WithColor(Color.DarkPurple)
                                .WithTitle($"{context.User} Esses são os tipos de gifs:")
                                .WithDescription(txt)
                                .WithFooter("Use 'weeb g <tipo>  |  para pegar o gif")
                            .Build());
                    }
                    else if (comando[1] == "g" || comando[1] == "get")
                    {
                        try
                        {
                            RandomData img = await weebClient.GetRandomAsync(comando[2], new string[] { }, FileType.Gif, false, NsfwSearch.False);

                            await context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                    .WithColor(Color.DarkPurple)
                                    .WithTitle(img.BaseType)
                                    .WithUrl(img.Url)
                                    .WithImageUrl(img.Url)
                                .Build());
                        }
                        catch
                        {
                            await context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                    .WithColor(Color.Red)
                                    .WithDescription($"**{context.User}** você não me disse o tipo de gif ou esse tipo não existe")
                                .Build());
                        }
                    }

                    //execessao default cairia bem
                }
                catch
                {
                    await context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithColor(Color.DarkPurple)
                            .WithTitle($"{context.User}")
                            .WithDescription("`'weeb t` para ver os tipos,\n`'weeb g <tipo>` para pegar o gif do tipo")
                        .Build());
                }
            } else
            {
                await context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithColor(Color.Red)
                        .WithDescription($"**{context.User}** você não pode usar esse comando")
                    .Build());
            }
        }
    }
}
