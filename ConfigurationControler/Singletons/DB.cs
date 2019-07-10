using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigurationControler.Singletons
{
    public static class DB
    {
        public static string LocalFile { private set; get; }
        public static bool ExisteFile { private set; get; }

        public static void SetDB (bool _ExisteFile, string _LocalFile)
        {
            ExisteFile = _ExisteFile;
            LocalFile = _LocalFile;
        }
    }
}
