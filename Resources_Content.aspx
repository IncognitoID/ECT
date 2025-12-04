<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Resources_Content.aspx.cs" Inherits="Resources_Content" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="https://fonts.googleapis.com/css?family=Open+Sans:400,700%7CPoppins:400,500" rel="stylesheet">
    <script src="js/jquery-3.3.1.min.js"></script>

    <style>

        .content-name-container{
            font-family: 'Poppins', sans-serif;
            padding-bottom: 2%;
        }

        .content_name{
            font-size: 2rem;
            padding-bottom: 1.5rem !important;
            color: saddlebrown;
        }

        .content-desc-container{
            font-family: 'Poppins', sans-serif;
            padding-left: 5%;
        }

        .content_desc{
            font-size: 1rem;
        }

        .content-image-container{
            padding: 40px 10%;
            padding-bottom: 100px;
            border-bottom: 1px solid grey;
            display: none;
        }

        .content_image{
            width: 100%;
            padding: 0 5%;
        }

        .subcontent-container-wrapper {
            display: flex;
            flex-wrap: wrap;
            margin: 0% auto;
            scrollbar-width: thin; /* For Firefox */
            scrollbar-color: white transparent; /* For Firefox */
        }

        .subcontent-container {
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
            width: 100%;
            min-width: 160px;
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
            border-radius: 5px;
        }

        .image-container {
            width: 100%;
            height: 65%;
            align-content: center;
            border-radius: 5px;
        }

        .image-container p{
            margin: auto;
            text-align: center
        }

        .card_image {
            width: 100%;
            height: 100%;
            object-fit: contain;
            border-top-left-radius: 5px;
            border-top-right-radius: 5px;
        }

        .card-bottom-part {
            height: 40%;
            display: flex;
            flex-direction: column;
            justify-content: space-between;
        }

        .subcontent-text{
            display: block;
            max-width: 230px;
        }

        .subcontent-type-container {
            padding: 0;
            margin: auto;
            color: saddlebrown;
            font-size: 0.7rem;
        }
        .subcontent_type{
            margin-left: 5%;
        }

        .subcontent-name-container {
            padding: 0;
            margin: auto;
            color: #000;
            text-align: center;
            white-space: nowrap;
            overflow: hidden;
            text-overflow: ellipsis;
        }

        .subcontent-button-container{
            display: inline-flex;
            flex-wrap: nowrap;
            justify-content: space-evenly;
            border-top: 1px solid #efe8e8;
        }

        .subcontent-button {
            text-align: center; /* Center-align the button */
            width: 100%;
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


        #view{
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
            padding: 0 10px; /* added padding for spacing */
        }

        .lightbox_header_download{
            margin: auto;
        }

        .lightbox_content {
            display: flex;
            justify-content: center;
            align-items: center;
            height: calc(100vh - 48px); /* adjusted for full height minus header */
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
        .lightbox_content_media video{
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

        .download2{
            color: white;
        }

        .download2:hover{
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
            .subcontent-container-wrapper{
                margin: 7% auto;
            }

            .card-container{
                min-width: 220px
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container p-4 mb-5 mt-3 hv-100">
        <div class="row text-left mb-3">
            <div class="col-12 d-inline-flex">
                <p class="membership_title fw-200" id="lbl_204">Resources -</p>
                &nbsp;<p class="membership_title fw-500" id="lbl_203">Resources Details</p>
            </div>
        </div>

        <hr />

        <div class="row mt-3" runat="server">
            <div class="col-lg-12">
                    <div class="content-text">
                        <div class="content-name-container">
                            <h4 id="content_name" runat="server"></h4>
                        </div>
                        <div>
                            <p id="content_desc" runat="server" class="content_desc"></p>
                        </div>
                    </div>
                    <div class="content-image-container">
                        <img id="content_image"  runat="server" class="content_image" src='<%# "https://ecentra.com.my/backoffice/" + Eval("ImgLink") %>' onerror="this.src='img/Icon/imagenotfound.png'" alt='<%# Eval("Name") %>'/>
                    </div>
                    <div class="subcontent-container-wrapper">
                        <div class="subcontent-container">
                            <asp:Repeater ID="rpt_content" runat="server">
                                <ItemTemplate>
                                    <div class="card-container">
                                        <div class="image-container" style="background-color: grey">
                                            <%# Eval("MediaType").ToString() == "Image" ? 
                                            "<img class='card_image' src='https://ecentra.com.my/backoffice/" + Eval("Thumbnail") + "' onerror=\"this.src='img/Icon/imagenotfound.png'\" alt='" + Eval("Name") + "' />" :
                                            "<asp:Label runat='server' Style='display: grid; align-content: center; text-align: center; font-size: 2rem;'>" + Eval("MediaType") + "</asp:Label>" %>
                                        </div>
                                        <div class="card-bottom-part">
                                            <div class="subcontent-text">
                                                <div class="subcontent-type-container">
                                                    <span class="subcontent_type"><%# Eval("MediaType") %></span>
                                                </div>
                                                <div class="subcontent-name-container">
                                                    <span class="subcontent_name"><%# Eval("Name") %></span>
                                                </div>
                                            </div>
                                            <div class="subcontent-button-container">
                                                <div class="subcontent-button">
                                                    <a id="view" href="javascript:void(0);" class="button" data-toggle="modal" data-target="#viewModal"
                                                       data-name='<%# Eval("Name") %>'
                                                       data-media-type='<%# Eval("MediaType") %>' data-media-link='<%# "https://ecentra.com.my/backoffice/" + Eval("MediaLink") %>'>View</a>
                                                </div>

                                                <div class="subcontent-button">
                                                    <a id="download" target="_blank" 
                                                       href='<%# "https://ecentra.com.my/backoffice/" + Eval("MediaLink") %>' 
                                                       download='<%# Eval("MediaLink") %>' 
                                                       class="button" 
                                                       <%# Eval("MediaType").ToString().ToLower() == "video" ? "disabled='disabled'" : "" %>>
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

                    <div class="modal fade" id="viewModal" tabindex="-1" role="dialog" aria-labelledby="viewModalLabel" aria-hidden="true">
                      <div class="modal-dialog modal-dialog-centered" role="document">
                          <div class="lightbox">
                              <div class="lightbox_header">
                                  <div class="lightbox_header_download">
                                      <a id="download2" href='<%# Eval("MediaLink") %>' class="download2" 
                                                       <%# Eval("MediaType").ToString().ToLower() == "video" ? "disabled='disabled'" : "" %>>
                                          Download
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

            </div>
        </div>
    </div>

    <script>
        $(document).ready(function () {
            $('#viewModal').on('show.bs.modal', function (event) {
                var button = $(event.relatedTarget);
                var name = button.data('name');
                var mediaType = button.data('media-type');
                var mediaLink = button.data('media-link');

                var modal = $(this);
                modal.find('.modal-title').text(name);
                var contentArea = modal.find('.lightbox_content_media');

                // Clear previous content
                contentArea.html('');

                // Determine content based on media type
                if (mediaType === 'Image') {
                    contentArea.html('<img src="https://ecentra.com.my/backoffice/' + mediaLink + '" class="img-fluid" alt="' + name + '"/>');
                } else if (mediaType === 'Video') {
                    contentArea.html('<video controls><source src="https://ecentra.com.my/backoffice/' + mediaLink + '" type="video/mp4">Your browser does not support the video tag.</video>');
                } else if (mediaType === 'PDF') {
                    contentArea.html('<iframe src="' + mediaLink + '"" style="height: 80vh; width: 80vw;" frameborder="0"></iframe>');
                } else {
                    contentArea.html('<p>Unsupported media type.</p>');
                }
            });
        });


    </script>
</asp:Content>