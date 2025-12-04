<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <title>Ecentra</title>
    <meta name="description" content="Welcome to Ecentra, where passion meets purpose in our quest to source the finest wellness, beauty, and life-enhancing products from around the globe.">
    <meta charset="utf-8" accesskey="" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=no" />
    <meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1, minimum-scale=1, maximum-scale=1">
    <meta name="apple-mobile-web-app-capable" content="yes" />

    <style>
        .carousel-inner img {
            width: 100%;
            height: auto;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row m-auto">
        <div class="col-12 p-0">
            <div id="carouselExampleIndicators" class="carousel slide" data-ride="carousel">
                <ol class="carousel-indicators">
                    <asp:Repeater runat="server" ID="rpt_slider" OnItemDataBound="rpt_slider_ItemDataBound">
                        <ItemTemplate>
                            <li data-target="#carouselExampleIndicators" runat="server" id="li_slider"></li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ol>
                <div class="carousel-inner">
                    <asp:Repeater runat="server" ID="rpt_content" OnItemDataBound="rpt_content_ItemDataBound">
                        <ItemTemplate>
                            <div class="carousel-item" runat="server" id="div_content">
                                <img class="d-block w-100 img-responsive" runat="server" id="img_slider" src="img/Banner/banner.png" alt="First slide">
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <a class="carousel-control-prev" href="#carouselExampleIndicators" role="button" data-slide="prev">
                    <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                    <span class="sr-only">Previous</span>
                </a>
                <a class="carousel-control-next" href="#carouselExampleIndicators" role="button" data-slide="next">
                    <span class="carousel-control-next-icon" aria-hidden="true"></span>
                    <span class="sr-only">Next</span>
                </a>
            </div>
        </div>
    </div>
    <div class="row m-auto">
        <div class="col-12 p-0">
            <img class="d-block w-100 img-responsive" runat="server" id="img_slider" src="img/Banner/about_us_v2.png" alt="First slide">
        </div>
    </div>

    <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            // Initialize the slider
            $('.hero-slider').slick({
                // Your existing slider options
                dots: false,
                // Add other options as needed
                // ...
            });

            // Update indicators on slide change
            $('.hero-slider').on('afterChange', function (event, slick, currentSlide) {
                updateIndicators(currentSlide);
            });

            // Handle indicator click to navigate to the corresponding slide
            $('.indicator').click(function () {
                var index = $(this).data('slide-index');
                $('.hero-slider').slick('slickGoTo', index);
            });

            // Initial update of indicators
            updateIndicators(0);

            // Function to update indicators
            function updateIndicators(currentIndex) {
                $('.indicator').removeClass('active');
                $('.indicator[data-slide-index="' + currentIndex + '"]').addClass('active');
            }
        });
    </script>

</asp:Content>

