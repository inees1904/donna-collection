using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Threading.Tasks;
using pap.User;
using Syncfusion.Pdf.Graphics;

namespace pap.Admin
{
    public partial class Orders : System.Web.UI.Page 
    { 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ConfigurarSqlDataSource();
                BindOrders();
                Session["breadCumbTitle"] = "Gerir Pedidos";
                Session["breadCumbPage"] = "Pedidos";
            }
        }

        private void ConfigurarSqlDataSource()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
            SqlDataSourceEstado.ConnectionString = connectionString;
        }

        protected void rOrders_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "update")
            {
                int pedidoId = Convert.ToInt32(e.CommandArgument);
                DropDownList ddlEstado = (DropDownList)e.Item.FindControl("ddlEstado");
                TextBox txtLinkRastreio = (TextBox)e.Item.FindControl("txtLinkRastreio");
                int novoEstadoId = Convert.ToInt32(ddlEstado.SelectedValue);
                string linkRastreio = txtLinkRastreio.Text.Trim();

                AtualizarEstadoPedido(pedidoId, novoEstadoId, linkRastreio);
                lblmsg.Text = "Estado do pedido alterado com sucesso";
            }
            else if (e.CommandName == "viewAddress")
            {
                int pedidoId = Convert.ToInt32(e.CommandArgument);
                ExibirMoradaPedido(pedidoId);
            }
            else if (e.CommandName == "viewDetails")
            {
                int pedidoId = Convert.ToInt32(e.CommandArgument);
                ExibirDetalhesPedido(pedidoId);
            }
            else if(e.CommandName == "PDF")
            {
                string pedidoN = e.CommandArgument.ToString();
                DownloadPDF(pedidoN);
            }
        }

        private void ExibirDetalhesPedido(int pedidoId)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT pi.produtoId, p.nomeProduto, pi.quantidade, pi.preco, pi.tamanhoId, pi.corId, t.tamanho, c.cor " +
                "FROM Pedido_Items pi " +
                "INNER JOIN Produto p ON pi.produtoId = p.produtoId " +
                "LEFT JOIN Tamanhos t ON pi.tamanhoId = t.tamanhoId " +
                "LEFT JOIN Cores c ON pi.corId = c.corId " +
                "WHERE pi.detalhesPedidoId = @pedidoId";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@pedidoId", pedidoId);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    gvOrderDetails.DataSource = reader;
                    gvOrderDetails.DataBind();
                }
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openDetailsModal();", true);
        }

        private void ExibirMoradaPedido(int pedidoId)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"SELECT nomeFaturacao, telemovelFaturacao, emailFaturacao, moradaFaturacao, codPostalFaturacao, nifFaturacao, 
                                nomeEnvio, telemovelEnvio, moradaEnvio, codPostalEnvio FROM Pedidos WHERE detalhesPedidoId = @pedidoId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@pedidoId", pedidoId);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        lblNomeFaturacao.Text = reader["nomeFaturacao"].ToString();
                        lblTelemovelFaturacao.Text = reader["telemovelFaturacao"].ToString();
                        lblEmailFaturacao.Text = reader["emailFaturacao"].ToString();
                        lblMoradaFaturacao.Text = reader["moradaFaturacao"].ToString();
                        lblCodPostalFaturacao.Text = reader["codPostalFaturacao"].ToString();
                        lblNifFaturacao.Text = reader["nifFaturacao"].ToString();

                        lblNomeEnvio.Text = reader["nomeEnvio"].ToString();
                        lblTelemovelEnvio.Text = reader["telemovelEnvio"].ToString();
                        lblMoradaEnvio.Text = reader["moradaEnvio"].ToString();
                        lblCodPostalEnvio.Text = reader["codPostalEnvio"].ToString();
                    }
                    con.Close();
                }
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openAddressModal();", true);
        }

        private void BindOrders()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
            DataTable table = new DataTable();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT detalhesPedidoId, pedidoN, nomeFaturacao, estado, dataPedido, total, moradaEnvio, linkRastreio FROM Pedidos";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    table.Load(reader);
                    reader.Close();
                    rOrders.DataSource = table;
                    rOrders.DataBind();
                }
            }
        }

        private void AtualizarEstadoPedido(int pedidoId, int estadoId, string linkRastreio)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "UPDATE Pedidos SET estado = @estado, linkRastreio = @linkRastreio WHERE detalhesPedidoId = @pedidoId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@estado", estadoId);
                    cmd.Parameters.AddWithValue("@linkRastreio", linkRastreio);
                    cmd.Parameters.AddWithValue("@pedidoId", pedidoId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            Task.Run(() => EnviarEmailNotificacao(pedidoId));
        }

        private void DownloadPDF(string pedidoN)
        {
            string fileName = $"{pedidoN}.pdf";
            string filePath = Server.MapPath($"~/Pedidos/{fileName}");

            if (File.Exists(filePath))
            {
                Response.ContentType = "application/pdf";
                Response.AppendHeader("Content-Disposition", $"attachment; filename={fileName}");
                Response.TransmitFile(filePath);
                Response.End();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('PDF não encontrado.');", true);
            }
        }

        private async Task EnviarEmailNotificacao(int pedidoId)
        {
            string emailCliente = ""; 
            string novoEstado = ""; 

            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["cs"].ConnectionString))
            {
                string query = "SELECT p.emailFaturacao, e.estado FROM Pedidos p INNER JOIN Estado e " +
                    "ON p.estado = e.estadoId WHERE p.detalhesPedidoId = @pedidoId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@pedidoId", pedidoId);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        emailCliente = reader["emailFaturacao"].ToString();
                        novoEstado = reader["estado"].ToString();
                    }
                    con.Close();
                }
            }

            if (!string.IsNullOrEmpty(emailCliente))
            {
                string subject = "Atualização do Pedido";
                string body = $"O estado do seu pedido foi atualizado para: {novoEstado}. <br/><br/>Com amor, Donna Collection ❤️";

                SmtpClient client = new SmtpClient("smtp-mail.outlook.com")
                {
                    Port = 587,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("no-replydonnacollection@outlook.pt", "DonnaCollection"),
                    EnableSsl = true,
                };

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress("no-replydonnacollection@outlook.pt"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                };

                try
                {
                    mailMessage.To.Add(emailCliente);

                    await client.SendMailAsync(mailMessage);
                    Console.WriteLine("Email enviado com sucesso!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
    }
}