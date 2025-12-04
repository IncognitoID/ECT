<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Ipay88_Result_Upgrade_Level.aspx.cs" Inherits="Ipay88_Result_Upgrade_Level" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style>
        body {
            text-align: center;
            background: #EBF0F5;
        }

        h2 {
            color: #88B04B;
            font-family: "Nunito Sans", "Helvetica Neue", sans-serif;
            font-weight: 900;
            font-size: 30px;
            margin-bottom: 10px;
        }

        .success_text {
            color: #404F5E;
            font-family: "Nunito Sans", "Helvetica Neue", sans-serif;
            font-size: 20px;
            margin: auto;
            width: 100%;
        }
        
        .failed_text {
            color: #404F5E;
            font-family: "Nunito Sans", "Helvetica Neue", sans-serif;
            font-size: 20px;
            margin: auto;
            width: 55%;
        }

        i {
            color: #9ABC66;
            font-size: 100px;
            line-height: 200px;
            margin-left: -15px;
        }

        .card {
            background: white;
            padding: 60px;
            border-radius: 4px;
            box-shadow: 0 2px 3px #C8D0D8;
            display: inline-block;
            margin: 0 auto;
        }

        .btn_continue {
            border: 1px solid #efefef;
            padding: 20px 100px;
            border-radius: 80px;
            background-color: red;
            color: white !important;
            font-weight: bold;
        }

        .frame_icon {
            border-radius: 200px;
            height: 200px;
            width: 200px;
            background: #F8FAF5;
            margin: 0 auto;
            margin-bottom: 1rem;
        }

        @media only screen and (max-width: 700px) {
            .failed_text {
                color: #404F5E;
                font-family: "Nunito Sans", "Helvetica Neue", sans-serif;
                font-size: 20px;
                margin: auto;
                width: 80%;
            }
        }

        @media only screen and (max-width: 600px) {

            .failed_card {
                background: white;
                padding: 45px;
                border-radius: 4px;
                box-shadow: 0 2px 3px #C8D0D8;
                display: inline-block;
                margin: 0 auto;
            }

            .failed_text {
                color: #404F5E;
                font-family: "Nunito Sans", "Helvetica Neue", sans-serif;
                font-size: 20px;
                margin: auto;
                width: 100%;
            }

            .frame_icon {
                border-radius: 200px;
                height: 150px;
                width: 150px;
                background: #F8FAF5;
                margin: 0 auto;
                margin-bottom: 1rem;
            }

            i {
                color: #9ABC66;
                font-size: 70px;
                line-height: 150px;
                margin-left: -15px;
            }
        }

        @media only screen and (max-width: 450px) {

            .card {
                background: white;
                padding: 40px;
                border-radius: 4px;
                box-shadow: 0 2px 3px #C8D0D8;
                display: inline-block;
                margin: 0 auto;
            }

            .failed_card {
                background: white;
                padding: 40px 0px;
                border-radius: 4px;
                box-shadow: 0 2px 3px #C8D0D8;
                display: inline-block;
                margin: 0 auto;
            }

            h2 {
                color: #88B04B;
                font-family: "Nunito Sans", "Helvetica Neue", sans-serif;
                font-weight: 900;
                font-size: 23px;
                margin-bottom: 10px;
            }

            .failed_text {
                color: #404F5E;
                font-family: "Nunito Sans", "Helvetica Neue", sans-serif;
                font-size: 17px;
                margin: auto;
                width: 100%;
                padding: 0px 30px;
            }

            .frame_icon {
                border-radius: 200px;
                height: 150px;
                width: 150px;
                background: #F8FAF5;
                margin: 0 auto;
                margin-bottom: 1rem;
            }

            i {
                color: #9ABC66;
                font-size: 70px;
                line-height: 150px;
                margin-left: -15px;
            }

            .btn_continue {
                border: 1px solid #efefef;
                padding: 12px 70px;
                border-radius: 80px;
                background-color: red;
                color: white !important;
                font-weight: bold;
            }
        }

        @media only screen and (max-width: 325px) {
            .card {
                background: white;
                padding: 30px;
                border-radius: 4px;
                box-shadow: 0 2px 3px #C8D0D8;
                display: inline-block;
                margin: 0 auto;
            }
        }

        @media only screen and (max-width: 300px) {

            .failed_card {
                background: white;
                padding: 40px 0px;
                border-radius: 4px;
                box-shadow: 0 2px 3px #C8D0D8;
                display: inline-block;
                margin: 0 auto;
            }

            .failed_text {
                color: #404F5E;
                font-family: "Nunito Sans", "Helvetica Neue", sans-serif;
                font-size: 13px;
                margin: auto;
                width: 80%;
                padding: 0px;
            }

            .frame_icon {
                border-radius: 200px;
                height: 130px;
                width: 130px;
                background: #F8FAF5;
                margin: 0 auto;
                margin-bottom: 1rem;
            }

            i {
                color: #9ABC66;
                font-size: 60px;
                line-height: 125px;
                margin-left: -10px;
            }

            h2 {
                color: #88B04B;
                font-family: "Nunito Sans", "Helvetica Neue", sans-serif;
                font-weight: 900;
                font-size: 20px;
                margin-bottom: 10px;
            }

            .btn_continue {
                border: 1px solid #efefef;
                padding: 10px 50px;
                border-radius: 80px;
                background-color: red;
                color: white !important;
                font-weight: bold;
                font-size: 12px;
            }
        }
    </style>
    <link href="https://fonts.googleapis.com/css?family=Nunito+Sans:400,400i,700,900&display=swap" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="card mt-5 mb-5" id="div_success" runat="server">
            <div class="frame_icon">
                <i class="checkmark">✓</i>
            </div>
            <h2>Payment Successful</h2>
            <p class="success_text">Successful upgrade to level eo.</p>
            <br />
            <br />
            <a class="btn_continue" href="https://ecentra.com.my/">Continue</a>
            <br />
        </div>

        <div class="failed_card mt-5 mb-5" id="div_failed" runat="server">
            <div class="frame_icon">
                <i class="closemark">✗</i>
            </div>
            <h2>Payment Failed</h2>
            <p class="failed_text">Please make payment again or cancel the order in order history.</p>
            <br />
            <br />
            <a class="btn_continue" href="https://ecentra.com.my/">Continue</a>
            <br />
        </div>
    </div>

</asp:Content>

