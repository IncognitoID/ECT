<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Assign_Member.aspx.cs" Inherits="Assign_Member" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

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
            background-color: #e2e5e8;
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

        /*.table-container {
            overflow-x: auto;*/ /* Enable horizontal scrolling */
        /*width: 100%;*/ /* Ensure the container fills the available space */
        /*}*/

        /* CSS for table */
        /*.table {
            width: 100%;*/ /* Ensure the table fills the container */
        /* Other styles for the table */
        /*}*/

        table {
            width: auto; /* Allow table to expand to maximum content width */
            border-collapse: collapse; /* Collapse borders to ensure consistent spacing */
        }

        th {
            padding: 8px;
            text-align: center;
            white-space: nowrap; /* Prevent wrapping */
        }

        .div_keyinid {
            display: inline-flex !important;
            border: 1px solid #e8e8e8;
            border-radius: 5px;
            padding: 10px 10px;
            margin-top: 5px;
        }

        .d-grid{
            display:grid;
        }

        /* Responsive styles */
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

            .div_keyinid {
                display: grid !important;
                border: 1px solid #e8e8e8;
                border-radius: 5px;
                padding: 10px 10px;
                margin-top: 40px;
            }

            .w-75 {
                width: 100% !important;
            }
        }

        @media (max-width: 300px) {

            .membership_title {
                font-size: 14px;
                color: black;
            }
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="pf-details mt-5">
        <div class="container">
            <div class="row text-left mb-3">
                <div class="col-12 d-inline-flex">
                    <p class="membership_title fw-200" id="lbl_158">Registration - </p>
                    &nbsp;<p class="membership_title fw-500" id="lbl_159">Member Restructure</p>
                </div>
            </div>

            <div class="row mb-5">
                <div class="col-12 mb-3 table-container">
                    <table class="table">
                        <tr>
                            <th id="lbl_160">Name</th>
                            <th id="lbl_161">Join Date & Time</th>
                            <th id="lbl_162">Package</th>
                            <th id="lbl_163">BV</th>
                            <th class="mobile-width" id="lbl_164">Binary Placement</th>
                            <th></th>
                        </tr>

                        <asp:Repeater runat="server" ID="rpt_assign_member" OnItemDataBound="rpt_assign_member_ItemDataBound" OnItemCommand="rpt_assign_member_ItemCommand">
                            <ItemTemplate>
                                <tr>
                                    <td style="vertical-align: middle;">
                                        <asp:Label runat="server" ID="lbl_member_name"></asp:Label></td>
                                    <td style="vertical-align: middle;">
                                        <asp:Label runat="server" ID="lbl_member_join_date"></asp:Label></td>
                                    <td style="vertical-align: middle;">
                                        <asp:Label runat="server" ID="lbl_member_package"></asp:Label></td>
                                    <td style="vertical-align: middle;">
                                        <asp:Label runat="server" ID="lbl_member_bv"></asp:Label></td>
                                    <td class="mobile-width" style="vertical-align: middle;">
                                        <asp:DropDownList runat="server" ID="ddl_member_placement" AutoPostBack="true" OnSelectedIndexChanged="ddl_member_placement_SelectedIndexChanged"></asp:DropDownList>
                                        <div class="d-grid w-100" runat="server" id="div_id" visible="false">
                                            <div class="div_keyinid">
                                                <span class="w-25 m-auto">ID : </span>
                                                <asp:TextBox runat="server" ID="txt_keyinid" CssClass="w-75" MaxLength="10" Placeholder="Key in downline ID" OnTextChanged="txt_keyinid_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                            <div class="div_keyinid">
                                                <span class="w-25 m-auto">Name : </span>
                                                <asp:TextBox runat="server" ID="txt_member_name" CssClass="w-75" Placeholder="" Enabled="false"></asp:TextBox>
                                            </div>
                                            <div class="div_keyinid">
                                                <span class="w-25 m-auto">Placement : </span>
                                                <asp:DropDownList runat="server" ID="ddl_default_placement" CssClass="w-75">
                                                    <asp:ListItem Value="Default" id="lbl_454">Default Member Placement</asp:ListItem>
                                                    <asp:ListItem Value="Left" id="lbl_455">Left</asp:ListItem>
                                                    <asp:ListItem Value="Right" id="lbl_456">Right</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </td>
                                    <td style="vertical-align: middle; white-space: nowrap;">
                                        <asp:LinkButton runat="server" ID="btn_confirm" Text="Confirm" CssClass="btn_confirm" CommandName="Confirm" CommandArgument='<%# Eval("UserID") %>'></asp:LinkButton></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        <tr runat="server" id="tr_no_record" visible="false" class="w-100">
                            <td colspan="6">No record found.</td>
                        </tr>
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
            var page = 'Member Restructure';
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

    </script>

</asp:Content>

