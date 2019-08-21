using System;
using System.Collections.Generic;
using System.Text;

namespace MainDatabaseControler.Modelos
{
    public class Cargos
    {
        public enum Tipos_Cargos { AjudanteDeIdol, XpRole}

        public ulong Cod { private set; get; }
        public Tipos_Cargos TiposCargos { private set; get; }
        public ulong Id { private set; get; }
        public string Cargo { private set; get; }
        public long Requesito { private set; get; } 
        public Servidores Servidor { private set; get; }
        

        public Cargos(Servidores servidor, long requesito, Tipos_Cargos tipos_Cargos = Tipos_Cargos.XpRole)
        {
            CriarCargos(servidor: servidor, requesito: requesito, tipos_Cargos: tipos_Cargos);
        }

        public Cargos (Tipos_Cargos tipos_Cargos, ulong id, string cargo, long requesito, Servidores servidor)
        {
            CriarCargos(0, tipos_Cargos, id, cargo, requesito, servidor);
        }

        public void AdicionarDadosCargo(ulong id, string cargo, ulong cod)
        {
            Id = id;
            Cargo = cargo;
            Cod = cod;
        }

        private void CriarCargos(ulong cod = 0, Tipos_Cargos tipos_Cargos = Tipos_Cargos.XpRole, ulong id = 0, string cargo = "", long requesito = 0, Servidores servidor = null)
        {
            Cod = cod;
            TiposCargos = tipos_Cargos;
            Id = id;
            Servidor = servidor;
            Cargo = cargo;
            Requesito = requesito;
        }
        
    }
}
