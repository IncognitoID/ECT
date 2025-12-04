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

public partial class Direct_Referral_Listing : System.Web.UI.Page
{
    protected static String ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
    public enum MessageType { Success, Error, Info, Warning };

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bool cookieExists = HttpContext.Current.Request.Cookies["userid"] != null;
            if (cookieExists == true)
            {
                if (Request.Cookies["userid"].Value != null && Request.Cookies["userid"].Value != "")
                {
                    txt_start_date.Text = DateTime.UtcNow.AddHours(8).AddMonths(-2).ToString("yyyy-MM-dd");
                    txt_end_date.Text = DateTime.UtcNow.AddHours(8).ToString("yyyy-MM-dd");
                    Load_Member_Direct_Referral(Request.Cookies["userid"].Value, txt_start_date.Text, txt_end_date.Text);
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
    }

    protected void Load_Member_Direct_Referral(string memberid, string startdate, string enddate)
    {
        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("Load_Member_All_Direct_Referral", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Memberid", memberid);
                //cmd.Parameters.AddWithValue("@Start_Date", Convert.ToDateTime(startdate));
                //cmd.Parameters.AddWithValue("@End_Date", Convert.ToDateTime(enddate).AddDays(1));
                con.Open();

                SqlDataReader idr = cmd.ExecuteReader();

                if (idr.HasRows == true)
                {
                    rpt_direct_referral.Visible = true;
                    tr_no_record.Visible = false;
                    DataTable v = new DataTable();

                    v.Load(idr);
                    rpt_direct_referral.DataSource = v;
                    rpt_direct_referral.DataBind();
                }
                else
                {
                    rpt_direct_referral.Visible = false;
                    tr_no_record.Visible = true;
                }

                idr.Close();
                con.Close();

            }
        }
    }

    protected void rpt_direct_referral_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Label lbl_member_name = (Label)e.Item.FindControl("lbl_member_name");
        Label lbl_member_id = (Label)e.Item.FindControl("lbl_member_id");
        Label lbl_member_rank = (Label)e.Item.FindControl("lbl_member_rank");
        Label lbl_member_join_date = (Label)e.Item.FindControl("lbl_member_join_date");
        Label lbl_downline_A_group_bv = (Label)e.Item.FindControl("lbl_downline_A_group_bv");
        Label lbl_downline_B_group_bv = (Label)e.Item.FindControl("lbl_downline_B_group_bv");
        Label lbl_downline_Total_group_bv = (Label)e.Item.FindControl("lbl_downline_Total_group_bv");
        Label lbl_downline_A_name = (Label)e.Item.FindControl("lbl_downline_A_name");
        Label lbl_downline_A_id = (Label)e.Item.FindControl("lbl_downline_A_id");
        Label lbl_downline_A_rank = (Label)e.Item.FindControl("lbl_downline_A_rank");
        Label lbl_downline_B_name = (Label)e.Item.FindControl("lbl_downline_B_name");
        Label lbl_downline_B_id = (Label)e.Item.FindControl("lbl_downline_B_id");
        Label lbl_downline_B_rank = (Label)e.Item.FindControl("lbl_downline_B_rank");
        HtmlTableRow tr_first = (HtmlTableRow)e.Item.FindControl("tr_first");
        HtmlTableRow tr_second = (HtmlTableRow)e.Item.FindControl("tr_second");
        HtmlGenericControl btn_expand = (HtmlGenericControl)e.Item.FindControl("btn_expand");

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView drv = (DataRowView)(e.Item.DataItem);

            if (!string.IsNullOrEmpty(drv.Row["Username"].ToString()))
            {
                lbl_member_name.Text = drv.Row["Username"].ToString();
            }
            else
            {
                lbl_member_name.Text = drv.Row["Company_Name"].ToString();
            }
            lbl_member_id.Text = drv.Row["Userid"].ToString();
            lbl_member_rank.Text = drv.Row["Rank"].ToString();
            if (drv.Row["Member_Category"].ToString() == "EO")
            {
                if (drv.Row["Rank"].ToString() == "Business Owner")
                {
                    lbl_member_rank.Text = "Entrepreneur Owner";
                }
                else if (drv.Row["Rank"].ToString() == "Bronze")
                {
                    lbl_member_rank.Text = "Bronze EO";
                }
                else if (drv.Row["Rank"].ToString() == "Silver")
                {
                    lbl_member_rank.Text = "Silver EO";
                }
                else if (drv.Row["Rank"].ToString() == "Gold")
                {
                    lbl_member_rank.Text = "Gold EO";
                }
            }
            lbl_member_join_date.Text = Convert.ToDateTime(drv.Row["Created_DT"].ToString()).ToString("dd-MM-yyyy");
            
