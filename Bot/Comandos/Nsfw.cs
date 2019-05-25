using Discord.Commands;
using System;

namespace Bot.Comandos
{
    public class Nsfw : Image
    {
        public void hentai(CommandContext context, object[] args)
        {
            Tuple<string, string>[] tuple =
            {
                links.hentai,
                links.nsfw_hentai_gif,
                links.lewdk
            };

            getImg(context, imgs: tuple, nsfw: true);
        }

        public void hentaibomb(CommandContext context, object[] args)
        {

            Tuple<string, string>[] tuple =
            {
                links.hentai,
                links.nsfw_hentai_gif,
                links.lewdk
            };

            getImg(context, imgs: tuple, nsfw: true, quantidade: 5);
        }

        public void hneko(CommandContext context, object[] args)
        {
            Tuple<string, string>[] tuple =
            {
                links.nsfw_hentai_gif,
                links.lewdk
            };

            getImg(context, imgs: tuple, nsfw: true);
        }

        public void anal(CommandContext context, object[] args)
        {
            getImg(context, img: links.anal, nsfw: true);

        }
    }
}


