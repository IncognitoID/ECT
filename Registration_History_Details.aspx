<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Registration_History_Details.aspx.cs" Inherits="Registration_History_Details" %>

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
            font-size:13px;
        }

        .title_bar {
            background-color: #57585c;
            color: white;
            padding: 10px;
            font-weight: bold;
            font-size: 1rem;
        }
        
        .product_title{
            font-size:25px;
            font-weight:400;
        }

        label{
            margin-bottom:0px;
        }

        .row{
            /*margin:15px;*/
            margin:5px 0px;
        }

        @media only screen and (max-width: 767px) {

            .order_frame {
                border: 2px solid #efefef;
                border-radius: 10px;
                margin: 0px;
                padding: 10px;
                font-size:11px;
            }

            .product_title{
                font-size:18px;
                font-weight:400;
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
                padding-left:10px !important;
                padding-right:10px !important;
            }
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
                    <div class="row">
                        <div class="col-12 mb-3 title_bar">
                            <asp:Label runat="server">Payment Information</asp:Label>
                        </div>
                        <div class="col-12 p-0">
                            <div>
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
                    </div>
            </div>
        </div>
    </section>

</asp:Content>

