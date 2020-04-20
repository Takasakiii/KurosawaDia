using ConfigController.Models;

namespace DataBaseController.Singletons
{
    internal static class DBDataSingleton
    {
        private static DBConfig _ConfigDB;

        internal static DBConfig ConfigDB
        {
            get
            {
                return _ConfigDB ?? new DBConfig
                {
                    Database = "Kurosawa_Dia",
                    Porta = 3306,
                    IP = "127.0.0.1",
                    User = "imprementacao",
                    Senha = "Imprementacao@123"
                };
            }
            set
            {
                _ConfigDB = value;
            }
        }
    }
}
