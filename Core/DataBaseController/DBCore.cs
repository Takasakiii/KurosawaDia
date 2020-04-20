using ConfigController.Models;
using DataBaseController.Singletons;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseController
{
    public sealed class DBCore
    {
        public DBCore(DBConfig dBConfig)
        {
            DBDataSingleton.ConfigDB = dBConfig;
        }
    }
}
