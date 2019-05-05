using Discord;
using Discord.Commands;
using System;
using System.Linq;

namespace Bot.Extensions
{
    public class UserExtensions
    {
        public Tuple<bool, IUser> GetUserAsync(CommandContext context, object[] args = null, string txt = null)
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
                    string[] comando = (string[])args[1];
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
            if (!context.IsPrivate)
            {
                user = context.Guild.GetUserAsync(id).GetAwaiter().GetResult();
            }
            else
            {
                user = null;
            }

            if (user != null)
            {
                return Tuple.Create(true, user);
            }
            else
            {
                return Tuple.Create(false, user);
            }
        }
    }
}
