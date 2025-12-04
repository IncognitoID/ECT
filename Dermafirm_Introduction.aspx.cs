using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            Load_Dermafirm_Video();
        }
    }

    protected void Load_Dermafirm_Video()
    {
        using (SqlCommand cmd = new SqlCommand("Load_Dermafirm_Video", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows == true)
            {
                DataTable v = new DataTable();
                v.Load(idr);

                //if (v.Rows[0]["VIDEO_1_TYPE"].ToString() == "Video")
                //{
                //    video_1.Attributes["autoplay"] = "autoplay";
                //    video_1.Attributes["controls"] = "controls";
                //    //video_1.Src = "https://ecentra.com.my/Backoffice/" + v.Rows[0]["VIDEO_1"].ToString();
                //    video_1.Src = "https://www.youtube-nocookie.com/embed/jKRKNgO_5Z0?si=p3iVmOedAY0xvPVo";
                //    video_1.Visible = true;
                //    banner_1.Visible = false;
                //}
                //else
                //{
                //    banner_1.Src = "https://ecentra.com.my/Backoffice/" + v.Rows[0]["BANNER_1"].ToString();
                //    video_1.Visible = false;
                //    banner_1.Visible = true;
                //}

                if (v.Rows[0]["VIDEO_2_TYPE"].ToString() == "Video")
                {
                    video_2.Attributes["autoplay"] = "autoplay";
                    video_2.Attributes["controls"] = "controls";
                    video_2.Src = "https://ecentra.com.my/Backoffice/" + v.Rows[0]["VIDEO_2"].ToString();
                    video_2.Visible = true;
                    banner_2.Visible = false;
                }
                else
                {
                    banner_2.Src = "https://ecentra.com.my/Backoffice/" + v.Rows[0]["BANNER_2"].ToString();
                    video_2.Visible = false;
                    banner_2.Visible = true;
                }
            }

            idr.Close();
            con.Close();
        }
    }
}