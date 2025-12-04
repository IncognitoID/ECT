<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Purchase_History.aspx.cs" Inherits="Purchase_History" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style>
        .table tr, .table td, .table th {
            text-align: center;
            vertical-align: middle !important;
        }

        .btn_view {
            border: 1px solid #efefef;
            padding: 10px;
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

        table {
            width: auto; /* Allow table to expand to maximum content width */
            border-collapse: collapse; /* Collapse borders to ensure consistent spacing */
        }

        @media (max-width: 768px) {

            .table-container {
                overflow: auto;
            }

            .responsive-table {
                display: block;
                width: 100%;
                overflow-x: auto;
                white-space: nowrap;
            }

            /* Ensure the first column is visible */
            .responsive-table td:first-child,
            .responsive-table th:first-child {
                min-width: 50px;
                max-width: 60px;
                text-align: center;
            }

            /* Expand/collapse icon styling */
            .expand-container {
                display: flex;
                align-items: center;
                justify-content: center;
                width: 40px;
                min-width: 40px;
            }

            .expand-icon, .collapse-icon {
                width: 20px;
                cursor: pointer;
                display: inline-block;
            }

            .btn_download{
                display: ruby-text;
            }
        }

        .responsive-table {
            width: 100%;
            border-collapse: collapse;
        }

        .responsive-table td, .responsive-table th {
            padding: 12px;
            text-align: center;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="pf-details mt-4">
        <div class="container">

            <div class="row text-left mb-3">
                <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12 d-inline-flex">
                    <p class="membership_title fw-200">Order - </p>
                    &nbsp;<p class="membership_title fw-500">Purchase History</p>
                </div>
            </div>

            <hr />

            <div id="div_ddl" class="row text-right">
                <div class="col-xl-9 col-lg-9 col-md-0 col-sm-0 col-0 d-flex"></div>
                <div class="col-xl-3 col-lg-3 col-md-12 col-sm-12 col-12 d-flex align-items-end text-right">
                    <label class="mb-1 w-100 text-center">Page Size</label>
                    &nbsp;
                    <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                        <asp:ListItem Text="10" Value="10" />
                        <asp:ListItem Text="25" Value="25" />
                        <asp:ListItem Text="50" Value="50" />
                    </asp:DropDownList>
                </div>
            </div>

            <br />

            <div class="row mb-5">
                <div class="col-12 mb-3" style="overflow:scroll;">

                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <asp:GridView runat="server" ID="grd_order_history" class="table table-bordered table-condensed table-responsive table-hover" AutoGenerateColumns="false" OnRowDataBound="grd_order_history_RowDataBound" OnRowCommand="grd_order_history_RowCommand" EmptyDataText="No Record Found">
                                <Columns>
                                    <asp:TemplateField HeaderText="Member ID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle CssClass="tablerRestColumn"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_member_id" runat="server" Text='<%# Eval("MemberID") %>'></asp:Label>
                                            <br />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle CssClass="tablerRestColumn"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_order_date" runat="server" Text='<%# Eval("Order_Date", "{0:dd-MM-yyyy}") %>'></asp:Label>
                                            <br />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle CssClass="tablerRestColumn"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_order_no" runat="server" Text='<%# Eval("Order_No") %>'></asp:Label>
                                            <br />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Payment Method" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle CssClass="tablerRestColumn"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_payment_method" runat="server" Text='<%# Eval("Payment_Type") %>'></asp:Label>
                                            <br />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Amount" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle CssClass="tablerRestColumn"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_total_amount" runat="server" Text='<%# "RM " + Eval("Payment_Amt", "{0:###,###,###,##0.00}") %>'></asp:Label>
                                            <br />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Shipping Type" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle CssClass="tablerRestColumn"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_shipping_type" runat="server" Text='<%# Eval("Delivery_Service") %>'></asp:Label>
                                            <br />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle CssClass="tablerRestColumn"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_status" runat="server" Text='<%# Eval("Order_Status") + ", " + Eval("Delivery_Service") %>'></asp:Label>
                                            <br />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Download Invoice" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <div class="row w-100">
                                                <div class="col-12">
                                                    <asp:Label runat="server" ID="lbl_disabled">-</asp:Label>
                                                    <asp:LinkButton runat="server" ID="btn_download" Text="Download Invoice" CssClass="btn_view btn_download" CommandName="Download" CommandArgument='<%# Eval("Order_No") %>'></asp:LinkButton>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle CssClass="tablerRestColumn"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="btn_view" Text="View" CssClass="btn_view" CommandName="View" CommandArgument='<%# Eval("Order_No") %>'></asp:LinkButton>
                                            <br />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                            <div class="modal fade" id="Invoice_Modal" role="dialog">
                                <div class="modal-dialog modal-lg">
                                    <div class="modal-content" id="iframemodal">
                                        <div class="modal-header" style="border: none; background-color: #f1f1f1">
                                            <h4 class="modal-title">Invoice</h4>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="true">&times;</span>
                                            </button>
                                        </div>
                                        <iframe id="InvoiceFrame" style="height: 600px; width: 100%" runat="server"></iframe>
                                    </div>
                                </div>
                            </div>
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
                    </nav>

                </div>
            </div>
        </div>
    </section>

    <script src="js/jquery-3.6.0.min.js"></script>

    <script>
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

        function ViewInvoiceModal() {
            $('#Invoice_Modal').modal('show');
        }
    </script>

</asp:Content>

