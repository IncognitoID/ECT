<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Invoice.aspx.vb" Inherits="Invoice" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link href="https://ecentra.com.my/Backoffice/Template/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://ecentra.com.my/Backoffice/Template/vendor/bootstrap/js/bootstrap.min.js"></script>
    <script src="https://ecentra.com.my/Backoffice/Template/vendor/jquery/jquery.min.js"></script>
    <script src="js/html2pdf.bundle.min.js"></script>

    <style type="text/css">
        .modal-backdrop {
            background: none;
        }


        #datatable-responsive > tbody > tr > td:nth-child(2) {
            text-align: left;
            padding: 10px;
        }

        .padding {
            padding: 5px;
        }

        @media screen {
            .maindiv {
                margin-left: 40px;
                margin-right: 40px;
            }
        }

        .labelfontsize {
            font-size: 10px !important;
        }

        .tabledebtor {
            width: 100%;
        }

        #datatable-responsive th {
            border-top: 1px solid black;
            border-bottom: 1px solid black;
            border: 1px solid white;
        }

        #datatable-responsive > tr {
            border: none;
        }

        #datatable-responsive > tbody > tr > td {
            margin: 1px;
            padding: 10px 0px;
            border: 1px solid black;

        }

        #footers {
            bottom: 0;
            left: 0;
            right: 0;
        }

        body {
            background-color: white;
            color: black;
        }

        .invoice_font_size {
            font-size: 12px;
        }

        .invoice_label {
            margin-bottom: 0.5rem;
            display: inline-block;
        }

        th {
            font-weight: normal;
            padding: 10px 0px;
        }

        #datatable-responsive > thead > tr {
            background-color: #404040;
            color: white;
        }

        label{
            font-weight: 500;
        }

        .computer_generated{
            margin: 10rem 0px;
        }

        .company_details{
            font-size:11px;
            justify-content: center;
        }

        #divFooter{
            text-align: center;
        }

        .product_table{
            font-size: 12px; font-weight: normal; white-space: nowrap; text-align: center;
        }

        .title{
            white-space:nowrap;
        }

        @media screen and (max-width: 900px) {
            .computer_generated{
                margin: 7rem 0px;
                font-size:11px;
            }
        }

        @media screen and (max-width: 700px) {
            #closebtn {
                width: 75%;
                left: 50%;
                margin-left: -37.5%;
                position: fixed;
                text-align: center;
            }

            .qrcodeimg {
                height: 30px !important;
                width: 30px !important;
            }

            #shopnamelbl {
                font-size: 12px;
            }

            #shopadd1lbl, #shopadd2lbl, #shopadd3lbl, #shopadd4lbl, #gstid, #shopgstidlbl, #telnolbl, #shopphonenolbl, .totaltd {
                font-size: 9px !important;
            }

            #gsttable {
                font-size: 5px !important;
            }

            #Notetitlelbl {
                font-size: smaller !important;
            }

            .labelfontsize, #datatable-responsive {
                font-size: 6px !important;
            }

            .printcls, #panelbtn {
                display: none;
            }

            .maindiv {
                margin: 10px;
            }

            .invoice_font_size {
                font-size: 9px;
            }

            .computer_generated{
                margin: 5rem 0px;
                font-size:8px;
            }
        }

        @media print {
            .maindiv {
                padding-top: 10px !important;
                padding-bottom:0px !important;
            }

            @page {
                margin-top: 0;
                margin-bottom: 0;
            }

            body {
                padding-top: 20px;
                padding-bottom: 0px;
            }

            body {
                -webkit-print-color-adjust: exact; /* Chrome, Safari */
                color-adjust: exact; /* Firefox */
            }

            #datatable-responsive > thead > tr {
                background-color: black;
                color: white;
            }

            #closebtn {
                display: none;
            }

            .invoice_font_size {
                font-size: 15px;
            }

            .divBody{
                margin-top:4rem !important;
                margin-bottom:4rem !important;
            }

            .product_table{
                font-size: 15px; font-weight: normal; white-space: nowrap; text-align: center;
            }

            .computer_generated{
                margin: 10rem 0px;
                font-size:15px;
            }

            .company_details{
                font-size:15px;
                justify-content: center;
            }

            #divFooter{
                bottom:30px;
                position:absolute;
                margin-bottom: 0px;
                text-align: center;
            }

            form{
                margin-block-end: 0px !important;
                margin-bottom:0px !important;
                padding-bottom:0px !important;
            }

        }
    </style>

    <script>

        var dt;
        var ds = "<table id='datatable-responsive' class='product_table' cellspacing='0' width='100%'><thead>";
        $(document).ready(function () {
            $('#vbhtml').html(ds + dt);
        });

        function back() {
            window.close();
        }

        // New download function
        function downloadInvoice() {
            // Select the invoice content div
            var element = document.getElementById('SalesOrdercontent_1');

            // Configure html2pdf options
            var opt = {
                margin: [0.1, 0.1, 0.1, 0.1], // Top, Left, Bottom, Right (in inches) - reduced to 0.1 inches
                filename: 'Invoice_' + getParameterByName('id') + '.pdf',
                image: { type: 'jpeg', quality: 0.98 },
                html2canvas: { scale: 2, useCORS: true },
                jsPDF: { unit: 'in', format: 'letter', orientation: 'portrait' }
            };

            // Generate and download the PDF
            html2pdf().set(opt).from(element).save();
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">

        <div id="closebtn" style="width: 75%; left: 50%; margin-left: -37.5%; position: fixed; text-align: center; margin-top: 5px;">
            <asp:Button ID="btnsave" runat="server" OnClientClick="downloadInvoice(); return false;" Style="margin-bottom: 10px" CssClass="btn btn-primary btn-sm" Text="Download Invoice" />
        </div>

        <div class="maindiv" id="SalesOrdercontent_1" style="padding-top: 40px;">
            <div class="row">
                <div class="col-12 text-center" style="border-bottom: 1px solid black;">
                    <h4>INVOICE</h4>
                </div>
            </div>

            <div class="mt-4 row">
                <div class="col-6">
                    <table class="invoice_font_size">
                        <tr>
                            <td>
                                <label class="title">Member ID</label></td>
                            <td>
                                <label>:&nbsp;</label></td>
                            <td>
                                <asp:Label runat="server" ID="lbl_member_id" CssClass="invoice_label"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <label class="title">Member Name</label></td>
                            <td>
                                <label>:&nbsp;</label></td>
                            <td>
                                <asp:Label runat="server" ID="lbl_member_name" CssClass="invoice_label"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <label class="title">Order No</label></td>
                            <td>
                                <label>:&nbsp;</label></td>
                            <td>
                                <asp:Label runat="server" ID="lbl_order_no" CssClass="invoice_label"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <label class="title">Order Date</label></td>
                            <td>
                                <label>:&nbsp;</label></td>
                            <td>
                                <asp:Label runat="server" ID="lbl_order_date" CssClass="invoice_label"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <label class="title">Order Handle By</label></td>
                            <td>
                                <label>:&nbsp;</label></td>
                            <td>
                                <asp:Label runat="server" ID="lbl_order_handle_by" CssClass="invoice_label"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <label class="title">Payment Reference No.</label></td>
                            <td>
                                <label>:&nbsp;</label></td>
                            <td>
                                <asp:Label runat="server" ID="lbl_payment_reference" CssClass="invoice_label"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <label class="title">Payment Remark</label></td>
                            <td>
                                <label>:&nbsp;</label></td>
                            <td>
                                <asp:Label runat="server" ID="lbl_payment_remark" CssClass="invoice_label"></asp:Label></td>
                        </tr>
                    </table>
                </div>

                <div class="col-6">
                    <table class="invoice_font_size">
                        <tr>
                            <td>
                                <label class="title" runat="server" id="lbl_title_1">Recipient Name</label></td>
                            <td>
                                <label>:&nbsp;</label></td>
                            <td>
                                <asp:Label runat="server" ID="lbl_receiver_name" CssClass="invoice_label"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <label class="title" runat="server" id="lbl_title_3">Contact No.</label></td>
                            <td>
                                <label>:&nbsp;</label></td>
                            <td>
                                <asp:Label runat="server" ID="lbl_receiver_contact" CssClass="invoice_label"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <label class="title">Shipping Method</label></td>
                            <td>
                                <label>:&nbsp;</label></td>
                            <td>
                                <asp:Label runat="server" ID="lbl_shipping_method" CssClass="invoice_label"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <label class="title" runat="server" id="lbl_title_2">Delivery Address</label></td>
                            <td>
                                <label>:&nbsp;</label></td>
                            <td>
                                <asp:Label runat="server" ID="lbl_shipping_address" CssClass="invoice_label"></asp:Label></td>
                        </tr>
                    </table>
                </div>
            </div>

            <div class="mt-4 mb-4 divBody" id="body">
                <div id="vbhtml"></div>
            </div>

            <div class="row">
                <div class="col-md-1 col-1"></div>
                <div class="col-md-5 col-5">
                    <table class="invoice_font_size">
                        <tr>
                            <td>
                                <label class="title">Total BV</label></td>
                            <td>
                                <label>:&nbsp;</label></td>
                            <td>
                                <asp:Label runat="server" ID="lbl_total_bv" CssClass="invoice_label"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <label class="title">Total DC Used</label></td>
                            <td>
                                <label>:&nbsp;</label></td>
                            <td>
                                <asp:Label runat="server" ID="lbl_total_dc_used" CssClass="invoice_label"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <label class="title">Total DC Earned</label></td>
                            <td>
                                <label>:&nbsp;</label></td>
                            <td>
                                <asp:Label runat="server" ID="lbl_total_dc_earn" CssClass="invoice_label"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <label class="title">Total RP Used</label></td>
                            <td>
                                <label>:&nbsp;</label></td>
                            <td>
                                <asp:Label runat="server" ID="lbl_total_rp_used" CssClass="invoice_label"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <label class="title">Total RP Earned</label></td>
                            <td>
                                <label>:&nbsp;</label></td>
                            <td>
                                <asp:Label runat="server" ID="lbl_total_rp_earn" CssClass="invoice_label"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <label class="title">Payment Type</label></td>
                            <td>
                                <label>:&nbsp;</label></td>
                            <td>
                                <asp:Label runat="server" ID="lbl_payment_type" CssClass="invoice_label"></asp:Label></td>
                        </tr>
                    </table>
                </div>

                <div class="col-md-5 col-5">
                    <table class="invoice_font_size">
                        <tr>
                            <td>
                                <label class="title">Subtotal</label></td>
                            <td>
                                <label>:&nbsp;</label></td>
                            <td>
                                <asp:Label runat="server" ID="lbl_subtotal" CssClass="invoice_label"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <label class="title">Shipping Fees</label></td>
                            <td>
                                <label>:&nbsp;</label></td>
                            <td>
                                <asp:Label runat="server" ID="lbl_shipping_fees" CssClass="invoice_label"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <label class="title">Shipping Discount</label></td>
                            <td>
                                <label>:&nbsp;</label></td>
                            <td>
                                <asp:Label runat="server" ID="lbl_shipping_discount" CssClass="invoice_label"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <label class="title">Total</label></td>
                            <td>
                                <label>:&nbsp;</label></td>
                            <td>
                                <asp:Label runat="server" ID="lbl_total" CssClass="invoice_label"></asp:Label></td>
                        </tr>
                    </table>
                </div>
                <div class="col-md-1 col-1"></div>
            </div>

            <div id="divFooter" class="row">
                <div class="col-12 computer_generated">
                    <label>"This is a computer generated copy. No signature is required"</label>
                </div>
                <div class="col-12 mt-3 mb-3" style="display: inline-flex; align-items: flex-end; flex-wrap: nowrap; justify-content: center;">
                    <img src="img/Logo/invoice_logo_v2.png" class="w-50" style="margin-left: 5rem;" />&nbsp;<span>(1514070-M)</span>
                </div>
                <div class="col-12 company_details d-flex">
                    <label>ECENTRA SDN BHD</label>&nbsp;
                    <label style="font-weight: normal;">E-12A-2, The Icon Tower (East Wing), Level 12A, No 1, Jalan 1/68F, Jalan Tun Razak, 50400, Kuala Lumpur</label>
                </div>
            </div>

        </div>

        <script>
            function getParameterByName(name, url) {
                if (!url) url = window.location.href;
                name = name.replace(/[\[\]]/g, "\\$&");
                var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
                    results = regex.exec(url);
                if (!results) return null;
                if (!results[2]) return '';
                return decodeURIComponent(results[2].replace(/\+/g, " "));
            }

            function printfunction() {
                window.print()
            }

        </script>

    </form>

</body>
</html>
