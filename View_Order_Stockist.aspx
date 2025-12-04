<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="View_Order_Stockist.aspx.cs" Inherits="View_Order_Stockist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style>
        .table tr, .table td, .table th {
            text-align: center;
        }

        .btn_view {
            border: 1px solid #efefef;
            padding: 5px;
            border-radius: 5px;
            background-color: #e2e5e8;
        }

        hr {
            background-color: #ccc;
            border: 0;
            height: 1px;
            margin-bottom: 1em;
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
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="pf-details mt-4">
        <div class="container">

            <!-- Modal -->
            <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="exampleModalLabel">Filter</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="form-group row pt-0">
                                <label for="inputname" class="col-12 col-form-label" id="lbl_395">Search</label>
                                <div class="col-12">
                                    <input type="text" class="form-control" ng-model="name" placeholder="Search orders no / member id" runat="server" id="txt_search">
                                </div>
                            </div>
                            <div class="form-group row pt-0">
                                <label for="inputDate" class="col-12 col-form-label" id="lbl_396">Date From</label>
                                <div class="col-12">
                                    <input type="date" class="form-control" ng-model="dob" runat="server" id="txt_start_date">
                                </div>
                            </div>
                            <div class="form-group row pt-0">
                                <label for="inputDate" class="col-12 col-form-label" id="lbl_397">Date To</label>
                                <div class="col-12">
                                    <input type="date" class="form-control" ng-model="dob" runat="server" id="txt_end_date">
                                </div>
                            </div>
                            <div class="form-group row pt-0">
                                <label for="inputPosition" class="col-12 col-form-label" id="lbl_398">Delivery Service</label>
                                <div class="col-12">
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddl_delivery_service">
                                        <asp:ListItem Text="All Delivery Service" Value="%%"></asp:ListItem>
                                        <asp:ListItem Text="Delivery" Value="Delivery"></asp:ListItem>
                                        <asp:ListItem Text="Self Pickup" Value="Self Pickup"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group row pt-0">
                                <label for="inputPosition" class="col-12 col-form-label" id="lbl_399">Order Status</label>
                                <div class="col-12">
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddl_status">
                                        <asp:ListItem Text="All Status" Value="%%"></asp:ListItem>
                                        <asp:ListItem Text="To Pay" Value="To Pay"></asp:ListItem>
                                        <asp:ListItem Text="To Ship" Value="To Ship"></asp:ListItem>
                                        <asp:ListItem Text="Collect at HQ" Value="Collect at HQ"></asp:ListItem>
                                        <asp:ListItem Text="To Receive" Value="To Receive"></asp:ListItem>
                                        <asp:ListItem Text="Completed" Value="Completed"></asp:ListItem>
                                        <asp:ListItem Text="Cancelled" Value="Cancelled"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button runat="server" ID="btn_search" CssClass="btn btn-primary w-100" Text="Search" OnClick="btn_search_Click" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="row text-left mb-3">
                <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12 d-inline-flex">
                    <p class="membership_title fw-200" id="lbl_393">Mobile Stockist - </p>
                    &nbsp;<p class="membership_title fw-500" id="lbl_394">Order History</p>
                </div>
            </div>

            <hr />

            <div id="div_ddl" class="row text-right">
                <div class="col-xl-8 col-lg-8 col-md-0 col-sm-0 col-0 d-flex"></div>
                <div class="col-xl-4 col-lg-4 col-md-6 col-sm-6 col-6 d-flex align-items-end text-right">
                    <label class="mb-1 w-100 text-center" id="lbl_401">Page Size</label>
                    &nbsp;
                    <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                        <asp:ListItem Text="10" Value="10" />
                        <asp:ListItem Text="25" Value="25" />
                        <asp:ListItem Text="50" Value="50" />
                    </asp:DropDownList>
                    <div class="form-control" data-toggle="modal" data-target="#exampleModal" style="width: 200px; text-align: center;"><i class='bx bx-search' style="color: #3a3030;"></i><span id="lbl_402">Filter</span></div>
                </div>
            </div>

            <br />

            <div class="row mb-5">
                <div class="col-12 mb-3" style="overflow: scroll;">

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
                                    <%--<asp:TemplateField HeaderText="Download" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle CssClass="tablerRestColumn"></ItemStyle>
                                        <ItemTemplate>
                                            <br />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle CssClass="tablerRestColumn"></ItemStyle>
                                        <ItemTemplate>
                                            <div class="row">
                                                <div class="col-12">
                                                    <asp:LinkButton runat="server" ID="btn_view" Text="View Details" CssClass="btn_view" CommandName="View" CommandArgument='<%# Eval("Order_No") %>'></asp:LinkButton>
                                                </div>
                                            </div>
                                            <hr />
                                            <div class="row">
                                                <div class="col-12">
                                                    <asp:LinkButton runat="server" ID="btn_download" Text="Download Invoice" CssClass="btn_view" CommandName="Download" CommandArgument='<%# Eval("Order_No") %>'></asp:LinkButton>
                                                </div>
                                            </div>
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

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

    <script>
        $(document).ready(function () {
            Load_Language();
        });

        function Load_Language() {
            var page = 'Stockist Order';
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
                        } else if (item.Label_Type === 'Button') {
                            var element = document.getElementById(item.Label_Name);
                            if (element) {
                                element.textContent = item.Language_Content;
                            }
                        } else if (item.Label_Type === 'New_Button') {
                            var element = document.getElementById('ContentPlaceHolder1_' + item.Label_Name);
                            if (element) {
                                element.textContent = item.Language_Content;
                            }
                        } else if (item.Label_Type === 'LinkButton') {
                            var element = document.getElementById('ContentPlaceHolder1_' + item.Label_Name);
                            if (element) {
                                element.value = item.Language_Content;
                            }
                        } else {
                            window[item.Label_Name] = item.Language_Content;
                        }
                    });
                }
            });
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

        function ViewInvoiceModal() {
            $('#Invoice_Modal').modal('show');
        }
    </script>

</asp:Content>

