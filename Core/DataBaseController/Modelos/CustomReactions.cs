using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseController.Modelos
{
    public class CustomReactions
    {
        public ulong Cod { get; set; }
        public string Trigger { get; set; }
        public string Resposta { get; set; }
        public bool Modo { get; set; }
        public Servidores Servidor { get; set; }
    }
}
