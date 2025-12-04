<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register_Member_Share_Link_Product_Details.aspx.cs" Inherits="Register_Member_Share_Link_Product_Details" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">

    <!-- Google Fonts -->
    <link href="https://fonts.googleapis.com/css?family=Poppins:200i,300,300i,400,400i,500,500i,600,600i,700,700i,800,800i,900,900i&display=swap" rel="stylesheet">

    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="css/bootstrap.min.css">
    <!-- Nice Select CSS -->
    <link rel="stylesheet" href="css/nice-select.css">
    <!-- Font Awesome CSS -->
    <link rel="stylesheet" href="css/font-awesome.min.css">
    <!-- icofont CSS -->
    <link rel="stylesheet" href="css/icofont.css">
    <!-- Slicknav -->
    <link rel="stylesheet" href="css/slicknav.min.css">
    <!-- Owl Carousel CSS -->
    <link rel="stylesheet" href="css/owl-carousel.css">
    <!-- Datepicker CSS -->
    <link rel="stylesheet" href="css/datepicker.css">
    <!-- Animate CSS -->
    <link rel="stylesheet" href="css/animate.min.css">
    <!-- Magnific Popup CSS -->
    <link rel="stylesheet" href="css/magnific-popup.css">

    <!-- Medipro CSS -->
    <link rel="stylesheet" href="css/normalize.css">
    <link rel="stylesheet" href="css/style.css">
    <link rel="stylesheet" href="css/responsive.css">
    <link rel="stylesheet" href="css/sweetalert.min.css" />

    <link href="css/product_details.css" rel="stylesheet" />

    <style>

        body{
            background: #f2f1f1;
        }

        .container{
            border: 1px solid #efefef;
            border-radius: 10px;
            background-color: white;
        }

        .header-nav-menu {
            /* add background styles here */
            height: 36px;
            background-size: 100%;
            line-height: 36px;
            margin-bottom: 10px;
        }

        .color-option {
            display: inline-block;
            cursor: pointer;
            margin: 2px;
        }

            .color-option img {
                width: 66px;
                border: 2px solid transparent; /* Initial border style */
            }

        .selected {
            border-color: #000; /* Change this to the color you want when selected */
        }

        input[type="radio"] {
            width: 0px;
        }

        .variationstyle {
            display: inline-block;
            margin: 5px;
            border: 1px solid rgba(0, 0, 0, .125);
            border-top-width: 1px !important;
            cursor: pointer;
        }

        label {
            margin-bottom: 0px;
        }

        .cursor-pointer {
            cursor: pointer;
        }
    </style>

    <style>
        #loadingOverlay {
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-color: rgb(253 251 251 / 80%); /* semi-transparent background color */
            display: flex;
            justify-content: center;
            align-items: center;
            z-index: 999; /* Ensure it overlays other content */
        }

        .spinner-border {
            width: 3rem;
            height: 3rem;
        }

        .spinnercolor {
            color: #e03cce;
            font-size: 60px;
        }
    </style>

    <style>
        .product-small-img img {
            height: 92px;
            margin: 10px 10px;
            cursor: pointer;
            display: block;
            opacity: .6;
        }

            .product-small-img img:hover {
                opacity: 1;
            }

        .product-small-img {
            float: left;
        }

        .img-container img {
            height: 450px;
        }

        .img-container {
            float: right;
            margin: 0 auto;
        }

        .product_title {
            font-size: 30px;
            font-weight: bold;
        }

        .font_black {
            color: black;
        }

        .description_title {
            font-size: 1.5rem;
            font-weight: bold;
        }

        .product_price {
            font-size: 15px;
        }

        .qty_box {
            background-color: white;
            color: black !important;
            width: 60px;
            height: 30px;
            margin: 0;
            padding: 0 !important;
            border: 1px solid #000000 !important;
            border-radius: 0px !important;
        }

        .not_available {
            position: relative;
            display: inline-block;
            background: #cdc1c1;
            color: #000;
            border: none;
            border-radius: 0;
            text-transform: unset;
            padding: 1.25rem 2.5rem;
            font-size: 1rem;
            cursor: pointer;
            transform: translateZ(0);
            transition: color 0.3s ease;
            letter-spacing: 0.0625rem;
        }

        .underline {
            text-decoration: underline;
        }

        .package_style {
            border: 1px solid #dad6d6;
            border-radius: 10px;
            padding: 10px;
        }

        ul {
            list-style: disc;
            margin-left: 1rem;
        }


        .product_details_margin{
            margin: 4rem 0 !important;
        }

        .nav-tabs .nav-item {
            margin-right:0px;
        }

        .nav-tabs .nav-link {
            color: #555; /* Default inactive color */
            background-color: #bab3b324;
            border-radius: 0px;
        }

        .nav-tabs .nav-link.active {
            color: #007bff; /* Active tab color (default Bootstrap primary color) */
            background-color: #fff; /* Active tab background color */
            border-color: #dee2e6 #dee2e6 #fff; /* Active tab border colors */
        }

        .nav-tabs .nav-link:hover {
            color: #0056b3; /* Color when hovering over inactive tabs */
        }

        /* Define keyframes for zoom animation */
        @keyframes zoom {
            0% {
                transform: scale(1);
            }

            50% {
                transform: scale(1.1);
            }
            /* Zoom to 1.1x */
            100% {
                transform: scale(1);
            }
            /* Return to normal size */
        }

        /* Apply the animation to the image */
        .img-container img {
            animation: zoom 1.5s ease; /* Use the defined animation */
        }

        /* Apply zoom effect on hover */
        .img-container:hover img {
            transform: scale(1.1); /* Zoom to 1.1x on hover */
        }
        
        /* Apply the animation to the image */
        .small-img img {
            animation: zoom 1.5s ease; /* Use the defined animation */
        }

        /* Apply zoom effect on hover */
        .small-img:hover img {
            transform: scale(1.1); /* Zoom to 1.1x on hover */
        }

        .carousel-control-prev {
            left: -9px;
            background-color: #0000002b;
        }

        .carousel-control-next {
            right: 5px;
            background-color: #0000002b;
        }

        .carousel-control-next, .carousel-control-prev {
            width: 8%;
        }

        @media only screen and (max-width: 700px) {

            .carousel-control-prev {
                left: -10px;
                background-color: #0000002b;
            }

            .carousel-control-next {
                right: -9px;
                background-color: #0000002b;
            }

            .nav-tabs {
                display: flex;
                flex-wrap: nowrap;
                overflow-x: auto;
                -webkit-overflow-scrolling: touch; /* Smooth scrolling for iOS */
            }

                .nav-tabs .nav-item {
                    flex: 0 0 auto;
                }
        }

        @media only screen and (max-width: 500px) {

            .img-container img {
                height: 300px;
            }

            .product-small-img img {
                height: 75px;
                margin: 10px 10px;
                cursor: pointer;
                display: block;
                opacity: .6;
            }

            .description_title {
                font-size: 1.5rem;
                font-weight: bold;
            }

            .product_price {
                font-size: 13px;
            }
        }

        @media only screen and (max-width: 300px) {

            .img-container img {
                height: 250px;
            }

            .product-small-img img {
                height: 60px;
                margin: 10px 10px;
                cursor: pointer;
                display: block;
                opacity: .6;
            }

            .description_title {
                font-size: 1rem;
                font-weight: bold;
            }

            .qty_box {
                background-color: white;
                color: black !important;
                width: 40px;
                height: 30px;
                margin: 0;
                padding: 0 !important;
                border: 1px solid #000000 !important;
                border-radius: 0px !important;
            }
        }
    </style>

    <script>
        function myFunction(smallImg) {
            var fullImg = document.getElementById("imageBox");
            fullImg.src = smallImg.src;
        }

        function Set_First_Image(first_img) {
            var fullImg = document.getElementById("imageBox");
            fullImg.src = first_img;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>

        <div id="loadingOverlay" style="display: none;">
            <div class="spinner-border text-primary" role="status">
                <i class="fa fa-spinner fa-spin spinnercolor"></i>
            </div>
        </div>

        <div class="container mt-2 mb-2 pl-4 pr-4">
            <div class="row" style="border-bottom: 1px solid #efefef;">
                <div class="col-12 p-0">
                    <div onclick="BackToPreviousPage()">
                        <label style="padding: 7px 15px; margin: 10px 0px; background-color: #232020; color: white; border-radius: 5px;">Back</label></div>
                </div>
            </div>

            <div class="row grid product">
                <div class="column-xs-12 column-md-6">
                    <div class="product-gallery d-flex">
                        <div class="img-container">
                            <img id="imageBox" src="" alt="product image">
                        </div>
                    </div>
                    <div>
                        <%--<div class="product-small-img d-flex">
                            <asp:Repeater runat="server" ID="rpt_product_image" OnItemDataBound="rpt_product_image_ItemDataBound">
                                <ItemTemplate>
                                    <img id="product_img" runat="server" src="" alt="product image" onclick="myFunction(this)">
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>--%>
                        <div id="carouselExampleControls" class="carousel slide" data-ride="carousel">
                            <div class="carousel-inner">
                                <asp:Repeater runat="server" ID="rptCarousel" OnItemDataBound="rptCarousel_ItemDataBound">
                                    <ItemTemplate>
                                        <div class="carousel-item <%# Container.ItemIndex == 0 ? "active" : "" %>">
                                            <div class="row">
                                                <asp:Repeater runat="server" ID="rptImages" OnItemDataBound="rptImages_ItemDataBound">
                                                    <ItemTemplate>
                                                        <div class="col-md-3 col-3 small-img">
                                                            <img id="product_img" runat="server" class="d-block w-100" src="" alt="slide" onclick="myFunction(this)">
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            <a class="carousel-control-prev" href="#carouselExampleControls" role="button" data-slide="prev">
                                <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                                <span class="sr-only">Previous</span>
                            </a>
                            <a class="carousel-control-next" href="#carouselExampleControls" role="button" data-slide="next">
                                <span class="carousel-control-next-icon" aria-hidden="true"></span>
                                <span class="sr-only">Next</span>
                            </a>
                        </div>
                    </div>
                </div>
                <div class="column-xs-12 column-md-6 product_details_margin font_black">
                    <div class="row m-0">
                        <div class="col-12" style="border-bottom: 0.0625rem solid #e3dddd; padding-bottom: 0.5rem; margin-bottom: 0.5rem;">
                            <asp:Label runat="server" ID="lbl_product_name" CssClass="product_title"></asp:Label>
                        </div>
                    </div>

                    <div class="row mb-3" runat="server" id="div_dc">
                        <div class="col-12">
                            <span id="lbl_263"></span>&nbsp;<asp:Label runat="server" CssClass="font-weight-bold" ID="lbl_user_dc" Style="font-size: 14px;"></asp:Label>
                        </div>
                    </div>
                    <div class="mb-3 mt-2">
                        <div class="row">
                            <div class="col-6 m-auto">
                                <span id="lbl_264"></span>&nbsp;<asp:Label runat="server" ID="lbl_retail_price" CssClass="product_price"></asp:Label>
                                <asp:HiddenField runat="server" ID="hdn_retail_price" />
                                <asp:HiddenField runat="server" ID="hdn_promotion" />
                                <asp:HiddenField runat="server" ID="hdn_promotion_price" />
                                <asp:HiddenField runat="server" ID="hdn_promotion_dc" />
                                <asp:HiddenField runat="server" ID="hdn_total_dc" />
                                <asp:HiddenField runat="server" ID="hdn_total_quantity" />
                            </div>
                            <div class="col-6" style="display: flex; align-items: center">
                                <button type="button" id="btndecrease" onclick="adjustAndValidateQuantity(this, 'decrease', 'txtQuantity1')" style="background-color: white; color: black; width: 60px; height: 30px; margin: 0; padding: 0; border: 1px solid #000000; border-radius: 0px;">-</button>
                                <asp:TextBox ID="txtQuantity1" Text="1" onblur="adjustAndValidateQuantity(this, 'validate', 'txtQuantity1')" onkeypress="return isNumberKey(event)" CssClass="text-center qty_box" runat="server"></asp:TextBox>
                                <button type="button" id="btnincrease" onclick="adjustAndValidateQuantity(this, 'increase', 'txtQuantity1')" style="background-color: white; color: black; width: 60px; height: 30px; margin: 0; padding: 0; border: 1px solid #000000; border-radius: 0px;">+</button>
                            </div>
                            <div class="col-12 m-auto">
                                <span>BV :</span>&nbsp;<asp:Label runat="server" ID="lbl_retail_price_bv" CssClass="product_price"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="mb-4" runat="server" id="div_promotion">
                        <div class="row">
                            <div class="col-6 m-auto">
                                <span id="lbl_265"></span>&nbsp;<asp:Label runat="server" ID="lbl_promotion_price" CssClass="product_price"></asp:Label>&nbsp;<span id="lbl_273"></span>
                            </div>
                            <div class="col-6" style="display: flex; align-items: center">
                                <button type="button" id="btnDec" onclick="adjustAndValidateQuantity(this, 'decrease', 'txtQuantity2')" style="background-color: white; color: black; width: 60px; height: 30px; margin: 0; padding: 0; border: 1px solid #000000; border-radius: 0px;">-</button>
                                <asp:TextBox ID="txtQuantity2" Text="0" onblur="adjustAndValidateQuantity(this, 'validate', 'txtQuantity2')" onkeypress="return isNumberKey(event)" CssClass="text-center qty_box" runat="server"></asp:TextBox>
                                <button type="button" id="btnInc" onclick="adjustAndValidateQuantity(this, 'increase', 'txtQuantity2')" style="background-color: white; color: black; width: 60px; height: 30px; margin: 0; padding: 0; border: 1px solid #000000; border-radius: 0px;">+</button>
                            </div>
                            <div class="col-12 m-auto">
                                <span>BV :</span>&nbsp;<asp:Label runat="server" ID="lbl_promotion_price_bv" CssClass="product_price"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="mb-4 package_style" runat="server" id="div_package">
                        <span id="lbl_329" class="underline"></span>
                        <ul class="ml-3">
                            <asp:Repeater runat="server" ID="rpt_package_item" OnItemDataBound="rpt_package_item_ItemDataBound">
                                <ItemTemplate>
                                    <li>
                                        <asp:Label runat="server" ID="lb_item_name"></asp:Label></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                    <div class="mb-2" runat="server" id="div_variation">
                        <span id="lbl_266"></span>&nbsp;<asp:Label runat="server" ID="lbl_variation_name"></asp:Label>
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <asp:RadioButtonList ID="rbtnlist_variation_value" runat="server" CssClass="variation-value-list" AutoPostBack="false"></asp:RadioButtonList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="rbtnlist_variation_value" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <p class="mb-2">
                        <div style="font-size: 14px; border: 1px solid #e4dfdf; padding: 10px; border-radius: 10px; margin-bottom: 1rem;">
                            <span id="lbl_267"></span>&nbsp;<asp:Label runat="server" ID="lbl_total_quantity"></asp:Label>
                            <br />
                            <span id="lbl_268"></span>&nbsp;<asp:Label runat="server" ID="lbl_total_amount"></asp:Label>
                            <br />
                            <span id="lbl_269"></span>&nbsp;<asp:Label runat="server" ID="lbl_point_balance"></asp:Label>
                            <div id="div_insufficient_text" style="width: 100%; line-height: 1; display: none;">
                                <span style="font-size: 11px; color: red;" id="lbl_270">* Insufficient DC in your account, please make sure your DC is enough before add to cart.</span>
                            </div>
                            <div id="div_no_qty" style="width: 100%; line-height: 1; display: none;">
                                <span style="font-size: 11px; color: red;" id="lbl_271">* Please select least one item before add to cart.</span>
                            </div>
                        </div>
                    </p>

                    <asp:Button runat="server" ID="btn_final_add_to_cart" OnClick="btn_final_add_to_cart_Click" Style="display: none;" />
                    <button type="button" runat="server" id="btn_add_to_cart" onclick="AddToCart()" class="add-to-cart w-100 d-none">Add To Cart</button>
                    <button type="button" runat="server" id="btn_sold_out" disabled="disabled" class="add-to-cart w-100 d-none">Sold Out</button>
                    <button type="button" runat="server" id="btn_not_available" disabled="disabled" class="add-to-cart w-100 d-none">Not Available</button>

                </div>
            </div>

            <%--<div class="row related-products">
                <div class="col-12 mt-3 mb-3">
                    <span class="description_title" id="lbl_274">Product Description</span>
                </div>
                <div class="col-12">
                    <asp:Label runat="server" ID="lbl_product_description"></asp:Label>
                </div>
            </div>--%>

        <div class="row mt-3">
            <!-- Nav tabs -->
            <ul class="nav nav-tabs w-100" id="myTab" role="tablist">
                <li class="nav-item">
                    <a class="nav-link active" id="product-description-tab" data-toggle="tab" href="#product-description" role="tab" aria-controls="product-description" aria-selected="true">Product Description</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" runat="server" id="ingredients_tab" data-toggle="tab" href="#ingredients" role="tab" aria-controls="ingredients" aria-selected="false">Ingredients</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" runat="server" id="how_to_use_tab" data-toggle="tab" href="#how-to-use" role="tab" aria-controls="how-to-use" aria-selected="false">How To Use</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" runat="server" id="more_info_tab" data-toggle="tab" href="#more-info" role="tab" aria-controls="more-info" aria-selected="false">More Info</a>
                </li>
            </ul>

            <!-- Tab panes -->
            <div class="tab-content" id="myTabContent">
                <div class="tab-pane fade show active" id="product-description" role="tabpanel" aria-labelledby="product-description-tab">
                    <div class="col-12 p-3">
                        <asp:Label runat="server" ID="lbl_product_description"></asp:Label>
                    </div>
                </div>
                <div class="tab-pane fade" id="ingredients" role="tabpanel" aria-labelledby="ingredients_tab">
                    <div class="col-12 p-3">
                        <asp:Label runat="server" ID="lbl_ingredients"></asp:Label>
                    </div>
                </div>
                <div class="tab-pane fade" id="how-to-use" role="tabpanel" aria-labelledby="how_to_use_tab">
                    <div class="col-12 p-3">
                        <asp:Label runat="server" ID="lbl_how_to_use"></asp:Label>
                    </div>
                </div>
                <div class="tab-pane fade" id="more-info" role="tabpanel" aria-labelledby="more_info_tab">
                    <div class="col-12 p-3">
                        <asp:Label runat="server" ID="lbl_more_info"></asp:Label>
                    </div>
                </div>
            </div>
        </div>

        </div>

        <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

        <!-- jquery Min JS -->
        <script src="js/jquery.min.js"></script>
        <!-- jquery Migrate JS -->
        <script src="js/jquery-migrate-3.0.0.js"></script>
        <!-- jquery Ui JS -->
        <script src="js/jquery-ui.min.js"></script>
        <!-- Easing JS -->
        <script src="js/easing.js"></script>
        <!-- Color JS -->
        <script src="js/colors.js"></script>
        <!-- Popper JS -->
        <script src="js/popper.min.js"></script>
        <!-- Bootstrap Datepicker JS -->
        <script src="js/bootstrap-datepicker.js"></script>
        <!-- Jquery Nav JS -->
        <script src="js/jquery.nav.js"></script>
        <!-- Slicknav JS -->
        <script src="js/slicknav.min.js"></script>
        <!-- ScrollUp JS -->
        <script src="js/jquery.scrollUp.min.js"></script>
        <!-- Niceselect JS -->
        <script src="js/niceselect.js"></script>
        <!-- Tilt Jquery JS -->
        <script src="js/tilt.jquery.min.js"></script>
        <!-- Owl Carousel JS -->
        <script src="js/owl-carousel.js"></script>
        <!-- counterup JS -->
        <script src="js/jquery.counterup.min.js"></script>
        <!-- Steller JS -->
        <script src="js/steller.js"></script>
        <!-- Wow JS -->
        <script src="js/wow.min.js"></script>
        <!-- Magnific Popup JS -->
        <script src="js/jquery.magnific-popup.min.js"></script>
        <!-- Counter Up CDN JS -->
        <script src="http://cdnjs.cloudflare.com/ajax/libs/waypoints/2.0.3/waypoints.min.js"></script>
        <!-- Bootstrap JS -->
        <script src="js/bootstrap.min.js"></script>
        <!-- Main JS -->
        <script src="js/main.js"></script>
        <script src="js/sweetalert.min.js"></script>

        <script>
            const activeImage = document.querySelector(".product-image .active");
            const productImages = document.querySelectorAll(".image-list img");
            const navItem = document.querySelector('a.toggle-nav');

            function changeImage(e) {
                activeImage.src = e.target.src;
            }

            function toggleNavigation() {
                this.nextElementSibling.classList.toggle('active');
            }

            productImages.forEach(image => image.addEventListener("click", changeImage));
            navItem.addEventListener('click', toggleNavigation);
        </script>

        <script type="text/javascript">

            $(document).ready(function () {
                Load_Language();
                $("tr").addClass("variationstyle");
                CheckFirstVariation();
            });

            function getCookieValue(cookieName) {
                var name = cookieName + "=";
                var decodedCookie = decodeURIComponent(document.cookie);
                var cookieArray = decodedCookie.split(";");

                for (var i = 0; i < cookieArray.length; i++) {
                    var cookie = cookieArray[i].trim();

                    if (cookie.indexOf(name) === 0) {
                        return cookie.substring(name.length, cookie.length);
                    }
                }

                return "";
            }

            function Load_Language() {
                var page = 'Product Details';
                var language = 'English';
                var cookies_language = getCookieValue("language");
                if (cookies_language) {
                    language = cookies_language;
                }

                $.ajax({
                    type: "POST",
                    url: "Language.asmx/Load_Language",
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({ page: page, language: language }),
                    success: function (response) {
                        var responseData = JSON.parse(response.d); // Parse the response JSON string
                        responseData.forEach(function (item) {
                            if (item.Label_Type === 'Label') {
                                $('#' + item.Label_Name).text(item.Language_Content);
                            } else if (item.Label_Type === 'Placeholder') {
                                var element = document.getElementById(item.Label_Name);
                                element.placeholder = item.Language_Content;
                            } else if (item.Label_Type === 'Button') {
                                var element = document.getElementById('ContentPlaceHolder1_' + item.Label_Name);
                                if (element) {
                                    if (element.innerText === 'ADD TO CART') {
                                        element.textContent = item.Language_Content;
                                    } else {
                                        element.textContent = item.Language_Content;
                                    }
                                }
                            } else {
                                window[item.Label_Name] = item.Language_Content;
                            }
                        });
                    }
                });
            }

            function BackToPreviousPage() {
                var member_id = getParameterByName('member_id');
                var referral_id = getParameterByName('referral_id');
                window.location.href = "Register_Member_Share_Link_Product_List.aspx?id=" + member_id + "&referral_id=" + referral_id;
            }

            function getParameterByName(name, url = window.location.href) {
                name = name.replace(/[\[\]]/g, '\\$&');
                var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
                    results = regex.exec(url);
                if (!results) return null;
                if (!results[2]) return '';
                return decodeURIComponent(results[2].replace(/\+/g, ' '));
            }

            function sweetalert_success(message, messagetype) {
                swal({
                    title: message,
                    type: "success"
                });
            }

            function sweetalert_warning(message, messagetype) {
                swal({
                    title: message,
                    icon: "warning",
                    button: "OK",
                });
            }

            // Function to decrease quantity
            function decreaseQuantity(btnMinus) {
                var repeaterItem = btnMinus.closest('.col-md-4');
                var quantityTextBox = repeaterItem.querySelector('.text-center');

                var currentQuantity = parseInt(quantityTextBox.value, 10);
                if (currentQuantity > 1) {
                    quantityTextBox.value = currentQuantity - 1;
                }
            }

            // Function to increase quantity
            function increaseQuantity(btnPlus) {
                var repeaterItem = btnPlus.closest('.col-md-4');
                var quantityTextBox = repeaterItem.querySelector('.text-center');

                var currentQuantity = parseInt(quantityTextBox.value, 10);
                quantityTextBox.value = currentQuantity + 1;
            }

            function AddToCart() {
                var divInsufficient = document.getElementById("div_insufficient_text");
                var div_no_qty = document.getElementById("div_no_qty");
                var divStyle = window.getComputedStyle(divInsufficient);
                var div_no_qty_style = window.getComputedStyle(div_no_qty);

                if (divStyle.display === "block") {
                    // Do nothing, insufficient text is displayed
                }
                else if (div_no_qty_style.display === "block") {
                    // Do nothing, insufficient text is displayed
                } else {
                    document.getElementById("btn_final_add_to_cart").click();
                }
            }

            function CheckFirstVariation() {
                // Check if any radio button is checked
                var anyChecked = $(".variation-value-list input[type='radio']:checked").length > 0;

                // If none are checked, check the last child
                if (!anyChecked) {
                    $(".variation-value-list input[type='radio']").first().prop('checked', true).change();
                }

                if (document.getElementById("lbl_point_balance").style.display == "none") {
                    document.getElementById("lbl_269").style.display = "none";
                }

                var arrayvariation = [];

                var newrepeater = "rbtnlist_variation_value";
                var table = document.getElementById(newrepeater);
                for (var i = 0, row; row = table.rows[i]; i++) {
                    for (var j = 0, cell; cell = row.cells[j]; j++) {
                        if (cell.firstChild.type == "radio") {
                            if (cell.firstChild.checked) {
                                var selectedvalue = cell.firstChild.value; // Assuming the value you need is in the radio button itself
                                row.style.border = "1px solid black";
                                cell.style.color = "black";
                                arrayvariation.push(selectedvalue);
                            } else {
                                row.style.border = "1px solid rgba(0, 0, 0, .125)";
                                cell.style.color = "#666";
                            }
                        }
                    }
                }
                var id = getParameterByName('id');
                $.ajax({
                    type: "POST",
                    url: "Register_Member_Share_Link_Product_Details.aspx/ProcessArray",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({ myArray: arrayvariation, id: id }),
                    success: function (response) {
                        var responseData = JSON.parse(response.d); // Parse the response JSON string
                        for (var i = 0; i < responseData.variation_details.length; i++) {
                            var variation_details = responseData.variation_details[i];

                            document.getElementById("lbl_retail_price").innerText = "RM " + numberWithCommas(parseFloat(variation_details.Variation_Retail_Price).toFixed(2));
                            document.getElementById("lbl_retail_price_bv").innerText = numberWithCommas(parseFloat(variation_details.Variation_Retail_Price_BV_Points).toFixed(2));

                            var currentDate = new Date();
                            var dateWithOffset = new Date(currentDate.setHours(currentDate.getHours() + 8));

                            var promotionDurationFrom = new Date(variation_details.Variation_Promotion_Duration_From);
                            var promotionDurationTo = new Date(variation_details.Variation_Promotion_Duration_To);

                            if (variation_details.Variation_Promotion == "Y" && (dateWithOffset > promotionDurationFrom) && (dateWithOffset < promotionDurationTo)) {
                                document.getElementById("div_promotion").style.display = "block";
                                document.getElementById("lbl_promotion_price").innerText = "RM " + numberWithCommas(parseFloat(variation_details.Variation_Promotion_Price_Selling_Price).toFixed(2)) + " + " + variation_details.Variation_Promotion_Price_EC_Points;
                                document.getElementById("lbl_promotion_price_bv").innerText = numberWithCommas(parseFloat(variation_details.Variation_Promotion_Price_BV_Points).toFixed(2));
                                document.getElementById("lbl_point_balance").style.display = "contents";
                                document.getElementById("lbl_269").style.display = "contents";
                            } else {
                                document.getElementById("div_promotion").style.display = "none";
                                document.getElementById("lbl_point_balance").style.display = "none";
                                document.getElementById("lbl_269").style.display = "none";
                                document.getElementById("txtQuantity2").value = "0";
                            }
                        }
                    },
                    error: function (response) {
                        console.log(response.responseText);
                    }
                });

                function numberWithCommas(x) {
                    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                }

            }

            function VariationSeleted() {
                $("#loadingOverlay").show();
                setTimeout(function () {

                    var arrayvariation = [];

                    var newrepeater = "rbtnlist_variation_value";
                    var table = document.getElementById(newrepeater);
                    for (var i = 0, row; row = table.rows[i]; i++) {
                        for (var j = 0, cell; cell = row.cells[j]; j++) {
                            if (cell.firstChild.type == "radio") {
                                if (cell.firstChild.checked) {
                                    var selectedvalue = cell.firstChild.value; // Assuming the value you need is in the radio button itself
                                    row.style.border = "1px solid black";
                                    cell.style.color = "black";
                                    console.log(selectedvalue);
                                    arrayvariation.push(selectedvalue);
                                } else {
                                    row.style.border = "1px solid rgba(0, 0, 0, .125)";
                                    cell.style.color = "#666";
                                }
                            }
                        }
                    }
                    var id = getParameterByName('id');
                    $.ajax({
                        type: "POST",
                        url: "Register_Member_Share_Link_Product_Details.aspx/ProcessArray",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: JSON.stringify({ myArray: arrayvariation, id: id }),
                        success: function (response) {
                            $("#loadingOverlay").hide();

                            var responseData = JSON.parse(response.d); // Parse the response JSON string
                            for (var i = 0; i < responseData.variation_details.length; i++) {
                                var variation_details = responseData.variation_details[i];

                                document.getElementById("lbl_retail_price").innerText = "RM " + numberWithCommas(parseFloat(variation_details.Variation_Retail_Price).toFixed(2));

                                var currentDate = new Date();
                                var dateWithOffset = new Date(currentDate.setHours(currentDate.getHours() + 8));

                                var promotionDurationFrom = new Date(variation_details.Variation_Promotion_Duration_From);
                                var promotionDurationTo = new Date(variation_details.Variation_Promotion_Duration_To);

                                if (variation_details.Variation_Promotion == "Y" && (dateWithOffset > promotionDurationFrom) && (dateWithOffset < promotionDurationTo)) {
                                    document.getElementById("div_promotion").style.display = "block";
                                    document.getElementById("lbl_promotion_price").innerText = "RM " + numberWithCommas(parseFloat(variation_details.Variation_Promotion_Price_Selling_Price).toFixed(2)) + " + " + variation_details.Variation_Promotion_Price_EC_Points;
                                    document.getElementById("lbl_point_balance").style.display = "contents";
                                    document.getElementById("lbl_269").style.display = "contents";
                                } else {
                                    document.getElementById("div_promotion").style.display = "none";
                                    document.getElementById("lbl_point_balance").style.display = "none";
                                    document.getElementById("lbl_269").style.display = "none";
                                    document.getElementById("txtQuantity2").value = "0";
                                }

                                document.getElementById("hdn_promotion_price").value = variation_details.Variation_Promotion;
                                document.getElementById("hdn_retail_price").value = variation_details.Variation_Retail_Price;
                                document.getElementById("hdn_promotion_price").value = variation_details.Variation_Promotion_Price_Selling_Price;
                                document.getElementById("hdn_promotion_dc").value = variation_details.Variation_Promotion_Price_EC_Points;
                                var promotion_qty_element = document.getElementById("txtQuantity2");
                                var qty = "";
                                if (promotion_qty_element) {
                                    qty = parseFloat(document.getElementById("txtQuantity1").value) + parseFloat(document.getElementById("txtQuantity2").value);
                                } else {
                                    qty = parseFloat(document.getElementById("txtQuantity1").value);
                                }
                                document.getElementById("hdn_total_quantity").value = qty;
                            }
                            validateQuantity_Modal();
                        },
                        error: function (response) {
                            $("#loadingOverlay").hide();

                            console.log(response.responseText);
                        }
                    });

                    function numberWithCommas(x) {
                        return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                    }
                }, 500); // 1000 milliseconds = 1 second
            }

            function getParameterByName(name, url = window.location.href) {
                name = name.replace(/[\[\]]/g, '\\$&');
                var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
                    results = regex.exec(url);
                if (!results) return null;
                if (!results[2]) return '';
                return decodeURIComponent(results[2].replace(/\+/g, ' '));
            }
        </script>

        <script type="text/javascript">

            function validateQuantity(input) {
                // Remove leading zeros and non-numeric characters
                var sanitizedValue = input.value.replace(/^0+|[^\d]/g, '');

                // Set the value to 1 if it's empty or starts with 0
                input.value = sanitizedValue === '' || sanitizedValue.startsWith('0') ? '1' : sanitizedValue;
            }

            function adjustAndValidateQuantity(button, action, textboxId) {
                var repeaterItem = button.parentNode;
                var txtQuantity = repeaterItem.querySelector('[id$="' + textboxId + '"]');

                if (txtQuantity) {
                    if (txtQuantity.value === '') {
                        txtQuantity.value = 0;
                    }

                    var currentValue = parseInt(txtQuantity.value, 10);

                    // Determine the action to perform
                    if (action === 'increase') {
                        currentValue++;
                    } else if (action === 'decrease') {
                        if (currentValue > 0) {
                            currentValue--;
                        } else {
                            currentValue = "0";
                        }
                    }

                    // Update the textbox value
                    txtQuantity.value = currentValue;

                    // Validate the updated quantity
                    validateQuantity_Modal();
                }
            }

            function validateQuantity_Modal() {
                //Retail Price
                var retail_qty = document.getElementById("txtQuantity1").value;
                var retail_price = document.getElementById("hdn_retail_price").value;
                var retail_total_price = "0";
                var retail_total_price = (parseFloat(retail_qty) * parseFloat(retail_price));
                var retail_total_qty = document.getElementById("txtQuantity1").value;
                var promotion_qty = "0";
                var promotion_price = "0";
                var promotion_total_price = "0";
                var promotion_total_qty = "0";
                var promotion_dc = "0";
                //Retail Price

                var promotion_qty_element = document.getElementById("txtQuantity2");

                if (promotion_qty_element) {
                    var promotion_qty = document.getElementById("txtQuantity2").value;
                    if (promotion_qty != "0") {
                        //Promotion Price
                        promotion_price = document.getElementById("hdn_promotion_price").value;
                        promotion_dc = document.getElementById("hdn_promotion_dc").value;
                        var totaldc = document.getElementById("hdn_total_dc").value;
                        promotion_total_price = (parseFloat(promotion_price) * parseFloat(promotion_qty));
                        promotion_total_qty = promotion_qty;
                        promotion_dc = totaldc - (parseFloat(promotion_dc) * parseFloat(promotion_qty));
                        console.log(promotion_qty);
                        // Check if result_dc is negative and apply red color
                        if (promotion_dc < 0) {
                            document.getElementById("div_insufficient_text").style.display = "block";
                            document.getElementById("lbl_point_balance").style.color = "red";
                        } else if (promotion_dc === 0) {
                            // Set the color to black if result_dc is 0
                            document.getElementById("div_insufficient_text").style.display = "none";
                            document.getElementById("lbl_point_balance").style.color = "black";
                        } else {
                            // Reset the color to its default value if needed
                            document.getElementById("div_insufficient_text").style.display = "none";
                            document.getElementById("lbl_point_balance").style.color = ""; // or use null or the original color value
                        }
                        //Promotion Price
                    } else {
                        document.getElementById("div_insufficient_text").style.display = "none";
                        document.getElementById("lbl_point_balance").style.color = "black";
                    }
                }

                var final_price = "0";
                var final_qty = "0";
                var final_dc = "0";
                final_price = (parseFloat(retail_total_price) + parseFloat(promotion_total_price));
                final_qty = (parseFloat(retail_total_qty) + parseFloat(promotion_total_qty));
                final_dc = promotion_dc;
                document.getElementById("lbl_total_amount").innerText = "RM " + numberWithCommas(parseFloat(final_price).toFixed(2));
                document.getElementById("hdn_total_quantity").value = final_qty;
                document.getElementById("lbl_total_quantity").innerText = final_qty;
                var promotion_qty_element2 = document.getElementById("lbl_point_balance");

                if (promotion_qty_element2) {
                    document.getElementById("lbl_point_balance").innerText = final_dc;
                }

                if (final_qty == "0") {
                    document.getElementById("div_no_qty").style.display = "block";
                } else {
                    document.getElementById("div_no_qty").style.display = "none";
                }
            }

            function isNumberKey(evt) {
                var charCode = (evt.which) ? evt.which : event.keyCode;

                // Allow only digits (0-9)
                if (charCode < 48 || charCode > 57) {
                    return false;
                }

                return true;
            }

            function numberWithCommas(x) {
                return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
            }
        </script>
    </form>
</body>
</html>
