using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace pap.Admin
{
    public partial class Sizes : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        SqlDataReader reader;
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["breadCumbTitle"] = "Gerir Tamanhos";
            Session["breadCumbPage"] = "Tamanhos";
            lblmsg.Visible = false;
            getSizes();
        }

        void getSizes()
        {
            conn = new SqlConnection(Utils.getConnection());
            cmd = new SqlCommand("SELECT * FROM Tamanhos", conn);
            adapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            adapter.Fill(dt);
            rSizes.DataSource = dt;
            rSizes.DataBind();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        protected void btnAddUpdate_Click(object sender, EventArgs e)
        {
            string actionName = string.Empty;
            int tamanhoId = Convert.ToInt32(hfs.Value);
            conn = new SqlConnection(Utils.getConnection());
            cmd = new SqlCommand("Tamanho_Crud", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", tamanhoId == 0 ? "INSERT" : "UPDATE");
            cmd.Parameters.AddWithValue("@tamanhoId", tamanhoId);
            cmd.Parameters.AddWithValue("@tamanho", txtTamanho.Text.Trim());

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                actionName = tamanhoId == 0 ? "inserido" : "atualizado";
                lblmsg.Visible = true;
                lblmsg.Text = "Tamanho " + actionName + " com sucesso!";
                lblmsg.CssClass = "alert alert-success";
                getSizes();
                clear();
            }
            catch (Exception ex)
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Error: " + ex.Message;
                lblmsg.CssClass = "alert alert-danger";
            }
            finally
            {
                conn.Close();
            }
        }

        void clear()
        {
            txtTamanho.Text = string.Empty;
            hfs.Value = "0";
            btnAddUpdate.Text = "Adicionar";
        }

        protected void rSizes_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "edit")
            {
                int tamanhoId = Convert.ToInt32(e.CommandArgument);
                conn = new SqlConnection(Utils.getConnection());
                cmd = new SqlCommand("SELECT * FROM Tamanhos WHERE tamanhoId = @tamanhoId", conn);
                cmd.Parameters.AddWithValue("@tamanhoId", tamanhoId);
                btnAddUpdate.Text = "Update";

                try
                {
                    conn.Open();
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtTamanho.Text = reader["tamanho"].ToString();
                        hfs.Value = reader["tamanhoId"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "Error: " + ex.Message;
                    lblmsg.CssClass = "alert alert-danger";
                }
                finally
                {
                    conn.Close();
                }
            }
            else if (e.CommandName == "delete")
            {
                int tamanhoId = Convert.ToInt32(e.CommandArgument);
                conn = new SqlConnection(Utils.getConnection());
                cmd = new SqlCommand("DELETE FROM Tamanhos WHERE tamanhoId = @tamanhoId", conn);
                cmd.Parameters.AddWithValue("@tamanhoId", tamanhoId);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    lblmsg.Visible = true;
                    lblmsg.Text = "Tamanho eliminado com sucesso!";
                    lblmsg.CssClass = "alert alert-success";
                    getSizes();
                }
                catch (Exception ex)
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "Error: " + ex.Message;
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