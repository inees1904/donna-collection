using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace pap.Admin
{
    public partial class Utilizadores : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateCargos();
                Session["breadCumbTitle"] = "Gerir Utilizadores";
                Session["breadCumbPage"] = "Utilizadoress";
                lblmsg.Visible = false;
                getUsers();
            }
        }

        void getUsers()
        {
            conn = new SqlConnection(Utils.getConnection());
            cmd = new SqlCommand("Utilizador_Crud", conn);
            cmd.Parameters.AddWithValue("@Action", "GETALL");
            cmd.CommandType = CommandType.StoredProcedure;
            adapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            adapter.Fill(dt);
            rUser.DataSource = dt;
            rUser.DataBind();
        }

        private void PopulateCargos()
        {
            using (var conn = new SqlConnection(Utils.getConnection()))
            {
                string query = "SELECT cargoId, nomeCargo from Cargos";
                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ListItem item = new ListItem
                            {
                                Text = reader["nomeCargo"].ToString(),
                                Value = reader["cargoId"].ToString()
                            };
                            cargoDD.Items.Add(item);
                        }
                    }
                }
            }
        }

        protected void btnAddUpdate_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string actionName;
                string imagePath = string.Empty;
                string fileExtension = string.Empty;
                bool isValidToExecute = false;
                int userId = Convert.ToInt32(hfuserId.Value);
                
                using (var conn = new SqlConnection(Utils.getConnection()))
                {
                    cmd = new SqlCommand("Utilizador_Crud", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Action", SqlDbType.NVarChar).Value = userId == 0 ? "INSERTADMIN" : "UPDATE";
                    cmd.Parameters.Add("@userId", SqlDbType.Int).Value = userId;
                    cmd.Parameters.Add("@nome", SqlDbType.NVarChar).Value = txtNome.Text.Trim();
                    cmd.Parameters.Add("@userName", SqlDbType.NVarChar).Value = txtUsername.Text.Trim();
                    cmd.Parameters.Add("@telemovel", SqlDbType.NVarChar).Value = txtTelemovel.Text.Trim();
                    cmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = txtEmail.Text.Trim();
                    cmd.Parameters.Add("@morada", SqlDbType.NVarChar).Value = txtMorada.Text.Trim();
                    cmd.Parameters.Add("@codigoPostal", SqlDbType.NVarChar).Value = txtCodigoPostal.Text.Trim();
                    cmd.Parameters.Add("@pssword", SqlDbType.NVarChar).Value = txtPassword.Text.Trim();
                    cmd.Parameters.Add("@cargoId", SqlDbType.Int).Value = cargoDD.SelectedValue;

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
                            lblmsg.Text = "Por favor selecionar imagem .jpg, .png, .jpeg";
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
                            actionName = userId == 0 ? "inserido" : "atualizado";
                            lblmsg.Visible = true;
                            lblmsg.Text = "Utilizador " + actionName + " com sucesso!";
                            lblmsg.CssClass = "alert alert-success";
                            getUsers();
                            clear();
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

        private void ShowMessage(string message, string cssClass)
        {
            lblmsg.Visible = true;
            lblmsg.Text = message;
            lblmsg.CssClass = cssClass;
        }

        void clear()
        {
            txtNome.Text = string.Empty;
            txtUsername.Text = string.Empty;
            txtTelemovel.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtMorada.Text = string.Empty;
            txtCodigoPostal.Text = string.Empty;
            txtPassword.Text = string.Empty;
            hfuserId.Value = "0";
            btnAddUpdate.Text = "Adicionar";
            imagePreview.ImageUrl = string.Empty;
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        protected void rUser_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            lblmsg.Visible = false;

            if (e.CommandName == "edit")
            {
                LoadUserForEdit(Convert.ToInt32(e.CommandArgument));
            }
            else if (e.CommandName == "delete")
            {
                DeleteUser(Convert.ToInt32(e.CommandArgument));
            }
        }

        private void LoadUserForEdit(int userId)
        {
            using (var conn = new SqlConnection(Utils.getConnection()))
            {
                using (var cmd = new SqlCommand("Utilizador_Crud", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Action", SqlDbType.NVarChar).Value = "GETBYID";
                    cmd.Parameters.Add("@userId", SqlDbType.Int).Value = userId;

                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        var dt = new DataTable();
                        adapter.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            var row = dt.Rows[0];
                            txtNome.Text = row["nome"].ToString();
                            txtUsername.Text = row["userName"].ToString();
                            txtTelemovel.Text = row["telemovel"].ToString();
                            txtEmail.Text = row["email"].ToString();
                            txtMorada.Text = row["morada"].ToString();
                            txtCodigoPostal.Text = row["codigoPostal"].ToString();
                            txtPassword.Text = row["pssword"].ToString();
                            imagePreview.ImageUrl = string.IsNullOrEmpty(row["imagemUrl"].ToString()) ? "~/Images/No_image.png" : "~/" + row["imagemUrl"].ToString();
                            cargoDD.SelectedValue = row["cargoId"].ToString();
                            hfuserId.Value = row["userId"].ToString();
                            btnAddUpdate.Text = "Atualizar";
                        }
                    }
                }
            }
        }

        private void DeleteUser(int userId)
        {
            using (var conn = new SqlConnection(Utils.getConnection()))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        using (var cmd = new SqlCommand("DELETE FROM Pedido_Items WHERE detalhesPedidoId IN (SELECT detalhesPedidoId FROM Pedidos WHERE userId = @userId)", conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@userId", userId);
                            cmd.ExecuteNonQuery();
                        }
                        using (var cmd = new SqlCommand("DELETE FROM Pedidos WHERE userId = @userId", conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@userId", userId);
                            cmd.ExecuteNonQuery();
                        }
                        using (var cmd = new SqlCommand("Utilizador_Crud", conn, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@Action", SqlDbType.NVarChar).Value = "DELETE";
                            cmd.Parameters.Add("@userId", SqlDbType.Int).Value = userId;
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        ShowMessage("Utilizador eliminado com sucesso!", "alert alert-success");
                        getUsers();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        ShowMessage($"Error: {ex.Message}", "alert alert-danger");
                    }
                }
            }
        }
    }
}