<%@ Page Title="Administração Donna Collection-Tamanhos" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Sizes.aspx.cs" Inherits="pap.Admin.Sizes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        window.onload = function () {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%=lblmsg.ClientID%>").style.display = "none";
            }, seconds * 1000);
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mb-4">
        <asp:Label ID="lblmsg" runat="server"></asp:Label>
    </div>
    <div class="row">
        <div class="col-sm-12 col-md-4">
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">Tamanhos</h4>
                    <hr />
                    <div class="form-body">
                        <label>Nome do Tamanho</label>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:TextBox ID="txtTamanho" runat="server" CssClass="form-control" placeholder="Introduzir Tamanho"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvSizeName" runat="server" ForeColor="Red" Font-Size="Small" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtTamanho" ErrorMessage="Size Name is required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="form-action pb-5">
                        <div class="text-left">
                            <asp:Button ID="btnAddUpdate" runat="server" CssClass="btn btn-info" Text="Adicionar" OnClick="btnAddUpdate_Click"/>
                            <asp:Button ID="btnClear" runat="server" CssClass="btn btn-dark" Text="Limpar" OnClick="btnClear_Click"/>
                        </div>
                    </div>
                    <asp:HiddenField ID="hfs" runat="server" Value="0" />
                </div>
            </div>
        </div>

        <div class="col-sm-12 col-md-8  ">
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">Lista de Tamanhos</h4>
                    <hr />
                    <div class="table-responsive">
                        <asp:Repeater ID="rSizes" runat="server" OnItemCommand="rSizes_ItemCommand">
                            <HeaderTemplate>
                                <table class="table data-table-export table-hover nowrap">
                                    <thead>
                                        <tr>
                                            <th>Tamanho</th>
                                            <th class="datatable-nosort">Action</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td class="table-plus"><%#Eval("tamanho") %></td>
                                    <td>
                                        <asp:LinkButton ID="lbEdit" Text="Edit" runat="server" CssClass="badge badge-primary"
                                            CommandArgument='<%# Eval("tamanhoId") %>' CommandName="edit" CausesValidation="false">
                                            <i class="fas fa-edit"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lbDelete" Text="Delete" runat="server" CssClass="badge badge-danger"
                                            CommandArgument='<%# Eval("tamanhoId") %>' CommandName="delete" CausesValidation="false">
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
