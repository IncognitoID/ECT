<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Announce_Details.aspx.cs" Inherits="Announce_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="https://fonts.googleapis.com/css?family=Open+Sans:400,700%7CPoppins:400,500" rel="stylesheet">
    <script src="js/jquery-3.3.1.min.js"></script>

    <style>
        section {
            margin: 2% 5%;
            background-color: white;
        }

        .back-btn {
            color: steelblue !important;
        }

        .header-text {
            border-top: 1px solid lightgrey;
            border-bottom: 1px solid lightgrey;
            padding: 5px 0;
        }

        .title-container {
            padding: 20px 0;
        }

        .date-container span {
            display: flex;
            gap: 5px;
        }

        .description-container {
            margin: 20px;
        }

            .description-container span {
                display: grid;
                gap: 20px;
            }

        .poster-container-wrapper {
            display: flex;
            flex-wrap: wrap;
            gap: 10%;
            justify-content: center;
            margin: 3%;
        }

            .poster-container-wrapper .poster-container {
                min-width: 200px;
                max-width: 60%;
                display: block;
                justify-content: center;
            }

                .poster-container-wrapper .poster-container .poster-image {
                    width: 100%;
                }

                    .poster-container-wrapper .poster-container .poster-image img {
                        height: auto;
                        width: 100%;
                    }

                .poster-container-wrapper .poster-container .poster_caption {
                    margin: 28px;
                    text-align: center;
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
        }

        @media (max-width: 800px) {
        }

        @media (max-width: 450px) {
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
                <p class="membership_title fw-200" id="lbl_204">Announcement - </p>
                &nbsp;<p class="membership_title fw-500" id="lbl_203">Announcement Details</p>
            </div>
        </div>

        <hr />

        <div class="row mt-4" runat="server">
            <div class="col-lg-12">

                <div class="top">
                    <div class="back-button-container">
                        <a class="back-btn" href="Announcement.aspx">◀ BACK TO ANNOUNCEMENT</a>
                    </div>

                    <div class="header-text">
                        <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Size="Large"></asp:Label>
                    </div>
                </div>

                <div>
                    <asp:Label ID="lblDescription" runat="server"></asp:Label>
                </div>

                <div class="poster-container-wrapper">
                    <asp:Repeater ID="rptPoster" runat="server">
                        <ItemTemplate>
                            <div class="poster-container">
                                <div class="poster-image">
                                    <img id="poster_image" src='<%# "https://ecentra.com.my/Backoffice/" +  Eval("PosterLink") %>' alt="Poster Image" runat="server" />
                                </div>
                                <div class="poster_caption">
                                    <asp:Label ID="lblCaption" runat="server" Text='<%# Eval("Caption") %>'></asp:Label>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>

                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="gridview-container-wrapper">
                            <div class="gridview-container">
                                <asp:GridView ID="gvPdf" runat="server" AutoGenerateColumns="False" GridLines="Horizontal" EmptyDataText="No PDF available." CssClass="table table-ecommerce-simple mb-0 newtable">
                                    <Columns>
                                        <asp:TemplateField HeaderText="PDF">
                                            <ItemStyle CssClass="tablerRestColumn left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblCaption" runat="server" Text='<%# String.IsNullOrEmpty(Eval("Caption").ToString()) ? "-" : Eval("Caption").ToString() %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemStyle CssClass="tablerRestColumn right" />
                                            <ItemTemplate>
                                            <a id="download" target="_blank" href='<%# "https://ecentra.com.my/Backoffice/" + Server.UrlEncode(Eval("PDFLink").ToString()) %>' 
                                               download='<%# Server.UrlEncode(Eval("PDFLink").ToString()) %>' 
                                               class="button">Download</a>
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
                        <div class="pagination-container">
                            <asp:Button ID="btnPrevious" runat="server" Text="&lt;" OnClick="btnPrevious_Click" />
                            <asp:Label ID="lblPageInfo" runat="server" Text="Page 1 of 1"></asp:Label>
                            <asp:Button ID="btnNext" runat="server" Text="&gt;" OnClick="btnNext_Click" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function BackToPreviousPage() {
            window.location.href = "Announcement.aspx";
        }
    </script>

</asp:Content>

