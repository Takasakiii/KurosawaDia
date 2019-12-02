using Bot;
using ConfigurationControler.DAO;
using ConfigurationControler.Factory;
using ConfigurationControler.Modelos;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ElectronGUI.Pages
{
    public class IndexModel : PageModel
    {
        public bool dbexiste = false;

        public void OnGet()
        {
            dbexiste = ConnectionFactory.VerificarDB();
        }

        public async Task OnPostAsync()
        {
            string botaovalue = Request.Form["btOK"];
            switch (botaovalue)
            {
                case "iniciar":
                    Core core = new Core();
                    await core.CriarClienteAsync();
                    break;
                case "salvar":
                    string token = Request.Form["txToken"];
                    string prefixo = Request.Form["txPrefixo"];
                    string iddono = Request.Form["txIDDono"];
                    string ip = Request.Form["txIP"];
                    string porta = Request.Form["txPorta"];
                    string usuario = Request.Form["txUsuario"];
                    string senha = Request.Form["txSenha"];
                    string db = Request.Form["txDB"];
                    try
                    {
                        DiaConfig diaConfig = new DiaConfig(token, prefixo, Convert.ToUInt64(iddono));
                        DBConfig dbConfig = new DBConfig(ip, db, usuario, senha, Convert.ToInt32(porta));
                        ApisConfig[] apis = { new ApisConfig("weeb", "", false, 1), new ApisConfig("dba", "", false, 2) };
                        await new DBDAO().AdicionarAtualizarAsync(apis, dbConfig, diaConfig);
                        Response.Redirect("index");
                    }
                    catch
                    {
                        Debug.WriteLine("Morri");
                    }
                    break;
                default:
                    break;
            }

        }
    }
}
