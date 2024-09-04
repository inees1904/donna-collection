using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace pap.Admin
{
    public partial class Category : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        SqlDataReader reader;
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["breadCumbTitle"] = "Gerir Categorias";
            Session["breadCumbPage"] = "Categoria";
            lblmsg.Visible = false;
            getCategories();
        }

        void getCategories()
        {
            conn = new SqlConnection(Utils.getConnection());
            cmd = new SqlCommand("Categoria_Crud", conn);
            cmd.Parameters.AddWithValue("@Action", "GETALL");
            cmd.CommandType = CommandType.StoredProcedure;
            adapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            adapter.Fill(dt);
            rCategory.DataSource = dt;
            rCategory.DataBind();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        protected void btnAddUpdate_Click(object sender, EventArgs e)
        {
            string actionName = string.Empty;
            string imagePath = string.Empty;
            string fileExtension = string.Empty;
            bool isValidToExecute = false;
            int categoriaId = Convert.ToInt32(hfCategoriaId.Value);
            conn = new SqlConnection(Utils.getConnection());
            cmd = new SqlCommand("Categoria_Crud", conn);
            cmd.Parameters.AddWithValue("@Action", categoriaId == 0 ? "INSERT" : "UPDATE");
            cmd.Parameters.AddWithValue("@categoriaId", categoriaId);
            cmd.Parameters.AddWithValue("@nomeSubCategoria", txtCategoriaNome.Text.Trim());
            cmd.Parameters.AddWithValue("@isActive", cbIsActive.Checked);
            cmd.Parameters.AddWithValue("@tipoCategoria", txtTipoCategoria.Text.Trim());
            if (fuCategoryImage.HasFile)
            {
                if (Utils.isValidExtension(fuCategoryImage.FileName))
                {
                    string newImageName = Utils.getUniqueId();
                    fileExtension = Path.GetExtension(fuCategoryImage.FileName);
                    imagePath = "Images/Category/" + newImageName.ToString() + fileExtension;
                    fuCategoryImage.PostedFile.SaveAs(Server.MapPath("~/Images/Category/" + newImageName.ToString() + fileExtension));
                    cmd.Parameters.AddWithValue("@imagemCategoria", imagePath);
                    isValidToExecute = true;
                }
                else
                {
                    lblmsg.Visible = false;
                    lblmsg.Text = "Por favor selecionar uma imagem .jpg, .png, .jpeg";
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
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    conn.Open();
                    var str = cmd.ExecuteNonQuery();
                    actionName = categoriaId == 0 ? "inserida" : "atualizada";
                    lblmsg.Visible = true;
                    lblmsg.Text = "Categoria " + actionName + " com sucesso!";
                    lblmsg.CssClass = "alert alert-success";
                    getCategories();
                    clear();
                }
                catch(Exception ex)
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

        void clear()
        {
            txtCategoriaNome.Text = string.Empty;
            cbIsActive.Checked = false;
            hfCategoriaId.Value = "0";
            btnAddUpdate.Text = "Adicionar";
            imagePreview.ImageUrl = string.Empty;
            txtTipoCategoria.Text = string.Empty;
        }

        protected void rCategory_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            lblmsg.Visible = false;
            if(e.CommandName == "edit")
            {
                conn = new SqlConnection(Utils.getConnection());
                cmd = new SqlCommand("Categoria_Crud", conn);
                cmd.Parameters.AddWithValue("@Action", "GETBYID");
                cmd.Parameters.AddWithValue("@categoriaId", e.CommandArgument);
                cmd.CommandType = CommandType.StoredProcedure;
                adapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                txtCategoriaNome.Text = dt.Rows[0]["nomeSubCategoria"].ToString();  
                txtTipoCategoria.Text = dt.Rows[0]["tipoCategoria"].ToString();
                cbIsActive.Checked = Convert.ToBoolean(dt.Rows[0]["isActive"]);
                imagePreview.ImageUrl = string.IsNullOrEmpty(dt.Rows[0]["imagemCategoria"].ToString()) ? "../Images/No_image.png" : "../" + dt.Rows[0]["imagemCategoria"].ToString();
                imagePreview.Height = 200;
                imagePreview.Width = 200;
                hfCategoriaId.Value = dt.Rows[0]["categoriaId"].ToString();
                btnAddUpdate.Text = "Editar";
            }
            else if(e.CommandName == "delete")
            {
                conn = new SqlConnection(Utils.getConnection());
                cmd = new SqlCommand("Categoria_Crud", conn);
                cmd.Parameters.AddWithValue("@Action", "DELETE");
                cmd.Parameters.AddWithValue("@categoriaId", e.CommandArgument);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    lblmsg.Visible = true;
                    lblmsg.Text = "Categoria eliminada com sucesso!";
                    lblmsg.CssClass = "alert alert-success";
                    getCategories();
                }
                catch (Exception ex)
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "Erro: " + ex.Message;
                    lblmsg.CssClass = "alert alert-danger";
                }
                finally
                {
                    conn?.Close();
                }
            }
        }
    }
}