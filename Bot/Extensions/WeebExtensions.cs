using Bot.Constructor;
using Discord;
using Discord.Commands;
using System;
using System.Linq;
using System.Threading.Tasks;
using Weeb.net;
using Weeb.net.Data;

namespace Bot.Extensions
{
    public class WeebExtensions
    {
        private WeebClient weebClient = new WeebGen().weebClient;
        public async Task<IUserMessage> WeebCmd(bool auto, string tipo, string msg, CommandContext context, object[] args)
        {
            RandomData img = await weebClient.GetRandomAsync(tipo, new string[] { }, FileType.Gif, false, NsfwSearch.False);

            string user;
            try
            {
                if (context.Message.MentionedUserIds.Count != 0)
                {
                    user = context.Client.GetUserAsync(context.Message.MentionedUserIds.ElementAt(0)).GetAwaiter().GetResult().Username;
                }
                else
                {
                    string[] comando = (string[])args[1];
                    user = context.Client.GetUserAsync(Convert.ToUInt64(comando[1])).GetAwaiter().GetResult().Username;
                }
            }
            catch
            {
                user = "ele(a) mesmo";
            }

            if (user == null)
            {
                user = "ele(a) mesmo";
            }

            string txt = "";
            if(auto)
            {
                txt = $"{context.User.Username} esta {msg} {user}";
            } else
            {
                txt = msg;
            }

            return await context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle(txt)
                    .WithImageUrl(img.Url)
                    .WithColor(Color.DarkPurple)
                .Build());
        }
    }
}
