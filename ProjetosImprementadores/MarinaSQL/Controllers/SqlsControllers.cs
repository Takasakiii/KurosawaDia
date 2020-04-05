using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MarinaSQL.Controllers
{
    internal sealed class SqlsControllers
    {
        private FileInfo[] Arquivos;
        internal SqlsControllers(string path)
        {
            DirectoryInfo diretorio = new DirectoryInfo(path);
            Arquivos = diretorio.GetFiles("*.sql", SearchOption.AllDirectories);
        }


        internal async Task<string> GetSql()
        {
            string sql = "";
            foreach (FileInfo arquivo in Arquivos)
            {
                byte[] buffer = new byte[arquivo.Length];
                using (FileStream memoria = arquivo.OpenRead())
                {
                    await memoria.ReadAsync(buffer, 0, buffer.Length);
                    sql += $"{Encoding.UTF8.GetString(buffer, 0, buffer.Length)}\n\n";
                }
            }
            return sql;
        }
    }
}
