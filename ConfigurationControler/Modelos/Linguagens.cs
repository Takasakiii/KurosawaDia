using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigurationControler.Modelos
{
    public class Linguagens
    {
        public ulong idString { private set; get; }
        public string idiomaString { private set; get; }
        public string stringIdentifier { private set; get; }
        public string texto { private set; get; }

        public Linguagens(string idiomaString, string stringIdentifier)
        {
            this.idiomaString = idiomaString;
            this.stringIdentifier = stringIdentifier;
        }

        public void SetString (ulong idString, string texto)
        {
            this.idString = idString;
            this.texto = texto;
        }
    }
}
