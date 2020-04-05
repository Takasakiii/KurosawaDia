using System;

namespace KurosawaCore.Models.Atributes
{
    internal class Modulo : Attribute
    { 
        internal string Nome { private set; get; }
        internal string Icon { private set; get; }

        internal Modulo(string nome, string icon = null)
        {
            Nome = nome;
            Icon = icon;
        }
    }
}
