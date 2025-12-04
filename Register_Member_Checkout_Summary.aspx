<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Register_Member_Checkout_Summary.aspx.cs" Inherits="Register_Member_Checkout_Summary" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style>
        body {
            background-color: #f4f6f9;
        }

        .account-box, .card {
            border: none;
            border-radius: 16px;
            background: #ffffff;
            box-shadow: 0 8px 24px rgba(0, 0, 0, 0.05);
            padding: 30px;
            margin-bottom: 30px;
        }

        .form-section {
            background: #fff;
            border-radius: 12px;
            padding: 20px;
            margin-bottom: 20px;
            border: 1px solid #dee2e6;
        }

        .product-option {
            transition: all 0.3s ease;
        }

            .product-option.active {
                border: 2px solid #ffc107 !important;
                background-color: #fffbea;
            }

        .modal-custom .option {
            border-bottom: 1px solid #eee;
            padding: 12px;
            cursor: pointer;
            display: flex;
            justify-content: space-between;
            align-items: center;
            border-radius: 6px;
        }

            .modal-custom .option:hover {
                background-color: #f8f9fa;
            }

            .modal-custom .option.selected {
                background-color: #e9f7ef;
                border: 2px solid #28a745;
                font-weight: bold;
            }

                .modal-custom .option.selected i {
                    display: inline !important;
                }

        .summary-section {
            background: #f9f9f9;
            border-radius: 12px;
            padding: 20px;
            margin-top: 30px;
        }

        .summary-row {
            display: flex;
            justify-content: space-between;
            margin-bottom: 8px;
        }

        .summary-total {
            font-weight: bold;
            font-size: 18px;
        }

        .breadcrumb__area {
            background: linear-gradient(to right, #fceabb, #f8b500);
            padding: 0px 0;
            color: #fff;
        }

        .breadcrumb .breadcrumb-item a {
            color: #000000;
        }

        .breadcrumb .breadcrumb-item.active {
            color: #000000;
            font-weight: bold;
        }

        .breadcrumb__wrapper .breadcrumb {
            margin-bottom: 0;
            padding-top: 20px;
            padding-bottom: 20px;
        }

        .product-date {
            font-size: 0.9rem;
            color: #6c757d;
        }

            .product-date i {
                margin-right: 6px;
                color: #ffc107;
            }

        .font-weight-bold {
            font-weight: bold;
        }

        /* Chrome, Safari, Edge, Opera */
        input[type=number]::-webkit-inner-spin-button,
        input[type=number]::-webkit-outer-spin-button {
            -webkit-appearance: none;
            margin: 0;
        }

        /* Firefox */
        input[type=number] {
            -moz-appearance: textfield;
        }

        .nice-select {
            line-height: 30px;
        }

        .form-select {
            background: none;
        }

        .nice-select:after {
            height: 7px;
            width: 7px;
            right: 20px;
        }

        .list-group input[type="radio"] {
            display: none;
        }

        .list-group label {
            display: flex;
            justify-content: space-between;
            align-items: center;
            padding: 12px 16px;
            border: 1px solid #ccc;
            border-radius: 6px;
            margin-bottom: 10px;
            cursor: pointer;
            background-color: white;
            transition: all 0.2s ease-in-out;
            font-weight: 500;
        }

        .list-group input[type="radio"]:checked + label {
            border: 2px solid #4ade80;
            background-color: #ecfdf5;
            color: #065f46;
            font-weight: bold;
        }

        .list-group label::after {
            content: '';
            width: 18px;
            height: 18px;
            border: 2px solid #ccc;
            border-radius: 50%;
            background-color: white;
            margin-left: auto;
        }

        .list-group input[type="radio"]:checked + label::after {
            border-color: #22c55e;
            background-color: #22c55e;
            background-image: url('data:image/svg+xml;utf8,<svg fill="white" height="16" viewBox="0 0 24 24" width="16" xmlns="http://www.w3.org/2000/svg"><path d="M20.285 6.709l-11.1 11.1-5.475-5.475 1.414-1.414 4.06 4.061 9.686-9.686z"/></svg>');
            background-repeat: no-repeat;
            background-position: center;
        }
    </style>

    <style>
        /* Remove <br> between radio items */
        .payment-options br {
            display: none !important;
        }

        /* Styling the list */
        .payment-options input[type="radio"] {
            display: none;
        }

        .payment-options label {
            display: block;
            width: 100%;
            border: 1px solid #ccc;
            border-radius: 8px;
            padding: 12px 16px;
            margin-bottom: 10px;
            cursor: pointer;
            font-weight: 500;
            background-color: white;
            transition: all 0.2s ease-in-out;
            position: relative;
        }

        .payment-options input[type="radio"]:checked + label {
            border: 2px solid #22c55e;
            background-color: #f0fdf4;
            color: #166534;
            font-weight: 600;
        }

        .payment-options label::after {
            content: '';
            position: absolute;
            top: 50%;
            right: 16px;
            transform: translateY(-50%);
            width: 20px;
            height: 20px;
            border-radius: 50%;
            border: 2px solid #ccc;
            background-color: white;
        }

        .payment-options input[type="radio"]:checked + label::after {
            background-color: #22c55e;
            border-color: #22c55e;
            background-image: url("data:image/svg+xml,%3Csvg viewBox='0 0 24 24' fill='white' xmlns='http://www.w3.org/2000/svg'%3E%3Cpath d='M9 16.2l-3.5-3.6-1.4 1.4L9 19 20 8l-1.4-1.4z'/%3E%3C/svg%3E");
            background-repeat: no-repeat;
            background-position: center;
        }

        .text-black {
            color: black;
        }

        #countdownTimer {
            font-size: 1.4rem;
            letter-spacing: 1px;
        }
    </style>

    <style class="payment-method">
        .green-btn {
            background-color: #149474 !important;
        }

        #ewalletModal {
            align-content: center;
        }

            #ewalletModal .modal-content {
                padding: 0;
            }

            #ewalletModal .ewallet-password-div {
                height: 15vh;
                padding: 20px;
                justify-content: center;
            }

            #ewalletModal .member-details-div {
                display: flex;
                flex-direction: column;
                gap: 10px;
                margin-bottom: 10px;
            }

            #ewalletModal .ewallet-payment-div {
                gap: 10px;
                display: none;
                flex-direction: column;
            }

            #ewalletModal .ewallet-password-div, #ewalletModal .payment-details-div, #ewalletModal .payment-details-div div {
                display: flex;
                flex-direction: column;
                gap: 10px;
            }

            #ewalletModal .payment-details-div {
                padding: 10px;
                border: 1px solid lightgray;
                border-radius: 10px;
            }

            #ewalletModal hr {
                margin: 0;
            }

            #ewalletModal .payment-details-amount-div input[type="text"] {
                margin-bottom: 10px;
            }

        .none {
            display: none;
        }

        .custom-radio {
            position: relative;
            display: inline-block;
        }

            .custom-radio .form-check-input {
                display: none;
            }

            .custom-radio .form-check-label {
                padding: 0.5rem 1.5rem;
                background-color: #e9ecef;
                border: 2px solid #ced4da;
                border-radius: 20px;
                cursor: pointer;
                transition: all 0.3s ease;
                font-weight: 500;
                color: #495057;
            }

            .custom-radio .form-check-input:checked + .form-check-label {
                background-color: #28a745;
                color: #fff;
                border-color: #218838;
                box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
            }

            .custom-radio .form-check-label:hover {
                background-color: #dee2e6;
                border-color: #adb5bd;
            }
    </style>

    <style>
        .btn_primary {
            background-color: #1A76D1 !important;
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

        #previewField2_div {
            display: none;
        }

        .paymentrequirement1 {
            display: none;
        }

        .paymentrequirement2 {
            display: block;
        }

        .remove-btn {
            position: absolute;
            top: 5px;
            right: 5px;
            background-color: red;
            color: white;
            border: none;
            border-radius: 50%;
            width: 20px;
            height: 20px;
            cursor: pointer;
        }

        .image-frame-wrapper {
            position: relative;
        }


        @media only screen and (max-width: 767px) {

            #previewField2_div {
                display: block;
                margin: 10px 0 !important;
            }

            #previewField1_div {
                display: none;
            }

            #btnUploadFiles_div {
                text-align: right;
            }

            .paymentsliptextdiv .col-12 {
                padding: 0;
            }

            .paymentrequirement1 {
                display: block;
            }

            .paymentrequirement2 {
                display: none;
            }
        }
    </style>

    <style>
        /* Remove <br> between radio items */
        .payment-options br {
            display: none !important;
        }

        /* Styling the list */
        .payment-options input[type="radio"] {
            display: none;
        }

        .payment-options label {
            display: block;
            width: 100%;
            border: 1px solid #ccc;
            border-radius: 8px;
            padding: 12px 16px;
            margin-bottom: 10px;
            cursor: pointer;
            font-weight: 500;
            background-color: white;
            transition: all 0.2s ease-in-out;
            position: relative;
        }

        .payment-options input[type="radio"]:checked + label {
            border: 2px solid #22c55e;
            background-color: #f0fdf4;
            color: #166534;
            font-weight: 600;
        }

        .payment-options label::after {
            content: '';
            position: absolute;
            top: 50%;
            right: 16px;
            transform: translateY(-50%);
            width: 20px;
            height: 20px;
            border-radius: 50%;
            border: 2px solid #ccc;
            background-color: white;
        }

        .payment-options input[type="radio"]:checked + label::after {
            background-color: #22c55e;
            border-color: #22c55e;
            background-image: url("data:image/svg+xml,%3Csvg viewBox='0 0 24 24' fill='white' xmlns='http://www.w3.org/2000/svg'%3E%3Cpath d='M9 16.2l-3.5-3.6-1.4 1.4L9 19 20 8l-1.4-1.4z'/%3E%3C/svg%3E");
            background-repeat: no-repeat;
            background-position: center;
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

        .new_address_textbox {
            padding: 10px !important;
            margin: 10px 0px;
        }

        .nice-select {
            height: 45px !important;
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <main>

        <div class="container my-5">
            <div class="row">
                <div class="col-lg-12">

                    <div id="loadingOverlay" style="display: none;">
                        <div class="spinner-border text-primary" role="status">
                            <i class="fa fa-spinner fa-spin spinnercolor"></i>
                        </div>
                    </div>

                    <div class="account-box card">

                        <div class="row text-left mb-3">
                            <div class="col-12 d-inline-flex">
                                <p class="membership_title fw-200" id="lbl_331">Register New Member - </p>
                                &nbsp;<p class="membership_title fw-200" id="lbl_332">Checkout Summary</p>
                            </div>
                        </div>

                        <hr />

                        <div class="form-section d-flex align-items-center justify-content-between flex-wrap mb-4" style="background: linear-gradient(to right, #f9fafb, #e3f2fd); border-left: 6px solid #2196f3;">
                            <div>
                                <h5 class="fw-bold text-primary mb-1" id="lbl_501">Order Session Timer</h5>
                                <div class="d-flex">
                                    <p class="mb-0 text-muted small" id="lbl_502">You have </p>
                                    &nbsp;<span id="countdownTimer" class="fw-bold text-danger">15:00</span>&nbsp;<p class="mb-0 text-muted small" id="lbl_503">minutes to complete your order.</p>
                                </div>
                            </div>
                            <div>
                                <i class="fa fa-clock fa-2x text-primary"></i>
                            </div>
                        </div>

                        <div class="form-section">
                            <h5 class="mb-3 fw-bold" id="lbl_478">Payment Method :</h5>
                            <div class="col-12 p-0">
                                <asp:DropDownList ID="ddlPaymentMethod" runat="server" CssClass="w-100 new_address_textbox" onchange="handlePaymentChange(this)" formnovalidate>
                                </asp:DropDownList>
                                <asp:Button runat="server" ID="btn_online_payment" CssClass="d-none" OnClick="btn_online_payment_Click" formnovalidate></asp:Button>
                            </div>
                        </div>

                        <div class="form-section" id="div_manual_payment_details" style="display: none;">
                            <h5 class="mb-3 fw-bold" id="lbl_504">Manual Transfer Bank Details</h5>
                            <div class="row g-3">
                                <div class="col-md-4">
                                    <div class="text-muted small font-weight-bold" id="lbl_505">Bank</div>
                                    <div class="fw-semibold text-black"><span runat="server" id="lbl_bank"></span></div>
                                </div>
                                <div class="col-md-4">
                                    <div class="text-muted small font-weight-bold" id="lbl_506">Account Name</div>
                                    <div class="fw-semibold text-black"><span runat="server" id="lbl_account_name"></span></div>
                                </div>
                                <div class="col-md-4">
                                    <div class="text-muted small font-weight-bold" id="lbl_507">Account Number</div>
                                    <div class="fw-semibold text-black">
                                        <span runat="server" id="lbl_account_number"></span>
                                        &nbsp;
                                        <div onclick="CopyLink()" style="display: inline; cursor: pointer; border: 1px solid #000000; border-radius: 5px; background-color: black; padding: 1px 5px;"><i style="font-size: 13px;" class="text-white fa fa-clone"></i></div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="form-section" id="div_payment_slip" style="display: none;">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12 col-lg-4 m-auto paymentsliptextdiv">
                                        <div class="col-12">
                                            <div class="col-12">
                                                <label id="lbl_481" class="mb-3 font-weight-bold paymentslipstyle">Please Upload Payment Slip</label>
                                            </div>
                                            <div class="col-12 paymentrequirement1">
                                                <span style="color: red;" id="lbl_482">*Minimum 1 Payment Slip</span>
                                            </div>
                                        </div>

                                        <div id="previewField2_div" class="col-12" style="gap: 10px; min-height: 160px; max-height: 250px; border: 1px dashed gray; border-radius: 10px; margin: 0 auto; overflow: auto; padding: 10px;">
                                            <div class="form-group row align-items-center " style="max-height: 150px; width: 100%; margin: auto;">
                                                <div class="col" style="height: 100%; overflow: auto;">
                                                    <div class="row " style="margin: 0; gap: 10px;" id="previewField2"></div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-12">
                                            <div class="col-12" id="btnUploadFiles_div">
                                                <asp:Button runat="server" ID="btn_upload_slip_img" Text="Upload Photo" CssClass="btn btn_primary bg-green text-white" OnClientClick="triggerFileUpload(); return false;" formnovalidate />
                                            </div>
                                            <div class="col-12 paymentrequirement2">
                                                <span style="color: red;" id="lbl_483">*Minimum 1 Payment Slip</span>
                                            </div>
                                        </div>

                                    </div>
                                    <div id="previewField1_div" class="col-12 col-lg-7" style="gap: 10px; min-height: 160px; max-height: 250px; border: 1px dashed gray; border-radius: 10px; margin: 0 auto">
                                        <div class="form-group row align-items-center " style="height: 100%; padding: 10px;">
                                            <div class="col" style="height: 100%; overflow: auto; padding: 15px;">
                                                <div class="row " style="margin: 0; gap: 10px;" id="previewField1"></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>


                        <!-- Payment Summary -->
                        <div class="summary-section">
                            <h5 class="mb-3 fw-bold" id="lbl_495">Payment Details</h5>

                            <div class="summary-row">
                                <span id="lbl_496">Subtotal</span>
                                <span id="itemSubtotal" runat="server">RM 0.00</span>
                            </div>

                            <div class="summary-row">
                                <span id="lbl_497">Shipping Total</span>
                                <span id="shippingtotal" runat="server">RM 0.00</span>
                            </div>

                            <div class="summary-row">
                                <span id="lbl_498">Shipping Discount</span>
                                <span id="shippingdiscount" runat="server">- RM 0.00</span>
                            </div>

                            <div class="summary-row">
                                <span id="lbl_499">Wallet Payment</span>
                                <span id="walletamount" runat="server">- RM 0.00</span>
                            </div>

                            <hr />
                            <div class="summary-row summary-total">
                                <span id="lbl_500">Total Payment</span>
                                <span id="totalPayment" runat="server">RM 0.00</span>
                            </div>
                        </div>

                        <!-- Submit -->
                        <div class="text-end text-right mt-3">
                            <asp:LinkButton runat="server" ID="btn_submit" CssClass="btn btn-success px-5 py-2 rounded-pill" OnClick="btn_submit_Click" OnClientClick="return validateTermsAgreement();">Confirm</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <asp:Button runat="server" Text="Place Order" CssClass="hidden btn btn_primary bg-black text-white" OnClick="btn_check_fileupload_Click" ID="btn_check_fileupload" OnClientClick="ShowLoading()" formnovalidate />
        <asp:HiddenField runat="server" ID="hforderid" />
        <asp:FileUpload ID="FileUpload1" AllowMultiple="true" runat="server" type="file" name="file" onchange="previewFile()" accept="image/*" CssClass=" hidden dropzone-modern dz-square dz-clickable dropzone initialized fileuploadwidth" />
        <asp:HiddenField ID="hfShowModal" runat="server" />

        <asp:HiddenField ID="rbeWallet" runat="server" Value="online" />
        <asp:HiddenField ID="hdn_wallet_deduct_amount" runat="server" Value="0" />
        <asp:HiddenField ID="hdn_wallet_balance" runat="server" Value="0" />

        <!-- Updated eWallet Modal -->
        <div class="modal fade" id="ewalletModal" tabindex="-1" role="dialog" aria-labelledby="ewalletModalLabel" aria-hidden="true" data-backdrop="static">
            <div class="modal-dialog modal-dialog-centered" role="document">
                <div class="modal-content rounded-4 shadow-lg border-0" style="background: linear-gradient(145deg, #ffffff, #f8f9fa);">
                    <div class="modal-header border-0 py-3">
                        <h5 class="modal-title w-100 text-center fw-bold text-dark" style="font-size: 1.5rem;" id="lbl_484">Pay with Wallet</h5>
                    </div>

                    <hr />

                    <div class="modal-body p-4">
                        <div class="ewallet-password-div mb-4">
                            <label for="txt_ewallet_password" class="form-label fw-semibold text-dark" id="lbl_485">Wallet Password</label>
                            <div class="position-relative">
                                <asp:TextBox ID="txt_ewallet_password" runat="server"
                                    CssClass="form-control w-100 rounded-3 shadow-sm pe-5"
                                    PlaceHolder="Enter password"
                                    TextMode="Password"
                                    Style="padding: 0.75rem 2.5rem 0.75rem 0.75rem; border: 1px solid #ced4da;"></asp:TextBox>

                                <span class="position-absolute" style="top: 50%; right: 1rem; transform: translateY(-50%); cursor: pointer;"
                                    onclick="togglePasswordVisibility('<%= txt_ewallet_password.ClientID %>')">
                                    <i class="fa fa-eye-slash" id="eyeIcon_<%= txt_ewallet_password.ClientID %>"></i>
                                </span>
                            </div>
                        </div>
                        <div class="ewallet-payment-div">
                            <div class="mb-4">
                                <div class="d-flex justify-content-between align-items-center mb-2">
                                    <label class="fw-semibold text-dark" id="lbl_486">Wallet Balance:</label>
                                    <asp:Label ID="lbl_wallet_balance" runat="server" CssClass="font-weight-bold"></asp:Label>
                                </div>
                                <div class="d-flex justify-content-between align-items-center">
                                    <label class="fw-semibold text-dark" id="lbl_487">Total Amount:</label>
                                    <asp:Label ID="lbl_tot_amount" runat="server" CssClass="font-weight-bold"></asp:Label>
                                </div>
                            </div>

                            <hr style="margin-bottom: 1.5em !important;" />

                            <div class="mb-4">
                                <label class="fw-semibold text-dark mb-2" id="lbl_488">Payment Method</label>
                                <div class="d-flex gap-3">
                                    <asp:RadioButtonList ID="rbrn_payment_option" runat="server" RepeatLayout="Flow" RepeatDirection="Vertical" CssClass="payment-options w-100">
                                    </asp:RadioButtonList>
                                </div>
                            </div>

                            <div class="mb-4 payment-details-amount-div" style="display: none;">
                                <label for="txt_ewallet_amount" class="form-label fw-semibold text-dark" id="lbl_489">Please key in partial payment amount</label>
                                <asp:TextBox ID="txt_ewallet_amount" runat="server" CssClass="form-control shadow-sm" PlaceHolder="0.00" onkeypress="return checkAmount(event)" oninput="validateDecimalPlaces(this, 2);" onblur="validateAndFormatPartialAmount()" onfocus="resetInputFlag()" Style="padding: 0.75rem; border: 1px solid #ced4da;"></asp:TextBox>
                            </div>

                            <hr style="margin-bottom: 1.5em !important;" />

                            <div class="border rounded-4 p-4 bg-white shadow-sm">
                                <h6 class="fw-bold mb-3" id="lbl_490">Payment Summary</h6>
                                <hr style="margin-bottom: 1rem !important;" />
                                <div class="row">
                                    <div class="col-6 text-secondary fw-semibold" id="lbl_491">Total Amount</div>
                                    <div class="col-6 text-end text-dark text-right">
                                        <asp:Label ID="lbl_final_total_amount" runat="server"></asp:Label>
                                    </div>

                                    <div class="col-6 text-secondary fw-semibold" id="lbl_492">Wallet Pay Amount</div>
                                    <div class="col-6 text-end text-dark text-right">
                                        <asp:Label ID="lbl_wallet_balance_pay" runat="server"></asp:Label>
                                    </div>

                                    <div class="col-6 fw-bold border-top" id="lbl_493">Amount Need To Pay</div>
                                    <div class="col-6 text-end fw-bold border-top text-right font-weight-bold">
                                        <asp:Label ID="lbl_final_amount_need_to_pay" runat="server" CssClass="font-weight-bold"></asp:Label>
                                    </div>

                                    <div class="col-6 text-secondary fw-semibold" id="lbl_494">Final Wallet Balance</div>
                                    <div class="col-6 text-end text-dark text-right">
                                        <asp:Label ID="lbl_final_wallet_balance" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>

                    <div class="modal-footer border-0 pt-3 pb-4">
                        <asp:Button runat="server" Text="Cancel" CssClass="btn btn-outline-secondary w-50 me-2 rounded-3 shadow-sm" ID="btn_Cancel" OnClick="btn_cancel_ewallet_Click" formnovalidate Style="padding: 0.75rem; transition: all 0.3s ease;" />
                        <asp:Button runat="server" Text="Confirm" CssClass="btn btn-success w-50 rounded-3 shadow-sm" ID="btn_Confirm_Password" OnClientClick="handleConfirmClick(); return false;" formnovalidate Style="padding: 0.75rem; transition: all 0.3s ease;" />
                        <asp:Button runat="server" Text="Confirm" CssClass="btn btn-success w-50 rounded-3 shadow-sm" ID="btn_Confirm_Ewallet" Style="display: none; padding: 0.75rem;" OnClick="btn_confirm_ewallet_Click" formnovalidate />
                    </div>
                </div>
            </div>
        </div>

    </main>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

    <script class="payment-slip-script">

        function triggerFileUpload() {
            var fileUploadElement = document.getElementById('<%= FileUpload1.ClientID %>');

            if (fileUploadElement) {
                fileUploadElement.click();
            } else {
                console.error('File upload element not found.');
            }
        }

        var selectedFiles = []; // Store the selected files
        const MAX_FILE_SIZE = 10 * 1024 * 1024; // 10 MB in bytes

        function previewFile() {
            var fileInput = document.querySelector('#<%=FileUpload1.ClientID %>').files;
            selectedFiles = Array.from(fileInput); // Copy the selected files

            var previewField1 = document.getElementById("previewField1");
            var previewField2 = document.getElementById("previewField2");

            // Clear previous previews
            previewField1.innerHTML = '';
            previewField2.innerHTML = '';

            if (selectedFiles.length > 0) {
                previewField1.className += 'btmmargin15em ';
                previewField2.className += 'btmmargin15em ';
            }

            selectedFiles.forEach((file, index) => {

                // Check file size
                if (file.size > MAX_FILE_SIZE) {
                    alert(`${file.name} exceeds the 10 MB limit and will not be added.`);
                    return; // Skip this file
                }

                const reader = new FileReader();

                reader.onload = () => {
                    var image1 = new Image();
                    image1.src = String(reader.result);
                    var image2 = new Image();
                    image2.src = String(reader.result);

                    // Add the preview with the "remove" button for each image
                    var imageWrapper1 = `<div class="imgpaymentslips">
                                    <div class="image-frame mb-2">
                                        <div class="image-frame-wrapper slipimg">
                                            <img class="img-fluid" src="${image1.src}"/>
                                            <button class="remove-btn" onclick="removeImage(${index})">X</button>
                                        </div>
                                    </div>
                                 </div>`;

                    var imageWrapper2 = `<div class="imgpaymentslips">
                                    <div class="image-frame mb-2">
                                        <div class="image-frame-wrapper slipimg">
                                            <img class="img-fluid" src="${image2.src}"/>
                                            <button class="remove-btn" onclick="removeImage(${index})">X</button>
                                        </div>
                                    </div>
                                 </div>`;

                    // Add to preview fields
                    previewField1.innerHTML += imageWrapper1;
                    previewField2.innerHTML += imageWrapper2;
                };

                reader.readAsDataURL(file);
            });
        }

        function removeImage(index) {
            // Remove file from selectedFiles
            selectedFiles.splice(index, 1);

            // Recreate the FileList (since FileList is immutable, you cannot directly remove items)
            const dataTransfer = new DataTransfer();
            selectedFiles.forEach(file => dataTransfer.items.add(file));

            // Update the input element's FileList
            document.querySelector('#<%=FileUpload1.ClientID %>').files = dataTransfer.files;

            // Re-render the preview
            previewFile();
        }

        function btnCheckFileUploadClick() {
            var btnCheckFileUploadClickElement = document.getElementById('<%= btn_check_fileupload.ClientID %>');

            if (btnCheckFileUploadClickElement) {
                btnCheckFileUploadClickElement.click();
            } else {
                console.error('Check File Upload Button element not found.');
            }
        }

        <%--function btnPlaceOrderClick() {
            var btnPlaceOrderElement = document.getElementById('<%= btn_place_order.ClientID %>');

            if (btnPlaceOrderElement) {
                btnPlaceOrderElement.click();
            } else {
                console.error('Place Order Button element not found.');
            }
        }--%>

    </script>

    <script type="text/javascript" class="payment-method-script-3">

        document.addEventListener('DOMContentLoaded', function () {
            const radios_payment_option = document.querySelectorAll('input[name="ctl00$ContentPlaceHolder1$rbrn_payment_option"]');

            radios_payment_option.forEach(function (radio_payment) {
                radio_payment.addEventListener('change', function () {
                    if (this.checked) {
                        togglePaymentDetails(this.value);
                    }
                });
            });
        });

        //toggle ewallet amount
        function togglePaymentDetails(value) {
            const paymentDetailsDiv = document.querySelector('.payment-details-amount-div');
            const txtAmount = document.getElementById('<%= txt_ewallet_amount.ClientID %>');

            if (value === "fullewallet") {
                paymentDetailsDiv.style.display = 'none';
            } else {
                paymentDetailsDiv.style.display = 'block';
                setTimeout(() => txtAmount.focus(), 100);
            }

            updateEwalletCalculation(); // 🔁 recalculate
        }


        function validateAndFormatPartialAmount() {
            const txt = document.getElementById('<%= txt_ewallet_amount.ClientID %>');
            const totalText = document.getElementById('<%= lbl_tot_amount.ClientID %>').innerText.replace(/[^\d.]/g, '');
            const walletText = document.getElementById('<%= lbl_wallet_balance.ClientID %>').innerText.replace(/[^\d.]/g, '');

            const totalAmount = parseFloat(totalText) || 0;
            const walletBalance = parseFloat(walletText) || 0;
            let enteredAmount = parseFloat(txt.value) || 0;

            if (enteredAmount > totalAmount) {
                enteredAmount = totalAmount;
                sweetalert_warning("Amount cannot exceed total payment.", "warning");
            } else if (enteredAmount > walletBalance) {
                enteredAmount = walletBalance;
                sweetalert_warning("Amount cannot exceed wallet balance.", "warning");
            }

            txt.value = enteredAmount.toFixed(2); // format after user finishes input
            updateEwalletCalculation(); // update all labels
        }

        //display modal
        function handlePaymentChange(dropdown) {
            var selectedValue = dropdown.value;
            if (selectedValue === "online") {
                document.getElementById('div_manual_payment_details').style.display = "none";
                document.getElementById('div_payment_slip').style.display = "none";
                document.getElementById("<%= btn_online_payment.ClientID %>").click();
            } else if (selectedValue === "ewallet") {
                document.getElementById('div_manual_payment_details').style.display = "none";
                document.getElementById('div_payment_slip').style.display = "none";
                $('#ewalletModal').modal('show');
            } else if (selectedValue === "manual") {
                document.getElementById("<%= btn_online_payment.ClientID %>").click();
            }
        }

        function Show_Payment_Slip() {
            document.getElementById('div_payment_slip').style.display = "block";
            document.getElementById('div_manual_payment_details').style.display = "block";
        }

        function togglePasswordVisibility(inputId) {
            const input = document.getElementById(inputId);
            const icon = document.getElementById("eyeIcon_" + inputId);

            if (!input || !icon) return;

            if (input.type === "password") {
                input.type = "text";
                icon.classList.remove("fa-eye-slash");
                icon.classList.add("fa-eye");
            } else {
                input.type = "password";
                icon.classList.remove("fa-eye");
                icon.classList.add("fa-eye-slash");
            }
        }

        //display modal payment details div
        function showEwalletModalPayment() {
            $('#ewalletModal').modal('show');
            document.querySelector('.ewallet-payment-div').style.display = 'block';
            document.querySelector('.ewallet-password-div').style.display = 'none';
            document.getElementById('<%= btn_Confirm_Password.ClientID%>').style.display = 'none';
            document.getElementById('<%= btn_Confirm_Ewallet.ClientID%>').style.display = 'block';

            const paymentDetailsDiv = document.querySelector('.payment-details-amount-div');
            const selectedMethod = document.querySelector('#<%= rbrn_payment_option.ClientID %> input[type="radio"]:checked')?.value || '';
            const txtAmount = document.getElementById('<%= txt_ewallet_amount.ClientID %>');

            if (selectedMethod === 'partialewallet') {
                paymentDetailsDiv.style.display = 'block';
                setTimeout(() => txtAmount.focus(), 100);
            }
            updateEwalletCalculation(); // update all labels

        }

        //display modal password div
        function showEwalletModalPassword() {
            $('#ewalletModal').modal('show');
            document.querySelector('.ewallet-payment-div').style.display = 'none';
            document.querySelector('.ewallet-password-div').style.display = 'block';
            document.getElementById('<%= btn_Confirm_Password.ClientID%>').style.display = 'block';
            document.getElementById('<%= btn_Confirm_Ewallet.ClientID%>').style.display = 'none';
        }

        //close modal (cancel button)
        function canceleWallet() {
            $('#ewalletModal').modal('hide');

            return false;
        }

        //close ewallet modal (confirm button)
        function closeModalWithoutReset() {
            var modalElement = document.getElementById('ewalletModal');
            modalElement.classList.remove('show');

            document.body.classList.remove('modal-open');

            modalElement.style.display = 'none';
            modalElement.setAttribute('aria-hidden', 'true');

            removeModalBackdrop();

            document.getElementById('ewallet').checked = true;
        }

        //remove modal backdrop
        function removeModalBackdrop() {
            var backdrops = document.querySelectorAll('.modal-backdrop');
            backdrops.forEach(function (backdrop) {
                backdrop.parentNode.removeChild(backdrop);
            });
        }

        //validate password
        function handleConfirmClick() {
            var password = document.getElementById('<%= txt_ewallet_password.ClientID %>').value;

            $.ajax({
                type: "POST",
                url: "Checkout_Summary.aspx/ValidatePassword",
                data: JSON.stringify({ password: password }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var data = response.d;

                    if (data.IsValid) {
                        // Toggle visibility
                        document.querySelector('.ewallet-payment-div').style.display = 'block';
                        document.querySelector('.ewallet-password-div').style.display = 'none';
                        document.getElementById('<%= btn_Confirm_Password.ClientID %>').style.display = 'none';
                        document.getElementById('<%= btn_Confirm_Ewallet.ClientID %>').style.display = 'block';
                        var hdn_wallet_deduct_amount = document.getElementById('<%= hdn_wallet_deduct_amount.ClientID %>');
                        var hdn_wallet_balance = document.getElementById('<%= hdn_wallet_balance.ClientID %>');

                        // Format and set wallet balance
                        var walletBalance = parseFloat(data.WalletBalance || 0);
                        var walletFormatted = "RM " + walletBalance.toLocaleString('en-MY', {
                            minimumFractionDigits: 2,
                            maximumFractionDigits: 2
                        });
                        hdn_wallet_balance.value = data.WalletBalance;
                        document.getElementById('<%= lbl_wallet_balance.ClientID %>').innerText = walletFormatted;

                        // Get and parse total amount
                        var totalAmountText = document.getElementById('<%= totalPayment.ClientID %>').innerText.replace(/[^\d.-]/g, '');
                        var totalAmount = parseFloat(totalAmountText || 0);
                        var totalFormatted = "RM " + totalAmount.toLocaleString('en-MY', {
                            minimumFractionDigits: 2,
                            maximumFractionDigits: 2
                        });

                        // Update all summary labels
                        // Update all summary labels
                        document.getElementById('<%= lbl_tot_amount.ClientID %>').innerText = totalFormatted;
                        document.getElementById('<%= lbl_final_total_amount.ClientID %>').innerText = totalFormatted;

                        var walletUsed = Math.min(walletBalance, totalAmount);
                        var walletUsedFormatted = "- RM " + walletUsed.toLocaleString('en-MY', {
                            minimumFractionDigits: 2,
                            maximumFractionDigits: 2
                        });
                        document.getElementById('<%= lbl_wallet_balance_pay.ClientID %>').innerText = walletUsedFormatted;

                        // Calculate remaining and update
                        var remaining = totalAmount - walletUsed;
                        var remainingFormatted = "RM " + Math.max(0, remaining).toLocaleString('en-MY', {
                            minimumFractionDigits: 2,
                            maximumFractionDigits: 2
                        });

                        hdn_wallet_deduct_amount.value = walletUsed;
                        document.getElementById('<%= lbl_final_amount_need_to_pay.ClientID %>').innerText = remainingFormatted;
                        document.getElementById('<%= lbl_final_wallet_balance.ClientID %>').innerText = "RM " + Math.max(0, walletBalance - walletUsed).toLocaleString('en-MY', {
                            minimumFractionDigits: 2,
                            maximumFractionDigits: 2
                        });
                    } else {
                        sweetalert_warning("Incorrect password.", "warning");
                    }
                },
                error: function () {
                    sweetalert_warning("Error validating password.", "warning");
                }
            });

            return false;
        }

        function updateEwalletCalculation() {
            var hdn_wallet_deduct_amount = document.getElementById('<%= hdn_wallet_deduct_amount.ClientID %>');
            var hdn_wallet_balance = document.getElementById('<%= hdn_wallet_balance.ClientID %>');
            const lbl_wallet_balance = document.getElementById('<%= lbl_wallet_balance.ClientID %>');
            const lblTotalAmount = document.getElementById('<%= totalPayment.ClientID %>');
            const lblWalletPay = document.getElementById('<%= lbl_wallet_balance_pay.ClientID %>');
            const lblFinalTotal = document.getElementById('<%= lbl_final_total_amount.ClientID %>');
            const lblTotAmount = document.getElementById('<%= lbl_tot_amount.ClientID %>');
            const lblNeedToPay = document.getElementById('<%= lbl_final_amount_need_to_pay.ClientID %>');
            const lblFinalBalance = document.getElementById('<%= lbl_final_wallet_balance.ClientID %>');

            const txtPartialAmount = document.getElementById('<%= txt_ewallet_amount.ClientID %>');
            const selectedMethod = document.querySelector('#<%= rbrn_payment_option.ClientID %> input[type="radio"]:checked')?.value || '';

            const walletBalance = parseFloat(hdn_wallet_balance.value) || 0;
            const totalAmount = parseFloat(lblTotalAmount.innerText.replace(/[^\d.]/g, '')) || 0;
            let usedAmount = 0;

            if (selectedMethod === 'fullewallet') {
                usedAmount = Math.min(walletBalance, totalAmount);
            } else {
                let entered = parseFloat(txtPartialAmount.value) || 0;

                if (entered > totalAmount) entered = totalAmount;
                if (entered > walletBalance) entered = walletBalance;

                usedAmount = entered;
            }

            const remaining = totalAmount - usedAmount;
            const finalWallet = walletBalance - usedAmount;

            const format = amt => "RM " + amt.toLocaleString('en-MY', {
                minimumFractionDigits: 2,
                maximumFractionDigits: 2
            });

            const format_negative = amt => "- RM " + amt.toLocaleString('en-MY', {
                minimumFractionDigits: 2,
                maximumFractionDigits: 2
            });

            // Update labels
            hdn_wallet_deduct_amount.value = usedAmount;
            lbl_wallet_balance.innerText = format(walletBalance);
            lblWalletPay.innerText = format_negative(usedAmount);
            lblFinalTotal.innerText = format(totalAmount);
            lblTotAmount.innerText = format(totalAmount);
            lblNeedToPay.innerText = format(Math.max(0, remaining));
            lblFinalBalance.innerText = format(Math.max(0, finalWallet));
        }

        // Variable to track if it's the first input
        var isFirstInput = true;

        // Validate amount (input)
        function checkAmount(event) {
            var charCode = (event.which) ? event.which : event.keyCode;
            var inputValue = event.target.value;

            if (inputValue.length === 0) {
                isFirstInput = true;
            }

            if (isFirstInput) {
                if (charCode >= 48 && charCode <= 57) {
                    isFirstInput = false;
                    return true;
                }
                return false;
            } else {
                if (charCode == 8) {
                    return true;
                }

                if ((charCode >= 48 && charCode <= 57) || charCode == 46) {
                    if (charCode == 46 && inputValue.includes('.')) {
                        return false;
                    }

                    if (inputValue.includes('.')) {
                        var parts = inputValue.split('.');
                        if (parts[1].length >= 2) {
                            return false;
                        }
                    }
                    return true;
                }
                return false;
            }
        }

        // Validate amount (decimal places)
        function validateDecimalPlaces(element, decimalPlaces) {
            var value = element.value;

            if (value.includes('.')) {
                var parts = value.split('.');
                if (parts[1].length > decimalPlaces) {
                    element.value = parts[0] + '.' + parts[1].substring(0, decimalPlaces);
                }
            }
        }

        // Reset the flag on focus (optional)
        function resetInputFlag() {
            isFirstInput = true;
        }
    </script>

    <script>

        $(document).ready(function () {
            Load_Language();
        });

        function validateTermsAgreement() {
            // Optional: Add custom validations here
            // If all validations pass:
            document.getElementById("loadingOverlay").style.display = "flex";

            // Optional: Disable button to prevent resubmit
            document.getElementById("<%= btn_submit.ClientID %>").disabled = true;

            return true; // continue postback
        }

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
            var page = 'Checkout Summary';
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
                        } else {
                            window[item.Label_Name] = item.Language_Content;
                        }
                    });
                }
            });
        }


        let totalSeconds = 900; // 10 minutes
        const timerDisplay = document.getElementById("countdownTimer");
        const submitButton = document.getElementById("<%= btn_submit.ClientID %>");

        function startCountdown() {
            const interval = setInterval(() => {
                const minutes = Math.floor(totalSeconds / 60);
                const seconds = totalSeconds % 60;
                timerDisplay.textContent = `${minutes.toString().padStart(2, '0')}:${seconds.toString().padStart(2, '0')}`;

                if (totalSeconds <= 0) {
                    clearInterval(interval);
                    submitButton.disabled = true;
                    submitButton.classList.add("disabled", "btn-secondary");
                    submitButton.classList.remove("btn-success");

                    let language = "English"; // default
                    const cookies_language = getCookieValue("language");
                    if (cookies_language) {
                        language = cookies_language;
                    }

                    // Multilingual alert
                    let title = "⏰ Session Expired";
                    let message = "You’ve exceeded the 15-minute time limit for this order. For your security, the session has been closed. Please make another order.";
                    let buttonText = "Refresh Now";

                    if (language === "Chinese") {
                        title = "⏰ 订单倒计时已过期";
                        message = "您已超过15分钟的下单时间，为了您的安全，此订单倒计时已被关闭。请重新下单。";
                        buttonText = "立即刷新";
                    }

                    swal({
                        title: title,
                        text: message,
                        icon: "warning",
                        button: {
                            text: buttonText,
                            className: "swal-button--danger"
                        }
                    }, function () {
                        // Callback triggered after user clicks OK
                        document.cookie = "Payment_order_no=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
                        window.location.href = "Home.aspx";
                    });
                }

                totalSeconds--;
            }, 1000);
        }

        $(document).ready(() => {
            startCountdown();
        });
    </script>

    <script>
        function CopyLink() {
            const trackingText = document.getElementById("ContentPlaceHolder1_lbl_account_number").innerText;
            if (navigator.clipboard) {
                navigator.clipboard.writeText(trackingText).then(() => {
                    alert("Account number copied: " + trackingText);
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
                alert("Account number copied: " + trackingText);
            }
        }

        function sweetalert_success(message, messagetype) {
            swal({
                title: message,
                icon: "success",
                button: "OK",
            }, function () {
                window.location.href = "Home.aspx";
            });
        }

        function sweetalert_success_redirect(message, messagetype) {
            swal({
                title: message,
                icon: "success",
                button: "OK",
            });
            setTimeout(function () {
                window.location.href = "Home.aspx";
            }, 2000);
        }

        function sweetalert_warning(message, messagetype) {
            swal({
                title: message,
                icon: "warning",
                button: "OK",
            });
        }
    </script>

</asp:Content>

