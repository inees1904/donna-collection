using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace pap.User
{
    public partial class User : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userName"] != null)
                {
                    userLabel.Text = Session["userName"].ToString();
                }
            }
        }

        protected int GetTotalItensWishlist()
        {
            int userId = Convert.ToInt32(Session["userId"]);
            if (userId > 0)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
                string query = "SELECT COUNT(*) FROM Wishlist WHERE userId = @userId";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);
                        con.Open();
                        return (int)cmd.ExecuteScalar();
                    }
                }
            }
            return 0;
        }

        protected int GetTotalItensCarrinho()
        {
            List<CartItem> cartItems = Session["CartItems"] as List<CartItem>;
            if (cartItems != null)
            {
                return cartItems.Sum(item => item.Quantity);
            }
            return 0;
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (ScriptManager.GetCurrent(Page) != null && ScriptManager.GetCurrent(Page).IsInAsyncPostBack)
            {
                Page.ClientScript.RegisterForEventValidation("someUniqueID");
            }

            base.Render(writer);
        }
    }
}