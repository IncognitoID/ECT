<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Dermafirm_Introduction.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style>
        .content-section {
            padding: 20px;
        }

            .content-section img {
                max-width: 100%;
                height: auto;
            }

            .content-section h2 {
                font-weight: bold;
            }

            .content-section p {
                margin-top: 10px;
            }

        .image-container {
            background: linear-gradient(to right, black 50%, transparent 50%);
        }

        .text-container {
            display: flex;
            align-items: center;
        }

        body {
            font-size: 16px;
        }

        .width-75{
            width:75% !important;
        }

        .div_padding_1{
            padding: 20rem;
        }

        .div_padding_3{
            padding: 5rem 8rem;
        }

        .div_padding_4{
            padding: 5rem 8rem;
        }

        .div_padding_5{
            padding: 15rem;
        }

        .div_padding_6{
            padding:4rem;
        }

        .div_flex{
            flex-wrap: nowrap;
        }

        @media (max-width: 768px) {
            .width-75{
                width:100% !important;
            }

            .div_padding_1{
                padding: 0rem;
            }

            .div_padding_2{
                padding: 0rem;
            }

            .div_padding_3{
                padding: 0rem;
            }

            .div_padding_4{
                padding: 0rem;
            }

            .div_padding_5{
                padding: 0rem;
            }

            .div_padding_6{
                padding:0rem;
            }

            .mobile_reverse{
                flex-direction: column-reverse;
            }

            .div_flex{
                flex-wrap: wrap !important;
            }
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row m-0">
        <div class="col-12 p-0">
            <iframe 
                width="100%" 
                height="700" 
                src="https://www.youtube-nocookie.com/embed/jKRKNgO_5Z0?autoplay=1&loop=1&playlist=jKRKNgO_5Z0&mute=1" 
                title="YouTube video player" 
                frameborder="0" 
                allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" 
                referrerpolicy="strict-origin-when-cross-origin" 
                allowfullscreen>
            </iframe>

            <%--<video runat="server" id="video_1" class="w-100" src="https://www.youtube-nocookie.com/embed/jKRKNgO_5Z0?si=FGky6rgNbcIUNvpg?autoplay=1&loop=1" loop></video>
            <img runat="server" id="banner_1" class="d-block w-100 img-responsive" src="" />--%>
        </div>
    </div>

    <div class="ml-2 mr-2">
        <div class="row m-0">
            <div class="col-lg-6 text-end">
                <img src="https://ecentra.com.my/Backoffice/UploadImage/Dermarfirm_Image/Image_1.jpg" alt="DERMAFIRM Lab Coat" class="img-fluid">
            </div>
            <div class="col-lg-6 m-auto">
                <h2 class="mt-3">DERMAFIRM</h2>
                <p class="width-75 mt-3">DERMAFIRM, established in 2002, operates under the motto: “The most important thing is invisible to the eye.” During that time, the cosmetic industry often lacked transparency regarding the importance and production process of quality raw materials. Consequently, DERMAFIRM was founded as a cosmetic brand committed to producing its own raw materials, validated to be effective through extensive research. Since its inception, DERMAFIRM has focused on manufacturing high-purity and functional raw ingredients in its dedicated production facilities, ensuring the accessibility of trusted derma cosmetics to all.</p>
            </div>
        </div>

        <div class="row m-0 mt-4">
            <div class="col-lg-6 order-lg-2 text-center">
                <img src="https://ecentra.com.my/Backoffice/UploadImage/Dermarfirm_Image/Image_2.png" alt="Lab Equipment" class="img-fluid">
            </div>
            <div class="col-lg-6 order-lg-1 m-auto text-left">
                <h2 class="mt-3">Exclusive Liposome Technology</h2>
                <p class="width-75 mt-3">DERMAFIRM, exclusive patented Liposome technology utilizes innovative nano-capsules to optimize the stability and delivery of specially selected active ingredients, transporting them deep into the dermis in a powerful and efficient manner. Each nano-capsule is 1/100th the size of a pore, allowing it to rapidly and effectively penetrate the skin.</p>
            </div>
        </div>
    </div>

    <div class="row m-0 mt-4">
        <div class="col-12 p-0">
            <video runat="server" id="video_2" class="w-100" muted loop></video>
            <img runat="server" id="banner_2" class="d-block w-100 img-responsive" src="" />
        </div>
    </div>

    <div class="row m-0 mt-4 div_padding_1">
        <div class="col-lg-12 m-auto text-center">
            <h1>DERMAFIRM’S Philosophy</h1>
        </div>
        <div class="col-lg-12 mt-5">
            <p>DERMAFIRM, established in 2002, operates under the motto: “The most important thing is invisible to the eye.” During that time, the cosmetic industry often lacked transparency regarding the importance and production process of quality raw materials. Consequently, DERMAFIRM was founded as a cosmetic brand committed to producing its own raw materials, validated to be effective through extensive research. Since its inception, DERMAFIRM has focused on manufacturing high-purity and functional raw ingredients in its dedicated production facilities, ensuring the accessibility of trusted derma cosmetics to all.</p>
        </div>
    </div>

    <div class="row m-0 mt-4">
        <div class="col-lg-6 text-center">
            <img src="https://ecentra.com.my/Backoffice/UploadImage/Dermarfirm_Image/Image_3.png" alt="Lab Equipment" class="img-fluid">
        </div>
        <div class="col-lg-6 m-auto">
            <h3 class="mt-3">Sincerity With Premium Ingredients & Innovation</h3>
            <p class="mt-4 width-75">DERMAFIRM's groundbreaking formula utilizes Liposomes—a transdermal drug delivery system (TDDS)—to deliver active ingredients to the skin quickly and efficiently. The stability of the ingredients has been improved by encapsulating active components between double layers of phospholipids, allowing products to deliver powerful agents securely to the skin. DERMAFIRM's revolutionary Liposomes enable carefully chosen ingredients such as Peptides, Azulene, and Astaxanthin to penetrate the dermis effectively, achieving clinically-proven, visible results.</p>
        </div>
    </div>

    <div class="row m-0 mt-4">
        <div class="col-lg-6">
            <img src="https://ecentra.com.my/Backoffice/UploadImage/Dermarfirm_Image/Image_4.jpg" alt="DERMAFIRM Lab Coat" class="img-fluid">
        </div>
        <div class="col-lg-6 m-auto">
            <p class="width-75 mt-3">DERMAFIRM utilizes a collection of 22 functional peptides renowned for their assistance with anti-aging, skin brightening, and hair strengthening. The innovative patented peptides—Heptapeptide-48 and Palmitoyl heptapeptide-48—were developed in-house by DERMAFIRM's scientific experts for our potent anti-aging formulations.</p>
        </div>
    </div>
    
    <div class="row m-0 mt-4 mobile_reverse">
        <div class="col-lg-6 order-lg-1 m-auto">
            <p class="width-75 mt-3">DERMAFIRM has embarked on new scientific research to explore the impact of microbiomes on skin disorders, resulting in the development of a patented microbiome technology called Terrabiome®. Formulated with fermentation and natural extracts, this innovative technology assists in balancing the skin's ecosystem for healthier, better-functioning skin.</p>
        </div>
        <div class="col-lg-6 order-lg-2">
            <img src="https://ecentra.com.my/Backoffice/UploadImage/Dermarfirm_Image/Image_5.jpg" alt="Lab Equipment" class="img-fluid">
        </div>
    </div>

    <div class="row m-0 mt-5">
        <div class="col-lg-12">
            <img src="https://ecentra.com.my/Backoffice/UploadImage/Dermarfirm_Image/Image_6.png" alt="DERMAFIRM Lab Coat" class="img-fluid w-100">
        </div>
        <div class="col-lg-12 mt-2 div_padding_2">
            <p class="p-3">In adhering to the essence of Derma Cosmetics, DERMAFIRM avoids dependence on external OEM/ODM cosmetic manufacturers. Instead, the company focuses on bolstering its own R&D capabilities. Through four research institutes—namely, the Derma Science Research Center, the Peptide Research Center, the Medical Device Research Center, and the Natural Materials Research Center—30% of the workforce is engaged. Consequently, DERMAFIRM boasts the most advanced R&D capabilities in the domestic derma cosmetics industry. This commitment has led to pioneering research in skin microbiome design, the development of new production technology, and the creation of innovative material peptides and liposome technology.</p>
        </div>
    </div>

    <div class="row m-0 mt-5">
        <div class="col-lg-12">
            <img src="https://ecentra.com.my/Backoffice/UploadImage/Dermarfirm_Image/Image_7.png" alt="DERMAFIRM Lab Coat" class="img-fluid w-100">
        </div>
        <div class="div_padding_3">
            <div class="col-lg-12 m-auto text-center">
                <h2>DERMAFIRM’s Global Presence</h2>
            </div>
            <div class="col-lg-12 mt-3">
                <p>DERMAFIRM began developing cosmetics exclusively for hospitals and clinics in 2005 and is now widely recognized for its excellence in more than 30 countries worldwide. The company actively communicates with its customers to guarantee superior sales and quality assurance services.</p>
            </div>
            <div class="col-lg-12 mt-3">
                <p>As a pioneer in the skincare industry, DERMAFIRM is leading the way for K-beauty globally through the use of trusted dermatological science and creative innovation.</p>
            </div>
        </div>

    </div>

    <div class="row m-0 mt-4">
        <div class="col-lg-12">
            <img src="https://ecentra.com.my/Backoffice/UploadImage/Dermarfirm_Image/Image_8.png" alt="DERMAFIRM Lab Coat" class="img-fluid w-100">
        </div>
    </div>

    <div class="row m-0 div_padding_4">
        <div class="col-lg-12 m-auto text-center">
            <h3>Open Innovation</h3>
        </div>
        <div class="col-lg-12 mt-3">
            <p>Open innovation at DERMAFIRM is built on ongoing scientific study and partnerships with top laboratories around the globe. The company continually works to create new materials using the newest delivery methods.</p>
        </div>
        <div class="row p-3">
            <div class="col-lg-6 mt-3 ">
                <p>DERMAFIRM collaborates with Abio Materials, which holds a patent for 3rd generation exosome extraction technology (ExotractionTM). Exosomes are lipid bilayer-enclosed, nano-sized extracellular micro-vesicles secreted by all living cells. These exosomes are a promising smart delivery system as well as a new material for pharmaceutics and cosmetics.</p>
            </div>
            <div class="col-lg-6 mt-3">
                <p>DERMAFIRM also collaborates with SCAI Therapeutics to improve the penetration of substances such as HA, PDRN, and Azulene. MOASIS (Molecular Asociated Innovative Substance) is SCAI Therapeutics’ patented delivery technology, which enhances the permeability, solubility, and stability of specific substances.</p>
            </div>
        </div>
    </div>

    <div class="row m-0">
        <div class="col-lg-12">
            <img src="https://ecentra.com.my/Backoffice/UploadImage/Dermarfirm_Image/Image_9.jpg" alt="DERMAFIRM Lab Coat" class="img-fluid w-100">
        </div>
    </div>

    <div class="row m-0 mt-4">
        <div class="col-lg-12 m-auto text-center">
            <h1>The DERMAFIRM Wonju Plant</h1>
        </div>
        <div class="col-lg-12 mt-5">
            <p>The main DERMAFIRM plant is housed in a cutting-edge smart factory, measuring 40,000 square feet and standing four stories high. It is equipped with a “Full Cell Line” for cosmetic products, enabling one-stop production from raw materials formulation to filling and packaging, with a capacity of over 2,500 tons per year, as well as high-quality medical device production facilities.</p>
        </div>

        <div class="col-lg-12 mt-4">
            <p>The factory also has the capacity for producing more than 400 tons of products and features an advanced distribution system, allowing products to be quickly distributed globally. It is ISO (CGMP) certified, guaranteeing that products are made in a regulatory-approved facility and ensuring the quality control necessary to produce safe and effective products.</p>
        </div>
    </div>

    <div class="ml-2 mr-2">
        <div class="row m-0">
            <div class="col-lg-6 mt-3 text-end">
                <img src="https://ecentra.com.my/Backoffice/UploadImage/Dermarfirm_Image/Image_10.jpg" alt="DERMAFIRM Lab Coat" class="img-fluid">
            </div>
            <div class="col-lg-6 m-auto">
                <h2>Commitment to Ingredient Integrity</h2>
                <p class="width-75 mt-3">DERMAFIRM uses only safe ingredients, ensuring that all formulated products are clean, cruelty-free, paraben-free, preservative-free, and free of harsh elements or artificial colors. DERMAFIRM products are free of tar, parabens, ethylene oxide, benzophenone, diazolidinyl urea, artificial coloring, hydroquinone, and triclosan, making them suitable for sensitive skin and people of all ages.</p>
            </div>
        </div>
    </div>

    <div class="row m-0 div_flex">
        <div class="col-md-6 div_padding_6">
            <div class="col-md-12">
                <img src="https://ecentra.com.my/Backoffice/UploadImage/Dermarfirm_Image/Image_11.jpg" alt="DERMAFIRM Lab Coat" class="img-fluid w-100">
            </div>
            <div class="col-md-12 mt-5">
                <h3>Terrabiome 2.0</h3>
                <p class="mt-3">Pair text with an image to focus on your chosen product, collection, or blog post. Add details on availability, style, or even provide a review.</p>
            </div>
        </div>

        <div class="col-md-6 div_padding_6">
            <div class="col-md-12">
                <img src="https://ecentra.com.my/Backoffice/UploadImage/Dermarfirm_Image/Image_12.jpg" alt="Lab Equipment" class="img-fluid w-100">
            </div>
            <div class="col-md-12 mt-5">
                <h3>Centella Asiatica EXOSOME™</h3>
                <p class="mt-3">Provides intense anti-inflammatory effect powered by patented extraction technology.</p>
            </div>
        </div>
    </div>

    <div class="row m-0 div_flex">
        <div class="col-md-6 div_padding_6">
            <div class="col-md-12">
                <img src="https://ecentra.com.my/Backoffice/UploadImage/Dermarfirm_Image/Image_13.png" alt="DERMAFIRM Lab Coat" class="img-fluid w-100">
            </div>
            <div class="col-md-12 mt-5">
                <h3>8 Peptides</h3>
                <div class="mt-3">
                    <ul class="ml-3">
                        <li>Acetyl Hexapeptide-8</li>
                        <li>Copper Tripeptide-1</li>
                        <li>Palmitoyl Pentapeptide-4</li>
                        <li>Palmitoyl Tripeptide-1</li>
                        <li>Tripeptide-29</li>
                        <li>Heptapeptide-48</li>
                        <li>Dipeptide-2</li>
                        <li>Acetyl Tetrapeptide-5</li>
                    </ul>
                </div>
            </div>
        </div>

        <div class="col-md-6 div_padding_6">
            <div class="col-md-12">
                <img src="https://ecentra.com.my/Backoffice/UploadImage/Dermarfirm_Image/Image_14.jpg" alt="Lab Equipment" class="img-fluid w-100">
            </div>
            <div class="col-md-12 mt-5">
                <h3>Astaxanthin
                </h3>
                <p class="mt-3">
                    With 6,000 times the antioxidant effect of vitamin C, Astaxanthin prevents free radicals, giving skin a smooth appearance. Liposome effectively delivers the active ingredient to the optimal location of your skin
                </p>

            </div>
        </div>
    </div>

</asp:Content>

