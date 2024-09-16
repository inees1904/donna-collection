<%@ Page Title="Donna Collection-Checkout" Async="true" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="pap.User.Checkout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js" integrity="sha384-6LZmNyX4b7j4B+gJzBp0+5O2J5txh2bQZ7vuCaz99ssR/fgvP9w9geU+owpW+P02" crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.6.0/js/bootstrap.min.js" integrity="sha384-+Y0J3DE/Z+2zyr2e4FI7kkgmG/w8vxSZIz9k5VU2A0JbqyFZyAaF2k5KGYIUUjAw" crossorigin="anonymous"></script>
    <style>
        .textbox-com-linha,
        .textbox-com-linha:focus ,
        .textbox-com-linha:active,
        .textbox-com-linha:-webkit-autofill{
            background: none;
            border: none;
            border-bottom: 1px solid #000; 
            outline: none; 
            width: 100%; 
            padding: 5px 0; 
            box-sizing: border-box; 
        }

        .cart-item {
            display: flex;
            justify-content: space-between;
            margin-bottom: 10px; 
        }
    </style>
    <script>
        $(document).ready(function () {
            $('#shipping-address').collapse({ toggle: false });

            $('input[name="envio"]').change(function () {
                if ($(this).attr('id') === 'rbMoradaDiferente' && $(this).is(':checked')) {
                    $('#shipping-address').collapse('show');
                } else {
                    $('#shipping-address').collapse('hide');
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid pt-5">
        <div class="row px-xl-5">
            <div class="col-lg-8">
                <div class="mb-4">
                    <h4 class="font-weight-semi-bold mb-4">Morada de Faturação</h4>
                    <div class="row">
                        <div class="col-md-6 form-group">
                            <label>Nome</label>
                            <asp:TextBox runat="server" ID="txtNome" ToolTip="Nome" CssClass="form-control textbox-com-linha"></asp:TextBox>
                        </div>
                        <div class="col-md-6 form-group">
                            <label>Email</label>
                            <asp:TextBox runat="server" ID="txtEmail" ToolTip="Email" TextMode="Email" CssClass="form-control textbox-com-linha"></asp:TextBox>
                        </div>
                        <div class="col-md-6 form-group">
                            <label>Telemóvel</label>
                            <asp:TextBox runat="server" ID="txtTelemovel" ToolTip="Telemóvel" TextMode="Phone" CssClass="form-control textbox-com-linha"></asp:TextBox>
                        </div>
                        <div class="col-md-6 form-group">
                            <label>Morada</label>
                            <asp:TextBox runat="server" ID="txtMorada" ToolTip="Morada" CssClass="form-control textbox-com-linha"></asp:TextBox>
                        </div>
                        <div class="col-md-6 form-group">
                            <label>Código Postal</label>
                            <asp:TextBox runat="server" ID="txtCodPostal" ToolTip="Código Postal" CssClass="form-control textbox-com-linha"></asp:TextBox>
                        </div>
                        <div class="col-md-6 form-group">
                            <label>NIF</label>
                            <asp:TextBox runat="server" ID="txtNif" ToolTip="NIF" CssClass="form-control textbox-com-linha"></asp:TextBox>
                        </div>
                        <div class="col-md-6 form-group">
                            <label>País</label>
                            <asp:TextBox runat="server" ID="txtPais" ToolTip="País" CssClass="form-control textbox-com-linha"></asp:TextBox>
                        </div>
                        <div class="col-md-6 form-group">
                       
                        </div>
                        <asp:UpdatePanel ID="updatePanelEnvio" runat="server" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="rbLoja" EventName="CheckedChanged" />
                                <asp:AsyncPostBackTrigger ControlID="rbMoradaFaturacao" EventName="CheckedChanged" />
                                <asp:AsyncPostBackTrigger ControlID="rbMoradaDiferente" EventName="CheckedChanged" />
                            </Triggers>
                            <ContentTemplate>
                                <div class="col-md-12 form-group">
                                    <div>
                                        <label for="rbLoja">
                                            <asp:RadioButton runat="server" AutoPostBack="true" ID="rbLoja" GroupName="envio" OnCheckedChanged="rbLoja_CheckedChanged"/>
                                            Levantar na loja
                                        </label>
                                    </div>
                                </div>
                                <div class="col-md-12 form-group">
                                    <div>
                                        <label for="rbMoradaFaturacao">
                                            <asp:RadioButton runat="server" AutoPostBack="true" ID="rbMoradaFaturacao" GroupName="envio" OnCheckedChanged="rbMoradaFaturacao_CheckedChanged"/>
                                            Enviar para a mesma morada de faturação
                                        </label>
                                    </div>
                                </div>
                                <div class="col-md-12 form-group">
                                    <div>
                                        <label for="rbMoradaDiferente" data-toggle="collapse" data-target="#shipping-address">
                                            <asp:RadioButton runat="server" ID="rbMoradaDiferente" AutoPostBack="true" GroupName="envio" OnCheckedChanged="rbMoradaDiferente_CheckedChanged"/>
                                            Enviar para uma morada diferente
                                        </label>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="collapse mb-4" id="shipping-address">
                    <h4 class="font-weight-semi-bold mb-4">Morada de envio</h4>
                    <div class="row">
                        <div class="col-md-6 form-group">
                            <label>Nome</label>
                            <asp:TextBox runat="server" ID="txtNomeEnvio" ToolTip="Nome" CssClass="form-control textbox-com-linha"></asp:TextBox>
                        </div>
                        <div class="col-md-6 form-group">
                            <label>Telémovel</label>
                            <asp:TextBox runat="server" ID="txtTelemovelEnvio" ToolTip="Telemóvel" TextMode="Phone" CssClass="form-control textbox-com-linha"></asp:TextBox>
                        </div>
                        <div class="col-md-6 form-group">
                            <label>Morada</label>
                            <asp:TextBox runat="server" ID="txtMoradaEnvio" ToolTip="Morada" CssClass="form-control textbox-com-linha"></asp:TextBox>
                        </div>
                        <div class="col-md-6 form-group">
                            <label>Código Postal</label>
                            <asp:TextBox runat="server" ID="txtCodPostalEnvio" ToolTip="Código Postal" CssClass="form-control textbox-com-linha"></asp:TextBox>
                        </div>
                    </div>
                </div>
        </div>
            <div class="col-lg-4">
                <div class="card border-secondary mb-5">
                    <div class="card-header border-0" style="background-color: #D19C97">
                        <h4 class="font-weight-semi-bold m-0">Resumo do Pedido</h4>
                    </div>
                    <div class="card-body">
                        <h5 class="font-weight-medium mb-3">Produtos</h5>
                        <asp:Literal ID="cartItemsLiteral" runat="server"></asp:Literal>
                        <hr class="mt-0">
                        <div class="d-flex justify-content-between mb-3 pt-1">
                            <h6 class="font-weight-medium">Subtotal</h6>
                            <asp:Label runat="server" CssClass="font-weight-medium align-content-right" ID="subtotal" ForeColor="Black"></asp:Label>
                        </div>
                        <div class="d-flex justify-content-between">
                            <h6 class="font-weight-medium">Envio</h6>
                            <asp:Label runat="server" CssClass="font-weight-medium align-content-right" ID="lblenvio" ForeColor="Black"></asp:Label>
                        </div>
                    </div>
                    <div class="card-footer border-secondary bg-transparent">
                        <div class="d-flex justify-content-between mt-2">
                            <h5 class="font-weight-bold">Total</h5>
                            <asp:Label runat="server" CssClass="font-weight-medium align-content-right" ID="lbltotal" ForeColor="Black"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="card border-secondary mb-5">
                    <div class="card-header border-0" style="background-color: #D19C97">
                        <h4 class="font-weight-semi-bold m-0">Pagamento</h4>
                    </div>
                    <div class="card-body">
                        <div class="form-group">
                            <asp:RadioButton runat="server" AutoPostBack="true" GroupName="payment" ID="rbDinheiro"/>
                            <label for="dinheiro">Dinheiro(Exclusivo para levantamento em loja)</label>
                        </div>
                        <div class="form-group">
                            <asp:RadioButton runat="server" AutoPostBack="true" GroupName="payment" ID="rbMbWay" />
                            <label for="rbMbWay">MBWay</label>
                        </div>
                        <div class="">
                            <asp:RadioButton runat="server" AutoPostBack="true" GroupName="payment" ID="rbTransferencia" />
                            <label for="rbTransferencia">Transferência Bancária</label>
                        </div>
                    </div>
                    <div class="card-footer border-secondary bg-transparent">
                        <asp:Button runat="server" ID="btnFinalizar" Text="Finalizar Pedido" CssClass="btn btn-lg btn-block btn-primary font-weight-bold my-3 py-3" OnClick="btnFinalizar_Click" UseSubmitBehavior="false" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
