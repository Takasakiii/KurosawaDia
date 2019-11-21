using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bot.Extensions
{
    public class ReactionControler
    {
        //Versao 2 by Takasaki

        public bool ThreadLife { private set; get; }


        public ReactionControler()
        {
            ThreadLife = true;
        }


        public void DesligarReaction()
        {
            ThreadLife = false;
        }

        public void GetReaction(IUserMessage mensagem, Emoji emoji, IUser usuarioComparado, ReturnMethod returnMethod, int tempoAnalise = 60)
        {
            
            
            Thread processo = new Thread(async () =>
            {
                bool gatilho = false;
 

                do
                {
                    try
                    {

                        List<IUser> retono = (await mensagem.GetReactionUsersAsync(emoji, 10).FlattenAsync()).ToList();
                        if (retono.FindLast(x => usuarioComparado.Id == x.Id) != null)
                        {
                            gatilho = true;
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
                } while (!gatilho && ThreadLife);
                if (ThreadLife)
                {
                    returnMethod.Invoke();
                } else
                {
                    bool tentativa = false;
                    do
                    {
                        try
                        {
                            await mensagem.RemoveReactionAsync(emoji, mensagem.Author);
                        }
                        catch
                        {
                            Thread.Sleep(134);
                            tentativa = true;
                        }
                    } while (tentativa);
                }
            });
            processo.Start();
            new Thread(() =>
            {
                Thread.Sleep(tempoAnalise * 1000);
                DesligarReaction();
            }).Start();
        }


    }

    public class ReturnMethod
    {
        public Func<Task> methodResult { private set; get; }


        public ReturnMethod(Func<Task> methodResult)
        {
            this.methodResult = methodResult;
        }

        public void Invoke()
        {
            methodResult.Invoke();
        }
    }

}
