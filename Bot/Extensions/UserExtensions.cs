using Discord;
using System;
using System.Collections.Generic;

namespace Bot.Extensions
{
    public class UserExtensions
    {
        public IUser GetUser(IReadOnlyCollection<IGuildUser> userCollection, string txt)
        {
            string[] txtArray = txt.Split(' ');
            char[] idChar = txtArray[0].ToCharArray();
            string id = "";
            IUser user = null;

            foreach (char tmp in idChar)
            {
                if (tmp != '<' && tmp != '!' && tmp != '@' && tmp != '>')
                {
                    id += tmp.ToString();
                }
            }

            if (id != "")
            {
                List<IGuildUser> userList = new List<IGuildUser>(userCollection);

                try
                {
                    ulong userId = Convert.ToUInt64(id);
                    user = userList.Find(x => x.Id == userId);
                }
                catch
                {
                    if (txtArray[0].Contains("#"))
                    {
                        user = userList.Find(x => x.ToString().ToLowerInvariant() == txtArray[0].ToLowerInvariant());
                    }
                    else
                    {
                        if (userList.Exists(x => x.Nickname != null && x.Nickname.ToLowerInvariant() == txtArray[0].ToLowerInvariant()))
                        {
                            user = userList.Find(x => x.Nickname != null && x.Nickname.ToLowerInvariant() == txtArray[0].ToLowerInvariant());
                        }
                        else if (userList.Exists(x => x.Username != null && x.Username.ToLowerInvariant() == txtArray[0].ToLowerInvariant()))
                        {
                            user = userList.Find(x => x.Username != null && x.Username.ToLowerInvariant() == txtArray[0].ToLowerInvariant());
                        }
                    }
                }
            }
            return user;
        }

        public string GetNickname(IUser user, bool servidor)
        {
            string nome = "";
            if (servidor)
            {
                IGuildUser guildUser = user as IGuildUser;

                if(guildUser.Nickname != null)
                {
                    nome = guildUser.Nickname;
                }
                else
                {
                    nome = user.Username;
                }
            }
            else
            {
                nome = user.Username;
            }
            return nome;
        }
    }
}
