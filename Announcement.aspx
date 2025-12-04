<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Announcement.aspx.cs" Inherits="Announcement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="https://fonts.googleapis.com/css?family=Open+Sans:400,700%7CPoppins:400,500" rel="stylesheet">
    <script src="js/jquery-3.3.1.min.js"></script>

    <style>
        section {
            margin: 3%;
            background-color: white;
        }

        .banner-container {
            height: 300px;
            width: 100%;
            overflow: hidden;
            display: flex;
            justify-content: center;
            align-items: center;
        }

        .banner {
            width: 100%;
        }

        .search-sort-container {
            display: flex;
            justify-content: space-between;
        }

        .search-container {
            width: 100%;
        }

        .search-bar {
            padding: 0;
        }

            .search-bar input, .search-bar span {
                height: 35px;
            }

            .search-bar input {
                border-top-left-radius: 10px;
                border-bottom-left-radius: 10px;
            }

            .search-bar span .btn {
                width: 50px;
                padding: 0;
                border-radius: 0;
                display: flex;
                border-bottom-right-radius: 10px;
                border-top-right-radius: 10px;
            }

                .search-bar span .btn i {
                    margin: auto;
                }

        .sort-container {
            width: 30%;
        }

        .sort-ddl {
            padding: 0;
            display: flex;
            justify-content: flex-end;
        }

        .nice-select {
            border-radius: 10px;
        }

        .table {
            border: 1px solid lightgrey;
        }

        .right {
            width: 20%;
        }

        .ann-title {
            color: steelblue;
        }

        .pagination-container {
            display: flex;
            justify-content: center;
            margin: 3% auto;
        }

            .pagination-container input {
                padding: 0 10px;
            }

            .pagination-container span {
                padding: 0 10px;
            }

        .gridview-container-wrapper {
            margin: 3% 0;
        }

        .main-area:after {
            content: '';
            position: absolute;
            top: 0;
            bottom: 0;
            left: 0;
            right: 0;
            z-index: -1;
            /* opacity: .4; */
            /* background: #fff;*/
        }

        .main-area .desc {
            margin: 20px auto;
            max-width: 500px;
            text-align: left;
        }

        .logoposition {
            position: absolute;
            top: 10rem;
            text-align: center;
            margin: auto;
            width: 100%;
        }

        .logosize {
            max-width: 500px;
        }

        @media (max-width: 1200px) {
            .logoposition {
                position: absolute;
                top: 6rem;
                text-align: center;
                margin: auto;
                width: 100%;
                left: 0rem;
            }

            .logosize {
                max-width: 300px;
            }

            .main-area .desc {
                margin: 20px auto;
                max-width: 1100px;
                text-align: left;
                font-size: 12px;
            }

            .mobile_table {
                width: 50%;
            }
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container p-4 mb-5 mt-3 hv-100">
        <div class="row text-left mb-3">
            <div class="col-12 d-inline-flex">
                <p class="membership_title fw-200" id="lbl_204">Support - </p>
                &nbsp;<p class="membership_title fw-500" id="lbl_203">Announcement</p>
            </div>
        </div>

        <hr />

        <div class="row mt-4" runat="server">
            <div class="col-md-6 col-12 mt-2">
                <!-- SEARCH & SORT -->
                <div class="search-sort-container-wrapper">
                    <div class="search-sort-container">

                        <!-- SEARCH -->
                        <div class="search-container">
                            <div class="col-12 col-lg-auto pl-lg-1 search-bar">
                                <div class="">
                                    <div class="input-group">
                                        <asp:TextBox ID="txt_Search" runat="server" AutoComplete="off" MaxLength="50" placeholder="Search by Announcement title" AutoPostBack="true" OnTextChanged="txt_Search_TextChanged" CssClass="search-term form-control" onkeydown="isEnter(event)"></asp:TextBox>
                                        <span class="input-group-append">
                                            <span class="btn btn-outline-secondary" onclick="isEnter(event)"><i class="bx bx-search"></i></span>
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- SEARCH -->

                    </div>
                </div>
                <!-- SEARCH & SORT -->
            </div>
            <div class="col-md-6 col-12 mt-2">
                <div>
                    <div class="d-flex" style="width: 10rem;">
                        <label class="ws-nowrap mr-3 mb-0">Sort:</label>
                        <asp:DropDownList ID="ddlsort" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSort_SelectedIndexChanged" CssClass="form-control select-style-1 results-per-page">
                            <asp:ListItem Text="Newest"></asp:ListItem>
                            <asp:ListItem Text="Oldest"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>

            <div class="col-lg-12">
                <!-- GRID VIEW & PAGINATION -->
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>

                        <!-- GRID VIEW -->
                        <div class="gridview-container-wrapper">
                            <div class="gridview-container">
                                <asp:GridView ID="gvAnnouncements" runat="server" AutoGenerateColumns="False" GridLines="Horizontal" EmptyDataText="No Announcement Uploaded." CssClass="table table-ecommerce-simple mb-0 newtable">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Announcements">
                                            <ItemStyle CssClass="tablerRestColumn left mobile_table" />
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hlTitle" runat="server" CssClass="ann-title" Text='<%# Eval("Title") %>' NavigateUrl='<%# "Announce_Details.aspx?Ids=" + Eval("Ids") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemStyle CssClass="tablerRestColumn right mobile_table" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblPubStartDT" runat="server" CssClass="ann-desc" Text='<%# Eval("PubStartDT", "{0:dd-MM-yyyy}") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>

                                    <HeaderStyle CssClass="headertable" BackColor="#dddddd" />
                                    <RowStyle CssClass="tablerowstyle" ForeColor="SteelBlue" />
                                    <SelectedRowStyle BackColor="#e7e7e7" Font-Bold="true" ForeColor="#333333" />
                                    <EditRowStyle BackColor="#999999" />
                                    <AlternatingRowStyle BackColor="#f9f9f9" />
                                </asp:GridView>
                            </div>
                        </div>
                        <!-- GRID VIEW -->

                        <!-- PAGINATION -->
                        <div class="pagination-container">
                            <asp:Button ID="btnPrevious" runat="server" Text="&lt;" OnClick="btnPrevious_Click" />
                            <asp:Label ID="lblPageInfo" runat="server" Text="Page 1 of 1"></asp:Label>
                            <asp:Button ID="btnNext" runat="server" Text="&gt;" OnClick="btnNext_Click" />
                        </div>
                        <!-- PAGINATION -->

                    </ContentTemplate>
                </asp:UpdatePanel>
                <!-- GRID VIEW & PAGINATION -->
            </div>
        </div>
    </div>
    <script>

</script>
</asp:Content>

