using Discord.Commands;
using System;

namespace Bot.Comandos
{
    public class Nsfw : Image
    {
        public void hentai(CommandContext context, object[] args)
        {
            getImg(context, img: links.hentai, nsfw: true);
        }

        public void hentaibomb(CommandContext context, object[] args)
        {

            getImg(context, img: links.hentai, nsfw: true, quantidade: 5);
        }

        public void anal(CommandContext context, object[] args)
        {
            getImg(context, img: links.anal, nsfw: true);

        }
    }
}


