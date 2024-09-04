<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="Wishlist.aspx.cs" Inherits="pap.User.Wishlist" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .search-input {
            max-width: 200px; 
        }

        .btn-primary {
            padding: 0.375rem 0.75rem;
        }

        .btn-primary i {
            font-size: 18px; 
        }

        .product-item .product-img img {
            width: 100%;
            height: 400px; 
            object-fit: cover; 
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid mb-5">
        <div class="d-flex flex-column align-items-center justify-content-center" style="min-height: 100px">
            <h1 class="font-weight-semi-bold text-uppercase mb-3">Wishlist</h1> 
        </div>
    </div>
    <div class="container-fluid pt-5">
        <div class="row px-xl-5">
            <div class="col-lg-9 col-md-12">
                <div class="row pb-3">
                    <div class="col-12 pb-1 justify-content-end align-content-end">
                        <div class="d-flex align-items-center justify-content-between mb-4">
                            <div class="input-group">
                                <asp:TextBox ID="searchInput" runat="server" CssClass="form-control search-input" Placeholder="Procurar por nome"></asp:TextBox>
                                <div class="input-group-append">
                                    <asp:LinkButton runat="server" ID="btnPesquisar" CssClass="btn btn-primary" Text='<i class="fa fa-search"></i>' OnClick="btnPesquisar_Click"/>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:Repeater ID="produtos" runat="server">
                        <ItemTemplate>
                            <div class="col-lg-4 col-md-6 col-sm-12 pb-1">
                                <div class="card product-item mb-4" style="background-color: transparent;">
                                    <div class="card-header product-img position-relative overflow-hidden bg-transparent p-0">
                                        <a href="ShopDetail.aspx?productId=<%# Eval("produtoId") %>">
                                            <img class="img-fluid w-100" src='<%# pap.Utils.getImageUrl(Eval("imagemUrl1")) %>' alt='<%# Eval("nomeProduto") %>'>
                                        </a>
                                    </div>
                                    <div class="card-body text-center p-0 pt-4 pb-3">
                                        <h6 class="text-truncate mb-3"><%# Eval("nomeProduto") %></h6>
                                        <div class="d-flex justify-content-center">
                                            <h6><%# Eval("preco", "{0:C}") %></h6>
                                        </div>
                                    </div>
                                    <div class="card-footer d-flex justify-content-between">
                                        <asp:LinkButton runat="server" ID="verDetalhes" CssClass="btn btn-sm text-dark p-0" CommandArgument='<%# Eval("produtoId") %>' OnClick="verDetalhes_Click">
                                            <i class="fas fa-eye text-primary mr-1"></i>Ver Detalhes
                                        </asp:LinkButton>    
                                        <asp:LinkButton runat="server" ID="btnWishlist" CssClass="btn btn-sm text-dark p-0" CommandArgument='<%# Eval("produtoId") %>' OnClick="btnWishlist_Click">
                                            <i class="fas fa-heart text-primary mr-1"></i>Remover da Wishlist
                                        </asp:LinkButton>   
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
