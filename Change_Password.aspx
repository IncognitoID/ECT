<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Change_Password.aspx.cs" Inherits="Change_Password" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

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

        .hv-100{
            height:100vh;
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div id="loadingOverlay" style="display: none;">
        <div class="spinner-border text-primary" role="status">
            <i class="fa fa-spinner fa-spin spinnercolor"></i>
        </div>
    </div>

    <div class="container p-4 mb-5 mt-3 hv-100">
        <div class="row text-left mb-3">
            <div class="col-12 d-inline-flex">
                <p class="membership_title fw-200" id="lbl_204">My Profile - </p>
                &nbsp;<p class="membership_title fw-500" id="lbl_203">Change Password</p>
            </div>
        </div>

        <hr />
        <form>
            <div class="row mt-5" runat="server">
                <div class="col-lg-5">
                    <h5 class="mb-3"id="lbl_205">Change Password</h5>
                    <div class="form-group mb-3">
                        <div class="row align-items-center">
                            <div class="col-lg-4">
                                <label class="mb-1" for="input_oldPass" id="lbl_206">Old Password</label>
                            </div>
                            <div class="col-lg-8">
                                <div class="input-group">
                                    <asp:TextBox ID="txt_old_password" runat="server" TextMode="Password" CssClass="form-control password-field" MaxLength="20"></asp:TextBox>
                                    <div class="input-group-append">
                                        <button type="button" class="btn-outline-secondary h-100 p-10 btn-password btn-clean" onclick="TogglePassword('<%= txt_old_password.ClientID %>', 'icon-old-password')">
                                            <i class="fa fa-eye-slash icon-old-password" aria-hidden="true"></i>
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
                                <label class="mb-1" for="input_newPass" id="lbl_207">New Password</label>
                            </div>
                            <div class="col-lg-8">
                                <div class="input-group">
                                    <asp:TextBox ID="txt_new_password" runat="server" TextMode="Password" CssClass="form-control password-field" MaxLength="20"></asp:TextBox>
                                    <div class="input-group-append">
                                        <button type="button" class="btn-outline-secondary h-100 p-10 btn-password btn-clean" onclick="TogglePassword('<%= txt_new_password.ClientID %>', 'icon-new-password')">
                                            <i class="fa fa-eye-slash icon-new-password" aria-hidden="true"></i>
                                            <!-- Closed Eye Icon -->
                                        </button>
                                    </div>
                                </div>
                                <label style="margin: 0px; font-size: 10px; color: red; font-weight: bold;" id="lbl_208">
                                    * minimum 6 characters</label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group mb-3">
                        <div class="row align-items-center">
                            <div class="col-lg-4">
                                <label class="mb-1" for="input_confirmPass" id="lbl_209">Confirm Password</label>
                            </div>
                            <div class="col-lg-8">
                                <div class="input-group">
                                    <asp:TextBox ID="txt_confirm_new_password" runat="server" TextMode="Password" CssClass="form-control password-field" MaxLength="20"></asp:TextBox>
                                    <div class="input-group-append">
                                        <button type="button" class="btn-outline-secondary h-100 p-10 btn-password btn-clean" onclick="TogglePassword('<%= txt_confirm_new_password.ClientID %>', 'icon-confirm-new-password')">
                                            <i class="fa fa-eye-slash icon-confirm-new-password" aria-hidden="true"></i>
                                            <!-- Closed Eye Icon -->
                                        </button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-2"></div>
                <div class="col-lg-5" runat="server" id="div_change_e_wallet_password">
                    <h5 class="mb-3" id="lbl_210">Change Wallet Password</h5>
                    <div class="form-group mb-3">
                        <div class="row align-items-center">
                            <div class="col-lg-4">
                                <label class="mb-1" for="input_oldeWallet" id="lbl_211">Old Wallet Password</label>
                            </div>
                            <div class="col-lg-8">
                                <div class="input-group">
                                    <asp:TextBox ID="txt_old_e_wallet_password" runat="server" TextMode="Password" CssClass="form-control password-field" MaxLength="6" onkeypress='return event.charCode >= 48 && event.charCode <= 57'></asp:TextBox>
                                    <div class="input-group-append">
                                        <button type="button" class="btn-outline-secondary h-100 p-10 btn-password btn-clean" onclick="TogglePassword('<%= txt_old_e_wallet_password.ClientID %>', 'icon-old-ewallet-password')">
                                            <i class="fa fa-eye-slash icon-old-ewallet-password" aria-hidden="true"></i>
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
                                <label class="mb-1" for="input_neweWallet" id="lbl_212">New Wallet Password</label>
                            </div>
                            <div class="col-lg-8">
                                <div class="input-group">
                                    <asp:TextBox ID="txt_new_e_wallet_password" runat="server" TextMode="Password" CssClass="form-control password-field" MaxLength="6" onkeypress='return event.charCode >= 48 && event.charCode <= 57'></asp:TextBox>
                                    <div class="input-group-append">
                                        <button type="button" class="btn-outline-secondary h-100 p-10 btn-password btn-clean" onclick="TogglePassword('<%= txt_new_e_wallet_password.ClientID %>', 'icon-new-ewallet-password')">
                                            <i class="fa fa-eye-slash icon-new-ewallet-password" aria-hidden="true"></i>
                                            <!-- Closed Eye Icon -->
                                        </button>
                                    </div>
                                </div>
                                <label style="margin: 0px; font-size: 10px; color: red; font-weight: bold;" id="lbl_214">
                                    * minimum 6 characters</label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group mb-3">
                        <div class="row align-items-center">
                            <div class="col-lg-4">
                                <label class="mb-1" for="input_confirmeWallet" id="lbl_213">Confirm Wallet Password</label>
                            </div>
                            <div class="col-lg-8">
                                <div class="input-group">
                                    <asp:TextBox ID="txt_new_confirm_e_wallet_password" runat="server" TextMode="Password" CssClass="form-control password-field" MaxLength="6" onkeypress='return event.charCode >= 48 && event.charCode <= 57'></asp:TextBox>
                                    <div class="input-group-append">
                                        <button type="button" class="btn-outline-secondary h-100 p-10 btn-password btn-clean" onclick="TogglePassword('<%= txt_new_confirm_e_wallet_password.ClientID %>', 'icon-new-confirm-ewallet-password')">
                                            <i class="fa fa-eye-slash icon-new-confirm-ewallet-password" aria-hidden="true"></i>
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
            var page = 'Change Password';
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

