﻿<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="Informacoes.aspx.cs" Inherits="pap.User.Informacoes" %>
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
                
        .info-section,
        .social-section,
        .timetable-section{
            flex: 1; 
            margin: 0 10px; 
        }

        .timetable-section{
            max-width:500px;
        }

        .info-section {
            max-width: 500px; 
        }

        .social-section {
            max-width: 500px; 
        }

        .d-flex {
            display: flex;
            flex-direction: row; 
            justify-content: center;
        }

        .d-flex > div {
            text-align: center;
        }

        @media (max-width: 992px) {
            .d-flex {
                flex-direction: column;
                align-items: center;
            }

            .info-section,
            .social-section,
            .timetable-section{
                max-width: 100%; 
                margin: 0; 
            }
        }

    </style>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons/font/bootstrap-icons.css" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="container-fluid mb-5">
        <div class="d-flex flex-column align-items-center">
            <h1 class="font-weight-semi-bold text-uppercase mb-3">INFORMAÇÕES</h1>
        </div>
    </div>

    <div class="container-fluid justify-content-center">
        <div class="container-fluid">
            <div class="d-flex flex-wrap">
                <div class="info-section mb-3">
                    <h5 class="font-weight-semi-bold mb-3">Informações</h5>
                    <p class="mb-2"><i class="fa fa-map-marker-alt text-primary mr-3"></i><a class="link" target="_blank" href="https://www.google.com/maps/dir//DONNA+COLLECTION+R.+Cruz+de+Pedra+118+4700-219+Braga/@41.5469631,-8.4327652,16z/data=!4m5!4m4!1m0!1m2!1m1!1s0xd24fff2a99d7c5d:0xb0c8b84a7bf809d6">Rua Cruz de Pedra 118, 4700-219, Braga</a></p>
                    <a href="mailto:donnacollection21@gmail.com" class="link" target="_blank"><p class="mb-2"><i class="fa fa-envelope text-primary mr-3"></i>donnacollection21@gmail.com</p></a>
                    <a href="tel:+351910276923" class="link"><p class="mb-0"><i class="fa fa-phone-alt text-primary mr-3"></i>910 276 923</p></a>
                </div>

                <div class="timetable-section mb-3">
                    <h5 class="font-weight-semi-bold mb-3 justify-content-center align-content-center">Horário</h5>
                    <p class="mb-2 text-dark"><span class="font-weight-semi-bold">Terça-Feira a Sexta-Feira: </span><br />09h30m às 13h00m e das 15h00m às 18h30m</p>
                    <p class="mb-2 text-dark"><span class="font-weight-semi-bold">Sábado: </span>09h30m às 13h00m</p>
                    <p class="mb-2 text-dark"><span class="font-weight-semi-bold">Domingo e Segunda-Feira: </span>Fechado</p>
                </div>

                <div class="social-section mb-4">
                    <h5 class="font-weight-semi-bold mb-3">Redes Sociais</h5>
                    <a href="https://www.facebook.com/DONNA.COLLETION/" target="_blank" class="link"><i class="fab fa-facebook" style="color: #d49c94"></i>&nbsp Facebook</a>
                    <br />
                    <a href="https://wa.me/910276923" target="_blank" class="link"><i class="fab fa-whatsapp" style="color: #d49c94"></i>&nbsp Whatsapp</a>
                    <br />
                    <a href="https://www.instagram.com/_donna.colletion_/" target="_blank" class="link"><i class="fab fa-instagram" style="color: #d49c94"></i>&nbsp Instagram</a>
                    <br />
                    <a href="https://www.tiktok.com/@donna.colletion?is_from_webapp=1&sender_device=pc" target="_blank" class="link"><i class="bi bi-tiktok" style="color: #d49c94"></i>&nbsp TikTok</a>
                </div>
            </div>
            <div class="text-center justify-content-center">
                <h5 class="font-weight-semi-bold mb-3">Direções</h5>
                <div class="embed-responsive embed-responsive-16by9">
                    <iframe class="embed-responsive-item" src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2986.029289100538!2d-8.435340124112857!3d41.54696307128006!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0xd24fff2a99d7c5d%3A0xb0c8b84a7bf809d6!2sDONNA%20COLLECTION!5e0!3m2!1spt-PT!2spt!4v1707336401980!5m2!1spt-PT!2spt" style="border:0;" allowfullscreen="true" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>        
                </div>
            </div>
          </div>
    </div>
</asp:Content>
