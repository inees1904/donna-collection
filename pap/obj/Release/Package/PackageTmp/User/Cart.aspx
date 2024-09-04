<%@ Page Title="Donna Collection-Carrinho de Compras" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="pap.User.Cart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid pt-5">
        <div class="row px-xl-5">
            <div class="col-lg-8 table-responsive mb-5">
                <table class="table table-bordered text-center mb-0" style="border-color: #D19C97;">
                    <thead class="text-dark" style="background-color: #D19C97; border-color: #D19C97;">
                        <tr>
                            <th>Produtos</th>
                            <th>Tamanho</th>
                            <th>Cor</th>
                            <th>Preço</th>
                            <th>Quantidade</th>
                            <th>Total</th>
                            <th>Remover</th>
                        </tr>
                    </thead>
                    <tbody class="align-middle">
                        <asp:Repeater ID="rptCartItems" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td class="align-middle text-dark"><img src="<%# pap.Utils.getImageUrl(Eval("ImagemUrl1")) %>" alt="" style="width: 50px;"> <%# Eval("ProductName") %></td>
                                    <td class="align-middle text-dark"><%# Eval("Size") %> </td>
                                    <td class="align-middle text-dark"><%# Eval("Color") %> </td>
                                    <td class="align-middle text-dark"><%# Eval("Price", "{0:C}") %></td>
                                    <td class="align-middle text-dark">
                                        <div class="input-group quantity mx-auto" style="width: 100px;">
                                            <div class="input-group-btn">
                                                <asp:Button runat="server" ID="btnDecrease" CssClass="btn btn-sm btn-primary btn-minus" Text="-" CommandArgument='<%# Eval("ProductId") + ";" + Eval("SizeId") + ";" + Eval("ColorId") + ";"%>' OnClick="btnDecrease_Click" />
                                            </div>
                                            <span>&nbsp <%# Eval("Quantity") %> &nbsp</span>
                                            <div class="input-group-btn">
                                                <asp:Button runat="server" ID="btnIncrease" CssClass="btn btn-sm btn-primary btn-plus" Text="+" CommandArgument='<%# Eval("ProductId") + ";" + Eval("SizeId") + ";" + Eval("ColorId") + ";" %>' OnClick="btnIncrease_Click"/>
                                            </div>
                                        </div>
                                    </td>
                                    <td class="align-middle text-dark"><%# Eval("TotalPrice", "{0:C}") %></td>
                                    <td class="align-middle text-dark font-weight-bold"><asp:LinkButton runat="server" ID="btnRemoverItem" CommandArgument='<%# Eval("ProductId") + ";" + Eval("SizeId") + ";" + Eval("ColorId") + ";" %>' CssClass="btn btn-sm btn-primary font-weight-bold" Text="X" OnClick="btnRemoverItem_Click" /></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
            <div class="col-lg-4">
                <div class="card border-secondary mb-5">
                    <div class="card-header border-0" style="background-color: #D19C97;">
                        <h4 class="font-weight-semi-bold m-0">Resumo do Pedido</h4>
                    </div>
                    <div class="card-footer border-secondary" style="background-color: ghostwhite;">
                        <div class="d-flex justify-content-between mt-2">
                            <h5 class="font-weight-bold">Total</h5>
                            <h5 class="font-weight-bold" id="total"></h5>
                        </div>
                        <asp:Button runat="server" ID="btnFinalizarPedido" CssClass="btn btn-block btn-primary my-3 py-3" Text="Finalizar Pedido" OnClick="btnFinalizarPedido_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
