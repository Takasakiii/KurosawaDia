using System;
using System.Collections.Generic;
using System.Text;

namespace Bot.DataBase.MainDB.Modelos
{
    public class Usuarios
    {
        public ulong id { get; private set; }
        public string nome { get; private set; }
        public uint codigo { get; private set; }



        public Usuarios(ulong id, string nome, uint codigo = 0)
        {
            this.id = id;
            this.nome = nome;
            this.codigo = codigo;
        }
    }
}
