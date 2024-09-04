<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="pap.Admin.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .page-wrapper {
            margin: 0;
            padding: 0px 0px 0px 0px !important;    
        }
        .widget-card-1 {
            padding: 0px;
            border-radius: 8px;
            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
        }
        .card1-icon {
            font-size: 2rem;
            margin-bottom: 10px;
            background-color: black;
        }
        .f-w-600 {
            font-weight: 600;
        }
        .align-left {
            display: flex;
            justify-content: flex-start;
            flex-wrap: wrap; 
        }
        .pcoded-inner-content{
            padding: 0px !important;
        }
        .container-fluid{
            padding: 5px 70px 0px 0px !important;
            border: 0px;
            box-shadow: none !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="pcoded-inner-content">
            <div class="main-body">
                <div class="page-wrapper">
                    <div class="page-body">
                        <div class="row">
                            <div class="col-md-6 col-xl-3">
                                <div class="card widget-card-1">
                                    <div class="card-block-small">
                                        <i class="fas fa-tag card1-icon"></i>
                                        <span class="text-dark f-w-600">Categorias</span>
                                        <h4><asp:Label ID="lblCategorias" runat="server"></asp:Label></h4>
                                        <div>
                                            <span class="f-left m-t-10 text-muted">
                                                <a href="Category.aspx" style="color: black;"><i class="fas fa-eye"></i> Ver Detalhes</a>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6 col-xl-3">
                                <div class="card widget-card-1">
                                    <div class="card-block-small">
                                        <i class="fas fa-palette card1-icon"></i>
                                        <span class="text-dark f-w-600">Cores</span>
                                        <h4 style="color: black;"><asp:Label ID="lblCores" runat="server"></asp:Label></h4>
                                        <div>
                                            <span class="f-left m-t-10 text-muted">
                                                <a href="Color.aspx" style="color: black;"><i class="fas fa-eye"></i> Ver Detalhes</a>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6 col-xl-3">
                                <div class="card widget-card-1">
                                    <div class="card-block-small">
                                        <i class="fas fa-ruler card1-icon"></i>
                                        <span class="text-dark f-w-600">Tamanhos</span>
                                        <h4><asp:Label ID="lblTamanhos" runat="server"></asp:Label></h4>
                                        <div>
                                            <span class="f-left m-t-10 text-muted">
                                                <a href="Size.aspx" style="color: black;"><i class="fas fa-eye"></i> Ver Detalhes</a>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6 col-xl-3">
                                <div class="card widget-card-1">
                                    <div class="card-block-small">
                                        <i class="fas fa-shopping-cart card1-icon"></i>
                                        <span class="text-dark f-w-600">Pedidos</span>
                                        <h4><asp:Label ID="lblPedidos" runat="server"></asp:Label></h4>
                                        <div>
                                            <span class="f-left m-t-10 text-muted">
                                                <a href="Orders.aspx" style="color: black;"><i class="fas fa-eye"></i> Ver Detalhes</a>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6 col-xl-3">
                                <div class="card widget-card-1">
                                    <div class="card-block-small">
                                        <i class="fas fa-box-open card1-icon"></i>
                                        <span class="text-dark f-w-600">Produtos</span>
                                        <h4><asp:Label ID="lblProdutos" runat="server"></asp:Label></h4>
                                        <div>
                                            <span class="f-left m-t-10 text-muted">
                                                <a href="Products.aspx" style="color: black;"><i class="fas fa-eye"></i> Ver Detalhes</a>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6 col-xl-3">
                                <div class="card widget-card-1">
                                    <div class="card-block-small">
                                        <i class="fas fa-users card1-icon"></i>
                                        <span class="text-dark f-w-600">Utilizadores</span>
                                        <h4><asp:Label ID="lblUtilizadores" runat="server"></asp:Label></h4>
                                        <div>
                                            <span class="f-left m-t-10 text-muted">
                                                <a href="Users.aspx" style="color: black;"><i class="fas fa-eye"></i> Ver Detalhes</a>
                                            </span>
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
</asp:Content>