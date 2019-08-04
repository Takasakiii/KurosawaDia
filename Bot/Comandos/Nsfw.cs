using Bot.Constantes;
using Bot.DataBase.MainDB.DAO;
using Bot.DataBase.MainDB.Modelos;
using Discord.Commands;
using System;
using System.Collections.Generic;

namespace Bot.Comandos
{
    public class Nsfw : Image
    {
        public void hentai(CommandContext context, object[] args)
        {
            Links links = new Links();

            List<Tuple<string, string>> imgs = new List<Tuple<string, string>>();
            imgs.Add(links.hentai);
            if (!context.IsPrivate)
            {
                Servidores servidor = new Servidores(context.Guild.Id);
                if(new ServidoresDAO().GetPermissoes(ref servidor))
                {
                    if(servidor.permissoes == Servidores.Permissoes.LolisEdition || servidor.permissoes == Servidores.Permissoes.ServidorPika)
                    {
                        imgs.Add(links.nsfw_hentai_gif);
                        imgs.Add(links.lewdk);
                    }
                }
            }
            getImg(context, imgs: imgs.ToArray(), nsfw: true);
        }

        public void hentaibomb(CommandContext context, object[] args)
        {
            Links links = new Links();

            List<Tuple<string, string>> imgs = new List<Tuple<string, string>>();
            imgs.Add(links.hentai);
            if (!context.IsPrivate)
            {
                Servidores servidor = new Servidores(context.Guild.Id);
                if (new ServidoresDAO().GetPermissoes(ref servidor))
                {
                    if (servidor.permissoes == Servidores.Permissoes.LolisEdition || servidor.permissoes == Servidores.Permissoes.ServidorPika)
                    {
                        imgs.Add(links.nsfw_hentai_gif);
                        imgs.Add(links.lewdk);
                    }
                }
            }
            getImg(context, img: links.hentai, nsfw: true, quantidade: 5);
        }

        public void anal(CommandContext context, object[] args)
        {
            Links links = new Links();

            getImg(context, img: links.anal, nsfw: true);

        }
    }
}


