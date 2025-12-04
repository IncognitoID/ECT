<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Event.aspx.cs" Inherits="Event" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="https://fonts.googleapis.com/css?family=Open+Sans:400,700%7CPoppins:400,500" rel="stylesheet">
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Allura&family=Grey+Qo&display=swap" rel="stylesheet">
    <script src="js/jquery-3.3.1.min.js"></script>

    <style>
        section {
            margin: 3%;
            background-color: white;
        }

        * {
            margin: 0;
            padding: 0;
            font-family: 'Poppins', sans-serif;
        }

        .header-text-container {
            border-bottom: 1px solid lightgrey;
        }

            .header-text-container h1 {
                font-size: 1.5rem;
                margin: 10px 0;
            }

        .calendar-wrapper-wrapper {
            margin: 20px 0;
        }

        .calendar-container-wrapper {
            display: flex;
            margin: auto;
            justify-content: center;
        }

        /* Ensure the calendar container is a flex container */
        .calendar-container {
            background: #fff;
            width: 100%;
            border-radius: 10px;
            grid-template-rows: auto auto 1fr;
            margin: 0;
        }

            /* Header styling */
            .calendar-container header {
                display: flex;
                align-items: center;
                justify-content: space-between;
                margin: 0 3%;
            }

        /* Navigation icons styling */
        .calendar-container-wrapper span {
            height: 38px;
            width: 38px;
            margin: auto 0;
            cursor: pointer;
            text-align: center;
            line-height: 38px;
            border-radius: 50%;
            user-select: none;
            color: black;
            font-size: 1.9rem;
        }

            .calendar-container-wrapper span:hover {
                background: #f2f2f2;
            }

        /* Month and year styling */
        header .calendar-month-text {
            width: 100%;
            text-align: end;
            padding: 2%;
        }

        header .calendar-current-month {
            font-weight: bold;
            font-size: 3rem;
            font-family: "Allura", cursive;
        }

        header .calendar-current-year {
            font-size: 1.5rem;
            font-family: "Allura";
        }

        /* Calendar body setup */
        .calendar-body {
            overflow: scroll;
            padding-top: 0;
            display: grid;
            grid-template-columns: repeat(7, 1fr); /* 7 columns for days of the week */
        }

            /* Styling for weekdays and dates */
            .calendar-body .calendar-weekdays,
            .calendar-body .calendar-dates {
                display: contents; /* Make the ul elements behave like grid items */
            }

                .calendar-body .calendar-weekdays li,
                .calendar-body .calendar-dates li {
                    text-align: center;
                    padding: 10px;
                    border: 1px solid #ddd; /* Add border to each cell */
                    box-sizing: border-box; /* Include border in the element's total width and height */
                }

                .calendar-body .calendar-dates li {
                    text-align: right;
                }

        /* Styling for inactive and active dates */
        .calendar-dates li.inactive {
            color: #aaa;
        }

        .calendar-dates li.active {
            color: #000;
            background: #e4ebe7;
            font-weight: bold;
        }

        /* Weekdays styling */
        .calendar-body .calendar-weekdays li {
            cursor: default;
            font-weight: 500;
            padding: 5px;
            color: white;
            background: gray;
        }

        .calendar-navigation {
            display: inline-flex;
        }

        .calendar-curryear-cotainer {
            margin-top: 15px;
        }

        .calendar-body ul {
            list-style: none;
        }

        .button-container {
            height: 5rem;
            overflow-y: auto;
        }

            .button-container::-webkit-scrollbar {
                width: 4px; /* Adjust the width for vertical scrollbar */
            }

            .button-container::-webkit-scrollbar-thumb {
                background-color: rgba(0, 0, 0, 0.5); /* Change color as needed */
                border-radius: 10px; /* Make the scrollbar thumb rounded */
            }

            .button-container::-webkit-scrollbar-track {
                background-color: transparent; /* Make the track invisible */
            }

            .button-container .eventbtn {
                width: auto;
                overflow-y: hidden;
                text-align: center;
            }

        .calendar-button {
            padding: 5%;
            width: 100%;
            box-sizing: border-box; /* Ensures padding does not affect button's width */
            white-space: normal; /* Allows the button text to wrap */
            word-wrap: break-word; /* Allows long words to break within the button */
            overflow-y: hidden;
            height: 100%;
            font-size: 0.7rem;
            border-color: #aaa #aaa #888;
        }

        .inactive .calendar-button {
            color: #aaa;
            background: #f6f6f8;
            cursor: default;
            border-color: #111 #111 #000;
        }

        .main-area:after {
            content: '';
            position: absolute;
            top: 0;
            bottom: 0;
            left: 0;
            right: 0;
            z-index: -1;
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

            .calendar-body .calendar-weekdays li, .calendar-body .calendar-dates li {
                min-width: 105px !important;
            }
        }

        @media (max-width: 900px) {
            .calendar-body .calendar-weekdays li, .calendar-body .calendar-dates li {
                min-width: 90px !important;
                width: 95px !important;
            }
        }

        @media (max-width: 800px) {
            .calendar-body .calendar-weekdays li, .calendar-body .calendar-dates li {
                min-width: 80px !important;
                width: 90px !important;
            }
        }

        @media (max-width: 550px) {
            .calendar-body .calendar-weekdays li, .calendar-body .calendar-dates li {
                min-width: 85px !important;
                width: 85px !important;
            }
        }

        @media (max-width: 450px) {
            .calendar-body .calendar-weekdays li, .calendar-body .calendar-dates li {
                min-width: 85px !important;
                width: 85px !important;
            }
        }

        @media (max-width: 375px) {
            .calendar-body .calendar-weekdays li, .calendar-body .calendar-dates li {
                min-width: 85px !important;
                width: 85px !important;
            }
        }

        @media (max-width: 350px) {
            .calendar-body .calendar-weekdays li, .calendar-body .calendar-dates li {
                min-width: 85px !important;
                width: 85px !important;
            }
        }

        .ms-header {
            display: flex;
            align-items: center;
            jsutify-content: center;
            font-family: sans-serif;
        }

        .ms-header__title {
            flex: 1 1 100%;
            width: 100%;
            text-align: center;
            font-size: 2rem;
            /* font-weight: bold; */
            color: black;
            text-shadow: 0px 0px 2px rgba(0, 0, 0, 0.4);
        }

        .ms-slider {
            display: inline-block;
            height: 1.5em;
            overflow: hidden;
            vertical-align: middle;
            -webkit-mask-image: linear-gradient(transparent, white, white, white, transparent);
            mask-image: linear-gradient(transparent, white, white, white, transparent);
            mask-type: luminance;
            mask-mode: alpha;
        }

        .ms-slider__words {
            display: inline-block;
            margin: 0;
            padding: 0;
            list-style: none;
            -webkit-animation-name: wordSlider;
            animation-name: wordSlider;
            -webkit-animation-timing-function: ease-out;
            animation-timing-function: ease-out;
            -webkit-animation-iteration-count: infinite;
            animation-iteration-count: infinite;
            -webkit-animation-duration: 7s;
            animation-duration: 7s;
        }

        .ms-slider__word {
            display: block;
            line-height: 1.3em;
            text-align: left;
        }

        @-webkit-keyframes wordSlider {
            0%, 10% {
                transform: translateY(0%);
            }

            10%, 20% {
                transform: translateY(-10%);
            }

            20%, 30% {
                transform: translateY(-20%);
            }

            30%, 40% {
                transform: translateY(-30%);
            }

            40%, 50% {
                transform: translateY(-40%);
            }

            50%, 60% {
                transform: translateY(-50%);
            }

            60%, 70% {
                transform: translateY(-60%);
            }

            70%, 80% {
                transform: translateY(-70%);
            }

            80%, 90% {
                transform: translateY(-80%);
            }

            90%, 100% {
                transform: translateY(-90%);
            }
        }

        @keyframes wordSlider {
            0%, 10% {
                transform: translateY(0%);
            }

            10%, 20% {
                transform: translateY(-10%);
            }

            20%, 30% {
                transform: translateY(-20%);
            }

            30%, 40% {
                transform: translateY(-30%);
            }

            40%, 50% {
                transform: translateY(-40%);
            }

            50%, 60% {
                transform: translateY(-50%);
            }

            60%, 70% {
                transform: translateY(-60%);
            }

            70%, 80% {
                transform: translateY(-70%);
            }

            80%, 90% {
                transform: translateY(-80%);
            }

            90%, 100% {
                transform: translateY(-90%);
            }
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container p-4 mb-5 mt-3 hv-100">
        <div class="row text-left mb-3">
            <div class="col-12 d-inline-flex">
                <p class="membership_title fw-200" id="lbl_204">Support - </p>
                &nbsp;<p class="membership_title fw-500" id="lbl_203">Events & Activities</p>
            </div>
        </div>

        <hr />

        <div class="row mt-4" runat="server">
            <div class="col-lg-12">
                <!-- CALENDAR SECTION -->
                <div class="calendar-wrapper-wrapper">
                    <div class="calendar-container-wrapper">
                        <span id="calendar-prev" class="material-symbols-rounded"><</span>
                        <div class="calendar-container">
                            <div id="calendarContainer" runat="server"></div>
                        </div>
                        <span id="calendar-next" class="material-symbols-rounded">></span>
                    </div>
                </div>
                <!-- CALENDAR SECTION -->
            </div>
        </div>
    </div>
    <script>
        document.querySelector("#calendar-prev").addEventListener("click", function () {
            let currentMonth = parseInt(document.querySelector(".calendar-current-month").dataset.month, 10);
            let currentYear = parseInt(document.querySelector(".calendar-current-year").dataset.year, 10);

            let newMonth = currentMonth - 1;
            let newYear = currentYear;

            if (newMonth < 0) {
                newMonth = 11;
                newYear -= 1;
            }

            window.location.href = `Event.aspx?month=${newMonth}&year=${newYear}`;
        });

        document.querySelector("#calendar-next").addEventListener("click", function () {
            let currentMonth = parseInt(document.querySelector(".calendar-current-month").dataset.month, 10);
            let currentYear = parseInt(document.querySelector(".calendar-current-year").dataset.year, 10);

            let newMonth = currentMonth + 1;
            let newYear = currentYear;

            if (newMonth > 11) {
                newMonth = 0;
                newYear += 1;
            }

            window.location.href = `Event.aspx?month=${newMonth}&year=${newYear}`;
        });

        document.addEventListener('DOMContentLoaded', function () {
            // Add click event listener to all calendar buttons
            document.querySelectorAll('.calendar-button').forEach(function (button) {
                //alert('redirecting...');
                button.addEventListener('click', function () {
                    //alert('redirecting...');
                    var eventId = this.getAttribute('data-event-id');
                    if (eventId) {
                        // Redirect to Announcement.aspx with event ID as a query string parameter
                        window.location.href = 'Event_Details.aspx?Ids=' + encodeURIComponent(eventId);
                    }
                });
            });
        });

        function btn_click(eventID) {
            alert(eventID);
            window.location.href = "Event_Details.aspx?Ids=" + eventID;
        }
    </script>
</asp:Content>


