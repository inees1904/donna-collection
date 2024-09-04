<%@ Page Title="Donna Collection-Métodos de Pagamento e Envio" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="Metodos.aspx.cs" Inherits="pap.User.Metodos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .responsive-image {
            max-width: 100%; 
            height: auto;  
            display: block; 
            margin: 0 auto; 
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid mb-5">
        <div class="d-flex flex-column align-items-center">
            <h1 class="font-weight-semi-bold text-uppercase mb-3 justify-content-center align-content-center text-center">Métodos de Pagamento e Envio</h1>
            <div class="p-4 text-black" style="color: black;">
                <p class="text-justify">
                    <strong>Envio</strong><br />
                    A Donna Collection dispõe de 2 métodos de envio. Dispomos de envio por CTT, para morada disponibilizada pelo cliente, cujos valores estão na tabela abaixo (a estes valores acresce o IVA de 23%).
                    Dispomos também de levantamento em loja, que é gratuito. 
                    <br />
                    <strong>Nacional:</strong>
                    <br />
                    <asp:Image alt="Tabela Internacional" class="responsive-image" runat="server" ImageUrl="~/UserTemplate/img/Captura de ecrã 2024-09-04 115022.png" />
                    <br />
                    <strong>Internacional:</strong>
                    <br />
                    <asp:Image alt="Tabela Nacional" class="responsive-image" runat="server" ImageUrl="~/UserTemplate/img/Captura de ecrã 2024-09-04 115935.png" />
                </p>
                <p class="text-justify">
                    <strong>Pagamento</strong><br />
                    De momento, não temos métodos de pagamento diretamente associados ao website. Permitimos pagamentos por MBWay, cujos 
                    dados serão enviados por email assim que a encomenda for confirmada. O processamento da encomenda só começará após receção e confirmação de 
                    pagamento. 
                </p>
                <p class="text-justify">
                    Dispomos também de pagamento em dinheiro, em encomendas para levantamento em loja.
                </p>
            </div>
        </div>
    </div>
</asp:Content>
