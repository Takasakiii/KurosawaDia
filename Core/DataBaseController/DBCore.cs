using ConfigController.Models;
using DataBaseController.Singletons;

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
