using Bot.Extensions;
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
        internal  ErrorExtension Erro { private set; get; }
        protected List<object> DumpComandos { private set; get; }

        internal ModulesConcat<GenericModule> ModuleContexto;
        public GenericModule(CommandContext contexto, params object[] args)
        {
            DumpComandos = new List<object>();
            Contexto = contexto;
            object[] obj = new object[4];
            for(int i = 0; i < args.Length; i++)
            {
                obj[i] = args[i];
            }

            PrefixoServidor = (string)obj[0];
            Comando = (string[])obj[1];
            Erro = (ErrorExtension)obj[2];
            ModuleContexto = (ModulesConcat<GenericModule>)obj[3];
        }
    }
}
