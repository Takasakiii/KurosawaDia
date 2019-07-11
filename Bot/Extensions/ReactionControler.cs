using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Linq;

namespace Bot.Extensions
{
    public class ReactionControler
    {
        //Versao 2 by Takasaki

        public Thread GetReaction(IUserMessage mensagem, Emoji emoji, IUser usuarioComparado, ReturnMethod returnMethod)
        {
            Thread processo = new Thread(() =>
            {
                bool gatilho = false;
                do
                {
                    List<IUser> retono = mensagem.GetReactionUsersAsync(emoji, 1).FlattenAsync().GetAwaiter().GetResult().ToList();
                    if (retono.FindLast(x => usuarioComparado.Id == x.Id) != null)
                    {
                        gatilho = true;
                    }
                    Thread.Sleep(100);
                } while (!gatilho);
                returnMethod.Invoke();
            });
            processo.Start();
            return processo;
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
