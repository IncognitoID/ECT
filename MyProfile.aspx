<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MyProfile.aspx.cs" Inherits="MyProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <script src="https://kit.fontawesome.com/58a2e30ed2.js" crossorigin="anonymous"></script>

    <style>
        .f-red-member {
            font-weight: bold;
            color: red;
            font-size: 1.5rem;
        }

        .redtext {
            color: red;
            font-weight: bold;
        }

        .greentext {
            color: #40d840;
            font-weight: bold;
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

        .card_div {
            position: absolute;
            text-align: left;
            top: 35%;
            bottom: 50%;
            left: 10%;
        }

        .card_title {
            color: black;
            font-size: 9px;
            line-height: 1.5;
        }

        .card_content {
            color: black;
            font-size: 15px;
        }

        .card_qr {
            position: absolute;
            text-align: right;
            top: 53%;
            bottom: 50%;
            right: 13.5%;
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

        .div_padding_1 {
            padding: 100px 30px;
        }

        .div_padding_2 {
            padding: 100px 0px;
        }

        .div_padding_3 {
            padding: 50px 0px;
        }

        .div_assign_member {
            display: inline-flex !important;
            padding: 0px;
        }

        #hr_line_1 {
            display: none;
        }

        @media only screen and (max-width: 767px) {

            .div_padding_1 {
                padding: 15px 15px;
            }

            .div_padding_2 {
                padding: 15px 0px;
            }

            .div_padding_3 {
                padding: 30px 0px;
            }

            .div_assign_member {
                display: block !important;
            }

            #hr_line_1 {
                display: block;
            }

            .card_qr {
                position: absolute;
                text-align: right;
                top: 62%;
                bottom: 50%;
                right: 12.5%;
                display: flex;
            }
        }
    </style>

    <style>
        .btn_previousbv {
            position: relative;
            display: inline-block;
            background: #3e3e3f;
            color: #fff;
            border: none;
            border-radius: 0;
            padding: 1.25rem 2.5rem;
            font-size: 1rem;
            text-transform: uppercase;
            cursor: pointer;
            transform: translateZ(0);
            transition: color 0.3s ease;
            letter-spacing: 0.0625rem;
        }

        .btn_previousbv:hover::before {
            transform: scaleX(1);
        }

        .btn_previousbv::before {
            position: absolute;
            content: "";
            z-index: -1;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background: #565657;
            transform: scaleX(0);
            transform-origin: 0 50%;
            transition: transform 0.3s ease-out;
        }


        .div-progress-bar {
            background-color: lightyellow;
        }

        .progress-bar {
            background-color: lightyellow;
            display: flex;
            justify-content: space-between;
            align-items: center;
            width: 80%;
            margin: 50px auto;
            position: relative;
            flex-wrap: wrap; /* Allow circles to wrap on smaller screens */
        }

        .circle {
            width: 30px;
            height: 30px;
            border-radius: 50%;
            margin: 5px; /* Add some margin for spacing */
        }

        .labels {
            position: relative;
            width: 80%;
            display: flex;
            justify-content: space-between;
            text-align: center;
            margin: 50px auto;
        }

        .label {
            position: absolute;
            bottom: 0px;
            transform: translateX(-50%);
            font-size: 20px;
        }


        .line {
            position: absolute;
            top: -48px;
            width: 1px;
            height: 20px;
            background-color: black;
            transform: translateX(-50%);
        }

        .rank_header {
            margin: 7rem auto -35px auto;
            position: relative;
            width: 80%;
            display: flex;
            justify-content: space-between;
            text-align: center;
        }

        .header_1 {
            left: 3%;
        }
        
        .header_4 {
            left: 24%;
            width: 120px;
        }

        .header_2 {
            left: 49.5%;
            width: 120px;
        }

        .header_3 {
            right: -10%;
            width: 120px;
        }

        .bo_box {
            border: 1px solid black;
            border-radius: 10px 0px 0px 10px;
            padding: 10px;
            min-width: 100px;
            font-size: calc(.5em + 1vw);
        }

        .eo_box {
            border: 1px solid black;
            border-radius: 0px 10px 10px 0px;
            padding: 10px;
            min-width: 100px;
            font-size: calc(.5em + 1vw);
        }

        .bo_tick_box_icon {
            position: absolute;
            margin-top: -30px;
            width: 35px;
            margin-left: -5px;
        }

        .eo_tick_box_icon {
           position: absolute;
           margin-top: -30px;
           width: 35px;
           margin-left: 7px;
        }

        .img_bo_tick {
            width: 35px;
        }

        .img_eo_tick {
            width: 35px;
        }

        .countdown-box {
            background-color: #f8f9fa;
            padding: 10px 15px;
            border: 2px dashed #28a745;
            border-radius: 10px;
            font-family: 'Segoe UI', sans-serif;
            font-size: 1.2rem;
            font-weight: bold;
            color: #333;
            text-align: center;
            margin-bottom: 15px;
            display: inline-block;
            width: 100%;
        }

        .countdown-box span {
            color: #28a745;
        }

        @media (max-width: 768px) {
            .progress-bar {
                justify-content: space-evenly; /* Adjust spacing for smaller screens */
            }

            .label {
                font-size: 12px; /* Adjust font size for smaller screens */
            }

            .rank_header {
                margin: 5rem auto -50px auto;
                position: relative;
                width: 80%;
                display: flex;
                justify-content: space-between;
                text-align: center;
            }

            .header_1 {
                left: -5%;
                width: 65px;
            }

            .header_4 {
                left: 19%;
                width: 55px;
            }

            .header_2 {
                left: 49.5%;
                width: 55px;
            }

            .header_3 {
                left: 114%;
                width: 55px;
            }

            .progress-bar {
                background-color: #fffdd0;
                display: flex;
                justify-content: space-between;
                align-items: center;
                width: 100%;
                margin: 50px auto;
                position: relative;
                flex-wrap: wrap; /* Allow circles to wrap on smaller screens */
            }

            .labels {
                position: relative;
                width: 100%;
                display: flex;
                justify-content: space-between;
                text-align: center;
                margin: 0px auto;
            }

            .line {
                position: absolute;
                top: -45px;
                width: 1px;
                height: 14px;
                background-color: black;
                transform: translateX(-50%);
            }

            .circle {
                width: 15px; /* Adjust circle size for smaller screens */
                height: 15px; /* Adjust circle size for smaller screens */
                margin: 0px;
            }

            .label {
                bottom: 10px;
                font-size: 10px; /* Further adjust font size for very small screens */
            }

            .bo_box {
                border: 1px solid black;
                border-radius: 10px 0px 0px 10px;
                padding: 10px;
                min-width: 80px;
                font-size: calc(.5em + 1vw);
            }

            .eo_box {
                border: 1px solid black;
                border-radius: 0px 10px 10px 0px;
                padding: 10px;
                min-width: 80px;
                font-size: calc(.5em + 1vw);
            }

            .bo_tick_box_icon {
                position: absolute;
                margin-top: -25px;
                width: 30px;
                margin-left: -5px;
            }

            .eo_tick_box_icon {
               position: absolute;
               margin-top: -25px;
               width: 30px;
               margin-left: 7px;
            }

            .title_size {
                font-size:20px;
            }

            .img_bo_tick {
                width: 30px;
            }

            .img_eo_tick {
                width: 30px;
            }
        }
    </style>

    <style>
        .title_font {
            font-size: 12px;
        }

        /*Product Voucher Title*/
        .product_voucher_title {
            position: relative;
            margin: 0 auto 20px;
            padding: 10px 40px;
            text-align: center;
            background-color: #FF2D55;
            width: 70%;
            font-size: 30px;
            padding: 10px 30px;
            font-weight: bold;
            color: white;
            border: 1px solid #e33838;
            border-radius: 3px;
            font-size: 25px;
        }

            .product_voucher_title::before, .product_voucher_title::after {
                content: '';
                width: 80px;
                height: 100%;
                background-color: #FF2D55;
                position: absolute;
                z-index: -1;
                top: 15px;
                clip-path: polygon(0 0, 100% 0, 100% 100%, 0 100%, 25% 50%);
                background-image: linear-gradient(45deg, transparent 50%, #c22744 50%);
                background-size: 20px 20px;
                background-repeat: no-repeat;
                background-position: bottom right;
            }

            .product_voucher_title::before {
                left: -60px;
            }

            .product_voucher_title::after {
                right: -60px;
                transform: scaleX(-1); /* flip horizontally */
            }

        @media (max-width: 400px) {
            .product_voucher_title {
                font-size: 1.5rem;
            }
        }


        @media (max-width: 330px) {
            .title_font {
                font-size: 11px;
            }
        }


        @media (max-width: 300px) {
            .title_font {
                font-size: 9px;
            }
        }

        .progress .progress-bar-shopper {
            animation-name: animateBar;
            animation-iteration-count: 1;
            animation-timing-function: ease-in;
            animation-duration: .4s;
            background: linear-gradient(to right, #ddec27 0%, #eb8d10 100%);
        }

        @keyframes animateBar {
            0% {
                transform: translateX(-100%);
            }

            100% {
                transform: translateX(0);
            }
        }

        .collect-text {
            position: absolute;
            color: #2b2929;
            font-weight: bold;
            margin-top: -3px;
        }

        .justify-content-space {
            justify-content: space-evenly !important;
        }

        .text-underline {
            text-decoration: underline;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <div class="row m-auto" style="padding: 10px 50px;">
            <div class="col-12 text-right">
                <span id="lbl_224">Filter By: &nbsp;</span>
                <div class="d-inline-flex">
                    <asp:TextBox runat="server" Style="border-radius: 5px 0px 0px 5px;"></asp:TextBox>
                    <div style="padding: 5px 10px 5px 10px; background-color: #d3d32f; border-radius: 0px 5px 5px 0px;">
                        <span><i class="fa-solid fa-magnifying-glass" style="font-size: 15px; color: white;"></i></span>
                    </div>
                </div>
            </div>
        </div>

        <div class="row m-auto div_padding_1">
            <div class="col-xl-3"></div>
            <div class="col-xl-3 col-12 cursor_pointer" onclick="GoToMembership_Card()">
                <img src="img/Background/Membership.png" style="box-shadow: 5px 5px 5px lightblue; border-radius: 10px; border: 1px solid #efefef;" />
                <div class="card_div">
                    <div class="mb-2">
                        <p class="card_title" id="lbl_225">Member ID</p>
                        <p class="card_content" runat="server" id="lbl_user_id"></p>
                    </div>
                    <div class="mb-2">
                        <p class="card_title" id="lbl_226">Name</p>
                        <p class="card_content" runat="server" id="lbl_user_name" style="max-width: 240px;"></p>
                    </div>
                    <div>
                        <p class="card_title" id="lbl_227">Join Date</p>
                        <p class="card_content" runat="server" id="lbl_user_join_date"></p>
                    </div>
                </div>
                <div class="card_qr">
                    <div class="text-left" style="font-size: 10px; color: #a5a3a3 !important; margin-top: 2.3rem; margin-right: 15px;">
                        <p style="font-size: 8px; color: #a5a3a3 !important;" id="lbl_228">
                            Share Your
                        </p>
                        <p style="font-size: 8px; color: #a5a3a3 !important; line-height: 5px;" id="lbl_229">
                            Personal Invitation
                        </p>
                    </div>
                    <div style="position: relative; left: -5px;">
                        <div style="position: relative; top: 10px;">
                            <img src="img/Icon/icon_share.png" style="width: 25px;" />
                        </div>
                        <div class="copy-text2" style="position: relative; top: 20px;">
                            <img src="img/Icon/icon_copy.png" style="width: 25px;" />
                        </div>
                    </div>
                    <div>
                        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                    </div>
                </div>
            </div>
            <div class="col-xl-3 col-12 mt-3">
                <div>
                    <span class="font-weight-bold" style="font-size: 22px;" runat="server" id="lbl_member_name"></span>
                    <br />
                    <span id="lbl_230">ID : </span>&nbsp;<span style="font-size: 17px;" runat="server" id="lbl_member_id"></span>
                </div>
                <div class="mt-2" style="font-size: 17px; line-height: 2;">
                    <span id="lbl_231">Profit Center : </span>&nbsp;<span runat="server" id="lbl_profit_center"></span>
                    <br />
                    <span id="lbl_232">Rank : </span>&nbsp;<span runat="server" id="lbl_rank"></span>
                    <br />
                    <span id="lbl_233">Total Referral : </span>&nbsp;<span runat="server" id="lbl_total_referral"></span>
                    <br />
                    <span id="lbl_234">Total Group : </span>&nbsp;<span runat="server" id="lbl_total_group"></span>
                </div>
                <div class="mt-2 w-100" style="font-size: 17px;">
                    <span id="lbl_343">Monthly</span>
                </div>
                <div class="d-inline-flex w-100" style="border: 1px solid #efefef;">
                    <div class="w-50" style="border-right: 1px solid #efefef; padding: 10px;">
                        <span id="lbl_235">Left:</span><br />
                        <span runat="server" id="lbl_left_downline_point"></span>
                    </div>
                    <div class="w-50" style="padding: 10px;">
                        <span id="lbl_236">Right:</span><br />
                        <span runat="server" id="lbl_right_downline_point"></span>
                    </div>
                </div>
            </div>
            <div class="col-xl-3 col-12 mt-3" style="display:none;border-left: 5px solid orange; border-right: 5px solid orange; border-radius: 10px; margin: auto 0;">
                <div class="mb-3" style="line-height: 1.5;">
                    <span style="font-size: 21px;" id="lbl_237">Current pBV:</span><br />
                    <span class="font-weight-bold" style="font-size: 25px;" runat="server" id="lbl_pBV_value">100 pBV (Qualified)</span>
                </div>

                <div class="mt-2 mb-2 pt-2 pb-2" style="border-top: 2px solid #efefef; border-bottom: 2px solid #efefef;">
                    <span class="font-weight-bold" style="font-size: 18px;" id="lbl_240">Gentle Reminder</span><br />
                    <div class="mb-2" style="line-height: 1.2; font-size: 11px;" id="lbl_241"><span>Each account must maintain a minimum of 100 pBV (Personal BV) per month.</span></div>
                    <div class="mb-2" style="line-height: 1.2; font-size: 11px;" id="lbl_242"><span>Failure to do so will result in a maximum monthly payout limit of RM 200 until the required pBV is met.</span></div>
                    <div class="mb-2" style="line-height: 1.2; font-size: 11px;" id="lbl_243"><span>a) Accounts with bonuses below RM 200 are exempt from pBV maintenance.</span></div>
                </div>

            </div>
            <div class="col-xl-2"></div>
            <div class="col-xl-12" style="text-align:center;padding-top:15px">
                <%-- BB History --%>
                <asp:LinkButton ID="btn_previousbv" runat="server" OnClick="btn_previousbv_Click" class="btn_previousbv">Previous Month Total BV</asp:LinkButton>

            </div>
        </div>

        <hr />

        <div class="row m-auto div_padding_2">
            <div class="col-xl-12 col-12 div_assign_member">
                <div class="col-xl-6 col-12 text-center">
                    <div>
                        <img src="img/MyProfile/User_icon.png" style="width: 90px; padding: 10px;" />
                    </div>
                    <div>
                        <img src="img/MyProfile/tree_line.png" style="width: 30%;" />
                    </div>
                    <div class="col-xl-8 col-12 d-inline-flex">
                        <div class="col-6">
                            <span runat="server" id="lbl_left_member_id"></span>
                            <br />
                            <div style="border: 1px solid black; border-radius: 10px; padding: 10px; font-size: calc(.5em + 1vw);">
                                <span runat="server" id="lbl_left_member_point"></span>
                            </div>
                        </div>
                        <div class="col-6">
                            <span runat="server" id="lbl_right_member_id"></span>
                            <br />
                            <div style="border: 1px solid black; border-radius: 10px; padding: 10px; font-size: calc(.5em + 1vw);">
                                <span runat="server" id="lbl_right_member_point"></span>
                            </div>
                        </div>
                    </div>
                    <div class="col-xl-5 col-12 text-center m-auto pt-3 f-black" style="font-size: 1rem;" runat="server" id="div_bonus">
                        <span id="lbl_245">Qualified <b>100</b> pBV to be entitled to a balance bonus of </span>
                        <span runat="server" id="lbl_total_payout" class="font-weight-bold"></span>
                    </div>
                </div>

                <hr id="hr_line_1" />

                <div class="col-xl-4 col-12 text-left" style="margin: auto 0 !important;">
                    <div>
                        <span class="f-red-member" runat="server" id="lbl_total_member_waiting_assign"></span>
                        <br />
                        <span class="f-red-member" id="lbl_246">waiting to be assign.</span>
                    </div>
                    <br />
                    <div style="font-size: 1rem;">
                        <span id="lbl_247">Please click <a href="Assign_Member.aspx" style="text-decoration: underline; font-weight: bold; color: #944794;">HERE</a> to assign the members.<br />
                            If you do not assign the members within the circle period, the system will automatically assign them. No changes will be allowed after the assignment is made.</span>
                    </div>
                </div>
            </div>
        </div>

        <hr runat="server" id="eo_hr" />

        <div runat="server" id="div_eo_mission" style="display:none" class="row m-auto div_padding_3">
            <div class="col-xl-6 col-12 mb-3 text-center text-underline">
                <h4 class="ml-3 mr-3">Redemption Point Mission</h4>
            </div>
            <div class="col-xl-12 col-12 div_assign_member">
                <div class="col-xl-6 col-12 text-center">
                    <div>
                        <img src="img/MyProfile/User_icon.png" style="width: 90px; padding: 10px;" />
                    </div>
                    <div>
                        <img src="img/MyProfile/tree_line.png" style="width: 30%;" />
                    </div>
                    <div class="d-flex justify-content-space gap-3">
                        <!-- Left Side -->
                        <div class="d-flex gap-2">
                            <div class="bo_box">
                                <span runat="server" class="title_size">BO</span>
                                <img src="img/Icon/tick.png" class="bo_tick_box_icon" runat="server" id="img_left_bo" visible="false" />
                            </div>
                            <div class="eo_box">
                                <span runat="server" class="title_size">EO</span>
                                <img src="img/Icon/tick.png" class="eo_tick_box_icon" runat="server" id="img_left_eo" visible="false" />
                            </div>
                        </div>
                        <!-- Right Side -->
                        <div class="d-flex gap-2">
                            <div class="bo_box">
                                <span runat="server" class="title_size">BO</span>
                                <img src="img/Icon/tick.png" class="bo_tick_box_icon" runat="server" id="img_right_bo" visible="false" />
                            </div>
                            <div class="eo_box">
                                <span runat="server" class="title_size">EO</span>
                                <img src="img/Icon/tick.png" class="eo_tick_box_icon" runat="server" id="img_right_eo" visible="false" />
                            </div>
                        </div>
                    </div>
                </div>

                <hr id="hr_line_2" />

                <div class="col-xl-4 col-12 text-left" style="margin: auto 0 !important;">
                    <div class="countdown-box">
                        <asp:HiddenField runat="server" ID="hdn_countdown_value" Value="0" />
                        <span id="countdown"></span>
                    </div>
                    <div style="font-size: 1rem;">
                        <span id="lbl_453">Complete recruit 2 BO or 2 EO, will additionally award.</span>
                        <br />
                        <span id="lbl_451">1.) 2 BO - 300 RP</span>&nbsp;<img src="img/Icon/tick.png" class="img_bo_tick" runat="server" id="img_list_bo" visible="false" />
                        <br />
                        <span id="lbl_452">2.) 2 EO - 1,000 RP</span>&nbsp;<img src="img/Icon/tick.png" class="img_eo_tick" runat="server" id="img_list_eo" visible="false" />
                    </div>
                </div>
            </div>
        </div>

        <hr />

        <div class="row m-auto" runat="server" id="div_shopper" visible="false">
            <div class="col-xl-12 col-12 div_assign_member">
                <div class="col-xl-6 col-12 mb-3 text-center">
                    <div class="col-xl-8 col-12 text-left m-auto pt-3 f-black" style="font-size: 1.5rem; font-weight: bold; text-decoration: underline;">
                        <span id="lbl_352"></span>
                    </div>
                    <div>
                        <img src="img/MyProfile/User_icon.png" style="width: 90px; padding: 10px;" />
                    </div>
                    <div class="col-xl-8 col-12 m-auto p-0 progress mt-2">
                        <div class="progress-bar-shopper progress-bar-warning" runat="server" id="div_voucher_progress_bar">
                            <asp:Label runat="server" ID="lbl_voucher_collected" CssClass="collect-text"></asp:Label>
                        </div>
                    </div>
                </div>

                <hr id="hr_line_1" />

                <div class="col-xl-4 col-12 text-left" style="margin: auto 0 !important;">
                    <div>
                        <span class="shopper-member" id="lbl_350"></span><span class="f-red-member" runat="server" id="lbl_shopper_upgrade"></span>
                        <br />
                        <span class="shopper-member" id="lbl_351"></span>
                        <br />
                        <span class="shopper-member" id="lbl_353"></span>
                        <br />
                        <span class="shopper-member" id="lbl_356"></span>
                        <br />
                        <span class="shopper-member" id="lbl_357"></span><span class="f-red-member" id="lbl_358"></span><span class="shopper-member" id="lbl_359"></span>
                        <br />
                        <span class="shopper-member" id="lbl_360"></span>

                    </div>
                </div>
            </div>
        </div>

        
    <div class="modal fade" id="monthbvhisModal" tabindex="-1" role="dialog" aria-labelledby="productModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-body">
                    <h4>Previous Month Total BV</h4>
                    <asp:GridView runat="server" ID="grd_bv_month_record" class="table table-bordered table-condensed table-responsive table-hover" AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField HeaderText="Period" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                <ItemStyle CssClass="tablerRestColumn"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_date" runat="server" Text='<%# Eval("Last_Month") %>'></asp:Label>
                                    <br />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Left" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                <ItemStyle CssClass="tablerRestColumn"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_direct_profit" runat="server" Text='<%# Eval("Downline_A_Monthly_Group_Sales_BV", "{0:###,###,###,##0}") %>'></asp:Label><span style="font-weight:bold"> BV</span>
                                    <br />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Right" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                <ItemStyle CssClass="tablerRestColumn"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_bb_cycle_1" runat="server" Text='<%# Eval("Downline_B_Monthly_Group_Sales_BV", "{0:###,###,###,##0}") %>'></asp:Label><span style="font-weight:bold"> BV</span>
                                    <br />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                </div>
            </div>
        </div>
    </div>

        <div class="row m-auto div-progress-bar" runat="server" id="div_bo">
            <div class="col-12">
                <asp:Literal ID="ProgressBarLiteral" runat="server"></asp:Literal>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function Show_BvMonth() {
            $('#monthbvhisModal').modal('show');
        }

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
            var page = 'My Profile';
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
                        } else if (item.Label_Type === 'HTML') {
                            var linkButton = document.getElementById(item.Label_Name);
                            if (linkButton) {
                                linkButton.innerHTML = item.Language_Content;
                            }
                        } else if (item.Label_Type === 'Label') {
                            $('#' + item.Label_Name).text(item.Language_Content);
                        }
                        else {
                            window[item.Label_Name] = item.Language_Content;
                        }
                    });
                }
            });
        }

        function GoToMembership_Card() {
            window.location.href = "Membership_Card.aspx";
        }

        var totalSeconds = 0; // Declare globally so both functions can access it

        function calculate_countdown() {
            totalSeconds = parseInt(document.getElementById('<%= hdn_countdown_value.ClientID %>').value);

            updateCountdown(); // Initial run
            setInterval(updateCountdown, 1000); // Update every second
        }

        function updateCountdown() {
            if (totalSeconds <= 0) {
                document.getElementById("countdown").innerText = "Countdown : 00H 00M 00S";
                return;
            }

            const hours = Math.floor(totalSeconds / 3600);
            const minutes = Math.floor((totalSeconds % 3600) / 60);
            const seconds = totalSeconds % 60;

            document.getElementById("countdown").innerText =
                `Countdown : ${hours.toString().padStart(2, '0')}H ` +
                `${minutes.toString().padStart(2, '0')}M ` +
                `${seconds.toString().padStart(2, '0')}S`;

            totalSeconds--;
        }

    </script>

</asp:Content>

