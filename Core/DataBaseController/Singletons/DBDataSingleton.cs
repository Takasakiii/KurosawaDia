using ConfigController.Models;

namespace DataBaseController.Singletons
{
    internal static class DBDataSingleton
    {

        internal static string ConnectionString { get; private set; } = "Server = ; Database = ; Uid = ; Pwd = ;";

        internal static void SetConnectionString(DBConfig dbconfig)
        {
            ConnectionString = $"Server={dbconfig.IP};Port={dbconfig.Porta};Database={dbconfig.Database};Uid={dbconfig.User};Pwd={dbconfig.Senha};";
        }
    }
}
