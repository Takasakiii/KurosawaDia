using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Extensions
{
    public class User
    {
        public async Task<IUser> GetUserAsync(CommandContext context)
        {
            ulong id = context.Message.MentionedUserIds.First();
            IUser user = await context.Guild.GetUserAsync(id);

            return user;
        }
    }
}
