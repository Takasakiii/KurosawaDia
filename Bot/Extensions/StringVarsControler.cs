﻿using Discord.Commands;
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

        public StringVarsControler(CommandContext contexto)
        {

            variaveis = new List<VarTypes>();

            variaveis.Add(new VarTypes("%user%", contexto.User.ToString()));
            variaveis.Add(new VarTypes("%server%", contexto.Guild.Name));
        }

        public StringVarsControler(SocketGuildUser user)
        {

            variaveis = new List<VarTypes>();

            variaveis.Add(new VarTypes("%user%", user.ToString()));
            variaveis.Add(new VarTypes("%server%", user.Guild.Name));
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
            string replacedString = "";
            string[] parts = stringReplace.Split(' ');
            for (int i = 0; i < parts.Length; i++)
            {
                VarTypes findVar = variaveis.Find(x => x.VarName == parts[i]);
                if (findVar.Replace != null)
                {
                    parts[i] = findVar.Replace;
                }
            }
            replacedString = string.Join(" ", parts);
            return replacedString;
        }
    }
}