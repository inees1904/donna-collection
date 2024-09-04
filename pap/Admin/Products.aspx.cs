using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Drawing.Imaging;
using Image = System.Drawing.Image;

namespace pap.Admin
{
    public partial class Clothes : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateCategories();
                Session["breadCumbTitle"] = "Gerir Produtos";
                Session["breadCumbPage"] = "Produtos";
                lblmsg.Visible = false;
                getProducts();
                CarregarTamanhos();
                CarregarCores();
            }
        }

        private void CarregarTamanhos()
        {
            using (SqlConnection con = new SqlConnection(Utils.getConnection()))
            {
                string query = "SELECT * FROM Tamanhos";

                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                cblstTamanhos.Items.Clear();
                while (reader.Read())
                {
                    int tamanhoId = Convert.ToInt32(reader["tamanhoId"]);
                    string nomeTamanho = reader["tamanho"].ToString();
                    cblstTamanhos.Items.Add(new ListItem(nomeTamanho, tamanhoId.ToString()));
                }
                reader.Close();
                con.Close();
            }
        }

        private void CarregarCores()
        {
            using (SqlConnection con = new SqlConnection(Utils.getConnection()))
            {
                string query = "SELECT * FROM Cores";

                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                cblstCores.Items.Clear();
                while (reader.Read())
                {
                    int corId = Convert.ToInt32(reader["corId"]);
                    string cor = reader["cor"].ToString();
                    cblstCores.Items.Add(new ListItem(cor, corId.ToString()));
                }
                reader.Close();
                con.Close();
            }
        }

        void getProducts()
        {
            conn = new SqlConnection(Utils.getConnection());
            cmd = new SqlCommand("Produto_Crud", conn);
            cmd.Parameters.AddWithValue("@Action", "GETALL");
            cmd.CommandType = CommandType.StoredProcedure;
            adapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            adapter.Fill(dt);
            rProduct.DataSource = dt;
            rProduct.DataBind();
        }

        private void PopulateCategories()
        {
            using (var conn = new SqlConnection(Utils.getConnection()))
            {
                string query = "SELECT categoriaId, nomeSubCategoria from Categoria";
                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ListItem item = new ListItem
                            {
                                Text = reader["nomeSubCategoria"].ToString(),
                                Value = reader["categoriaId"].ToString()
                            };
                            categoriaDD.Items.Add(item);
                        }
                    }
                }
            }
        }

        protected void btnAddUpdate_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                bool isValidToExecute = false;
                int produtoId = Convert.ToInt32(hfProductId.Value);
                var imagePaths = SaveUploadedImages(out isValidToExecute);

                if (isValidToExecute)
                {
                    using (var conn = new SqlConnection(Utils.getConnection()))
                    {
                        conn.Open();
                        using (var transaction = conn.BeginTransaction())
                        {
                            try
                            {
                                using (var cmd = new SqlCommand("", conn, transaction))
                                {
                                    if (produtoId == 0)
                                    {
                                        cmd.CommandText = @" INSERT INTO Produto (nomeProduto, descricaoCurtaProduto, descricaoLongaProduto, preco, quantidade, fornecedor, categoriaId, isActive, createDate, categoria2, imagemUrl1, imagemUrl2)
                                                            VALUES (@nomeProduto, @descricaoCurtaProduto, @descricaoLongaProduto, @preco, @quantidade, @fornecedor, @categoriaId, @isActive, @createDate, @categoria2, @imagemUrl1, @imagemUrl2);
                                                            SELECT SCOPE_IDENTITY();";
                                        cmd.Parameters.AddWithValue("@imagemUrl1", imagePaths.Item1);
                                        cmd.Parameters.AddWithValue("@imagemUrl2", imagePaths.Item2);
                                    }
                                    else
                                    {
                                        cmd.CommandText = @"UPDATE Produto SET nomeProduto = @nomeProduto, descricaoCurtaProduto = @descricaoCurtaProduto, 
                                                                descricaoLongaProduto = @descricaoLongaProduto, preco = @preco, quantidade = @quantidade, 
                                                                fornecedor = @fornecedor, categoriaId = @categoriaId, isActive = @isActive, 
                                                                categoria2 = @categoria2";
                                        if (!string.IsNullOrEmpty(imagePaths.Item1))
                                        {
                                            cmd.CommandText += ", imagemUrl1 = @imagemUrl1";
                                            cmd.Parameters.AddWithValue("@imagemUrl1", imagePaths.Item1);
                                        }

                                        if (!string.IsNullOrEmpty(imagePaths.Item2))
                                        {
                                            cmd.CommandText += ", imagemUrl2 = @imagemUrl2";
                                            cmd.Parameters.AddWithValue("@imagemUrl2", imagePaths.Item2);
                                        }

                                        cmd.CommandText += " WHERE produtoId = @produtoId; SELECT @produtoId;";
                                    }

                                    cmd.Parameters.AddWithValue("@nomeProduto", txtNomeProduto.Text.Trim());
                                    cmd.Parameters.AddWithValue("@descricaoCurtaProduto", txtDescricaoCurta.Text.Trim());
                                    cmd.Parameters.AddWithValue("@descricaoLongaProduto", txtDescricaoLonga.Text.Trim());

                                    decimal preco;
                                    if (!decimal.TryParse(txtPreco.Text.Trim(), out preco))
                                    {
                                        ShowMessage("Formato de preço inválido.", "alert alert-danger");
                                        return;
                                    }
                                    cmd.Parameters.AddWithValue("@preco", preco);

                                    int quantidade;
                                    if (!int.TryParse(txtQuantidade.Text.Trim(), out quantidade))
                                    {
                                        ShowMessage("Formato de quantidade inválido.", "alert alert-danger");
                                        return;
                                    }
                                    cmd.Parameters.AddWithValue("@quantidade", quantidade);

                                    cmd.Parameters.AddWithValue("@fornecedor", txtFornecedor.Text.Trim());

                                    int categoriaId;
                                    if (!int.TryParse(categoriaDD.SelectedValue, out categoriaId))
                                    {
                                        ShowMessage("Categoria inválida.", "alert alert-danger");
                                        return;
                                    }
                                    cmd.Parameters.AddWithValue("@categoriaId", categoriaId);
                                    cmd.Parameters.AddWithValue("@isActive", cbIsActive.Checked);
                                    cmd.Parameters.AddWithValue("@createDate", DateTime.Now);
                                    cmd.Parameters.AddWithValue("@categoria2", txtCategoriaPrincipal.Text.Trim());
                                    cmd.Parameters.AddWithValue("@produtoId", produtoId);
                                    produtoId = Convert.ToInt32(cmd.ExecuteScalar());

                                    SqlCommand deleteCmd = new SqlCommand("DELETE FROM Produto_Tamanhos WHERE produtoId = @produtoId", conn, transaction);
                                    deleteCmd.Parameters.AddWithValue("@produtoId", produtoId);
                                    deleteCmd.ExecuteNonQuery();

                                    SqlCommand deleteCmd1 = new SqlCommand("DELETE FROM Produto_Cores WHERE produtoId = @produtoId", conn, transaction);
                                    deleteCmd1.Parameters.AddWithValue("@produtoId", produtoId);
                                    deleteCmd1.ExecuteNonQuery();

                                    foreach (ListItem item in cblstTamanhos.Items)
                                    {
                                        if (item.Selected)
                                        {
                                            int tamanhoId = Convert.ToInt32(item.Value);
                                            SqlCommand insertTamanhoCmd = new SqlCommand("INSERT INTO Produto_Tamanhos (produtoId, tamanhoId) VALUES (@produtoId, @tamanhoId)", conn, transaction);
                                            insertTamanhoCmd.Parameters.AddWithValue("@produtoId", produtoId);
                                            insertTamanhoCmd.Parameters.AddWithValue("@tamanhoId", tamanhoId);
                                            insertTamanhoCmd.ExecuteNonQuery();
                                        }
                                    }

                                    foreach (ListItem item in cblstCores.Items)
                                    {
                                        if (item.Selected)
                                        {
                                            int corId = Convert.ToInt32(item.Value);
                                            SqlCommand insertCorCmd = new SqlCommand("INSERT INTO Produto_Cores (produtoId, corId) VALUES (@produtoId, @corId)", conn, transaction);
                                            insertCorCmd.Parameters.AddWithValue("@produtoId", produtoId);
                                            insertCorCmd.Parameters.AddWithValue("@corId", corId);
                                            insertCorCmd.ExecuteNonQuery();
                                        }
                                    }

                                    transaction.Commit();
                                    string actionName = hfProductId.Value == "0" ? "inserido" : "atualizado";
                                    ShowMessage($"Produto {actionName} com sucesso!", "alert alert-success");
                                    getProducts();
                                    clear();
                                }
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

        private (string, string) SaveUploadedImages(out bool isValid)
        {
            string image1Path = string.Empty;
            string image2Path = string.Empty;
            isValid = false;

            if (fuImagem1Url.HasFile && fuImagem2Url.HasFile)
            {
                if (Utils.isValidExtension(fuImagem1Url.FileName) && Utils.isValidExtension(fuImagem2Url.FileName))
                {
                    image1Path = SaveImage(fuImagem1Url);
                    image2Path = SaveImage(fuImagem2Url);
                    isValid = true;
                }
                else
                {
                    ShowMessage("Por favor selecionar imagem .jpg, .png, .jpeg", "alert alert-danger");
                }
            }
            else if (fuImagem1Url.HasFile)
            {
                if (Utils.isValidExtension(fuImagem1Url.FileName))
                {
                    image1Path = SaveImage(fuImagem1Url);
                    isValid = true;
                }
                else
                {
                    ShowMessage("Por favor selecionar imagem .jpg, .png, .jpeg", "alert alert-danger");
                }
            }
            else if (fuImagem2Url.HasFile)
            {
                if (Utils.isValidExtension(fuImagem2Url.FileName))
                {
                    image2Path = SaveImage(fuImagem2Url);
                    isValid = true;
                }
                else
                {
                    ShowMessage("Por favor selecionar imagem .jpg, .png, .jpeg", "alert alert-danger");
                }
            }
            else
            {
                isValid = true;
            }

            return (image1Path, image2Path);
        }

        private string SaveImage(FileUpload fileUpload)
        {
            string uniqueFileName = Utils.getUniqueId();
            string fileExtension = Path.GetExtension(fileUpload.FileName);
            string imagePath = "Images/Products/" + uniqueFileName + fileExtension;

            string serverPath = Server.MapPath("~/" + imagePath);
            if (!Directory.Exists(Path.GetDirectoryName(serverPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(serverPath));
            }

            using (var srcImage = Image.FromStream(fileUpload.PostedFile.InputStream))
            {
                int originalWidth = srcImage.Width;
                int originalHeight = srcImage.Height;
                int targetSize = 800;
                int newWidth, newHeight;

                if (originalWidth > originalHeight)
                {
                    newWidth = targetSize;
                    newHeight = (int)((double)originalHeight / originalWidth * targetSize);
                }
                else
                {
                    newHeight = targetSize;
                    newWidth = (int)((double)originalWidth / originalHeight * targetSize);
                }

                using (var resizedImage = new Bitmap(newWidth, newHeight))
                {
                    using (var graphics = Graphics.FromImage(resizedImage))
                    {
                        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        graphics.SmoothingMode = SmoothingMode.HighQuality;
                        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        graphics.CompositingQuality = CompositingQuality.HighQuality;

                        graphics.Clear(Color.Transparent);
                        graphics.DrawImage(srcImage, 0, 0, newWidth, newHeight);
                    }

                    if (fileExtension.Equals(".jpg", StringComparison.OrdinalIgnoreCase) ||
                        fileExtension.Equals(".jpeg", StringComparison.OrdinalIgnoreCase))
                    {
                        var encoderParameters = new EncoderParameters(1);
                        encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 100L);
                        ImageCodecInfo imageCodecInfo = GetEncoderInfo(ImageFormat.Jpeg);
                        resizedImage.Save(serverPath, imageCodecInfo, encoderParameters);
                    }
                    else if (fileExtension.Equals(".png", StringComparison.OrdinalIgnoreCase))
                    {
                        resizedImage.Save(serverPath, ImageFormat.Png);
                    }
                    else
                    {
                        resizedImage.Save(serverPath, srcImage.RawFormat);
                    }
                }
            }
            return imagePath;
        }

        private static ImageCodecInfo GetEncoderInfo(ImageFormat format)
        {
            return ImageCodecInfo.GetImageDecoders()
                .FirstOrDefault(codec => codec.FormatID == format.Guid);
        }

        private void ShowMessage(string message, string cssClass)
        {
            lblmsg.Visible = true;
            lblmsg.Text = message;
            lblmsg.CssClass = cssClass;
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        void clear()
        {
            txtNomeProduto.Text = string.Empty;
            txtDescricaoCurta.Text = string.Empty;
            txtDescricaoLonga.Text = string.Empty;
            txtPreco.Text = string.Empty;
            txtQuantidade.Text = string.Empty;
            txtFornecedor.Text = string.Empty;
            cbIsActive.Checked = false;
            hfProductId.Value = "0";
            btnAddUpdate.Text = "Add";
            image1Preview.ImageUrl = string.Empty;
            image2Preview.ImageUrl = string.Empty;
            txtCategoriaPrincipal.Text = string.Empty;
            cblstCores.SelectedIndex = -1;
            cblstTamanhos.SelectedIndex = -1;
        }

        protected void rProduct_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            lblmsg.Visible = false;

            if (e.CommandName == "edit")
            {
                LoadProductForEdit(Convert.ToInt32(e.CommandArgument));
            }
            else if (e.CommandName == "delete")
            {
                DeleteProduct(Convert.ToInt32(e.CommandArgument));
            }
        }

        private void LoadProductForEdit(int productId)
        {
            using (var conn = new SqlConnection(Utils.getConnection()))
            {
                using (var cmd = new SqlCommand("Produto_Crud", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Action", SqlDbType.NVarChar).Value = "GETBYID";
                    cmd.Parameters.Add("@produtoId", SqlDbType.Int).Value = productId;

                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        var dt = new DataTable();
                        adapter.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            var row = dt.Rows[0];
                            txtNomeProduto.Text = row["nomeProduto"].ToString();
                            txtDescricaoCurta.Text = row["descricaoCurtaProduto"].ToString();
                            txtDescricaoLonga.Text = row["descricaoLongaProduto"].ToString();
                            txtPreco.Text = row["preco"].ToString();
                            txtQuantidade.Text = row["quantidade"].ToString();
                            txtFornecedor.Text = row["fornecedor"].ToString();
                            categoriaDD.SelectedValue = row["categoriaId"].ToString();
                            cbIsActive.Checked = Convert.ToBoolean(row["isActive"]);
                            image1Preview.ImageUrl = string.IsNullOrEmpty(row["imagemUrl1"].ToString()) ? "~/Images/No_image.png" : "~/" + row["imagemUrl1"].ToString();
                            image2Preview.ImageUrl = string.IsNullOrEmpty(row["imagemUrl2"].ToString()) ? "~/Images/No_image.png" : "~/" + row["imagemUrl2"].ToString();
                            hfProductId.Value = row["produtoId"].ToString();
                            txtCategoriaPrincipal.Text = row["categoria2"].ToString();
                            btnAddUpdate.Text = "Update";
                            LoadProductSizes(productId);
                            LoadProductColours(productId);
                        }
                    }
                }
            }
        }

        private void LoadProductSizes(int productId)
        {
            using (var conn = new SqlConnection(Utils.getConnection()))
            {
                using (var cmd = new SqlCommand("SELECT tamanhoId FROM Produto_Tamanhos WHERE produtoId = @produtoId", conn))
                {
                    cmd.Parameters.AddWithValue("@produtoId", productId);

                    try
                    {
                        conn.Open();
                        var reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            int tamanhoId = Convert.ToInt32(reader["tamanhoId"]);
                            foreach (ListItem item in cblstTamanhos.Items)
                            {
                                if (item.Value == tamanhoId.ToString())
                                {
                                    item.Selected = true;
                                    break;
                                }
                            }
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        ShowMessage($"Error loading product sizes: {ex.Message}", "alert alert-danger");
                    }
                }
            }
        }

        private void LoadProductColours(int productId)
        {
            using (var conn = new SqlConnection(Utils.getConnection()))
            {
                using (var cmd = new SqlCommand("SELECT corId FROM Produto_Cores WHERE produtoId = @produtoId", conn))
                {
                    cmd.Parameters.AddWithValue("@produtoId", productId);

                    try
                    {
                        conn.Open();
                        var reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            int corId = Convert.ToInt32(reader["corId"]);
                            foreach (ListItem item in cblstCores.Items)
                            {
                                if (item.Value == corId.ToString())
                                {
                                    item.Selected = true;
                                    break;
                                }
                            }
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        ShowMessage($"Error loading product sizes: {ex.Message}", "alert alert-danger");
                    }
                }
            }
        }


        private void DeleteProduct(int productId)
        {
            using (var conn = new SqlConnection(Utils.getConnection()))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        using (var deleteCmd = new SqlCommand("DELETE FROM Pedido_Items WHERE produtoId = @produtoId", conn, transaction))
                        {
                            deleteCmd.Parameters.AddWithValue("@produtoId", productId);
                            deleteCmd.ExecuteNonQuery();
                        }

                        using (var deleteTamanhos = new SqlCommand("DELETE FROM Produto_Tamanhos WHERE produtoId = @produtoId", conn, transaction))
                        {
                            deleteTamanhos.Parameters.AddWithValue("@produtoId", productId);
                            deleteTamanhos.ExecuteNonQuery();
                        }

                        using (var deleteCmd1 = new SqlCommand("DELETE FROM Produto_Cores WHERE produtoId = @produtoId", conn, transaction))
                        {
                            deleteCmd1.Parameters.AddWithValue("@produtoId", productId);
                            deleteCmd1.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("Produto_Crud", conn, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@Action", SqlDbType.NVarChar).Value = "DELETE";
                            cmd.Parameters.Add("@produtoId", SqlDbType.Int).Value = productId;
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();

                        ShowMessage("Produto eliminado com sucesso!", "alert alert-success");
                        getProducts();
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