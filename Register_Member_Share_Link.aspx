<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register_Member_Share_Link.aspx.cs" Inherits="Register_Member_Share_Link" EnableEventValidation="false" %>

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

    <style>
        .btn-oval {
            border-radius: 50px; /* Adjust the value to control the oval shape */
            padding-left: 20px; /* Adjust padding for the oval effect */
            padding-right: 20px; /* Adjust padding for the oval effect */
        }

        @media (min-width: 992px) {
            .borderBottom {
                border-bottom-width: 1px;
                border-bottom-style: solid;
                border-bottom-color: initial;
            }
        }

        .inline-block {
            display: inline-block;
        }

        .toggle {
            position: relative;
            display: inline-block;
            width: 85px;
            height: 38px;
            border-radius: 30px;
            border: 2px solid #ced4da;
        }

            /* After slide changes */
            .toggle:after {
                content: '';
                position: absolute;
                width: 31px;
                height: 31px;
                border-radius: 50%;
                background-color: white;
                border: 2px solid #ced4da;
                top: 2px;
                left: 1px;
                transition: all 0.5s;
            }

        /* Toggle text */
        .label-text {
            position: absolute;
            top: 50%;
            transform: translate(-50%, -50%);
            font-weight: bold;
            cursor: pointer;
        }

            .label-text:first-child {
                left: 20px; /* Adjust the left position for the "No" label */
            }

            .label-text:last-child {
                right: -5px; /* Adjust the right position for the "Yes" label */
            }

        /* Checkbox checked effect */
        .checkbox:checked + .toggle::after {
            left: 49px;
        }

        /*Checkbox vanished */
        .checkbox {
            display: none;
        }
    </style>

    <style>
        .header-nav-menu {
            /* add background styles here */
            height: 36px;
            background-size: 100%;
            line-height: 36px;
            margin-bottom: 10px;
        }

        .table-img {
            max-width: 100px;
        }

        .hidden {
            display: none;
        }
    </style>

    <style type="text/css">
        .switch {
            position: relative;
            display: inline-block;
            width: 50px;
            height: 24px;
            float: right;
            margin-left: 45px;
        }

            .switch input {
                opacity: 0;
            }

        .slider {
            position: absolute;
            cursor: pointer;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: #ccc;
            -webkit-transition: .4s;
            transition: .4s;
        }

            .slider:before {
                position: absolute;
                content: "";
                height: 16px;
                width: 16px;
                left: 4px;
                bottom: 4px;
                background-color: white;
                -webkit-transition: .4s;
                transition: .4s;
            }

        input:checked + .slider {
            background-color: #2196F3;
        }

        input:focus + .slider {
            box-shadow: 0 0 1px #2196F3;
        }

        input:checked + .slider:before {
            -webkit-transform: translateX(26px);
            -ms-transform: translateX(26px);
            transform: translateX(26px);
        }

        /* Rounded sliders */
        .slider.round {
            border-radius: 34px;
        }

            .slider.round:before {
                border-radius: 50%;
            }

        .p-10 {
            padding: 0px 10px;
        }

        input[type="text"], input[type="email"], input[type="url"], input[type="password"], input[type="search"], input[type="number"], input[type="tel"], input[type="range"], input[type="date"], input[type="month"], input[type="week"], input[type="time"], input[type="datetime"], input[type="datetime-local"], input[type="color"], textarea {
            padding: 7px 20px;
        }

        .membership_title {
            font-size: 20px;
            color: black;
        }

        .fw-200 {
            font-weight: 200;
        }

        .fw-500 {
            font-weight: 500;
        }
    </style>

    <style>
        #loadingOverlay {
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

        .member_card {
            background-color: #ebebeb;
            border-radius: 10px;
            padding: 10px;
            color: #1f1f1f;
            margin: 10px;
        }

        .div-memberid{
            margin: auto;
            padding: 5px 10px;
            border: 1px solid #dddddd;
            background-color: #e9ecef;
            border-radius: 3px 0px 0px 3px;
        }

        .div-memberid-margin {
            margin-bottom: 25px !important;
        }

        @media (max-width: 450px) {
            .div-memberid-margin {
                margin-bottom: 5px !important;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        
        <div id="loadingOverlay" style="display: none;">
            <div class="spinner-border text-primary" role="status">
                <i class="fa fa-spinner fa-spin spinnercolor"></i>
            </div>
        </div>

        <div id="registration" class="pb-5">

            <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:HiddenField ID="hfCheck" runat="server" />
                    <div class="container p-4">

                        <div class="row text-left mb-3">
                            <div class="col-12 d-inline-flex">
                                <p class="membership_title fw-200" id="lbl_120">Registration</p>
                            </div>
                        </div>

                        <hr style="background-color:#efefef;" />

                        <form>
                            <div class="row pb-3" style="border-bottom: 1px solid #efefef">
                                <div class="col-lg-5">
                                    <div class="form-group mb-3">
                                        <div class="row align-items-center">
                                            <div class="col-lg-4">
                                                <label class="mb-1" for="referralID" id="lbl_119">Direct Referral ID* </label>
                                            </div>
                                            <div class="col-lg-8">
                                                <input class="form-control" runat="server" id="txt_referal_id" type="text" required maxlength="10" oninput="lettersandNumberOnly(this)" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group mb-3">
                                        <div class="row align-items-center">
                                            <div class="col-lg-4">
                                                <label class="mb-1" id="lbl_82">Direct Referral Name* </label>
                                            </div>
                                            <div class="col-lg-8">
                                                <input class="form-control" runat="server" id="txt_referal_name" type="text" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group mb-3">
                                        <div class="row">
                                            <div class="col-lg-4"></div>
                                            <div class="col-lg-8">
                                                <div runat="server" id="btn_name" class="btn-white btn-oval inline-block mr-2 cursor_pointer" onclick="btnNameClick()" style="color: black; font-weight: 500; background-color: white; padding: 5px 10px; border: 1px solid #848383;">
                                                    <span id="lbl_85">Full Name</span>
                                                </div>
                                                <div runat="server" id="btn_company" class="btn-white btn-oval inline-block cursor_pointer" onclick="btnNameClick()" style="color: black; background-color: white; padding: 5px 10px; border: 1px solid #e9e8e8;">
                                                    <span id="lbl_86">Company</span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group mb-3" id="divName">
                                        <div class="form-group mb-3">
                                            <div class="row align-items-center">
                                                <div class="col-lg-4">
                                                    <label class="mb-1" id="lbl_87">Full Name Per NRIC* </label>
                                                </div>
                                                <div class="col-lg-8">
                                                    <input class="form-control" runat="server" id="txt_fullname" type="text" maxlength="50" onkeypress="return lettersOnly(event)" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group mb-3" id="div_cmpname" style="display: none;">
                                        <div class="row align-items-center">
                                            <div class="col-lg-4">
                                                <label class="mb-1" id="lbl_88">Company Name* </label>
                                            </div>
                                            <div class="col-lg-8">
                                                <input class="form-control" runat="server" id="txt_companyname" type="text" maxlength="50" onkeypress="return lettersOnly(event)" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group mb-3" id="div_cmpno" style="display: none;">
                                        <div class="row align-items-center">
                                            <div class="col-lg-4">
                                                <label class="mb-1" id="lbl_89">Company No.* </label>
                                            </div>
                                            <div class="col-lg-8">
                                                <input class="form-control" runat="server" id="txt_companyno" type="text" maxlength="15" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group mb-3" id="div_nricpass">
                                        <div class="row align-items-center">
                                            <div class="col-lg-4">
                                                <label class="mb-1" id="lbl_90">NRIC/Passport* </label>
                                            </div>
                                            <div class="col-lg-8">
                                                <asp:DropDownList runat="server" ID="ddl_type" CssClass="form-control" onchange="toggleDocumentInput()">
                                                    <asp:ListItem Text="NRIC">NRIC</asp:ListItem>
                                                    <asp:ListItem Text="Passport">Passport</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group mb-3" id="div_nric">
                                        <div class="row align-items-center">
                                            <div class="col-lg-4">
                                                <label class="label" for="name" id="lbl_91">NRIC *</label>
                                            </div>
                                            <div class="col-lg-8">
                                                <asp:TextBox runat="server" ID="txt_nric" CssClass="form-control" MaxLength="12" onkeypress='return event.charCode >= 48 && event.charCode <= 57'></asp:TextBox>
                                                <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator2" ControlToValidate="txt_nric" ValidationExpression="^\d{6}\d{2}\d{4}$" ErrorMessage="Invalid format. Please enter a valid NRIC number." Display="Dynamic" ForeColor="Red" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group mb-3" id="div_passport" style="display: none;">
                                        <div class="row align-items-center">
                                            <div class="col-lg-4">
                                                <label class="label" for="name" id="lbl_92">Passport *</label>
                                            </div>
                                            <div class="col-lg-8">
                                                <asp:TextBox runat="server" ID="txt_passport" CssClass="form-control" MaxLength="30"></asp:TextBox>
                                                <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator4" ControlToValidate="txt_passport" ValidationExpression="^(?!^0+$)[a-zA-Z0-9]{3,20}$" ErrorMessage="Invalid format. Please enter a valid Passport number." Display="Dynamic" ForeColor="Red" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group mb-3" id="div_gender">
                                        <div class="row align-items-center">
                                            <div class="col-lg-4">
                                                <label class="mb-1" id="lbl_93">Gender* </label>
                                            </div>
                                            <div class="col-lg-8">
                                                <select class="form-control" runat="server" id="ddl_gender">
                                                    <option value="" disabled="disabled" selected="selected" id="lbl_94">Select your gender</option>
                                                    <option value="Male" id="lbl_95">Male</option>
                                                    <option value="Female" id="lbl_96">Female</option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group mb-3">
                                        <div class="row align-items-center">
                                            <div class="col-lg-4">
                                                <label class="mb-1" id="lbl_97">Mobile No.* </label>
                                            </div>
                                            <div class="col-lg-8">
                                                <input class="form-control" runat="server" id="txt_phonenumber" type="text" required maxlength="11" onkeypress='return event.charCode >= 48 && event.charCode <= 57' />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group mb-3">
                                        <div class="row align-items-center">
                                            <div class="col-lg-4">
                                                <label class="mb-1" id="lbl_98">Email* </label>
                                            </div>
                                            <div class="col-lg-8">
                                                <input runat="server" id="txt_email" type="email" class="form-control" required maxlength="50">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-1"></div>
                                <div class="col-lg-5">
                                    <div class="form-group mb-3">
                                        <div class="row align-items-center">
                                            <div class="col-lg-4">
                                                <label class="mb-1" id="lbl_99">Nationality* </label>
                                            </div>
                                            <div class="col-lg-8">
                                                <asp:DropDownList runat="server" ID="ddl_nationality" CssClass="form-control">
                                                    <asp:ListItem Text="Select your nationality" Value="" Disabled="true" Selected="true"></asp:ListItem>
                                                    <asp:ListItem Text="Malaysian" Value="Malaysian"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group mb-3">
                                        <div class="row align-items-center">
                                            <div class="col-lg-4">
                                                <label class="mb-1" id="lbl_100">Country* </label>
                                            </div>
                                            <div class="col-lg-8">
                                                <asp:DropDownList runat="server" ID="ddl_country" CssClass="form-control" onchange="toggleCountry()">
                                                </asp:DropDownList>
                                                <input class="form-control hidden" runat="server" id="txt_country" type="text" maxlength="50" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group mb-3" id="div_state">
                                        <div class="row align-items-center">
                                            <div class="col-lg-4">
                                                <label class="mb-1" id="lbl_101">State* </label>
                                            </div>
                                            <div class="col-lg-8">
                                                <asp:DropDownList runat="server" ID="ddl_State" CssClass="form-control">
                                                </asp:DropDownList>
                                                <input class="form-control hidden" runat="server" id="txt_state" type="text" maxlength="50" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group mb-3">
                                        <div class="row align-items-center">
                                            <div class="col-lg-4">
                                                <label class="mb-1" id="lbl_102">City* </label>
                                            </div>
                                            <div class="col-lg-8">
                                                <input class="form-control" runat="server" id="txt_city" type="text" maxlength="50" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group mb-3">
                                        <div class="row ">
                                            <div class="col-lg-4">
                                                <label class="mb-1" id="lbl_103">Address* </label>
                                            </div>
                                            <div class="col-lg-8">
                                                <input class="form-control mb-3" runat="server" id="txt_address1" type="text" maxlength="50" />
                                                <input class="form-control" runat="server" id="txt_address2" type="text" maxlength="50" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group mb-3">
                                        <div class="row align-items-center">
                                            <div class="col-lg-4">
                                                <label class="mb-1" id="lbl_104">Postcode* </label>
                                            </div>
                                            <div class="col-lg-8">
                                                <input class="form-control" runat="server" id="txt_postcode" type="text" maxlength="5" autocomplete="off" onkeypress='return event.charCode >= 48 && event.charCode <= 57' />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group mb-3" style="margin-bottom: 5px !important;">
                                        <div class="row align-items-center">
                                            <div class="col-lg-4">
                                                <label class="mb-1" id="lbl_105">Password* </label>
                                            </div>
                                            <div class="col-lg-8">
                                                <div class="input-group">
                                                    <input class="form-control" runat="server" id="txt_password" autocomplete="off" required maxlength="20" oninput="validatePassword(event)" onkeypress="return validateKeyPress(event)" />
                                                    <div class="input-group-append">
                                                        <button type="button" id="togglePassword" class="btn-outline-secondary h-100 p-10" onclick="togglePasswordVisibility()">
                                                            <i class="fa fa-eye-slash icon1" aria-hidden="true"></i>
                                                            <!-- Closed Eye Icon -->
                                                        </button>
                                                    </div>
                                                </div>
                                                <label style="margin: 0px; font-size: 10px; color: red; font-weight: bold;" id="lbl_106">
                                                    * minimum 6 characters</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group mb-3">
                                        <div class="row align-items-center">
                                            <div class="col-lg-4">
                                                <label class="mb-1" id="lbl_107">Confirm Password* </label>
                                            </div>
                                            <div class="col-lg-8">
                                                <div class="input-group">
                                                    <input class="form-control" runat="server" id="txt_confirmpassword" required maxlength="20" oninput="validatePassword(event)" onkeypress="return validateKeyPress(event)" />
                                                    <div class="input-group-append">
                                                        <button type="button" id="toggleConfirmPassword" class="btn-outline-secondary h-100 p-10" onclick="toggleConfirmPasswordVisibility()">
                                                            <i class="fa fa-eye-slash icon2" aria-hidden="true"></i>
                                                            <!-- Closed Eye Icon -->
                                                        </button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row mt-5">
                                <div class="col-lg-5" id="col_package" runat="server">
                                <div class="form-group mb-3">
                                    <div class="row align-items-center">
                                        <div class="col-lg-4">
                                            <label for="toggleSwitch" class="mb-1" id="lbl_347">Shopper*</label>
                                        </div>
                                        <div class="col-lg-8">
                                            <input type="checkbox" id="btn_shopper" runat="server" class="checkbox" />
                                            <label for="ctl00_ContentPlaceHolder1_btn_shopper" class="toggle" onclick="Shopper_Checkbox()">
                                                <span class="label-text" id="lbl_348">Yes</span>
                                                <span class="label-text" id="lbl_349">No</span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                    <div class="form-group mb-3">
                                        <div class="row align-items-center" id="div_profit_center">
                                            <div class="col-lg-4">
                                                <label class="mb-1" id="lbl_115">Profit Center*</label>
                                            </div>
                                            <div class="col-lg-8">
                                                <select runat="server" id="ddl_package" class="form-control" onclick="updateTitle()">
                                                    <option value="" disabled="disabled" id="lbl_116">Select your package</option>
                                                    <option value="1" selected="selected" id="lbl_117">Business Owner (1,000BV)</option>
                                                    <option value="EO1" id="lbl_350">Entrepreneur Owner (3,000 BV)</option>
                                                    <option value="3" id="lbl_118">3 x Business Owner (3,000BV)</option>
                                                    <option value="EO5" id="lbl_351">1 x EO & 2 x BO (5,000BV)</option>
                                                    <option value="EO9" id="lbl_352">3 x EO (9,000BV)</option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </form>

                        <hr style="background-color:#efefef;" />

                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel runat="server" ID="chkall1" UpdateMode="Conditional">
                <ContentTemplate>
                    <%--<div class="container mb-5">--%>
                    <div class="container">
                        <asp:Repeater ID="rpt_policy" runat="server">
                            <ItemTemplate>
                                <div class="p-2 mb-1 mt-1" data-toggle="collapse" data-target="#mmrCollapse" aria-expanded="false">
                                    <h5 class="m-1"><i class="fa fa-chevron-circle-down"></i>
                                        <asp:Label ID="title_policy" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                                    </h5>
                                </div>
                                <div id="mmrCollapse" class="collapse show">
                                    <section class="card" style="font-family: system-ui,-apple-system,BlinkMacSystemFont,Helvetica Neue,Helvetica,sans-serif !important; height: 250px; overflow-y: auto;">
                                        <div class="col-inner" id="contentDiv" runat="server" style="padding: 10px;">
                                            <%# Eval("Content") %>
                                        </div>
                                    </section>
                                    <div class="row">
                                        <div class="col-12 text-right">
                                            <h7>Please check the box if you agree. &nbsp;</h7>
                                            <input type="checkbox" runat="server" id="chkPolicy" autopostback="true" />
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <div class="float-right mb-5">
                            <asp:Button ID="btn_Register" runat="server" OnClick="Register_Click" CssClass="btn" Style="background-color: #149474; color: white;" Text="Register" OnClientClick="ValidatePhoneNumber()" />
                        </div>
                    </div>
                    <div hidden>
                        <asp:HiddenField ID="divNameVisible" runat="server" Value="true" />
                    </div>

                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Register" EventName="click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>

        <script type="text/javascript" src="js/jquery-3.2.1.min.js"></script>

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

        <script type="text/javascript">

            $(document).ready(function () {

                btnNameClick();
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
                var page = 'Registration';
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
                                var element = $('#' + item.Label_Name);
                                if (element) {
                                    element.text(item.Language_Content);
                                }
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

            function validateNumericInput(input) {
                // Remove non-numeric characters
                input.value = input.value.replace(/[^0-9]/g, '');
            }

            function isValidEmail(email) {
                // Regular expression to validate email format
                var emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
                return emailRegex.test(email);
            }

            function ValidatePhoneNumber() {
                var phoneNumberField = document.getElementById('<%=txt_phonenumber.ClientID%>');
                var emailField = document.getElementById('<%=txt_email.ClientID%>');
                var txt_password = document.getElementById('<%=txt_password.ClientID%>');
                var txt_confirmpassword = document.getElementById('<%=txt_confirmpassword.ClientID%>');
                var phoneNumber = phoneNumberField.value.trim();

                if (phoneNumber === "") {
                    // If phone number field is empty, return false to prevent further action
                    return false;
                }
                else if (emailField.value.trim() === "") {
                    // If phone number field is empty, return false to prevent further action
                    return false;
                }
                else if (!isValidEmail(emailField.value.trim())) {
                    // If email is not in a valid format, return false to prevent further action
                    return false;
                }
                else if (txt_password.value.trim() === "") {
                    // If phone number field is empty, return false to prevent further action
                    return false;
                }
                else if (txt_confirmpassword.value.trim() === "") {
                    // If phone number field is empty, return false to prevent further action
                    return false;
                } else {
                    // If phone number field is not empty, show loading
                    $("#loadingOverlay").show();
                    //return true; // Allow button click to continue
                }
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

            function BinaryPlacementCheckbox() {
                var checkbox = document.getElementById("ContentPlaceHolder1_btn_placement");
                var div_downline = document.getElementById("div_downline");

                // Check the current state of the checkbox and toggle it
                if (checkbox.checked) {
                    checkbox.checked = false;
                    div_downline.style.display = 'block';
                } else {
                    checkbox.checked = true;
                    div_downline.style.display = 'none';
                }
            }

            function Shopper_Checkbox() {
                var checkbox = document.getElementById("btn_shopper");
                var div_profit_center = document.getElementById("div_profit_center");

                // Check the current state of the checkbox and toggle it
                if (checkbox.checked) {
                    checkbox.checked = false;
                    div_profit_center.style.display = 'flex';
                } else {
                    checkbox.checked = true;
                    div_profit_center.style.display = 'none';
                }
            }

            function toggleDocumentInput() {
                var ddl = document.getElementById('<%= ddl_type.ClientID %>');
                var txt_nric = document.getElementById('<%= txt_nric.ClientID %>');
                var txt_passport = document.getElementById('<%= txt_passport.ClientID %>');
                var divNric = document.getElementById('div_nric');
                var divPassport = document.getElementById('div_passport');

                if (ddl.value === 'NRIC') {
                    divNric.style.display = 'block';
                    divPassport.style.display = 'none';
                    txt_nric.value = "";
                    txt_passport.value = "";

                } else if (ddl.value === 'Passport') {
                    divNric.style.display = 'none';
                    divPassport.style.display = 'block';
                    txt_nric.value = "";
                    txt_passport.value = "";
                }
            }

            function toggleCountry() {
                var ddl_country = document.getElementById('<%= ddl_country.ClientID %>');
                var div_state = document.getElementById('div_state');

                if (ddl_country.value === 'MY') {
                    div_state.style.display = 'block';
                } else {
                    div_state.style.display = 'none';
                }
            }

            function btnNameClick() {
                var div_gender = document.getElementById('div_gender');
                var btnName = document.getElementById('<%= btn_name.ClientID %>');
                var btnCmp = document.getElementById('<%= btn_company.ClientID %>');
                var divName = document.getElementById('divName');
                var divNricpass = document.getElementById('div_nricpass');
                var ddl = document.getElementById('<%= ddl_type.ClientID %>');
                var divic = document.getElementById('div_nric');
                var divpass = document.getElementById('div_passport');
                var divCmpname = document.getElementById('div_cmpname');
                var divCmpno = document.getElementById('div_cmpno');
                var divNameVisible = document.getElementById('<%= divNameVisible.ClientID %>')

                btnName.addEventListener('click', function () {
                    divName.style.display = 'block';
                    divNameVisible.value = 'true';
                    divNricpass.style.display = 'block';
                    divic.style.display = 'block';
                    divpass.style.display = 'none';
                    ddl.value = 'NRIC';
                    divCmpname.style.display = 'none';
                    divCmpno.style.display = 'none';
                    btnName.style.borderColor = '#848383';
                    btnName.style.color = 'black';
                    btnName.style.fontWeight = '500';
                    btnCmp.style.borderColor = '#e9e8e8';
                    btnCmp.style.color = 'black';
                    btnCmp.style.fontWeight = 'normal';
                    div_gender.style.display = 'block';
                });

                btnCmp.addEventListener('click', function () {
                    divName.style.display = 'none';
                    divNameVisible.value = 'false';
                    divNricpass.style.display = 'none';
                    divic.style.display = 'none';
                    divpass.style.display = 'none';
                    divCmpname.style.display = 'block';
                    divCmpno.style.display = 'block';
                    btnCmp.style.borderColor = '#848383';
                    btnCmp.style.color = 'black';
                    btnCmp.style.fontWeight = '500';
                    btnName.style.borderColor = '#e9e8e8';
                    btnName.style.color = 'black';
                    btnName.style.fontWeight = 'normal';
                    div_gender.style.display = 'none';
                });
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
                var charCode = event.keyCode || event.which;

                // Allow: A-Z, a-z, 0-9, Backspace (8), Space (32), @ (64)
                if (
                    (charCode >= 65 && charCode <= 90) ||     // A-Z
                    (charCode >= 97 && charCode <= 122) ||    // a-z
                    (charCode >= 48 && charCode <= 57) ||     // 0-9
                    charCode === 8 ||                         // Backspace
                    charCode === 32 ||                        // Space
                    charCode === 64 ||                          // @
                    charCode === 47                           // /
                ) {
                    return true;
                } else {
                    return false;
                }
            }

            function togglePasswordVisibility() {
                var passwordInput = document.getElementById('<%= txt_password.ClientID %>');
                if (passwordInput.type === 'text') {
                    passwordInput.type = 'password';
                    $('.icon1').removeClass('fa-eye').addClass('fa-eye-slash');
                } else {
                    passwordInput.type = 'text';
                    $('.icon1').removeClass('fa-eye-slash').addClass('fa-eye');
                }
            }

            function toggleConfirmPasswordVisibility() {
                var confirmPasswordInput = document.getElementById('<%= txt_confirmpassword.ClientID %>');
                if (confirmPasswordInput.type === 'text') {
                    confirmPasswordInput.type = 'password';
                    $('.icon2').removeClass('fa-eye').addClass('fa-eye-slash');
                } else {
                    confirmPasswordInput.type = 'text';
                    $('.icon2').removeClass('fa-eye-slash').addClass('fa-eye');
                }
            }

            function sweetalert_success(message, messagetype) {
                $("#loadingOverlay").hide();
                swal({
                    html: true,
                    title: message,
                    type: "success"
                }, function () {
                    window.location.href = window.location.href;
                });
            }

            function sweetalert_warning(message, messagetype) {
                $("#loadingOverlay").hide();
                swal({
                    title: message,
                    icon: "warning",
                    button: "OK",
                });
            }
        </script>

    </form>
</body>
</html>
