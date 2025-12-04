<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Purchase_History_Details.aspx.cs" Inherits="Purchase_History_Details" %>

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

        .btn-danger {
            color: #fff !important;
            background-color: #dc3545 !important;
            border-color: #dc3545 !important;
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
                    <p class="membership_title fw-200">Order - </p>
                    &nbsp;<p class="membership_title fw-500">Purchase History Details</p>
                </div>
            </div>

            <div class="order_frame">
                <div class="row mb-3">
                    <div class="col-12 mb-3 title_bar">
                        <asp:Label runat="server">Order Information</asp:Label>
                    </div>
                    <div class="col-12 p-0">
                        <div>
                            <div class="row">
                                <div class="col-4 col-lg-3">
                                    <label>Order No</label>
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
                                    <label>Order Status</label>
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
                                    <label>Delivery Service</label>
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
                                    <label>Payment Status</label>
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
                                    <label>Total Order Quantity</label>
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
                                    <label>Total Order Weight</label>
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
                        <asp:Label runat="server">Shipping Information</asp:Label>
                    </div>
                    <div class="col-12 p-0">
                        <div>
                            <div class="row">
                                <div class="col-4 col-lg-3">
                                    <label>Recipient Name</label>
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
                                    <label>Contact</label>
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
                                    <label>Delivery Address</label>
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
                                    <label>Postcode</label>
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
                                    <label>City</label>
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
                                    <label>State</label>
                                </div>
                                <div class="col-1 col-lg-1">
                                    <span>:</span>
                                </div>
                                <div class="col-6 col-lg-8">
                                    <asp:Label runat="server" ID="lbl_delivery_state"></asp:Label>
                                </div>
                            </div>
                            <div class="row" runat="server" id="div_delivery_company" visible="false">
                                <div class="col-4 col-lg-3">
                                    <label>Delivery Company</label>
                                </div>
                                <div class="col-1 col-lg-1">
                                    <span>:</span>
                                </div>
                                <div class="col-6 col-lg-8">
                                    <asp:Label runat="server" ID="lbl_delivery_company"></asp:Label>
                                </div>
                            </div>
                            <div class="row" runat="server" id="div_delivery_tracking_no" visible="false">
                                <div class="col-4 col-lg-3">
                                    <label>Tracking No.</label>
                                </div>
                                <div class="col-1 col-lg-1">
                                    <span>:</span>
                                </div>
                                <div class="col-6 col-lg-8">
                                    <asp:Label runat="server" ID="lbl_tracking_no" ClientIDMode="Static"></asp:Label>
                                    &nbsp;
                                        <div onclick="CopyLink()" style="display: inline; cursor: pointer; border: 1px solid #000000; border-radius: 5px; background-color: black; padding: 3px 5px;"><i class="text-white fa fa-clone"></i></div>
                                    <a runat="server" id="btn_track_now" href="https://www.tracking.my/" class="text-white btn-sm btn-primary ms-2">Track Now</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row mb-4" runat="server" id="div_self_pickup">
                    <div class="col-12 mb-3 title_bar">
                        <asp:Label runat="server">Self Pickup Information</asp:Label>
                    </div>
                    <div class="col-12 p-0">
                        <div>
                            <div class="row">
                                <div class="col-4 col-lg-3">
                                    <label>Person In Charge</label>
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
                                    <label>Phone Number</label>
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
                                    <label>Pickup Address</label>
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
                                    <label>Operation Time</label>
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
                        <asp:Label runat="server">Order Items</asp:Label>
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
                                                <label>Variation</label>
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
                                                <label>Normal Qty</label>
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
                                                <label>Normal Price</label>
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
                                                <label>DC Qty</label>
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
                                                <label>DC Price</label>
                                            </div>
                                            <div class="col-1 col-lg-1">
                                                <span>:</span>
                                            </div>
                                            <div class="col-6 col-lg-8">
                                                <asp:Label runat="server" ID="lbl_dc_price"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-4 col-lg-3">
                                                <label>RP Qty</label>
                                            </div>
                                            <div class="col-1 col-lg-1">
                                                <span>:</span>
                                            </div>
                                            <div class="col-6 col-lg-8">
                                                <asp:Label runat="server" ID="lbl_rp_qty"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-4 col-lg-3">
                                                <label>RP Points</label>
                                            </div>
                                            <div class="col-1 col-lg-1">
                                                <span>:</span>
                                            </div>
                                            <div class="col-6 col-lg-8">
                                                <asp:Label runat="server" ID="lbl_rp_points"></asp:Label>
                                            </div>
                                        </div>
                                        <hr />
                                        <div class="row">
                                            <div class="col-4 col-lg-3">
                                                <label>Total</label>
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
                <div class="row" runat="server" id="div_payment_slip">
                    <div class="col-12 mb-3 title_bar">
                        <asp:Label runat="server" id="lbl_436">Payment Slips</asp:Label>
                    </div>
                    <div class="col-12 p-0">
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
                <div class="row">
                    <div class="col-12 mb-3 title_bar">
                        <asp:Label runat="server">Payment Information</asp:Label>
                    </div>
                    <div class="col-12 p-0">
                        <div>
                            <div class="row">
                                <div class="col-4 col-lg-3">
                                    <label>Total RP Used</label>
                                </div>
                                <div class="col-1 col-lg-1">
                                    <span>:</span>
                                </div>
                                <div class="col-6 col-lg-8">
                                    <asp:Label runat="server" ID="lbl_total_rp_used"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-4 col-lg-3">
                                    <label>Total DC Used</label>
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
                                    <label>Total DC Earn</label>
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
                                    <label>Total BV Earn</label>
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
                                    <label>Subtotal</label>
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
                                    <label>Shipping Fees</label>
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
                                    <label>Shipping Discount</label>
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
                                    <label>Wallet Amount</label>
                                </div>
                                <div class="col-1 col-lg-1">
                                    <span>:</span>
                                </div>
                                <div class="col-6 col-lg-8">
                                    <asp:Label runat="server" ID="lbl_wallet_amount"></asp:Label>
                                </div>
                            </div>
                            <div style="display:none" class="row">
                                <div class="col-4 col-lg-3">
                                    <label>Product Wallet Amount</label>
                                </div>
                                <div class="col-1 col-lg-1">
                                    <span>:</span>
                                </div>
                                <div class="col-6 col-lg-8">
                                    <asp:Label runat="server" ID="lbl_product_wallet_amount"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-4 col-lg-3">
                                    <label>Total Amount</label>
                                </div>
                                <div class="col-1 col-lg-1">
                                    <span>:</span>
                                </div>
                                <div class="col-6 col-lg-8">
                                    <asp:Label runat="server" ID="lbl_total_amount"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 p-0" runat="server" id="div_cancel" visible="false">
                        <asp:Button runat="server" Text="Cancel Order" ID="btn_cancel" CssClass="btn btn-danger form-control" OnClick="btn_cancel_Click" OnClientClick="this.disabled = true; this.value = 'Processing...'; __doPostBack(this.name,'');" />
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

    <script>
        function CopyLink() {
            const trackingText = document.getElementById("lbl_tracking_no").innerText;
            if (navigator.clipboard) {
                navigator.clipboard.writeText(trackingText).then(() => {
                    alert("Tracking number copied: " + trackingText);
                }).catch(err => {
                    alert("Failed to copy: " + err);
                });
            } else {
                // Fallback for older browsers
                const tempInput = document.createElement("input");
                tempInput.value = trackingText;
                document.body.appendChild(tempInput);
                tempInput.select();
                document.execCommand("copy");
                document.body.removeChild(tempInput);
                alert("Tracking number copied: " + trackingText);
            }
        }

        function openEnlargedModal(imageSrc) {
            $('#payment_slip_enlarge_modal').modal('show');
            var modalImg = document.getElementById('<%= enlarged_img_payment_slip.ClientID %>');

            modalImg.src = imageSrc;
        }

        function openUploadSlipModal() {
            $('#upload_Slip_Modal').modal('show');
        }
    </script>

</asp:Content>

