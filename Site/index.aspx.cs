using ConfigurationControler.DAO;
using ConfigurationControler.Modelos;
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
            DiaConfig config = new DiaConfigDAO().Carregar();
            Servidores servFinal = new Servidores(Convert.ToUInt64(txId.Text), config.prefix.ToCharArray());
            Servidores servidores = servFinal;
            if (new ServidoresDAO().GetPrefix(ref servidores))
            {
                servFinal = servidores;
            }
            lbPrefix.Text = $"O prefixo do servidor eh: {new string(servFinal.Prefix)}";
        }
    }
}