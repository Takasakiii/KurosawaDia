using Discord;
using System;
using System.Collections.Generic;

namespace Bot.Extensions
{
    public class UserExtensions
    {
        public Tuple<IUser, string> GetUser(IReadOnlyCollection<IGuildUser> userCollection, string txt)
        {
            char[] txtArray = txt.ToCharArray();
            char[] idChar = txt.Trim().Split(' ')[0].ToCharArray();
            string id = "";
            IUser user = null;

            foreach (char tmp in idChar)
            {
                if (tmp != '<' && tmp != '!' && tmp != '@' && tmp != '>')
                {
                    id += tmp.ToString();
                }
            }

            int tamanho = 0;
            if (id != "")
            {
                List<IGuildUser> userList = new List<IGuildUser>(userCollection);

                try
                {
                    ulong userId = Convert.ToUInt64(id);
                    user = userList.Find(x => x.Id == userId);
                    tamanho = idChar.Length;
                }
                catch
                {
                    string tmp2 = "";
                    foreach (char a in txtArray)
                    {
                        tmp2 += a.ToString();

                        if (tmp2.Contains("#"))
                        {
                            user = userList.Find(x => x.ToString().ToLowerInvariant() == tmp2.ToLowerInvariant());
                            tamanho = tmp2.Length;
                        }
                        else
                        {
                            if (userList.Exists(x => x.Nickname != null && x.Nickname.ToLowerInvariant() == tmp2.ToLowerInvariant()))
                            {
                                user = userList.Find(x => x.Nickname != null && x.Nickname.ToLowerInvariant() == tmp2.ToLowerInvariant());
                                tamanho = tmp2.Length;
                            }
                            else if (userList.Exists(x => x.Username != null && x.Username.ToLowerInvariant() == tmp2.ToLowerInvariant()))
                            {
                                user = userList.Find(x => x.Username != null && x.Username.ToLowerInvariant() == tmp2.ToLowerInvariant());
                                tamanho = tmp2.Length;
                            }
                        }
                    }
                }
            }
            return Tuple.Create(user, txt.Substring(tamanho, txt.Length - tamanho).Trim());
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
