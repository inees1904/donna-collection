using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace pap.User
{
    public partial class Cart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userId"] == null)
            {
                string script = "alert('Inicie sessão para iniciar um carrinho de compras!')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", script, true);
                Response.Redirect("~/User/Login.aspx");
            }
            if (!IsPostBack)
            {
                LoadCartItems();
            }
        }

        private void LoadCartItems()
        {
            List<CartItem> cartItems = Session["CartItems"] as List<CartItem>;
            if (cartItems != null)
            {
                rptCartItems.DataSource = cartItems;
                rptCartItems.DataBind();

                decimal subtotal = cartItems.Sum(item => item.TotalPrice);

                ClientScript.RegisterStartupScript(this.GetType(), "updateTotal", $"document.getElementById('total').innerText = '{subtotal:C}';", true);
            }
        }

        protected void btnFinalizarPedido_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/User/Checkout.aspx");
        }

        protected void btnRemoverItem_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string[] arguments = btn.CommandArgument.Split(';');
            string productId = arguments[0];
            int sizeId = int.Parse(arguments[1]);
            int colorId = int.Parse(arguments[2]);

            List<CartItem> cartItems = Session["CartItems"] as List<CartItem>;
            if (cartItems != null)
            {
                CartItem itemToRemove = cartItems.Find(item => item.ProductId == productId && item.SizeId == sizeId && item.ColorId == colorId);
                if (itemToRemove != null)
                {
                    cartItems.Remove(itemToRemove);
                    Session["CartItems"] = cartItems;
                    LoadCartItems();
                }
            }
        }

        protected void btnDecrease_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string[] arguments = btn.CommandArgument.Split(';');
            string productId = arguments[0];
            int sizeId = int.Parse(arguments[1]);
            int colorId = int.Parse(arguments[2]);

            List<CartItem> cartItems = Session["CartItems"] as List<CartItem>;
            if (cartItems != null)
            {
                CartItem item = cartItems.Find(ci => ci.ProductId == productId && ci.SizeId == sizeId && ci.ColorId == colorId);
                if (item != null && item.Quantity > 1)
                {
                    item.Quantity--;
                    Session["CartItems"] = cartItems;
                    LoadCartItems();
                }
                else if (item.Quantity < 1)
                {
                    item.Quantity = 1;
                }
            }
        }

        protected void btnIncrease_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string[] arguments = btn.CommandArgument.Split(';');
            string productId = arguments[0];
            int sizeId = int.Parse(arguments[1]);
            int colorId = int.Parse(arguments[2]);

            List<CartItem> cartItems = Session["CartItems"] as List<CartItem>;
            if (cartItems != null)
            {
                CartItem item = cartItems.Find(ci => ci.ProductId == productId && ci.SizeId == sizeId && ci.ColorId == colorId);
                if (item != null)
                {
                    item.Quantity++;
                    Session["CartItems"] = cartItems;
                    LoadCartItems();
                }
            }
        }
    }
}