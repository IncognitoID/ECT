<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Register_Member_Old.aspx.cs" Inherits="Register_Member_Old" EnableEventValidation="false" %>

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
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div id="registration">
        <asp:UpdatePanel runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:HiddenField ID="hfCheck" runat="server" />
                <div class="container p-4">
                    <form>
                        <div class="row">
                            <div class="col-lg-6">
                                <div class="form-group mb-3">
                                    <div class="row align-items-center">
                                        <div class="col-lg-4">
                                            <label class="mb-1" for="referralID">Direct Referral ID* </label>
                                        </div>
                                        <div class="col-lg-8">
                                            <input class="form-control" runat="server" id="txt_referal_id" type="text" required maxlength="10" oninput="lettersandNumberOnly(this)" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-6">
                                <div class="form-group mb-3">
                                    <div class="row align-items-center">
                                        <div class="col-lg-4">
                                            <label class="mb-1">Direct Referral Name* </label>
                                        </div>
                                        <div class="col-lg-8">
                                            <input class="form-control" runat="server" id="txt_referal_name" type="text" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="border-bottom: 1px solid">
                            <div class="col-lg-6">
                                <div class="form-group mb-3">
                                    <div class="row align-items-center">
                                        <div class="col-lg-4">
                                            <label class="mb-1">Nationality* </label>
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
                                    <div class="row">
                                        <div class="col-lg-4"></div>
                                        <div class="col-lg-8">
                                            <button runat="server" id="btn_name" class="btn-white btn-oval inline-block mr-2" onclick="btnNameClick()" style="color: black; font-weight:500; background-color: white; padding: 10px 15px; border: 1px solid #848383;">
                                                Full Name</button>
                                            <button runat="server" id="btn_company" class="btn-white btn-oval inline-block" onclick="btnNameClick()" style="color: black; background-color: white; padding: 10px 15px; border: 1px solid #e9e8e8;">Company</button>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group mb-3" id="divName">
                                    <div class="form-group mb-3">
                                        <div class="row align-items-center">
                                            <div class="col-lg-4">
                                                <label class="mb-1">Full Name Per NRIC* </label>
                                            </div>
                                            <div class="col-lg-8">
                                                <input class="form-control" runat="server" id="txt_fullname" type="text" maxlength="100" onkeypress="return lettersOnly(event)" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group mb-3" id="div_cmpname" style="display: none;">
                                    <div class="row align-items-center">
                                        <div class="col-lg-4">
                                            <label class="mb-1">Company Name* </label>
                                        </div>
                                        <div class="col-lg-8">
                                            <input class="form-control" runat="server" id="txt_companyname" type="text" maxlength="100" onkeypress="return lettersOnly(event)" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group mb-3" id="div_cmpno" style="display: none;">
                                    <div class="row align-items-center">
                                        <div class="col-lg-4">
                                            <label class="mb-1">Company No.* </label>
                                        </div>
                                        <div class="col-lg-8">
                                            <input class="form-control" runat="server" id="txt_companyno" type="text" maxlength="15" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group mb-3" id="div_nricpass">
                                    <div class="row align-items-center">
                                        <div class="col-lg-4">
                                            <label class="mb-1">NRIC/Passport* </label>
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
                                            <label class="label" for="name">NRIC *</label>
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
                                            <label class="label" for="name">Passport *</label>
                                        </div>
                                        <div class="col-lg-8">
                                            <asp:TextBox runat="server" ID="txt_passport" CssClass="form-control" MaxLength="30"></asp:TextBox>
                                            <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator4" ControlToValidate="txt_passport" ValidationExpression="^(?!^0+$)[a-zA-Z0-9]{3,20}$" ErrorMessage="Invalid format. Please enter a valid Passport number." Display="Dynamic" ForeColor="Red" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group mb-3">
                                    <div class="row align-items-center">
                                        <div class="col-lg-4">
                                            <label class="mb-1">Date Of Birth* </label>
                                        </div>
                                        <div class="col-lg-8">
                                            <input class="form-control" runat="server" type="date" id="txt_dob" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group mb-3">
                                    <div class="row align-items-center">
                                        <div class="col-lg-4">
                                            <label class="mb-1">Gender* </label>
                                        </div>
                                        <div class="col-lg-8">
                                            <select class="form-control" runat="server" id="ddl_gender">
                                                <option value="" disabled selected>Select your gender</option>
                                                <option value="Male">Male</option>
                                                <option value="Female">Female</option>
                                            </select>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group mb-3">
                                    <div class="row align-items-center">
                                        <div class="col-lg-4">
                                            <label class="mb-1">Mobile No.* </label>
                                        </div>
                                        <div class="col-lg-8">
                                            <input class="form-control" runat="server" id="txt_phonenumber" type="text" required maxlength="11" onkeypress='return event.charCode >= 48 && event.charCode <= 57' />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group mb-3">
                                    <div class="row align-items-center">
                                        <div class="col-lg-4">
                                            <label class="mb-1">Email* </label>
                                        </div>
                                        <div class="col-lg-8">
                                            <input runat="server" id="txt_email" type="email" class="form-control" required maxlength="50">
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group mb-3">
                                    <div class="row align-items-center">
                                        <div class="col-lg-4">
                                            <label for="toggleSwitch" class="mb-1">Married</label>
                                        </div>
                                        <div class="col-lg-8">
                                            <input type="checkbox" id="btn_married" runat="server" class="checkbox" />
                                            <label for="ctl00_ContentPlaceHolder1_btn_married" class="toggle" onclick="MarryCheckbox()">
                                                <span class="label-text">Yes</span>
                                                <span class="label-text">No</span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-6">
                                <div class="form-group mb-3">
                                    <div class="row align-items-center">
                                        <div class="col-lg-4">
                                            <label class="mb-1">Country* </label>
                                        </div>
                                        <div class="col-lg-8">
                                            <asp:DropDownList runat="server" ID="ddl_country" CssClass="form-control">
                                                <asp:ListItem Value="MY" Selected="True">Malaysia</asp:ListItem>
                                            </asp:DropDownList>
                                            <input class="form-control hidden" runat="server" id="txt_country" type="text" maxlength="50" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group mb-3">
                                    <div class="row align-items-center">
                                        <div class="col-lg-4">
                                            <label class="mb-1">State* </label>
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
                                            <label class="mb-1">City* </label>
                                        </div>
                                        <div class="col-lg-8">
                                            <input class="form-control" runat="server" id="txt_city" type="text" maxlength="50" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group mb-3">
                                    <div class="row ">
                                        <div class="col-lg-4">
                                            <label class="mb-1">Address* </label>
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
                                            <label class="mb-1">Postcode* </label>
                                        </div>
                                        <div class="col-lg-8">
                                            <input class="form-control" runat="server" id="txt_postcode" type="text" maxlength="5" autocomplete="off" onkeypress='return event.charCode >= 48 && event.charCode <= 57' />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group mb-3" style="margin-bottom: 5px !important;">
                                    <div class="row align-items-center">
                                        <div class="col-lg-4">
                                            <label class="mb-1">Password* </label>
                                        </div>
                                        <div class="col-lg-8">
                                            <div class="input-group">
                                                <input class="form-control" runat="server" id="txt_password" type="password" autocomplete="off" required maxlength="20" />
                                                <div class="input-group-append">
                                                    <button type="button" id="togglePassword" class="btn-outline-secondary h-100 p-10" onclick="togglePasswordVisibility()">
                                                        <i class="fa fa-eye-slash icon1" aria-hidden="true"></i>
                                                        <!-- Closed Eye Icon -->
                                                    </button>
                                                </div>
                                            </div>
                                            <label style="margin: 0px; font-size: 10px; color: red; font-weight: bold;">
                                                * minimum 6 characters</label>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group mb-3">
                                    <div class="row align-items-center">
                                        <div class="col-lg-4">
                                            <label class="mb-1">Confirm Password* </label>
                                        </div>
                                        <div class="col-lg-8">
                                            <div class="input-group">
                                                <input class="form-control" runat="server" id="txt_confirmpassword" type="password" required maxlength="20" />
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
                            <div class="col-lg-6">
                                <div class="form-group mb-3">
                                    <div class="row align-items-center">
                                        <div class="col-lg-4">
                                            <label for="toggleSwitch" class="mb-1">Binary Placement*</label>
                                        </div>
                                        <div class="col-lg-8">
                                            <input type="checkbox" id="btn_placement" runat="server" class="checkbox" />
                                            <label for="ctl00_ContentPlaceHolder1_btn_placement" class="toggle" onclick="BinaryPlacementCheckbox()">
                                                <span class="label-text">Yes</span>
                                                <span class="label-text">No</span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group mb-3">
                                    <div class="row align-items-center">
                                        <div class="col-lg-4">
                                            <label class="mb-1" style="font-style: italic">**For reference usage</label>
                                        </div>
                                        <div class="col-lg-8">
                                            <asp:DropDownList CssClass="form-control" ID="ddl_ref" runat="server">
                                                <asp:ListItem Text="Left">Left</asp:ListItem>
                                                <asp:ListItem Text="Right">Right</asp:ListItem>
                                                <asp:ListItem Text="Blank">Blank</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-6" id="col_package" runat="server">
                                <div class="form-group mb-3">
                                    <div class="row align-items-center">
                                        <div class="col-lg-4">
                                            <label class="mb-1">Profit Center*</label>
                                        </div>
                                        <div class="col-lg-8">
                                            <select runat="server" id="ddl_package" class="form-control" onclick="updateTitle()">
                                                <option value="" disabled>Select your package</option>
                                                <option value="Package 1000BV" selected>1 Profit Center (1000BV)</option>
                                                <option value="Package 3000BV">3 Profit Center (3000BV)</option>
                                            </select>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel runat="server" ID="chkall1" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="container p-4 mb-5">
                    <div class="row">
                        <div class="col-12 text-right p-2" style="background-color: #eaeaea">
                            <h7>All agree &nbsp;</h7>
                            <asp:CheckBox ID="chkall" runat="server" OnCheckedChanged="chkall_CheckedChanged" AutoPostBack="true" />
                            <%--<asp:Checkbox id="chkall" runat="server" OnCheckedChanged="chkall_CheckedChanged" AutoPostBack="true" ClientIDMode="AutoID" />--%>
                        </div>
                    </div>
                    <asp:Repeater ID="rpt_policy" runat="server">
                        <ItemTemplate>
                            <div class="p-2 mb-1 mt-4" data-toggle="collapse" data-target="#mmrCollapse" aria-expanded="false">
                                <h5 class="m-1"><i class="fa fa-chevron-circle-down"></i>
                                    <asp:Label ID="title_policy" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                                </h5>
                            </div>
                            <div id="mmrCollapse" class="collapse show">
                                <section class="card" style="font-family: system-ui,-apple-system,BlinkMacSystemFont,Helvetica Neue,Helvetica,sans-serif !important; height: 250px; overflow-y: auto;">
                                    <div class="col-inner" id="contentDiv" runat="server" style="padding: 10px;">
                                        <%# Eval("Description") %>
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
                    <div class="float-right mt-3">
                        <asp:Button ID="Next" runat="server" OnClick="Next_Click" CssClass="btn" Style="background-color: #149474; color: white;" Text="Next" />
                    </div>
                </div>
                <div hidden>
                    <asp:Button ID="selectAll" runat="server" OnClick="selectAll_Click" />
                    <asp:HiddenField ID="divNameVisible" runat="server" Value="true" />
                </div>

            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="selectAll" EventName="click" />
                <asp:AsyncPostBackTrigger ControlID="chkall" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="Next" EventName="click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>

    <div id="package" style="display: none;">
        <div class="container p-4 mb-4">
            <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="header-nav-menu">
                        <header>
                            <div class="left">
                                <div class="nav-back" style="margin-top: 5px; line-height: 36px; display: flex; flex-direction: row;">
                                    <h6>
                                        <a runat="server" class="text-muted" id="profile" style="text-decoration: none;">Register New Member - Package -</a>
                                        <a class="text-header" id="txt_title" runat="server" style="text-decoration: none;"></a>
                                    </h6>
                                </div>
                            </div>
                        </header>
                    </div>
                    <hr class="mt-0 mb-2" />

                    <asp:HiddenField ID="hfQuantity" runat="server" />
                    <div class="d-flex justify-content-end mb-4">
                        <label class="mr-4">
                            <input type="radio" runat="server" name="package" disabled checked /><label id="package1" class="ml-1" runat="server"></label>
                        </label>
                        <label>
                            <input type="radio" id="alacarte" runat="server" name="alacarte" style="cursor: pointer;" data-toggle="modal" data-target="#alacarteModal" />
                            A la Carte
                        </label>
                    </div>

                    <div class="text-center">
                        <div class="mx-auto">
                            <h4 id="title" runat="server" style="color: white; border: 3px solid #149474; background-color: #149474;"></h4>
                        </div>
                    </div>
                    <div class="modal fade" id="alacarteModal" tabindex="-1" role="dialog" aria-hidden="true">
                        <div class="modal-dialog" style="max-width: 900px !important" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <div class="left">
                                        <div class="nav-back" style="margin-top: 5px; line-height: 36px; display: flex; flex-direction: row;">
                                            <h6>
                                                <a runat="server" class="text-muted" id="A2" style="text-decoration: none;">Register New Member - Package -</a>
                                                <a class="text-header" style="text-decoration: none;">A La Carte</a>
                                            </h6>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <asp:Repeater ID="rpt_alacarte" runat="server">
                                            <ItemTemplate>
                                                <div class="col-md-4">
                                                    <div class="card">
                                                        <img src="images/HomePage/promotion2.jpg" />
                                                        <div class="card-body">
                                                            <h5 class="card-title"><%# Eval("Product_Name") %></h5>
                                                            <p class="card-text mb-2">20 Packs</p>
                                                            <div class="mb-2">
                                                                <p class="card-text">Member: <b>RM <%# Eval("Product_Price") %></b></p>
                                                                <p class="card-text">BV: <b>100</b></p>
                                                            </div>
                                                            <div style="display: flex; align-items: center">
                                                                <button type="button" id="btnplus" onclick="increaseQuantity(this)" style="background-color: white; color: black; width: 30px; height: 30px; margin: 0; padding: 0; border: 1px solid #000000;">+</button>
                                                                <asp:TextBox ID="txtQuantity" Text="0" CssClass="text-center" runat="server" Style="background-color: white; color: black; width: 30px; height: 30px; margin: 0; padding: 0; border: 1px solid #000000;" ReadOnly="true"></asp:TextBox>
                                                                <button type="button" id="btnminus" onclick="decreaseQuantity(this)" style="background-color: white; color: black; width: 30px; height: 30px; margin: 0; padding: 0; border: 1px solid #000000;">-</button>
                                                                <button type="button" id="btn_cart" style="color: white; background-color: #149474; border: 1px solid #149474; margin-left: 10px; width: 55px; height: 30px;" data-toggle="modal" data-target="#productModal<%# Eval("Product_Code") %>"><i class="fa fa-cart-plus"></i></button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <div class="float-right">
                                        <button id="btn_confirm" class="close" data-dismiss="modal" aria-label="Close">Confirm</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="row mt-5">
                        <asp:Repeater ID="rpt_product" runat="server" OnItemDataBound="rpt_product_ItemDataBound">
                            <ItemTemplate>
                                <asp:HiddenField ID="hfProductCode" runat="server" Value='<%# Eval("Product_Code") %>' />
                                <div class="col-lg-4 mb-4">
                                    <div class="card">
                                        <img src="images/HomePage/promotion1.jpg" class="card-img-top" style="cursor: pointer;" data-toggle="modal" data-target="#productModal<%# Eval("Product_Code") %>" />
                                        <div class="modal fade" id="productModal<%# Eval("Product_Code") %>" tabindex="-1" role="dialog" aria-labelledby="productModalLabel" aria-hidden="true">
                                            <div class="modal-dialog" role="document">
                                                <div class="modal-content">
                                                    <div class="modal-header">
                                                        <h5 class="modal-title" id="productModalLabel">Product Details</h5>
                                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                            <span aria-hidden="true">&times;</span>
                                                        </button>
                                                    </div>
                                                    <div class="modal-body">
                                                        <!-- Add your product details content here -->
                                                        <h4><%# Eval("Product_Name") %></h4>
                                                        <p>
                                                            Price: RM
                                                            <asp:Label ID="price" runat="server" Text='<%# Eval("Product_Price") %>'></asp:Label>
                                                        </p>
                                                        <p>Weight: <%# Eval("Product_Weight") %></p>
                                                        <!-- Add more product details as needed -->
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="card-body">
                                            <h5 class="card-title"><%# Eval("Product_Name") %></h5>
                                            <p class="card-text">Price: RM<%# Eval("Product_Price") %></p>
                                            <div>
                                                Quantity:
                                                <button type="button" id="btnplus" onclick="increaseQuantity(this)" style="color: white; background-color: #149474; border: 1px solid #149474">+</button>
                                                <asp:TextBox ID="txtQuantity" Text="0" CssClass="text-center" runat="server" Style="width: 50px;" ReadOnly="true"></asp:TextBox>
                                                <button type="button" id="btnminus" onclick="decreaseQuantity(this)" style="color: white; background-color: #149474; border: 1px solid #149474">-</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <div class="float-right">
                        <asp:LinkButton runat="server" ID="btn_back" OnClick="btn_back_Click" Text="Back" CssClass="btn" Style="background-color: black; border: black; color: white; width: 90px;"></asp:LinkButton>
                        <asp:LinkButton runat="server" ID="btn_continue" OnClick="btn_continue_Click" Text="Continue" CssClass="btn" Style="background-color: black; border: black; color: white; width: 90px;" />
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_continue" EventName="click" />
                </Triggers>
            </asp:UpdatePanel>

        </div>
    </div>

    <div id="summary" style="display: none;">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="pswd" runat="server" />
                <div class="container p-4">
                    <div class="header-nav-menu">
                        <header>
                            <div class="left">
                                <div class="nav-back" style="margin-top: 5px; line-height: 36px; display: flex; flex-direction: row;">
                                    <h6>
                                        <a runat="server" class="text-muted" id="A1" style="text-decoration: none;">Register New Member -</a>
                                        <a class="text-header" style="text-decoration: none;">Summary</a>
                                    </h6>
                                </div>
                            </div>
                        </header>
                    </div>
                    <hr class="mt-0 mb-2" />

                    <div class="d-flex justify-content-end mb-4">
                        <asp:LinkButton ID="btn_edit" runat="server" CssClass="btn" Text="Edit" OnClick="btn_edit_Click" Style="color: white; background-color: black;"></asp:LinkButton>
                    </div>

                    <form>
                        <div class="row">
                            <div class="col-lg-6">
                                <div class="form-group mb-3">
                                    <div class="row align-items-center">
                                        <div class="col-lg-4">
                                            <label class="mb-1" for="referralID">Direct Referral ID* </label>
                                        </div>
                                        <div class="col-lg-8">
                                            <input class="form-control" runat="server" id="txt_referal_id1" type="text" required maxlength="10" oninput="lettersandNumberOnly(this)" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-6">
                                <div class="form-group mb-3">
                                    <div class="row align-items-center">
                                        <div class="col-lg-4">
                                            <label class="mb-1">Direct Referral Name* </label>
                                        </div>
                                        <div class="col-lg-8">
                                            <input class="form-control" runat="server" id="txt_referal_name1" type="text" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row borderBottom">
                            <div class="col-lg-6">
                                <div class="form-group mb-3">
                                    <div class="row align-items-center">
                                        <div class="col-lg-4">
                                            <label class="mb-1">Nationality* </label>
                                        </div>
                                        <div class="col-lg-8">
                                            <asp:DropDownList runat="server" ID="ddl_nationality1" CssClass="form-control">
                                                <asp:ListItem Text="Select your nationality" Value="default" Disabled="true" Selected="true"></asp:ListItem>
                                                <asp:ListItem Text="Malay" Value="Malay"></asp:ListItem>
                                                <asp:ListItem Text="Chinese" Value="Chinese"></asp:ListItem>
                                                <asp:ListItem Text="Indian" Value="Indian"></asp:ListItem>
                                                <asp:ListItem Text="Others" Value="Others"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group mb-3">
                                    <div class="row">
                                        <div class="col-lg-4"></div>
                                        <div class="col-lg-8">
                                            <button runat="server" id="btn_name1" class="btn btn-white btn-oval inline-block mr-2" onclick="btnCmpClick()" style="border-color: black; color: black; font-weight: bold;">Full Name</button>
                                            <button runat="server" id="btn_company1" class="btn btn-white btn-oval inline-block" onclick="btnCmpClick()" style="border-color: #777777;">Company</button>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group mb-3" id="divName1" runat="server">
                                    <div class="form-group mb-3">
                                        <div class="row align-items-center">
                                            <div class="col-lg-4">
                                                <label class="mb-1">Full Name Per NRIC* </label>
                                            </div>
                                            <div class="col-lg-8">
                                                <input class="form-control" runat="server" id="txt_fullname1" type="text" maxlength="100" onkeypress="return lettersOnly(event)" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group mb-3" id="div_cmpname1" runat="server" style="display: none;">
                                    <div class="row align-items-center">
                                        <div class="col-lg-4">
                                            <label class="mb-1">Company Name* </label>
                                        </div>
                                        <div class="col-lg-8">
                                            <input class="form-control" runat="server" id="txt_companyname1" type="text" maxlength="100" onkeypress="return lettersOnly(event)" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group mb-3" id="div_cmpno1" runat="server" style="display: none;">
                                    <div class="row align-items-center">
                                        <div class="col-lg-4">
                                            <label class="mb-1">Company No.* </label>
                                        </div>
                                        <div class="col-lg-8">
                                            <input class="form-control" runat="server" id="txt_companyno1" type="text" maxlength="15" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group mb-3" runat="server" id="div_nricpass1">
                                    <div class="row align-items-center">
                                        <div class="col-lg-4">
                                            <label class="mb-1">NRIC/Passport* </label>
                                        </div>
                                        <div class="col-lg-8">
                                            <asp:DropDownList runat="server" ID="ddl_type1" CssClass="form-control" onchange="toggleDocumentInput()">
                                                <asp:ListItem Text="NRIC">NRIC</asp:ListItem>
                                                <asp:ListItem Text="Passport">Passport</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group mb-3" id="div_nric1" runat="server">
                                    <div class="row align-items-center">
                                        <div class="col-lg-4">
                                            <label class="label" for="name">NRIC *</label>
                                        </div>
                                        <div class="col-lg-8">
                                            <asp:TextBox runat="server" ID="txt_nric1" CssClass="form-control" MaxLength="12" onkeypress='return event.charCode >= 48 && event.charCode <= 57'></asp:TextBox>
                                            <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" ControlToValidate="txt_nric1" ValidationExpression="^\d{6}\d{2}\d{4}$" ErrorMessage="Invalid format. Please enter a valid NRIC number." Display="Dynamic" ForeColor="Red" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group mb-3" id="div_passport1" style="display: none;">
                                    <div class="row align-items-center">
                                        <div class="col-lg-4">
                                            <label class="label" for="name">Passport *</label>
                                        </div>
                                        <div class="col-lg-8">
                                            <asp:TextBox runat="server" ID="txt_passport1" CssClass="form-control" MaxLength="30"></asp:TextBox>
                                            <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator3" ControlToValidate="txt_passport1" ValidationExpression="^(?!^0+$)[a-zA-Z0-9]{3,20}$" ErrorMessage="Invalid format. Please enter a valid Passport number." Display="Dynamic" ForeColor="Red" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group mb-3">
                                    <div class="row align-items-center">
                                        <div class="col-lg-4">
                                            <label class="mb-1">Date Of Birth* </label>
                                        </div>
                                        <div class="col-lg-8">
                                            <input class="form-control" runat="server" type="date" id="txt_dob1" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group mb-3">
                                    <div class="row align-items-center">
                                        <div class="col-lg-4">
                                            <label class="mb-1">Gender* </label>
                                        </div>
                                        <div class="col-lg-8">
                                            <select class="form-control" runat="server" id="ddl_gender1">
                                                <option value="" disabled selected>Select your gender</option>
                                                <option value="Male">Male</option>
                                                <option value="Female">Female</option>
                                            </select>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group mb-3">
                                    <div class="row align-items-center">
                                        <div class="col-lg-4">
                                            <label class="mb-1">Mobile No.* </label>
                                        </div>
                                        <div class="col-lg-8">
                                            <input class="form-control" runat="server" id="txt_phonenumber1" type="text" required maxlength="15" onkeypress='return event.charCode >= 48 && event.charCode <= 57' />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group mb-3">
                                    <div class="row align-items-center">
                                        <div class="col-lg-4">
                                            <label class="mb-1">Email* </label>
                                        </div>
                                        <div class="col-lg-8">
                                            <input runat="server" id="txt_email1" type="email" class="form-control" required maxlength="50">
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group mb-3">
                                    <div class="row align-items-center">
                                        <div class="col-lg-4">
                                            <label for="toggleSwitch" class="mb-1">Married</label>
                                        </div>
                                        <div class="col-lg-8">
                                            <input type="checkbox" id="btn_married1" runat="server" class="checkbox" />
                                            <label for="ctl00_ContentPlaceHolder1_btn_married1" class="toggle">
                                                <span class="label-text">Yes</span>
                                                <span class="label-text">No</span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-6">
                                <div class="form-group mb-3">
                                    <div class="row align-items-center">
                                        <div class="col-lg-4">
                                            <label class="mb-1">Country* </label>
                                        </div>
                                        <div class="col-lg-8">
                                            <input class="form-control" runat="server" id="txt_country1" type="text" maxlength="50" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group mb-3">
                                    <div class="row align-items-center">
                                        <div class="col-lg-4">
                                            <label class="mb-1">State* </label>
                                        </div>
                                        <div class="col-lg-8">
                                            <input class="form-control" runat="server" id="txt_state1" type="text" maxlength="50" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group mb-3">
                                    <div class="row align-items-center">
                                        <div class="col-lg-4">
                                            <label class="mb-1">City* </label>
                                        </div>
                                        <div class="col-lg-8">
                                            <input class="form-control" runat="server" id="txt_city1" type="text" maxlength="50" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group mb-3">
                                    <div class="row ">
                                        <div class="col-lg-4">
                                            <label class="mb-1">Address* </label>
                                        </div>
                                        <div class="col-lg-8">
                                            <input class="form-control mb-3" runat="server" id="txtAddress1" type="text" maxlength="50" />
                                            <input class="form-control" runat="server" id="txtAddress2" type="text" maxlength="50" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group mb-3">
                                    <div class="row align-items-center">
                                        <div class="col-lg-4">
                                            <label class="mb-1">Postcode* </label>
                                        </div>
                                        <div class="col-lg-8">
                                            <input class="form-control" runat="server" id="txt_postcode1" type="text" maxlength="5" onkeypress='return event.charCode >= 48 && event.charCode <= 57' />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group mb-3">
                                    <div class="row align-items-center">
                                        <div class="col-lg-4">
                                            <label class="mb-1">Password* </label>
                                        </div>
                                        <div class="col-lg-8">
                                            <div class="input-group">
                                                <input class="form-control" runat="server" id="txt_password1" type="text" required maxlength="20" />
                                                <div class="input-group-append">
                                                    <button type="button" id="togglePassword1" class="btn btn-outline-secondary h-100" onclick="togglePasswordVisibility()">
                                                        <i class="fa fa-eye icon1" aria-hidden="true"></i>
                                                        <!-- Closed Eye Icon -->
                                                    </button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group mb-3">
                                    <div class="row align-items-center">
                                        <div class="col-lg-4">
                                            <label class="mb-1">Confirm Password* </label>
                                        </div>
                                        <div class="col-lg-8">
                                            <div class="input-group">
                                                <input class="form-control" runat="server" id="txt_confirmpassword1" type="text" required maxlength="20" />
                                                <div class="input-group-append">
                                                    <button type="button" id="toggleConfirmPassword1" class="btn btn-outline-secondary h-100" onclick="toggleConfirmPasswordVisibility()">
                                                        <i class="fa fa-eye icon2" aria-hidden="true"></i>
                                                        <!-- Closed Eye Icon -->
                                                    </button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row borderBottom mt-2">
                            <div class="col-lg-6">
                                <div class="form-group mb-3">
                                    <div class="row align-items-center">
                                        <div class="col-lg-4">
                                            <label class="mb-1">Binary Placement*</label>
                                        </div>
                                        <div class="col-lg-8">
                                            <input type="checkbox" id="btn_placement1" runat="server" class="checkbox" />
                                            <label for="ctl00_ContentPlaceHolder1_btn_placement1" class="toggle">
                                                <span class="label-text">Yes</span>
                                                <span class="label-text">No</span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group mb-3">
                                    <div class="row align-items-center">
                                        <div class="col-lg-4">
                                            <label class="mb-1" style="font-style: italic">**For Reference Usage</label>
                                        </div>
                                        <div class="col-lg-8">
                                            <asp:DropDownList ID="ddl_ref1" CssClass="form-control" runat="server">
                                                <asp:ListItem Text="Left">Left</asp:ListItem>
                                                <asp:ListItem Text="Right">Right</asp:ListItem>
                                                <asp:ListItem Text="Blank">Blank</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-6">
                                <div class="form-group mb-3">
                                    <div class="row align-items-center">
                                        <div class="col-lg-4">
                                            <label class="mb-1">Profit Center*</label>
                                        </div>
                                        <div class="col-lg-8">
                                            <label id="ddl_package1" runat="server"></label>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group mb-3">
                                    <div class="row">
                                        <div class="col-lg-4">
                                            <label class="mb-1">Package*</label>
                                        </div>
                                        <div class="col-lg-8">
                                            <p class="mb-1">
                                                Package:
                                                <asp:Label ID="lbl_package" runat="server"></asp:Label>
                                            </p>
                                            <p class="mb-1">Code: P1234</p>
                                            <p class="mb-1">
                                                BV:
                                                <asp:Label ID="lbl_bv" runat="server"></asp:Label>
                                            </p>
                                            <p>Amount: MYR1500</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="container p-4">
                    <div class="float-right">
                        <asp:Button runat="server" ID="btn_add" Style="color: white; background-color: black; border-color: black;" Text="Add" />
                    </div>
                    <div style="margin-top: 40px;">
                        <table border="1">
                            <colgroup>
                                <col style="width: 10%" />
                                <!-- Product Picture -->
                                <col style="width: 5%" />
                                <!-- Product Code -->
                                <col style="width: 10%" />
                                <!-- Product Name -->
                                <col style="width: 7%" />
                                <!-- Price -->
                                <col style="width: 7%" />
                                <!-- DC Used -->
                                <col style="width: 7%" />
                                <!-- DC Price -->
                                <col style="width: 5%" />
                                <!-- Quantity -->
                                <col style="width: 7%" />
                                <!-- Weight -->
                                <col style="width: 10%" />
                                <!-- Total Amount -->
                                <col style="width: 5%" />
                                <!-- BV -->
                                <col style="width: 7%" />
                                <!-- DC Earn -->
                                <col style="width: 20%" />
                                <!-- Edit -->
                            </colgroup>
                            <thead>
                                <tr>
                                    <th class="text-center"></th>
                                    <th class="text-center">Code</th>
                                    <th class="text-center">Name</th>
                                    <th class="text-center">Price</th>
                                    <th class="text-center">DC Used</th>
                                    <th class="text-center">DC Price</th>
                                    <th class="text-center">Quantity</th>
                                    <th class="text-center">Weight</th>
                                    <th class="text-center">Total Amount</th>
                                    <th class="text-center">BV</th>
                                    <th class="text-center">DC Earn</th>
                                    <th class="text-center">Edit</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td class="text-center">
                                        <img src="images/HomePage/promotion4.jpg" class="table-img" /></td>
                                    <td class="text-center">P1234</td>
                                    <td class="text-center">Product Name</td>
                                    <td class="text-center">MYR0.00</td>
                                    <td class="text-center">2</td>
                                    <td class="text-center">MYR130</td>
                                    <td class="text-center">1</td>
                                    <td class="text-center">0.5kg</td>
                                    <td class="text-center">MYR130</td>
                                    <td class="text-center">50</td>
                                    <td class="text-center">5</td>
                                    <td class="text-center">
                                        <div style="display: flex; align-items: center">
                                            <button type="button" id="btnInc" onclick="increaseQuantity(this)" style="background-color: white; color: black; width: 30px; height: 30px; margin: 0; padding: 0; border: 1px solid #000000;">+</button>
                                            <asp:TextBox ID="txtValue" Text="0" CssClass="text-center" runat="server" Style="background-color: white; color: black; width: 30px; height: 30px; margin: 0; padding: 0; border: 1px solid #000000;" ReadOnly="true"></asp:TextBox>
                                            <button type="button" id="btnDec" onclick="decreaseQuantity(this)" style="background-color: white; color: black; width: 30px; height: 30px; margin: 0; padding: 0; border: 1px solid #000000;">-</button>
                                            <button type="button" id="btnConfirm" style="color: white; background-color: #149474; border: 1px solid #149474; margin-left: 10px; height: 30px;">Confirm</button>
                                            <button type="button" id="btnDelete" style="color: white; background-color: #149474; border: 1px solid #149474; margin-left: 10px; height: 30px;">Delete</button>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-center">
                                        <img src="images/HomePage/promotion1.jpg" class="table-img" /></td>
                                    <td class="text-center">P1234</td>
                                    <td class="text-center">Product Name</td>
                                    <td class="text-center">MYR0.00</td>
                                    <td class="text-center">2</td>
                                    <td class="text-center">MYR130</td>
                                    <td class="text-center">1</td>
                                    <td class="text-center">0.5kg</td>
                                    <td class="text-center">MYR130</td>
                                    <td class="text-center">50</td>
                                    <td class="text-center">5</td>
                                    <td class="text-center">
                                        <div style="display: flex; align-items: center">
                                            <button type="button" id="btnInc1" onclick="increaseQuantity(this)" style="background-color: white; color: black; width: 30px; height: 30px; margin: 0; padding: 0; border: 1px solid #000000;">+</button>
                                            <asp:TextBox ID="txtValue1" Text="0" CssClass="text-center" runat="server" Style="background-color: white; color: black; width: 30px; height: 30px; margin: 0; padding: 0; border: 1px solid #000000;" ReadOnly="true"></asp:TextBox>
                                            <button type="button" id="btnDec1" onclick="decreaseQuantity(this)" style="background-color: white; color: black; width: 30px; height: 30px; margin: 0; padding: 0; border: 1px solid #000000;">-</button>
                                            <button type="button" id="btnConfirm1" style="color: white; background-color: #149474; border: 1px solid #149474; margin-left: 10px; height: 30px;">Confirm</button>
                                            <button type="button" id="btnDelete1" style="color: white; background-color: #149474; border: 1px solid #149474; margin-left: 10px; height: 30px;">Delete</button>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border-left: none; border-right: none;"></td>
                                    <td style="border-left: none; border-right: none;"></td>
                                    <td style="border-left: none; border-right: none;"></td>
                                    <td class="text-center" style="border-left: none; border-right: none;">MYR0.00</td>
                                    <td class="text-center" style="border-left: none; border-right: none;">4</td>
                                    <td class="text-center" style="border-left: none; border-right: none;">MYR260</td>
                                    <td class="text-center" style="border-left: none; border-right: none;">2</td>
                                    <td class="text-center" style="border-left: none; border-right: none;">1.0kg</td>
                                    <td class="text-center" style="border-left: none; border-right: none;">MYR260</td>
                                    <td class="text-center" style="border-left: none; border-right: none;">100</td>
                                    <td class="text-center" style="border-left: none; border-right: none;">10</td>
                                    <td style="border-left: none; border-right: none;"></td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div class="mt-4">
                        <div class="row">
                            <div class="col-md-6">
                                <p class="mb-1">Total Qty: 3</p>
                                <p class="mb-1">Total BV: 1000</p>
                                <p class="mb-1">DC Used: 4</p>
                                <p class="mb-1">DC Earn: 10</p>
                            </div>
                            <div class="col-md-6">
                                <p class="mb-1">Subtotal: MYR260.00</p>
                                <p class="mb-1">Shipping Fee: MYR 10.00</p>
                                <p class="mb-1">Shipping Discount: MYR 10.00</p>
                                <p class="mb-1">Total: MYR 260.00</p>
                            </div>
                        </div>
                    </div>
                    <hr class="mt-0 mb-2" />
                    <div class="p-2 mb-1 text-white align-content-center" style="background-color: dimgray">
                        <h4 class="m-1 text-white">Shipping Information</h4>
                    </div>
                    <div class="row mt-4">
                        <div class="col-md-6">
                            <label class="mr-4">
                                <input type="radio" id="address" runat="server" style="cursor: pointer;" />
                                My Addresses
                            </label>
                            <label>
                                <input type="radio" id="newAddress" runat="server" style="cursor: pointer;" checked />
                                New Delivery Address
                            </label>
                        </div>
                        <div class="col-md-6">
                            <div class="d-flex justify-content-end">
                                <button id="btn_newAddress" runat="server" style="color: white; background-color: black;">Add New Address</button>
                            </div>
                        </div>
                    </div>
                    <div class="mb-2 mt-2">
                        <p>Recipient Name:</p>
                        <p>Contact:</p>
                        <p>Delivery Address</p>
                        <p>Postcode</p>
                        <p>City</p>
                        <p>State:</p>
                    </div>
                    <div class="p-2 mb-1 text-white align-content-center" style="background-color: dimgray">
                        <h4 class="m-1 text-white">Payment Information</h4>
                    </div>
                    <div class="row align-items-center mt-4">
                        <label class="mr-4 ml-3">eWallet</label>
                        <input type="checkbox" id="btn_ewallet" runat="server" class="checkbox" data-toggle="modal" data-target="#eWalletModal" />
                        <label for="ctl00_ContentPlaceHolder1_btn_ewallet" class="toggle">
                            <span class="label-text">Yes</span>
                            <span class="label-text">No</span>
                        </label>
                    </div>
                    <div class="modal fade" id="eWalletModal" tabindex="-1" role="dialog" aria-hidden="true">
                        <div class="modal-dialog" style="max-width: 900px !important" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title">eWallet Password</h5>
                                </div>
                                <div class="modal-body">
                                    <div class="row mb-3">
                                        <div class="col-4">
                                            Password
                                        </div>
                                        <div class="col-8">
                                            <input id="pass_ewallet" runat="server" placeholder="6 digit numbers" type="password" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-4">
                                            Amount
                                        </div>
                                        <div class="col-8">
                                            <input id="input_amount" runat="server" type="text" />
                                        </div>
                                    </div>
                                    <div class="float-right">
                                        <button id="btn_proceed" class="btn" data-dismiss="modal" aria-label="Close" style="color: white; background-color: #149474">Confirm</button>
                                    </div>
                                    <p class="mt-4">Member ID: MY1234567</p>
                                    <p>Name: Your Name</p>
                                    <p>eWallet Balance: RM2,000</p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <p>Total Price: RM260.00</p>
                    <hr class="mt-0 mb-2" />
                    <div class="row">
                        <div class="col-2">
                            <strong>Note:</strong>
                        </div>
                        <div class="col-10">
                            <p>- Ensure accurate personal information to avoid potential delivery delays.</p>
                            <p>- Please include your mobile phone number on the order form.</p>
                            <p>- Enjoy free delivery in West Malaysia for orders over RM250 in a single receipt.</p>
                            <p>- Get a delivery discount for East Malaysia for orders over RM250 in a single receipt.</p>
                            <p>- For a delays exceeding a week, use 'track my order(s)' or contact Customer Support.</p>
                        </div>
                    </div>
                    <hr class="mt-0 mb-2" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="container p-4" style="margin-bottom: 20px;">
                    <div class="row">
                        <div class="col-md-6">
                            <span style="font-size: 24px; font-weight: bold; color: black;" class="mr-4">Payment Method</span>
                            <br />
                            <label class="mr-4">
                                <input type="radio" id="cardPayment" runat="server" style="cursor: pointer;" checked />
                                Credit Card / Debit Card
                            </label>
                            <label>
                                <input type="radio" id="onlinePayment" runat="server" style="cursor: pointer;" />
                                Online Payment
                            </label>
                        </div>
                        <div class="col-md-6">
                            <div class="d-flex justify-content-end">
                                <asp:LinkButton runat="server" ID="btn_back1" OnClick="btn_back1_Click" Text="Back" CssClass="btn mr-2" Style="background-color: black; border: black; color: white; width: 90px;"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btn_continue1" OnClick="btn_continue1_Click" Text="Continue" CssClass="btn" Style="background-color: black; border: black; color: white; width: 90px;"></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:HiddenField ID="divNameVisible1" runat="server" Value="true" />
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_continue1" EventName="click" />
                <asp:AsyncPostBackTrigger ControlID="btn_back1" EventName="click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>

    <script>

        function BinaryPlacementCheckbox() {
            var checkbox = document.getElementById("ContentPlaceHolder1_btn_placement");

            // Check the current state of the checkbox and toggle it
            if (checkbox.checked) {
                checkbox.checked = false;
            } else {
                checkbox.checked = true;
            }
        }

        function MarryCheckbox() {
            var checkbox = document.getElementById("ContentPlaceHolder1_btn_married");

            // Check the current state of the checkbox and toggle it
            if (checkbox.checked) {
                checkbox.checked = false;
            } else {
                checkbox.checked = true;
            }
        }

        function checkAll() {
            document.getElementById("<%= selectAll.ClientID %>").click();
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

        function btnNameClick() {
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
            var charCode = event.keyCode;

            if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode === 8 || charCode === 32) {
                return true;
            } else {
                return false;
            }
        }

        function togglePasswordVisibility() {
            var passwordInput = document.getElementById('<%= txt_password.ClientID %>');
            var passInput = document.getElementById('<%= txt_password1.ClientID %>');
            if (passwordInput.type === 'text') {
                passwordInput.type = 'password';
                $('.icon1').removeClass('fa-eye').addClass('fa-eye-slash');
            } else {
                passwordInput.type = 'text';
                $('.icon1').removeClass('fa-eye-slash').addClass('fa-eye');
            }
            if (passInput.type === 'text') {
                passInput.type = 'password';
                $('.icon1').removeClass('fa-eye').addClass('fa-eye-slash');
            } else {
                passInput.type = 'text';
                $('.icon1').removeClass('fa-eye-slash').addClass('fa-eye');
            }
        }

        function toggleConfirmPasswordVisibility() {
            var confirmPasswordInput = document.getElementById('<%= txt_confirmpassword.ClientID %>');
            var confirmPassInput = document.getElementById('<%= txt_confirmpassword1.ClientID %>');
            if (confirmPasswordInput.type === 'text') {
                confirmPasswordInput.type = 'password';
                $('.icon2').removeClass('fa-eye').addClass('fa-eye-slash');
            } else {
                confirmPasswordInput.type = 'text';
                $('.icon2').removeClass('fa-eye-slash').addClass('fa-eye');
            }
            if (confirmPassInput.type === 'text') {
                confirmPassInput.type = 'password';
                $('.icon2').removeClass('fa-eye').addClass('fa-eye-slash');
            } else {
                confirmPassInput.type = 'text';
                $('.icon2').removeClass('fa-eye-slash').addClass('fa-eye');
            }
        }

        function packagePage() {
            var title = document.getElementById('<%= title.ClientID %>');
            var ddl_package = document.getElementById('<%= ddl_package.ClientID %>');
            var lblpackage = document.getElementById('<%= package1.ClientID %>');
            var txt_title = document.getElementById('<%= txt_title.ClientID %>')

            document.getElementById('registration').style.display = 'none';
            document.getElementById('package').style.display = 'block';
            document.getElementById('summary').style.display = 'none';
            title.innerText = ddl_package.value;
            lblpackage.innerText = ddl_package.value;
            txt_title.innerText = ddl_package.value;


        }

        function sweetalert_success(message, messagetype) {
            swal({
                title: message,
                type: "success"
            }, function () {
                window.location = "Home.aspx";
            });
        }

        function sweetalert_warning(message, messagetype) {
            swal({
                title: message,
                icon: "warning",
                button: "OK",
            });
        }
    </script>

    <script>
        function updateTitle() {
            var ddlPackage = document.getElementById('<%= ddl_package.ClientID %>');
            var title = document.getElementById('<%= title.ClientID %>');
            var hfQuantity = document.getElementById('<%= hfQuantity.ClientID %>');

            if (ddlPackage.value === "Package 1000BV") {
                hfQuantity.value = "1";
            }
            else {
                title.innerText = "Package 3000BV";
                hfQuantity.value = "3";
            }
        }

        function increaseQuantity(button) {
            var hfQuantity = document.getElementById('<%= hfQuantity.ClientID %>');
            var allButtons = document.querySelectorAll('.btnplus');
            var allTextboxes = document.querySelectorAll('[id$="txtQuantity"]');

            // Calculate the total sum of all textbox values
            var totalSum = Array.from(allTextboxes).reduce(function (sum, txt) {
                // Parse the current value to an integer
                var currentValue = parseInt(txt.value, 10);
                return sum + currentValue;
            }, 0);

            // If the total sum is greater than or equal to hfQuantity.value, disable all buttons
            if (totalSum >= parseInt(hfQuantity.value, 10)) {
                allButtons.forEach(function (btn) {
                    btn.disabled = true;
                });
            } else {
                // Get the parent container of the button, which is the Repeater item
                var repeaterItem = button.parentNode;
                var txtQuantity = repeaterItem.querySelector('[id$="txtQuantity"]');

                // Check if the TextBox exists
                if (txtQuantity) {
                    // Parse the current value to an integer
                    var currentValue = parseInt(txtQuantity.value, 10);

                    // Check if the current value is less than hfQuantity.value
                    if (currentValue < parseInt(hfQuantity.value, 10)) {
                        // Increment the value
                        currentValue++;

                        // Update the TextBox with the new value
                        txtQuantity.value = currentValue;
                    }
                }
            }
        }

        function decreaseQuantity(button) {
            var repeaterItem = button.parentNode;
            var txtQuantity = repeaterItem.querySelector('[id$="txtQuantity"]');

            if (txtQuantity) {
                var currentValue = parseInt(txtQuantity.value, 10);

                if (currentValue > "0") {
                    currentValue--;
                }

                txtQuantity.value = currentValue;
            }
        }

        function registrationPage() {
            document.getElementById('registration').style.display = 'block';
            document.getElementById('package').style.display = 'none';
            document.getElementById('summary').style.display = 'none';

        }

        function summaryPage() {

            document.getElementById('summary').style.display = 'block';
            document.getElementById('package').style.display = 'none';
            document.getElementById('registration').style.display = 'none';

            var lblPackage = document.getElementById('<%= lbl_package.ClientID %>');
            alert(ddlPackage.value);
            lblPackage.value = ddlPackage.value;
        }

    </script>

    <script>
        function productPage() {
            document.getElementById('summary').style.display = 'none';
            document.getElementById('package').style.display = 'block';
            document.getElementById('registration').style.display = 'none';
        }

        function btnCmpClick() {
            var btnName = document.getElementById('<%= btn_name1.ClientID %>');
            var btnCmp = document.getElementById('<%= btn_company1.ClientID %>');
            var divName1 = document.getElementById('<%= divName1.ClientID %>');
            var divNricpass = document.getElementById('<%= div_nricpass1.ClientID %>');
            var ddl = document.getElementById('<%= ddl_type1.ClientID %>');
            var divic = document.getElementById('<%= div_nric1.ClientID %>');
            var divpass = document.getElementById('div_passport1');
            var divCmpname = document.getElementById('<%= div_cmpname1.ClientID %>');
            var divCmpno = document.getElementById('<%= div_cmpno1.ClientID %>');
            var divNameVisible = document.getElementById('<%= divNameVisible1.ClientID %>');
            var txtName = document.getElementById('<%= txt_fullname1.ClientID %>');
            var txtNric = document.getElementById('<%= txt_nric1.ClientID %>');
            var txtCmpName = document.getElementById('<%= txt_companyname1.ClientID %>');
            var txtCmpNo = document.getElementById('<%= txt_companyno1.ClientID %>');

            btnName.addEventListener('click', function () {
                divName1.style.display = 'block';
                divNameVisible.value = 'true';
                divNricpass.style.display = 'block';
                divic.style.display = 'block';
                divpass.style.display = 'none';
                ddl.value = 'NRIC';
                divCmpname.style.display = 'none';
                divCmpno.style.display = 'none';
                btnName.style.borderColor = 'black';
                btnName.style.color = 'black';
                btnName.style.fontWeight = 'bold';
                btnCmp.style.borderColor = '#777777';
                btnCmp.style.color = '#777777';
                btnCmp.style.fontWeight = '400';
                txtCmpName.value = "";
                txtCmpNo.value = "";
            });

            btnCmp.addEventListener('click', function () {
                divName1.style.display = 'none';
                divNameVisible.value = 'false';
                divNricpass.style.display = 'none';
                divic.style.display = 'none';
                divpass.style.display = 'none';
                divCmpname.style.display = 'block';
                divCmpno.style.display = 'block';
                btnCmp.style.borderColor = 'black';
                btnCmp.style.color = 'black';
                btnCmp.style.fontWeight = 'bold';
                btnName.style.borderColor = '#777777';
                btnName.style.color = '#777777';
                btnName.style.fontWeight = '400';
                txtName.value = "";
                txtNric.value = "";
            });
        }
    </script>

</asp:Content>

