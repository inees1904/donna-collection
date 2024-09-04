<%@ Page Title="Administração Donna Collection-Produtos" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="pap.Admin.Clothes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        window.onload = function () {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%=lblmsg.ClientID%>").style.display = "none";
            }, seconds * 1000);
        };
    </script>
    <script>
        function Image1Preview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%= image1Preview.ClientID%>').prop('src', e.target.result).width(200).height(200);
                };
                reader.readAsDataURL(input.files[0]);
            }
        }
        function Image2Preview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%= image2Preview.ClientID%>').prop('src', e.target.result).width(200).height(200);
                };
                reader.readAsDataURL(input.files[0]);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mb-4">
        <asp:Label ID="lblmsg" runat="server"></asp:Label>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">Produtos</h4>
                    <hr />
                    <div class="form-body">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Nome Produto</label>
                                    <asp:TextBox ID="txtNomeProduto" runat="server" CssClass="form-control" placeholder="Introduzir Nome"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvNomeProduto" runat="server" ForeColor="Red" Font-Size="Small" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtNomeProduto" ErrorMessage="Product Name is required"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <label>Descrição Curta Produto</label>
                                    <asp:TextBox ID="txtDescricaoCurta" runat="server" CssClass="form-control" placeholder="Introduzir Descrição Curta"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Descrição Longa Produto</label>
                                    <asp:TextBox ID="txtDescricaoLonga" TextMode="MultiLine" runat="server" CssClass="form-control" placeholder="Introduzir Descrição Longa"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>Preço</label>
                                    <asp:TextBox ID="txtPreco" runat="server" CssClass="form-control" placeholder="Introduzir Preço"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvPreco" runat="server" ForeColor="Red" Font-Size="Small" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtPreco" ErrorMessage="Price is required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Quantidade</label>
                                    <asp:TextBox ID="txtQuantidade" TextMode="Number" min="1" max="50" step="1" runat="server" CssClass="form-control" placeholder="Introduzir Quantidade"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvQuantidade" runat="server" ForeColor="Red" Font-Size="Small" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtPreco" ErrorMessage="Quantity is required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Fornecedor</label>
                                    <asp:TextBox ID="txtFornecedor" runat="server" CssClass="form-control" placeholder="Introduzir Fornecedor"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvFornecedor" runat="server" ForeColor="Red" Font-Size="Small" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtPreco" ErrorMessage="Fornecedor is required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Categoria</label>
                                    <asp:DropDownList ID="categoriaDD" CssClass="form-control" runat="server"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvcategory" runat="server" ForeColor="Red" Font-Size="Small" Display="Dynamic" SetFocusOnError="true" ControlToValidate="categoriaDD" ErrorMessage="Product Category is required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Imagem Produto Principal</label>
                                    <asp:FileUpload ID="fuImagem1Url" runat="server" CssClass="form-control" onchange="Image1Preview(this);" /> 
                                    <asp:HiddenField ID="hfProductId" runat="server" Value="0"/>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Imagem Produto</label>
                                    <asp:FileUpload ID="fuImagem2Url" runat="server" CssClass="form-control" onchange="Image2Preview(this);" /> 
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:CheckBox ID="cbIsActive" runat="server" Text="&nbsp; isActive" />
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Categoria Principal</label>
                                    <asp:TextBox ID="txtCategoriaPrincipal" runat="server" CssClass="form-control" placeholder="Introduzir Categoria"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvCategoriaPrincipal" runat="server" ForeColor="Red" Font-Size="Small" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtCategoriaPrincipal" ErrorMessage="Categoria is required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Tamanhos Disponíveis</label>
                                    <asp:CheckBoxList ID="cblstTamanhos" runat="server" CssClass="form-check">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Cores Disponíveis</label>
                                    <asp:CheckBoxList ID="cblstCores" runat="server" CssClass="form-check">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                        </div>
                    <div class="form-action pb-5">
                        <div class="text-left">
                            <asp:Button ID="btnAddUpdate" runat="server" CssClass="btn btn-info" Text="Add" OnClick="btnAddUpdate_Click"/>
                            <asp:Button ID="btnClear" runat="server" CssClass="btn btn-dark" Text="Reset" OnClick="btnClear_Click"/>
                        </div>
                    </div>
                    <div>
                        <asp:Image ID="image1Preview" runat="server" CssClass="img-thumbnail" AlternateText="" />
                        <asp:Image ID="image2Preview" runat="server" CssClass="img-thumbnail" AlternateText="" />
                    </div>

                </div>
            </div>
        </div>
        <div class="col-md-12">
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">Lista de Produtos</h4>
                    <hr />
                    <div class="table-responsive">
                        <asp:Repeater ID="rProduct" runat="server" OnItemCommand="rProduct_ItemCommand">
                            <HeaderTemplate>
                                <table class="table data-table-export table-hover nowrap">
                                    <thead>
                                        <tr>
                                            <th>Nome</th>
                                            <th>Descrição Curta</th>
                                            <th>Descrição Longa</th>
                                            <th>Preço</th>
                                            <th>Quantidade</th>
                                            <th>Fornecedor</th>
                                            <th>Categoria</th>
                                            <th>Categoria Principal</th>
                                            <th>Is Active</th>
                                            <th>Create Date</th>
                                            <th>Imagem 1</th>
                                            <th>Imagem 2</th>
                                            <th class="datatable-nosort">Action</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td class="table-plus"><%#Eval("nomeProduto") %></td>
                                    <td><%#Eval("descricaoCurtaProduto") %></td>
                                    <td><%#Eval("descricaoLongaProduto") %></td>
                                    <td><%#Eval("preco") %>€</td>
                                    <td><%#Eval("quantidade") %></td>
                                    <td><%#Eval("fornecedor") %></td>
                                    <td><%#Eval("nomeSubCategoria") %></td>
                                    <td><%#Eval("categoria2") %></td>
                                    <td>
                                        <asp:Label ID="lblIsActive" runat="server"
                                            Text='<%# (bool)Eval("isActive") ? "Active" : "In-Active" %>'
                                            CssClass='<%# (bool)Eval("isActive") ? "badge badge-success" : "badge badge-danger" %>'>
                                        </asp:Label>
                                    </td>
                                    <td>
                                        <%# Eval("createDate") %>
                                    </td>
                                    <td>
                                        <img width="40" src='<%# pap.Utils.getImageUrl(Eval("imagemUrl1")) %>' alt="image" />
                                    </td>
                                    <td>
                                        <img width="40" src='<%# pap.Utils.getImageUrl(Eval("imagemUrl2")) %>' alt="image" />
                                    </td>
                  
                                    <td>
                                        <asp:LinkButton ID="lbEdit" Text="Edit" runat="server" CssClass="badge badge-primary"
                                            CommandArgument='<%# Eval("produtoId") %>' CommandName="edit" CausesValidation="false">
                                            <i class="fas fa-edit"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lbDelete" Text="Delete" runat="server" CssClass="badge badge-danger"
                                            CommandArgument='<%# Eval("produtoId") %>' CommandName="delete" CausesValidation="false">
                                            <i class="fas fa-trash-alt"></i>
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
</asp:Content>
