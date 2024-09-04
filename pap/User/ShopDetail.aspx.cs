using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace pap.User
{
    public partial class ShopDetail : System.Web.UI.Page
    {
        SqlCommand cmd;
        SqlConnection con;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string productId = Request.QueryString["productId"];
                LoadProductDetails(productId);
            }
        }

        private void LoadProductDetails(string productId)
        {
            if (string.IsNullOrEmpty(productId))
            {
                Response.Redirect("Roupa.aspx");
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
            string productQuery = "SELECT * FROM Produto WHERE produtoId = @ProductId";
            string sizesQuery = @"SELECT t.tamanho, t.tamanhoId FROM Produto_Tamanhos pt JOIN Tamanhos t ON pt.tamanhoId = t.tamanhoId
                                    WHERE pt.produtoId = @ProductId";
            string colorsQuery = @"SELECT c.cor, c.corId FROM Produto_Cores pc JOIN Cores c ON pc.corId = c.corId WHERE pc.produtoId = @ProductId";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(productQuery, con))
                {
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        reader.Close();
                        DataTable productTable = new DataTable();
                        productTable.Load(cmd.ExecuteReader());
                        productTable.Columns.Add("Quantity", typeof(int));

                        foreach (DataRow row in productTable.Rows)
                        {
                            row["Quantity"] = 1; 
                        }

                        produto.DataSource = productTable;
                        produto.DataBind();

                        var sizes = LoadSizes(productId, sizesQuery, con);
                        var colors = LoadColors(productId, colorsQuery, con);

                        Session["Sizes"] = sizes;
                        Session["Colors"] = colors;


                        foreach (RepeaterItem item in produto.Items)
                        {
                            RadioButtonList rblSizes = (RadioButtonList)item.FindControl("rblSizes");
                            RadioButtonList rblColors = (RadioButtonList)item.FindControl("rblColors");

                            if (rblSizes != null)
                            {
                                rblSizes.DataSource = sizes;
                                rblSizes.DataTextField = "Name";
                                rblSizes.DataValueField = "Id";
                                rblSizes.DataBind();
                                foreach (ListItem listItem in rblSizes.Items)
                                {
                                    listItem.Attributes.Add("class", "radioListHorizontal");
                                }
                            }

                            if (rblColors != null)
                            {
                                rblColors.CssClass = "radioListHorizontal"; 
                                rblColors.DataSource = colors;
                                rblColors.DataTextField = "Name";
                                rblColors.DataValueField = "Id";
                                rblColors.DataBind();
                                foreach (ListItem listItem in rblColors.Items)
                                {
                                    listItem.Attributes.Add("class", "radioListHorizontal");
                                }
                            }
                        }
                    }
                    else
                    {
                        Response.Redirect("Roupa.aspx");
                    }
                }
            }
        }

        protected void verDetalhes_Click(object sender, EventArgs e)
        {
            if (Session["userId"] == null)
            {
                string script = "alert('Inicie sessão para iniciar um carrinho de compras!')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", script, true);
                Response.Redirect("~/User/Login.aspx");
                return;
            }

            LinkButton btn = (LinkButton)sender;
            string productId = btn.CommandArgument;
            int currentQuantity = 1;
            string selectedSize = "";
            int selectedSizeId = 0;
            string selectedColor = "";
            int selectedColorId = 0;

            foreach (RepeaterItem item in produto.Items)
            {
                HiddenField hfProductId = (HiddenField)item.FindControl("hfProductId");
                if (hfProductId != null && hfProductId.Value == productId)
                {
                    Label lblQuantity = (Label)item.FindControl("lblQuantity");
                    currentQuantity = int.Parse(lblQuantity.Text);

                    RadioButtonList rblSizes = (RadioButtonList)item.FindControl("rblSizes");
                    if (rblSizes != null && rblSizes.SelectedItem != null)
                    {
                        selectedSize = rblSizes.SelectedItem.ToString();
                        selectedSizeId = int.Parse(rblSizes.SelectedItem.Value);
                    }

                    RadioButtonList rblColors = (RadioButtonList)item.FindControl("rblColors");
                    if (rblColors != null && rblColors.SelectedItem != null)
                    {
                        selectedColor = rblColors.SelectedItem.ToString();
                        selectedColorId = int.Parse(rblColors.SelectedItem.Value);
                    }

                    if (currentQuantity <= 0)
                    {
                        string script = "alert('A quantidade tem que ser superior a 0!')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", script, true);
                        return;
                    }
                    break;
                }
            }

            List<CartItem> cartItems = Session["CartItems"] as List<CartItem>;
            if (cartItems == null)
            {
                cartItems = new List<CartItem>();
                Session["CartItems"] = cartItems;
            }

            CartItem existingItem = cartItems.FirstOrDefault(item => item.ProductId == productId && item.SizeId == selectedSizeId && item.ColorId == selectedColorId);
            if (existingItem != null)
            {
                existingItem.Quantity += currentQuantity;
            }
            else
            {
                con = new SqlConnection(Utils.getConnection());

                string query = "SELECT nomeProduto, preco, imagemUrl1 FROM Produto WHERE produtoId = @produtoId";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@produtoId", productId);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string nomeProduto = reader["nomeProduto"].ToString();
                    decimal preco = Convert.ToDecimal(reader["preco"]);
                    string imagemUrl1 = reader["imagemUrl1"].ToString();
                    cartItems.Add(new CartItem(productId, imagemUrl1, nomeProduto, preco, currentQuantity, selectedSize, selectedSizeId, selectedColor, selectedColorId));
                }
                reader.Close();
            }
            Session["CartItems"] = cartItems;
        }

        protected void btnDecrease_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string productId = btn.CommandArgument;
            UpdateProductQuantityInRepeater(productId, -1);
        }

        protected void btnIncrease_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string productId = btn.CommandArgument;
            UpdateProductQuantityInRepeater(productId, 1);
        }

        private void UpdateProductQuantityInRepeater(string productId, int quantityChange)
        {
            foreach (RepeaterItem item in produto.Items)
            {
                HiddenField hfProductId = (HiddenField)item.FindControl("hfProductId");
                if (hfProductId != null && hfProductId.Value == productId)
                {
                    Label lblQuantity = (Label)item.FindControl("lblQuantity");
                    if (lblQuantity != null)
                    {
                        int currentQuantity = int.Parse(lblQuantity.Text);
                        currentQuantity += quantityChange;
                        if (currentQuantity < 1)
                        {
                            currentQuantity = 1; 
                        }
                        lblQuantity.Text = currentQuantity.ToString();
                    }
                    break;
                }
            }
        }

        private List<Size> LoadSizes(string productId, string sizesQuery, SqlConnection con)
        {
            List<Size> sizes = new List<Size>();
            using (SqlCommand cmd = new SqlCommand(sizesQuery, con))
            {
                cmd.Parameters.AddWithValue("@ProductId", productId);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        sizes.Add(new Size { Id = (int)reader["tamanhoId"], Name = reader["tamanho"].ToString() });
                    }
                }
            }
            return sizes;
        }

        private List<Color> LoadColors(string productId, string colorsQuery, SqlConnection con)
        {
            List<Color> colors = new List<Color>();
            using (SqlCommand cmd = new SqlCommand(colorsQuery, con))
            {
                cmd.Parameters.AddWithValue("@ProductId", productId);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        colors.Add(new Color { Id = (int)reader["corId"], Name = reader["cor"].ToString() });
                    }
                }
            }
            return colors;
        }
    }

    public class Size
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Color
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}