<%@ Page Title="Administração Donna Collection-Pedidos" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="pap.Admin.Orders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
    function openDetailsModal() {
        $('#detailsModal').modal('show');
    }

    function openAddressModal() {
        $('#addressModal').modal('show');
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
                    <h4 class="card-title">Pedidos</h4>
                    <hr />
                    <div class="table-responsive">
                        <asp:Repeater ID="rOrders" runat="server" OnItemCommand="rOrders_ItemCommand">
                            <HeaderTemplate>
                                <table class="table data-table-export table-hover nowrap">
                                    <thead>
                                        <tr>
                                            <th>Número Pedido</th>
                                            <th>Cliente</th>
                                            <th>Estado</th>
                                            <th>Data</th>
                                            <th>Total</th>
                                            <th>Morada</th>
                                            <th>Link Rastreio</th>
                                            <th>Ação</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%#Eval("pedidoN") %></td>
                                    <td><%#Eval("nomeFaturacao") %></td>
                                    <td>
                                        <asp:DropDownList ID="ddlEstado" runat="server" DataSourceID="SqlDataSourceEstado" DataTextField="estado" DataValueField="estadoId" SelectedValue='<%#Eval("estado") %>'>
                                        </asp:DropDownList>
                                    </td>
                                    <td><%#Eval("dataPedido") %></td>
                                    <td><%#Eval("total") %>€</td>
                                    <td>
                                        <asp:LinkButton ID="lbViewAddress" Text="Ver Morada" runat="server" CommandArgument='<%# Eval("detalhesPedidoId") %>' 
                                            CommandName="viewAddress" CausesValidation="false"></asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLinkRastreio" runat="server" Text='<%# Eval("linkRastreio") %>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lbUpdate" Text="Atualizar" runat="server" CommandArgument='<%# Eval("detalhesPedidoId") %>' 
                                            CommandName="update" CausesValidation="false">
                                            <i class="fas fa-upload"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lbViewDetails" Text="Ver Detalhes" runat="server" CommandArgument='<%# Eval("detalhesPedidoId") %>' 
                                            CommandName="viewDetails" CausesValidation="false">
                                            <i class="fas fa-eye"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lbPDF" Text="PDF" runat="server" CommandArgument='<%# Eval("pedidoN") %>'
                                            CommandName="PDF" CausesValidation="false">
                                            <i class="fas fa-file-pdf"></i>
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                        <asp:SqlDataSource ID="SqlDataSourceEstado" runat="server" 
                            SelectCommand="SELECT estadoId, estado FROM Estado"></asp:SqlDataSource>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="detailsModal" tabindex="-1" role="dialog" aria-labelledby="detailsModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="detailsModalLabel">Detalhes do Pedido</h5>
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

    <div class="modal fade" id="addressModal" tabindex="-1" role="dialog" aria-labelledby="addressModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="addressModalLabel">Detalhes do Endereço</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:Panel ID="pnlAddressDetails" runat="server">
                        <strong>Faturação:</strong><br />
                        Nome: <asp:Label ID="lblNomeFaturacao" runat="server" /><br />
                        Telemóvel: <asp:Label ID="lblTelemovelFaturacao" runat="server" /><br />
                        Email: <asp:Label ID="lblEmailFaturacao" runat="server" /><br />
                        Morada: <asp:Label ID="lblMoradaFaturacao" runat="server" /><br />
                        Código Postal: <asp:Label ID="lblCodPostalFaturacao" runat="server" /><br />
                        NIF: <asp:Label ID="lblNifFaturacao" runat="server" /><br /><br />
                        <strong>Envio:</strong><br />
                        Nome: <asp:Label ID="lblNomeEnvio" runat="server" /><br />
                        Telemóvel: <asp:Label ID="lblTelemovelEnvio" runat="server" /><br />
                        Morada: <asp:Label ID="lblMoradaEnvio" runat="server" /><br />
                        Código Postal: <asp:Label ID="lblCodPostalEnvio" runat="server" />
                    </asp:Panel>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
