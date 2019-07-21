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

        public void GetReaction(IUserMessage mensagem, Emoji emoji, IUser usuarioComparado, ReturnMethod returnMethod, int tempoAnalise = 60, bool addEmoji = true)
        {
            if (addEmoji)
            {
                RequestOptions request = new RequestOptions();
                request.RetryMode = RetryMode.AlwaysRetry;
                mensagem.AddReactionAsync(emoji, request);
            }
            
            Thread processo = new Thread(() =>
            {
                bool gatilho = false;
 

                do
                {
                    try
                    {
                        
                        List<IUser> retono = mensagem.GetReactionUsersAsync(emoji, 1).FlattenAsync().GetAwaiter().GetResult().ToList();
                        if (retono.FindLast(x => usuarioComparado.Id == x.Id) != null)
                        {
                            gatilho = true;
                        }
                        Thread.Sleep(100);
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
                    mensagem.RemoveReactionAsync(emoji, mensagem.Author);
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
        public Action<CommandContext, object[]> methodResult { private set; get; }
        public CommandContext command { private set; get; }
        public object[] args { private set; get; }

        public ReturnMethod(Action<CommandContext, object[]> methodResult, CommandContext command, object[] args)
        {
            this.methodResult = methodResult;
            this.command = command;
            this.args = args;
        }

        public void Invoke()
        {
            methodResult.Invoke(command, args);
        }
    }

}
