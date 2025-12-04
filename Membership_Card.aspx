<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Membership_Card.aspx.cs" Inherits="Membership_Card" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.5/font/bootstrap-icons.css">

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

        .card_div {
            position: absolute;
            text-align: left;
            top: 35%;
            bottom: 50%;
            left: 23%;
        }

        .card_title {
            color: black;
            font-size: 11px;
            line-height: 1.5;
        }

        .card_content {
            color: black;
            font-size: 22px;
        }

        .card_qr {
            position: absolute;
            text-align: right;
            top: 65%;
            bottom: 50%;
            right: 24.5%;
            display: flex;
        }

        .btn_share {
            align-items: center;
            display: flex;
            flex-wrap: nowrap;
            justify-content: center;
            font-size: 25px;
            padding: 15px;
            cursor: pointer;
        }
    </style>

    <style>
        .mobile-refer {
            display: none;
        }

        @media (max-width: 991.98px) {
            .desktop-refer {
                display: none;
            }

            .mobile-refer {
                display: block;
            }
        }

        li {
            list-style: none;
            border-radius: 50%;
        }


        .label {
            padding: 10px;
            font-size: 18px;
            color: #111;
        }

        .copy-text {
            position: relative;
            padding: 10px;
            background: #fff;
            border: 1px solid #ddd;
            border-radius: 10px;
            display: flex;
        }

            .copy-text input.text {
                padding: 10px;
                font-size: 18px;
                color: #555;
                border: none;
                outline: none;
            }

            .copy-text div {
                padding: 10px;
                background: #5784f5;
                color: #fff;
                font-size: 18px;
                border: none;
                outline: none;
                border-radius: 10px;
                cursor: pointer;
            }

                .copy-text div:active {
                    background: #809ce2;
                }

                .copy-text div:before {
                    content: "Copied";
                    position: absolute;
                    top: -40px;
                    right: -5px;
                    background: #a9c2ff;
                    padding: 8px 10px;
                    border-radius: 20px;
                    font-size: 15px;
                    color: #575353;
                    display: none;
                }

                .copy-text div:after {
                    content: "";
                    position: absolute;
                    top: -7px;
                    right: 25px;
                    width: 10px;
                    height: 10px;
                    background: #a9c2ff;
                    transform: rotate(45deg);
                    display: none;
                }

            .copy-text.active div:before,
            .copy-text.active div:after {
                display: block;
            }

        .copy-text2 div:active {
            background: #809ce2;
        }

        .copy-text2 div:before {
            content: "Copied";
            position: absolute;
            top: -45px;
            right: -20px;
            background: #a9c2ff;
            padding: 8px 10px;
            border-radius: 20px;
            font-size: 15px;
            color: #575353;
            display: none;
        }

        .copy-text2 div:after {
            content: "";
            position: absolute;
            top: -13px;
            right: 10px;
            width: 10px;
            height: 10px;
            background: #a9c2ff;
            transform: rotate(45deg);
            display: none;
        }

        .copy-text2.active div:before,
        .copy-text2.active div:after {
            display: block;
        }


        .inputtext {
            position: absolute;
            height: 0px;
            width: 0px !important;
            left: -15px;
            border: none !important;
        }

        .inputtext2 {
            position: absolute;
            height: 10px;
            width: 100% !important;
            left: -15px;
            border: none !important;
            z-index: -1;
        }

        .div_main{
            max-width: 700px; padding: 50px 10px;
        }

        .share_class{
            font-size: 10px; color: #a5a3a3 !important; margin-top: 3.5rem; margin-right: 20px;
        }

        .share_icon_1{
            position: relative; top: 15px;
        }

        .share_icon_2{
            position: relative; top: 25px;
        }

        .share_word_1{
            font-size: 10px; color: #a5a3a3 !important;
        }
        
        .share_word_2{
            font-size: 10px; color: #a5a3a3 !important; line-height: 5px;
        }

        @media only screen and (max-width: 600px) {
            .card_div {
                left: calc(23% - 49px);
            }   
            
            .card_qr {
                right: calc(24.5% - 49px);
            }

            .membership_title {
                font-size: 20px;
                color: black;
            }

            .div_main{
                max-width: 700px; padding: 20px 10px;
            }
        }

        @media only screen and (max-width: 400px) {
  
            .section {
                padding: 30px 0px;
            }

            .membership_title {
                font-size: 17px;
                color: black;
            }
            
            .card_title {
                color: black;
                font-size: 11px;
                line-height: 1;
            }

            .card_content {
                color: black;
                font-size: 17px;
            }

            .card_qr {
                right: calc(26% - 49px);
            }

            .mobile_qr{
                height: 75px !important;
                width: 75px !important;
            }

            .share_class{
                font-size: 10px; color: #a5a3a3 !important; margin-top: 2.5rem; margin-right: 20px;
            }

            .share_icon_1{
                position: relative; top: 5px;
            }

            .share_icon_2{
                position: relative; top: 10px;
            }
        }

        @media only screen and (max-width: 320px) {
  
            .membership_title {
                font-size: 15px;
                color: black;
            }
            
            .card_title {
                color: black;
                font-size: 9px;
                line-height: 1;
            }

            .card_content {
                color: black;
                font-size: 14px;
            }
            
            .card_title {
                color: black;
                font-size: 11px;
                line-height: 1;
            }

            .share_class {
                font-size: 10px;
                color: #a5a3a3 !important;
                margin-top: 2rem;
                margin-right: 10px;
            }

            .card_qr {
                right: calc(29% - 49px);
            }

            .mobile_qr{
                height: 65px !important;
                width: 65px !important;
            }

            .share_icon_1{
                position: relative; top: 5px;
            }

            .share_icon_2{
                position: relative; top: 10px;
            }

            .share_word_1{
                font-size: 8px; color: #a5a3a3 !important;
            }
        
            .share_word_2{
                font-size: 8px; color: #a5a3a3 !important; line-height: 5px;
            }

            .share_img_1{
                width:25px;
            }

            .share_img_2{
                width:25px;
            }

        }


        @media only screen and (max-width: 290px) {
  
            .membership_title {
                font-size: 13px;
                color: black;
            }

            .card_div {
                left: calc(25% - 49px);
            }
            
            .card_title {
                color: black;
                font-size: 9px;
                line-height: 1;
            }

            .card_content {
                color: black;
                font-size: 12px;
            }

            .share_class {
                font-size: 10px;
                color: #a5a3a3 !important;
                margin-top: 1.5rem;
                margin-right: 10px;
            }

            .card_qr {
                right: calc(31% - 49px);
            }

            .mobile_qr{
                height: 55px !important;
                width: 55px !important;
            }

            .share_icon_1{
                position: relative; top: 0px;
            }

            .share_icon_2{
                position: relative; top: 5px;
            }

            .share_word_1{
                font-size: 8px; color: #a5a3a3 !important;
            }
        
            .share_word_2{
                font-size: 8px; color: #a5a3a3 !important; line-height: 5px;
            }

            .share_img_1{
                width:25px;
            }

            .share_img_2{
                width:25px;
            }

        }

    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!-- Start Portfolio Details Area -->
    <section class="pf-details section" style="background-image: url('img/Background/Background_Membership.png'); background-repeat: no-repeat; background-size: cover; height:100vh;">
        <div class="container">
            <div class="row text-left">
                <div class="col-12 d-inline-flex">
                    <p class="membership_title fw-200" id="lbl_215">My profile - </p>
                    &nbsp;<p class="membership_title fw-500" id="lbl_216">eMembership Card</p>
                </div>
            </div>
        </div>
        <div class="container div_main">
            <div class="row">
                <div class="col-12 text-center">
                    <img src="img/Background/Membership.png" />
                    <div class="card_div">
                        <div class="mb-2">
                            <p class="card_title" id="lbl_217">Member ID</p>
                            <p class="card_content" runat="server" id="lbl_user_id"></p>
                        </div>
                        <div class="mb-2">
                            <p class="card_title" id="lbl_218">Name</p>
                            <p class="card_content" runat="server" id="lbl_user_name" style="max-width: 240px;"></p>
                        </div>
                        <div>
                            <p class="card_title" id="lbl_219">Join Date</p>
                            <p class="card_content" runat="server" id="lbl_user_join_date"></p>
                        </div>
                    </div>
                    <div>
                    <div class="card_qr">
                        <div class="text-left share_class">
                            <p class="share_word_1" id="lbl_220">
                                Share Your
                            </p>
                            <p class="share_word_2" id="lbl_221">
                                Personal Invitation
                            </p>
                        </div>
                        <div style="position: relative; left: -5px;">
                            <div onclick="ShowModal()" class="share_icon_1">
                                <img src="img/Icon/icon_share.png" class="share_img_1" />
                            </div>
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <div class="copy-text2 share_icon_2">
                                        <input runat="server" id="lbl_referal_link" type="text" class="text w-100 inputtext2" value="https://ecentra.com.my/Register" />
                                        <div onclick="CopyLink2()" style="border: none; background: none; outline: none;">
                                            <img src="img/Icon/icon_copy.png" class="share_img_1" />
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div>
                            <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                        </div>
                    </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" tabindex="-1" role="dialog" id="ShareModal">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="lbl_222">Share Referral Link</h5>
                        <button type="button" class="close" onclick="CloseModal()">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">

                        <div class="row text-center ">
                            <div class="col-4">
                                <!-- Facebook -->
                                <a class="btn_share icon_style" onclick="shareOnFacebook()" role="button">
                                    <i class="bi bi-facebook"></i>
                                </a>
                            </div>

                            <div class="col-4">
                                <!-- Twitter -->
                                <a class="btn_share icon_style" onclick="shareOnTwitter()" role="button">
                                    <i class="bi bi-twitter"></i>
                                </a>
                            </div>

                            <div class="col-4">
                                <!-- Whatsapp -->
                                <a class="btn_share icon_style" onclick="shareOnWhatsApp()" role="button">
                                    <i class="bi bi-whatsapp"></i>
                                </a>
                            </div>

                            <div class="col-4">
                                <!-- Messenger -->
                                <a class="btn_share icon_style" onclick="shareOnMessenger()" role="button">
                                    <i class="bi bi-messenger"></i>
                                </a>
                            </div>

                            <div class="col-4">
                                <!-- Telegram -->
                                <a class="btn_share icon_style" onclick="shareOnTelegram()" role="button">
                                    <i class="bi bi-telegram"></i>
                                </a>
                            </div>
                        </div>

                        <div>
                            <div>
                                <p id="lbl_223">Referral Link</p>
                            </div>
                            <div>
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <div class="copy-text">
                                            <input runat="server" id="lbl_referralid" type="text" class="text w-100" value="https://ecentra.com.my/Register" />
                                            <div onclick="CopyLink()"><i class="fa fa-clone"></i></div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </section>
    <!-- End Portfolio Details Area -->

    <script type="text/javascript">

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
            var page = 'eMembership Card';
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

        function ShowModal() {
            $('#ShareModal').modal('show');
        }

        function CloseModal() {
            $('#ShareModal').modal('hide');
            $('.modal-backdrop').remove();
        }

        function shareOnFacebook() {
            var userid = getCookieValue("userid");
            const referralLink = 'https://ecentra.com.my/Register?referral_id=' + userid + '&id=';
            const shareUrl = 'https://www.facebook.com/sharer/sharer.php?u=' + encodeURIComponent(referralLink);

            // Open the share dialog in a new window or tab
            window.open(shareUrl, '_blank');
        }

        function shareOnTwitter() {
            var userid = getCookieValue("userid");
            const referralLink = 'https://ecentra.com.my/Register?referral_id=' + encodeURIComponent(userid) + '&id=';
            const tweetText = 'Be an Ecentra user now to enjoy great perks with us!';

            // Construct the Twitter app custom URL scheme
            const twitterAppUrl = 'twitter://post?message=' + encodeURIComponent(tweetText + ' ' + referralLink);

            // Open the Twitter app if available, or fall back to the web intent
            window.location.href = twitterAppUrl;

            // If the Twitter app is not available, open the web intent
            setTimeout(function () {
                const twitterWebUrl = 'https://twitter.com/intent/tweet?text=' + encodeURIComponent(tweetText + ' ' + referralLink);
                window.location.href = twitterWebUrl;
            }, 500); // Wait for 500ms before falling back to the web intent
        }

        function shareOnInstagram() {
            var userid = getCookieValue("userid");

            const referralLink = 'https://ecentra.com.my/Register?referral_id=' + userid + '&id=';
            const instagramUrl = 'https://www.instagram.com/?url=' + encodeURIComponent(referralLink);

            window.open(instagramUrl, '_blank');
        }

        function shareOnWhatsApp() {
            var userid = getCookieValue("userid");
            const referralLink = 'https://ecentra.com.my/Register?referral_id=' + userid + '&id=';
            const message = 'Be a Ecentra user now to enjoy great perks with us ! : ' + referralLink;

            const whatsappUrl = 'https://api.whatsapp.com/send?text=' + encodeURIComponent(message);

            window.open(whatsappUrl, '_blank');
        }

        function shareOnMessenger() {
            var userid = getCookieValue("userid");
            const referralLink = 'https://ecentra.com.my/Register?referral_id=' + userid + '&id=';
            const message = 'Be an Ecentra user now to enjoy great perks with us: ' + referralLink;

            // Try opening the Messenger app
            const appUrl = 'fb-messenger://share?link=' + encodeURIComponent(referralLink) + '&app_id=YOUR_APP_ID';
            window.location.href = appUrl;

            // If the app URL doesn't work, open the Messenger share dialog in the browser
            const webUrl = 'https://www.facebook.com/dialog/send?app_id=YOUR_APP_ID&link=' + encodeURIComponent(referralLink) + '&redirect_uri=' + encodeURIComponent(referralLink) + '&quote=' + encodeURIComponent(message);
            window.open(webUrl, '_blank');
        }

        function shareOnTelegram() {
            var userid = getCookieValue("userid");
            const referralLink = 'https://ecentra.com.my/Register?referral_id=' + userid + '&id=';
            const message = 'Be a Ecentra user now to enjoy great perks with us ! : ' + referralLink;

            const telegramUrl = 'https://t.me/share/url?url=' + encodeURIComponent(referralLink) + '&text=' + encodeURIComponent(message);

            window.open(telegramUrl, '_blank');
        }

        function shareOnWeChat() {
            var userid = getCookieValue("userid");
            const referralLink = 'https://ecentra.com.my/Register?referral_id=' + userid + '&id=';
            copyToClipboard(referralLink);

            window.open("https://web.wechat.com/?lang=zh", "", "width=800,height=500");

        }

        function copyToClipboard(text) {
            let input = copyText.querySelector("input.text");
            input.select();
            document.execCommand("copy");
            window.getSelection().removeAllRanges();
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

        function CopyLink() {
            let copyText = document.querySelector(".copy-text");
            let input = copyText.querySelector("input.text");
            input.select();
            document.execCommand("copy");
            copyText.classList.add("active");
            window.getSelection().removeAllRanges();
            setTimeout(function () {
                copyText.classList.remove("active");
            }, 2500);
        }

        function CopyLink2() {
            let copyText = document.querySelector(".copy-text2");
            let input = copyText.querySelector("input.text");
            input.select();
            document.execCommand("copy");
            copyText.classList.add("active");
            window.getSelection().removeAllRanges();
            setTimeout(function () {
                copyText.classList.remove("active");
            }, 2500);
        }

    </script>


</asp:Content>

