using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bot.Extensions
{
    public class UserExtensions
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

                SocketGuild guild = context.Guild as SocketGuild;

                IReadOnlyCollection<SocketGuildUser> users = guild.Users;

                try
                {
                    id = users.FirstOrDefault(x => x.Username.ToLowerInvariant() == msg.ToLowerInvariant()).Id;
                }
                catch (NullReferenceException)
                {
                    id = users.FirstOrDefault(x => x.Id == Convert.ToUInt64(msg)).Id;
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
