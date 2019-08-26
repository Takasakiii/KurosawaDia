using System;

namespace Site.pags
{
    public partial class teste : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("https://discordapp.com/oauth2/authorize?client_id=389917977862078484&scope=bot&permissions=8");
        }
    }
}