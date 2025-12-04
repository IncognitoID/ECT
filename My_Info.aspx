<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="My_Info.aspx.cs" Inherits="My_Info" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style>
        .header-nav-menu {
            /* add background styles here */
            height: 36px;
            background-size: 100%;
            line-height: 36px;
            margin-bottom: 10px;
        }

        input[type="text"], input[type="email"], input[type="url"], input[type="password"], input[type="search"], input[type="number"], input[type="tel"], input[type="range"], input[type="date"], input[type="month"], input[type="week"], input[type="time"], input[type="datetime"], input[type="datetime-local"], input[type="color"], textarea {
            padding: 7px 20px;
        }

        .bg-grey {
            background-color: #e9ecef;
        }

        .password-field {
            border-right: 0px solid #ccc !important;
        }

        .btn-password {
            padding: 10px;
            border: 1px solid #ccc;
            border-left: 0px;
            border-radius: 0px 3px 3px 0px;
            outline: 0px;
        }

        /* Remove button click effect */
        .btn-clean:focus,
        .btn-clean:active {
            outline: none;
            box-shadow: none;
        }

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
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div id="loadingOverlay" style="display: none;">
        <div class="spinner-border text-primary" role="status">
            <i class="fa fa-spinner fa-spin spinnercolor"></i>
        </div>
    </div>

    <div class="container p-4 mb-5">
        <div class="row text-left mb-3">
            <div class="col-12 d-inline-flex">
                <p class="membership_title fw-200" id="lbl_176">My Profile - </p>
                &nbsp;<p class="membership_title fw-500" id="lbl_177">My Info</p>
            </div>
        </div>

        <hr />
        <form>
            <div class="row">
                <div class="col-lg-5">
                    <h5 class="mb-3" id="lbl_178">Member Profile</h5>
                    <div class="form-group mb-3">
                        <div class="row align-items-center">
                            <div class="col-lg-4">
                                <label class="mb-1" for="input_nationality" id="lbl_179">Nationality </label>
                            </div>
                            <div class="col-lg-8">
                                <asp:Label runat="server" ID="lbl_nationality" CssClass="form-control bg-grey" Enabled="false"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group mb-3">
                        <div class="row align-items-center">
                            <div class="col-lg-4">
                                <label class="mb-1" for="input_id" id="lbl_180">Member ID </label>
                            </div>
                            <div class="col-lg-8">
                                <asp:Label runat="server" ID="lbl_member_id" CssClass="form-control bg-grey"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group mb-3">
                        <div class="row align-items-center">
                            <div class="col-lg-4">
                                <label class="mb-1" for="input_join" id="lbl_181">Join Date </label>
                            </div>
                            <div class="col-lg-8">
                                <asp:Label runat="server" ID="lbl_member_join_date" CssClass="form-control bg-grey"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group mb-3" id="div_name" runat="server">
                        <div class="row align-items-center">
                            <div class="col-lg-4">
                                <label class="mb-1" for="input_name" id="lbl_182">Full Name </label>
                            </div>
                            <div class="col-lg-8">
                                <asp:Label runat="server" ID="lbl_member_name" CssClass="form-control bg-grey"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group mb-3" id="div_identity" runat="server">
                        <div class="row align-items-center">
                            <div class="col-lg-4">
                                <label class="mb-1" for="input_nric" id="lbl_183">NRIC / Passport </label>
                            </div>
                            <div class="col-lg-8">
                                <asp:Label runat="server" ID="lbl_member_identity" CssClass="form-control bg-grey"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group mb-3" id="div_company_name" runat="server" style="display: none;">
                        <div class="row align-items-center">
                            <div class="col-lg-4">
                                <label class="mb-1" for="input_cmpName" id="lbl_184">Company Name </label>
                            </div>
                            <div class="col-lg-8">
                                <asp:Label runat="server" ID="lbl_company_name" CssClass="form-control bg-grey"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group mb-3" id="div_company_no" runat="server" style="display: none;">
                        <div class="row align-items-center">
                            <div class="col-lg-4">
                                <label class="mb-1" for="input_cmpNo" id="lbl_185">Company No. </label>
                            </div>
                            <div class="col-lg-8">
                                <asp:Label runat="server" ID="lbl_company_no" CssClass="form-control bg-grey"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group mb-3" id="div_gender" runat="server">
                        <div class="row align-items-center">
                            <div class="col-lg-4">
                                <label class="mb-1" for="input_gender" id="lbl_186">Gender </label>
                            </div>
                            <div class="col-lg-8">
                                <asp:Label runat="server" ID="lbl_member_gender" CssClass="form-control bg-grey"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group mb-3">
                        <div class="row align-items-center">
                            <div class="col-lg-4">
                                <label class="mb-1" for="input_phone" id="lbl_187">Mobile No. </label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_member_mobile" runat="server" CssClass="form-control" required onkeypress='return event.charCode >= 48 && event.charCode <= 57' MaxLength="11"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-group mb-3">
                        <div class="row align-items-center">
                            <div class="col-lg-4">
                                <label class="mb-1" for="input__email" id="lbl_188">Email </label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_member_email" runat="server" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-2"></div>
                <div class="col-lg-5">
                    <h5 class="mb-3" id="lbl_189">Address</h5>
                    <div class="form-group mb-3">
                        <div class="row align-items-center">
                            <div class="col-lg-4">
                                <label class="mb-1" for="input_country" id="lbl_190">Country </label>
                            </div>
                            <div class="col-lg-8">
                                <asp:DropDownList runat="server" ID="ddl_country" CssClass="form-control" onchange="toggleCountry()">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="form-group mb-3" id="div_state">
                        <div class="row align-items-center">
                            <div class="col-lg-4">
                                <label class="mb-1" for="input_state" id="lbl_191">State </label>
                            </div>
                            <div class="col-lg-8">
                                <asp:DropDownList runat="server" ID="ddl_State" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="form-group mb-3">
                        <div class="row align-items-center">
                            <div class="col-lg-4">
                                <label class="mb-1" for="input_city" id="lbl_192">City </label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_city" runat="server" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-group mb-3">
                        <div class="row align-items-center">
                            <div class="col-lg-4">
                                <label class="mb-1" for="input_address" id="lbl_193">Address </label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_address_1" runat="server" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-group mb-3">
                        <div class="row align-items-center">
                            <div class="col-lg-4">
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_address_2" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-group mb3">
                        <div class="row align-items-center">
                            <div class="col-lg-4">
                                <label class="mb-1" for="input_postcode" id="lbl_194">Postcode </label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_postcode" runat="server" CssClass="form-control" required onkeypress='return event.charCode >= 48 && event.charCode <= 57' MaxLength="5"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <hr />

            <div class="row">
                <div class="col-lg-5">
                    <h5 class="mb-3" id="lbl_195">Bank Information</h5>
                    <div class="form-group mb-3">
                        <div class="row align-items-center">
                            <div class="col-lg-4">
                                <label class="mb-1" for="input_ownerName" id="lbl_196">Bank Account Owner Name</label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_account_name" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-group mb-3">
                        <div class="row align-items-center">
                            <div class="col-lg-4">
                                <label class="mb-1" for="input_bankName" id="lbl_197">Bank Name </label>
                            </div>
                            <div class="col-lg-8">
                                <asp:DropDownList runat="server" ID="ddl_bank" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="form-group mb-3">
                        <div class="row align-items-center">
                            <div class="col-lg-4">
                                <label class="mb-1" for="input_bankNo" id="lbl_198">Account No. </label>
                            </div>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txt_account_no" runat="server" CssClass="form-control" MaxLength="20" onkeypress='return event.charCode >= 48 && event.charCode <= 57'></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-2"></div>
                <div class="col-lg-5" runat="server" id="div_e_wallet_password">
                    <h5 class="mb-3" id="lbl_199">Wallet Password</h5>
                    <div class="form-group mb-3">
                        <div class="row align-items-center">
                            <div class="col-lg-4">
                                <label class="mb-1" for="input_ewalletpswd" id="lbl_200">Password </label>
                            </div>
                            <div class="col-lg-8">
                                <div class="input-group">
                                    <asp:TextBox ID="txt_ewallet_password" runat="server" TextMode="Password" CssClass="form-control password-field" placeholder="6 digit numbers" Text="" MaxLength="6" onkeypress='return event.charCode >= 48 && event.charCode <= 57'></asp:TextBox>
                                    <div class="input-group-append">
                                        <button type="button" class="btn-outline-secondary h-100 p-10 btn-password btn-clean" onclick="TogglePassword('<%= txt_ewallet_password.ClientID %>', 'icon-ewallet')">
                                            <i class="fa fa-eye-slash icon-ewallet" aria-hidden="true"></i>
                                            <!-- Closed Eye Icon -->
                                        </button>
                                    </div>
                                </div>
                                <label style="margin: 0px; font-size: 10px; color: red; font-weight: bold;" id="lbl_201">
                                    * minimum 6 characters</label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group mb-3">
                        <div class="row align-items-center">
                            <div class="col-lg-4">
                                <label class="mb-1" for="input_ewalletconfirmpswd" id="lbl_202">Confirm Password </label>
                            </div>
                            <div class="col-lg-8">
                                <div class="input-group">
                                    <asp:TextBox ID="txt_confirm_ewallet_password" runat="server" TextMode="Password" CssClass="form-control password-field" placeholder="confirm 6 digit numbers" Text="" MaxLength="6" onkeypress='return event.charCode >= 48 && event.charCode <= 57'></asp:TextBox>
                                    <div class="input-group-append">
                                        <button type="button" class="btn-outline-secondary h-100 p-10 btn-password btn-clean" onclick="TogglePassword('<%= txt_confirm_ewallet_password.ClientID %>', 'icon-confirm-ewallet')">
                                            <i class="fa fa-eye-slash icon-confirm-ewallet" aria-hidden="true"></i>
                                            <!-- Closed Eye Icon -->
                                        </button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </form>
        <div class="float-right mt-4">
            <asp:Button ID="btn_update" runat="server" CssClass="btn" Style="color: white; background-color: #149474; border: 1px solid #149474;" OnClick="btn_update_Click" OnClientClick="ShowLoading()" Text="Update" />
        </div>
    </div>

    <script>

        document.addEventListener("DOMContentLoaded", function () {
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
            var page = 'My Info';
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
                            if (element) {
                                element.placeholder = item.Language_Content;
                            }
                        } else if (item.Label_Type === 'Button') {
                            var element = document.getElementById('ContentPlaceHolder1_' + item.Label_Name);
                            if (element) {
                                element.value = item.Language_Content;
                            }
                        } else if (item.Label_Type === 'RadioButton') {
                            var radioButton = document.getElementById('ContentPlaceHolder1_' + item.Label_Name);
                            if (radioButton) {
                                radioButton.nextSibling.textContent = item.Language_Content;
                            }
                        } else if (item.Label_Type === 'LinkButton') {
                            var linkButton = document.getElementById('ContentPlaceHolder1_' + item.Label_Name);
                            if (linkButton) {
                                linkButton.innerText = item.Language_Content;
                            }
                        } else {
                            window[item.Label_Name] = item.Language_Content;
                        }
                    });
                }
            });
        }

        function ShowLoading() {
            $("#loadingOverlay").show();
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

        function TogglePassword(inputId, iconClass) {
            var passwordInput = document.getElementById(inputId);
            if (passwordInput.type === 'text') {
                passwordInput.type = 'password';
                $('.' + iconClass).removeClass('fa-eye').addClass('fa-eye-slash');
            } else {
                passwordInput.type = 'text';
                $('.' + iconClass).removeClass('fa-eye-slash').addClass('fa-eye');
            }
        }

        function sweetalert_success(message, messagetype) {
            swal({
                title: message,
                type: "success"
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

</asp:Content>