            lbl_downline_A_group_bv.Text = Convert.ToDecimal(drv.Row["Downline_A_Daily_Group_Sales_BV"]) == 0 ? "0" : Convert.ToDecimal(drv.Row["Downline_A_Daily_Group_Sales_BV"]).ToString("###,###,###.##");
            lbl_downline_B_group_bv.Text = Convert.ToDecimal(drv.Row["Downline_B_Daily_Group_Sales_BV"]) == 0 ? "0" : Convert.ToDecimal(drv.Row["Downline_B_Daily_Group_Sales_BV"]).ToString("###,###,###.##");
            lbl_downline_Total_group_bv.Text = Convert.ToDecimal(drv.Row["Total Group BV"]) == 0 ? "0" : Convert.ToDecimal(drv.Row["Total Group BV"]).ToString("###,###,###.##");

            if(!string.IsNullOrEmpty(drv.Row["Downline_A_ID"].ToString()))
            {
                if (!string.IsNullOrEmpty(drv.Row["Downline_A_Name"].ToString()))
                {
                    lbl_downline_A_name.Text = drv.Row["Downline_A_Name"].ToString();
                }
                else
                {
                    lbl_downline_A_name.Text = drv.Row["Downline_A_Company_Name"].ToString();
                }
                lbl_downline_A_id.Text = drv.Row["Downline_A_ID"].ToString();
                lbl_downline_A_rank.Text = drv.Row["Downline_A_Rank"].ToString();
                if (drv.Row["Downline_A_Category"].ToString() == "EO")
                {
                    if (drv.Row["Downline_A_Rank"].ToString() == "Business Owner")
                    {
                        lbl_downline_A_rank.Text = "Entrepreneur Owner";
                    }
                    else if (drv.Row["Downline_A_Rank"].ToString() == "Bronze")
                    {
                        lbl_downline_A_rank.Text = "Bronze EO";
                    }
                    else if (drv.Row["Downline_A_Rank"].ToString() == "Silver")
                    {
                        lbl_downline_A_rank.Text = "Silver EO";
                    }
                    else if (drv.Row["Downline_A_Rank"].ToString() == "Gold")
                    {
                        lbl_downline_A_rank.Text = "Gold EO";
                    }
                }
            }
            else
            {
                tr_first.Visible = false;
            }

            if (!string.IsNullOrEmpty(drv.Row["Downline_B_ID"].ToString()))
            {
                if (!string.IsNullOrEmpty(drv.Row["Downline_B_Name"].ToString()))
                {
                    lbl_downline_B_name.Text = drv.Row["Downline_B_Name"].ToString();
                }
                else
                {
                    lbl_downline_B_name.Text = drv.Row["Downline_B_Company_Name"].ToString();
                }
                lbl_downline_B_id.Text = drv.Row["Downline_B_ID"].ToString();
                lbl_downline_B_rank.Text = drv.Row["Downline_B_Rank"].ToString();
                if (drv.Row["Downline_B_Category"].ToString() == "EO")
                {
                    if (drv.Row["Downline_B_Rank"].ToString() == "Business Owner")
                    {
                        lbl_downline_B_rank.Text = "Entrepreneur Owner";
                    }
                    else if (drv.Row["Downline_B_Rank"].ToString() == "Bronze")
                    {
                        lbl_downline_B_rank.Text = "Bronze EO";
                    }
                    else if (drv.Row["Downline_B_Rank"].ToString() == "Silver")
                    {
                        lbl_downline_B_rank.Text = "Silver EO";
                    }
                    else if (drv.Row["Downline_B_Rank"].ToString() == "Gold")
                    {
                        lbl_downline_B_rank.Text = "Gold EO";
                    }
                }
            }
            else
            {
                tr_second.Visible = false;
            }

            if (string.IsNullOrEmpty(drv.Row["Downline_A_ID"].ToString()) && string.IsNullOrEmpty(drv.Row["Downline_B_ID"].ToString()))
            {
                btn_expand.Visible = false;
            }
        }
    }

    #region Message
    protected void ShowMessage(string Message, MessageType type)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "sweetalert_success('" + Message + "','" + type + "');", true);
    }

    protected void ShowMessage_warning(string Message, MessageType type)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "sweetalert_warning('" + Message + "','" + type + "');", true);
    }
    #endregion


    protected void btn_seach_Click(object sender, EventArgs e)
    {
        if (Convert.ToDateTime(txt_start_date.Text) > Convert.ToDateTime(txt_end_date.Text))
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "AlertCode", "alert('Filter date range invalid.');", true);
            txt_start_date.Focus();
        }
        else
        {

            Load_Member_Direct_Referral(Request.Cookies["userid"].Value, txt_start_date.Text, txt_end_date.Text);
        }
    }
}