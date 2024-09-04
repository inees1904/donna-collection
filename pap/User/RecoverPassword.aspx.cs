using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace pap.User
{
    public partial class RecoverPassword : System.Web.UI.Page
    {
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

        protected void btnUpdatePassword_Click(object sender, EventArgs e)
        {
            bool isValidToExecute = false;
            string connectionString = Utils.getConnection();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE Utilizadores SET pssword = @pssword WHERE email = @Email", con))
                {
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());

                    if (txtPassword.Text == txtPassword2.Text)
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
                        try
                        {
                            con.Open();
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                string script = "alert('Password alterada com sucesso! Vai ser redirecionado para o login...')";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", script, true);
                                Response.Redirect("Login.aspx");
                            }
                            else
                            {
                                string script = "alert('Erro ao alterar a password. Por favor, verifique se o email está correto.')";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", script, true);
                            }
                        }
                        catch (Exception ex)
                        {
                            string script = "alert('Erro: " + ex.Message + "')";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", script, true);
                        }
                    }
                }
            }
        }
    }
}