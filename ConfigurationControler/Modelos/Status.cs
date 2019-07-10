using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigurationControler.Modelos
{
    public class Status
    {
        //Tipos de Dados:
        public enum TiposDeStatus { Jogando, Live, Ouvindo, Assistindo }

        //Atributos
        public uint id { private set; get; }
        public string status_jogo { private set; get; }
        public TiposDeStatus status_tipo { private set; get; }
        
        public Status(string status_jogo, TiposDeStatus status_tipo)
        {
            this.status_jogo = status_jogo;
            this.status_tipo = status_tipo;
        }

        public Status(uint id, string status_jogo, TiposDeStatus status_tipo)
        {
            this.id = id;
            this.status_jogo = status_jogo;
            this.status_tipo = status_tipo;
        }

    }

}
