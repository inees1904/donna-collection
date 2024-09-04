using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace pap.User
{
    public partial class Acessorios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadProducts();
            }
        }

        private void LoadProducts()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

            string query = "SELECT p.produtoId, p.nomeProduto, p.preco, p.imagemUrl1 FROM Produto p WHERE p.categoria2 = 'Acessórios' AND p.isActive = 1";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    produtos.DataSource = reader;
                    produtos.DataBind();
                }
            }
        }

        protected void verDetalhes_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string productId = btn.CommandArgument;
            Response.Redirect($"ShopDetail.aspx?productId={productId}");
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            string searchTerm = searchInput.Text.Trim();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                SearchProducts(searchTerm);
            }
            else
            {
                LoadProducts();
            }
        }

        private void SearchProducts(string searchTerm)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
            string query = "SELECT p.produtoId, p.nomeProduto, p.preco, p.imagemUrl1 FROM Produto p WHERE p.categoria2 = 'Roupa' AND p.nomeProduto LIKE '%' + @searchTerm + '%' AND p.isActive = 1";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@searchTerm", searchTerm);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    produtos.DataSource = reader;
                    produtos.DataBind();
                }
            }
        }

        protected void btnWishlist_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string productId = btn.CommandArgument;

            if (Session["userId"] == null)
            {
                Response.Redirect("~/User/Login.aspx");
                return;
            }

            int userId = Convert.ToInt32(Session["userId"]);

            string connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
            string query = "INSERT INTO Wishlist (produtoId, userId, createDate) VALUES (@produtoId, @userId, GETDATE())";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@produtoId", productId);
                    cmd.Parameters.AddWithValue("@userId", userId);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Produto adicionado à wishlist com sucesso!');", true);
        }
    }
}