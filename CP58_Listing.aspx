<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CP58_Listing.aspx.cs" Inherits="CP58_Listing" %>

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

        .d-grid {
            display: grid;
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


    <section class="pf-details mt-3">
        <div class="container">
            <div class="row text-left mb-2">
                <div class="col-12 d-inline-flex">
                    <p class="membership_title fw-200" id="lbl_444">Bonus - </p>
                    &nbsp;<p class="membership_title fw-500" id="lbl_445">CP58 (Tax Statements)</p>
                </div>
            </div>

            <div class="row mb-5">
                <div class="col-12 mb-1 float-right text-right">
                    <asp:Button runat="server" ID="btn_update_my_profile" CssClass="btn btn-primary" Text="Update My Tax Information" OnClientClick="ShowModal(); return false;" />
                </div>

                <div class="col-12 mb-3 table-container">
                    <table class="table">
                        <tr>
                            <th id="lbl_446">Report Year</th>
                            <th id="lbl_447">Action</th>
                        </tr>

                        <asp:Repeater runat="server" ID="rpt_year" OnItemDataBound="rpt_year_ItemDataBound" OnItemCommand="rpt_year_ItemCommand">
                            <ItemTemplate>
                                <tr>
                                    <td style="vertical-align: middle;">
                                        <asp:Label runat="server" ID="lbl_report_year" Text='<%# Eval("Year") %>'></asp:Label></td>
                                    <td style="vertical-align: middle; white-space: nowrap;">
                                        <asp:LinkButton runat="server" ID="btn_confirm" Text="Download" CssClass="btn_confirm" CommandName="Download" CommandArgument='<%# Eval("Year") %>'></asp:LinkButton></td>
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

    <div class="modal fade" id="UpdateModal" tabindex="-1" role="dialog" aria-labelledby="ewalletModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
              <div class="modal-header">
                <h5 class="modal-title">Update My Tax Information</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-12">
                            <asp:Label runat="server" ID="lbl_tax_number" Text="Income Tax No."></asp:Label>
                        </div>
                        <div class="col-12 mb-2">
                            <asp:TextBox runat="server" ID="txt_income_tax_number" CssClass="form-control w-100"></asp:TextBox>
                        </div>
                        <div class="col-12">
                            <asp:Label runat="server" ID="lbl_stay" Text="Stay In Malaysia"></asp:Label>
                        </div>
                        <div class="col-12">
                            <asp:DropDownList runat="server" ID="ddl_stay" CssClass="form-control w-100">
                                <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                <asp:ListItem Text="No" Value="No"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-12">
                            <asp:Button runat="server" ID="btn_final_update" OnClick="btn_final_update_Click" Style="display: none;" />
                            <button type="button" id="btn_update" onclick="Update_Information()" class="form-control btn btn-primary">Update</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>
        function ShowModal() {
            $('#UpdateModal').modal('show');
        }

        function Update_Information() {
            var tax_number = document.getElementById("ContentPlaceHolder1_txt_income_tax_number");

            if (tax_number.value === "") {
                alert("Please key in income tax number.")
            } else {
                document.getElementById("ContentPlaceHolder1_btn_final_update").click();
            }
        }

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
            var page = 'CP58 Report';
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

        function showModal(url) {
            var modal = document.getElementById("pdfModal");
            var iframe = document.getElementById("pdfIframe");
            iframe.src = url + "#toolbar=1";
            modal.style.display = "block";
        }

        function closeModal() {
            var modal = document.getElementById("pdfModal");
            var iframe = document.getElementById("pdfIframe");
            iframe.src = "";
            modal.style.display = "none";
        }

        function downloadPDF() {
            var iframe = document.getElementById("pdfIframe");
            var url = iframe.src.split('#')[0];
            window.location.href = url + "&download=true";
        }

        function previewPDF(memberId, year) {
            var url = "CP58_Listing.aspx?memberId=" + memberId + "&year=" + year + "&preview=true";
            showModal(url);
        }

    </script>

</asp:Content>

