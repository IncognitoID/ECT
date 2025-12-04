<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Product.aspx.cs" Inherits="Product" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <title>Shop Now</title>

    <meta charset="utf-8" accesskey="" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=no" />
    <meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1, minimum-scale=1, maximum-scale=1">
    <meta name="apple-mobile-web-app-capable" content="yes" />

    <style>
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

        .plusminus {
            background-color: white;
            color: black;
            width: 60px;
            height: 30px;
            margin: 0;
            padding: 0;
            border: 1px solid #000000;
            border-radius: 0px;
        }

        .imgmask {
            display: none;
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-color: white;
            opacity: 0.7;
            text-align: center;
            border: 1px solid #efefef;
        }

            .imgmask span {
                position: absolute;
                top: 50%;
                left: 50%;
                transform: translate(-50%, -50%);
                display: inline-block;
                font-size: 30px;
                position: absolute;
                background-color: white;
                font-weight: bold;
                color: #363535;
                white-space: break-spaces;
                line-height: 1.2;
            }

        .cart_note {
            font-size: 11px;
        }

        .underline {
            text-decoration: underline;
        }

        .package_style{
            border: 1px solid #dad6d6;
            border-radius: 10px;
            padding: 10px;
        }

        /* Define keyframes for zoom animation */
        @keyframes zoom {
            0% { transform: scale(1); }
            50% { transform: scale(1.1); } /* Zoom to 1.1x */
            100% { transform: scale(1); } /* Return to normal size */
        }

        /* Apply the animation to the image */
        .product_img img {
            animation: zoom 1.5s ease; /* Use the defined animation */
        }

        /* Apply zoom effect on hover */
        .product_img:hover img {
            transform: scale(1.1); /* Zoom to 1.1x on hover */
        }

        @media only screen and (max-width: 500px) {

            .product_price {
                font-size: 13px;
            }
        }

        @media only screen and (max-width: 300px) {

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

    <script type="text/javascript">
        function Show_Variation() {
            $('#productModal').modal('show');
            $("tr").addClass("variationstyle");

            var newrepeater = "ContentPlaceHolder1_rbtnlist_variation_value";
            var table = document.getElementById(newrepeater);
            for (var i = 0, row; row = table.rows[i]; i++) {
                for (var j = 0, cell; cell = row.cells[j]; j++) {
                    if (cell.firstChild.type == "radio") {
                        if (cell.firstChild.checked) {
                            row.style.border = "1px solid black";
                            cell.style.color = "black";
                        }
                    }
                }
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container p-3">
        <div class="row mt-4">
            <asp:Repeater ID="rpt_product" runat="server" OnItemDataBound="rpt_product_ItemDataBound" OnItemCommand="rpt_product_ItemCommand">
                <ItemTemplate>
                    <div class="col-md-4 col-12 mb-3">
                        <div class="card cursor-pointer">
                            <div style="position: relative; margin: auto;" class="product_img" id="div_product_img" runat="server">
                                <img runat="server" id="img_product" />
                                <div runat="server" id="div_mask" class="imgmask">
                                    <span id="lbl_mask_title" runat="server"></span>
                                </div>
                            </div>
                            <div class="card-body">
                                <div id="div_product_details" runat="server">
                                    <asp:Label CssClass="card-title font-weight-bold" runat="server" ID="lbl_product_name"></asp:Label>
                                    <br />
                                    <div class="mb-2 mt-3">
                                        <asp:Label CssClass="card-text" runat="server" ID="lbl_product_price"></asp:Label>
                                        <br />
                                        <asp:Label CssClass="card-text" runat="server" ID="lbl_product_bv"></asp:Label>
                                        <br />
                                        <asp:Label CssClass="card-text" Style="display:none" runat="server" ID="lbl_product_RP"></asp:Label>
                                    </div>
                                </div>
                                <div style="display: flex; align-items: center">
                                    <button type="button" id="btnminus" onclick="decreaseQuantity(this)" style="background-color: white; color: black; width: 60px; height: 30px; margin: 0; padding: 0; border: 1px solid #000000; border-radius: 0px;">-</button>
                                    <asp:TextBox ID="txtQuantity" Text="1" onblur="validateQuantity(this)" onkeypress="return isNumberKey(event)" CssClass="text-center" runat="server" Style="background-color: white; color: black; width: 60px; height: 30px; margin: 0; padding: 0; border: 1px solid #000000; border-radius: 0px;"></asp:TextBox>
                                    <button type="button" id="btnplus" onclick="increaseQuantity(this)" style="background-color: white; color: black; width: 60px; height: 30px; margin: 0; padding: 0; border: 1px solid #000000; border-radius: 0px;">+</button>
                                    <asp:LinkButton runat="server" ID="btn_view_cart" Style="color: white; background-color: #149474; border: 1px solid #149474; width: 75px; height: 30px; text-align: center; margin: auto; border-radius: 2px;" CommandName="View_Product" CommandArgument='<%# Eval("Product_Code") %>'><i class="fa fa-cart-plus" style="margin-top: 7px; margin-left: -4px;"></i></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>

    <div class="modal fade" id="productModal" tabindex="-1" role="dialog" aria-labelledby="productModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-body">
                    <div id="loadingOverlay" style="display: none;">
                        <div class="spinner-border text-primary" role="status">
                            <i class="fa fa-spinner fa-spin spinnercolor"></i>
                        </div>
                    </div>

                    <asp:HiddenField runat="server" ID="hdn_item_code" />
                    <div class="row mb-3">
                        <div class="col-12">
                            <span id="lbl_252"></span>&nbsp;<asp:Label runat="server" CssClass="font-weight-bold" ID="lbl_user_dc" Style="font-size: 14px;"></asp:Label>
                        </div>
                        <div class="col-12" style="display:none">
                            <span id="lbl_458"></span>&nbsp;<asp:Label runat="server" CssClass="font-weight-bold" ID="lbl_user_rp" Style="display:none;font-size: 14px;"></asp:Label>
                        </div>
                    </div>

                    <div class="mb-3 mt-2">
                        <div class="row">
                            <div class="col-6 m-auto">
                                <span id="lbl_253"></span>&nbsp;<asp:Label runat="server" ID="lbl_retail_price" CssClass="product_price"></asp:Label>
                                <asp:HiddenField runat="server" ID="hdn_retail_price" />
                                <asp:HiddenField runat="server" ID="hdn_rp_points" />
                                <asp:HiddenField runat="server" ID="hdn_promotion" />
                                <asp:HiddenField runat="server" ID="hdn_promotion_price" />
                                <asp:HiddenField runat="server" ID="hdn_promotion_dc" />
                                <asp:HiddenField runat="server" ID="hdn_total_dc" />
                                <asp:HiddenField runat="server" ID="hdn_total_rp" />
                                <asp:HiddenField runat="server" ID="hdn_total_quantity" />
                            </div>
                            <div class="col-6" style="display: flex; align-items: center">
                                <button type="button" id="btndecrease" onclick="adjustAndValidateQuantity(this, 'decrease', 'txtQuantity1')" class="plusminus">-</button>
                                <asp:TextBox ID="txtQuantity1" Text="1" onblur="adjustAndValidateQuantity(this, 'validate', 'txtQuantity1')" onkeypress="return isNumberKey(event)" CssClass="text-center qty_box" runat="server"></asp:TextBox>
                                <button type="button" id="btnincrease" onclick="adjustAndValidateQuantity(this, 'increase', 'txtQuantity1')" class="plusminus">+</button>
                            </div>
                            <div class="col-12 m-auto">
                                <span>BV :</span>&nbsp;<asp:Label runat="server" ID="lbl_retail_price_bv" CssClass="product_price"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="mb-4" runat="server" id="div_promotion">
                        <div class="row">
                            <div class="col-6 m-auto">
                                <span id="lbl_254"></span>&nbsp;<asp:Label runat="server" ID="lbl_promotion_price" CssClass="product_price"></asp:Label>&nbsp;<span id="lbl_262"></span>
                            </div>
                            <div class="col-6" style="display: flex; align-items: center">
                                <button type="button" id="btnDec" onclick="adjustAndValidateQuantity(this, 'decrease', 'txtQuantity2')" class="plusminus">-</button>
                                <asp:TextBox ID="txtQuantity2" Text="0" onblur="adjustAndValidateQuantity(this, 'validate', 'txtQuantity2')" onkeypress="return isNumberKey(event)" CssClass="text-center qty_box" runat="server"></asp:TextBox>
                                <button type="button" id="btnInc" onclick="adjustAndValidateQuantity(this, 'increase', 'txtQuantity2')" class="plusminus">+</button>
                            </div>
                            <div class="col-12 m-auto">
                                <span>BV :</span>&nbsp;<asp:Label runat="server" ID="lbl_promotion_price_bv" CssClass="product_price"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="mb-4" style="display:none" runat="server" id="div_rp">
                        <div class="row">
                            <div class="col-6 m-auto">
                                <span>RP :</span>&nbsp;<asp:Label runat="server" ID="lbl_rp" CssClass="product_price"></asp:Label>
                            </div>
                            <div class="col-6" style="display: flex; align-items: center">
                                <button type="button" id="btn_rp_Dec" onclick="adjustAndValidateQuantity(this, 'decrease', 'txtQuantity3')" class="plusminus">-</button>
                                <asp:TextBox ID="txtQuantity3" Text="0" onblur="adjustAndValidateQuantity(this, 'validate', 'txtQuantity3')" onkeypress="return isNumberKey(event)" CssClass="text-center qty_box" runat="server"></asp:TextBox>
                                <button type="button" id="btn_rp_Inc" onclick="adjustAndValidateQuantity(this, 'increase', 'txtQuantity3')" class="plusminus">+</button>
                            </div>
                        </div>
                    </div>
                    <div class="mb-2 package_style" runat="server" id="div_package">
                        <span id="lbl_328" class="underline"></span>
                        <ul class="ml-3 mb-0">
                            <asp:Repeater runat="server" ID="rpt_package_item" OnItemDataBound="rpt_package_item_ItemDataBound">
                                <ItemTemplate>
                                    <li>
                                        <asp:Label runat="server" ID="lb_item_name"></asp:Label></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                    <div class="mb-2" runat="server" id="div_variation">
                        <span id="lbl_255"></span>&nbsp;<asp:Label runat="server" ID="lbl_variation_name"></asp:Label>
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
                        <div style="font-size: 14px;">
                            <span id="lbl_256"></span>&nbsp;<asp:Label runat="server" ID="lbl_total_quantity"></asp:Label>
                            <br />
                            <span id="lbl_257"></span>&nbsp;<asp:Label runat="server" ID="lbl_total_amount"></asp:Label>
                            <br />
                            <span id="lbl_258"></span>&nbsp;<asp:Label runat="server" ID="lbl_point_balance"></asp:Label>
                            <div id="div_insufficient_text" style="width: 55%; line-height: 1; display: none;">
                                <span style="font-size: 11px; color: red;" id="lbl_259"></span>
                            </div>
                            <br />
                            <span id="lbl_459" style="display:none"></span>&nbsp;<asp:Label runat="server" style="display:none" ID="lbl_rp_balance"></asp:Label>
                            <div id="div_insufficient_rp_text" style="width: 55%; line-height: 1; display: none;">
                                <span style="font-size: 11px; color: red;" id="lbl_460"></span>
                            </div>
                            <div id="div_no_qty" style="width: 55%; line-height: 1; display: none;">
                                <span style="font-size: 11px; color: red;" id="lbl_260"></span>
                            </div>
                        </div>
                    </p>
                    <div class="d-flex justify-content-end">
                        <asp:Button runat="server" ID="btn_final_add_to_cart" OnClick="btn_final_add_to_cart_Click" Style="display: none;" />
                        <button type="button" id="btn_add_to_cart" onclick="AddToCart()" style="color: white; background-color: #149474; border: 1px solid #149474; width: 55px; height: 30px;"><i class="fa fa-cart-plus"></i></button>
                    </div>
                    <hr class="mt-2 mb-2" />
                    <asp:Label runat="server" ID="lbl_cart_note" CssClass="cart_note"></asp:Label>
                </div>
            </div>
        </div>
    </div>

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

    <script type="text/javascript">

        $(document).ready(function () {
            Load_Language();
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
            var page = 'Product Listing';
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
                                element.value = item.Language_Content;
                            }
                        } else {
                            window[item.Label_Name] = item.Language_Content;
                        }
                    });
                }
            });
        }

        function redirectToProduct(productCode) {
            window.location.href = "Product_Details.aspx?id=" + productCode;
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
            var div_insufficient_rp_text = document.getElementById("div_insufficient_rp_text");
            var div_no_qty = document.getElementById("div_no_qty");
            var divStyle = window.getComputedStyle(divInsufficient);
            var div_insufficient_Style = window.getComputedStyle(div_insufficient_rp_text);
            var div_no_qty_style = window.getComputedStyle(div_no_qty);

            if (divStyle.display === "block") {
                // Do nothing, insufficient text is displayed
            }
            else if (div_no_qty_style.display === "block") {
                // Do nothing, insufficient text is displayed
            } else if (div_insufficient_Style.display === "block") {
                // Do nothing, insufficient text is displayed
            } else {
                document.getElementById("ContentPlaceHolder1_btn_final_add_to_cart").click();
            }
        }

        function CheckFirstVariation() {
            // Check if any radio button is checked
            var anyChecked = $(".variation-value-list input[type='radio']:checked").length > 0;

            // If none are checked, check the last child
            if (!anyChecked) {
                $(".variation-value-list input[type='radio']").first().prop('checked', true).change();
            }

            if (document.getElementById("ContentPlaceHolder1_lbl_point_balance").style.display == "none") {
                document.getElementById("lbl_258").style.display = "none";
            }

            var arrayvariation = [];
            var newrepeater = "ContentPlaceHolder1_rbtnlist_variation_value";
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
            var id = document.getElementById("ContentPlaceHolder1_hdn_item_code").value;
            $.ajax({
                type: "POST",
                url: "Product.aspx/ProcessArray",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ myArray: arrayvariation, id: id }),
                success: function (response) {
                    var responseData = JSON.parse(response.d); // Parse the response JSON string
                    for (var i = 0; i < responseData.variation_details.length; i++) {
                        var variation_details = responseData.variation_details[i];
                        document.getElementById("ContentPlaceHolder1_lbl_retail_price").innerText = "RM " + numberWithCommas(parseFloat(variation_details.Variation_Retail_Price).toFixed(2));
                        document.getElementById("ContentPlaceHolder1_lbl_retail_price_bv").innerText = numberWithCommas(parseFloat(variation_details.Variation_Retail_Price_BV_Points).toFixed(2));
                        document.getElementById("ContentPlaceHolder1_lbl_rp").innerText = numberWithCommas(parseFloat(variation_details.Variation_Product_RP_Points).toFixed(2));
                        var currentDate = new Date();
                        var dateWithOffset = new Date(currentDate.setHours(currentDate.getHours() + 8));

                        var promotionDurationFrom = new Date(variation_details.Variation_Promotion_Duration_From);
                        var promotionDurationTo = new Date(variation_details.Variation_Promotion_Duration_To);

                        if (variation_details.Variation_Promotion == "Y" && (dateWithOffset > promotionDurationFrom) && (dateWithOffset < promotionDurationTo)) {
                            document.getElementById("ContentPlaceHolder1_div_promotion").style.display = "block";
                            document.getElementById("ContentPlaceHolder1_lbl_promotion_price").innerText = "RM " + numberWithCommas(parseFloat(variation_details.Variation_Promotion_Price_Selling_Price).toFixed(2)) + " + " + variation_details.Variation_Promotion_Price_EC_Points;
                            document.getElementById("ContentPlaceHolder1_lbl_promotion_price_bv").innerText = numberWithCommas(parseFloat(variation_details.Variation_Promotion_Price_BV_Points).toFixed(2));
                            document.getElementById("ContentPlaceHolder1_lbl_point_balance").style.display = "contents";
                            document.getElementById("lbl_258").style.display = "contents";
                        } else {
                            document.getElementById("ContentPlaceHolder1_div_promotion").style.display = "none";
                            document.getElementById("ContentPlaceHolder1_lbl_point_balance").style.display = "none";
                            document.getElementById("lbl_258").style.display = "none";
                            document.getElementById("ContentPlaceHolder1_txtQuantity2").value = "0";
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

                var newrepeater = "ContentPlaceHolder1_rbtnlist_variation_value";
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
                var id = document.getElementById("ContentPlaceHolder1_hdn_item_code").value;
                $.ajax({
                    type: "POST",
                    url: "Product.aspx/ProcessArray",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({ myArray: arrayvariation, id: id }),
                    success: function (response) {
                        $("#loadingOverlay").hide();

                        var responseData = JSON.parse(response.d); // Parse the response JSON string
                        for (var i = 0; i < responseData.variation_details.length; i++) {
                            var variation_details = responseData.variation_details[i];

                            document.getElementById("ContentPlaceHolder1_lbl_retail_price").innerText = "RM " + numberWithCommas(parseFloat(variation_details.Variation_Retail_Price).toFixed(2));
                            document.getElementById("ContentPlaceHolder1_lbl_retail_price_bv").innerText = numberWithCommas(parseFloat(variation_details.Variation_Retail_Price_BV_Points).toFixed(2));
                            document.getElementById("ContentPlaceHolder1_lbl_rp").innerText = numberWithCommas(parseFloat(variation_details.Variation_Product_RP_Points).toFixed(2));

                            var currentDate = new Date();
                            var dateWithOffset = new Date(currentDate.setHours(currentDate.getHours() + 8));

                            var promotionDurationFrom = new Date(variation_details.Variation_Promotion_Duration_From);
                            var promotionDurationTo = new Date(variation_details.Variation_Promotion_Duration_To);

                            if (variation_details.Variation_Promotion == "Y" && (dateWithOffset > promotionDurationFrom) && (dateWithOffset < promotionDurationTo)) {
                                document.getElementById("ContentPlaceHolder1_div_promotion").style.display = "block";
                                document.getElementById("ContentPlaceHolder1_lbl_promotion_price").innerText = "RM " + numberWithCommas(parseFloat(variation_details.Variation_Promotion_Price_Selling_Price).toFixed(2)) + " + " + variation_details.Variation_Promotion_Price_EC_Points;
                                document.getElementById("ContentPlaceHolder1_lbl_promotion_price_bv").innerText = numberWithCommas(parseFloat(variation_details.Variation_Promotion_Price_BV_Points).toFixed(2));
                                document.getElementById("ContentPlaceHolder1_lbl_point_balance").style.display = "contents";
                                document.getElementById("lbl_258").style.display = "contents";
                            } else {
                                document.getElementById("ContentPlaceHolder1_div_promotion").style.display = "none";
                                document.getElementById("ContentPlaceHolder1_lbl_point_balance").style.display = "none";
                                document.getElementById("lbl_258").style.display = "none";
                                document.getElementById("ContentPlaceHolder1_txtQuantity2").value = "0";

                            }

                            document.getElementById("ContentPlaceHolder1_hdn_retail_price").value = variation_details.Variation_Retail_Price;
                            document.getElementById("ContentPlaceHolder1_hdn_rp_points").value = variation_details.Variation_Product_RP_Points;
                            document.getElementById("ContentPlaceHolder1_hdn_promotion_price").value = variation_details.Variation_Promotion_Price_Selling_Price;
                            document.getElementById("ContentPlaceHolder1_hdn_promotion_dc").value = variation_details.Variation_Promotion_Price_EC_Points;
                            var promotion_qty_element = document.getElementById("ContentPlaceHolder1_txtQuantity2");
                            var qty = "";
                            if (promotion_qty_element && variation_details.Variation_Promotion == "Y" && (dateWithOffset > promotionDurationFrom) && (dateWithOffset < promotionDurationTo)) {
                                qty = parseFloat(document.getElementById("ContentPlaceHolder1_txtQuantity1").value) + parseFloat(document.getElementById("ContentPlaceHolder1_txtQuantity2").value);
                            } else {
                                qty = parseFloat(document.getElementById("ContentPlaceHolder1_txtQuantity1").value);
                            }
                            document.getElementById("ContentPlaceHolder1_hdn_total_quantity").value = qty;
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
                }
                else if (action === 'decrease') {
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
            var retail_qty = document.getElementById("ContentPlaceHolder1_txtQuantity1").value;
            var retail_price = document.getElementById("ContentPlaceHolder1_hdn_retail_price").value;
            var retail_total_price = "0";
            var retail_total_price = (parseFloat(retail_qty) * parseFloat(retail_price));
            var retail_total_qty = document.getElementById("ContentPlaceHolder1_txtQuantity1").value;
            var promotion_qty = "0";
            var promotion_price = "0";
            var promotion_total_price = "0";
            var promotion_total_qty = "0";
            var promotion_dc = "0";
            var rp_point = "0";
            //Retail Price

            var promotion_qty_element = document.getElementById("ContentPlaceHolder1_txtQuantity2");

            if (promotion_qty_element) {
                var promotion_qty = document.getElementById("ContentPlaceHolder1_txtQuantity2").value;
                if (promotion_qty != "0") {
                    //Promotion Price
                    promotion_price = document.getElementById("ContentPlaceHolder1_hdn_promotion_price").value;
                    promotion_dc = document.getElementById("ContentPlaceHolder1_hdn_promotion_dc").value;
                    var totaldc = document.getElementById("ContentPlaceHolder1_hdn_total_dc").value;
                    promotion_total_price = (parseFloat(promotion_price) * parseFloat(promotion_qty));
                    promotion_total_qty = promotion_qty;
                    promotion_dc = totaldc - (parseFloat(promotion_dc) * parseFloat(promotion_qty));
                    console.log(promotion_qty);
                    //console.log(promotion_dc);
                    // Check if result_dc is negative and apply red color
                    if (promotion_dc < 0) {
                        document.getElementById("div_insufficient_text").style.display = "block";
                        document.getElementById("ContentPlaceHolder1_lbl_point_balance").style.color = "red";
                    } else if (promotion_dc === 0) {
                        // Set the color to black if result_dc is 0
                        document.getElementById("div_insufficient_text").style.display = "none";
                        document.getElementById("ContentPlaceHolder1_lbl_point_balance").style.color = "black";
                    } else {
                        // Reset the color to its default value if needed
                        document.getElementById("div_insufficient_text").style.display = "none";
                        document.getElementById("ContentPlaceHolder1_lbl_point_balance").style.color = ""; // or use null or the original color value
                    }
                    //Promotion Price
                } else {
                    document.getElementById("div_insufficient_text").style.display = "none";
                    document.getElementById("ContentPlaceHolder1_lbl_point_balance").style.color = "black";
                }
            }

            var rp_qty_element = document.getElementById("ContentPlaceHolder1_txtQuantity3");
            var rp_qty = document.getElementById("ContentPlaceHolder1_txtQuantity3").value;
            if (rp_qty_element) {
                if (rp_qty != "0") {
                    //Promotion Price
                    var rp = document.getElementById("ContentPlaceHolder1_hdn_rp_points").value;
                    var totalrp = document.getElementById("ContentPlaceHolder1_hdn_total_rp").value;
                    rp_point = totalrp - (parseFloat(rp) * parseFloat(rp_qty));
                    // Check if result_dc is negative and apply red color
                    if (rp_point < 0) {
                        document.getElementById("div_insufficient_rp_text").style.display = "block";
                        document.getElementById("ContentPlaceHolder1_lbl_rp_balance").style.color = "red";
                    } else if (rp_point === 0) {
                        // Set the color to black if result_dc is 0
                        document.getElementById("div_insufficient_rp_text").style.display = "none";
                        document.getElementById("ContentPlaceHolder1_lbl_rp_balance").style.color = "black";
                    } else {
                        // Reset the color to its default value if needed
                        document.getElementById("div_insufficient_rp_text").style.display = "none";
                        document.getElementById("ContentPlaceHolder1_lbl_rp_balance").style.color = ""; // or use null or the original color value
                    }
                    //Promotion Price
                } else {
                    document.getElementById("div_insufficient_rp_text").style.display = "none";
                    document.getElementById("ContentPlaceHolder1_lbl_rp_balance").style.color = "black";
                }
            }

            var final_price = "0";
            var final_qty = "0";
            var final_dc = "0";
            final_price = (parseFloat(retail_total_price) + parseFloat(promotion_total_price));
            final_qty = (parseFloat(retail_total_qty) + parseFloat(promotion_total_qty) + parseFloat(rp_qty));
            final_dc = promotion_dc;
            document.getElementById("ContentPlaceHolder1_lbl_total_amount").innerText = "RM " + numberWithCommas(parseFloat(final_price).toFixed(2));
            document.getElementById("ContentPlaceHolder1_hdn_total_quantity").value = final_qty;
            document.getElementById("ContentPlaceHolder1_lbl_total_quantity").innerText = final_qty;
            var promotion_qty_element2 = document.getElementById("ContentPlaceHolder1_lbl_point_balance");
            var rp_balance = document.getElementById("ContentPlaceHolder1_lbl_rp_balance");

            if (promotion_qty_element2) {
                document.getElementById("ContentPlaceHolder1_lbl_point_balance").innerText = final_dc;
            }

            if (rp_balance) {
                document.getElementById("ContentPlaceHolder1_lbl_rp_balance").innerText = rp_point;
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

</asp:Content>

