using System;
using System.Collections.Generic;
using System.Text;

namespace Bot.DataBase.MainDB.Modelos
{
    public class Servidores_Usuarios
    {
        public Servidores servidor { private set; get; }
        public Usuarios usuario { private set; get; }

        public Servidores_Usuarios(Servidores servidor, Usuarios usuario)
        {
            this.servidor = servidor;
            this.usuario = usuario;
        }
    }
}
