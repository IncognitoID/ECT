<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Network_Tree.aspx.cs" Inherits="Network_Tree" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="https://kit.fontawesome.com/58a2e30ed2.js" crossorigin="anonymous"></script>

    <style>
        .tree-container {
            position: relative;
        }

        .line-left::after {
            content: '';
            position: absolute;
            top: 265%;
            left: 0;
            transform: translateY(-200%) translateX(115%);
            background-image: url(img/NetworkTree/main_left_line.png);
            background-repeat: no-repeat;
            background-position: center;
            background-size: contain;
            width: 22%;
            height: 100%;
        }

        .line-right::before {
            content: '';
            position: absolute;
            top: 265%;
            right: 0;
            transform: translateY(-200%) translateX(-115%);
            background-image: url(img/NetworkTree/main_right_line.png);
            background-repeat: no-repeat;
            background-position: center;
            background-size: contain;
            width: 22%;
            height: 100%;
        }

        .line-sub-left::after {
            content: '';
            position: absolute;
            top: 270%;
            left: 0;
            transform: translateY(-200%) translateX(145%);
            background-image: url(img/NetworkTree/sub_left_line.png);
            background-repeat: no-repeat;
            background-position: center;
            background-size: contain;
            width: 18%;
            height: 100%;
        }

        .line-sub-right::before {
            content: '';
            position: absolute;
            top: 270%;
            right: 0;
            transform: translateY(-200%) translateX(-145%);
            background-image: url(img/NetworkTree/sub_right_line.png);
            background-repeat: no-repeat;
            background-position: center;
            background-size: contain;
            width: 18%;
            height: 100%;
        }

        .w-60 {
            width: 60px;
        }

        .w-75 {
            width: 75px !important;
        }

        .p-50 {
            padding: 30px 0px;
            margin: 0px;
        }

        .p-root{
            padding: 0px 0px 15px 0px;
            margin: 0px;
        }

        .table-container {
            overflow-x: visible; /* Enable horizontal scrolling */
            width: 100%; /* Ensure the container fills the available space */
        }

        /* CSS for table */
        .tree-container {
            width: 100%; /* Ensure the table fills the container */
            /* Other styles for the table */
        }

        .member_details {
            text-align: center;
            position: absolute;
            margin: auto;
            left: 120px;
            right: 0;
        }

        @media only screen and (max-width: 1200px) {

            .table-container {
                overflow-x: scroll; /* Enable horizontal scrolling */
                width: 100%; /* Ensure the container fills the available space */
            }

            /* CSS for table */
            .tree-container {
                width: 150%; /* Ensure the table fills the container */
                /* Other styles for the table */
            }

            .member_details {
                text-align: center;
                position: absolute;
                margin: auto;
                left: 85px;
                right: 0;
            }
        }

        @media only screen and (max-width: 767px) {
            .membership_title {
                font-size: 16px;
                color: black;
            }

            .table-container {
                overflow-x: scroll; /* Enable horizontal scrolling */
                width: 100%; /* Ensure the container fills the available space */
            }

            /* CSS for table */
            .tree-container {
                width: 300%; /* Ensure the table fills the container */
                /* Other styles for the table */
            }

            .member_details {
                text-align: center;
                position: absolute;
                margin: auto;
                left: 85px;
                right: 0;
            }
        }

        @media only screen and (max-width: 300px) {
            .membership_title {
                font-size: 16px;
                color: black;
            }

            .table-container {
                overflow-x: scroll; /* Enable horizontal scrolling */
                width: 100%; /* Ensure the container fills the available space */
            }

            /* CSS for table */
            .tree-container {
                width: 300%; /* Ensure the table fills the container */
                /* Other styles for the table */
            }
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class=" table-container">
        <div class="tree-container" style="margin-bottom: 15rem;">
            <div class="container">
                <div class="row">
                    <div class="col-xl-6 col-12 text-left d-inline-flex mt-3 mb-3">
                        <p class="membership_title fw-200" id="lbl_139">Registration - </p>
                        &nbsp;<p class="membership_title fw-500" id="lbl_138">Binary Tree View</p>
                    </div>
                    <div class="col-xl-6 col-12 text-right mt-3 mb-3">
                        <span id="lbl_133">Filter By: &nbsp;</span>
                        <div class="d-inline-flex">
                            <asp:TextBox runat="server" ID="txt_filter" Placeholder="Name / ID / IC" Style="text-align: center;"></asp:TextBox>
                            <div style="padding: 5px 10px 5px 10px; background-color: #d3d32f; border-radius: 0px 5px 5px 0px;">
                                <span><i class="fa-solid fa-magnifying-glass" style="font-size: 15px; color: white;"></i></span>
                            </div>
                        </div>
                    </div>
                </div>

            <hr style="margin: 10px 0px;" />

            <div class="row" runat="server" id="div_back">
                <div class="col-12">
                    <div onclick="BackToPreviousPage()">
                        <label style="padding: 7px 15px; background-color: #232020; color: white; border-radius: 5px;">Back</label>
                    </div>
                </div>
            </div>
            </div>

            <!-- Root Node -->
            <div class="row p-root">
                <div class="col line-left line-right">
                    <div class="text-center mb-2">
                        <img runat="server" id="img_main" src="img/NetworkTree/Icon2.png" class="w-60" />
                    </div>
                    <div runat="server" id="div_member_details" style="text-align: center; position: absolute; margin: auto; left: 120px; right: 0;">
                        <div style="width: 200px; margin: auto; text-align: left; font-size: 12px;">
                            <div style="line-height: 1.5;">
                                <div>
                                    <asp:Label runat="server" ID="lbl_selected_member_name"></asp:Label>
                                </div>
                                <div><span id="lbl_135">ID : </span>&nbsp;<asp:Label runat="server" ID="lbl_selected_member_id"></asp:Label></div>
                                <div><span id="lbl_136">Rank : </span>&nbsp;<asp:Label runat="server" ID="lbl_selected_member_rank"></asp:Label></div>
                            </div>

                            <div class="mt-2" id="lbl_137">Daily BV:</div>
                            <div class="d-inline-flex" style="border-top: 1px solid #9683cc; border-bottom: 1px solid #9683cc; padding: 5px 0px; line-height: 1.3; width: 140px;">
                                <div style="border-right: 1px solid #9683cc; padding: 0px 10px 0px 0px; width: 70px;">
                                    <span>Left:</span>
                                    <br />
                                    <span>
                                        <asp:Label runat="server" ID="lbl_selected_member_left_point"></asp:Label></span>
                                </div>
                                <div style="padding: 0px 0px 0px 10px;">
                                    <span>Right:</span>
                                    <br />
                                    <span>
                                        <asp:Label runat="server" ID="lbl_selected_member_right_point"></asp:Label></span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- First Level -->
            <div class="row p-50">
                <div runat="server" id="div_first_level_left_1" class="col line-sub-left line-sub-right">
                    <asp:HiddenField runat="server" ID="hdn_first_level_left_id" />
                    <div class="text-center mb-2">
                        <img runat="server" id="img_first_level_left" src="img/NetworkTree/Icon.png" class="w-60" />
                    </div>
                    <div runat="server" id="div_first_level_left" class="member_details">
                        <div style="width: 200px; margin: auto; text-align: left; font-size: 12px;">
                            <div style="line-height: 1.5;">
                                <div>
                                    <asp:Label runat="server" ID="lbl_first_level_left_member_name"></asp:Label>
                                </div>
                                <div><span id="lbl_146">ID : </span>&nbsp;<asp:Label runat="server" ID="lbl_first_level_left_member_id"></asp:Label></div>
                                <div><span id="lbl_147">Rank : </span>&nbsp;<asp:Label runat="server" ID="lbl_first_level_left_member_rank"></asp:Label></div>
                            </div>
                            <div class="mt-2" id="lbl_140">Daily BV:</div>
                            <div class="d-inline-flex" style="border-top: 1px solid #9683cc; border-bottom: 1px solid #9683cc; padding: 5px 0px; line-height: 1.3; width: 140px;">
                                <div style="border-right: 1px solid #9683cc; padding: 0px 10px 0px 0px; width: 70px;">
                                    <span>Left:</span>
                                    <br />
                                    <span>
                                        <asp:Label runat="server" ID="lbl_first_level_left_left_point"></asp:Label></span>
                                </div>
                                <div style="padding: 0px 0px 0px 10px;">
                                    <span>Right:</span>
                                    <br />
                                    <span>
                                        <asp:Label runat="server" ID="lbl_first_level_left_right_point"></asp:Label></span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div runat="server" id="div_first_level_right_1" class="col line-sub-left line-sub-right">
                    <asp:HiddenField runat="server" ID="hdn_first_level_right_id" />
                    <div class="text-center mb-2">
                        <img runat="server" id="img_first_level_right" src="img/NetworkTree/Icon.png" class="w-60" />
                    </div>
                    <div runat="server" id="div_first_level_right" class="member_details">
                        <div style="width: 200px; margin: auto; text-align: left; font-size: 12px;">
                            <div style="line-height: 1.5;">
                                <div>
                                    <asp:Label runat="server" ID="lbl_first_level_right_member_name"></asp:Label>
                                </div>
                                <div><span id="lbl_148">ID : </span>&nbsp;<asp:Label runat="server" ID="lbl_first_level_right_member_id"></asp:Label></div>
                                <div><span id="lbl_149">Rank : </span>&nbsp;<asp:Label runat="server" ID="lbl_first_level_right_member_rank"></asp:Label></div>
                            </div>
                            <div class="mt-2" id="lbl_141">Daily BV:</div>
                            <div class="d-inline-flex" style="border-top: 1px solid #9683cc; border-bottom: 1px solid #9683cc; padding: 5px 0px; line-height: 1.3; width: 140px;">
                                <div style="border-right: 1px solid #9683cc; padding: 0px 10px 0px 0px; width: 70px;">
                                    <span>Left:</span>
                                    <br />
                                    <span>
                                        <asp:Label runat="server" ID="lbl_first_level_right_left_point"></asp:Label></span>
                                </div>
                                <div style="padding: 0px 0px 0px 10px;">
                                    <span>Right:</span>
                                    <br />
                                    <span>
                                        <asp:Label runat="server" ID="lbl_first_level_right_right_point"></asp:Label></span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Second Level -->
            <div class="row p-50">
                <div runat="server" id="div_second_level_left_1" class="col">
                    <asp:HiddenField runat="server" ID="hdn_second_level_1_left_id" />
                    <div class="text-center mb-2">
                        <img runat="server" id="img_second_level_1_left" src="img/NetworkTree/Icon.png" class="w-60" />
                    </div>
                    <div runat="server" id="div_second_level_1_left" class="member_details">
                        <div style="width: 200px; margin: auto; text-align: left; font-size: 12px;">
                            <div style="line-height: 1.5;">
                                <div>
                                    <asp:Label runat="server" ID="lbl_second_level_1_left_member_name"></asp:Label>
                                </div>
                                <div><span id="lbl_150">ID : </span>&nbsp;<asp:Label runat="server" ID="lbl_second_level_1_left_member_id"></asp:Label></div>
                                <div><span id="lbl_151">Rank : </span>&nbsp;<asp:Label runat="server" ID="lbl_second_level_1_left_member_rank"></asp:Label></div>
                            </div>
                            <div class="mt-2" id="lbl_142">Daily BV:</div>
                            <div class="d-inline-flex" style="border-top: 1px solid #9683cc; border-bottom: 1px solid #9683cc; padding: 5px 0px; line-height: 1.3; width: 140px;">
                                <div style="border-right: 1px solid #9683cc; padding: 0px 10px 0px 0px; width: 70px;">
                                    <span>Left:</span>
                                    <br />
                                    <span>
                                        <asp:Label runat="server" ID="lbl_second_level_1_left_left_point"></asp:Label></span>
                                </div>
                                <div style="padding: 0px 0px 0px 10px;">
                                    <span>Right:</span>
                                    <br />
                                    <span>
                                        <asp:Label runat="server" ID="lbl_second_level_1_left_right_point"></asp:Label></span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div runat="server" id="div_second_level_left_2" class="col">
                    <asp:HiddenField runat="server" ID="hdn_second_level_1_right_id" />
                    <div class="text-center mb-2">
                        <img runat="server" id="img_second_level_1_right" src="img/NetworkTree/Icon.png" class="w-60" />
                    </div>
                    <div runat="server" id="div_second_level_1_right" class="member_details">
                        <div style="width: 200px; margin: auto; text-align: left; font-size: 12px;">
                            <div style="line-height: 1.5;">
                                <div>
                                    <asp:Label runat="server" ID="lbl_second_level_1_right_member_name"></asp:Label>
                                </div>
                                <div><span id="lbl_152">ID : </span>&nbsp;<asp:Label runat="server" ID="lbl_second_level_1_right_member_id"></asp:Label></div>
                                <div><span id="lbl_153">Rank : </span>&nbsp;<asp:Label runat="server" ID="lbl_second_level_1_right_member_rank"></asp:Label></div>
                            </div>
                            <div class="mt-2" id="lbl_143">Daily BV:</div>
                            <div class="d-inline-flex" style="border-top: 1px solid #9683cc; border-bottom: 1px solid #9683cc; padding: 5px 0px; line-height: 1.3; width: 140px;">
                                <div style="border-right: 1px solid #9683cc; padding: 0px 10px 0px 0px; width: 70px;">
                                    <span>Left:</span>
                                    <br />
                                    <span>
                                        <asp:Label runat="server" ID="lbl_second_level_1_right_left_point"></asp:Label></span>
                                </div>
                                <div style="padding: 0px 0px 0px 10px;">
                                    <span>Right:</span>
                                    <br />
                                    <span>
                                        <asp:Label runat="server" ID="lbl_second_level_1_right_right_point"></asp:Label></span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div runat="server" id="div_second_level_right_1" class="col">
                    <asp:HiddenField runat="server" ID="hdn_second_level_2_left_id" />
                    <div class="text-center mb-2">
                        <img runat="server" id="img_second_level_2_left" src="img/NetworkTree/Icon.png" class="w-60" />
                    </div>
                    <div runat="server" id="div_second_level_2_left" class="member_details">
                        <div style="width: 200px; margin: auto; text-align: left; font-size: 12px;">
                            <div style="line-height: 1.5;">
                                <div>
                                    <asp:Label runat="server" ID="lbl_second_level_2_left_member_name"></asp:Label>
                                </div>
                                <div><span id="lbl_154">ID : </span>&nbsp;<asp:Label runat="server" ID="lbl_second_level_2_left_member_id"></asp:Label></div>
                                <div><span id="lbl_155">Rank : </span>&nbsp;<asp:Label runat="server" ID="lbl_second_level_2_left_member_rank"></asp:Label></div>
                            </div>
                            <div class="mt-2" id="lbl_144">Daily BV:</div>
                            <div class="d-inline-flex" style="border-top: 1px solid #9683cc; border-bottom: 1px solid #9683cc; padding: 5px 0px; line-height: 1.3; width: 140px;">
                                <div style="border-right: 1px solid #9683cc; padding: 0px 10px 0px 0px; width: 70px;">
                                    <span>Left:</span>
                                    <br />
                                    <span>
                                        <asp:Label runat="server" ID="lbl_second_level_2_left_left_point"></asp:Label></span>
                                </div>
                                <div style="padding: 0px 0px 0px 10px;">
                                    <span>Right:</span>
                                    <br />
                                    <span>
                                        <asp:Label runat="server" ID="lbl_second_level_2_left_right_point"></asp:Label></span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div runat="server" id="div_second_level_right_2" class="col">
                    <asp:HiddenField runat="server" ID="hdn_second_level_2_right_id" />
                    <div class="text-center mb-2">
                        <img runat="server" id="img_second_level_2_right" src="img/NetworkTree/Icon.png" class="w-60" />
                    </div>
                    <div runat="server" id="div_second_level_2_right" class="member_details">
                        <div style="width: 200px; margin: auto; text-align: left; font-size: 12px;">
                            <div style="line-height: 1.5;">
                                <div>
                                    <asp:Label runat="server" ID="lbl_second_level_2_right_member_name"></asp:Label>
                                </div>
                                <div><span id="lbl_156">ID : </span>&nbsp;<asp:Label runat="server" ID="lbl_second_level_2_right_member_id"></asp:Label></div>
                                <div><span id="lbl_157">Rank : </span>&nbsp;<asp:Label runat="server" ID="lbl_second_level_2_right_member_rank"></asp:Label></div>
                            </div>
                            <div class="mt-2" id="lbl_145">Daily BV:</div>
                            <div class="d-inline-flex" style="border-top: 1px solid #9683cc; border-bottom: 1px solid #9683cc; padding: 5px 0px; line-height: 1.3; width: 140px;">
                                <div style="border-right: 1px solid #9683cc; padding: 0px 10px 0px 0px; width: 70px;">
                                    <span>Left:</span>
                                    <br />
                                    <span>
                                        <asp:Label runat="server" ID="lbl_second_level_2_right_left_point"></asp:Label></span>
                                </div>
                                <div style="padding: 0px 0px 0px 10px;">
                                    <span>Right:</span>
                                    <br />
                                    <span>
                                        <asp:Label runat="server" ID="lbl_second_level_2_right_right_point"></asp:Label></span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
    <script type="text/javascript">

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

        function getParameterByName(name, url = window.location.href) {
            name = name.replace(/[\[\]]/g, '\\$&');
            var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
                results = regex.exec(url);
            if (!results) return null;
            if (!results[2]) return '';
            return decodeURIComponent(results[2].replace(/\+/g, ' '));
        }

        function Load_Language() {
            var page = 'Network Tree';
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

        function View_Member_Downline(memberid) {
            if (memberid !== '') {
                var downlineArray = getCookieValue("downlineArray");

                // Initialize array if not set
                if (downlineArray) {
                    downlineArray = JSON.parse(downlineArray);
                } else {
                    downlineArray = [];
                }

                downlineArray.push(memberid);
                document.cookie = "downlineArray=" + JSON.stringify(downlineArray) + "; path=/";
                window.location.href = "Network_Tree.aspx?id=" + encodeURIComponent(memberid);
            }
        }

        function BackToPreviousPage() {
            var downlineArray = getCookieValue("downlineArray");

            if (downlineArray) {
                downlineArray = JSON.parse(downlineArray);
                if (downlineArray.length > 0) {

                    downlineArray.pop(); // Remove the last memberid
                    var previousid = downlineArray[downlineArray.length - 1]; // Get the last memberid

                    // Update the cookie with the modified array
                    if (downlineArray.length > 0) {
                        document.cookie = "downlineArray=" + JSON.stringify(downlineArray) + "; path=/";
                        window.location.href = "Network_Tree.aspx?id=" + encodeURIComponent(previousid);
                    } else {
                        // Clear the cookie if the array is empty
                        document.cookie = "downlineArray=; expires=Thu, 01 Jan 1970 00:00:00 GMT; path=/";
                        window.location.href = "Network_Tree.aspx?id=";
                    }

                } else {
                    // If the array is empty, go to the base tree
                    window.location.href = "Network_Tree.aspx?id=";
                }
            } else {
                // If no downline array found, navigate to base tree
                window.location.href = "Network_Tree.aspx?id=";
            }
        }

        function Register_Member_Downline(referral_id, memberid, side) {
            // Set cookies for memberid and side with 1 hour expiration time
            setCookie("uplineid", memberid, 1);
            setCookie("side", side, 1);

            window.location.href = "Register_Member.aspx?id=";
        }

        function setCookie(name, value, hours) {
            var date = new Date();
            date.setTime(date.getTime() + (hours * 60 * 60 * 1000)); // Convert hours to milliseconds
            var expires = "expires=" + date.toUTCString();
            document.cookie = name + "=" + encodeURIComponent(value) + ";" + expires + ";path=/";
        }

    </script>

</asp:Content>

