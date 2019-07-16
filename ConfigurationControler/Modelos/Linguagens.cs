using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigurationControler.Modelos
{
    public class Linguagens
    {
        public enum Idiomas { Portugues, Ingles, Espanhol, Russo}

        public ulong idString { private set; get; }
        public Idiomas idiomaString { private set; get; }
        public string stringIdentifier { private set; get; }
        public string texto { private set; get; }

        public Linguagens(Idiomas idiomaString, string stringIdentifier)
        {
            this.idiomaString = idiomaString;
            this.stringIdentifier = stringIdentifier;
        }

        public Linguagens(Idiomas idiomaString, string stringIdentifier, string texto)
        {
            this.idiomaString = idiomaString;
            this.stringIdentifier = stringIdentifier;
            this.texto = texto;
        }

        public void SetString (ulong idString, string texto)
        {
            this.idString = idString;
            this.texto = texto;
        }
    }
}
