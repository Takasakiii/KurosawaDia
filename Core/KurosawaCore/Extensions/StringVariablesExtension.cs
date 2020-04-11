using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KurosawaCore.Extensions
{
    internal class StringVariablesExtension : JsonEmbedExtension.JsonEmbedExtension
    {
        private struct Variables
        {
            public string Var { get; set; }
            public string Value { get; set; }
        }

        private Variables[] Vars;

        internal StringVariablesExtension(DiscordMember membro, DiscordGuild servidor)
        {
            List<Variables> variaveis = new List<Variables>();
            variaveis.Add(new Variables
            {
                Var = "%user%",
                Value = $"{membro.Username}#{membro.Discriminator}"
            });
            variaveis.Add(new Variables
            {
                Var = "%username%",
                Value = membro.Username
            });
            variaveis.Add(new Variables
            {
                Var = "%usermention%",
                Value = membro.Mention
            });
            variaveis.Add(new Variables
            {
                Var = "%id%",
                Value = membro.Id.ToString()
            });
            variaveis.Add(new Variables
            {
                Var = "%avatar%",
                Value = membro.AvatarUrl
            });
            variaveis.Add(new Variables
            {
                Var = "%membros%",
                Value = servidor.MemberCount.ToString()
            });
            variaveis.Add(new Variables
            {
                Var = "%idservidor%",
                Value = servidor.Id.ToString()
            });
            variaveis.Add(new Variables
            {
                Var = "%server%",
                Value = servidor.Name
            });
            Vars = variaveis.ToArray();
        }

        internal override DiscordEmbed GetJsonEmbed(ref string message)
        {
            foreach(Variables variable in Vars)
            {
                message.Replace(variable.Var, variable.Value);
            }

            return base.GetJsonEmbed(ref message);
        }

        internal override async Task SendMessage(DiscordChannel canal, string msg)
        {
            try
            {
                DiscordEmbed embed = GetJsonEmbed(ref msg);
                await canal.SendMessageAsync(msg, embed: embed);
            }
            catch
            {
                await canal.SendMessageAsync(msg);
            }
        }
    }
}
