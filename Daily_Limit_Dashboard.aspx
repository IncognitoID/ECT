<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Daily_Limit_Dashboard.aspx.cs" Inherits="Daily_Limit_Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style>
        .box_style {
            padding: 15px;
            border: 1px solid #efefef;
            border-radius: 10px;
            box-shadow: 1px 2px 6px #80808099;
        }

        input[type="checkbox"], input[type="radio"] {
            box-sizing: border-box;
            padding: 0;
            margin-right: 5px;
        }

        .available_balace_title{
            font-size: 20px;
        }
        
        .available_balace_amount{
            font-size: 30px;
            /*white-space: nowrap;*/   /* Prevent text wrapping */
            /*overflow: hidden;*/      /* Ensure any overflow is handled */
            /*text-overflow: ellipsis;*/ /* Add ellipsis if the text is too long */
            /*display: inline-block;*/
        }

        .view_transaction{
            font-size: 17px !important;
            background-color: white !important;
            border: 0px !important;
            font-weight: 400 !important;
            cursor: pointer;
        }

        .withdrawal_setting_title{
            font-size: 20px
        }

        .radio_button_list{
            font-size: 14px; font-weight: 500;
        }

        .withdrawal_amount_title{
            font-size: 18px;
        }
        
        .withdrawal_amount_title{
            font-size: 20px;
        }

        .remark{
            font-size: 12px;
        }

        .btn_update_class{
            background-color: #149474 !important; color: white !important;
        }

        .Payout_balance_title{
            font-size: 18px;
        }

        .Payout_balance{
            border: 1px solid black; padding: 10px; font-size: 22px;
        }

        .bonus_list{
            font-size: 16px; line-height: 1.9;
        }

        @media only screen and (max-width: 767px) {
            .payout_div{
                margin-top:1.5rem;
            }
        }

    </style>

    <style>
        .sales-header{
            width: 100%;

        }

        .sales-header .sales-title-div{
            align-content: center;
        }

        .sales-header .sales-date-div{
            display: flex;
            flex-wrap: nowrap;
            gap: 10px;
        }

        .sales-header .sales-date-div span{
            display: flex;
            flex-wrap: wrap;
            align-content: center;
        }

        #salesChart {
            width: 100%;
            min-height: 400px; /* Set a default height */
            height:auto;
        }

        .greentext{
            color: #149474;
        }

        .btn-filter{
            background-color: #149474 !important; color: white !important;
            height: 35px;
            padding: 7px;
        }

        .btn-confirm-request{
            background-color: #149474 !important; color: white !important;
        }

        .daily-limit-div{
            display: flex;
            flex-direction: row;
            justify-content: center;
            gap: 10px;
            white-space: nowrap;
        }

        .divider1 {
            display: block;
            color: #149474;
        }

        .divider2 {
            display: none;
            padding: 0 30px;
        }

            .divider2 hr {
                margin: 0;
                background: #149474;
            }

        #dailyLimitRequstModal{
            align-content: center;
        }

        #dailyLimitRequstModal .modal-content{
            /*padding: 0;*/
        }

        .request-amount-div{
            display: flex;
            flex-direction: column;
            gap: 10px;
        }

        .current-limit-div{
            display: flex;
            flex-direction: column;
            align-content: center;
            flex-wrap: wrap;
            height: 100px;
            justify-content: center;
            gap: 10px;
            text-align: center;
        }

        .request-input-div{
            display: flex;
        }

        .viewhistory{
            font-size: 12px;
            color: #787878d9;
        }

        @media (min-width: 991.11px){
            .daily-limit-div{
                flex-direction: column;
            }

            .divider1{
                display: none;
            }

            .divider2{
                display: block;
            }
        }

        @media (max-width: 500px) {
            .sales-header .sales-date-div {
                display: grid;
                flex-wrap: nowrap;
                gap: 10px;
                text-align: center;
                margin: 0 auto;
            } 
        }

        @media (max-width: 450px){
            #salesChart {
                width: 100%;
                min-height: 500px;
            }

            .daily-limit-div{
                flex-direction: column;
            }

            .divider1{
                display: none;
            }

            .divider2{
                display: block;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="pf-details mt-4">
        <div class="container">
            <div class="row text-left mb-3">
                <div class="col-12 d-inline-flex">
                    <p class="membership_title fw-200" id="lbl_372">Mobile Stockist - </p>
                    &nbsp;<p class="membership_title fw-500" id="lbl_373">Daily Limit</p>
                </div>
            </div>
            <hr />

            <div class="row mb-5">
                <div class="col-12 col-lg-4" style="gap: 30px; display: flex; flex-direction: column;">


                    <div style="height: 100%">
                        <div class="box_style" style="height: 100%; align-content: center; text-align: center;">
                            <div class="col-12">
                                <span class="available_balace_title" id="lbl_374">Daily Limit</span>
                            </div>
                            <div class="col-12 mt-4 mb-4 daily-limit-div">
                                <asp:Label runat="server" ID="lblRemainingDailyLimit" CssClass="available_balace_amount"></asp:Label>
                                <span class="divider1 available_balace_amount">/ </span>
                                <div class="divider2">
                                    <hr />
                                </div>
                                <asp:Label runat="server" ID="lblDailyLimit" CssClass="available_balace_amount greentext"></asp:Label>
                            </div>
                            <div class="col-12">
                                <asp:Button runat="server" Text="Request Increase Daily Limit  >" ID="btn_view_transaction" CssClass="view_transaction" OnClientClick="showRequestModal();     return false;" />
                            </div>
                        </div>
                    </div>

                    <div style="height: 100%">
                        <div class="box_style" style="height: 100%;">
                            <span class="" style="font-size: 17px;" id="lbl_375">Daily Limit History</span> <asp:LinkButton runat="server" CssClass="float-right viewhistory" ID="btn_view_history" OnClick="btn_view_history_Click">View all history ></asp:LinkButton>
                            <hr />
                            <div class="" style="max-height: 300px; overflow-y: auto;">
                                <asp:Repeater ID="rptrequesthistory" runat="server">
                                    <ItemTemplate>
                                        <div class="row justify-content-between" style="width: 100%; margin: auto;">
                                            <div class="col-8" style="padding: 0;">
                                                <div>
                                                    <asp:Label ID="lblrequestdate" runat="server" Text='<%# Eval("Created_DT", "{0:ddd, dd MMM yyyy}") %>' CssClass="font-weight-bold"></asp:Label>
                                                </div>
                                                <div>
                                                    <asp:Label ID="lblrequestamount" runat="server" Text='<%# FormatRequestAmount(Eval("Limit_Used"), Eval("Limit_Earn")) %>' />
                                                </div>
                                            </div>

                                            <div class="col-4 text-right align-content-center" style="padding-left: 0;">
                                                <asp:Label ID="lblrequeststatus" runat="server" Text='<%# Eval("Status") %>' CssClass="font-weight-bold greentext"></asp:Label>
                                            </div>
                                        </div>
                                        <hr />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            <div id="norequesthistory" class="row justify-content-center center" style="width: 100%; margin: auto; height: 100px;" runat="server">
                                    <asp:Label ID="lblnorequesthistory" runat="server" Text="No Limit Request History"></asp:Label>
                                </div>
                        </div>
                    </div>

                </div>

                <div class="col-12 col-lg-8 payout_div">
                    <div class="box_style" style="height: 100%;">
                        <div class="row" style="justify-content: center; height: 100%;">
                            <div class="sales-header row" style="height: fit-content;">
                                <div class="sales-title-div col-12 col-lg-4">
                                    <span class="Payout_balance_title" id="lbl_376">Sales</span>
                                </div>
                                <div class="sales-date-div col-12 col-lg-8">
                                    <asp:TextBox ID="salesStartDate" runat="server" TextMode="Date" CssClass="form-control" />
                                    <span id="lbl_377">to</span>
                                    <asp:TextBox ID="salesEndDate" runat="server" TextMode="Date" CssClass="form-control" />
                                    <asp:LinkButton runat="server" ID="btn_filter_date" CssClass="btn btn-primary btn-filter" Text="Search" ClientIDMode="Static" UseSubmitBehavior="false" OnClientClick="return false;"></asp:LinkButton>
                                </div>
                            </div>

                            <div class="col-12">
                                <canvas id="salesChart"></canvas>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <div class="modal fade" id="dailyLimitRequstModal" tabindex="-1" role="dialog" aria-labelledby="ewalletModalLabel" aria-hidden="true" >
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-body">

                    <div class="current-limit-div">
                        <span class="available_balace_title" id="lbl_378">Current Limit:</span>
                        <asp:Label runat="server" ID="lblcurrentlimit" CssClass="available_balace_amount greentext"></asp:Label>
                    </div>

                    <hr />

                    <div class="request-amount-div">
                        <span id="lbl_379">Please enter request amount :</span>
                        <div class="request-input-div">
                            <asp:TextBox ID="txt_request_amount" runat="server" CssClass="w-100 form-control p-2" PlaceHolder="0.00" onkeypress="return checkAmount(event)" oninput="validateDecimalPlaces(this, 2)" onfocus="resetInputFlag()"></asp:TextBox>
                            <asp:Button ID="confirm_request" runat="server" CssClass="btn btn-primary btn-confirm-request" Text="Confirm" UseSubmitBehavior="false" OnClick="btn_confirm_request_Click"/>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>

    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

    <script>
        var sales_amount = 'Sales Amount';

        $(document).ready(function () {
            Load_Language();
        });

        function Load_Language() {
            var page = 'Daily Limit Dashboard';
            var language = 'English';
            var cookies_language = getCookieValue("language");
            if (cookies_language) {
                language = cookies_language;
            }

            if (language == 'Chinese') {
                sales_amount = '销售金额';
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

        //request daily limit increase
        function showRequestModal() {
            $('#dailyLimitRequstModal').modal('show');
        }

        // Validate request amount (input)
        function checkAmount(event) {
            var charCode = (event.which) ? event.which : event.keyCode;
            var inputValue = event.target.value;

            if (inputValue.length === 0) {
                isFirstInput = true;
            }

            if (isFirstInput) {
                if (charCode >= 48 && charCode <= 57) {
                    isFirstInput = false;
                    return true;
                }
                return false;
            } else {
                if (charCode == 8) {
                    return true;
                }

                if ((charCode >= 48 && charCode <= 57) || charCode == 46) {
                    if (charCode == 46 && inputValue.includes('.')) {
                        return false;
                    }

                    if (inputValue.includes('.')) {
                        var parts = inputValue.split('.');
                        if (parts[1].length >= 2) {
                            return false;
                        }
                    }
                    return true;
                }
                return false;
            }
        }

        // Validate request amount (decimal places)
        function validateDecimalPlaces(element, decimalPlaces) {
            var value = element.value;

            if (value.includes('.')) {
                var parts = value.split('.');
                if (parts[1].length > decimalPlaces) {
                    element.value = parts[0] + '.' + parts[1].substring(0, decimalPlaces);
                }
            }
        }

        // Reset the flag on focus (optional)
        function resetInputFlag() {
            isFirstInput = true;
        }

        function getCookieValue(name) {
            var nameEQ = name + "=";
            var cookies = document.cookie.split(';');
            for (var i = 0; i < cookies.length; i++) {
                var cookie = cookies[i];
                while (cookie.charAt(0) === ' ') cookie = cookie.substring(1, cookie.length); // Remove leading spaces
                if (cookie.indexOf(nameEQ) === 0) return cookie.substring(nameEQ.length, cookie.length);
            }
            return null; // Return null if the cookie is not found
        }

        //bar chart
        $(document).ready(function () {
            var chart;

            var memberid = "";
            memberid = getCookieValue("userid");

            // Function to load sales data and handle chart rendering
            function loadSalesData(startDate, endDate) {
                $.ajax({
                    type: "POST",
                    url: "Daily_Limit_Dashboard.aspx/GetSalesData",
                    data: JSON.stringify({ memberid: memberid, startDate: startDate, endDate: endDate }),  // Pass startDate and endDate
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        var labels = [];
                        var data = [];

                        for (var i = 0; i < response.d.length; i++) {
                            var item = response.d[i];
                            labels.push(item.OrderDate);  // All dates
                            data.push(item.TotalSalesAmount);   // Sales data, 0 if no sales
                        }

                        // Determine the axis type based on screen width
                        var indexAxis = (window.innerWidth < 450) ? 'y' : 'x'; // 'y' for horizontal, 'x' for vertical

                        var ctx = document.getElementById('salesChart').getContext('2d');
                        if (chart) {
                            chart.destroy(); // Destroy existing chart if it exists before creating a new one
                        }

                        chart = new Chart(ctx, {
                            type: 'bar',
                            data: {
                                labels: labels,
                                datasets: [{
                                    label: sales_amount,
                                    data: data,
                                    backgroundColor: 'rgba(75, 192, 192, 0.2)',
                                    borderColor: 'rgba(75, 192, 192, 1)',
                                    borderWidth: 1
                                }]
                            },
                            options: {
                                responsive: true,
                                maintainAspectRatio: false,
                                indexAxis: indexAxis,  // Switch between horizontal and vertical
                                scales: {
                                    y: {
                                        beginAtZero: true  // Start the Y-axis at 0
                                    }
                                }
                            }
                        });
                    },
                    error: function (xhr, status, error) {
                        console.error("Error: " + error);
                    }
                });
            }

            // Initial load of sales data with no date range (default to past 7 days)
            loadSalesData("", "");

            // Handle filter button click
            $("#btn_filter_date").click(function (event) {
                event.preventDefault();  // Prevent the default behavior (postback)

                var startDate = $("input[id$='salesStartDate']").val();  // Get the start date
                var endDate = $("input[id$='salesEndDate']").val();      // Get the end date

                // Log the values to the console for debugging
                console.log("Start Date: " + startDate);
                console.log("End Date: " + endDate);

                // Validate that both startDate and endDate are provided
                if (!startDate || !endDate) {
                    sweetalert_warning("Please select both a start and end date.", "warning");
                    return;
                }

                // Convert the start and end dates to JavaScript Date objects
                var start = new Date(startDate);
                var end = new Date(endDate);
                var today = new Date();  // Get the current date

                // Check if the start date exceeds the end date
                if (start > end) {
                    sweetalert_warning("The start date cannot be later than the end date.", "warning");
                    return;
                }

                // Check if the end date exceeds the current date
                if (end > today) {
                    sweetalert_warning("The end date cannot be later than the current date.", "warning");
                    return;
                }

                // Calculate the time difference in milliseconds
                var timeDifference = end.getTime() - start.getTime();

                // Calculate the number of days between the two dates
                var daysDifference = timeDifference / (1000 * 3600 * 24);  // Convert milliseconds to days

                // Validate that the date range is not more than 1 month (30 or 31 days)
                if (daysDifference > 31) {
                    sweetalert_warning("The date range cannot exceed 1 month.", "warning");
                    return;
                }

                // Load sales data for the selected date range
                loadSalesData(startDate, endDate);
            });


            // Listen for window resize events to switch between vertical and horizontal charts
            $(window).resize(function () {
                var startDate = $("input[id$='salesStartDate']").val();  // Get the start date
                var endDate = $("input[id$='salesEndDate']").val();      // Get the end date

                // Recalculate indexAxis and reload sales data based on current screen size
                loadSalesData(startDate, endDate);
            });
        });
    </script>

    <script type="text/javascript">
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

