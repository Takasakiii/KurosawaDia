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

        private List<Variables> Vars;

        internal StringVariablesExtension(DiscordMember membro, DiscordGuild servidor)
        {
            Vars.Add(new Variables
            {
                Var = "user",
                Value = $"{membro.Username}#{membro.Discriminator}"
            });
            Vars.Add(new Variables
            {
                Var = "username",
            });
            Vars.Add(new Variables
            {
                Var = "usermention",
                Value = membro.Mention
            });
            Vars.Add(new Variables
            {
                Var = "id",
                Value = membro.Id.ToString()
            });
            Vars.Add(new Variables
            {
                Var = "avatar",
                Value = membro.AvatarUrl
            });
            Vars.Add(new Variables
            {
                Var = "membros",
                Value = servidor.MemberCount.ToString()
            });
            Vars.Add(new Variables
            {
                Var = "idservidor",
                Value = servidor.Id.ToString()
            });
            Vars.Add(new Variables
            {
                Var = "server",
                Value = servidor.Name
            });
            Vars.Add(new Variables
            {
                Var = "icon",
                Value = new ServerIconExtension().Get(servidor)
            });
        }

        internal override DiscordEmbed GetJsonEmbed(ref string message)
        {
            message = TrocarVariaveis(message);

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
                msg = TrocarVariaveis(msg);
                await canal.SendMessageAsync(msg);
            }
        }

        private string TrocarVariaveis(string message)
        {
            foreach (Variables tipo in Vars)
            {
                if (message.Contains(tipo.Var))
                {
                    message = message.Replace(tipo.Var, tipo.Value);
                }
            }

            return message;
        }
    }
}
