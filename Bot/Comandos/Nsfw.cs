using Bot.Constantes;
using Bot.Extensions;
using Bot.GenericTypes;
using Discord.Commands;
using MainDatabaseControler.DAO;
using MainDatabaseControler.Modelos;
using System;
using System.Collections.Generic;
using static MainDatabaseControler.Modelos.Servidores;

namespace Bot.Comandos
{
    public class Nsfw : GenericModule
    {
        public Nsfw(CommandContext contexto, object[] args) : base (contexto, args)
        {

        }

        public void hentai()
        {
            Links links = new Links();

            List<Tuple<string, string>> imgs = new List<Tuple<string, string>>();
            imgs.Add(links.hentai);
            if (!contexto.IsPrivate)
            {
                Servidores servidor = new Servidores(contexto.Guild.Id);
                if(new ServidoresDAO().GetPermissoes(ref servidor))
                {
                    if(servidor.Permissoes == PermissoesServidores.LolisEdition || servidor.Permissoes == PermissoesServidores.ServidorPika)
                    {
                        imgs.Add(links.nsfw_hentai_gif);
                        imgs.Add(links.lewdk);
                        imgs.Add(links.hentaiLoli);
                    }
                }
            }
            new ImageExtensions().getImg(contexto, imgs: imgs.ToArray(), nsfw: true);
        }

        public void hentaibomb()
        {
            Links links = new Links();

            List<Tuple<string, string>> imgs = new List<Tuple<string, string>>();
            imgs.Add(links.hentai);
            if (!contexto.IsPrivate)
            {
                Servidores servidor = new Servidores(contexto.Guild.Id);
                if (new ServidoresDAO().GetPermissoes(ref servidor))
                {
                    if (servidor.Permissoes == PermissoesServidores.LolisEdition || servidor.Permissoes == PermissoesServidores.ServidorPika)
                    {
                        imgs.Add(links.nsfw_hentai_gif);
                        imgs.Add(links.lewdk);
                        imgs.Add(links.hentaiLoli);
                    }
                }
            }
            new ImageExtensions().getImg(contexto, img: links.hentai, nsfw: true, quantidade: 5);
        }

        public void anal()
        {
            Links links = new Links();

            new ImageExtensions().getImg(contexto, img: links.anal, nsfw: true);

        }
    }
}


