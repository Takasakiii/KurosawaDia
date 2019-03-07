using Bot.Nucleo.Modulos;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Weeb.net;

namespace Bot.Nucleo
{
    public class Catalogo
    {
        public async Task IrComando(CommandContext contexto, DiscordSocketClient client, SocketMessage sock, string[] comando)
        {
            switch(comando[0])
            {
                case "ping":
                    await new Utility(contexto).Ping();
                    break;
                case "avatar":
                    await new Utility(contexto).Avatar(client, comando);
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
//ok
