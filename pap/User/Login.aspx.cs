using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace pap.User
{
    public partial class Login : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        string userEmail;
        string userName;
        string userId;
        string nome;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["email"] != null)
            {
                Response.Redirect("Default.aspx");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string pssword = txtPassword.Text.Trim();
            conn = new SqlConnection(Utils.getConnection());
            cmd = new SqlCommand("Utilizador_Crud", conn);
            cmd.Parameters.AddWithValue("@Action", "SELECT4LOGIN");
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@pssword", pssword);
            cmd.CommandType = CommandType.StoredProcedure;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            if(dt.Rows.Count == 1)
            {
                Session["email"] = email;
                userId = dt.Rows[0]["userId"].ToString();
                Session["userId"] = userId;
                nome = dt.Rows[0]["nome"].ToString();
                Session["nome"] = nome;
                userName = dt.Rows[0]["userName"].ToString();
                Session["username"] = userName;
                int cargoId = Convert.ToInt32(dt.Rows[0]["cargoId"]);
                Session["morada"] = dt.Rows[0]["morada"].ToString();
                Session["codigoPostal"] = dt.Rows[0]["codigoPostal"].ToString();
                Session["telemovel"] = dt.Rows[0]["telemovel"].ToString();

                UpdateLastLogin(userId, conn);
                if (cargoId == 1)
                {
                    Response.Redirect("../Admin/Dashboard.aspx");
                }
                Response.Redirect("Default.aspx");
            }
            else
            {
                string script = "alert('Dados inválidos! Tente novamente...')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", script, true);
            }
        }

        private void UpdateLastLogin(string userId, SqlConnection conn)
        {
            using (SqlCommand cmdUpdate = new SqlCommand("UPDATE Utilizadores SET lastLogin = @lastLogin WHERE userId = @userId", conn))
            {
                conn.Open();
                cmdUpdate.Parameters.AddWithValue("@lastLogin", DateTime.Now);
                cmdUpdate.Parameters.AddWithValue("@userId", Convert.ToInt32(userId));
                cmdUpdate.ExecuteNonQuery();
            }
        }
    }
}