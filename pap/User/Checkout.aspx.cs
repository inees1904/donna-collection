using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net.Mail;
using System.Reflection;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System.Threading.Tasks;
using System.Net;

namespace pap.User
{
    public partial class Checkout : System.Web.UI.Page
    {
        decimal subtotalValue;
        decimal envioValue = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                txtEmail.Text = Session["email"].ToString();
                txtNome.Text = Session["nome"].ToString();
                txtMorada.Text = Session["morada"].ToString();
                txtCodPostal.Text = Session["codigoPostal"].ToString();
                txtTelemovel.Text = Session["telemovel"].ToString();
                GenerateCartItemsHtml();
            }
        }
        private void GenerateCartItemsHtml()
        {
            List<CartItem> cartItems = Session["CartItems"] as List<CartItem>;
            if (cartItems != null)
            {
                string cartHtml = "";
                foreach (var item in cartItems)
                {
                    cartHtml += $"<div class='cart-item'>" +
                                $"{item.ProductName} - Tamanho: {item.Size}, Cor: {item.Color} - {item.TotalPrice:C}" +
                                $"</div>";
                }
                cartItemsLiteral.Text = cartHtml;
                subtotalValue = cartItems.Sum(item => item.TotalPrice);
                lbltotal.Text = (subtotalValue + envioValue).ToString() + "€";
                lblenvio.Text = "0€"; 
                subtotal.Text = subtotalValue.ToString() + "€";
            }
        }

        protected void btnFinalizar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNome.Text) || string.IsNullOrEmpty(txtEmail.Text) ||
                string.IsNullOrEmpty(txtTelemovel.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Preencha todos os campos para a faturação!');", true);
                return;
            }

            if(rbMoradaDiferente.Checked && rbDinheiro.Checked || rbMoradaFaturacao.Checked && rbDinheiro.Checked)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('O pagamento em dinheiro é exclusivo para levantamento da encomenda em loja!');", true);
                return;
            }

            if(rbMoradaDiferente.Checked && string.IsNullOrEmpty(txtNomeEnvio.Text) || rbMoradaDiferente.Checked && string.IsNullOrEmpty(txtMoradaEnvio.Text) ||
                rbMoradaDiferente.Checked && string.IsNullOrEmpty(txtTelemovelEnvio.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Preencha os dados de envio!');", true);
                return;
            }

            if(!rbLoja.Checked && !rbMoradaDiferente.Checked && !rbMoradaFaturacao.Checked)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Selecione um método de envio!');", true);
                return;
            }

            if (!rbDinheiro.Checked && !rbMbWay.Checked)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Selecione um método de envio!');", true);
                return;
            }

            string metodoEnvio = rbLoja.Checked ? "Loja" : rbMoradaFaturacao.Checked ? "Faturacao" : "Diferente";
            string metodoPagamento = rbDinheiro.Checked ? "Dinheiro" : rbMbWay.Checked ? "MBWay" : "Transferencia Bancaria";

            Pedido novoPedido = new Pedido
            {
                PedidoN = GeneratePedidoNumber(),
                UserId = int.Parse(Session["userId"].ToString()),
                Estado = 1,
                DataPedido = DateTime.Now,
                IsCancel = false,
                Total = CalculateTotal(),
                MetodoPagamento = metodoPagamento,
                NomeFaturacao = txtNome.Text,
                EmailFaturacao = txtEmail.Text,
                TelemovelFaturacao = txtTelemovel.Text,
                MoradaFaturacao = txtMorada.Text,
                CodPostalFaturacao = txtCodPostal.Text,
                NifFaturacao = txtNif.Text,
                NomeEnvio = metodoEnvio == "Diferente" ? txtNomeEnvio.Text : (metodoEnvio == "Loja" ? "Loja" : txtNome.Text),
                TelemovelEnvio = metodoEnvio == "Diferente" ? txtTelemovelEnvio.Text : (metodoEnvio == "Loja" ? "Loja" : txtTelemovel.Text),
                MoradaEnvio = metodoEnvio == "Diferente" ? txtMoradaEnvio.Text : (metodoEnvio == "Loja" ? "Loja" : txtMorada.Text),
                CodPostalEnvio = metodoEnvio == "Diferente" ? txtCodPostalEnvio.Text : (metodoEnvio == "Loja" ? "Loja" : txtCodPostal.Text),
            };

            using (var con = new SqlConnection(Utils.getConnection()))
            {
                string query = "INSERT INTO Pedidos(pedidoN, userId, estado, dataPedido, isCancel, total, metodoPagamento, nomeFaturacao, emailFaturacao, telemovelFaturacao, moradaFaturacao, codPostalFaturacao, nifFaturacao, nomeEnvio, telemovelEnvio, moradaEnvio, codPostalEnvio) " +
                    "VALUES(@pedidoN, @userId, @estado, @dataPedido, @isCancel, @total, @metodoPagamento, @nomeFaturacao, @emailFaturacao, @telemovelFaturacao, @moradaFaturacao, @codPostalFaturacao, @nifFaturacao, @nomeEnvio, @telemovelEnvio, @moradaEnvio, @codPostalEnvio); " +
                    "SELECT SCOPE_IDENTITY()";
                using(var cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@pedidoN", novoPedido.PedidoN);
                    cmd.Parameters.AddWithValue("@userId", novoPedido.UserId);
                    cmd.Parameters.AddWithValue("@estado", novoPedido.Estado);
                    cmd.Parameters.AddWithValue("@dataPedido", novoPedido.DataPedido);
                    cmd.Parameters.AddWithValue("@isCancel", novoPedido.IsCancel);
                    cmd.Parameters.AddWithValue("@total", novoPedido.Total);
                    cmd.Parameters.AddWithValue("@metodoPagamento", novoPedido.MetodoPagamento);
                    cmd.Parameters.AddWithValue("@nomeFaturacao", novoPedido.NomeFaturacao);
                    cmd.Parameters.AddWithValue("@emailFaturacao", novoPedido.EmailFaturacao);
                    cmd.Parameters.AddWithValue("@telemovelFaturacao", novoPedido.TelemovelFaturacao);
                    cmd.Parameters.AddWithValue("@moradaFaturacao", novoPedido.MoradaFaturacao);
                    cmd.Parameters.AddWithValue("@codPostalFaturacao", novoPedido.CodPostalFaturacao);
                    cmd.Parameters.AddWithValue("@nifFaturacao", novoPedido.NifFaturacao);
                    cmd.Parameters.AddWithValue("@nomeEnvio", novoPedido.NomeEnvio);
                    cmd.Parameters.AddWithValue("@telemovelEnvio", novoPedido.TelemovelEnvio);
                    cmd.Parameters.AddWithValue("@moradaEnvio", novoPedido.MoradaEnvio);
                    cmd.Parameters.AddWithValue("@codPostalEnvio", novoPedido.CodPostalEnvio);

                    con.Open();
                    var pedidoId = cmd.ExecuteScalar();
                    novoPedido.DetalhesPedidoId = Convert.ToInt32(pedidoId);
                }

                List<CartItem> cartItems = Session["CartItems"] as List<CartItem>;
                if (cartItems != null)
                {
                    foreach (var item in cartItems)
                    {
                        string itemQuery = "INSERT INTO Pedido_Items(detalhesPedidoId, produtoId, quantidade, preco, tamanhoId, corId) " +
                                           "VALUES(@DetalhesPedidoId, @ProdutoId, @Quantidade, @Preco, @TamanhoId, @CorId)";
                        using (var itemCmd = new SqlCommand(itemQuery, con))
                        {
                            itemCmd.Parameters.AddWithValue("@DetalhesPedidoId", novoPedido.DetalhesPedidoId);
                            itemCmd.Parameters.AddWithValue("@ProdutoId", item.ProductId);
                            itemCmd.Parameters.AddWithValue("@Quantidade", item.Quantity);
                            itemCmd.Parameters.AddWithValue("@Preco", item.Price);
                            itemCmd.Parameters.AddWithValue("@TamanhoId", item.SizeId);
                            itemCmd.Parameters.AddWithValue("@CorId", item.ColorId);
                            itemCmd.ExecuteNonQuery();
                        }

                        string updateQuery = "UPDATE Produto SET quantidade = quantidade - @Quantidade WHERE produtoId = @ProdutoId";
                        using (var updateCmd = new SqlCommand(updateQuery, con))
                        {
                            updateCmd.Parameters.AddWithValue("@Quantidade", item.Quantity);
                            updateCmd.Parameters.AddWithValue("@ProdutoId", item.ProductId);
                            updateCmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            string pdfPath = GeneratePDF(novoPedido, Session["CartItems"] as List<CartItem>);

            Task.Run(() => SendEmailWithPDF(txtEmail.Text, pdfPath, novoPedido.PedidoN));

            Session["CartItems"] = null;

            Response.Redirect("~/User/Confirmation.aspx");
        }
        private string GeneratePDF(Pedido pedido, List<CartItem> cartItems)
        {
            string pdfFilePath = Server.MapPath("~/Pedidos/" + pedido.PedidoN + ".pdf");

            using (PdfDocument document = new PdfDocument())
            {
                document.Info.Title = "Pedido " + pedido.PedidoN;

                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);

                XFont fontTitle = new XFont("Arial", 20, XFontStyle.Bold);
                XFont fontRegular = new XFont("Arial", 12, XFontStyle.Regular);
                XFont fontBold = new XFont("Arial", 12, XFontStyle.Bold);

                string logoPath = Server.MapPath("~/UserTemplate/img/i.png");
                XImage logo = XImage.FromFile(logoPath);
                double logoWidth = 150; 
                double logoHeight = logoWidth * logo.PixelHeight / logo.PixelWidth;
                double logoX = (page.Width - logoWidth) / 2;
                double logoY = 40;
                gfx.DrawImage(logo, logoX, logoY, logoWidth, logoHeight);


                gfx.DrawString("Detalhes do Pedido", fontTitle, XBrushes.Black,
                    new XRect(0, logoY + logoHeight + 20, page.Width, 50),
                    XStringFormats.Center);

                int infoXLeft = 40;
                int infoYStart = (int)(logoY + logoHeight + 80);
                gfx.DrawString("Informações de Faturação", fontBold, XBrushes.Black,
                    new XRect(infoXLeft, infoYStart, 200, 20),
                    XStringFormats.TopLeft);
                infoYStart += 30;
                gfx.DrawString($"Nome: {pedido.NomeFaturacao}", fontRegular, XBrushes.Black,
                    new XRect(infoXLeft, infoYStart, 200, 20),
                    XStringFormats.TopLeft);
                infoYStart += 20;
                gfx.DrawString($"Email: {pedido.EmailFaturacao}", fontRegular, XBrushes.Black,
                    new XRect(infoXLeft, infoYStart, 200, 20),
                    XStringFormats.TopLeft);
                infoYStart += 20;
                gfx.DrawString($"Telemóvel: {pedido.TelemovelFaturacao}", fontRegular, XBrushes.Black,
                    new XRect(infoXLeft, infoYStart, 200, 20),
                    XStringFormats.TopLeft);
                infoYStart += 20;
                gfx.DrawString($"Morada: {pedido.MoradaFaturacao}", fontRegular, XBrushes.Black,
                    new XRect(infoXLeft, infoYStart, 200, 20),
                    XStringFormats.TopLeft);
                infoYStart += 20;
                gfx.DrawString($"Código Postal: {pedido.CodPostalFaturacao}", fontRegular, XBrushes.Black,
                    new XRect(infoXLeft, infoYStart, 200, 20),
                    XStringFormats.TopLeft);

                int infoXRight = (int)(page.Width - 240);
                infoYStart = (int)(logoY + logoHeight + 80);
                gfx.DrawString("Informações de Envio", fontBold, XBrushes.Black,
                    new XRect(infoXRight, infoYStart, 200, 20),
                    XStringFormats.TopLeft);
                infoYStart += 30;
                gfx.DrawString($"Nome: {pedido.NomeEnvio}", fontRegular, XBrushes.Black,
                    new XRect(infoXRight, infoYStart, 200, 20),
                    XStringFormats.TopLeft);
                infoYStart += 20;
                gfx.DrawString($"Telemóvel: {pedido.TelemovelEnvio}", fontRegular, XBrushes.Black,
                    new XRect(infoXRight, infoYStart, 200, 20),
                    XStringFormats.TopLeft);
                infoYStart += 20;
                gfx.DrawString($"Morada: {pedido.MoradaEnvio}", fontRegular, XBrushes.Black,
                    new XRect(infoXRight, infoYStart, 200, 20),
                    XStringFormats.TopLeft);
                infoYStart += 20;
                gfx.DrawString($"Código Postal: {pedido.CodPostalEnvio}", fontRegular, XBrushes.Black,
                    new XRect(infoXRight, infoYStart, 200, 20),
                    XStringFormats.TopLeft);

                int tableX = 40;
                int tableY = infoYStart + 80;
                const int nameWidth = 200; 
                const int colWidth = 100;
                const int rowHeight = 20;
                const int imgSize = 40;

                gfx.DrawString("Produto", fontBold, XBrushes.Black,
                    new XRect(tableX, tableY, nameWidth, rowHeight),
                    XStringFormats.TopLeft);
                gfx.DrawString("Preço", fontBold, XBrushes.Black,
                    new XRect(tableX + nameWidth, tableY, colWidth, rowHeight),
                    XStringFormats.TopLeft);
                gfx.DrawString("Tamanho", fontBold, XBrushes.Black,
                    new XRect(tableX + nameWidth + colWidth, tableY, colWidth, rowHeight),
                    XStringFormats.TopLeft);
                gfx.DrawString("Cor", fontBold, XBrushes.Black,
                    new XRect(tableX + nameWidth + 2 * colWidth, tableY, colWidth, rowHeight),
                    XStringFormats.TopLeft);

                tableY += 30;

                foreach (var item in cartItems)
                {
                    XImage productImage = XImage.FromFile(Server.MapPath(Utils.getImageUrl(item.ImagemUrl1)));
                    gfx.DrawImage(productImage, tableX, tableY, imgSize, imgSize);

                    gfx.DrawString(item.ProductName, fontRegular, XBrushes.Black,
                        new XRect(tableX + imgSize + 10, tableY, nameWidth, rowHeight),
                        XStringFormats.TopLeft);
                    gfx.DrawString(item.TotalPrice.ToString("C"), fontRegular, XBrushes.Black,
                        new XRect(tableX + nameWidth, tableY, colWidth, rowHeight),
                        XStringFormats.TopLeft);
                    gfx.DrawString(item.Size, fontRegular, XBrushes.Black,
                        new XRect(tableX + nameWidth + colWidth, tableY, colWidth, rowHeight),
                        XStringFormats.TopLeft);
                    gfx.DrawString(item.Color, fontRegular, XBrushes.Black,
                        new XRect(tableX + nameWidth + 2 * colWidth, tableY, colWidth, rowHeight),
                        XStringFormats.TopLeft);

                    tableY += 40; 
                }

                gfx.DrawString($"Total: {pedido.Total.ToString("C")}", fontBold, XBrushes.Black,
                    new XRect(tableX + 2 * colWidth, tableY, colWidth, rowHeight),
                    XStringFormats.TopLeft);

                document.Save(pdfFilePath);
            }

            return pdfFilePath;
        }

        private async Task SendEmailWithPDF(string email, string pdfPath, string nPedido)
        {
            try
            {
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
                    Subject = "Confirmação do pedido " + nPedido,
                    Body = "Obrigada por confiar na nossa loja e fazer compras na nossa loja! Segue em baixo o PDF com a confirmação do seu " +
                    "pedido. Aguardamos pagamento caso a sua encomenda não seja para levantar em loja. Esperamos vê-la numa " +
                    "próxima!! <br/><br/> Dados para pagamento: <br/> MBWay: 910276923 <br/><br/>Com amor, Donna Collection ❤️",
                    IsBodyHtml = true,
                };

                mailMessage.To.Add(email);

                Attachment attachment = new Attachment(pdfPath);
                mailMessage.Attachments.Add(attachment);

                await client.SendMailAsync(mailMessage);
                Console.WriteLine("Email enviado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao enviar email: {ex.Message}");
                throw;
            }
        }

        protected void rbMoradaFaturacao_CheckedChanged(object sender, EventArgs e)
        {
            if(txtPais.Text == "Portugal")
            {
                AtualizarResumoPedido(5.95m);
            }
            else
            {
                AtualizarResumoPedido(20.5m);
            }
            updatePanelEnvio.Update();
        }

        protected void rbLoja_CheckedChanged(object sender, EventArgs e)
        {
            AtualizarResumoPedido(0);
            updatePanelEnvio.Update();
        }

        protected void rbMoradaDiferente_CheckedChanged(object sender, EventArgs e)
        {
            if (txtPais.Text == "Portugal")
            {
                AtualizarResumoPedido(5.95m);
            }
            else
            {
                AtualizarResumoPedido(20.5m);
            }
            updatePanelEnvio.Update();
        }

        private void AtualizarResumoPedido(decimal custoEnvio)
        {
            List<CartItem> cartItems = Session["CartItems"] as List<CartItem>;
            if (cartItems != null)
            {
                decimal subtotal = cartItems.Sum(item => item.TotalPrice);
                decimal total = subtotal + custoEnvio;
                lblenvio.Text = custoEnvio.ToString() + "€";    
                lbltotal.Text = total.ToString() + "€";
            }
        }

        private decimal CalculateTotal()
        {
            List<CartItem> cartItems = Session["CartItems"] as List<CartItem>;
            if (cartItems != null)
            {
                decimal subtotal = cartItems.Sum(item => item.TotalPrice);
                decimal shipping = rbLoja.Checked ? 0 : 10; 
                return subtotal + shipping;
            }
            return 0;
        }

        public static string GeneratePedidoNumber()
        {
            return "#" + DateTime.Now.ToString("yyyyMMddHHmmss");
        }
    }
}