<%@ Page Title="Donna Collection-Sobre nós" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="pap.User.Contact" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .p {
            font-family: serif;
            font-size: 20px;
            color: black;
        }
        .link{color: black;}
        .link i{color: inherit;}
        .embed-responsive {position: relative; display: block; width: 100%; padding: 0; overflow: hidden;}
        .embed-responsive .embed-responsive-item,
        .embed-responsive iframe,
        .embed-responsive embed,
        .embed-responsive object,
        .embed-responsive video {position: absolute; top: 0; left: 0; bottom: 0; height: 100%; width: 100%; border: 0;}
        @media (min-width: 768px) { .embed-responsive {max-width: 600px; max-height: 400px; margin: 0 auto; }}
    </style>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons/font/bootstrap-icons.css" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid mb-5">
        <div class="d-flex flex-column align-items-center">
            <h1 class="font-weight-semi-bold text-uppercase mb-3">SOBRE NÓS</h1>
<%--            <div class="d-inline-flex">
                <p class="m-0"><a href="Default.aspx">Início</a></p>
                <p class="m-0 px-2">-</p>
                <p class="m-0">Sobre nós</p>
            </div>--%>
        </div>
    </div>

    <div class="container-fluid justify-content-center">
        <div class="col-lg-8 mb-8 text-center justify-content-center" style="margin: 0 auto">
                <div class="p-8 text-black">
                    <p class="p text-justify text-center justify-content-center">A 18 de dezembro de 2021 eu, Andreia, iniciei aquele que era o meu grande sonho. <br /><br /> A Donna Collection, acredita que a moda deve ser acessível,
                        sustentável e moderna. Somos uma loja de roupa online e física dedicada a oferecer peças de qualidade, com o compromisso inabalável e satisfação com as 
                        nossas clientes. <br /><br /> Cada peça da nossa coleção é cuidadosamente selecionada para garantir acesso às últimas tendências da moda, acreditamos que a moda pode ser 
                        bonita e responsável ao mesmo tempo. A sua satisfação é a nossa prioridade. <br /><br /> Oferecemos um serviço de atendimento personalizado, pronto para ajudar a qualquer
                        dúvida. Garantimos entregas pela transportadora CTT Expresso, para que possa desfrutar das suas peças novas o mais rápido possível. <br /><br /> Explore a nossa coleção
                        e desfrute da diferença da qualidade, junte-se a nós na Donna Collection e vista-se com confiança e estilo! <br /><br /> Com amor da família Donna,
                    </p>
                </div>
            </div>          
    </div>
</asp:Content>
