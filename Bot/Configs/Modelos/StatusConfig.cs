using System;
using System.Collections.Generic;
using System.Text;

namespace Bot.Configs.Modelos
{
    public class StatusConfig
    {
        public int id { get; private set; }
        public string status { get; private set; }
        public int tipo { get; private set; }

        public void SetStatus(int id, string status, int tipo)
        {
            this.id = id;
            this.status = status;
            this.tipo = tipo;
        }
    }
}
