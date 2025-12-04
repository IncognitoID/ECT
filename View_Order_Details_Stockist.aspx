<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="View_Order_Details_Stockist.aspx.cs" Inherits="View_Order_Details_Stockist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style>
        .table tr, .table td, .table th {
            text-align: center;
        }

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

        .order_frame {
            border: 2px solid #efefef;
            border-radius: 10px;
            margin: 0px;
            padding: 10px;
            font-size: 13px;
        }

        .title_bar {
            background-color: #57585c;
            color: white;
            padding: 10px;
            font-weight: bold;
            font-size: 1rem;
        }

        .product_title {
            font-size: 25px;
            font-weight: 400;
        }

        label {
            margin-bottom: 0px;
        }

        .row {
            /*margin:15px;*/
            margin: 5px 0px;
        }

        @media only screen and (max-width: 767px) {

            .order_frame {
                border: 2px solid #efefef;
                border-radius: 10px;
                margin: 0px;
                padding: 10px;
                font-size: 11px;
            }

            .product_title {
                font-size: 18px;
                font-weight: 400;
            }

            .row {
                display: -ms-flexbox;
                display: flex;
                -ms-flex-wrap: wrap;
                flex-wrap: wrap;
                margin-right: 0px !important;
                margin-left: 0px !important;
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

            .row {
                margin: 0px !important;
            }

            .col-4, .col-1, .col-6 {
                padding-left: 10px !important;
                padding-right: 10px !important;
            }
        }
    </style>

    <style class="payment-slip-styles">
        .bg-green {
            background: #149474 !important;
        }

        .bg-black {
            background: black !important;
        }

        .payment-slip-img-div {
            border: 1px solid lightgray;
            width: 250px;
            height: 250px;
            overflow: hidden;
        }

        .payment-slip-img {
            min-height: 100%;
            min-width: 100%;
            object-fit: contain;
        }

        .imgpaymentslips {
            width: 100px;
            height: 100px;
            font-size: 40px;
            border: 1px solid lightgray;
            border-color: #ccc #ccc #bbb;
            border-radius: 3px;
            background: #e6e6e6;
            color: white;
        }

            .imgpaymentslips div {
                height: 100%;
                width: 100%;
            }

            .imgpaymentslips .slipimg {
                align-content: center;
                overflow: hidden;
                text-align: center;
            }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="pf-details mt-4 mb-4">
        <div class="container">
            <div class="row text-left mb-3">
                <div class="col-12 d-inline-flex">
                    <p class="membership_title fw-200" id="lbl_403">Mobile Stockist - </p>
                    &nbsp;<p class="membership_title fw-500" id="lbl_404">Order History Details</p>
                </div>
            </div>

            <div class="order_frame">
                <div class="row mb-3">
                    <div class="col-12 mb-3 title_bar">
                        <asp:Label runat="server" id="lbl_405">Order Information</asp:Label>
                    </div>
                    <div class="col-12 p-0">
                        <div>
                            <div class="row">
                                <div class="col-4 col-lg-3">
                                    <label id="lbl_406">Order No</label>
                                </div>
                                <div class="col-1 col-lg-1">
                                    <span>:</span>
                                </div>
                                <div class="col-6 col-lg-8">
                                    <asp:Label runat="server" ID="lbl_order_no"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-4 col-lg-3">
                                    <label id="lbl_407">Order Status</label>
                                </div>
                                <div class="col-1 col-lg-1">
                                    <span>:</span>
                                </div>
                                <div class="col-6 col-lg-8">
                                    <asp:Label runat="server" ID="lbl_order_status"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-4 col-lg-3">
                                    <label id="lbl_408">Delivery Service</label>
                                </div>
                                <div class="col-1 col-lg-1">
                                    <span>:</span>
                                </div>
                                <div class="col-6 col-lg-8">
                                    <asp:Label runat="server" ID="lbl_delivery_service"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-4 col-lg-3">
                                    <label id="lbl_409">Payment Status</label>
                                </div>
                                <div class="col-1 col-lg-1">
                                    <span>:</span>
                                </div>
                                <div class="col-6 col-lg-8">
                                    <asp:Label runat="server" ID="lbl_payment_status"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-4 col-lg-3">
                                    <label id="lbl_410">Total Order Quantity</label>
                                </div>
                                <div class="col-1 col-lg-1">
                                    <span>:</span>
                                </div>
                                <div class="col-6 col-lg-8">
                                    <asp:Label runat="server" ID="lbl_order_quantity"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-4 col-lg-3">
                                    <label id="lbl_411">Total Order Weight</label>
                                </div>
                                <div class="col-1 col-lg-1">
                                    <span>:</span>
                                </div>
                                <div class="col-6 col-lg-8">
                                    <asp:Label runat="server" ID="lbl_order_weight"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row" runat="server" id="div_delivery">
                    <div class="col-12 mb-3 title_bar">
                        <asp:Label runat="server" id="lbl_412">Shipping Information</asp:Label>
                    </div>
                    <div class="col-12 p-0">
                        <div>
                            <div class="row">
                                <div class="col-4 col-lg-3">
                                    <label id="lbl_413">Recipient Name</label>
                                </div>
                                <div class="col-1 col-lg-1">
                                    <span>:</span>
                                </div>
                                <div class="col-6 col-lg-8">
                                    <asp:Label runat="server" ID="lbl_delivery_name"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-4 col-lg-3">
                                    <label id="lbl_414">Contact</label>
                                </div>
                                <div class="col-1 col-lg-1">
                                    <span>:</span>
                                </div>
                                <div class="col-6 col-lg-8">
                                    <asp:Label runat="server" ID="lbl_delivery_contact_no"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-4 col-lg-3">
                                    <label id="lbl_415">Delivery Address</label>
                                </div>
                                <div class="col-1 col-lg-1">
                                    <span>:</span>
                                </div>
                                <div class="col-6 col-lg-8">
                                    <asp:Label runat="server" ID="lbl_delivery_address"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-4 col-lg-3">
                                    <label id="lbl_416">Postcode</label>
                                </div>
                                <div class="col-1 col-lg-1">
                                    <span>:</span>
                                </div>
                                <div class="col-6 col-lg-8">
                                    <asp:Label runat="server" ID="lbl_delivery_postcode"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-4 col-lg-3">
                                    <label id="lbl_417">City</label>
                                </div>
                                <div class="col-1 col-lg-1">
                                    <span>:</span>
                                </div>
                                <div class="col-6 col-lg-8">
                                    <asp:Label runat="server" ID="lbl_delivery_city"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-4 col-lg-3">
                                    <label id="lbl_418">State</label>
                                </div>
                                <div class="col-1 col-lg-1">
                                    <span>:</span>
                                </div>
                                <div class="col-6 col-lg-8">
                                    <asp:Label runat="server" ID="lbl_delivery_state"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row mb-4" runat="server" id="div_self_pickup">
                    <div class="col-12 mb-3 title_bar">
                        <asp:Label runat="server" id="lbl_419">Self Pickup Information</asp:Label>
                    </div>
                    <div class="col-12 p-0">
                        <div>
                            <div class="row">
                                <div class="col-4 col-lg-3">
                                    <label id="lbl_420">Person In Charge</label>
                                </div>
                                <div class="col-1 col-lg-1">
                                    <span>:</span>
                                </div>
                                <div class="col-6 col-lg-8">
                                    <asp:Label runat="server" ID="lbl_person_in_charge"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-4 col-lg-3">
                                    <label id="lbl_421">Phone Number</label>
                                </div>
                                <div class="col-1 col-lg-1">
                                    <span>:</span>
                                </div>
                                <div class="col-6 col-lg-8">
                                    <asp:Label runat="server" ID="lbl_person_in_charge_phone_number"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-4 col-lg-3">
                                    <label id="lbl_422">Pickup Address</label>
                                </div>
                                <div class="col-1 col-lg-1">
                                    <span>:</span>
                                </div>
                                <div class="col-6 col-lg-8">
                                    <asp:Label runat="server" ID="lbl_pickup_address"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-4 col-lg-3">
                                    <label id="lbl_423">Operation Time</label>
                                </div>
                                <div class="col-1 col-lg-1">
                                    <span>:</span>
                                </div>
                                <div class="col-6 col-lg-8">
                                    <asp:Label runat="server" ID="lbl_operation_time"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12 mb-3 title_bar">
                        <asp:Label runat="server" id="lbl_424">Order Items</asp:Label>
                    </div>
                    <div class="col-12 p-0">
                        <asp:Repeater runat="server" ID="rpt_item" OnItemDataBound="rpt_item_ItemDataBound">
                            <ItemTemplate>
                                <div class="row">
                                    <div class="col-12 col-lg-3 text-center">
                                        <img runat="server" id="product_img" />
                                    </div>
                                    <div class="col-12 col-lg-9 m-auto">
                                        <div class="row mt-0 mb-0">
                                            <div class="col-12">
                                                <asp:Label runat="server" ID="lbl_product_name" CssClass="product_title"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-4 col-lg-3">
                                                <label id="lbl_437">Variation</label>
                                            </div>
                                            <div class="col-1 col-lg-1">
                                                <span>:</span>
                                            </div>
                                            <div class="col-6 col-lg-8">
                                                <asp:Label runat="server" ID="lbl_product_variation"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-4 col-lg-3">
                                                <label id="lbl_438">Normal Qty</label>
                                            </div>
                                            <div class="col-1 col-lg-1">
                                                <span>:</span>
                                            </div>
                                            <div class="col-6 col-lg-8">
                                                <asp:Label runat="server" ID="lbl_normal_qty"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-4 col-lg-3">
                                                <label id="lbl_439">Normal Price</label>
                                            </div>
                                            <div class="col-1 col-lg-1">
                                                <span>:</span>
                                            </div>
                                            <div class="col-6 col-lg-8">
                                                <asp:Label runat="server" ID="lbl_normal_price"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-4 col-lg-3">
                                                <label id="lbl_440">DC Qty</label>
                                            </div>
                                            <div class="col-1 col-lg-1">
                                                <span>:</span>
                                            </div>
                                            <div class="col-6 col-lg-8">
                                                <asp:Label runat="server" ID="lbl_dc_qty"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-4 col-lg-3">
                                                <label id="lbl_441">DC Price</label>
                                            </div>
                                            <div class="col-1 col-lg-1">
                                                <span>:</span>
                                            </div>
                                            <div class="col-6 col-lg-8">
                                                <asp:Label runat="server" ID="lbl_dc_price"></asp:Label>
                                            </div>
                                        </div>
                                        <hr />
                                        <div class="row">
                                            <div class="col-4 col-lg-3">
                                                <label id="lbl_442">Total</label>
                                            </div>
                                            <div class="col-1 col-lg-1">
                                            </div>
                                            <div class="col-6 col-lg-8">
                                                <asp:Label runat="server" ID="lbl_total_price"></asp:Label>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <hr />
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12 mb-3 title_bar">
                        <asp:Label runat="server" id="lbl_425">Payment Information</asp:Label>
                    </div>
                    <div class="col-12 p-0">
                        <div>
                            <div class="row">
                                <div class="col-4 col-lg-3">
                                    <label id="lbl_426">Total DC Used</label>
                                </div>
                                <div class="col-1 col-lg-1">
                                    <span>:</span>
                                </div>
                                <div class="col-6 col-lg-8">
                                    <asp:Label runat="server" ID="lbl_total_dc_used"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-4 col-lg-3">
                                    <label id="lbl_427">Total DC Earn</label>
                                </div>
                                <div class="col-1 col-lg-1">
                                    <span>:</span>
                                </div>
                                <div class="col-6 col-lg-8">
                                    <asp:Label runat="server" ID="lbl_dc_earn"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-4 col-lg-3">
                                    <label id="lbl_428">Total BV Earn</label>
                                </div>
                                <div class="col-1 col-lg-1">
                                    <span>:</span>
                                </div>
                                <div class="col-6 col-lg-8">
                                    <asp:Label runat="server" ID="lbl_bv_earn"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-4 col-lg-3">
                                    <label id="lbl_429">Subtotal</label>
                                </div>
                                <div class="col-1 col-lg-1">
                                    <span>:</span>
                                </div>
                                <div class="col-6 col-lg-8">
                                    <asp:Label runat="server" ID="lbl_subtotal"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-4 col-lg-3">
                                    <label id="lbl_430">Shipping Fees</label>
                                </div>
                                <div class="col-1 col-lg-1">
                                    <span>:</span>
                                </div>
                                <div class="col-6 col-lg-8">
                                    <asp:Label runat="server" ID="lbl_shipping_fees"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-4 col-lg-3">
                                    <label id="lbl_431">Shipping Discount</label>
                                </div>
                                <div class="col-1 col-lg-1">
                                    <span>:</span>
                                </div>
                                <div class="col-6 col-lg-8">
                                    <asp:Label runat="server" ID="lbl_shipping_discount"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-4 col-lg-3">
                                    <label id="lbl_432">Total Amount</label>
                                </div>
                                <div class="col-1 col-lg-1">
                                    <span>:</span>
                                </div>
                                <div class="col-6 col-lg-8">
                                    <asp:Label runat="server" ID="lbl_total_amount"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-4 col-lg-3">
                                    <label id="lbl_433">Payment Type</label>
                                </div>
                                <div class="col-1 col-lg-1">
                                    <span>:</span>
                                </div>
                                <div class="col-6 col-lg-8">
                                    <asp:Label runat="server" ID="lbl_payment_type"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-4 col-lg-3">
                                    <label id="lbl_434">Payment Reference</label>
                                </div>
                                <div class="col-1 col-lg-1">
                                    <span>:</span>
                                </div>
                                <div class="col-6 col-lg-8">
                                    <asp:Label runat="server" ID="lbl_payment_reference"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-4 col-lg-3">
                                    <label id="lbl_435">Remark</label>
                                </div>
                                <div class="col-1 col-lg-1">
                                    <span>:</span>
                                </div>
                                <div class="col-6 col-lg-8">
                                    <asp:Label runat="server" ID="lbl_remark"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12 mb-3 title_bar">
                        <asp:Label runat="server" id="lbl_436">Payment Slips</asp:Label>
                    </div>
                    <div class="col-12 p-0">
                        <div class="row" style="justify-content: right;">
                            <asp:Button runat="server" ID="btn_upload_slip_img" Text="Upload Photo" CssClass="btn btn_primary bg-green text-white" OnClientClick="openUploadSlipModal(); return false;" />
                        </div>
                        <div class="row" style="overflow: auto">
                            <div class="row" style="gap: 10px; flex-wrap: nowrap; margin: 10px 0;">
                                <asp:Repeater ID="rpt_payment_slip" runat="server">
                                    <ItemTemplate>
                                        <div class="payment-slip-img-div">
                                            <asp:Image ID="img_payment_slip" runat="server" CssClass="payment-slip-img" ImageUrl='<%# Eval("Img_Link") %>' onclick="openEnlargedModal(this.src)" />
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </section>

    <div class="modal fade" id="payment_slip_enlarge_modal" tabindex="-1" role="dialog" aria-labelledby="paymentSlipModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <asp:Image runat="server" ID="enlarged_img_payment_slip" />
            </div>
        </div>
    </div>

    <div class="modal fade" id="upload_Slip_Modal" tabindex="-1" role="dialog" aria-labelledby="uploadSlipModalLabel" aria-hidden="true">
        <div class="modal-dialog " role="document">
            <div class="modal-content">
                <div class="modal-body" style="display: flex; flex-direction: column;">
                    <div class="row"><span style="font-size: 16px; font-weight: 500;"><span id="lbl_443">Files selected:</span></span></div>
                    <div id="previewField1_div" class="col-12" style="gap: 10px; min-height: 160px; max-height: 250px; border: 1px dashed gray; border-radius: 10px; overflow: auto; margin-bottom: 10px;">
                        <div class="form-group row align-items-center " style="height: 100%; padding: 10px;">
                            <div class="col" style="height: 100%; overflow: auto; padding: 15px;">
                                <div class="row " style="margin: 0; gap: 10px;" id="previewField1"></div>
                            </div>
                        </div>
                    </div>
                    <asp:FileUpload ID="FileUpload1" AllowMultiple="true" runat="server" type="file" name="file" onchange="previewFile()" accept="image/*" CssClass="hidden dropzone-modern dz-square dz-clickable dropzone initialized fileuploadwidth" />
                    <span class="hidden dropzone-upload-message text-center"></span>
                    <asp:Button runat="server" ID="btn_file_upload" Text="Choose Files" CssClass="btn btn_primary bg-green text-white" OnClientClick="triggerFileUpload(); return false;" formnovalidate />
                    <asp:Button runat="server" ID="btn_confirm" Text="Confirm" CssClass="btn btn_primary bg-black text-white" OnClick="btn_upload_slip_img_Click" />
                </div>
            </div>
        </div>
    </div>

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

    <script>
        $(document).ready(function () {
            Load_Language();
        });

        function Load_Language() {
            var page = 'Stockist Order Details';
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
                        } else if (item.Label_Type === 'New_Button') {
                            var element = document.getElementById('ContentPlaceHolder1_' + item.Label_Name);
                            if (element) {
                                element.textContent = item.Language_Content;
                            }
                        } else if (item.Label_Type === 'LinkButton') {
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

        function openEnlargedModal(imageSrc) {
            $('#payment_slip_enlarge_modal').modal('show');
            var modalImg = document.getElementById('<%= enlarged_img_payment_slip.ClientID %>');

            modalImg.src = imageSrc;
        }

        function openUploadSlipModal() {
            $('#upload_Slip_Modal').modal('show');
        }

        function triggerFileUpload() {
            var fileUploadElement = document.getElementById('<%= FileUpload1.ClientID %>');

            if (fileUploadElement) {
                fileUploadElement.click();
            } else {
                console.error('File upload element not found.');
            }
        }


        var selectedFiles = []; // Store the selected files

        function previewFile() {
            var fileInput = document.querySelector('#<%=FileUpload1.ClientID %>').files;
            selectedFiles = Array.from(fileInput); // Copy the selected files

            var previewField1 = document.getElementById("previewField1");

            // Clear previous previews
            previewField1.innerHTML = '';

            // Allowed file types
            var allowedExtensions = ['image/jpeg', 'image/png', 'image/jpg'];

            // Flag to track whether there is a non-image file
            var invalidFileFound = false;

            if (selectedFiles.length > 0) {
                previewField1.className += 'btmmargin15em ';
            }

            selectedFiles.forEach((file, index) => {
                // Check if the file type is allowed
                if (!allowedExtensions.includes(file.type)) {
                    invalidFileFound = true;
                    return;
                }

                const reader = new FileReader();

                reader.onload = () => {
                    var image1 = new Image();
                    image1.src = String(reader.result);

                    // Add the preview with the "remove" button for each image
                    var imageWrapper1 = `<div class="imgpaymentslips">
                                <div class="image-frame mb-2">
                                    <div class="image-frame-wrapper slipimg">
                                        <img class="img-fluid" src="${image1.src}"/>
                                    </div>
                                </div>
                             </div>`;

                    // Add to preview fields
                    previewField1.innerHTML += imageWrapper1;
                };

                reader.readAsDataURL(file);
            });

            // If an invalid file is found, show the warning message
            if (invalidFileFound) {
                ShowMessage_warning("Only image files are acceptable.", MessageType.Warning);
                previewField1.innerHTML = ''; // Clear the preview if invalid files are found
                selectedFiles = []; // Clear selected files
            }
        }


    </script>
</asp:Content>

