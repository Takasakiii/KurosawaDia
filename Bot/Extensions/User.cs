using Discord;
using Discord.Commands;
using System;
using System.Linq;

namespace Bot.Extensions
{
    public class User
    {
        public Tuple<bool, IUser> GetUserAsync(CommandContext context, object[] args)
        {
            ulong id = 0;
            if (context.Message.MentionedUserIds.Count != 0)
            {
                id = context.Message.MentionedUserIds.First();
            }
            else
            {
                string[] comando = (string[])args[1];
                string msg = string.Join(" ", comando, 1, (comando.Length - 1));

                try
                {
                    id = System.Convert.ToUInt64(msg);
                }
                catch
                {
                    id = 0;
                }
            }

            IUser user = null;
            if (id != 0)
            {
                user = context.Guild.GetUserAsync(id).GetAwaiter().GetResult();
                return Tuple.Create(true, user);
            }
            else
            {
                return Tuple.Create(false, user);
            }
        }
    }
}
