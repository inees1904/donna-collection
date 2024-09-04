<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="pap.User.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="header-carousel" class="carousel slide" data-ride="carousel">
    <div class="carousel-inner">
        <div class="carousel-item active" style="height: 410px;">
            <img class="img-fluid" src="../UserTemplate/img/imagem1.jpg" alt="Image">
                <div class="carousel-caption d-flex flex-column align-items-center justify-content-center">
                    <div class="p-3" style="max-width: 700px;">
                            <h3 class="display-4 text-white font-weight-semi-bold mb-4">A Nossa Coleção</h3>
                                <a href="Roupa.aspx" class="btn btn-light py-2 px-3" style="color: black;">Comprar agora</a>
                    </div>
                </div>
        </div>
        <div class="carousel-item" style="height: 410px;">
            <img class="img-fluid" src="../UserTemplate/img/449691363_18013544558419397_6084123901506776646_n.jpg" alt="Image">
                <div class="carousel-caption d-flex flex-column align-items-center justify-content-center">
                    <div class="p-3" style="max-width: 700px;">
                            <h3 class="display-4 text-white font-weight-semi-bold mb-4">Gama de Acessórios</h3>
                                <a href="Acessorios.aspx" class="btn btn-light py-2 px-3">Comprar agora</a>
                    </div>
                </div>
        </div>
    </div>
    <a class="carousel-control-prev" href="#header-carousel" data-slide="prev">
        <div class="btn btn-dark" style="width: 45px; height: 45px;">
            <span class="carousel-control-prev-icon mb-n2"></span>
        </div>
    </a>
    <a class="carousel-control-next" href="#header-carousel" data-slide="next">
        <div class="btn btn-dark" style="width: 45px; height: 45px;">
            <span class="carousel-control-next-icon mb-n2"></span>
        </div>
    </a>
