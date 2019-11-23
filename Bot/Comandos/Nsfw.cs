using Bot.Constantes;
using Bot.Extensions;
using Bot.GenericTypes;
using Bot.Modelos;
using Discord.Commands;
using MainDatabaseControler.DAO;
using MainDatabaseControler.Modelos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static MainDatabaseControler.Modelos.Servidores;

namespace Bot.Comandos
{
    public class Nsfw : GenericModule
    {
        private bool Autorizado = false;
        public Nsfw(CommandContext contexto, params object[] args) : base(contexto, args)
        {
            VerificarPermissao().Wait();
        }

        private async Task VerificarPermissao()
        {
            ulong id = 0;
            if (!Contexto.IsPrivate)
            {
                id = Contexto.Guild.Id;
            }

            Tuple<bool, Servidores> servidor = await new ServidoresDAO().GetPermissoesAsync(new Servidores(id));
            if (servidor.Item1)
            {
                if (servidor.Item2.Permissoes == PermissoesServidores.LolisEdition || servidor.Item2.Permissoes == PermissoesServidores.ServidorPika)
                {
                    Autorizado = true;
                }
            }
        }

        public async Task hentai()
        {
            Links links = new Links();

            List<Tuple<string, string>> imgs = new List<Tuple<string, string>>();
            imgs.Add(links.hentai);
            if (Autorizado)
            {
                imgs.Add(links.nsfw_hentai_gif);
                imgs.Add(links.lewdk);
                imgs.Add(links.hentaiLoli);
            }
            await new ImageExtensions().GetImgAsync(new ImgModel(Contexto.Channel, Contexto.User.ToString(), Nsfw: true), imgs.ToArray());
        }

        public async Task hentaibomb()
        {
            Links links = new Links();

            List<Tuple<string, string>> imgs = new List<Tuple<string, string>>();
            imgs.Add(links.hentai);
            if (Autorizado)
            {
                imgs.Add(links.nsfw_hentai_gif);
                imgs.Add(links.lewdk);
                imgs.Add(links.hentaiLoli);
            }
            await new ImageExtensions().GetImgAsync(new ImgModel(Contexto.Channel, Contexto.User.ToString(), Nsfw: true, Quantidade: 5), imgs.ToArray());
        }

        public async Task anal()
        {
            Links links = new Links();

            await new ImageExtensions().GetImgAsync(new ImgModel(Contexto.Channel, Contexto.User.ToString(), Nsfw: true), links.anal);
        }
    }
}


