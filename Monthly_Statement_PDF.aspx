<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Monthly_Statement_PDF.aspx.cs" Inherits="Monthly_Statement_PDF" %>

<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Account Statement</title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet">
    <style>
        .header img {
            max-width: 160px;
        }

        .summary-table, .transactions-table {
            margin-top: 20px;
        }

            .transactions-table th {
                background-color: #f8f9fa;
            }

        .footer {
            margin-top: 20px;
            font-size: 0.9em;
            text-align: center;
        }

        .ver-align-center {
            vertical-align: middle !important;
        }

        @media print {
            #closebtn {
                display: none;
            }

            table {
                page-break-inside: auto;
            }

            thead {
                display: table-header-group;
            }

            tfoot {
                display: table-row-group;
            }

            tr {
                page-break-inside: avoid;
            }

            .no-print {
                display: none;
            }

            @page {
                margin-top: 20px;
            }

            body {
                margin: 1.6cm;
            }
        }
    </style>
</head>

<body>
    <form runat="server">

        <div class="container my-5">

            <div id="closebtn" class="no-print" style="width: 75%; left: 50%; margin-left: -37.5%; position: fixed; text-align: center; margin-top: 5px;">
                <button onclick="printfunction();" class="btn btn-primary btn-sm">Print</button>
            </div>

            <div class="header text-left mb-4">
                <img src="img/Logo/new_logo2.png" alt="Logo">
                <address>
                    E-12A-2, The Icon Tower (East Wing),<br>
                    Level 12A, No.1, Jalan 1/ 68F,<br>
                    Jalan Tun Razak, 50400 K.L .<br>
                    <a>cs@ecentra.com.my</a><br>
                    +603-2181 3118
                </address>
            </div>

            <h1 class="text-center">Monthly Statement</h1>

            <div class="account-info mt-4">
                <p><strong>Member ID:</strong>
                    <label runat="server" id="lbl_member_id"></label>
                </p>
                <p><strong>Member Name :</strong>
                    <label runat="server" id="lbl_member_name"></label>
                </p>
                <p><strong>Period:</strong>
                    <label runat="server" id="lbl_period"></label>
                </p>
            </div>

            <div class="transactions-table">
                <h2>Transactions</h2>
                <table class="table table-bordered">
                    <thead>
                        <tr>
                            <th class="text-center ver-align-center">Date</th>
                            <th>Referral Incentive
                                <br />
                                (RI)</th>
                            <th>Branch Bonus
                                <br />
                                (BB) CYCLE-1</th>
                            <th>Branch Bonus
                                <br />
                                (BB) CYCLE-2</th>
                            <th>Income Booster
                                <br />
                                (IB)</th>
                            <th>Special Development
                                <br />
                                Bonus (SDB)</th>
                            <th class="text-center ver-align-center">Total</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater runat="server" ID="rpt_daily_statement" OnItemDataBound="rpt_daily_statement_ItemDataBound">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_date" runat="server"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lbl_direct_profit" runat="server"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lbl_bb_cycle_1" runat="server"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lbl_bb_cycle_2" runat="server"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lbl_income_booster" runat="server"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lbl_special_development_bonus" runat="server"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lbl_total_bonus" runat="server"></asp:Label></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                    <tfoot>
                        <tr>
                            <th colspan="6">Monthly Totals</th>
                            <th>
                                <asp:Label runat="server" ID="lbl_monthly_sales">RM 0.00</asp:Label></th>
                        </tr>
                    </tfoot>
                </table>
            </div>

            <div class="footer">
                <p>If you have found errors or have questions about your statement, please contact ECENTRA via email at <a href="cs@ecentra.com.my">cs@ecentra.com.my</a>, by phone at +603-2181 3118.</p>
            </div>
        </div>

        <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
        <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.2/dist/umd/popper.min.js"></script>
        <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>

        <script type="text/javascript">
            function printfunction() {
                window.print()
            }
        </script>

    </form>

</body>

</html>

