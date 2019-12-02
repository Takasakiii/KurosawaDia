using Bot;
using ConfigurationControler.Factory;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

                    break;
            }
        }
    }
}
