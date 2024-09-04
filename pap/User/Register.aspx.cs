using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace pap.User
{
    public partial class Register : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["userId"] != null)
                {
                    //getUserDetails();
                }
                else if (Session["userId"] != null)
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            bool isValidToExecute = false;
            int userId = Convert.ToInt32(Request.QueryString["userId"]);
            con = new SqlConnection(Utils.getConnection());
            cmd = new SqlCommand("Utilizador_Crud", con);
            cmd.Parameters.AddWithValue("@Action", "INSERT");
            cmd.Parameters.AddWithValue("@userName", txtUsername.Text.Trim());
            cmd.Parameters.AddWithValue("@nome", txtNome.Text.Trim());
            cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
            cmd.Parameters.AddWithValue("@cargoId", 2);
            if(txtPassword.Text == txtConfirmaPassword.Text)
            {
                cmd.Parameters.AddWithValue("@pssword", txtPassword.Text.Trim());
                isValidToExecute = true;
            }
            else
            {
                string script = "alert('As passwords não correspondem, tente novamente.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", script, true);
            }
            if (isValidToExecute)
            {
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    string script = "alert('Registo realizado com sucesso! Vai ser redirecionado para o login...')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", script, true);
                    clear();
                    Response.Redirect("Login.aspx");
                }
                catch(SqlException ex)
                {
                    if (ex.Message.Contains("Violation of UNIQUE KEY constraint"))
                    {
                        string script = "alert('Já existe um utilizador registado com este email!')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", script, true);
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private void clear()
        {
            txtNome.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtUsername.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtConfirmaPassword.Text = string.Empty;
        }
    }
}