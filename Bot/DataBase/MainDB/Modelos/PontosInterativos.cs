using System;
using System.Collections.Generic;
using System.Text;

namespace Bot.DataBase.MainDB.Modelos
{
    public class PontosInterativos
    {
        public ulong cod { private set; get; }
        public Servidores_Usuarios servidores_usuarios { private set; get; }
        public ulong PI { private set; get; }
        public ulong fragmentosPI { private set; get; }

        public PontosInterativos(Servidores_Usuarios servidores_usuarios, ulong cod = 0)
        {
            this.servidores_usuarios = servidores_usuarios;
            this.cod = cod;
        }

        public void addPIInfo(ulong cod, ulong PI, ulong fragmentosPI)
        {
            this.cod = cod;
            this.PI = PI;
            this.fragmentosPI = fragmentosPI;
        }
    }
}
