using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bot.GenericTypes
{
    public class GenericModule
    {
        //Discord Context para uso nos modulos
        protected CommandContext Contexto { private set; get; }

        protected string PrefixoServidor { private set; get; }
        protected string[] Comando { private set; get; }
        protected List<object> DumpComandos { private set; get; }


        public GenericModule(CommandContext contexto, string prefixo, string[] comando)
        {
            Contexto = contexto;
            PrefixoServidor = prefixo;
            Comando = comando;
            DumpComandos = new List<object>();
        }

    }
}
