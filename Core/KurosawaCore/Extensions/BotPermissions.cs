using DataBaseController.Abstractions;
using DataBaseController.DAOs;
using DataBaseController.Modelos;
using DSharpPlus.Entities;
using System.Threading.Tasks;

namespace KurosawaCore.Extensions
{
    internal class BotPermissions
    {
        internal static ulong IDOwner { private get; set; }

        internal static async Task<TiposAdms> CheckAdm(DiscordUser usuario)
        {
            AdmsBot usuarioAdm = await new AdmsBotDAO().Get(new AdmsBot
            {
                Usuario = new Usuarios
                {
                    ID = usuario.Id
                }
            }) ?? new AdmsBot { 
                Permissao = (usuario.Id == IDOwner) ? TiposAdms.Dono : TiposAdms.Nenhuma
            };

            return usuarioAdm.Permissao;
        }
    }
}
