using System;
using System.Collections.Generic;
using System.Text;

namespace Bot.DataBase.MainDB.Modelos
{
    public class Usuarios
    {
        public long id { get; private set; }
        public string nome { get; private set; }
        public int codigo { get; private set; }

        public void SetUsuario(long id, string nome, int codigo = 0)
        {
            this.id = id;
            this.nome = nome;
            this.codigo = codigo;
        }
    }
}
