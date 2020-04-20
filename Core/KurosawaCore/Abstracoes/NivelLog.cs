using System;
using System.Collections.Generic;
using System.Text;

namespace KurosawaCore.Abstracoes
{
    public enum NivelLog : byte
    {
        Critical = 0,
        Error = 1,
        Warning = 2,
        Info = 4,
        Debug = 8
    }
}
