<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DC_History.aspx.cs" Inherits="DC_History" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style>
       .table tr, .table td, .table th{
            text-align:center;
        }

       .btn_view{
            border: 1px solid #efefef;
            padding: 10px;
            border-radius: 5px;
            background-color: #e2e5e8;
       }

            .btn_view:hover {
                background-color: #007bff; /* Change this to the desired hover color */
                color: #ffffff; /* Change this to the desired text color on hover */
            }

        table {
            width: auto; /* Allow table to expand to maximum content width */
            border-collapse: collapse; /* Collapse borders to ensure consistent spacing */
        }

        @media (max-width: 768px) {

            .table-container {
                overflow: auto;
            }
        }

    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <section class="pf-details mt-5">
        <div class="container">
            <div class="row text-left mb-3">
                <div class="col-12 d-inline-flex">
                    <p class="membership_title fw-200">Order - </p>
                    &nbsp;<p class="membership_title fw-500">DC History</p>
                </div>
            </div>

            <div class="row mb-5">
                <div class="col-12 table-container">
                    <table class="table">
                        <tr>
                            <th>Date</th>
                            <th>Member ID</th>
                            <th>Order No</th>
                            <th>DC C/F</th>
                            <th>DC Used</th>
                            <th>DC Earn</th>
                            <th>DC Balance</th>
                            <th>Status</th>
                        </tr>
                        <tbody>
                            <asp:Repeater runat="server" ID="rpt_dc_history" OnItemDataBound="rpt_dc_history_ItemDataBound">
                                <ItemTemplate>
                                    <tr>
                                        <td style="width: 12%; vertical-align: middle;">
                                            <asp:Label runat="server" ID="lbl_date"></asp:Label></td>
                                        <td style="width: 10%; vertical-align: middle;">
                                            <asp:Label runat="server" ID="lbl_member_id"></asp:Label></td>
                                        <td style="width: 15%; vertical-align: middle;">
                                            <asp:Label runat="server" ID="lbl_order_no"></asp:Label></td>
                                        <td style="width: 15%; vertical-align: middle;">
                                            <asp:Label runat="server" ID="lbl_dc_cw"></asp:Label></td>
                                        <td style="width: 15%; vertical-align: middle;">
                                            <asp:Label runat="server" ID="lbl_dc_used"></asp:Label></td>
                                        <td style="width: 15%; vertical-align: middle;">
                                            <asp:Label runat="server" ID="lbl_dc_earn"></asp:Label></td>
                                        <td style="width: 15%; vertical-align: middle;">
                                            <asp:Label runat="server" ID="lbl_dc_balance"></asp:Label></td>
                                        <td style="width: 15%; vertical-align: middle;">
                                            <asp:Label runat="server" ID="lbl_status"></asp:Label></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                        <tr runat="server" id="tr_no_record" visible="false" class="w-100 text-center">
                            <td colspan="8">No record found.</td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </section>

</asp:Content>

