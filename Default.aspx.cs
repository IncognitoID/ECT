using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            bool cookieExists = HttpContext.Current.Request.Cookies["userid"] != null;

            if (cookieExists == true)
            {
                if (Request.Cookies["userid"].Value != null && Request.Cookies["userid"].Value != "")
                {
                    Response.Cookies["Page_Type"].Value = "MyOffice";
                    Response.Redirect("MyProfile.aspx");
                    //Response.Redirect("UnderMaintenance.aspx");
                }
                else
                {
                    Response.Redirect("Home.aspx");
                    //Response.Redirect("UnderMaintenance.aspx");

                }
            }
            else
            {
                Response.Redirect("Home.aspx");
                //Response.Redirect("UnderMaintenance.aspx");

            }

            HtmlMeta metatitle = new HtmlMeta();
            metatitle.Name = "og:title";
            metatitle.Content = "Welcome to Ecentra";
            Page.Header.Controls.Add(metatitle);

            HtmlMeta metadesc = new HtmlMeta();
            metadesc.Name = "og:description";
            metadesc.Content = "Join us on our journey as we evolve, innovate, and exceed expectations. Experience the extraordinary with Ecentra, where every product enriches lives and every purchases makes a differrence.";

            Page.Header.Controls.Add(metadesc);

            HtmlMeta metaimage = new HtmlMeta();
            metaimage.Name = "og:image";
            metaimage.Content = "https://ecentra.com.my/Backoffice/UploadImage/Banner_Image/banner_UBLI_0527111547.png";

            Page.Header.Controls.Add(metaimage);

            HtmlMeta metaimagewidth = new HtmlMeta();
            metaimagewidth.Name = "og:image:width";
            metaimagewidth.Content = "600";
            Page.Header.Controls.Add(metaimagewidth);


            HtmlMeta metaimageheight = new HtmlMeta();
            metaimageheight.Name = "og:image:height";
            metaimageheight.Content = "300";
            Page.Header.Controls.Add(metaimageheight);
        }
    }
}