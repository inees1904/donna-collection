using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace pap.Admin
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["breadCumbTitle"] = "Dashboard";
            Session["breadCumbPage"] = "";
            if (!IsPostBack)
            {
                LoadCounts();
            }
        }

        private void LoadCounts()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

            lblCategorias.Text = GetCount(connectionString, "SELECT COUNT(*) FROM Categoria").ToString();
            lblCores.Text = GetCount(connectionString, "SELECT COUNT(*) FROM Cores").ToString();
            lblTamanhos.Text = GetCount(connectionString, "SELECT COUNT(*) FROM Tamanhos").ToString();
            lblPedidos.Text = GetCount(connectionString, "SELECT COUNT(*) FROM Pedidos").ToString();
            lblProdutos.Text = GetCount(connectionString, "SELECT COUNT(*) FROM Produto").ToString();
            lblUtilizadores.Text = GetCount(connectionString, "SELECT COUNT(*) FROM Utilizadores").ToString();
        }

        private int GetCount(string connectionString, string query)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    return (int)cmd.ExecuteScalar();
                }
            }
        }
    }
}