<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Policy.aspx.cs" Inherits="Policy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style>

        .policy-title {
            font-size: 24px;
            font-weight: bold;
            margin-bottom: 20px;
        }

        ul li{
            margin-left: 3rem;
            margin-top: 1rem;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="container p-4" style="margin-bottom: 20rem;">
        <div class="row text-left mt-3 mb-4">
            <div class="col-12 d-inline-flex">
                <p class="membership_title fw-200">Support - </p>
                &nbsp;<p class="membership_title fw-500" runat="server" id="lbl_policy_header"></p>
            </div>
        </div>

        <h2 class="policy-title text-center" runat="server" id="lbl_policy_title"></h2>
        <hr>
        <div class="policy-content">
            <p runat="server" id="lbl_policy_content"></p>
        </div>
    </div>

</asp:Content>

