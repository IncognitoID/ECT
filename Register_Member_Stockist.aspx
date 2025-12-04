<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Register_Member_Stockist.aspx.cs" Inherits="Register_Member_Stockist" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

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
                left: 19px; /* Adjust the left position for the "No" label */
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

        .btn_verify{
            font-size: 12px;
            padding: 10px 10px;
            line-height: 0.5 !important;
            background-color: #6565ed !important;
            color: white !important;
        }

        .btn_remove{
            font-size: 12px;
            padding: 10px 10px;
            line-height: 0.5 !important;
            background-color: #f74242 !important;
            color: white !important;
        }

        @media (max-width: 450px) {
            .div-memberid-margin {
                margin-bottom: 5px !important;
            }
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <div id="loadingOverlay" style="display: none;">
        <div class="spinner-border text-primary" role="status">
            <i class="fa fa-spinner fa-spin spinnercolor"></i>
        </div>
    </div>

    <div id="registration">
        <asp:UpdatePanel runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:HiddenField ID="hfCheck" runat="server" />
                <div class="container pt-4 pb-1 pl-4 pr-4">
                    <form>
                        <div class="row pb-3" style="border-bottom: 1px solid #efefef">
                            <div class="col-lg-5">
                                <div class="form-group mb-3">
                                    <div class="row align-items-center">
                                        <div class="col-lg-4">
                                            <label class="mb-1" for="referralID" id="lbl_119">Direct Referral ID* </label>
                                        </div>
                                        <div class="col-lg-8 d-flex">
                                            <input class="form-control" runat="server" id="txt_referal_id" type="text" required maxlength="10" />
                                            <asp:Button runat="server" Text="Verify" ID="btn_verify" OnClick="btn_verify_Click" CausesValidation="false" UseSubmitBehavior="false" CssClass="submit-button btn btn-primary btn-px-4 py-3 d-flex align-items-center font-weight-semibold line-height-1 btn_verify" />
                                            <asp:Button runat="server" Text="Remove" ID="btn_remove" OnClick="btn_remove_Click" CausesValidation="false" UseSubmitBehavior="false" CssClass="submit-button btn btn-danger btn-px-4 py-3 d-flex align-items-center font-weight-semibold line-height-1 btn_remove" />
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
                                            <asp:DropDownList runat="server" ID="ddl_nationality" CssClass="form-control">
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
                            <div class="col-lg-5">
                                <div class="form-group mb-3">
                                    <div class="row align-items-center">
                                        <div class="col-lg-4">
                                            <label for="toggleSwitch" class="mb-1" id="lbl_108">Binary Placement*</label>
                                            <br />
                                            <label class="mb-1" style="font-style: italic; margin: 0px; font-size: 10px; color: red; font-weight: bold;" id="lbl_109">
                                                **For reference usage</label>
                                        </div>
                                        <div class="col-lg-8">
                                            <input type="checkbox" id="btn_placement" runat="server" class="checkbox" />
                                            <label for="ctl00_ContentPlaceHolder1_btn_placement" class="toggle" onclick="BinaryPlacementCheckbox()">
                                                <span class="label-text" id="lbl_110">Yes</span>
                                                <span class="label-text" id="lbl_111">No</span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <div class="form-group mb-3" id="div_downline" style="display: none;">
                                            <div class="row align-items-center">
                                                <div class="col-lg-4">
                                                </div>
                                                <div class="col-lg-8">
                                                    <asp:DropDownList CssClass="form-control" runat="server" ID="ddl_member_placement" onchange="toggleDiv()">
                                                        <asp:ListItem Value="Left" id="lbl_112">Left</asp:ListItem>
                                                        <asp:ListItem Value="Right" id="lbl_113">Right</asp:ListItem>
                                                        <asp:ListItem Value="Always Left" id="lbl_334">Left toward end</asp:ListItem>
                                                        <asp:ListItem Value="Always Right" id="lbl_335">Right toward end</asp:ListItem>
                                                        <asp:ListItem Value="KeyinID" id="lbl_114">Key in ID</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <div class="w-100 mt-4" style="border: 1px solid #e8e8e8; border-radius: 5px; padding: 10px 10px; margin-top: 5px; display: none;"
                                                        runat="server" id="div_id">
                                                        <span class="w-25 m-auto">ID : </span>
                                                        <asp:TextBox runat="server" ID="txt_keyinid" CssClass="w-75" MaxLength="10" Placeholder="Key in downline ID"></asp:TextBox>
                                                    </div>
                                                    <div class="w-100" style="border: 1px solid #e8e8e8; border-radius: 5px; padding: 10px 10px; margin-top: 5px; flex-direction: column; display: none;" runat="server" id="div_placement">
                                                        <span class="w-100 m-auto" id="lbl_457">Placement Left or Right : </span>
                                                        <asp:DropDownList CssClass="form-control w-100" runat="server" ID="ddl_default_placement">
                                                            <asp:ListItem Value="Default" id="lbl_454">Default Member Placement</asp:ListItem>
                                                            <asp:ListItem Value="Left" id="lbl_455">Left</asp:ListItem>
                                                            <asp:ListItem Value="Right" id="lbl_456">Right</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <%--<asp:DropDownList CssClass="form-control" ID="ddl_downline" runat="server" onchange="GetDownlineDetails(this.value)"></asp:DropDownList>

                                            <div runat="server" id="div_selected_member" style="padding: 10px; border: 1px solid #efefef; border-radius: 5px; margin-top: 3rem;">
                                                <div>
                                                    <asp:Label runat="server" ID="lbl_selected_member" CssClass="f-600"></asp:Label>
                                                </div>
                                                <table>
                                                    <tr>
                                                        <td style="width:20%;"><span>Left </span></td>
                                                        <td style="width:10%;"><span> : </span></td>
                                                        <td>
                                                            <asp:Label runat="server" ID="lbl_selected_member_left"></asp:Label>
                                                            <asp:HiddenField runat="server" ID="hdn_left_id" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width:20%;"><span>Right</span></td>
                                                        <td style="width:10%;"><span> : </span></td>
                                                        <td>
                                                            <asp:Label runat="server" ID="lbl_selected_member_right"></asp:Label>
                                                            <asp:HiddenField runat="server" ID="hdn_right_id" />
                                                        </td>
                                                    </tr>
                                                    <tr runat="server" id="div_keyinid">
                                                        <td style="width:20%;"><span>Key In ID</span></td>
                                                        <td style="width:10%;"><span> : </span></td>
                                                        <td><asp:TextBox runat="server" ID="txt_placement_member_id"></asp:TextBox></td>
                                                    </tr>
                                                </table>
                                            </div>--%>
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
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <div class="form-group mb-3">
                                            <div class="row align-items-center" id="div_profit_center">
                                                <div class="col-lg-4">
                                                    <label class="mb-1" id="lbl_115">Profit Center*</label>
                                                </div>
                                                <div class="col-lg-8">
                                                    <select runat="server" id="ddl_package" class="form-control" onclick="updateTitle()">
                                                        <option value="" id="lbl_116" disabled>Select your package</option>
                                                        <option value="1" id="lbl_117" selected>Business Owner (1,000BV)</option>
                                                        <option value="EO1" id="lbl_350">Entrepreneur Owner (3,000 BV)</option>
                                                        <option value="3" id="lbl_118">3 x Business Owner (3,000BV)</option>
                                                        <option value="EO5" id="lbl_351">1 x EO & 2 x BO (5,000BV)</option>
                                                        <option value="EO9" id="lbl_352">3 x EO (9,000BV)</option>
                                                    </select>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </form>
                    <hr style="background-color: #efefef; opacity: .7;" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel runat="server" ID="chkall1" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="container p-4 mb-5">
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
                                        <h7 id="lbl_333">Please check the box if you agree. &nbsp;</h7>
                                        <input type="checkbox" runat="server" id="chkPolicy" autopostback="true" />
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <div class="text-right mt-3 mb-5">
                        <asp:Button ID="btn_Register" runat="server" OnClick="Register_Click" CssClass="btn" Style="background-color: #149474; color: white;" Text="Next" OnClientClick="ValidatePhoneNumber()" />
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

            var checkbox_placement = document.getElementById("ContentPlaceHolder1_btn_shopper");
            var div_profit_center = document.getElementById("div_profit_center");

            // Check the current state of the checkbox and toggle it
            if (checkbox_placement.checked) {
                checkbox_placement.checked = true;
                div_profit_center.style.display = 'none';
            } else {
                checkbox_placement.checked = false;
                div_profit_center.style.display = 'flex';
            }

            var ddl = document.getElementById('<%= ddl_member_placement.ClientID %>');
            var div = document.getElementById('<%= div_id.ClientID %>');
            var div_placement = document.getElementById('<%= div_placement.ClientID %>');

            if (ddl.value === 'KeyinID') {
                div.style.display = 'flex';
                div_placement.style.display = 'flex';
            } else {
                div.style.display = 'none';
                div_placement.style.display = 'none';
            }

            var ddl = document.getElementById('<%= ddl_type.ClientID %>');
            var divNric = document.getElementById('div_nric');
            var divPassport = document.getElementById('div_passport');

            if (ddl.value === 'NRIC') {
                divNric.style.display = 'block';
                divPassport.style.display = 'none';

            } else if (ddl.value === 'Passport') {
                divNric.style.display = 'none';
                divPassport.style.display = 'block';
            }
            toggleCountry();
        }

        function toggleDiv() {
            var ddl = document.getElementById('<%= ddl_member_placement.ClientID %>');
            var div = document.getElementById('<%= div_id.ClientID %>');
            var div_placement = document.getElementById('<%= div_placement.ClientID %>');

            if (ddl.value === 'KeyinID') {
                div.style.display = 'flex';
                div_placement.style.display = 'flex';
            } else {
                div.style.display = 'none';
                div_placement.style.display = 'none';
            }
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
            var referal_id = document.getElementById('<%=txt_referal_id.ClientID%>');
            var phoneNumberField = document.getElementById('<%=txt_phonenumber.ClientID%>');
            var emailField = document.getElementById('<%=txt_email.ClientID%>');
            var txt_password = document.getElementById('<%=txt_password.ClientID%>');
            var txt_confirmpassword = document.getElementById('<%=txt_confirmpassword.ClientID%>');
            var phoneNumber = phoneNumberField.value.trim();

            if (referal_id.value.trim() === "") {
                // If phone number field is empty, return false to prevent further action
                return false;
            }
            else if (phoneNumber === "") {
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
            $("#loadingOverlay").show();
        }

        $(document).ready(function () {
            btnNameClick();
        });

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
                url: "Register_Member_Stockist.aspx/GetDownlineDetails",
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
                div_profit_center.style.display = 'flex';
            } else {
                div_profit_center.style.display = 'none';
                checkbox.checked = true;
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
                window.location = "Home.aspx";
            });
        }

        function sweetalert_warning(message, messagetype) {
            $("#loadingOverlay").hide();
            swal({
                title: message,
                icon: "warning",
                button: "OK",
            });
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
            var div = document.getElementById('<%= div_id.ClientID %>');
            var div_placement = document.getElementById('<%= div_placement.ClientID %>');

            if (ddl.value === 'KeyinID') {
                div.style.display = 'flex';
                div_placement.style.display = 'flex';
            } else {
                div.style.display = 'none';
                div_placement.style.display = 'none';
            }
        }
    </script>

</asp:Content>

