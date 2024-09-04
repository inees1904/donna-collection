<%@ Page Title="Administração Donna Collection-~Utilizadores" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="pap.Admin.Utilizadores" %>
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
        function ImagePreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%= imagePreview.ClientID%>').prop('src', e.target.result).width(200).height(200);
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
                    <h4 class="card-title">Utilizadores</h4>
                    <hr />
                    <div class="form-body">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Nome</label>
                                    <asp:TextBox ID="txtNome" runat="server" CssClass="form-control" placeholder="Introduzir Nome"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvNome" runat="server" ForeColor="Red" Font-Size="Small" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtNome" ErrorMessage="Name is required"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <label>Username</label>
                                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Introduzir Username"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ForeColor="Red" Font-Size="Small" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtUsername" ErrorMessage="Username is required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Telemóvel</label>
                                    <asp:TextBox ID="txtTelemovel" TextMode="Phone" runat="server" CssClass="form-control" placeholder="Introduzir Número de Telemóvel"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>Email</label>
                                    <asp:TextBox ID="txtEmail" TextMode="Email" runat="server" CssClass="form-control" placeholder="Introduzir Email"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ForeColor="Red" Font-Size="Small" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtEmail" ErrorMessage="Email is required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Morada</label>
                                    <asp:TextBox ID="txtMorada" runat="server" CssClass="form-control" placeholder="Introduzir Morada"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Código Postal</label>
                                    <asp:TextBox ID="txtCodigoPostal" runat="server" CssClass="form-control" placeholder="Introduzir Código Postal"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Password</label>
                                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" placeholder="Introduzir Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ForeColor="Red" Font-Size="Small" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtPassword" ErrorMessage="Password is required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Imagem</label>
                                    <asp:FileUpload ID="fuImagemUrl" runat="server" CssClass="form-control" onchange="ImagePreview(this);" /> 
                                    <asp:HiddenField ID="hfuserId" runat="server" Value="0"/>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Cargo</label>
                                    <asp:DropDownList ID="cargoDD" CssClass="form-control" runat="server"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvcargo" runat="server" ForeColor="Red" Font-Size="Small" Display="Dynamic" SetFocusOnError="true" ControlToValidate="cargoDD" ErrorMessage="Cargo is required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>

                    <div class="form-action pb-5">
                        <div class="text-left">
                            <asp:Button ID="btnAddUpdate" runat="server" CssClass="btn btn-info" Text="Adicionar" OnClick="btnAddUpdate_Click"/>
                            <asp:Button ID="btnClear" runat="server" CssClass="btn btn-dark" Text="Limpar"  OnClick="btnClear_Click"/>
                        </div>
                    </div>

                    <div>
                        <asp:Image ID="imagePreview" runat="server" CssClass="img-thumbnail" AlternateText="" />
                    </div>

                </div>
            </div>
        </div>

        <div class="col-md-12">
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">Lista de Utilizadores</h4>
                    <hr />
                    <div class="table-responsive">
                        <asp:Repeater ID="rUser" runat="server" OnItemCommand="rUser_ItemCommand">
                            <HeaderTemplate>
                                <table class="table data-table-export table-hover nowrap">
                                    <thead>
                                        <tr>
                                            <th>Nome</th>
                                            <th>Username</th>
                                            <th>Telemóvel</th>
                                            <th>Email</th>
                                            <th>Morada</th>
                                            <th>Código Postal</th>
                                            <th>Password</th>
                                            <th>Imagem</th>
                                            <th>Cargo</th>
                                            <th>Create Date</th>
                                            <th>Last Login</th>
                                            <th class="datatable-nosort">Action</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td class="table-plus"><%#Eval("nome") %></td>
                                    <td><%#Eval("userName") %></td>
                                    <td><%#Eval("telemovel") %></td>
                                    <td><%#Eval("email") %></td>
                                    <td><%#Eval("morada") %></td>
                                    <td><%#Eval("codigoPostal") %></td>
                                    <td><%#Eval("pssword") %></td>
                                    <td><img width="40" src='<%# pap.Utils.getImageUrl(Eval("imagemUrl")) %>' alt="image" /></td>
                                    <td><%# Eval("nomeCargo") %></td>
                                    <td><%# Eval("createDate") %></td>
                                    <td><%# Eval("lastLogin") %></td>                  
                                    <td>
                                        <asp:LinkButton ID="lbEdit" Text="Edit" runat="server" CssClass="badge badge-primary"
                                            CommandArgument='<%# Eval("userId") %>' CommandName="edit" CausesValidation="false">
                                            <i class="fas fa-edit"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lbDelete" Text="Delete" runat="server" CssClass="badge badge-danger"
                                            CommandArgument='<%# Eval("userId") %>' CommandName="delete" CausesValidation="false">
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
</asp:Content>
