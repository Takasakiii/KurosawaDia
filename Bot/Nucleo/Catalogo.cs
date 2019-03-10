using Bot.Nucleo.Modulos;
using Bot.Nucleo.Modulos.Owner;
using Discord.Commands;
using System.Threading.Tasks;

namespace Bot.Nucleo
{
    public class Catalogo
    {
        public async Task IrComando(CommandContext contexto, string[] comando)
        {
            switch(comando[0])
            {
                case "ping":
                    await new Owner(contexto).Ping();
                    break;
                case "avatar":
                    await new Utility(contexto).Avatar(comando);
                    break;
                case "hug":
                    await new weebCmds(contexto).Hug();
                    break;
                case "weeb":
                    await new weebCmds(contexto).Weeb(comando); 
                    break;
                case "webcam":
                    await new Utility(contexto).WebCam();
                    break;
            }
        }
    }
}
