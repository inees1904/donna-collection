using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace pap.User
{
    public partial class Wishlist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userId"] == null)
            {
                Response.Redirect("~/User/Login.aspx");
                return;
            }
            if (!IsPostBack)
            {
                LoadProducts();
            }
        }

        private void LoadProducts()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
            int userId = Convert.ToInt32(Session["userId"]);
            string query = @"SELECT p.produtoId, p.nomeProduto, p.preco, p.imagemUrl1 FROM Produto p 
                            INNER JOIN Wishlist w ON p.produtoId = w.produtoId WHERE w.userId = @userId";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@userId", userId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    produtos.DataSource = reader;
                    produtos.DataBind();
                }
            }
        }

        private void SearchProducts(string searchTerm)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
            string query = "SELECT p.produtoId, p.nomeProduto, p.preco, p.imagemUrl1 FROM Produto p WHERE p.categoria2 = 'Roupa' AND p.nomeProduto LIKE '%' + @searchTerm + '%'";

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

        protected void verDetalhes_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string productId = btn.CommandArgument;
            Response.Redirect($"ShopDetail.aspx?productId={productId}");
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
            string query = "DELETE FROM Wishlist WHERE produtoId = @produtoId AND userId = @userId";

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

            LoadProducts();
        }
    }
}