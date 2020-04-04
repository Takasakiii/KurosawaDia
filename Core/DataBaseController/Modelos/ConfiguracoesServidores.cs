using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseController.Modelos
{
    public class ConfiguracoesServidores
    {
        public uint Cod { get; set; }
        public string Key { get; set; }
        public virtual List<ConfiguracoesServidoresAplicada> Configuracoes { get; set; }
    }
}
