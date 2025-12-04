<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ecentra</title>
    <meta name="description" content="Welcome to Ecentra, where passion meets purpose in our quest to source the finest wellness, beauty, and life-enhancing products from around the globe.">
    <meta charset="utf-8" accesskey="" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width ,initial-scale=1,maximum-scale=1,user-scalable=no,viewport-fit=cover" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
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
        body {
            height: 100vh;
        }

        .w-65 {
            width: 65%;
        }

        .textbox_width {
            width: 65%;
        }

        .cursor-pointer {
            cursor: pointer;
        }

        .f-400 {
            font-weight: 400;
        }

        .f-500 {
            font-weight: 500;
        }

        @media only screen and (min-width: 768px) {

            .contact_div{
                display:inline-flex;
            }

            .bg-img {
                background-image: url('img/Background/Background_Login_v2.png');
                background-repeat: no-repeat;
                background-size: cover;
            }

            .div_verify {
                display: inline-flex;
                width: 34%;
                justify-content: space-between;
                text-align: center;
            }

            .div_forgot_password {
                display: inline-flex !important;
                justify-content: space-around;
                flex-wrap: nowrap;
                flex-direction: row;
                margin: 0px 50px;
            }
        }

        @media only screen and (max-width: 767px) {

            .contact_div{
                display:grid;
            }

            .bg-img {
                background-image: url('img/Background/Background_Login_v2.png');
                background-repeat: no-repeat;
                background-size: cover;
            }

            .mobile_div {
                width: 100%;
                display: grid;
            }

            .div_verify {
                display: inline-flex;
                width: 50%;
                justify-content: space-between;
                text-align: center;
            }

            .div_forgot_password {
                display: inline-flex !important;
                justify-content: space-around;
                flex-wrap: nowrap;
                flex-direction: row;
                margin: 0px;
            }

            .w-65 {
                width: 100%;
            }

            .textbox_width {
                width: 48%;
            }
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <!-- Start Portfolio Details Area -->
        <section class="pf-details bg-img section" style="padding: 133px 0; height: 100vh;">
            <div class="container" style="max-width: 700px; padding: 132px 0px;">
                <div class="row m-0">
                    <div class="col-12 d-inline-flex">
                        <p class="f-400 f-black cursor-pointer" onclick="SetLanguage('English')">
                            EN &nbsp; 
                        </p>
                        <p>|</p>
                        <p class="f-400 f-black cursor-pointer" onclick="SetLanguage('Chinese')">
                              &nbsp; 中文
                        </p>
                    </div>
                    <div class="col-12 mobile_div">
                        <div id="div_phone" style="background: #fadc00; border:1px solid black;">
                            <asp:TextBox runat="server" ID="txt_mobilenumber" Placeholder="ID / NRIC / PASSPORT" MaxLength="20" oninput="validateInput(this)" CssClass="textbox_width" Style="font-size: 1rem; padding: 10px; outline: none; text-align: center;"></asp:TextBox>
                            <div class="div_verify">
                                <div class="w-100 cursor-pointer" onclick="verifyphonenumber()">
                                    <p class="text-black" id="lbl_1">VERIFY</p>
                                </div>
                                <div>
                                    <p class="text-white">|</p>
                                </div>
                                <div class="w-100 cursor-pointer" onclick="ComingSoon()">
                                    <p class="text-black" id="lbl_2">JOIN US</p>
                                </div>
                            </div>
                        </div>

                        <div id="div_password" style="background: #fadc00;">
                            <asp:HiddenField runat="server" ID="hdn_Member_ID" />
                            <asp:TextBox runat="server" ID="txt_password" Placeholder="PASSWORD" MaxLength="20" TextMode="Password" CssClass="textbox_width" Style="font-size: 1rem; padding: 10px; outline: none; text-align: center;"></asp:TextBox>
                            <div class="div_verify">
                                <div class="w-100 cursor-pointer">
                                    <p class="text-black" id="lbl_3" onclick="verifypassword()">LOGIN</p>
                                </div>
                                <div>
                                    <p class="text-white">|</p>
                                </div>
                                <div class="w-100 cursor-pointer">
                                    <p class="text-black" id="lbl_4" onclick="backtoverifyphonenumber()">BACK</p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 div_forgot_password">
                        <a href="ComingSoon.aspx" class="f-400 f-black" id="lbl_5">Forgot Password</a>
                        <div class="d-inline-flex">
                            <asp:CheckBox runat="server" ID="chk_stayloggedin" />&nbsp;<p class="f-400 f-black" id="lbl_6">Stay Logged In</p>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row text-center m-0">
                <div class="col-12">
                    <img class="w-65" src="img/Background/Ecentra_login_logo_v2.png" />
                    <p class="f-black"></p>
                    <div class="contact_div mt-4">
                        <p class="f-black">• &nbsp; Customer Service : <b class="f-500">+603-2181 3118</b></p>&nbsp;<p class="f-black">&nbsp;  • WhatsApp Text : <b class="f-500">+6018-766 1230</b></p>
                    </div>

                </div>
            </div>
        </section>
        <!-- End Portfolio Details Area -->

        <script type="text/javascript" src="js/jquery-3.2.1.min.js"></script>

        <script type="text/javascript">

            $(document).ready(function () {
                loadlangsetting();
                var div_phone = document.getElementById('div_phone');
                var div_password = document.getElementById('div_password');
                if (div_phone) {
                    div_phone.style.display = 'block';
                    div_password.style.display = 'none';
                }

            });

            function SetLanguage(language) {
                var expirationDate = new Date();
                expirationDate.setDate(expirationDate.getDate() + 30); // Set the expiration date to 30 days from now

                // Convert the expiration date to a UTC string
                var expires = "expires=" + expirationDate.toUTCString();

                // Set the cookie with the expiration date
                document.cookie = "language=" + language + ";" + expires;
                loadlangsetting();
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

            function loadlangsetting() {
                var page = 'Login';
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
                            } else {
                                window[item.Label_Name] = item.Language_Content;
                            }
                        });
                    }
                });
            }

            function validateInput(input) {
                // Get the current value of the input
                var inputValue = input.value;

                // Remove any non-alphanumeric characters
                var cleanValue = inputValue.replace(/[^a-zA-Z0-9]/g, '');

                // Update the input value
                input.value = cleanValue;
            }

            function registermember() {
                window.location.href = "Register_Member.aspx";
            }

            function ComingSoon() {
                window.location.href = "ComingSoon.aspx";
            }

            function verifyphonenumber() {

                var mobilenumber = document.getElementById('<%= txt_mobilenumber.ClientID %>').value;
            if (mobilenumber == '') {
                swal({
                    title: "Please key in your ID / NRIC / PASSPORT",
                    icon: "error",
                    button: "ok",
                });
            } else {
                $.ajax({
                    type: "POST",
                    url: "Login.aspx/Check_Mobile_Number",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({ mobilenumber: mobilenumber }),
                    success: function (response) {
                        var responseData = JSON.parse(response.d); // Parse the response JSON string
                        if (responseData.userdetails[0].Result == "Success") {
                            document.getElementById('<%= hdn_Member_ID.ClientID %>').value = responseData.userdetails[0].Member_ID;

                            var div_phone = document.getElementById('div_phone');
                            var div_password = document.getElementById('div_password');
                            if (div_phone) {
                                div_phone.style.display = 'none';
                                div_password.style.display = 'block';
                            }
                        } else if (responseData.userdetails[0].Result == "User not exists") {
                            swal({
                                title: "User not exists",
                                icon: "error",
                                button: "ok",
                            });
                        }
                    }
                });
                }
            }

            function verifypassword() {
                var member_id = document.getElementById('<%= hdn_Member_ID.ClientID %>').value;
            var password = document.getElementById('<%= txt_password.ClientID %>').value;

            if (password == '') {
                swal({
                    title: "Please key in your password",
                    icon: "error",
                    button: "ok",
                });
            } else {
                $.ajax({
                    type: "POST",
                    url: "Login.aspx/Check_Password",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({ member_id: member_id, password: password }),
                    success: function (response) {
                        var responseData = JSON.parse(response.d); // Parse the response JSON string

                        if (responseData.userdetails[0].Result == "Success") {

                            var checkBox = document.getElementById('<%= chk_stayloggedin.ClientID %>');
                            var isChecked = checkBox.checked;

                            // Use the isChecked variable as needed
                            if (isChecked) {
                                var expirationDate = new Date();
                                expirationDate.setDate(expirationDate.getDate() + 30); // Set the expiration date to 30 days from now

                                // Convert the expiration date to a UTC string
                                var expires = "expires=" + expirationDate.toUTCString();

                                // Set the cookie with the expiration date
                                document.cookie = "userid=" + responseData.userdetails[0].cardno + ";" + expires;
                            } else {
                                var expirationDate = new Date();
                                expirationDate.setDate(expirationDate.getDate() + 1); // Set the expiration date to 30 days from now

                                // Convert the expiration date to a UTC string
                                var expires = "expires=" + expirationDate.toUTCString();

                                // Set the cookie with the expiration date
                                document.cookie = "userid=" + responseData.userdetails[0].cardno + ";" + expires;
                            }
                            window.location.href = "Home.aspx";

                        }
                        else if (responseData.userdetails[0].Result == "Incorrect Password") {
                            swal({
                                title: "Incorrect Password",
                                icon: "error",
                                button: "ok",
                            });
                        }
                    }
                });
                }
            }

            function backtoverifyphonenumber() {
                var div_phone = document.getElementById('div_phone');
                var div_password = document.getElementById('div_password');
                if (div_phone) {
                    div_phone.style.display = 'block';
                    div_password.style.display = 'none';
                }
            }

            function sweetalert_success(message, messagetype) {
                swal({
                    title: message,
                    icon: "success",
                    button: "ok",
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

        <script type="text/javascript">

            function Logout() {
                const cookies = document.cookie.split(";");

                for (let i = 0; i < cookies.length; i++) {
                    const cookie = cookies[i];
                    const eqPos = cookie.indexOf("=");
                    const name = eqPos > -1 ? cookie.substr(0, eqPos) : cookie;
                    document.cookie = name + "=;expires=Thu, 01 Jan 1970 00:00:00 GMT";
                }

                window.location.href = "Login.aspx";
            }

        </script>

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

    </form>
</body>
</html>
