<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Member_Placement_Setting.aspx.cs" Inherits="Member_Placement_Setting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style>
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

        .form-check-input input[type="radio"] {
            margin-right: 5px;
        }

        .form-check-input label {
            font-size: 15px;
        }

        .btn_confirm{
            border: 1px solid #efefef;
            padding: 10px;
            border-radius: 5px;
            background-color: #e2e5e8;
        }

        .btn_confirm:hover {
            background-color: #007bff; /* Change this to the desired hover color */
            color: #ffffff; /* Change this to the desired text color on hover */
        }

        @media only screen and (max-width: 767px) {
            .membership_title {
                font-size: 16px;
                color: black;
            }        
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <section class="pf-details mt-4" style="height:100vh;">
        <div class="container">
            <div class="row text-left mb-3">
                <div class="col-12 d-inline-flex">
                    <p class="membership_title fw-200" id="lbl_168">Registration - </p>
                    &nbsp;<p class="membership_title fw-500" id="lbl_169">Auto-Placement Setting</p>
                </div>
            </div>
            
            <hr />

            <div class="row mb-5">
                <div class="col-12 mb-3">
                    <p class="membership_title fw-200" id="lbl_170">Binary Placement</p>
                </div>
                <div class="col-12 d-inline-flex">
                    <div class="col-4 col-xl-2">
                        <asp:RadioButton runat="server" ID="btn_left" Text="Your Left" CssClass="form-check-input" OnCheckedChanged="btn_left_CheckedChanged" AutoPostBack="true" />
                    </div>
                    <div class="col-4 col-xl-2">
                        <asp:RadioButton runat="server" ID="btn_right" Text="Your Right" CssClass="form-check-input" OnCheckedChanged="btn_right_CheckedChanged" AutoPostBack="true" />
                    </div>
                    <div class="col-4 col-xl-2 text-right">
                        <asp:LinkButton runat="server" ID="btn_confirm" Text="Confirm" OnClick="btn_confirm_Click" CssClass="btn_confirm"></asp:LinkButton>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-12">
                    <p id="lbl_174">Note:</p>
                    <p id="lbl_175">All newly registed member will be placed according to the Placement Setting as specified above, after ending of each cycle OR when upline place a Downline to your account. You are allowed to change the Placement Setting from time to time to suit your requirements.</p>
                </div>
            </div>
        </div>
    </section>

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
            var page = 'Auto Placement';
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

