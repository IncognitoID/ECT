<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Register_Member_Open_New_Account_Checkout.aspx.cs" Inherits="Register_Member_Open_New_Account_Checkout" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <!---Registration-->
    <style>
        body{
            background: #f2f1f1;
        }

        .container{
            border: 1px solid #efefef;
            border-radius: 10px;
            background-color: white;
        }

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
                right: 0px; /* Adjust the right position for the "Yes" label */
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

        .nice-select {
            height: 45px !important;
        }

        .dropdownlist {
            height: 35px !important;
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

        .div-memberid {
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

    <!---Registration-->

    <style>
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

        /*.table.productlist th, td {
            vertical-align: middle !important;
            text-align: center;
        }*/

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

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <div id="loadingOverlay_Main" style="display: none;">
        <div class="spinner-border text-primary" role="status">
            <i class="fa fa-spinner fa-spin spinnercolor"></i>
        </div>
    </div>

    <div id="registration">
        <asp:UpdatePanel runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:HiddenField ID="hfCheck" runat="server" />
                <div class="container mt-3 pt-4 pb-1 pl-4 pr-4">

                    <div class="row text-left mb-3">
                        <div class="col-12 d-inline-flex">
                            <p class="membership_title fw-200" id="lbl_331">Register New Member - </p>
                            &nbsp;<p class="membership_title fw-200" id="lbl_332">Summary</p>
                        </div>
                    </div>

                    <hr />

                    <form>
                        <div class="row pb-3">
                            <div class="col-12 text-right">
                                <asp:LinkButton runat="server" ID="btn_edit" Text="Edit" CssClass="btn btn_primary bg-black text-white" OnClick="btn_edit_Click"></asp:LinkButton>
                            </div>
                        </div>
                        <div class="row pb-3" style="border-bottom: 1px solid #efefef">
                            <div class="col-lg-5">
                                <div class="form-group mb-3">
                                    <div class="row align-items-center">
                                        <div class="col-lg-4">
                                            <label class="mb-1" for="referralID" id="lbl_119">Direct Referral ID* </label>
                                        </div>
                                        <div class="col-lg-8">
                                            <input class="form-control" runat="server" id="txt_referal_id" disabled="disabled" type="text" required maxlength="10" oninput="lettersandNumberOnly(this)" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group mb-3">
                                    <div class="row align-items-center">
                                        <div class="col-lg-4">
                                            <label class="mb-1" id="lbl_82">Direct Referral Name* </label>
                                        </div>
                                        <div class="col-lg-8">
                                            <input class="form-control" runat="server" id="txt_referal_name" type="text" disabled="disabled" />
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
                                            <input class="form-control" runat="server" id="txt_companyname" type="text" maxlength="50" onkeypress="return allowOnlySpecificCharacters(event)" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group mb-3" id="div_cmpno" style="display: none;">
                                    <div class="row align-items-center">
                                        <div class="col-lg-4">
                                            <label class="mb-1" id="lbl_89">Company No.* </label>
                                        </div>
                                        <div class="col-lg-8">
                                            <input class="form-control" runat="server" id="txt_companyno" type="text" maxlength="30" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group mb-3" id="div_nricpass">
                                    <div class="row align-items-center">
                                        <div class="col-lg-4">
                                            <label class="mb-1" id="lbl_90">NRIC/Passport* </label>
                                        </div>
                                        <div class="col-lg-8">
                                            <asp:DropDownList runat="server" ID="ddl_type" CssClass="form-control dropdownlist" onchange="toggleDocumentInput()">
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
                                            <select class="form-control dropdownlist" runat="server" id="ddl_gender">
                                                <option value="" disabled selected id="lbl_94">Select your gender</option>
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
                                            <asp:DropDownList runat="server" ID="ddl_nationality" CssClass="form-control dropdownlist">
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
                                            <asp:DropDownList runat="server" ID="ddl_country" CssClass="form-control dropdownlist" onchange="toggleCountry()">
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
                                            <asp:DropDownList runat="server" ID="ddl_Member_State" CssClass="form-control dropdownlist">
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
                                            <input class="form-control" runat="server" id="txt_registration_city" type="text" maxlength="50" />
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
                                            <input class="form-control" runat="server" id="txt_registration_postocde" type="text" maxlength="5" autocomplete="off" onkeypress='return event.charCode >= 48 && event.charCode <= 57' />
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
                        <div class="row mt-4">
                            <div class="col-lg-5">
                                <div class="form-group mb-0">
                                    <div class="row align-items-center">
                                        <div class="col-lg-4">
                                            <label for="toggleSwitch" class="mb-1" id="lbl_108">Binary Placement*</label>
                                            <br />
                                            <label class="mb-1" style="font-style: italic; margin: 0px; font-size: 10px; color: red; font-weight: bold;" id="lbl_109">
                                                **For reference usage</label>
                                        </div>
                                        <div class="col-lg-8">
                                            <input type="checkbox" id="btn_placement" runat="server" class="checkbox" />
                                            <%--<label for="ctl00_ContentPlaceHolder1_btn_placement" class="toggle" onclick="BinaryPlacementCheckbox()">--%>
                                            <label for="ctl00_ContentPlaceHolder1_btn_placement" class="toggle" id="btn_placement_button">
                                                <span class="label-text" id="lbl_110">Yes</span>
                                                <span class="label-text" id="lbl_111">No</span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <div class="form-group mb-3" id="div_downline">
                                            <div class="row align-items-center">
                                                <div class="col-lg-4">
                                                </div>
                                                <div class="col-lg-8">
                                                    <asp:DropDownList CssClass="form-control dropdownlist" runat="server" ID="ddl_member_placement" onchange="toggleDiv()">
                                                        <asp:ListItem Value="Left" id="lbl_112">Left</asp:ListItem>
                                                        <asp:ListItem Value="Right" id="lbl_113">Right</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddl_member_placement" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-lg-1"></div>
                            <div class="col-lg-5" id="col_package" runat="server">
                                <div class="form-group mb-3">
                                    <div class="row align-items-center">
                                        <div class="col-lg-4">
                                            <label class="mb-1" id="lbl_115">Profit Center*</label>
                                        </div>
                                        <div class="col-lg-8">
                                            <select runat="server" id="ddl_package" disabled="disabled" class="form-control dropdownlist" onclick="updateTitle()">
                                                <option value="" id="lbl_116" disabled>Select your package</option>
                                                <option value="1" id="lbl_117">1 Profit Center (1000BV)</option>
                                                <option value="3" id="lbl_118">3 Profit Center (3000BV)</option>
                                            </select>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </form>
                    <hr style="background-color: #efefef; opacity: .7;" />
                    <asp:HiddenField ID="divNameVisible" runat="server" Value="true" />
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_edit" EventName="click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>

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
                                <div class="col-3">
                                    <input type="text" runat="server" id="txt_postcode" class="w-100 new_address_textbox" placeholder="Postcode" required onkeypress='return event.charCode >= 48 && event.charCode <= 57' onkeydown="handleEnterKeyPress(event, 'txt_city')" />
                                </div>
                                <div class="col-3">
                                    <input type="text" runat="server" id="txt_city" class="w-100 new_address_textbox" placeholder="City" required onkeydown="handleEnterKeyPress(event, '<%= ddl_state.ClientID %>')" />
                                </div>
                                <div class="col-3">
                                    <asp:DropDownList runat="server" ID="ddl_state" CssClass="w-100 new_address_textbox"></asp:DropDownList>
                                </div>
                                <div class="col-3">
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

        function Member_Type(type) {
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

            if (type === 'Personal') {
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
            } else {
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
            }

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

            var ddl = document.getElementById('<%= ddl_member_placement.ClientID %>');

        }

        function toggleDiv() {
            var ddl = document.getElementById('<%= ddl_member_placement.ClientID %>');
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
            var divNameVisible = document.getElementById('<%= divNameVisible.ClientID %>');

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

            var ddl = document.getElementById('<%= ddl_member_placement.ClientID %>');

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

            var ddl = document.getElementById('<%= ddl_member_placement.ClientID %>');
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

