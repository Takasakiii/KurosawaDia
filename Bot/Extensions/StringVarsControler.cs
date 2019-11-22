using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bot.Extensions
{
    public class StringVarsControler
    {
        public struct VarTypes
        {
            public string VarName { private set; get; }
            public string Replace { private set; get; }

            public VarTypes(string VarName, string Replace)
            {
                this.VarName = VarName;
                this.Replace = Replace;
            }
        }

        private List<VarTypes> variaveis;

        public StringVarsControler(CommandContext contexto = null, SocketGuildUser user = null)
        {

            variaveis = new List<VarTypes>();

            if (contexto != null && user == null)
            {
                variaveis.Add(new VarTypes("%user%", contexto.User.ToString()));
                variaveis.Add(new VarTypes("%server%", contexto.Guild.Name));
                variaveis.Add(new VarTypes("%id%", contexto.User.Id.ToString()));
                variaveis.Add(new VarTypes("%avatar%", contexto.User.GetAvatarUrl(size: 2048) ?? contexto.User.GetDefaultAvatarUrl()));
                variaveis.Add(new VarTypes("%membros%", contexto.Guild.GetUsersAsync().GetAwaiter().GetResult().Count.ToString()));
                variaveis.Add(new VarTypes("%idservidor%", contexto.Guild.Id.ToString()));
                variaveis.Add(new VarTypes("%usermention%", contexto.User.Mention));
            }
            else
            {
                variaveis.Add(new VarTypes("%user%", user.ToString()));
                variaveis.Add(new VarTypes("%server%", user.Guild.Name));
                variaveis.Add(new VarTypes("%id%", user.Id.ToString()));
                variaveis.Add(new VarTypes("%avatar%", user.GetAvatarUrl(size: 2048) ?? user.GetDefaultAvatarUrl()));
                variaveis.Add(new VarTypes("%membros%", (user.Guild as IGuild).GetUsersAsync().GetAwaiter().GetResult().Count.ToString()));
                variaveis.Add(new VarTypes("%idservidor%", user.Guild.Id.ToString()));
                variaveis.Add(new VarTypes("%usermention%", user.Mention));
            }
        }

        public void AdicionarComplemento (VarTypes complemento)
        {
            variaveis.Add(complemento);
        }

        public void AdicionarComplementos(VarTypes[] complementos)
        {
            foreach(VarTypes var in complementos)
            {
                variaveis.Add(var);
            }
        }

        public void AdicionarComplementos(List<VarTypes> complementos)
        {
            foreach (VarTypes var in complementos)
            {
                variaveis.Add(var);
            }
        }

        public string SubstituirVariaveis(string stringReplace)
        {
            foreach(VarTypes tipo in variaveis)
            {
                if (stringReplace.Contains(tipo.VarName))
                {
                    stringReplace = stringReplace.Replace(tipo.VarName, tipo.Replace);
                }
            }
            return stringReplace;
        }
    }
}
