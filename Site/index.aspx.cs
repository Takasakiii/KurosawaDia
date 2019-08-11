using MainDatabaseControler.DAO;
using MainDatabaseControler.Modelos;
using System;

namespace Site
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btOk_Click1(object sender, EventArgs e)
        {
            Servidores servidor = new Servidores(Convert.ToUInt64(txId.Text));
            if (new ServidoresDAO().GetPrefix(ref servidor))
            {
                lbPrefix.Text += new string(servidor.Prefix);
            }
            else
            {
                lbPrefix.Text = "não encontrei o prefixo desse servidor";
            }
        }
    }
}