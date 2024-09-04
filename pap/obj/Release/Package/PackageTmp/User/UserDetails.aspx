<%@ Page Title="Donna Collection-Perfil" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="UserDetails.aspx.cs" Inherits="pap.User.UserDetails" %>

<%@ Import Namespace="pap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .bg-transparent {
            background-color: transparent !important;
        }
        .bg-transparent .nav-link {
            background-color: transparent !important;
        }
    </style>
    <script>
        function openDetailsModal() {
            $('#pedidoDetalhesModal').modal('show');
        }
    </script>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.bundle.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <%
        string imageUrl = Session["imagemUrl"].ToString();
     %>
    <section class="book_section layout_padding">
        <div class="container border-primary">
            <div class="heading_container">
                <h2>Perfil</h2>
            </div>
            <div class="row">
                <div class="col-12">
                    <div class="card" style="background-color: #f3d5d3">
                        <div class="card-body">
                            <div class="card-title mb-4">
                                <div class="d-flex justify-content-start">
                                    <div class="image-container">
                                        <img src="<%= pap.Utils.getImageUrl(imageUrl)%>" id="imgProfile" style="width: 150px; height: 150px;" 
                                            class="img-thumbnail" />
                                        <div class="middle pt-2">
                                            <a href="EditProfile.aspx" class="btn btn-light">
                                                <i class="fa fa-pencil"></i>Editar Perfil
                                            </a>
                                        </div>
                                    </div>
                                    <div class="userData ml-3">
                                        <h2 class="d-block" style="font-size: 1.5rem; font-weight: bold;">
                                            <a href="javascript:void(0);" style="color: black;"><%Response.Write(Session["nome"]); %></a>
                                        </h2>
                                        <h6 class="d-block">
                                            <a href="javascript:void(0);" style="color: black;">
                                                <asp:Label ID="lblUsername" runat="server" ToolTip="Username">
                                                    @<%Response.Write(Session["username"]); %>
                                                </asp:Label>
                                            </a>
                                        </h6>
                                        <h6 class="d-block">
                                            <a href="javascript:void(0);" style="color: black;">
                                                <asp:Label ID="lblEmail" runat="server" ToolTip="Email">
                                                    <%Response.Write(Session["email"]); %>
                                                </asp:Label>
                                            </a>
                                        </h6>
                                        <h6 class="d-block">
                                            <a href="javascript:void(0);" style="color: black;">
                                                <asp:Label ID="lblDate" runat="server" ToolTip="Create Date">
                                                    <%Response.Write(Session["createDate"]); %>
                                                </asp:Label>
                                            </a>
                                        </h6>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12">
                                    <ul class="nav nav-tabs mb-4 bg-transparent border-primary" id="myTab" role="tablist">
                                        <li class="nav-item">
                                            <a class="nav-link active text-dark border-primary" id="basicInfo-tab" data-toggle="tab" href="#basicInfo" role="tab"
                                                aria-controls="basicInfo" aria-selected="true"><i class="fa fa-id-badge mr-2"></i>Informação</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link text-dark border-primary" id="moreInfo-tab" data-toggle="tab" href="#moreInfo" role="tab"
                                                aria-controls="moreInfo" aria-selected="false"><i class="fa fa-clock mr-2"></i>Histórico Pedidos</a>
                                        </li>
                                    </ul>
                                    <div class="tab-content ml-1" id="myTabContent">
                                        <div class="tab-pane fade show active text-dark" id="basicInfo" role="tabpanel" aria-labelledby="basicInfo-tab">
                                            <asp:Repeater ID="rUserProfile" runat="server">
                                                <ItemTemplate>
                                                    <div class="row">
                                                        <div class="col-sm-3 col-md-2 col-5">
                                                            <label style="font-weight: bold;">Nome</label>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <%#Eval("nome") %>
                                                        </div>
                                                    </div>
                                                    <hr />
                                                    <div class="row">
                                                        <div class="col-sm-3 col-md-2 col-5">
                                                            <label style="font-weight: bold;">Username</label>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <%#Eval("userName") %>
                                                        </div>
                                                    </div>
                                                    <hr />
                                                    <div class="row">
                                                        <div class="col-sm-3 col-md-2 col-5">
                                                            <label style="font-weight: bold;">Telemóvel</label>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <%#Eval("telemovel") %>
                                                        </div>
                                                    </div>
                                                    <hr />
                                                    <div class="row">
                                                        <div class="col-sm-3 col-md-2 col-5">
                                                            <label style="font-weight: bold;">Email</label>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <%#Eval("email") %>
                                                        </div>
                                                    </div>
                                                    <hr />
                                                    <div class="row">
                                                        <div class="col-sm-3 col-md-2 col-5">
                                                            <label style="font-weight: bold;">Nome</label>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <%#Eval("nome") %>
                                                        </div>
                                                    </div>
                                                    <hr />
                                                    <div class="row">
                                                        <div class="col-sm-3 col-md-2 col-5">
                                                            <label style="font-weight: bold;">Morada</label>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <%#Eval("morada") %>
                                                        </div>
                                                    </div>
                                                    <hr />
                                                    <div class="row">
                                                        <div class="col-sm-3 col-md-2 col-5">
                                                            <label style="font-weight: bold;">Código Postal</label>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <%#Eval("codigoPostal") %>
                                                        </div>
                                                    </div>
                                                    <hr />
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                        <div class="tab-pane fade" id="moreInfo" role="tabpanel" aria-labelledby="moreInfo-tab">
                                            <div class="col-md-12">
                                                <div class="card bg-transparent border-primary">
                                                    <div class="card-body bg-transparent border-primary">
                                                        <h4 class="card-title">Histórico de Pedidos</h4>
                                                        <hr />
                                                        <div class="table-responsive">
                                                            <asp:Repeater ID="rPedidos" runat="server" OnItemCommand="rPedidos_ItemCommand">
                                                                <HeaderTemplate>
                                                                    <table class="table data-table-export table-hover nowrap">
                                                                        <thead>
                                                                            <tr class="border-primary">
                                                                                <th class="border-primary">Número Pedido</th>
                                                                                <th class="border-primary">Data Pedido</th>
                                                                                <th class="border-primary">Total</th>
                                                                                <th class="border-primary">Pagamento</th>
                                                                                <th class="border-primary">Estado</th>
                                                                                <th class="datatable-nosort border-primary">Rastreio</th>
                                                                                <th class="datatable-nosort border-primary">Action</th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <tr class="border-primary">
                                                                        <td class="table-plus border-primary">
                                                                            <asp:LinkButton ID="lbPedidoDetalhes" runat="server" Text='<%# Eval("pedidoN") %>' CommandName="detalhes" CommandArgument='<%# Eval("pedidoN") %>' CssClass="text-primary"></asp:LinkButton>
                                                                        </td>
                                                                        <td class="border-primary"><%#Eval("dataPedido") %></td>
                                                                        <td class="border-primary"><%#Eval("total") %>€</td>
                                                                        <td class="border-primary"><%#Eval("metodoPagamento") %></td>
                                                                        <td class="border-primary"><%#Eval("estado") %></td>
                                                                        <td class="border-primary">
                                                                            <asp:LinkButton ID="lbRastreio" Text="Rastreio" runat="server" CssClass="badge badge-primary"
                                                                                CommandArgument='<%#Eval("linkRastreio") %>' CommandName="rastreio" CausesValidation="false">
                                                                                <i class="fas fa-truck-moving"></i>
                                                                            </asp:LinkButton>
                                                                        </td>
                                                                        <td class="border-primary">
                                                                            <asp:LinkButton ID="lblPdf" Text="PDF" runat="server" CssClass="badge badge-primary"
                                                                                CommandArgument='<%# Eval("pedidoN") %>' CommandName="pdf" CausesValidation="false">
                                                                                <i class="fas fa-print"></i>
                                                                            </asp:LinkButton>
                                                                            <asp:LinkButton ID="lbDelete" Text="Delete" runat="server" CssClass="badge badge-danger"
                                                                                CommandArgument='<%# Eval("pedidoN") %>' CommandName="delete" CausesValidation="false">
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
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <div class="modal fade" id="pedidoDetalhesModal" tabindex="-1" role="dialog" aria-labelledby="pedidoDetalhesModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="pedidoDetalhesModalLabel">Detalhes do Pedido</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:GridView ID="gvOrderDetails" runat="server" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField DataField="nomeProduto" HeaderText="Nome do Produto" />
                            <asp:BoundField DataField="tamanho" HeaderText="Tamanho" />
                            <asp:BoundField DataField="cor" HeaderText="Cor" />
                            <asp:BoundField DataField="quantidade" HeaderText="Quantidade" />
                            <asp:BoundField DataField="preco" HeaderText="Preço" />
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
