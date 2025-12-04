<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="FAQ.aspx.cs" Inherits="FAQ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style>
        .header-nav-menu {
            /* add background styles here */
            height: 36px;
            background-size: 100%;
            line-height: 36px;
            margin-bottom: 10px;
        }

        .row {
        }

        .contents {
            margin: 0 5%;
        }

        .top {
            justify-content: space-between;
        }

            .top .search-bar, .top .sort-ddl, .top .pg-size, .top .manual-pg, .top .no-record, .top .paging {
                margin: 0;
            }

        .pl-lg-1, .px-lg-1 {
            padding: 0 !important;
        }

        .ml-auto, .mx-auto {
            margin: 0 !important;
        }

        .faq {
            background: white;
            box-shadow: 0 2px 48px 0 rgba(0, 0, 0, 0.06);
            border-radius: 4px;
            margin: 0 auto;
            width: 100%;
        }

            .faq .card {
                border: none;
                background: none;
                border-bottom: 1px dashed #CEE1F8;
            }

                .faq .card .card-header {
                    padding: 0px;
                    border: none;
                    background: none;
                    -webkit-transition: all 0.3s ease 0s;
                    -moz-transition: all 0.3s ease 0s;
                    -o-transition: all 0.3s ease 0s;
                    transition: all 0.3s ease 0s;
                }

                    .faq .card .card-header:hover {
                        background: rgba(20, 148, 116, 0.1);
                        padding-left: 10px;
                    }

                    .faq .card .card-header .faq-title {
                        width: 100%;
                        text-align: left;
                        padding: 0px;
                        padding-left: 30px;
                        padding-right: 30px;
                        font-weight: 400;
                        font-size: 15px;
                        letter-spacing: 1px;
                        color: #3B566E;
                        text-decoration: none !important;
                        -webkit-transition: all 0.3s ease 0s;
                        -moz-transition: all 0.3s ease 0s;
                        -o-transition: all 0.3s ease 0s;
                        transition: all 0.3s ease 0s;
                        cursor: pointer;
                        padding-top: 20px;
                        padding-bottom: 20px;
                    }

                        .faq .card .card-header .faq-title .badge {
                            display: inline-block;
                            width: 20px;
                            height: 20px;
                            line-height: 14px;
                            float: left;
                            -webkit-border-radius: 100px;
                            -moz-border-radius: 100px;
                            border-radius: 100px;
                            text-align: center;
                            background: #149474;
                            color: #fff;
                            font-size: 12px;
                            margin-right: 20px;
                        }

                .faq .card .card-body {
                    padding: 30px;
                    padding-left: 35px;
                    padding-bottom: 16px;
                    font-weight: 400;
                    font-size: 16px;
                    color: #6F8BA4;
                    line-height: 28px;
                    letter-spacing: 1px;
                    border-top: 1px solid #F3F8FF;
                }

                    .faq .card .card-body p {
                        margin-bottom: 14px;
                    }

        .btn {
            background: black;
        }

        .btn-info {
            padding: 3% 5%;
            font-size: 13px;
        }

        .btn-outline-secondary {
            height: 2rem;
            width: 2rem;
            align-content: center;
            padding: 0;
        }

        .bx-search {
            margin: auto;
        }

        .search-term {
            border: solid 1px #e8e8e8 !important;
        }

        .ml-2, .mx-2 {
            margin-left: 0 !important;
        }

        .nice-select {
            align-content: center;
        }

        .input-group .form-control {
            z-index: 0;
        }

            .input-group .form-control:hover {
                z-index: 0;
            }

        .custom-dropdown {
            position: relative;
            display: inline-block;
            width: 150px;
        }

        .dropdown-button {
            border: 1px solid #ced4da;
            cursor: pointer;
            text-align: center;
            border-radius: 4px;
            width: 100%;
            font-size: 13px;
            padding: 2%;
        }

        .dropdown-content {
            display: none;
            position: absolute;
            background-color: #ffffff;
            border: 1px solid #ced4da;
            border-radius: 4px;
            width: 100%;
            z-index: 1;
            padding: 3% 5%;
            font-size: 13px;
        }

            .dropdown-content label {
                display: block;
                font-size: 13px;
            }

            .dropdown-content input {
                cursor: pointer;
            }

                .dropdown-content input[type="radio"] {
                    margin: 0 10px;
                }

        .dropdown-button.active + .dropdown-content {
            display: block;
        }

        .dropdown-content button {
            padding: 3%;
            background-color: black;
            color: white;
        }

        .button, input[type="button"], input[type="reset"], input[type="submit"] {
            margin: 10px;
        }


        @media (max-width: 991px) {
            .faq {
                margin-bottom: 30px;
            }

                .faq .card .card-header .faq-title {
                    line-height: 26px;
                    margin-top: 10px;
                }

            .sort-ddl, .pg-size {
                margin: 2% 0;
                padding: 0;
            }

            .faq .card .card-header .faq-title {
                padding: 1%;
                font-size: 13px;
                margin: 1% 2%;
            }

            .faq .card .card-body {
                padding: 2% 20px;
                font-size: 12px;
                color: black;
            }

                .faq .card .card-body ul {
                    margin: 0;
                    margin-left: 5%;
                }

            .pg-size-container, .sort-container {
                width: 99% !important;
                margin: auto;
            }

            .manual-container {
                margin: 2% 0;
            }

            .faq .card .card-header .faq-title .badge {
                float: none;
            }
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container p-4" style="margin-bottom: 20rem;">
        <div class="row text-left mt-3 mb-4">
            <div class="col-12 d-inline-flex">
                <p class="membership_title fw-200">Support - </p>
                &nbsp;<p class="membership_title fw-500">FAQ</p>
            </div>
        </div>

        <hr />

        <div class="row mt-4" runat="server">
            <div class="col-lg-12">
                <div class="row align-items-center mb-3 top">

                    <!-- Search Bar -->
                    <div class="col-12 col-lg-auto pl-lg-1 search-bar">
                        <div class="">
                            <div class="input-group">
                                <asp:TextBox ID="txt_Search" runat="server" AutoComplete="off" MaxLength="50" placeholder="Search by FAQ title" AutoPostBack="true" OnTextChanged="txt_Search_TextChanged" CssClass="search-term form-control" onkeydown="isEnter(event)"></asp:TextBox>
                                <span class="input-group-append">
                                    <span class="btn btn-outline-secondary" onclick="isEnter(event)"><i class="bx bx-search"></i></span>
                                </span>
                            </div>
                        </div>
                    </div>

                    <!-- Sort 2 -->
                    <div class="col-6 col-lg-auto ml-auto mb-3 mb-lg-0 sort-ddl">
                        <div class="d-flex align-items-lg-center flex-column flex-lg-row sort-container" style="width: 10rem;">
                            <label class="ws-nowrap mr-3 mb-0">Sort:</label>
                            <asp:DropDownList ID="ddlsort" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSort_SelectedIndexChanged" CssClass="form-control select-style-1 results-per-page">
                                <asp:ListItem Text="Newest"></asp:ListItem>
                                <asp:ListItem Text="Oldest"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <!-- Page Size -->
                    <div class="col-6 col-lg-auto ml-auto mb-3 mb-lg-0 pg-size">
                        <div class="d-flex align-items-lg-center flex-column flex-lg-row pg-size-container" style="width: 10rem;">
                            <label class="ws-nowrap mr-3 mb-0">Show:</label>
                            <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged" CssClass="form-control select-style-1 results-per-page">
                                <asp:ListItem Text="25"></asp:ListItem>
                                <asp:ListItem Text="50"></asp:ListItem>
                                <asp:ListItem Text="75"></asp:ListItem>
                                <asp:ListItem Text="100"></asp:ListItem>
                                <asp:ListItem Text="Manual"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <!-- Page Size - Manual -->
                    <div class="col-12 col-lg-auto pl-lg-1 manual-pg">
                        <div class="d-flex align-items-lg-center flex-column flex-lg-row manual-container">
                            <asp:TextBox ID="pagesize" runat="server" Visible="false" AutoPostBack="true"
                                Min="1" TextMode="Number" OnTextChanged="ddlPager_SelectedIndexChanged"
                                CssClass="form-control select-style-1 results-per-page ws-nowrap mr-3 mb-0" />
                            <asp:LinkButton ID="pagesizesearchicon" runat="server" CssClass="btn btn-info ml-2" Visible="false" OnClick="ddlPager_SelectedIndexChanged">Apply</asp:LinkButton>
                        </div>
                    </div>

                    <!-- No. of Record -->
                    <div class="col-12 col-lg-auto pl-lg-1 no-record">
                        <div>
                            <asp:Label ID="lbl_Record" runat="server" Text="No. Of Record - "></asp:Label>
                            <asp:Label ID="lbl_Record3" runat="server"></asp:Label>
                            of
                            <asp:Label ID="lbl_Record2" runat="server"></asp:Label>
                        </div>
                    </div>

                    <!-- Paging -->
                    <div class="col-12 col-lg-auto pl-lg-1 paging">
                        <asp:DropDownList ID="ddlPager" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPager_SelectedIndexChanged" CssClass="pagination-wrapper pagebutton"></asp:DropDownList>
                    </div>


                </div>

                <!-- FAQ Accordion -->
                <div class="row align-items-center mb-3 bottom">
                    <div class="faq" id="accordion">
                        <asp:Repeater runat="server" ID="rpt_faq">
                            <ItemTemplate>
                                <div class="card">
                                    <div class="card-header" id='<%# "faqHeading-" + Container.ItemIndex + 1 %>'>
                                        <div class="mb-0">
                                            <h5 class="faq-title" data-toggle="collapse" data-target='<%# "#faqCollapse-" + Container.ItemIndex + 1 %>' data-aria-expanded="true" data-aria-controls='<%# "faqCollapse-" + Container.ItemIndex + 1 %>'>
                                                <span class="badge"><%# Eval("RowNumber") %></span><%# Eval("Title") %>
                                            </h5>
                                        </div>
                                    </div>
                                    <div id='<%# "faqCollapse-" + Container.ItemIndex + 1 %>' class="collapse" aria-labelledby='<%# "faqHeading-" + Container.ItemIndex + 1 %>' data-parent="#accordion">
                                        <div class="card-body">
                                            <p><%# Eval("Content") %></p>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>


                        <div runat="server" id="div_no_record">
                            <p>No record found</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

    <script>
        $(document).ready(function () {
            $('.faq-title').click(function () {
                // Toggle the collapse state of the corresponding content
                $($(this).data('target')).collapse('toggle');
            });
        });

        function validatePageSize() {
            var pageSizeInput = document.getElementById('<%= pagesize.ClientID %>');
            var pageSize = parseInt(pageSizeInput.value, 10);

            if (isNaN(pageSize) || pageSize < 1) {
                alert("Please enter a valid number greater than or equal to 1.");
                pageSizeInput.value = "1"; // Set a default value
                pageSizeInput.focus();
                return false; // Prevent the form submission
            }
            return true;
        }

        function isEnter(event) {
            if (event.keyCode === 13 || event.type === 'click') {
                __doPostBack('<%= txt_Search.UniqueID %>', '');
            }
        }

        //-------------------------------
        function toggleDropdown() {
            document.querySelector('.dropdown-button').classList.toggle('active');
        }

        function applySort() {
            var sortCriterion = document.querySelector('input[name="sortCriterion"]:checked').value;
            var sortOrder = document.querySelector('input[name="sortOrder"]:checked').value;
            var url = new URL(window.location.href);

            // Set the sorting parameters
            url.searchParams.set('sortColumn', sortCriterion);
            url.searchParams.set('sortOrder', sortOrder);

            // Redirect to the updated URL to trigger a page load with new sorting parameters
            window.location.href = url.toString();
        }

        // Close dropdown if clicked outside
        window.onclick = function (event) {
            if (!event.target.matches('.dropdown-button')) {
                var dropdowns = document.getElementsByClassName("dropdown-content");
                for (var i = 0; i < dropdowns.length; i++) {
                    var openDropdown = dropdowns[i];
                    if (openDropdown.style.display === 'block') {
                        openDropdown.style.display = 'none';
                    }
                }
            }
        }

        //-----------------------------------------
        //$(document).ready(function () {
        //    // Update hidden field for sortCriterion
        //    $('input[name="sortCriterion"]').change(function () {
        //        $('#radsortCriterion').val($(this).val());
        //    });

        //    // Update hidden field for sortOrder
        //    $('input[name="sortOrder"]').change(function () {
        //        $('#radsortOrder').val($(this).val());
        //    });
        //});

        //$(document).ready(function (e) {

        //    $("input[type='radio']").click(function () {
        //        var radioValue = $("input[name='sortCriterion']:checked").val();
        //        if (radioValue) {
        //            $("#radsortCriterion").val(radioValue);
        //            console.clear();
        //            console.log($("#radsortCriterion").val());
        //        }
        //        var radioValue = $("input[name='sortOrder']:checked").val();
        //        if (radioValue) {
        //            $("#radsortOrder").val(radioValue);
        //            console.clear();
        //            console.log($("#radsortOrder").val());
        //        }
        //    });

        //})

        //function updateHiddenField() {
        //    // Get the selected radio button
        //    var selectedRadio1 = document.querySelector('input[name="sortCriterion"]:checked');
        //    var selectedRadio2 = document.querySelector('input[name="sortOrder"]:checked');

        //    // Check if a radio button is selected
        //    if (selectedRadio1) {
        //        // Update the hidden field's value
        //        document.getElementById('radsortCriterion').value = selectedRadio1.value;
        //    } else {
        //        // Clear the hidden field's value if no radio button is selected
        //        document.getElementById('radsortCriterion').value = '';
        //    }

        //    // Check if a radio button is selected
        //    if (selectedRadio2) {
        //        // Update the hidden field's value
        //        document.getElementById('radsortOrder').value = selectedRadio2.value;
        //    } else {
        //        // Clear the hidden field's value if no radio button is selected
        //        document.getElementById('radsortOrder').value = '';
        //    }
        //}

        //// Add event listeners to all radio buttons
        //var radioButtons = document.querySelectorAll('input[name="sortCriterion"]');
        //radioButtons.forEach(function (radio) {
        //    radio.addEventListener('change', updateHiddenField);
        //});

<%--        function setHiddenFields() {
            // Get selected sort criterion
            var sortCriterion = document.querySelector('input[name="sortCriterion"]:checked');
            if (sortCriterion) {
                document.getElementById('<%= radsortCriterion.ClientID %>').value = sortCriterion.value;
            } else {
                document.getElementById('<%= radsortCriterion.ClientID %>').value = ''; // Clear value if none selected
            }

            // Get selected sort order
            var sortOrder = document.querySelector('input[name="sortOrder"]:checked');
            if (sortOrder) {
                document.getElementById('<%= radsortOrder.ClientID %>').value = sortOrder.value;
            } else {
                document.getElementById('<%= radsortOrder.ClientID %>').value = ''; // Clear value if none selected
            }
        }--%>





    </script>


</asp:Content>

