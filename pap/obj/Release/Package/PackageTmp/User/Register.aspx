<%@ Page Title="Donna Collection-Registo" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="pap.User.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<link rel="stylesheet" type="text/css" href="../UserTemplate/fonts/font-awesome-4.7.0/css/font-awesome.min.css">
	<link rel="stylesheet" type="text/css" href="../UserTemplate/fonts/iconic/css/material-design-iconic-font.min.css">
	<link rel="stylesheet" type="text/css" href="../UserTemplate/css/util.css">
	<link rel="stylesheet" type="text/css" href="../UserTemplate/css/main.css">
	<link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.10.0/css/all.min.css" rel="stylesheet">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.11.2/css/all.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="limiter">
		<div class="container-login100">
			<div class="wrap-login100">
				<form class="login100-form validate-form">
					<span class="login100-form-title p-b-34 p-t-27">
						Registar
					</span>

					<div class="wrap-input100 validate-input" data-validate = "Enter name">
						<asp:TextBox CssClass="input100" AutoCompleteType="DisplayName" required="required" runat="server" placeholder="Nome" ID="txtNome"></asp:TextBox>
						<span class="focus-input100" data-placeholder="&#xf207;"></span>
					</div>

					<div class="wrap-input100 validate-input" data-validate="Enter username">
						<asp:TextBox CssClass="input100" required="required" runat="server" placeholder="Nome de utilizador" ID="txtUsername"></asp:TextBox>
						<span class="focus-input100" data-placeholder="&#xf207;"></span>
					</div>

					<div class="wrap-input100 validate-input" data-validate = "Enter email">
						<asp:TextBox CssClass="input100" TextMode="Email" required="required" runat="server" placeholder="Email" ID="txtEmail"></asp:TextBox>
						<span class="focus-input100" data-placeholder="&#xf207;"></span>
					</div>

					<div class="wrap-input100 validate-input" data-validate="Enter password">
						<asp:TextBox TextMode="Password" CssClass="input100" required="required" runat="server" placeholder="Palavra-passe" ID="txtPassword"></asp:TextBox>
						<span class="focus-input100" data-placeholder="&#xf191;"></span>
					</div>

					<div class="wrap-input100 validate-input" data-validate="Confirm password">
						<asp:TextBox TextMode="Password" CssClass="input100" required="required" runat="server" placeholder="Confirme a Palavra-passe" ID="txtConfirmaPassword"></asp:TextBox>
						<span class="focus-input100" data-placeholder="&#xf191;"></span>
					</div>

					<div class="container-login100-form-btn">
						<asp:Button runat="server" CssClass="login100-form-btn" ID="btnRegister" Text="Registar" OnClick="btnRegister_Click" />
					</div>

					<div class="text-center p-t-27">
						<a class="txt1" href="Login.aspx">
							Já possui uma conta? Faça o seu login!
						</a>
					</div>
				</form>
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
