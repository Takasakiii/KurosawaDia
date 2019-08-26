using System;

namespace Site
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Web.Routing.RouteTable.Routes.MapPageRoute("Convite", "Convite", "~/pags/convite.aspx");
            System.Web.Routing.RouteTable.Routes.MapPageRoute("Servidor", "Servidor", "~/pags/servidor.aspx");
        }
    }
}