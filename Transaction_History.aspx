<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Transaction_History.aspx.cs" Inherits="Transaction_History" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style>
        .table tr, .table td, .table th {
            text-align: center;
        }

        .btn_view {
            border: 1px solid #efefef;
            padding: 7px 15px;
            border-radius: 5px;
            background-color: #e2e5e8;
        }

            .btn_view:hover {
                background-color: #007bff; /* Change this to the desired hover color */
                color: #ffffff; /* Change this to the desired text color on hover */
            }

        .pagination {
            margin: 0px;
            display: inline-flex;
            border: 1px solid #efefef;
        }

        .page-link {
            position: relative;
            display: block;
            padding: 0.5rem 0.75rem;
            margin-left: -1px;
            line-height: 1.25;
            color: black;
            background-color: white;
            border: 1px solid #dee2e6;
        }

            .page-link.active {
                z-index: 1;
                color: #fff;
                background-color: #007bff;
                border-color: #007bff;
            }

        .page-item:last-child .page-link {
            border-radius: 0px;
        }

        .page-item:first-child .page-link {
            border-radius: 0px;
        }

        @media (max-width: 768px) {
            .flex-mobile{
                justify-content: normal;
            }
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="pf-details mt-4">
        <div class="container">

            <div class="row">
                <div class="col-xl-6 col-lg-6 col-md-12 col-sm-12 col-12">
                    <div class="row align-items-center">
                        <div class="col-xl-2 col-lg-2 col-md-2 col-sm-2 col-12"><span id="lbl_121">Filter By:</span></div>
                        <div class="col-xl-5 col-lg-5 col-md-5 col-sm-5 col-5">
                            <asp:TextBox runat="server" ID="txt_start_date" class="filter-date form-control" TextMode="Date"></asp:TextBox>
                        </div>
                        <div class="col-xl-1 col-lg-1 col-md-1 col-sm-1 col-2 text-center "><span id="lbl_122">to</span></div>
                        <div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-5">
                            <asp:TextBox runat="server" ID="txt_end_date" class="filter-date form-control" TextMode="Date"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-xl-6 col-lg-6 col-md-12 col-sm-12 col-12 mt-2 mt-xl-0">
                    <asp:LinkButton runat="server" ID="btn_seach" Text="Search" CssClass="btn_view" OnClick="btn_seach_Click" Style="float: right;"></asp:LinkButton>
                </div>
            </div>

            <hr />

            <div class="row text-left mb-3">
                <div class="col-12 d-inline-flex">
                    <p class="membership_title fw-200">Wallet - </p>
                    &nbsp;<p class="membership_title fw-500">Wallet Transaction History</p>
                </div>
            </div>

            <hr />

            <div id="div_ddl" class="row text-right">
                <div class="col-xl-9 col-lg-9 col-md-0 col-sm-0 col-0 d-flex"></div>
                <div class="col-xl-3 col-lg-3 col-md-12 col-sm-12 col-12 d-flex align-items-end text-right">
                    <label class="mb-1 w-100 text-center">Page Size</label>
                    <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                        <asp:ListItem Text="10" Value="10" />
                        <asp:ListItem Text="25" Value="25" />
                        <asp:ListItem Text="50" Value="50" />
                    </asp:DropDownList>
                </div>
            </div>

            <br />

            <div class="row mb-5">
                <div class="col-12 mb-3" style="overflow:scroll; display:block; width:100%;">

                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <asp:GridView runat="server" ID="grd_wallet_transaction_history" class="table table-bordered table-condensed table-responsive table-hover" AutoGenerateColumns="false" OnRowDataBound="grd_wallet_transaction_history_RowDataBound" EmptyDataText="No Record Found">
                                <Columns>
                                    <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle CssClass="tablerRestColumn"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_transaction_date" runat="server" Text='<%# Eval("Created_Date", "{0:dd-MM-yyyy}") %>'></asp:Label>
                                            <br />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Opening Balance" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle CssClass="tablerRestColumn"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# Eval("Wallet_Balance", "{0:###,###,###,##0.00}") %>'></asp:Label>
                                            <br />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Credit" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle CssClass="tablerRestColumn"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# Eval("Wallet_Balance_In", "{0:###,###,###,##0.00}") %>'></asp:Label>
                                            <br />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Debit" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle CssClass="tablerRestColumn"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# Eval("Wallet_Balance_Out", "{0:###,###,###,##0.00}") %>'></asp:Label>
                                            <br />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transaction Fees" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle CssClass="tablerRestColumn"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# Eval("Wallet_Transaction_Fees", "{0:###,###,###,##0.00}") %>'></asp:Label>
                                            <br />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Closing Balance" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle CssClass="tablerRestColumn"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# Eval("Final_Wallet_Balance", "{0:###,###,###,##0.00}") %>'></asp:Label>
                                            <br />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remark" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle CssClass="tablerRestColumn"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# Eval("Wallet_Description") %>'></asp:Label>
                                            <br />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle CssClass="tablerRestColumn"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_status" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                            <br />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <nav aria-label="Page navigation example">
                        <ul class="pagination">
                            <asp:Repeater ID="rpt_daily_pager" runat="server">
                                <ItemTemplate>
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <li class="page-item">
                                                <asp:LinkButton ID="btn_page" runat="server" CssClass="page-link" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' OnClientClick="return setPageActive(this);" OnClick="btn_page_Click"></asp:LinkButton>
                                            </li>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>

                        <hr />

                        <div class="col-12 text-right">
                            <div onclick="BackToPreviousPage()">
                                <label style="padding: 7px 15px; background-color: #232020; color: white; border-radius: 5px;">Back</label>
                            </div>
                        </div>
                    </nav>

                </div>
            </div>
        </div>
    </section>

    <script>
        function BackToPreviousPage() {
            window.location.href = "Wallet.aspx";
        }

        function setPageActive(clickedButton) {
            // Reset the "active" class for all page items
            var pageItems = document.querySelectorAll('.pagination .page-item');
            pageItems.forEach(function (item) {
                item.classList.remove('active');
            });

            // Set the "active" class for the clicked page item
            clickedButton.parentElement.classList.add('active');

            return true;
        }
    </script>

</asp:Content>

