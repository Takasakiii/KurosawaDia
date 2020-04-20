using DataBaseController.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseController.Modelos
{
    public class Cargos
    {
        public ulong Cod { get; set; }
        public TipoCargos TipoCargo { get; set; }
        public string Nome { get; set; }
        public ulong ID { get; set; }
        public Servidores Servidor { get; set; }
    }
}
