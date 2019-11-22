using Bot.Modelos;
using Discord;
using System;
using System.Threading.Tasks;

namespace Bot.Extensions
{
    public class ImageExtensions
    {
        public async Task GetImgAsync(ImgModel model, params Tuple<string, string>[] links)
        {
            if (model.Nsfw && !(model.Canal as ITextChannel).IsNsfw)
            {
                await model.Canal.SendMessageAsync(embed: new EmbedBuilder()
                        .WithTitle(model.Texto)
                        .WithColor(Color.Red)
                        .WithDescription(await StringCatch.GetStringAsync("imgNsfw", "**{0}**, esse comando só pode ser usado em canais NSFW.", model.NomeUsuario))
                    .Build());
            }
            else
            {
                HttpExtensions http = new HttpExtensions();
                for (int i = 0; i < model.Quantidade; i++)
                {
                    int random = new Random().Next(links.Length);
                    await model.Canal.SendMessageAsync(embed: new EmbedBuilder()
                            .WithTitle(model.Texto)
                            .WithColor(Color.DarkPurple)
                            .WithImageUrl(await http.GetSite(links[random].Item1, links[random].Item2))
                        .Build());
                }
            }
        }
    }
}
