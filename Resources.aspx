<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Resources.aspx.cs" Inherits="Resources" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="https://fonts.googleapis.com/css?family=Open+Sans:400,700%7CPoppins:400,500" rel="stylesheet">
    <script src="js/jquery-3.3.1.min.js"></script>

    <style>
        section {
            background-color: white;
            margin: 5% 7%;
        }

        ::-webkit-scrollbar-thumb {
            background: black; /* Color of the scrollbar thumb */
            border-radius: 10px; /* Round the corners of the scrollbar thumb */
        }

        ::-webkit-scrollbar-track {
            background: transparent; /* Background of the scrollbar track */
            border-radius: 10px;
        }

        .title {
            font-family: 'Poppins', sans-serif;
            color: #000;
            text-align: left;
        }

        .tab-container-wrapper::-webkit-scrollbar {
            height: 5px; /* Custom height for horizontal scrollbar */
        }

        .tab-container-wrapper::-webkit-scrollbar-thumb {
            background: #efe8e8; /* Color of the scrollbar thumb */
            border-radius: 10px; /* Round the corners of the scrollbar thumb */
        }

            .tab-container-wrapper::-webkit-scrollbar-thumb:hover {
                background: black; /* Color of the scrollbar thumb */
            }

        .tab-container-wrapper::-webkit-scrollbar-track {
            background: transparent; /* Background of the scrollbar track */
            border-bottom: 2px solid #efe8e8;
        }

        .tab-container-wrapper {
            margin: 0;
            overflow-x: auto;
            margin-bottom: 30px;
        }

        .tab-container {
            display: flex;
            justify-content: flex-start;
            flex-wrap: nowrap;
            gap: 10px;
            padding: 0 10px;
            list-style-type: none;
            margin: 0;
            margin-top: 0%;
        }

            .tab-container li {
                flex: none;
                display: inline-block;
                border-bottom: 5px solid white;
            }

                .tab-container li a {
                    padding-bottom: 5%;
                }

                .tab-container li.active-tab {
                    border-bottom: 5px solid black;
                }

                    .tab-container li.active-tab a {
                        color: black;
                    }

            .tab-container a {
                display: inline-block;
                padding: 0 20px;
                color: grey;
                border-radius: 5px;
                text-decoration: none;
                height: 100%;
                line-height: 20px;
                white-space: nowrap;
                font-size: 1rem;
                font-weight: bold;
            }

                .tab-container a:hover {
                    color: black;
                }

        .third_header {
            display: flex;
            justify-content: space-between; /* Distribute space between items */
            align-items: center; /* Vertically center items */
            padding-bottom: 10px;
        }

        /* Container for dropdown to ensure it matches tab container styling */
        .filter-dropdown {
            display: inline-block;
            padding: 10px 20px; /* Match padding to tabs */
            font-size: 1rem;
            color: #000; /* Text color */
            background-color: #fff; /* Background color matching tabs */
            border: none;
            font-family: 'Poppins', sans-serif; /* Font matching tabs */
            font-weight: bold;
            text-align: center; /* Center text */
            height: auto; /* Adjust height automatically */
            line-height: normal; /* Reset line-height */
            white-space: nowrap; /* Prevent text from wrapping */
            cursor: pointer; /* Pointer cursor for dropdown */
            width: 200px;
        }

            .filter-dropdown option {
                padding: 10px 20px; /* Match padding to tabs */
                background-color: #f1f1f1; /* Background color */
                color: #000; /* Text color */
            }

                /* Additional styling for dropdown when selected */
                .filter-dropdown option:checked {
                    background-color: #ddd; /* Highlighted background color */
                    color: #000; /* Highlighted text color */
                }

        .sort-container {
            display: flex;
            align-items: center; /* Vertically center items */
            position: relative;
        }

        .sort-text {
            padding: 10px;
        }

        .sort-button {
            padding: 10px 20px;
            font-size: 16px;
            border: 1px solid #ccc;
            border-radius: 25px; /* Rounded corners */
            color: white;
            cursor: pointer;
            text-decoration: none;
            background-color: #000; /* Default background color */
        }

            .sort-button:hover {
                background-color: #444; /* Darker color on hover */
                border: 1px solid #000;
            }

        #sort_icon {
            max-width: 10px;
        }

        .dropdown-content {
            display: none; /* Initially hidden */
            position: absolute;
            background-color: white;
            min-width: 200px;
            box-shadow: 0px 8px 16px 0px rgba(0, 0, 0, 0.2);
            z-index: 1;
            padding: 12px 16px;
            border: 1px solid #ccc;
            border-radius: 4px;
        }

        .dropdown-section {
            margin-bottom: 12px;
        }

            .dropdown-section p {
                margin-bottom: 8px;
                font-weight: bold;
            }

            .dropdown-section label {
                margin-right: 8px;
            }

        .dropdown-content button {
            padding: 5px 10px;
            font-size: 14px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            background-color: #000;
            color: white;
        }

        .content-container-wrapper {
            display: flex;
            flex-wrap: wrap;
            overflow-x: auto;
            margin-bottom: 30px;
            scrollbar-width: thin; /* For Firefox */
            scrollbar-color: white transparent; /* For Firefox */
            text-align: center; /* Center the flex container */
        }

        .content-container {
            display: flex; /* Align items in a row and center them */
            justify-content: flex-start; /* Center items horizontally */
            flex-wrap: wrap;
            border: hidden;
            border-radius: 5px;
            flex-wrap: wrap; /* Allow wrapping of items */
            gap: 20px; /* Gap between items */
            padding: 0 10px; /* Padding for edges */
            list-style-type: none;
            margin: 20px 0; /* Margin to add space above and below the tabs */
            max-height: 30%;
        }

        .card-container {
            display: flex;
            flex-direction: column;
            width: calc(25% - 20px);
            min-width: 160px;
            box-sizing: border-box;
            padding: 0;
            color: #FFF;
            background-color: #fff;
            border-radius: 5px;
            text-decoration: none;
            height: 330px;
            width: 230px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }

            .card-container:hover {
                background-color: #ddd;
            }

        .image-container {
            width: 100%;
            height: 65%;
        }

        .card_image {
            width: 100%;
            height: 100%;
            object-fit: contain;
            border-top-left-radius: 5px;
            border-top-right-radius: 5px;
        }

        .card-bottom-part {
            height: 35%;
            display: flex;
            flex-direction: column;
            justify-content: space-between;
            display: block;
            padding: 1%;
        }

        .content-tab-container {
            padding: 0;
            margin: auto;
            color: saddlebrown;
            font-size: 0.7rem;
            justify-content: flex-start;
            display: flex;
        }

        .content_tab {
            margin-left: 5%;
        }

        .content-date-container {
            padding: 0;
            margin: auto;
            color: saddlebrown;
            font-size: 0.7rem;
            justify-content: flex-start;
            display: flex;
        }

        .content_date {
            margin-left: 5%;
        }

        .content-name-container {
            margin: auto 2%;
            height: 60%;
            display: flex;
            flex-direction: column;
            justify-content: space-between;
            color: #000;
            text-align: left;
            line-height: 1.3;
            font-size: 0.8rem;
        }

        .content_name {
            display: block;
            margin: auto;
            margin-left: 3%;
        }

        .pagination-controls {
            display: flex;
            justify-content: center;
            align-items: center;
            margin: 20px 0;
        }

        .pagination-button {
            padding: 10px 20px;
            font-size: 16px;
            border: 1px solid #222;
            border-radius: 5px;
            background-color: #000;
            color: #000;
            cursor: pointer;
            margin: 0 10px;
            display: inline-block;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.5);
        }

            .pagination-button:disabled {
                background-color: #666;
                cursor: not-allowed;
            }

        .pagination-info {
            font-size: 16px;
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

            .title {
                font-family: 'Poppins', sans-serif;
                color: #000;
                text-align: left;
                font-size: 2rem;
            }
        }

        @media (max-width: 800px) {

            .title {
                font-family: 'Poppins', sans-serif;
                color: #000;
                text-align: left;
                font-size: 2rem;
            }
        }

        @media (max-width: 450px) {

            .title {
                font-family: 'Poppins', sans-serif;
                color: #000;
                text-align: left;
                font-size: 2rem;
            }

            .content-container {
                justify-content: center;
            }
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container p-4 mb-5 mt-3 hv-100">
        <div class="row text-left mb-3">
            <div class="col-12 d-inline-flex">
                <p class="membership_title fw-200" id="lbl_204">Resources</p>
            </div>
        </div>

        <hr />

        <div class="row mt-3" runat="server">
            <div class="col-lg-12">
                <div class="tab-container-wrapper">
                    <ul class="tab-container">
                        <asp:Repeater ID="rptResourceNames" runat="server" OnItemCommand="rptResourceNames_ItemCommand" OnItemDataBound="rptResourceNames_ItemDataBound">
                            <ItemTemplate>
                                <li runat="server" id="li">
                                    <asp:LinkButton runat="server" ID="LinkButton" CommandName="FilterContent" CommandArgument='<%# Eval("Code") %>'>
                <%# Eval("Name") %>
                                    </asp:LinkButton>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>

                    </ul>
                </div>

                <div class="third_header">
                    <div class="filter-container">
                        <asp:DropDownList ID="ddlMediaType" runat="server" AutoPostBack="True" CssClass="filter-dropdown" OnSelectedIndexChanged="ddlMediaType_SelectedIndexChanged">
                            <asp:ListItem class="filters" Text="All Media Type" Value=""></asp:ListItem>
                            <asp:ListItem class="filters" Text="PDF" Value="pdf"></asp:ListItem>
                            <asp:ListItem class="filters" Text="Image" Value="image"></asp:ListItem>
                            <asp:ListItem class="filters" Text="Video" Value="video"></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="sort-container">
                        <a class="sort-text">Sort by</a>
                        <a href="#" class="sort-button" onclick="toggleDropdown(event)">
                            <img id="sort_icon" src="img/Icon/sort.png" />
                        </a>
                        <div id="dropdown" class="dropdown-content">
                            <div class="dropdown-section">
                                <p>Select a criterion:</p>
                                <input type="radio" id="contentName" name="criterion" value="Content Name">
                                <label for="contentName">Content Name</label><br>
                                <input type="radio" id="dateCreated" name="criterion" value="Date Created">
                                <label for="dateCreated">Date Created</label>
                            </div>
                            <div class="dropdown-section">
                                <p>Select an order:</p>
                                <input type="radio" id="ascending" name="order" value="Ascending">
                                <label for="ascending">Ascending</label><br>
                                <input type="radio" id="descending" name="order" value="Descending">
                                <label for="descending">Descending</label>
                            </div>
                            <button onclick="applySorting()">Apply</button>

                            <asp:HiddenField ID="hiddenSortBy" runat="server" />
                            <asp:HiddenField ID="hiddenSortOrder" runat="server" />
                            <asp:Button ID="btnSort" runat="server" Style="display: none;" />

                        </div>
                    </div>
                </div>

                <div class="content-container-wrapper">
                    <div class="content-container">
                        <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">
                            <ItemTemplate>
                                <asp:LinkButton ID="linkbtn1" runat="server" CommandName="GoToContentPage" CommandArgument='<%# Eval("Content_Code") %>'>
                                        <div class="card-container">
                                            <div class="image-container">                                                        
                                                <img class="card_image" src='<%# Eval("ImgLink") %>' onerror="this.src='img/Icon/imagenotfound.png'" alt='<%# Eval("Name") %>' />
                                            </div>
                                            <div class="card-bottom-part">
                                                <div class="content-tab-container">
                                                    <span class="content_tab"><%# Eval("ResourceName") %></span>
                                                </div>
                                                <div class="content-name-container">
                                                    <span class="content_name"><%# Eval("Name") %></span>
                                                </div>
                                                <div class="content-date-container">
                                                    <span class="content_date"><%# Eval("Created_DT") %></span>
                                                </div>
                                            </div>
                                        </div>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:Repeater>

                        <div runat="server" id="div_no_record" visible="false" class="row" style="background-color:#efefef;">
                            <div class="col-12">
                                <p>No record found</p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>
    <script>
        function toggleDropdown(event) {
            event.preventDefault(); // Prevent the default action of the link
            const dropdown = document.getElementById("dropdown");
            dropdown.style.display = (dropdown.style.display === "block") ? "none" : "block";
        }

        function applySorting() {
            const criterion = document.querySelector('input[name="criterion"]:checked');
            const order = document.querySelector('input[name="order"]:checked');

            if (criterion && order) {
                const selectedCriterion = criterion.value;
                const selectedOrder = order.value === "Ascending" ? "ASC" : "DESC";

                // Set hidden field values
                document.getElementById('<%= hiddenSortBy.ClientID %>').value = selectedCriterion;
                document.getElementById('<%= hiddenSortOrder.ClientID %>').value = selectedOrder;

                // Make a postback to update the content based on sorting criteria
                __doPostBack('<%= btnSort.ClientID %>', '');

                console.log(`Sorting by ${selectedCriterion} in ${selectedOrder} order`);

                // Close the dropdown after applying sorting
                document.getElementById("dropdown").style.display = "none";
            } else {
                alert("Please select both a criterion and an order.");
            }
        }

        // Close the dropdown if the user clicks outside of it
        window.onclick = function (event) {
            if (!event.target.matches('.sort-button') && !event.target.matches('.sort-container *')) {
                const dropdown = document.getElementById("dropdown");
                if (dropdown.style.display === "block") {
                    dropdown.style.display = "none";
                }
            }
        }

        document.addEventListener("DOMContentLoaded", function () {
            // Set active tab based on ViewState
            var activeTabCode = '<%= ViewState["selectedFilterCode"] %>'; // Ensure this matches server-side ViewState key
            var tabs = document.querySelectorAll('.tab-container li');

            tabs.forEach(function (tab) {
                var commandArgument = tab.querySelector('asp:LinkButton').getAttribute('CommandArgument');
                if (commandArgument === activeTabCode) {
                    tab.classList.add('active-tab'); // Add 'active-tab' class to the active tab
                } else {
                    tab.classList.remove('active-tab'); // Ensure inactive tabs have the 'inactive-tab' class
                    tab.classList.add('inactive-tab');
                }
            });

            // Handle click events
            tabs.forEach(function (tab) {
                tab.addEventListener('click', function () {
                    tabs.forEach(function (t) {
                        t.classList.remove('active-tab'); // Remove 'active-tab' from all tabs
                        t.classList.add('inactive-tab'); // Add 'inactive-tab' to all tabs
                    });
                    this.classList.add('active-tab'); // Add 'active-tab' to the clicked tab
                    this.classList.remove('inactive-tab'); // Ensure 'inactive-tab' is removed
                });
            });
        });

    </script>

</asp:Content>

