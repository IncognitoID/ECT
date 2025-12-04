<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Form" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="https://fonts.googleapis.com/css?family=Open+Sans:400,700%7CPoppins:400,500" rel="stylesheet">
    <script src="js/jquery-3.3.1.min.js"></script>

    <style>
        section {
            margin: 5%;
            background-color: white;
        }

        .form-container-wrapper {
            display: flex;
            flex-wrap: wrap;
            margin: 10px 0;
            scrollbar-width: thin; /* For Firefox */
            scrollbar-color: white transparent; /* For Firefox */
        }

        .form-container {
            display: flex;
            justify-content: flex-start;
            flex-wrap: wrap;
            gap: 20px;
            padding: 0 10px;
            list-style-type: none;
            margin: 20px 0;
            max-height: 30%;
        }


        .card-container {
            width: calc(25% - 20px);
            min-width: 200px;
            box-sizing: border-box;
            color: #FFF;
            background-color: #fff;
            border-radius: 5px;
            text-decoration: none;
            height: 300px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            display: flex;
            flex-direction: column;
            justify-content: space-between; /* Space between top and bottom content */
        }


        .image-container {
            width: 100%;
            height: 65%;
            align-content: center;
        }

            .image-container p {
                margin: auto;
                text-align: center
            }

        .card_image {
            width: 100%;
            height: 100%;
            object-fit: cover;
            border-top-left-radius: 5px;
            border-top-right-radius: 5px;
        }

        .card-bottom-part {
            height: 40%;
            display: flex;
            flex-direction: column;
            justify-content: space-between;
        }

        .form-text {
            display: block;
        }

        .form-type-container {
            padding: 0;
            margin: auto;
            color: saddlebrown;
            font-size: 0.7rem;
        }

        .form_type {
            margin-left: 5%;
        }

        .form-title-container {
            padding: 0;
            margin: auto;
            color: #000;
            text-align: center;
        }

        .form-button-container {
            display: inline-flex;
            flex-wrap: nowrap;
            justify-content: space-evenly;
            border-top: 1px solid #efe8e8;
        }

        .form-button {
            text-align: center; /* Center-align the button */
            width: 50%;
        }

        .button {
            padding: 10px 20px;
            border: none;
            color: black !important;
            text-decoration: none;
            text-align: center;
            display: inline-block;
            cursor: pointer;
            width: 100%;
            font-size: 0.7rem;
        }

            .button:hover {
                text-decoration: underline;
            }

            .button[disabled] {
                pointer-events: none; /* Prevent clicks */
                opacity: 0.5; /* Make the button appear disabled */
                background-color: #d3d3d3; /* Optional: Change background color */
                cursor: not-allowed; /* Change cursor to indicate non-clickable */
            }


        #view {
            border-right: 0.5px solid #efe8e8;
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
            padding: 0px 30px; /* added padding for spacing */
        }

        .lightbox_header_download {
            margin: auto;
        }

        .lightbox_content {
            display: flex;
            justify-content: center;
            align-items: center;
            height: calc(100vh - 0px); /* adjusted for full height minus header */
            padding: 0%;
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

        @media (max-width: 450px) {

            .form-container {
                justify-content: center;
            }

            .card-container {
                min-width: 220px
            }
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container p-4 mb-5 mt-3 hv-100">
        <div class="row text-left mb-3">
            <div class="col-12 d-inline-flex">
                <p class="membership_title fw-200" id="lbl_204">Support - </p>
                &nbsp;<p class="membership_title fw-500" id="lbl_203">Form</p>
            </div>
        </div>

        <hr />

        <div class="row mt-4" runat="server">
            <div class="col-lg-12">
                <!-- CARD -->
                <div class="form-container-wrapper">
                    <div class="form-container">
                        <asp:Repeater ID="rptForm" runat="server">
                            <ItemTemplate>
                                <div class="card-container">
                                    <div class="image-container" style="background-color: grey">
                                        <asp:Label ID="lblFormType" runat="server" Style="display: grid; align-content: center; text-align: center; font-size: 2rem;" Text='<%# Eval("FormType") %>'></asp:Label>
                                    </div>
                                    <div class="card-bottom-part">
                                        <div class="form-text">
                                            <div class="form-type-container">
                                                <span class="form_type"><%# Eval("FormType") %></span>
                                            </div>
                                            <div class="form-title-container">
                                                <span class="form-title"><%# Eval("Title") %></span>
                                            </div>
                                        </div>
                                        <div class="form-button-container">
                                            <div class="form-button">
                                                <a id="view" href="javascript:void(0);" class="button" data-toggle="modal" data-target="#viewModal"
                                                    data-name='<%# Eval("Title") %>'
                                                    data-media-type='<%# Eval("FormType") %>' data-media-link='<%# Eval("FormLink") %>'>View</a>
                                            </div>

                                            <div class="subcontent-button">
                                                <a id="download" target="_blank" 
                                                   href='<%# "https://ecentra.com.my/Backoffice/" + Eval("FormLink") %>' 
                                                   download='<%# Eval("FormLink").ToString().Substring(Eval("FormLink").ToString().LastIndexOf("/") + 1) %>' 
                                                   class="button" 
                                                   <%# Eval("FormType").ToString().ToLower() == "video" ? "disabled='disabled'" : "" %>>
                                                   Download
                                                </a>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                <!-- CARD -->

                <!-- MODAL -->
                <div class="modal fade" id="viewModal" tabindex="-1" role="dialog" aria-labelledby="viewModalLabel" aria-hidden="true">
                    <div class="modal-dialog modal-dialog-centered" role="document">
                        <div class="lightbox">
                            <div class="lightbox_header">
                                <div class="lightbox_header_download">
                                    <a id="download2" href='<%# Eval("FormLink") %>' class="download2"
                                        <%# Eval("FormType").ToString().ToLower() == "video" ? "disabled='disabled'" : "" %>>Download
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
                var name = button.data('name'); // Use 'name' to match the data-name attribute
                var mediaType = button.data('media-type'); // Use 'media-type' to match the data-media-type attribute
                var mediaLink = button.data('media-link'); // Use 'media-link' to match the data-media-link attribute

                var modal = $(this);
                modal.find('.form-title').text(name);
                var contentArea = modal.find('.lightbox_content_media');

                // Clear previous content
                contentArea.html('');

                // Determine content based on media type
                if (mediaType === 'Image') {
                    contentArea.html('<img src="https://ecentra.com.my/Backoffice/' + mediaLink + '" class="img-fluid" alt="' + name + '"/>');
                } else if (mediaType === 'Video') {
                    contentArea.html('<video controls><source src="https://ecentra.com.my/Backoffice/' + mediaLink + '" type="video/mp4">Your browser does not support the video tag.</video>');
                } else if (mediaType === 'PDF') {
                    contentArea.html('<iframe src="https://ecentra.com.my/Backoffice/' + mediaLink + '" style="height: 95vh; width: 80vw;" frameborder="0"></iframe>');
                } else {
                    contentArea.html('<p>Unsupported media type.</p>');
                }
            });
        });

    </script>
</asp:Content>

