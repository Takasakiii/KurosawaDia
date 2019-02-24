using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.DB.Modelos
{
    public class Tokens
    {
        public int id { get; private set; }
        public string Token { get; set; }
        public string Prefixo { get; set; }
        public string WeebToken { get; set; }

        public Tokens(int id)
        {
            this.id = id;
        }
    }
}
