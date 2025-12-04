<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Upgrade_To_EO_Checkout.aspx.cs" Inherits="Upgrade_To_EO_Checkout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style>

        body{
            background: #f2f1f1;
        }

        .container{
            border: 1px solid #efefef;
            border-radius: 10px;
            background-color: white;
        }

        .title_bar {
            background-color: #57585c;
            color: white;
            padding: 10px;
            font-weight: bold;
            font-size: 1rem;
        }

        .bg-black {
            background-color: black !important;
        }

        .productlist {
            display: block;
            overflow-x: scroll;
        }

        .underline {
            text-decoration: underline;
        }

        .package_style {
            border: 1px solid #dad6d6;
            border-radius: 10px;
            padding: 10px;
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

        .notestyle {
            text-align: center;
            font-size: 1.5rem;
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

        .notecontent {
            font-size: 14px;
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
                font-size: 14px;
                position: absolute;
                background-color: white;
                font-weight: bold;
                color: #363535;
                white-space: break-spaces;
                /*white-space: nowrap;*/
                line-height: 1.2;
            }

        .cart_note {
            font-size: 11px;
        }

        @media only screen and (max-width: 767px) {

            .mobile_scroll_table {
                overflow: auto;
            }

            .notestyle {
                text-align: left;
                font-size: 16px;
            }

            .notecontent {
                font-size: 14px;
            }
        }

        @media only screen and (max-width: 500px) {

            .product_price {
                font-size: 13px;
            }
        }

        @media only screen and (max-width: 300px) {

            .notecontent {
                font-size: 12px;
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

        #loadingOverlay_Main {
            position: fixed;
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

        .editaddress_size {
            margin: 30px auto;
            max-width: 50%;
        }

        .addaddress_size {
            margin: 30px auto;
            max-width: 55%;
        }

        .add_new_address {
            border: 2px dashed red;
            font-size: 1rem;
            text-align: center;
            padding: 15px;
            border-radius: 5px;
            background-color: #fafafa;
            cursor: pointer;
        }

        .defualtaddress {
            border: 1px solid red;
            padding: 4px 8px;
            color: red;
            font-weight: 400;
        }

        .addresstext {
            color: #0000008a;
            font-size: 15px;
        }

        .addressname_phone {
            font-size: 18px;
        }

        .new_address_textbox {
            padding: 10px !important;
            margin: 10px 0px;
        }

        .editclass {
            border: 1px solid #9b9bef;
            padding: 3px 10px;
            color: #08f;
        }

        .table td, .table th {
            padding: .75rem;
            vertical-align: middle;
            border-top: 1px solid #e9ecef;
            text-align: center;
            white-space: nowrap;
        }

        .d-grid {
            display: grid !important;
        }

        .btn:hover {
            background-color: #007bff; /* Change this to the desired hover color */
            color: #ffffff; /* Change this to the desired text color on hover */
        }

        .normal_tr {
            display: table-row !important;
            margin: auto !important;
            border: 0px !important;
            border-top-width: 0px !important;
            cursor: unset !important;
        }

        @media only screen and (max-width: 900px) {
            .editaddress_size {
                margin: 10px;
                max-width: 100%;
            }

            .addaddress_size {
                margin: 10px;
                max-width: 100%;
            }
        }

    </style>

    <script type="text/javascript">
        function Show_Variation() {
            Load_Language();
            $('#productModal').modal('show');
            $("tr").addClass("variationstyle");

            // Get all <tr> elements within the table with class "productlist"
            var rows = document.querySelectorAll('.table.productlist tr');

            // Loop through each <tr> element and remove the class "variationstyle"
            rows.forEach(function (row) {
                row.classList.remove('variationstyle');
            });

            var rbtnlist_element = document.getElementById("ContentPlaceHolder1_rbtnlist_variation_value");

            if (rbtnlist_element) {
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

            validateQuantity_Modal();
        }

        function Show_All_Address_Modal() {
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
            $('#AllAddressModal').modal('show');
            Load_Language();
        }

        function Show_Edit_Address_Modal() {
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
            $('#NewAddressModal').modal('show');
            Load_Language();
        }

        function OpenNewAddressModal() {
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
            $('#NewAddressModal').modal('show');
            Load_Language();
        }

        function Set_Up_Language() {
            Load_Language();
        }

        function Set_Up_Language_and_btn_placement() {
            Load_Language();
            var btnPlacementButton = document.getElementById('btn_placement_button');
            var btn_shopper_button = document.getElementById('btn_shopper_button');

            // Add onclick event handler
            btnPlacementButton.onclick = BinaryPlacementCheckbox;
            btn_shopper_button.onclick = Shopper_Checkbox;

            var checkbox = document.getElementById("ContentPlaceHolder1_btn_placement");
            var div_downline = document.getElementById("div_downline");

            // Check the current state of the checkbox and toggle it
            if (checkbox.checked) {
                checkbox.checked = true;
                div_downline.style.display = 'block';

            } else {
                checkbox.checked = false;
                div_downline.style.display = 'none';
            }

            var checkbox_placement = document.getElementById("ContentPlaceHolder1_btn_shopper");
            var div_profit_center = document.getElementById("div_profit_center");

            // Check the current state of the checkbox and toggle it
            if (checkbox_placement.checked) {
                checkbox_placement.checked = true;
                div_profit_center.style.display = 'none';
            } else {
                checkbox_placement.checked = false;
                div_profit_center.style.display = 'block';

            }
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    
    <div id="loadingOverlay_Main" style="display: none;">
        <div class="spinner-border text-primary" role="status">
            <i class="fa fa-spinner fa-spin spinnercolor"></i>
        </div>
    </div>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:HiddenField runat="server" ID="hdn_minimum_package" Value="0" />

            <section class="container mt-3">
                <div class="row">
                    <div class="col-12 m-auto">
                        <div class="row text-left mt-3 mb-3">
                            <div class="col-12 d-inline-flex">
                                <p class="membership_title fw-200" id="lbl_286"></p>
                                &nbsp;<p class="membership_title fw-500" id="lbl_287"></p>
                            </div>
                        </div>

                        <div class="row mb-3">
                            <div class="col-12 mb-3 mobile_scroll_table">
                                <table class="table productlist">
                                    <tr>
                                        <th><span id="lbl_288">Product</span></th>
                                        <th><span id="lbl_289">Name</span></th>
                                        <th><span id="lbl_290">Variation</span></th>
                                        <th><span id="lbl_291">Price</span></th>
                                        <th><span id="lbl_292">Qty</span></th>
                                        <th><span id="lbl_293">DC Used</span></th>
                                        <th><span id="lbl_294">DC Price</span></th>
                                        <th><span id="lbl_295">DC Qty</span></th>
                                        <th><span id="lbl_296">Weight (g)</span></th>
                                        <th><span id="lbl_297">Total Amount</span></th>
                                        <th><span id="lbl_298">BV</span></th>
                                        <th><span id="lbl_299">DC Earn</span></th>
                                        <th><span id="lbl_300">Edit</span></th>
                                    </tr>

                                    <asp:Repeater runat="server" ID="rpt_cart" OnItemDataBound="rpt_cart_ItemDataBound" OnItemCommand="rpt_cart_ItemCommand">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <div style="position: relative; width: 90px;">
                                                        <img runat="server" id="img_product" />
                                                        <div runat="server" id="div_mask" class="imgmask">
                                                            <span id="lbl_mask_title" runat="server">NOT AVAILABLE</span>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lbl_product_name"></asp:Label></td>
                                                <td>
                                                    <asp:Label runat="server" ID="lbl_product_variation"></asp:Label></td>
                                                <td>
                                                    <asp:Label runat="server" ID="lbl_product_price"></asp:Label></td>
                                                <td>
                                                    <asp:Label runat="server" ID="lbl_qty"></asp:Label></td>
                                                <td>
                                                    <asp:Label runat="server" ID="lbl_dc_used"></asp:Label></td>
                                                <td>
                                                    <asp:Label runat="server" ID="lbl_dc_price"></asp:Label></td>
                                                <td>
                                                    <asp:Label runat="server" ID="lbl_dc_qty"></asp:Label></td>
                                                <td>
                                                    <asp:Label runat="server" ID="lbl_total_weight"></asp:Label></td>
                                                <td>
                                                    <asp:Label runat="server" ID="lbl_total_amount"></asp:Label></td>
                                                <td>
                                                    <asp:Label runat="server" ID="lbl_bv_earn"></asp:Label></td>
                                                <td>
                                                    <asp:Label runat="server" ID="lbl_dc_earn"></asp:Label></td>
                                                <td>
                                                    <div class="d-grid justify-content-around">
                                                        <asp:Button runat="server" ID="btn_edit" CssClass="btn" Text="Edit" Style="padding: 10px; width: 65px; margin-bottom: 10px;" CommandName="Edit" CommandArgument='<%# Eval("IDs") %>' formnovalidate />
                                                        <asp:Button runat="server" ID="btn_delete" CssClass="btn" Text="Delete" Style="padding: 10px; width: 65px;" CommandName="Delete" CommandArgument='<%# Eval("IDs") %>' formnovalidate />
                                                    </div>

                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr runat="server" id="tr_no_record" visible="false" class="w-100 text-center">
                                        <td colspan="13">No record found.</td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <!-- Column for desktop view -->
                            <div class="col-lg-6 mb-3">
                                <div>
                                    <div class="row">
                                        <div class="col-4 col-lg-4">
                                            <label id="lbl_301"></label>
                                        </div>
                                        <div class="col-1 col-lg-1">
                                            <span>:</span>
                                        </div>
                                        <div class="col-6 col-lg-6">
                                            <asp:Label runat="server" ID="lbl_total_qty">0</asp:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-4 col-lg-4">
                                            <label id="lbl_302"></label>
                                        </div>
                                        <div class="col-1 col-lg-1">
                                            <span>:</span>
                                        </div>
                                        <div class="col-6 col-lg-6">
                                            <asp:Label runat="server" ID="lbl_total_bv">0</asp:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-4 col-lg-4">
                                            <label id="lbl_303"></label>
                                        </div>
                                        <div class="col-1 col-lg-1">
                                            <span>:</span>
                                        </div>
                                        <div class="col-6 col-lg-6">
                                            <asp:Label runat="server" ID="lbl_total_dc_used">0</asp:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-4 col-lg-4">
                                            <label id="lbl_304"></label>
                                        </div>
                                        <div class="col-1 col-lg-1">
                                            <span>:</span>
                                        </div>
                                        <div class="col-6 col-lg-6">
                                            <asp:Label runat="server" ID="lbl_total_dc_earn">0</asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <!-- Column for desktop view -->
                            <div class="col-lg-6 mb-3">
                                <div>
                                    <div class="row">
                                        <div class="col-4 col-lg-4">
                                            <label id="lbl_305"></label>
                                        </div>
                                        <div class="col-1 col-lg-1">
                                            <span>:</span>
                                        </div>
                                        <div class="col-6 col-lg-6">
                                            <asp:Label runat="server" ID="lbl_sub_total">RM 0.00</asp:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-4 col-lg-4">
                                            <label id="lbl_306"></label>
                                        </div>
                                        <div class="col-1 col-lg-1">
                                            <span>:</span>
                                        </div>
                                        <div class="col-6 col-lg-6">
                                            <asp:Label runat="server" ID="lbl_total_shipping">RM 0.00</asp:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-4 col-lg-4">
                                            <label id="lbl_307"></label>
                                        </div>
                                        <div class="col-1 col-lg-1">
                                            <span>:</span>
                                        </div>
                                        <div class="col-6 col-lg-6">
                                            <asp:Label runat="server" ID="lbl_total_shipping_discount">RM 0.00</asp:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-4 col-lg-4">
                                            <label id="lbl_308"></label>
                                        </div>
                                        <div class="col-1 col-lg-1">
                                            <span>:</span>
                                        </div>
                                        <div class="col-6 col-lg-6">
                                            <asp:Label runat="server" ID="lbl_total_amout">RM 0.00</asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <hr />
                        <div id="div_customer_info" runat="server">
                            <div class="row mb-4">
                                <div class="col-12 mb-3 title_bar">
                                    <label id="lbl_309"></label>
                                </div>
                                <div class="col-12">
                                    <div>
                                        <div class="row">
                                            <div class="col-4 col-lg-3">
                                                <label id="lbl_310"></label>
                                            </div>
                                            <div class="col-1 col-lg-1">
                                                <span>:</span>
                                            </div>
                                            <div class="col-6 col-lg-8">
                                                <asp:Label runat="server" ID="lbl_member_id"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-4 col-lg-3">
                                                <label id="lbl_311"></label>
                                            </div>
                                            <div class="col-1 col-lg-1">
                                                <span>:</span>
                                            </div>
                                            <div class="col-6 col-lg-8">
                                                <asp:Label runat="server" ID="lbl_member_name"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-4 col-lg-3">
                                                <label id="lbl_312"></label>
                                            </div>
                                            <div class="col-1 col-lg-1">
                                                <span>:</span>
                                            </div>
                                            <div class="col-6 col-lg-8">
                                                <asp:Label runat="server" ID="lbl_member_contact_no"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="row mb-4">
                                <div class="col-12 mb-3 title_bar">
                                    <label id="lbl_313"></label>
                                </div>
                                <div class="col-12">
                                    <div>
                                        <div class="row">
                                            <div class="col-4 col-lg-3 m-auto">
                                                <label id="lbl_314"></label>
                                            </div>
                                            <div class="col-1 col-lg-1 m-auto">
                                                <span>:</span>
                                            </div>
                                            <div class="col-7 col-lg-8 m-auto">
                                                <asp:DropDownList runat="server" ID="ddl_delivery_option" CssClass="w-100 new_address_textbox" OnSelectedIndexChanged="ddl_delivery_option_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row" runat="server" id="div_delivery">
                                <div class="col-12 mb-3 title_bar">
                                    <label id="lbl_315"></label>
                                </div>

                                <div class="col-12 mb-3 text-right">
                                    <asp:Button runat="server" ID="btn_edit_address" Text=" Edit Address" OnClick="btn_edit_address_Click" CssClass="btn bg-black text-white" formnovalidate />
                                </div>
                                <div class="col-12">
                                    <div>
                                        <div class="row">
                                            <div class="col-4 col-lg-3">
                                                <label id="lbl_316">Recipient Name</label>
                                            </div>
                                            <div class="col-1 col-lg-1">
                                                <span>:</span>
                                            </div>
                                            <div class="col-7 col-lg-8">
                                                <asp:Label runat="server" ID="lbl_delivery_name"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-4 col-lg-3">
                                                <label id="lbl_326">Contact</label>
                                            </div>
                                            <div class="col-1 col-lg-1">
                                                <span>:</span>
                                            </div>
                                            <div class="col-7 col-lg-8">
                                                <asp:Label runat="server" ID="lbl_delivery_contact_no"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-4 col-lg-3">
                                                <label id="lbl_317"></label>
                                            </div>
                                            <div class="col-1 col-lg-1">
                                                <span>:</span>
                                            </div>
                                            <div class="col-7 col-lg-8">
                                                <asp:Label runat="server" ID="lbl_delivery_address"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-4 col-lg-3">
                                                <label id="lbl_318"></label>
                                            </div>
                                            <div class="col-1 col-lg-1">
                                                <span>:</span>
                                            </div>
                                            <div class="col-7 col-lg-8">
                                                <asp:Label runat="server" ID="lbl_delivery_postcode"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-4 col-lg-3">
                                                <label id="lbl_319"></label>
                                            </div>
                                            <div class="col-1 col-lg-1">
                                                <span>:</span>
                                            </div>
                                            <div class="col-7 col-lg-8">
                                                <asp:Label runat="server" ID="lbl_delivery_city"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-4 col-lg-3">
                                                <label id="lbl_320"></label>
                                            </div>
                                            <div class="col-1 col-lg-1">
                                                <span>:</span>
                                            </div>
                                            <div class="col-7 col-lg-8">
                                                <asp:Label runat="server" ID="lbl_delivery_state"></asp:Label>
                                            </div>
                                        </div>
                                            <div class="row">
                                                <div class="col-4 col-lg-3">
                                                    <label id="lbl_345"></label>
                                                </div>
                                                <div class="col-1 col-lg-1">
                                                    <span>:</span>
                                                </div>
                                                <div class="col-7 col-lg-8">
                                                    <asp:Label runat="server" ID="lbl_delivery_country"></asp:Label>
                                                </div>
                                            </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row mb-4" runat="server" id="div_self_pickup">
                                <div class="col-12 mb-3 title_bar">
                                    <label id="lbl_321"></label>
                                </div>
                                <div class="col-12">
                                    <div>
                                        <div class="row">
                                            <div class="col-4 col-lg-3">
                                                <label id="lbl_322"></label>
                                            </div>
                                            <div class="col-1 col-lg-1">
                                                <span>:</span>
                                            </div>
                                            <div class="col-7 col-lg-8">
                                                <asp:Label runat="server" ID="lbl_person_in_charge"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-4 col-lg-3">
                                                <label id="lbl_323"></label>
                                            </div>
                                            <div class="col-1 col-lg-1">
                                                <span>:</span>
                                            </div>
                                            <div class="col-7 col-lg-8">
                                                <asp:Label runat="server" ID="lbl_person_in_charge_phone_number"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-4 col-lg-3">
                                                <label id="lbl_324"></label>
                                            </div>
                                            <div class="col-1 col-lg-1">
                                                <span>:</span>
                                            </div>
                                            <div class="col-7 col-lg-8">
                                                <asp:Label runat="server" ID="lbl_pickup_address"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-4 col-lg-3">
                                                <label id="lbl_325"></label>
                                            </div>
                                            <div class="col-1 col-lg-1">
                                                <span>:</span>
                                            </div>
                                            <div class="col-7 col-lg-8">
                                                <asp:Label runat="server" ID="lbl_operation_time"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                                <div class="row mb-4">
                                    <div class="col-12 mb-3 title_bar">
                                        <label id="lbl_448"></label>
                                    </div>
                                    <div class="col-12">
                                        <div>
                                            <div class="row">
                                                <div class="col-12 col-lg-12">
                                                    <textarea runat="server" id="txt_remark" rows="4" class="form-control w-100" placeholder="Leave some remark here..." maxlength="200"></textarea>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            <hr />
                            <div class="row">
                                <div class="col-12 col-lg-2 m-auto notestyle font-weight-bold">
                                    <label id="lbl_327"></label>
                                </div>
                                <div class="col-12 col-lg-10 mb-3 notecontent">
                                    <asp:Label runat="server" ID="lbl_checkout_note"></asp:Label>
                                </div>
                            </div>
                            <hr />
                        </div>

                        <div>
                            <div class="row mb-5">
                                <div class="col-12 mb-3 text-right">
                                    <asp:Button runat="server" Text="Back" CssClass="btn btn_primary bg-black text-white" ID="btn_back" OnClick="btn_back_Click" formnovalidate />
                                    <asp:Button runat="server" Text="Place Order" CssClass="btn btn_primary bg-black text-white" ID="btn_place_order" OnClick="btn_place_order_Click" OnClientClick="ShowLoading()" formnovalidate />
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </section>

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
                            <asp:HiddenField runat="server" ID="hdn_cart_ids" />
                            <asp:HiddenField runat="server" ID="hdn_retail_price" />
                            <asp:HiddenField runat="server" ID="hdn_promotion" />
                            <asp:HiddenField runat="server" ID="hdn_promotion_price" />
                            <asp:HiddenField runat="server" ID="hdn_promotion_dc" />
                            <asp:HiddenField runat="server" ID="hdn_total_dc" />
                            <asp:HiddenField runat="server" ID="hdn_total_quantity" />
                            <div class="row mb-3" runat="server" id="div_dc">
                                <div class="col-12">
                                    <span id="lbl_275"></span>&nbsp;<asp:Label runat="server" CssClass="font-weight-bold" ID="lbl_user_dc" Style="font-size: 17px;"></asp:Label>
                                </div>
                            </div>
                            <div class="mb-3 mt-2">
                                <div class="row">
                                    <div class="col-6 m-auto">
                                        <span id="lbl_276"></span>&nbsp;<asp:Label runat="server" ID="lbl_retail_price" Style="font-size: 15px;"></asp:Label>
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
                                        <span id="lbl_277"></span>&nbsp;<asp:Label runat="server" ID="lbl_promotion_price" Style="font-size: 15px;"></asp:Label>
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
                            <div class="mb-2 package_style" runat="server" id="div_package">
                                <span id="lbl_330" class="underline"></span>
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
                                <span id="lbl_278"></span>&nbsp;<asp:Label runat="server" ID="lbl_variation_name"></asp:Label>
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
                                <div style="font-size: 16px;">
                                    <span id="lbl_279"></span>&nbsp;<asp:Label runat="server" ID="lbl_total_quantity"></asp:Label>
                                    <br />
                                    <span id="lbl_280"></span>&nbsp;<asp:Label runat="server" ID="lbl_total_amount"></asp:Label>
                                    <br />
                                    <span id="lbl_281"></span>&nbsp;<asp:Label runat="server" ID="lbl_point_balance"></asp:Label>
                                    <div id="div_insufficient_text" style="width: 55%; line-height: 1; display: none;">
                                        <span style="font-size: 11px; color: red;" id="lbl_282"></span>
                                    </div>
                                    <div id="div_no_qty" style="width: 100%; line-height: 1; display: none;">
                                        <span style="font-size: 11px; color: red;" id="lbl_283"></span>
                                    </div>
                                </div>
                            </p>
                            <div class="d-flex justify-content-end">
                                <asp:Button runat="server" ID="btn_final_update_cart" OnClick="btn_final_update_cart_Click" Style="display: none;" formnovalidate />
                                <button type="button" id="btn_add_to_cart" onclick="UpdateCart()" style="color: white; background-color: #149474; border: 1px solid #149474; width: 30%; height: 30px;">Update Cart</button>
                            </div>
                            <hr class="mt-2 mb-2" />
                            <asp:Label runat="server" ID="lbl_cart_note" CssClass="cart_note"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>

            <div class="modal" id="AllAddressModal" tabindex="-1" role="dialog" aria-labelledby="productModalLabel" aria-hidden="true">
                <div class="modal-dialog editaddress_size" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">My Address</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div id="loadingOverlay" style="display: none;">
                                <div class="spinner-border text-primary" role="status">
                                    <i class="fa fa-spinner fa-spin spinnercolor"></i>
                                </div>
                            </div>

                            <div class="add_new_address" onclick="OpenNewAddressModal()" data-dismiss="modal">
                                <asp:Label runat="server"> + Add New Address</asp:Label>
                            </div>

                            <div>
                                <asp:Repeater runat="server" ID="rpt_address" OnItemDataBound="rpt_address_ItemDataBound" OnItemCommand="rpt_address_ItemCommand">
                                    <ItemTemplate>
                                        <div style="padding: 1rem 0px;">
                                            <div class="d-flex justify-content-between">
                                                <div>
                                                    <asp:Label runat="server" ID="lbl_receiver_name" CssClass="addressname_phone"></asp:Label><span style="color: #0000008a; padding: 0px 10px;">|</span><asp:Label runat="server" ID="lbl_receiver_phone" CssClass="addressname_phone"></asp:Label>
                                                </div>
                                                <div>
                                                    <asp:LinkButton runat="server" CssClass="editclass" ID="btn_edit" CommandName="Edit" CommandArgument='<%# Eval("id") %>' formnovalidate>Edit</asp:LinkButton>
                                                </div>
                                            </div>
                                            <div>
                                                <asp:Label runat="server" ID="lbl_receiver_address" CssClass="addresstext"></asp:Label>
                                            </div>
                                            <div class="addresstext mb-2">
                                                <asp:Label runat="server" ID="lbl_receiver_postcode"></asp:Label><span>, </span>
                                                <asp:Label runat="server" ID="lbl_receiver_city"></asp:Label><span>, </span>
                                                <asp:Label runat="server" ID="lbl_receiver_state"></asp:Label><span>, </span>
                                                <asp:Label runat="server" ID="lbl_receiver_country"></asp:Label>
                                            </div>
                                            <div runat="server" id="div_default">
                                                <asp:Label runat="server" CssClass="defualtaddress">Default</asp:Label>
                                            </div>
                                        </div>
                                        <hr />
                                    </ItemTemplate>
                                </asp:Repeater>
                                <div runat="server" id="div_norecord">
                                    <span>No Record</span>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>

            <div class="modal" id="NewAddressModal" tabindex="-1" role="dialog" aria-labelledby="productModalLabel" aria-hidden="true">
                <div class="modal-dialog addaddress_size" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Address</h5>
                        </div>
                        <div class="modal-body">
                            <div id="loadingOverlay" style="display: none;">
                                <div class="spinner-border text-primary" role="status">
                                    <i class="fa fa-spinner fa-spin spinnercolor"></i>
                                </div>
                            </div>

                            <asp:HiddenField runat="server" ID="hdn_ids" />
                            <div class="row mt-2">
                                <div class="col-6">
                                    <input type="text" runat="server" id="txt_full_name" class="w-100 new_address_textbox" placeholder="Full Name" required onkeypress="return lettersOnly(event)" onkeydown="handleEnterKeyPress(event, 'txt_phone_number')" />
                                </div>
                                <div class="col-6">
                                    <input type="text" runat="server" id="txt_phone_number" class="w-100 new_address_textbox" placeholder="Phone Number" required onkeypress='return event.charCode >= 48 && event.charCode <= 57' onkeydown="handleEnterKeyPress(event, 'txt_address_1')" />
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-12">
                                    <input type="text" runat="server" id="txt_address_1" class="w-100 new_address_textbox" placeholder="Address Line 1" required onkeydown="handleEnterKeyPress(event, 'txt_address_2')" />
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-12">
                                    <input type="text" runat="server" id="txt_address_2" class="w-100 new_address_textbox" placeholder="Address Line 2" onkeydown="handleEnterKeyPress(event, 'txt_postcode')" />
                                </div>
                            </div>

                            <div class="row mb-2">
                                <div class="col-md-3 col-6">
                                    <input type="text" runat="server" id="txt_postcode" class="w-100 new_address_textbox" placeholder="Postcode" required onkeypress='return event.charCode >= 48 && event.charCode <= 57' onkeydown="handleEnterKeyPress(event, 'txt_city')" />
                                </div>
                                <div class="col-md-3 col-6">
                                    <input type="text" runat="server" id="txt_city" class="w-100 new_address_textbox" placeholder="City" required onkeydown="handleEnterKeyPress(event, '<%= ddl_state.ClientID %>')" />
                                </div>
                                <div class="col-md-3 col-6">
                                    <asp:DropDownList runat="server" ID="ddl_state" CssClass="w-100 new_address_textbox"></asp:DropDownList>
                                </div>
                                <div class="col-md-3 col-6">
                                    <asp:DropDownList runat="server" ID="ddl_address_country" CssClass="w-100 new_address_textbox"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="row mb-2">
                                <div class="col-12">
                                    <asp:CheckBox runat="server" ID="chk_default" Text="Set as default address" />
                                </div>
                            </div>

                            <div class=" d-inline-flex float-right">
                                <asp:Button runat="server" CssClass="form-control btn m-3" data-dismiss="modal" Text="Cancel" />
                                <asp:Button runat="server" CssClass="form-control btn btn-primary m-3" ID="btn_submit" OnClick="btn_submit_Click" Text="Submit" />
                                <asp:Button runat="server" CssClass="form-control btn btn-primary m-3" ID="btn_update" OnClick="btn_update_Click" Text="Update" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

    <script type="text/javascript">

        $(document).ready(function () {
            Load_Language();
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
            var page = 'Registration Checkout';
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

        function isValidEmail(email) {
            // Regular expression to validate email format
            var emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
            return emailRegex.test(email);
        }

        function validateNumericInput(input) {
            // Remove non-numeric characters
            input.value = input.value.replace(/[^0-9]/g, '');
        }

        function isNumeric(event) {
            // Allow only numeric characters (0-9)
            var charCode = (event.which) ? event.which : event.keyCode;
            if (charCode >= 48 && charCode <= 57) {
                return true;
            } else {
                event.preventDefault();
                return false;
            }
        }

        function ShowLoading() {
            $("#loadingOverlay_Main").show();
        }

        function validatePassword(event) {
            var inputValue = event.target.value;

            // Remove all spaces
            inputValue = inputValue.replace(/\s/g, '');

            // Allow only certain special characters
            inputValue = inputValue.replace(/[^\w!@%*&?]/g, '');

            // Update the input value
            event.target.value = inputValue;
        }

        function validateKeyPress(event) {
            // Disallow spaces
            if (event.key === ' ') {
                event.preventDefault();
            }
            // Allow only certain special characters
            else if (!/[\w!@%*&?]/.test(event.key)) {
                event.preventDefault();
            }
        }

        function GetDownlineDetails(selectedValue) {

            $.ajax({
                type: "POST",
                url: "Register_Member.aspx/GetDownlineDetails",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ memberid: selectedValue }),
                success: function (response) {

                    var responseData = JSON.parse(response.d); // Parse the response JSON string

                    if (responseData.memberdetails.length > 0) {

                        responseData.memberdetails.forEach(function (memberdetails) {

                            document.getElementById("ContentPlaceHolder1_lbl_selected_member").textContent = memberdetails["Selected_Member"];
                            if (memberdetails["Type"] == "A") {

                                document.getElementById("ContentPlaceHolder1_lbl_selected_member_left").textContent = memberdetails["Username"];
                                document.getElementById("ContentPlaceHolder1_hdn_left_id").value = memberdetails["Member_ID"];

                            } else {

                                document.getElementById("ContentPlaceHolder1_lbl_selected_member_right").textContent = memberdetails["Username"];
                                document.getElementById("ContentPlaceHolder1_hdn_right_id").value = memberdetails["Member_ID"];

                            }
                        });
                    } else {
                        var ddlDownline = document.getElementById("ContentPlaceHolder1_ddl_downline");
                        var selectedText = ddlDownline.options[ddlDownline.selectedIndex].text;

                        document.getElementById("ContentPlaceHolder1_lbl_selected_member").textContent = selectedText;
                        document.getElementById("ContentPlaceHolder1_lbl_selected_member_left").textContent = "-";
                        document.getElementById("ContentPlaceHolder1_hdn_left_id").value = "";
                        document.getElementById("ContentPlaceHolder1_lbl_selected_member_right").textContent = "-";
                        document.getElementById("ContentPlaceHolder1_hdn_right_id").value = "";
                    }
                }
            });
        }

        function BinaryPlacementCheckbox() {
            var checkbox = document.getElementById("ContentPlaceHolder1_btn_placement");
            var div_downline = document.getElementById("div_downline");

            // Check the current state of the checkbox and toggle it
            if (checkbox.checked) {
                checkbox.checked = false;
                div_downline.style.display = 'none';
            } else {
                checkbox.checked = true;
                div_downline.style.display = 'block';
            }
        }

        function Shopper_Checkbox() {
            var checkbox = document.getElementById("ContentPlaceHolder1_btn_shopper");
            var div_profit_center = document.getElementById("div_profit_center");

            // Check the current state of the checkbox and toggle it
            if (checkbox.checked) {
                checkbox.checked = false;
                div_profit_center.style.display = 'block';
            } else {
                checkbox.checked = true;
                div_profit_center.style.display = 'none';
            }
        }

        function allowOnlySpecificCharacters(event) {
            // Get the keyCode of the pressed key
            var keyCode = event.keyCode;

            // Allow backspace, delete, tab, and arrow keys
            if (keyCode == 8 || keyCode == 9 || keyCode == 46 || (keyCode >= 37 && keyCode <= 40)) {
                return true;
            }

            // Allow lowercase letters (a-z), uppercase letters (A-Z), numbers (0-9), and the characters ./_-&
            if ((keyCode >= 65 && keyCode <= 90) || // A-Z
                (keyCode >= 97 && keyCode <= 122) || // a-z
                (keyCode >= 48 && keyCode <= 57) || // 0-9
                keyCode == 46 || keyCode == 47 || keyCode == 45 || keyCode == 95 || keyCode == 38) { // ./_-&
                return true;
            }

            // Prevent any other keys from being pressed
            return false;
        }

        function lettersandNumberOnly(input) {
            // Regular expression to match only uppercase letters and numbers
            var regex = /^[A-Z0-9]+$/;

            // Get the value of the input field
            var inputValue = input.value;

            // Test if the input value matches the regular expression
            if (!regex.test(inputValue)) {
                // If the input contains invalid characters, remove them from the value
                input.value = inputValue.replace(/[^A-Z0-9]/g, '');
            }
        }

        function lettersOnly(event) {
            var charCode = event.keyCode;

            if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode === 8 || charCode === 32 || charCode === 64) {
                return true;
            } else {
                return false;
            }
        }
    </script>

    <script type="text/javascript">

        $(document).ready(function () {
            Load_Language();
            //CheckFirstVariation();
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
            var page = 'Registration Checkout';
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
                            var element = document.getElementById(item.Label_Name);
                            if (element) {
                                element.textContent = item.Language_Content;
                            }
                        } else if (item.Label_Type === 'LinkButton') {
                            var element = document.getElementById('ContentPlaceHolder1_' + item.Label_Name);
                            if (element) {
                                element.value = item.Language_Content;
                            }
                        } else if (item.Label_Type === 'LinkButton_Edit') {
                            var element = document.getElementById('ContentPlaceHolder1_' + item.Label_Name);
                            if (element) {
                                element.innerText = item.Language_Content; // or element.textContent = item.Language_Content;
                            }
                        } else {
                            window[item.Label_Name] = item.Language_Content;
                        }
                    });
                }
            });
        }

        function lettersOnly(event) {
            var charCode = event.keyCode;

            if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode === 8 || charCode === 32) {
                return true;
            } else {
                return false;
            }
        }

        function handleEnterKeyPress(event, nextElementId) {
            if (event.keyCode === 13) {
                event.preventDefault(); // Prevent default form submission behavior
                document.getElementById(nextElementId).focus(); // Focus on the next input field
            }
        }

        function sweetalert_success(message, messagetype) {
            $("#loadingOverlay_Main").hide();
            swal({
                title: message,
                type: "success"
            });
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
            Load_Language();
            var checkbox = document.getElementById("ContentPlaceHolder1_btn_placement");
            var div_downline = document.getElementById("div_downline");

            // Check the current state of the checkbox and toggle it
            if (checkbox.checked) {
                checkbox.checked = true;
                div_downline.style.display = 'block';

            } else {
                checkbox.checked = false;
                div_downline.style.display = 'none';
            }
        }

        function sweetalert_success_place_order(message, messagetype) {
            $("#loadingOverlay").hide();
            swal({
                html: true,
                title: message,
                type: "success"
            }, function () {
                window.location = "Home.aspx";
            });
        }

        function sweetalert_warning(message, messagetype) {
            $("#loadingOverlay_Main").hide();
            swal({
                title: message,
                icon: "warning",
                button: "OK",
            });
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
            Load_Language();
            var checkbox = document.getElementById("ContentPlaceHolder1_btn_placement");
            var div_downline = document.getElementById("div_downline");

            // Check the current state of the checkbox and toggle it
            if (checkbox.checked) {
                checkbox.checked = true;
                div_downline.style.display = 'block';

            } else {
                checkbox.checked = false;
                div_downline.style.display = 'none';
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
                document.getElementById("lbl_281").style.display = "none";
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
                url: "Register_Member_Checkout.aspx/ProcessArray",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ myArray: arrayvariation, id: id }),
                success: function (response) {
                    var responseData = JSON.parse(response.d); // Parse the response JSON string
                    for (var i = 0; i < responseData.variation_details.length; i++) {
                        var variation_details = responseData.variation_details[i];

                        document.getElementById("ContentPlaceHolder1_lbl_retail_price").innerText = "RM " + numberWithCommas(parseFloat(variation_details.Variation_Retail_Price).toFixed(2));
                        document.getElementById("ContentPlaceHolder1_lbl_retail_price_bv").innerText = numberWithCommas(parseFloat(variation_details.Variation_Retail_Price_BV_Points).toFixed(2));
                        var currentDate = new Date();
                        var dateWithOffset = new Date(currentDate.setHours(currentDate.getHours() + 8));

                        var promotionDurationFrom = new Date(variation_details.Variation_Promotion_Duration_From);
                        var promotionDurationTo = new Date(variation_details.Variation_Promotion_Duration_To);

                        if (variation_details.Variation_Promotion == "Y" && (dateWithOffset > promotionDurationFrom) && (dateWithOffset < promotionDurationTo)) {
                            document.getElementById("ContentPlaceHolder1_div_promotion").style.display = "block";
                            document.getElementById("ContentPlaceHolder1_lbl_promotion_price").innerText = "RM " + numberWithCommas(parseFloat(variation_details.Variation_Promotion_Price_Selling_Price).toFixed(2)) + " + " + variation_details.Variation_Promotion_Price_EC_Points;
                            document.getElementById("ContentPlaceHolder1_lbl_promotion_price_bv").innerText = numberWithCommas(parseFloat(variation_details.Variation_Promotion_Price_BV_Points).toFixed(2));
                            document.getElementById("ContentPlaceHolder1_lbl_point_balance").style.display = "contents";
                            document.getElementById("lbl_281").style.display = "contents";
                        } else {
                            document.getElementById("ContentPlaceHolder1_div_promotion").style.display = "none";
                            document.getElementById("ContentPlaceHolder1_lbl_point_balance").style.display = "none";
                            document.getElementById("lbl_281").style.display = "none";
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
                    url: "Register_Member_Checkout.aspx/ProcessArray",
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

                            var currentDate = new Date();
                            var dateWithOffset = new Date(currentDate.setHours(currentDate.getHours() + 8));

                            var promotionDurationFrom = new Date(variation_details.Variation_Promotion_Duration_From);
                            var promotionDurationTo = new Date(variation_details.Variation_Promotion_Duration_To);

                            if (variation_details.Variation_Promotion == "Y" && (dateWithOffset > promotionDurationFrom) && (dateWithOffset < promotionDurationTo)) {
                                document.getElementById("ContentPlaceHolder1_div_promotion").style.display = "block";
                                document.getElementById("ContentPlaceHolder1_lbl_promotion_price").innerText = "RM " + numberWithCommas(parseFloat(variation_details.Variation_Promotion_Price_Selling_Price).toFixed(2)) + " + " + variation_details.Variation_Promotion_Price_EC_Points;
                                document.getElementById("ContentPlaceHolder1_lbl_promotion_price_bv").innerText = numberWithCommas(parseFloat(variation_details.Variation_Promotion_Price_BV_Points).toFixed(2));
                                document.getElementById("ContentPlaceHolder1_lbl_point_balance").style.display = "contents";
                                document.getElementById("lbl_281").style.display = "contents";
                            } else {
                                document.getElementById("ContentPlaceHolder1_div_promotion").style.display = "none";
                                document.getElementById("ContentPlaceHolder1_lbl_point_balance").style.display = "none";
                                document.getElementById("lbl_281").style.display = "none";
                                document.getElementById("ContentPlaceHolder1_txtQuantity2").value = "0";

                            }
                            document.getElementById("ContentPlaceHolder1_hdn_promotion_price").value = variation_details.Variation_Promotion;
                            document.getElementById("ContentPlaceHolder1_hdn_retail_price").value = variation_details.Variation_Retail_Price;
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
                    }
                });

                function numberWithCommas(x) {
                    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                }
            }, 500); // 1000 milliseconds = 1 second
        }

    </script>

    <script type="text/javascript">

        function onRadioButtonClick(radioButton) {
            // Debugging: Log the class name of the clicked radio button
            console.log("Clicked radio button class:", radioButton.className);

            // Query for all radio buttons with the same class
            var radioButtons = document.querySelectorAll(radioButton.className);

            // Debugging: Log the number of radio buttons found
            console.log("Number of radio buttons found:", radioButtons.length);

            // Loop through the radio buttons and uncheck all except the clicked one
            for (var i = 0; i < radioButtons.length; i++) {
                if (radioButtons[i] !== radioButton) {
                    radioButtons[i].checked = false;
                }
            }
        }

        function UpdateCart() {
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
                document.getElementById("ContentPlaceHolder1_btn_final_update_cart").click();
            }
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
            //Retail Price

            if (document.getElementById("ContentPlaceHolder1_lbl_point_balance").style.display == "none") {
                document.getElementById("lbl_281").style.display = "none";
            }

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

            var final_price = "0";
            var final_qty = "0";
            var final_dc = "0";
            final_price = (parseFloat(retail_total_price) + parseFloat(promotion_total_price));
            final_qty = (parseFloat(retail_total_qty) + parseFloat(promotion_total_qty));
            final_dc = promotion_dc;
            document.getElementById("ContentPlaceHolder1_lbl_total_amount").innerText = "RM " + numberWithCommas(parseFloat(final_price).toFixed(2));
            document.getElementById("ContentPlaceHolder1_hdn_total_quantity").value = final_qty;
            document.getElementById("ContentPlaceHolder1_lbl_total_quantity").innerText = final_qty;
            var promotion_qty_element2 = document.getElementById("ContentPlaceHolder1_lbl_point_balance");

            if (promotion_qty_element2) {
                document.getElementById("ContentPlaceHolder1_lbl_point_balance").innerText = final_dc;
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

