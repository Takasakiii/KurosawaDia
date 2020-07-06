using ConfigController.Models;

namespace DataBaseController.Singletons
{
    internal static class DBDataSingleton
    {

        internal static string ConnectionString { get; private set; } = "Server=127.0.0.1;Database=Kurosawa_Dia;Uid=Implementacao;Pwd=Implementacao@123;";

        internal static void SetConnectionString(DBConfig dbconfig)
        {
            ConnectionString = $"Server={dbconfig.IP};Port={dbconfig.Porta};Database={dbconfig.Database};Uid={dbconfig.User};Pwd={dbconfig.Senha};";
        }
    }
}
