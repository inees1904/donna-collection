using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace pap.User
{
    public partial class EditProfile : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userId"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    getUserDetails();
                }
            }
        }

        void getUserDetails()
        {
            con = new SqlConnection(Utils.getConnection());
            cmd = new SqlCommand("Utilizador_Crud", con);
            cmd.Parameters.AddWithValue("@Action", "SELECT4PROFILE");
            cmd.Parameters.AddWithValue("@userId", Session["userId"]);
            cmd.CommandType = CommandType.StoredProcedure;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                var row = dt.Rows[0];
                txtNome.Text = dt.Rows[0]["nome"].ToString();
                txtUsername.Text = dt.Rows[0]["userName"].ToString();
                txtTelemovel.Text = dt.Rows[0]["telemovel"].ToString();
                txtEmail.Text = dt.Rows[0]["email"].ToString();
                txtMorada.Text = dt.Rows[0]["morada"].ToString();
                txtCodigoPostal.Text = dt.Rows[0]["codigoPostal"].ToString();
                imagePreview.ImageUrl = string.IsNullOrEmpty(row["imagemUrl"].ToString()) ? "~/Images/No_image.png" : "~/" + row["imagemUrl"].ToString();
                imagePreview.Height = 200;
                imagePreview.Width = 200;
            }
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string actionName;
                string imagePath = string.Empty;
                string fileExtension = string.Empty;
                bool isValidToExecute = false;
                int userId = Convert.ToInt32(Session["userId"]);

                using (var conn = new SqlConnection(Utils.getConnection()))
                {
                    cmd = new SqlCommand("Utilizador_Crud", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Action", SqlDbType.NVarChar).Value = "UPDATEUSER";
                    cmd.Parameters.Add("@userId", SqlDbType.Int).Value = userId;
                    cmd.Parameters.Add("@nome", SqlDbType.NVarChar).Value = txtNome.Text.Trim();
                    cmd.Parameters.Add("@userName", SqlDbType.NVarChar).Value = txtUsername.Text.Trim();
                    cmd.Parameters.Add("@telemovel", SqlDbType.NVarChar).Value = txtTelemovel.Text.Trim();
                    cmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = txtEmail.Text.Trim();
                    cmd.Parameters.Add("@morada", SqlDbType.NVarChar).Value = txtMorada.Text.Trim();
                    cmd.Parameters.Add("@codigoPostal", SqlDbType.NVarChar).Value = txtCodigoPostal.Text.Trim();

                    if (fuImagemUrl.HasFile)
                    {
                        if (Utils.isValidExtension(fuImagemUrl.FileName))
                        {
                            string newImageName = Utils.getUniqueId();
                            fileExtension = Path.GetExtension(fuImagemUrl.FileName);
                            imagePath = "Images/Users/" + newImageName.ToString() + fileExtension;
                            fuImagemUrl.PostedFile.SaveAs(Server.MapPath("~/Images/Users/" + newImageName.ToString() + fileExtension));
                            cmd.Parameters.AddWithValue("@imagemUrl", imagePath);
                            isValidToExecute = true;
                        }
                        else
                        {
                            lblmsg.Visible = false;
                            lblmsg.Text = "Please select .jpg, .png, .jpeg image";
                            lblmsg.CssClass = "alert alert-danger";
                            isValidToExecute = false;
                        }
                    }
                    else
                    {
                        isValidToExecute = true;
                    }
                    if (isValidToExecute)
                    {
                        try
                        {
                            conn.Open();
                            var str = cmd.ExecuteNonQuery();
                            actionName = userId == 0 ? "inserted" : "updated";
                            lblmsg.Visible = true;
                            lblmsg.Text = "User " + actionName + " successfully!";
                            lblmsg.CssClass = "alert alert-success";
                            Response.Redirect("UserDetails.aspx");
                        }
                        catch (Exception ex)
                        {
                            lblmsg.Visible = true;
                            lblmsg.Text = "Erro: " + ex.Message;
                            lblmsg.CssClass = "alert alert-danger";
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                }
            }
        }
    }
}