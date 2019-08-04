using Discord;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Linq;

namespace Bot.Extensions
{
    public class SubCommandControler
    {
        public bool ThreadLife { private set; get; }

        public SubCommandControler()
        {
            ThreadLife = true;
        }

        public void DesligarCommand()
        {
            ThreadLife = false;
        }

        public IMessage GetCommand(IMessageChannel canal, IUser usuario, int tempoAnalise = 60)
        {
            IMessage retorno = null;
            new Thread(() =>
            {
                Thread.Sleep(tempoAnalise * 1000);
                DesligarCommand();
            }).Start();
            do
            {
                try
                {
                    List<IMessage> messages = canal.GetMessagesAsync(1).FlattenAsync().GetAwaiter().GetResult().ToList();
                    IMessage msg = messages.Find(x => x.Author.Id == usuario.Id);
                    if (msg != null)
                    {
                        ThreadLife = false;
                        retorno = msg;
                    }
                    else
                    {
                        Thread.Sleep(135);
                    }
                }
                catch
                {
                    Thread.Sleep(134);
                }
            } while (ThreadLife);
            return retorno;
        }
    }
}
