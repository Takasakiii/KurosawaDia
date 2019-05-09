using Discord;
using Discord.Commands;
using System;
using System.Linq;

namespace Bot.Extensions
{
    public class UserExtensions
    {
        public Tuple<bool, IUser> GetUserAsync(CommandContext context, object[] args = null, string txt = null) //passamento de dados inuteis
        {
            ulong id = 0;
            if (context.Message.MentionedUserIds.Count != 0)
            {
                id = context.Message.MentionedUserIds.First();
            }
            else
            {
                string msg = "";

                if (txt == null && args != null)
                {
                    string[] comando = (string[])args[1]; //¯\_(ツ)_/¯
                    msg = string.Join(" ", comando, 1, (comando.Length - 1));
                }
                else
                {
                    msg = txt;
                }

                if (context.Message.MentionedUserIds.Count == 1)
                {
                    id = context.Message.MentionedUserIds.First();
                }
                else
                {
                    try
                    {
                        id = Convert.ToUInt64(msg);
                    }
                    catch
                    {
                        id = 0;
                    }
                }
            }

            IUser user;
            if (!context.IsPrivate) // isso devia estar acima ja aliviaria 90 % da carga do privado (otimização)
            {
                user = context.Guild.GetUserAsync(id).GetAwaiter().GetResult();
            }
            else
            {
                user = null;
            }

            if (user != null) // nem preciso falar o tanto de if errado q vc criou nas dependencias desse metodo sendo q vc ja tinha a solução 
            {
                return Tuple.Create(true, user);
            }
            else
            {
                return Tuple.Create(false, user);
            }
        }
    } // corige
}
