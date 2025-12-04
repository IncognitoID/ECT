<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Statement.aspx.cs" Inherits="Statement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style>
        .table tr, .table td, .table th {
            text-align: center;
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

        .btn_confirm {
            border: 1px solid #efefef;
            padding: 10px;
            border-radius: 5px;
            background-color: #7efa6d;
        }

            .btn_confirm:hover {
                background-color: #007bff; /* Change this to the desired hover color */
                color: #ffffff; /* Change this to the desired text color on hover */
            }

        .table thead th {
            vertical-align: middle;
            border-bottom: 2px solid #e9ecef;
            white-space: nowrap;
        }

        .table td, .table th {
            padding: 0.75rem;
            vertical-align: middle;
            border-top: 1px solid #e9ecef;
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

        .flex-mobile{
            justify-content: flex-end;
        }

        @media (max-width: 768px) {
            .flex-mobile{
                justify-content: normal;
            }
        }
    </style>

    <script type="text/javascript">
        function View_Statemnt_Modal() {
            $('#Statement_Modal').modal('show');
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="pf-details mt-5">
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
                <div class="col-xl-6 col-lg-6 col-md-12 col-sm-12 col-12 d-inline-flex">
                    <p class="membership_title fw-200">Bonus - </p>
                    &nbsp;<p class="membership_title fw-500">Statement - </p>
                    &nbsp;<p class="membership_title fw-500" id="daily_title">Daily Bonus</p>
                    <p class="membership_title fw-500" id="monthly_title">Monthly Bonus</p>
                </div>
                <div class="col-xl-6 col-lg-6 col-md-12 col-sm-12 col-12 d-inline-flex flex-mobile">
                    <label style="font-size: 16px; margin-right: 2rem;">
                        <input id="radio_daily" type="radio" name="frequency" value="daily" checked="checked">&nbsp;Daily Bonus</label>
                    <label style="font-size: 16px;">
                        <input id="radio_monthly" type="radio" name="frequency" value="monthly">&nbsp;Monthly Bonus</label>
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
                        <asp:ListItem Text="100" Value="100" />
                    </asp:DropDownList>
                </div>
            </div>

            <br />

            <div class="row mb-5">
                <div class="col-12 mb-3">

                    <div id="div_daily">

                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                               <asp:GridView runat="server" ID="grd_daily_record" class="table table-bordered table-condensed table-responsive table-hover" AutoGenerateColumns="false" OnRowDataBound="grd_daily_record_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemStyle CssClass="tablerRestColumn"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_date" runat="server" Text='<%# Eval("ReportDate", "{0:dd-MM-yyyy}") %>'></asp:Label>
                                                <br />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Referral Incentive <br /> (RI)" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemStyle CssClass="tablerRestColumn"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_direct_profit" runat="server" Text='<%# Eval("Total Direct Profit", "{0:###,###,###,##0.00}") %>'></asp:Label>
                                                <br />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Branch Bonus <br /> (BB) CYCLE-1" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemStyle CssClass="tablerRestColumn"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_bb_cycle_1" runat="server" Text='<%# Eval("Total BB Cycle 1", "{0:###,###,###,##0.00}") %>'></asp:Label>
                                                <br />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Branch Bonus <br /> (BB) CYCLE-2" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemStyle CssClass="tablerRestColumn"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_bb_cycle_2" runat="server" Text='<%# Eval("Total BB Cycle 2", "{0:###,###,###,##0.00}") %>'></asp:Label>
                                                <br />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Income Booster <br /> (IB)" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemStyle CssClass="tablerRestColumn"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_income_booster" runat="server" Text='<%# Eval("Total Income Booster", "{0:###,###,###,##0.00}") %>'></asp:Label>
                                                <br />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Special Development <br /> Bonus (SDB)" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemStyle CssClass="tablerRestColumn"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_special_development_bonus" runat="server" Text='<%# Eval("Total SDB", "{0:###,###,###,##0.00}") %>'></asp:Label>
                                                <br />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Bonus <br /> (RM)" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemStyle CssClass="tablerRestColumn"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_total_bonus" runat="server"></asp:Label>
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
                                        <li class="page-item">
                                            <asp:LinkButton ID="btn_page" runat="server" CssClass='<%# Eval("Value") == "" ? "page-link disabled" : "page-link" %>'
                                                Text='<%#Eval("Text") %>'
                                                CommandArgument='<%# Eval("Value") %>'
                                                OnClientClick='<%# Eval("Value") != "" ? "return setPageActive(this);" : "return false;" %>'
                                                OnClick="btn_page_Click"
                                                Enabled='<%# Eval("Value") != "" %>'>
                                            </asp:LinkButton>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </nav>
                    </div>

                    <div id="div_monthly">

                        <asp:GridView runat="server" ID="grd_monthly_record" class="table table-bordered table-condensed table-responsive table-hover" AutoGenerateColumns="false" AllowPaging="True" PageSize="10" OnRowDataBound="grd_monthly_record_RowDataBound" OnRowCommand="grd_monthly_record_RowCommand" OnPageIndexChanging="grd_monthly_record_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="Date" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemStyle CssClass="tablerRestColumn"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_date" runat="server" Text='<%# Eval("ReportDate", "{0:dd-MM-yyyy}") %>'></asp:Label>
                                        <br />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Referral Incentive <br /> (RI)" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemStyle CssClass="tablerRestColumn"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_direct_profit" runat="server" Text='<%# Eval("Total Direct Profit", "{0:###,###,###,##0.00}") %>'></asp:Label>
                                        <br />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Branch Bonus <br /> (BB) CYCLE-1" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemStyle CssClass="tablerRestColumn"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_bb_cycle_1" runat="server" Text='<%# Eval("Total BB Cycle 1", "{0:###,###,###,##0.00}") %>'></asp:Label>
                                        <br />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Branch Bonus <br /> (BB) CYCLE-2" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemStyle CssClass="tablerRestColumn"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_bb_cycle_2" runat="server" Text='<%# Eval("Total BB Cycle 2", "{0:###,###,###,##0.00}") %>'></asp:Label>
                                        <br />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Income Booster <br /> (IB)" HeaderStyle-Width="12%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemStyle CssClass="tablerRestColumn"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_income_booster" runat="server" Text='<%# Eval("Total Income Booster", "{0:###,###,###,##0.00}") %>'></asp:Label>
                                        <br />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Special Development <br /> Bonus (SDB)" HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemStyle CssClass="tablerRestColumn"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_special_development_bonus" runat="server" Text='<%# Eval("Total SDB", "{0:###,###,###,##0.00}") %>'></asp:Label>
                                        <br />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Bonus <br /> (RM)" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemStyle CssClass="tablerRestColumn"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_total_bonus" runat="server"></asp:Label>
                                        <br />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Download" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemStyle CssClass="tablerRestColumn"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="btn_download" Text="Download" CssClass="btn_view" CommandName="Download" CommandArgument='<%# Eval("ReportDate") %>'></asp:LinkButton>
                                        <br />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                        <asp:Repeater ID="rpt_monthly_pager" runat="server">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_page" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' Enabled='<%# Eval("Enabled") %>' OnClick="btn_page_Click"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:Repeater>

                    </div>

                </div>
            </div>
        </div>

                <div class="modal fade" id="Statement_Modal" role="dialog">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content" id="iframemodal">
                            <div class="modal-header" style="border: none; background-color: #f1f1f1">
                                <h4 class="modal-title">Statement</h4>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <iframe id="Statement_Frame" style="height: 600px; width: 100%" runat="server"></iframe>
                        </div>

                    </div>
                </div>
    </section>

    <script type="text/javascript" src="js/jquery-3.2.1.min.js"></script>

    <script type="text/javascript">

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

        document.addEventListener('DOMContentLoaded', function () {
            // Get the radio buttons and tables
            var dailyRadioButton = document.getElementById('radio_daily');
            var monthlyRadioButton = document.getElementById('radio_monthly');
            var daily_title = document.getElementById('daily_title');
            var monthly_title = document.getElementById('monthly_title');
            var dailyTable = document.getElementById('div_daily');
            var monthlyTable = document.getElementById('div_monthly');
            var div_ddl = document.getElementById('div_ddl');

            // Set up an event listener for the radio buttons
            dailyRadioButton.addEventListener('change', function () {
                if (dailyRadioButton.checked) {
                    // Show the daily table and hide the monthly table
                    dailyTable.style.display = 'block';
                    dailyTable.style.width = '100%';
                    dailyTable.style.overflow = 'scroll';
                    monthlyTable.style.display = 'none';
                    daily_title.style.display = 'block';
                    monthly_title.style.display = 'none';
                    div_ddl.style.display = 'flex';
                }
            });

            monthlyRadioButton.addEventListener('change', function () {
                if (monthlyRadioButton.checked) {
                    // Show the monthly table and hide the daily table
                    dailyTable.style.display = 'none';
                    monthlyTable.style.display = 'block';
                    monthlyTable.style.width = '100%';
                    monthlyTable.style.overflow = 'scroll';
                    daily_title.style.display = 'none';
                    monthly_title.style.display = 'block';
                    div_ddl.style.display = 'none';
                }
            });

            // Initial check to determine which radio button is checked on page load
            if (dailyRadioButton.checked) {
                // Show the daily table and hide the monthly table
                dailyTable.style.display = 'block';
                dailyTable.style.width = '100%';
                dailyTable.style.overflow = 'scroll';
                monthlyTable.style.display = 'none';
                daily_title.style.display = 'block';
                monthly_title.style.display = 'none';
                div_ddl.style.display = 'flex';
            } else if (monthlyRadioButton.checked) {
                // Show the monthly table and hide the daily table
                dailyTable.style.display = 'none';
                monthlyTable.style.display = 'block';
                monthlyTable.style.width = '100%';
                monthlyTable.style.overflow = 'scroll';
                daily_title.style.display = 'none';
                monthly_title.style.display = 'block';
                div_ddl.style.display = 'none';
            }
        });
    </script>

</asp:Content>

