<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Direct_Referral_Listing.aspx.cs" Inherits="Direct_Referral_Listing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link rel="stylesheet" href="treeview.min.css" />
    <script src="treeview.min.js"></script>

    <style>
        .membership_title {
            font-size: 20px;
            color: black;
        }

        .fw-200 {
            font-weight: 200;
        }

        .fw-500 {
            font-weight: 500;
        }

        .form-check-input input[type="radio"] {
            margin-right: 5px;
        }

        .form-check-input label {
            font-size: 15px;
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

        tr, th {
            text-align: center;
        }

        td {
            text-align: center;
        }

        .current {
            position: relative;
            top: 7px;
        }

        .indent.collapsed {
            display: none;
        }

        tr, td {
            list-style: none;
            margin: 0;
            padding: 0;
        }

        td {
            position: relative;
        }

            td:before {
                content: "";
                position: absolute;
                top: 0;
                left: -15px; /* Adjust the left position as needed */
                width: 15px; /* Adjust the width as needed */
                border-top: 1px solid black;
                height: 100%;
            }

            td:first-child:before {
                border-left: 1px solid black;
                left: 25px;
            }

        tr:not(.indent) td:before {
            display: none;
        }

        tr.indent td:first-child:before {
            border-bottom: 1px solid black;
            border-top: 0px;
            margin-top: -25px;
        }

        tr.indent td:before {
            border-top: 0px;
        }

        tr.indent td:first-child:before {
            border-left: 1px solid black; /* Adjust the style as needed */
        }

        .btn_search {
            border: 1px solid #efefef;
            padding: 7px 10px;
            border-radius: 5px;
            background-color: #bbb2b2;
            color: white;
        }

            .btn_search:hover {
                background-color: #007bff; /* Change this to the desired hover color */
                color: #ffffff; /* Change this to the desired text color on hover */
            }

        tr, th {
            text-align: center;
        }

        td {
            text-align: center;
        }

        table {
            width: auto; /* Allow table to expand to maximum content width */
            border-collapse: collapse; /* Collapse borders to ensure consistent spacing */
        }

        th {
            padding: 8px;
            text-align: center;
            white-space: nowrap; /* Prevent wrapping */
        }

        /* Set white-space to nowrap for the "Name" column */
        td:nth-child(2) { /* 2nd column for "Name" */
            white-space: nowrap;
        }

        td:nth-child(4) { /* 2nd column for "Name" */
            white-space: nowrap;
        }


        td:nth-child(5) { /* 2nd column for "Name" */
            white-space: nowrap;
        }

        @media (max-width: 768px) {

            .membership_title {
                font-size: 16px;
                color: black;
            }

            .table-container {
                overflow: auto;
            }

            .mobile-width {
                min-width: 200px; /* Minimum width for mobile view */
                width: auto !important; /* Allow width to adjust based on content */
            }

            th, td {
                padding: 4px;
                font-size: 12px; /* Decrease font size for smaller screens */
            }

            /* Adjust font-size for smaller screens */
            @media (max-width: 300px) {
                .membership_title {
                    font-size: 14px;
                    color: black;
                }
            }
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="pf-details mt-5" id="dateForm">
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
                    <asp:LinkButton runat="server" ID="btn_seach" Text="Search" CssClass="btn_search" OnClick="btn_seach_Click" Style="float: right;"></asp:LinkButton>
                </div>
            </div>

            <hr />

            <div class="row text-left mb-3">
                <div class="col-12 d-inline-flex">
                    <p class="membership_title fw-200" id="lbl_131">Registration - </p>
                    &nbsp;<p class="membership_title fw-500" id="lbl_132">Direct Referral Listing</p>
                </div>
            </div>

            <div class="row mb-5">
                <div class="col-12 mb-3 table-container">
                    <table class="table">
                        <tr>
                            <th></th>
                            <th id="lbl_124">Name</th>
                            <th id="lbl_125">Member ID</th>
                            <th id="lbl_126">Ranking</th>
                            <th id="lbl_127">Join Date</th>
                            <th id="lbl_128">BV (Left)</th>
                            <th id="lbl_129">BV (Right)</th>
                            <th id="lbl_130">Total BV</th>
                        </tr>

                        <tbody>
                            <asp:Repeater runat="server" ID="rpt_direct_referral" OnItemDataBound="rpt_direct_referral_ItemDataBound">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <span runat="server" id="btn_expand" class="expand-btn" onclick="toggleRow(this)"><i class="fa fa-plus-circle" style="position: relative; left: 7px;"></i></span>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lbl_member_name"></asp:Label>
                                        </td>
                                        <td class="text-center">
                                            <asp:Label runat="server" ID="lbl_member_id"></asp:Label></td>
                                        <td class="text-center">
                                            <asp:Label runat="server" ID="lbl_member_rank"></asp:Label></td>
                                        <td class="text-center">
                                            <asp:Label runat="server" ID="lbl_member_join_date"></asp:Label></td>
                                        <td class="text-center">
                                            <asp:Label runat="server" ID="lbl_downline_A_group_bv"></asp:Label></td>
                                        <td class="text-center">
                                            <asp:Label runat="server" ID="lbl_downline_B_group_bv"></asp:Label></td>
                                        <td class="text-center">
                                            <asp:Label runat="server" ID="lbl_downline_Total_group_bv"></asp:Label></td>
                                    </tr>
                                    <tr runat="server" id="tr_first" class="indent collapsed">
                                        <td></td>
                                        <td>
                                            <asp:Label runat="server" ID="lbl_downline_A_name"></asp:Label></td>
                                        <td class="text-center">
                                            <asp:Label runat="server" ID="lbl_downline_A_id"></asp:Label></td>
                                        <td class="text-center">
                                            <asp:Label runat="server" ID="lbl_downline_A_rank"></asp:Label></td>
                                    </tr>
                                    <tr runat="server" id="tr_second" class="indent collapsed">
                                        <td></td>
                                        <td>
                                            <asp:Label runat="server" ID="lbl_downline_B_name"></asp:Label></td>
                                        <td class="text-center">
                                            <asp:Label runat="server" ID="lbl_downline_B_id"></asp:Label></td>
                                        <td class="text-center">
                                            <asp:Label runat="server" ID="lbl_downline_B_rank"></asp:Label></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <tr runat="server" id="tr_no_record" visible="false" class="w-100">
                                <td colspan="8">No record found.</td>
                            </tr>
                        </tbody>

                    </table>
                </div>
            </div>
        </div>
    </section>

    <script>

        document.addEventListener("DOMContentLoaded", function () {
            Load_Language();
        });

        function getCookieValue(cookieName) {
            var name = cookieName + "=";
            var decodedCookie = decodeURIComponent(document.cookie);
            var cookieArray = decodedCookie.split(";");

            for (var i = 0; i < cookieArray.length; i++) {
                var cookie = cookieArray[i].trim();

                if (cookie.indexOf(name) === 0) {
                    return cookie.substring(name.length, cookie.length);
                }
            }

            return "";
        }

        function Load_Language() {
            var page = 'Direct Referral Listing';
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
                        } else if (item.Label_Type === 'Button') {
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

        function toggleRow(element) {
            var row = element.closest('tr');
            var indentRows = row.nextElementSibling; // Get the next row with class="indent"

            // Toggle the class for each row
            for (var i = 0; i < 2; i++) {
                if (indentRows && indentRows.classList.contains('indent')) {
                    if (indentRows.classList.contains('collapsed')) {
                        indentRows.classList.remove('collapsed');
                    } else {
                        indentRows.classList.add('collapsed');
                    }
                    indentRows = indentRows.nextElementSibling; // Move to the next row
                } else {
                    break; // Break the loop if no more rows with class="indent"
                }
            }

            // Toggle the class for the main row
            if (row.classList.contains('collapsed')) {
                row.classList.remove('collapsed');
                element.innerHTML = '<i class="fa fa-plus-circle" style="position: relative; left: 7px;"></i>';
            } else {
                row.classList.add('collapsed');
                element.innerHTML = '<i class="fa fa-minus-circle" style="position: relative; left: 7px;"></i>';
            }
        }

    </script>

</asp:Content>

