using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bot.GenericTypes
{
    public class GenericModule
    {
        //Discord Context para uso nos modulos
        protected CommandContext contexto { private set; get; }
        // Args para serem usados nos modulos
        protected object[] args { private set; get; }

        public GenericModule(CommandContext contexto, object[] args)
        {
            this.contexto = contexto;
            this.args = args;
        }


        
        //Instancia um GenericModule e preenche o contexto e o args
        public static GenericModule GenerateModule(CommandContext contexto, object[] args)
        {
            GenericModule modulo = new GenericModule(contexto, args);
            return modulo;
        }
    }
}
