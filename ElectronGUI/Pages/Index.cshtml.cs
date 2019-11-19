using Bot;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading;
using System.Threading.Tasks;

namespace ElectronGUI.Pages
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {

        }

        public async Task OnPostAsync()
        {
            Core core = new Core();
            await core.CriarClienteAsync();
        }
    }
}
