using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Home : System.Web.UI.Page
{
    protected static String ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
    int slidercount = 0;
    int licount = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            Response.Cookies["Page_Type"].Value = "MyOffice";

            Load_Banner();
        }
    }

    protected void Load_Banner()
    {
        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("Load_Slider", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader idr = cmd.ExecuteReader();

                if (idr.HasRows == false)
                {
                    DataTable v = new DataTable();
                    v.Load(idr);
                    rpt_content.DataSource = v;
                    rpt_content.DataBind();

                    rpt_slider.DataSource = v;
                    rpt_slider.DataBind();
                }
                else
                {
                    DataTable v = new DataTable();
                    v.Load(idr);
                    rpt_content.DataSource = v;
                    rpt_content.DataBind();

                    rpt_slider.DataSource = v;
                    rpt_slider.DataBind();
                }

                idr.Close();
                con.Close();
            }
        }
    }


    protected void rpt_slider_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        HtmlGenericControl li_slider = (HtmlGenericControl)e.Item.FindControl("li_slider");

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView drv = (DataRowView)(e.Item.DataItem);

            if(licount == 0)
            {
                li_slider.Attributes.Add("data-slide-to", licount.ToString());
                li_slider.Attributes.Add("class", "active");
            }
            else
            {
                li_slider.Attributes.Add("data-slide-to", licount.ToString());
            }
            licount++;
        }
    }

    protected void rpt_content_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        HtmlImage img_slider = (HtmlImage)e.Item.FindControl("img_slider");
        HtmlGenericControl div_content = (HtmlGenericControl)e.Item.FindControl("div_content");

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView drv = (DataRowView)(e.Item.DataItem);

            if(slidercount == 0)
            {
                div_content.Attributes.Add("class", "carousel-item active");
            }
            else
            {
                div_content.Attributes.Add("class", "carousel-item");
            }

            if (string.IsNullOrEmpty(drv.Row["Banner_Img"].ToString()) == true)
            {
                img_slider.Src = "Images/NoPic.png";
            }
            else
            {
                img_slider.Src = "https://ecentra.com.my/Backoffice/" + drv.Row["Banner_Img"].ToString();
            }
            slidercount++;
        }
    }
}