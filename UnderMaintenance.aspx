<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UnderMaintenance.aspx.cs" Inherits="UnderMaintenance" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="https://fonts.googleapis.com/css?family=Open+Sans:400,700%7CPoppins:400,500" rel="stylesheet">
 <link href="CommingSoon/css/styles.css" rel="stylesheet">
 <link href="CommingSoon/css/responsive.css" rel="stylesheet">
 <script src="js/jquery-3.3.1.min.js"></script>

 <style>
    
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

     @media (max-width: 990px) {
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
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="main-area center-text" style="background-image: url(CommingSoon/images/countdown-3-1600x900.jpg);">

    <div class="display-table">
        <div class="display-table-cell">

            <h1 class="title font-white"><b>Under Maintenance</b></h1>
            <p class="desc font-white" style="font-family: 'Poppins', sans-serif;">
                Our website is currently undergoing scheduled maintenance.
				We Should be back within 15 mins - 20 mins. Thank you for your patience.
            </p>

        </div>
        <!-- display-table -->
    </div>
    <!-- display-table-cell -->
</div>

        </div>
    </form>
</body>
</html>
