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
    public partial class Colors : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        SqlDataReader reader;
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["breadCumbTitle"] = "Gerir Cores";
            Session["breadCumbPage"] = "Cores";
            lblmsg.Visible = false;
            getColours();
        }

        void getColours()
        {
            conn = new SqlConnection(Utils.getConnection());
            cmd = new SqlCommand("SELECT * FROM Cores", conn);
            adapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            adapter.Fill(dt);
            rCategory.DataSource = dt;
            rCategory.DataBind();
        }

        protected void btnAddUpdate_Click(object sender, EventArgs e)
        {
            string actionName = string.Empty;
            int corId = Convert.ToInt32(hfc.Value);
            conn = new SqlConnection(Utils.getConnection());
            cmd = new SqlCommand("Cor_Crud", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", corId == 0 ? "INSERT" : "UPDATE");
            cmd.Parameters.AddWithValue("@corId", corId);
            cmd.Parameters.AddWithValue("@cor", txtCor.Text.Trim());

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                actionName = corId == 0 ? "inserida" : "atualizada";
                lblmsg.Visible = true;
                lblmsg.Text = "Cor " + actionName + " com sucesso!";
                lblmsg.CssClass = "alert alert-success";
                getColours();
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

        protected void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        void clear()
        {
            txtCor.Text = string.Empty;
            hfc.Value = "0";
            btnAddUpdate.Text = "Adicionar";
        }

        protected void rCategory_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "edit")
            {
                int corId = Convert.ToInt32(e.CommandArgument);
                conn = new SqlConnection(Utils.getConnection());
                cmd = new SqlCommand("SELECT * FROM Cores WHERE corId = @corId", conn);
                cmd.Parameters.AddWithValue("@corId", corId);
                btnAddUpdate.Text = "Atualizar";

                try
                {
                    conn.Open();
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtCor.Text = reader["cor"].ToString();
                        hfc.Value = reader["corId"].ToString();
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
                int corId = Convert.ToInt32(e.CommandArgument);
                conn = new SqlConnection(Utils.getConnection());
                cmd = new SqlCommand("DELETE FROM Cores WHERE corId = @corId", conn);
                cmd.Parameters.AddWithValue("@corId", corId);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    lblmsg.Visible = true;
                    lblmsg.Text = "Cor eliminada com sucesso!";
                    lblmsg.CssClass = "alert alert-success";
                    getColours();
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