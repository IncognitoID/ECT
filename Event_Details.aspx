<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Event_Details.aspx.cs" Inherits="Event_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>

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

        .carousel-item {
            height: 100%;
        }

        .carousel-inner img {
            height: 100%;
            object-fit: fill;
        }

        .carousel-control-prev, .carousel-control-next {
            background: transparent;
            border: none;
        }

        .description-container {
            margin: 20px 0;
            margin-bottom: 50px;
        }

            .description-container span {
                display: grid;
                gap: 20px;
            }

        .modal-dialog {
            margin: 0;
            pointer-events: visible;
            width: 100vw;
            height: 100vh;
        }

        .lightbox {
            position: relative;
            width: 100vw;
            height: 100vh;
        }

        .lightbox_header {
            display: flex;
            justify-content: space-between; /* adjusted to space between elements */
            align-items: center;
            width: 100%;
            height: 48px;
            background-color: #2c2c2c;
            color: #fff;
            padding: 0 10px; /* added padding for spacing */
        }

        .lightbox_header_download {
            margin: auto;
        }

        .lightbox_content {
            display: flex;
            justify-content: center;
            align-items: center;
            height: calc(100vh - 48px); /* adjusted for full height minus header */
            padding: 5%;
        }

        .lightbox_content_media {
            display: flex;
            justify-content: center;
            align-items: center;
            max-height: 100%;
            max-width: 100%;
            overflow: hidden; /* ensures content does not overflow */
        }

            .lightbox_content_media img,
            .lightbox_content_media video {
                max-width: 80vw; /* cap maximum width to 80% of viewport width */
                max-height: 80vh; /* cap maximum height to 80% of viewport height */
                width: auto;
                height: auto;
                display: block; /* block-level display */
            }

        .close-modal-button {
            background-color: transparent;
            border: none;
            font-size: 2rem;
            cursor: pointer;
            color: white;
            outline: none;
            width: auto;
            height: auto;
            margin: 0;
            padding: 0;
        }

            .close-modal-button:hover {
                color: #ff0000;
            }

        .modal {
            margin: 0 auto;
            overflow-x: hidden;
            overflow-y: auto;
            width: 100vw;
            height: 100vh;
        }

        .modal-body {
            padding: 0;
        }

        /* Center and size media items */
        #modal-content-area {
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100%;
        }

            /* Specific style for PDFs (if using iframe) */
            #modal-content-area iframe {
                border: none; /* Remove default iframe border */
            }

        .download2 {
            color: white;
        }

            .download2:hover {
                text-decoration: underline;
            }
        /*=============================*/

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

        @media (max-width: 550px) {
            .carousel-item {
                height: 100%;
            }
        }

        @media (max-width: 450px) {
            .carousel-item {
                height: 100%;
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
                <p class="membership_title fw-200" id="lbl_204">Events & Activities - </p>
                &nbsp;<p class="membership_title fw-500" id="lbl_203">Events & Activities Details</p>
            </div>
        </div>

        <hr />

        <div class="row mt-4" runat="server">
            <div class="col-lg-12">

                <div class="top">
                    <div class="back-button-container">
                        <a class="back-btn" href="Event.aspx">◀ BACK TO EVENTS & ACTIVITIES</a>
                    </div>

                    <div class="header-text">
                        <div class="title-container">
                            <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Size="Large"></asp:Label>
                        </div>
                    </div>
                </div>

                <!-- CAROUSEL -->
                <div class="carousel-container-wrapper">
                    <div class="carousel-container">
                        <div id="carouselExampleRide" class="carousel slide" data-bs-ride="carousel">
                            <div class="carousel-inner">
                                <asp:PlaceHolder ID="CarouselPlaceholder" runat="server"></asp:PlaceHolder>
                            </div>
                            <button class="carousel-control-prev" type="button" data-bs-target="#carouselExampleRide" data-bs-slide="prev">
                                <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                                <span class="visually-hidden"></span>
                            </button>
                            <button class="carousel-control-next" type="button" data-bs-target="#carouselExampleRide" data-bs-slide="next">
                                <span class="carousel-control-next-icon" aria-hidden="true"></span>
                                <span class="visually-hidden"></span>
                            </button>
                        </div>

                    </div>
                </div>
                <!-- CAROUSEL -->

                <!-- DESCRIPTION -->
                <div class="description-container">
                    <asp:Label ID="lblDescription" runat="server"></asp:Label>
                </div>
                <!-- DESCRIPTION -->

                <!-- MODAL -->
                <div class="modal fade" id="viewModal" tabindex="-1" role="dialog" aria-labelledby="viewModalLabel" aria-hidden="true">
                    <div class="modal-dialog modal-dialog-centered" role="document">
                        <div class="lightbox">
                            <div class="lightbox_header">
                                <div class="lightbox_header_download">
                                    <a id="download2" href='<%# Eval("PosterLink") %>' class="download2">Download
                                    </a>
                                </div>
                                <button type="button" class="close close-modal-button" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="lightbox_content">
                                <div class="lightbox_content_media">
                                    <!-- Dynamic content will be injected here -->
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- MODAL -->

            </div>
        </div>
    </div>

    <script>
        $(document).ready(function () {
            $('#viewModal').on('show.bs.modal', function (event) {
                var button = $(event.relatedTarget);
                var name = button.data('name');
                var mediaLink = button.data('media-link');

                var modal = $(this);
                modal.find('.modal-title').text(name);
                var contentArea = modal.find('.lightbox_content_media');

                // Clear previous content
                contentArea.html('');

                // Determine file extension
                var fileExtension = mediaLink.split('.').pop().toLowerCase();

                if (fileExtension === 'pdf') {
                    // Display PDF
                    contentArea.html('<embed src="https://ecentra.com.my/Backoffice/' + mediaLink + '" type="application/pdf" width="100%" height="500px" />');
                } else {
                    // Display image
                    contentArea.html('<img src="https://ecentra.com.my/Backoffice/' + mediaLink + '" class="img-fluid" alt="' + name + '"/>');
                }
            });
        });


    </script>
</asp:Content>

