<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Wallet.aspx.cs" Inherits="Wallet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style>
        .box_style {
            padding: 15px;
            border: 1px solid #efefef;
            border-radius: 10px;
            box-shadow: 1px 2px 6px #80808099;
        }

        input[type="checkbox"], input[type="radio"] {
            box-sizing: border-box;
            padding: 0;
            margin-right: 5px;
        }

        .available_balace_title{
            font-size: 20px;
        }
        
        .available_balace_amount{
            font-size: 30px
        }

        .view_transaction{
            font-size: 17px !important;
            background-color: white !important;
            border: 0px !important;
            font-weight: 400 !important;
            cursor: pointer;
        }

        .withdrawal_setting_title{
            font-size: 20px
        }

        .radio_button_list{
            font-size: 14px; font-weight: 500;
        }

        .withdrawal_amount_title{
            font-size: 18px;
        }
        
        .withdrawal_amount_title{
            font-size: 20px;
        }

        .remark{
            font-size: 12px;
        }

        .btn_update_class{
            background-color: #149474 !important; color: white !important;
        }

        .Payout_balance_title{
            font-size: 18px;
        }

        .Payout_balance{
            border: 1px solid black; padding: 10px; font-size: 22px;
        }

        .bonus_list{
            font-size: 16px; line-height: 1.9;
        }

        @media only screen and (max-width: 767px) {
            .payout_div{
                margin-top:1.5rem;
            }
        }

    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="pf-details mt-4">
        <div class="container">
            <div class="row text-left mb-3">
                <div class="col-12 d-inline-flex">
                    <p class="membership_title fw-200">Bonus - </p>
                    &nbsp;<p class="membership_title fw-500">Wallet</p>
                </div>
            </div>
            <hr />

            <div class="row mb-5">
                <div class="col-12 col-lg-6">
                    <div class="box_style">
                        <div class="col-12">
                            <span class="available_balace_title">Current Cash Balance</span>
                        </div>
                        <div class="col-12 mt-4 mb-4">
                            <asp:Label runat="server" ID="lbl_wallet_amount" CssClass="available_balace_amount"></asp:Label>
                        </div>
                        <div class="col-12">
                            <asp:Button runat="server" Text="Transaction >" ID="btn_view_transaction" OnClick="btn_view_transaction_Click" CssClass="view_transaction" />
                        </div>
                    </div>

                    <div class="box_style mt-4">
                        <div class="col-12">
                            <span class="withdrawal_setting_title">Withdrawal Setting</span>
                        </div>
                        <div class="col-12 mt-4 mb-4 radio_button_list">
                            <asp:RadioButtonList runat="server" ID="rbtn_withdrawal_setting">
                                <asp:ListItem Value="Withdraw_All">Withdraw all</asp:ListItem>
                                <asp:ListItem Value="Amount_Per_Transaction">Amount per transaction</asp:ListItem>
                                <asp:ListItem Value="No_Withdrawl">No withdrawal</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div class="col-12 mt-2 mb-2">
                            <span class="withdrawal_amount_title">Withdrawal Amount</span>
                        </div>
                        <div class="col-12">
                            <asp:TextBox runat="server" ID="txt_withdrawal_amount" CssClass="w-100 form-control p-2" PlaceHolder="Amount" onkeypress='return event.charCode >= 48 && event.charCode <= 57' MaxLength="5"></asp:TextBox>
                        </div>
                        <div class="col-12 mt-2 mb-2 remark">
                            <span>- Minimum withdrawal is RM 50</span><br />
                            <span>- A withdrawal transaction will incur an RM 1 charge to cover bank transaction fees and administrative costs.</span>
                        </div>

                        <div class="col-12 text-right mt-3">
                            <asp:Button ID="btn_Update" runat="server" OnClick="btn_Update_Click" CssClass="btn btn_update_class" Text="Update" />
                        </div>
                    </div>
                </div>

                <div class="col-12 col-lg-6 payout_div">
                    <div class="box_style">
                        <div class="col-12">
                            <span class="Payout_balance_title">Payout Maturity</span>
                        </div>
                        <div class="col-12 mt-4 mb-4 Payout_balance_title">
                            <span>Payout Maturity in 14 days</span>
                        </div>
                        <div class="col-12 mt-4 mb-4">
                            <asp:Label runat="server" ID="lbl_total_14days_amount" CssClass="Payout_balance">RM 0.00</asp:Label>
                        </div>

                        <div class="col-12">
                            <ul class="ml-3 bonus_list">
                                <asp:Repeater runat="server" ID="rpt_bonus" OnItemDataBound="rpt_bonus_ItemDataBound">
                                    <ItemTemplate>
                                        <li>
                                            <span runat="server" id="lbl_day"></span>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <script type="text/javascript">
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