</div>
    <div class="container-fluid pt-5">
        <div class="row px-xl-5 pb-3">
            <div class="col-lg-3 col-md-6 col-sm-12 pb-1">
                <div class="d-flex align-items-center mb-4" style="padding: 30px;">
                    <h1 class="fa fa-check text-primary m-0 mr-3"></h1>
                    <h5 class="font-weight-semi-bold m-0">Produto de Qualidade</h5>
                </div>
            </div>
            <div class="col-lg-3 col-md-6 col-sm-12 pb-1">
                <div class="d-flex align-items-center mb-4" style="padding: 30px;">
                    <h1 class="fa fa-shipping-fast text-primary m-0 mr-2"></h1>
                    <h5 class="font-weight-semi-bold m-0">Envio Seguro</h5>
                </div>
            </div>
            <div class="col-lg-3 col-md-6 col-sm-12 pb-1">
                <div class="d-flex align-items-center mb-4" style="padding: 30px;">
                    <h1 class="fas fa-exchange-alt text-primary m-0 mr-3"></h1>
                    <h5 class="font-weight-semi-bold m-0">Trocas e devoluções</h5>
                </div>
            </div>
            <div class="col-lg-3 col-md-6 col-sm-12 pb-1">
                <div class="d-flex align-items-center mb-4" style="padding: 30px;">
                    <h1 class="fa fa-phone-volume text-primary m-0 mr-3"></h1>
                    <h5 class="font-weight-semi-bold m-0">Apoio ao cliente</h5>
                </div>
            </div>
        </div>
    </div>

    <div class="container-fluid pt-5">
        <div class="row px-xl-5 pb-3">
            <div class="col-lg-4 col-md-6 pb-1">
                <div class="cat-item d-flex flex-column mb-4" style="padding: 30px;">
                    <a href="Roupa.aspx" class="cat-img position-relative overflow-hidden mb-3">
                        <img class="img-fluid" style="width: 400px; height: 500px;" src="../UserTemplate/img/roupa.jpg" alt="">
                    </a>
                    <h5 class="font-weight-semi-bold m-0">Roupa</h5>
                </div>
            </div>
            <div class="col-lg-4 col-md-6 pb-1">
                <div class="cat-item d-flex flex-column mb-4" style="padding: 30px;">
                    <a href="Acessorios.aspx" class="cat-img position-relative overflow-hidden mb-3">
                        <img class="img-fluid" src="../UserTemplate/img/acessorios.jpg" style="width: 400px; height: 500px;" alt="">
                    </a>
                    <h5 class="font-weight-semi-bold m-0">Acessórios</h5>
                </div>
            </div>
            <div class="col-lg-4 col-md-6 pb-1">
                <div class="cat-item d-flex flex-column mb-4" style="padding: 30px;">
                    <a href="Calcado.aspx" class="cat-img position-relative overflow-hidden mb-3">
                        <img class="img-fluid" src="../UserTemplate/img/calcado.jpg" style="width: 400px; height: 500px;" alt="">
                    </a>
                    <h5 class="font-weight-semi-bold m-0">Calçado</h5>
                </div>
            </div>
            </div>
    </div>

    <div class="container-fluid offer pt-5">
        <div class="row px-xl-5">
            <div class="elfsight-app-847c77fb-afb9-4f40-8a98-90d58488d9da justify-content-center align-content-center" data-elfsight-app-lazy=""></div>
        </div>
    </div>

     <div class="container-fluid my-5">
        <div class="row justify-content-center py-5">
            <div class="col-md-6 col-12">
                <div class="text-center mb-4">
                    <h2 class="section-title px-5 mb-3"><span style="background-color: #f3d5d3; padding: 10px;">Novidades</span></h2>
                    <p>Siga-nos nas redes sociais e confira em primeira mão as melhores novidades do mundo!</p>
                </div>
            </div>
        </div>

        <div class="row justify-content-center py-5">
            <div class="col-md-4 col-12 text-center mb-4">
                <h2 class="section-title px-5 mb-3"><span style="background-color: #f3d5d3; padding: 10px;">Instagram</span></h2>
                <div class="instagram-embed" style="width: 340px; margin: 0 auto;">
                    <blockquote class="instagram-media" data-instgrm-permalink="https://www.instagram.com/_donna.colletion_/" data-instgrm-version="13" style="height:500px; width: 340px; overflow:hidden; border: none;"></blockquote>
                    <script async src="https://www.instagram.com/embed.js"></script>
                </div>
            </div>

            <div class="col-md-4 col-12 text-center mb-4">
                <h2 class="section-title px-5 mb-3"><span style="background-color: #f3d5d3; padding: 10px;">TikTok</span></h2>
                <div class="tiktok-embed" style="width: 340px; margin: 0 auto;">
                    <blockquote class="tiktok-embed" cite="https://www.tiktok.com/@donna.colletion" data-unique-id="donna.colletion" data-embed-type="creator" style="max-width: 780px; min-width: 288px; height: 500px;" > 
                        <section> 
                            <a target="_blank" href="https://www.tiktok.com/@donna.colletion?refer=creator_embed" style="height: 500px;">@donna.colletion</a> 
                        </section> 
                    </blockquote> 
                    <script async src="https://www.tiktok.com/embed.js"></script>                
                </div>
            </div>
            <div class="col-md-4 col-12 text-center mb-4">
                <h2 class="section-title px-5 mb-3"><span style="background-color: #f3d5d3; padding: 10px;">Facebook</span></h2>
                <div class="facebook-embed">
                    <iframe src="https://www.facebook.com/plugins/page.php?href=https://www.facebook.com/DONNA.COLLETION/&tabs=timeline&width=340&height=500&small_header=true&adapt_container_width=true&hide_cover=false&show_facepile=true&appId" width="340" height="500" style="border:none;overflow:hidden" scrolling="no" frameborder="0" allowfullscreen="true" allow="autoplay; clipboard-write; encrypted-media; picture-in-picture; web-share"></iframe>
                </div>
            </div>
        </div>
    </div>

    <script src="https://static.elfsight.com/platform/platform.js" data-use-service-core defer></script>

</asp:Content>