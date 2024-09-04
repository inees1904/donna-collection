<%@ Page Title="Donna Collection-Editar Perfil" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="EditProfile.aspx.cs" Inherits="pap.User.EditProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../UserTemplate/fonts/font-awesome-4.7.0/css/font-awesome.min.css">
	<link rel="stylesheet" type="text/css" href="../UserTemplate/fonts/iconic/css/material-design-iconic-font.min.css">
	<link rel="stylesheet" type="text/css" href="../UserTemplate/css/util.css">
	<link rel="stylesheet" type="text/css" href="../UserTemplate/css/main.css">
	<link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.10.0/css/all.min.css" rel="stylesheet">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.11.2/css/all.css">
    <script>
        window.onload = function () {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%=lblmsg.ClientID%>").style.display = "none";
            }, seconds * 1000);
        };
    </script>
    <script>
        function ImagePreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%= imagePreview.ClientID%>').prop('src', e.target.result).width(200).height(200);
                };
                reader.readAsDataURL(input.files[0]);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mb-4">
        <asp:Label ID="lblmsg" runat="server"></asp:Label>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="card" style="background-color: #f3d5d3">
                <div class="card-body">
                    <h4 class="card-title">Editar Perfil</h4>
                    <hr />
                    <div class="form-body">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Nome</label>
                                    <asp:TextBox ID="txtNome" runat="server" CssClass="input100" placeholder="Enter Name"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvNome" runat="server" ForeColor="Red" Font-Size="Small" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtNome" ErrorMessage="Name is required"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <label>Username</label>
                                    <asp:TextBox ID="txtUsername" runat="server" CssClass="input100" placeholder="Enter Username"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ForeColor="Red" Font-Size="Small" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtUsername" ErrorMessage="Username is required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Telemóvel</label>
                                    <asp:TextBox ID="txtTelemovel" TextMode="Phone" runat="server" CssClass="input100" placeholder="Enter Phone Number"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>Email</label>
                                    <asp:TextBox ID="txtEmail" TextMode="Email" runat="server" CssClass="input100" placeholder="Enter Email"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ForeColor="Red" Font-Size="Small" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtEmail" ErrorMessage="Email is required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Morada</label>
                                    <asp:TextBox ID="txtMorada" runat="server" CssClass="input100" placeholder="Enter Address"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Código Postal</label>
                                    <asp:TextBox ID="txtCodigoPostal" runat="server" CssClass="input100" placeholder="Enter Postal Code"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Imagem</label>
                                    <asp:FileUpload ID="fuImagemUrl" runat="server" CssClass="input100" onchange="ImagePreview(this);"/> 
                                </div>
                            </div>
                        </div>

                    <div class="form-action pb-5">
                        <div class="text-left">
                            <asp:Button ID="btnEditar" runat="server" CssClass="btn btn-dark" Text="Editar" OnClick="btnEditar_Click"/>
                        </div>
                    </div>

                    <div>
                        <asp:Image ID="imagePreview" runat="server" CssClass="img-thumbnail" AlternateText="" />
                    </div>

                </div>
            </div>
        </div>
    </div>
    <script src="../UserTemplate/vendor/jquery/jquery-3.2.1.min.js"></script>
	<script src="../UserTemplate/vendor/animsition/js/animsition.min.js"></script>
	<script src="../UserTemplate/vendor/bootstrap/js/popper.js"></script>
	<script src="../UserTemplate/vendor/bootstrap/js/bootstrap.min.js"></script>
	<script src="../UserTemplate/vendor/select2/select2.min.js"></script>
	<script src="../UserTemplate/vendor/daterangepicker/moment.min.js"></script>
	<script src="../UserTemplate/vendor/daterangepicker/daterangepicker.js"></script>
	<script src="../UserTemplate/vendor/countdowntime/countdowntime.js"></script>
	<script src="../UserTemplate/js/main1.js"></script>
</asp:Content>
