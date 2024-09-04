using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace pap.User
{
    public partial class UserDetails : System.Web.UI.Page
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
                    getPedidos();
                }
            }
        }

        void getPedidos()
        {
            con = new SqlConnection(Utils.getConnection());
            cmd = new SqlCommand("SELECT p.detalhesPedidoId, p.pedidoN, p.dataPedido, p.total, p.metodoPagamento, e.estado, p.linkRastreio " +
                "FROM Pedidos p JOIN Estado e ON p.estado = e.estadoId WHERE p.userId = @userId AND p.isCancel = 0", con);
            cmd.Parameters.AddWithValue("@userId", Session["userId"]);
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            rPedidos.DataSource = dt;
            rPedidos.DataBind();
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
            rUserProfile.DataSource = dt;
            rUserProfile.DataBind();
            if(dt.Rows.Count == 1)
            {
                Session["nome"] = dt.Rows[0]["nome"].ToString();
                Session["userName"] = dt.Rows[0]["userName"].ToString();
                Session["telemovel"] = dt.Rows[0]["telemovel"].ToString();
                Session["email"] = dt.Rows[0]["email"].ToString();
                Session["morada"] = dt.Rows[0]["morada"].ToString();
                Session["codigoPostal"] = dt.Rows[0]["codigoPostal"].ToString();
                Session["imagemUrl"] = dt.Rows[0]["imagemUrl"].ToString();
                Session["createDate"] = dt.Rows[0]["createDate"].ToString();
            }
        }

        protected void rPedidos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string pedidoN = e.CommandArgument.ToString();
            switch (e.CommandName)
            {
                case "detalhes":
                    ShowPedidoDetalhes(pedidoN);
                    break;
                case "pdf":
                    DownloadPdf(pedidoN);
                    break;
                case "delete":
                    CancelPedido(pedidoN);
                    break;
                case "rastreio":
                    Response.Redirect(e.CommandArgument.ToString());
                    break;
            }
        }

        private void ShowPedidoDetalhes(string pedidoN)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT pi.produtoId, p.nomeProduto, pi.quantidade, pi.preco, pi.tamanhoId, pi.corId, t.tamanho, c.cor " +
                        "FROM Pedido_Items pi " +
                        "INNER JOIN Produto p ON pi.produtoId = p.produtoId " +
                        "LEFT JOIN Tamanhos t ON pi.tamanhoId = t.tamanhoId " +
                        "LEFT JOIN Cores c ON pi.corId = c.corId " +
                        "INNER JOIN Pedidos ped ON pi.detalhesPedidoId = ped.detalhesPedidoId " +  
                        "WHERE ped.pedidoN = @pedidoN"; 

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@pedidoN", pedidoN);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    gvOrderDetails.DataSource = reader;
                    gvOrderDetails.DataBind();
                }
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openDetailsModal();", true);
        }

        private void DownloadPdf(string pedidoN)
        {
            string filePath = Server.MapPath($"~/Pedidos/{pedidoN}.pdf");
            if (File.Exists(filePath))
            {
                Response.ContentType = "application/pdf";
                Response.AppendHeader("Content-Disposition", $"attachment; filename={pedidoN}.pdf");
                Response.TransmitFile(filePath);
                Response.End();
            }
            else
            {
                string script = "alert('O PDF deste pedido não foi encontrado!')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", script, true);
            }
        }

        private void CancelPedido(string pedidoN)
        {
            con = new SqlConnection(Utils.getConnection());
            cmd = new SqlCommand("UPDATE Pedidos SET isCancel = 1 WHERE pedidoN = @pedidoN", con);
            cmd.Parameters.AddWithValue("@pedidoN", pedidoN);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            getPedidos(); 
        }
    }
}