<%@ Page Title="Donna Collection-Detalhes Produto" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="ShopDetail.aspx.cs" Inherits="pap.User.ShopDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .nav-item.nav-link.active {
            background-color: #d49c94; 
            color: black; 
            border-color: #d49c94;
        }

        .nav-item.nav-link {
            color: black; 
        }

        .radioListHorizontal {
            margin-right: 10px;             
        }
        .carousel-item img {
            width: 100%;
            height: auto;
            object-fit: cover;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Repeater ID="produto" runat="server">
        <ItemTemplate>
            <div class="container-fluid py-5">
                <div class="row px-xl-5">
                    <div class="col-lg-5 pb-5">
                        <div id="product-carousel" class="carousel slide" data-ride="carousel">
                            <div class="carousel-inner border-primary">
                                <div class="carousel-item active">
                                    <img class="w-100 h-100" src="<%# pap.Utils.getImageUrl(Eval("imagemUrl1")) %>" alt="Image">
                                </div>
                                <div class="carousel-item">
                                    <img class="w-100 h-100" src="<%# pap.Utils.getImageUrl(Eval("imagemUrl2")) %>" alt="Image">
                                </div>
                            </div>
                            <a class="carousel-control-prev" href="#product-carousel" data-slide="prev">
                                <i class="fa fa-2x fa-angle-left text-dark"></i>
                            </a>
                            <a class="carousel-control-next" href="#product-carousel" data-slide="next">
                                <i class="fa fa-2x fa-angle-right text-dark"></i>
                            </a>
                        </div>
                    </div>
                    <div class="col-lg-7 pb-5">
                        <h3 class="font-weight-semi-bold"><%# Eval("nomeProduto") %></h3>
                        <h3 class="font-weight-semi-bold mb-4"><%# Eval("preco") %>€</h3>
                        <p class="mb-4"><%# Eval("descricaoCurtaProduto") %></p>
                        <div class="d-flex mb-3">
                            <p class="text-dark font-weight-medium mb-0 mr-3">Tamanhos:</p>
                            <asp:RadioButtonList ID="rblSizes" runat="server" RepeatDirection="Horizontal">
                            </asp:RadioButtonList>
                        </div>
                        <div class="d-flex mb-4">
                            <p class="text-dark font-weight-medium mb-0 mr-3">Cores:</p>
                            <asp:RadioButtonList ID="rblColors" runat="server" RepeatDirection="Horizontal" CssClass="custom-control-radio">
                            </asp:RadioButtonList>                        
                        </div>
                        <asp:HiddenField ID="hfProductId" runat="server" Value='<%# Eval("produtoId") %>' />
                        <div class="d-flex align-items-center mb-4 pt-2">
                            <div class="input-group quantity mr-3" style="width: 130px;">
                                <div class="input-group-btn">
                                    <asp:Button runat="server" ID="btnDecrease" CssClass="btn btn-primary btn-minus" Text="-" CommandArgument='<%# Eval("produtoId") %>' OnClick="btnDecrease_Click" />
                                </div>
                                <asp:Label ID="lblQuantity" runat="server" CssClass="form-control bg-secondary text-center" Text='<%# Eval("Quantity") %>' />
                                <div class="input-group-btn">
                                    <asp:Button runat="server" ID="btnIncrease" CssClass="btn btn-primary btn-plus" Text="+" CommandArgument='<%# Eval("produtoId") %>' OnClick="btnIncrease_Click"/>
                                </div>
                            </div>
                            <asp:LinkButton runat="server" ID="verDetalhes" CssClass="btn btn-primary px-3" CommandArgument='<%# Eval("produtoId") %>' OnClick="verDetalhes_Click">
                                <i class="fa fa-shopping-cart mr-1"></i> Adicionar ao carrinho
                            </asp:LinkButton>  
                        </div>
                    </div>
                </div>
            </div>
                <div class="row px-xl-5">
                    <div class="col">
                        <div class="nav nav-tabs justify-content-right border-primary mb-4">
                            <a class="nav-item nav-link active" data-toggle="tab" href="#tab-pane-1">Descrição</a>
                        </div>
                        <div class="tab-content">
                            <div class="tab-pane fade show active" id="tab-pane-1">
                                <h4 class="mb-3">Descrição</h4>
                                <p><%# Eval("descricaoLongaProduto") %></p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
