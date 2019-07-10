using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigurationControler.Singletons
{
    public static class DB
    {
        public static string LocalFile { private set; get; }

        public static void SetDB (string _LocalFile)
        {
            LocalFile = _LocalFile;
        }
    }
}
