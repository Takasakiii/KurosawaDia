using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading;

namespace Bot.Comandos
{
    public class Moderacao
    {
        /*
            pra que server
            tantos codigos
            se a vida
            não é programada
            e as melhores coisas
            não tem logica
        */
        /*
         *  Pra que serve
         *  o dinheiro 
         *  se a melhores coisas
         *  são feitas de graça
         *      - Takasaki 2019 
         *      (so vc pegar mina kkkkkkkkkkkkk ou ter amigos XD)
         */
        private void moderacao(int tipo, CommandContext context, object[] args) // piuwiiiiii
        {
            
        }

        public void kick(CommandContext context, object[] args)
        {
            moderacao(1, context, args); // la vai ele o trenzinho
        }
        public void ban(CommandContext context, object[] args)
        {
            moderacao(2, context, args); //¯\_(ツ)_/¯
        }
        public void softban(CommandContext context, object[] args)
        {
            moderacao(3, context, args); //¯\_(ツ)_/¯
        }
    }
}


