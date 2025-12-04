using QRCoder;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MyProfile : System.Web.UI.Page
{
    protected static String ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (!Page.IsPostBack)
            {
                bool cookieExists = HttpContext.Current.Request.Cookies["userid"] != null;

                if (cookieExists == true)
                {
                    if (Request.Cookies["userid"].Value != null && Request.Cookies["userid"].Value != "")
                    {
                        Load_Member_Details(Request.Cookies["userid"].Value);
                        if (div_eo_mission.Visible == true)
                        {
                            Load_EO_Mission(Request.Cookies["userid"].Value);
                        }
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "calculate_countdown();", true);
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
    }

    protected void Load_Member_Details(string memberid)
    {
        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("Load_Member_Details_My_Profile", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Memberid", memberid);
                con.Open();
                SqlDataReader idr = cmd.ExecuteReader();

                if (idr.HasRows == true)
                {
                    while (idr.Read())
                    {
                        if (string.IsNullOrEmpty(idr["First_Name"].ToString()))
                        {
                            lbl_member_name.InnerText = idr["Company_Name"].ToString();
                        }
                        else
                        {
                            lbl_member_name.InnerText = idr["First_Name"].ToString();
                        }
                        lbl_member_id.InnerText = idr["cardno"].ToString();
                        lbl_profit_center.InnerText = idr["Profit_Center"].ToString();
                        lbl_rank.InnerText = idr["Rank"].ToString();
                        if (idr["Member_Category"].ToString() == "EO")
                        {
                            //div_eo_mission.Visible = true;
                            eo_hr.Visible = true;
                            if (idr["Rank"].ToString() == "Business Owner")
                            {
                                lbl_rank.InnerText = "Entrepreneur Owner";
                            }
                            else if (idr["Rank"].ToString() == "Bronze")
                            {
                                lbl_rank.InnerText = "Bronze EO";
                            }
                            else if (idr["Rank"].ToString() == "Silver")
                            {
                                lbl_rank.InnerText = "Silver EO";
                            }
                            else if (idr["Rank"].ToString() == "Gold")
                            {
                                lbl_rank.InnerText = "Gold EO";
                            }
                        }
                        else
                        {
                            //div_eo_mission.Visible = false;
                            eo_hr.Visible = false;
                        }
                        lbl_total_referral.InnerText = idr["Total_Referral"].ToString();
                        double totalDownline = Convert.ToDouble(idr["Total_Downline"]);
                        string formattedTotal = totalDownline.ToString("###,###,##0.#"); // This removes any trailing .00

                        lbl_total_group.InnerText = formattedTotal;
                        if (Convert.ToInt32(idr["Member_Waiting_Assign"].ToString()) > 1)
                        {
                            string value = "";
                            bool cookieExists = HttpContext.Current.Request.Cookies["language"] != null;
                            if (cookieExists == true)
                            {
                                if (Request.Cookies["language"].Value != null && Request.Cookies["language"].Value != "")
                                {
                                    if (Request.Cookies["language"].Value == "Chinese")
                                    {
                                        value = " 名会员";
                                    }
                                    else
                                    {
                                        value = " members";
                                    }
                                }
                                else
                                {
                                    value = " members";
                                }
                            }
                            else
                            {
                                value = " members";
                            }
                            lbl_total_member_waiting_assign.InnerText = idr["Member_Waiting_Assign"].ToString() + value;
                        }
                        else
                        {
                            string value = "";
                            bool cookieExists = HttpContext.Current.Request.Cookies["language"] != null;
                            if (cookieExists == true)
                            {
                                if (Request.Cookies["language"].Value != null && Request.Cookies["language"].Value != "")
                                {
                                    if (Request.Cookies["language"].Value == "Chinese")
                                    {
                                        value = " 名会员";
                                    }
                                    else
                                    {
                                        value = " member";
                                    }
                                }
                                else
                                {
                                    value = " member";
                                }
                            }
                            else
                            {
                                value = " member";
                            }
                            lbl_total_member_waiting_assign.InnerText = idr["Member_Waiting_Assign"].ToString() + value;
                        }

                        if (idr["First_Month_Free_Maintain"].ToString() == "Y" && Convert.ToDateTime(idr["First_Month_Free_Maintain_Expired_Date"].ToString()) > DateTime.UtcNow.AddHours(8))
                        {
                            double pbv = Convert.ToDouble(idr["Maintain_pBV"].ToString());
                            int pbv_value = (int)Math.Floor(pbv);

                            string value = "";
                            bool cookieExists = HttpContext.Current.Request.Cookies["language"] != null;
                            if (cookieExists == true)
                            {
                                if (Request.Cookies["language"].Value != null && Request.Cookies["language"].Value != "")
                                {
                                    if (Request.Cookies["language"].Value == "Chinese")
                                    {
                                        value = " pBV (合格)";
                                    }
                                    else
                                    {
                                        value = " pBV (Qualified)";
                                    }
                                }
                                else
                                {
                                    value = " pBV (Qualified)";
                                }
                            }
                            else
                            {
                                value = " pBV (Qualified)";
                            }
                            lbl_pBV_value.InnerText = pbv_value + value;
                            lbl_pBV_value.Attributes.Add("class", "font-weight-bold greentext");
                        }
                        else
                        {    
                            if(idr["This_Month_Success_Maintain_pBV"].ToString() == "Y")
                            {
                                double pbv = Convert.ToDouble(idr["Maintain_pBV"].ToString());
                                int pbv_value = (int)Math.Floor(pbv);

                                string value = "";
                                bool cookieExists = HttpContext.Current.Request.Cookies["language"] != null;
                                if (cookieExists == true)
                                {
                                    if (Request.Cookies["language"].Value != null && Request.Cookies["language"].Value != "")
                                    {
                                        if (Request.Cookies["language"].Value == "Chinese")
                                        {
                                            value = " pBV (合格)";
                                        }
                                        else
                                        {
                                            value = " pBV (Qualified)";
                                        }
                                    }
                                    else
                                    {
                                        value = " pBV (Qualified)";
                                    }
                                }
                                else
                                {
                                    value = " pBV (Qualified)";
                                }
                                lbl_pBV_value.InnerText = pbv_value + value;
                                lbl_pBV_value.Attributes.Add("class", "font-weight-bold greentext");
                            }
                            else if(Convert.ToDecimal(idr["Maintain_pBV"].ToString()) >= Convert.ToDecimal(idr["Minimum_Pbv"].ToString()))
                            {
                                double pbv = Convert.ToDouble(idr["Maintain_pBV"].ToString());
                                int pbv_value = (int)Math.Floor(pbv);

                                string value = "";
                                bool cookieExists = HttpContext.Current.Request.Cookies["language"] != null;
                                if (cookieExists == true)
                                {
                                    if (Request.Cookies["language"].Value != null && Request.Cookies["language"].Value != "")
                                    {
                                        if (Request.Cookies["language"].Value == "Chinese")
                                        {
                                            value = " pBV (合格)";
                                        }
                                        else
                                        {
                                            value = " pBV (Qualified)";
                                        }
                                    }
                                    else
                                    {
                                        value = " pBV (Qualified)";
                                    }
                                }
                                else
                                {
                                    value = " pBV (Qualified)";
                                }
                                lbl_pBV_value.InnerText = pbv_value + value;
                                lbl_pBV_value.Attributes.Add("class", "font-weight-bold greentext");
                            }
                            else
                            {
                                double pbv = Convert.ToDouble(idr["Maintain_pBV"].ToString());
                                int pbv_value = (int)Math.Floor(pbv);

                                string value = "";
                                bool cookieExists = HttpContext.Current.Request.Cookies["language"] != null;
                                if (cookieExists == true)
                                {
                                    if (Request.Cookies["language"].Value != null && Request.Cookies["language"].Value != "")
                                    {
                                        if (Request.Cookies["language"].Value == "Chinese")
                                        {
                                            value = " pBV (不合格)";
                                        }
                                        else
                                        {
                                            value = " pBV (Not Qualified)";
                                        }
                                    }
                                    else
                                    {
                                        value = " pBV (Not Qualified)";
                                    }
                                }
                                else
                                {
                                    value = " pBV (Not Qualified)";
                                }
                                lbl_pBV_value.InnerText = pbv_value + value;
                                lbl_pBV_value.Attributes.Add("class", "font-weight-bold redtext");
                            }
                        }

                        if (!string.IsNullOrEmpty(idr["Downline_A"].ToString()))
                        {
                            double pbv = Convert.ToDouble(idr["Downline_A_Monthly_Group_Sales_BV"].ToString());
                            int pbv_value = (int)Math.Floor(pbv);
                            string formatted_pbv = pbv_value.ToString("N0"); // Formats the number as ###,###,##0

                            double daily_pbv = Convert.ToDouble(idr["Downline_A_Daily_Group_Sales_BV"].ToString());
                            int daily_pbv_value = (int)Math.Floor(daily_pbv);
                            string formatted_daily_pbv = daily_pbv_value.ToString("N0"); // Formats the number as ###,###,##0

                            lbl_left_member_id.InnerText = idr["Downline_A"].ToString();
                            lbl_left_downline_point.InnerHtml = String.Format("<b>{0}</b> BV", formatted_pbv);
                            lbl_left_member_point.InnerHtml = String.Format("<b>{0}</b> BV", formatted_daily_pbv);
                        }
                        else
                        {
                            string value = "";
                            bool cookieExists = HttpContext.Current.Request.Cookies["language"] != null;
                            if (cookieExists == true)
                            {
                                if (Request.Cookies["language"].Value != null && Request.Cookies["language"].Value != "")
                                {
                                    if (Request.Cookies["language"].Value == "Chinese")
                                    {
                                        value = "新注册会员";
                                    }
                                    else
                                    {
                                        value = "New Recruit";
                                    }
                                }
                                else
                                {
                                    value = "New Recruit";
                                }
                            }
                            else
                            {
                                value = "New Recruit";
                            }
                            lbl_left_member_id.InnerText = value;
                            lbl_left_downline_point.InnerHtml = "<b>0</b> BV";
                            lbl_left_member_point.InnerHtml = "<b>0</b> BV";
                        }

                        if (!string.IsNullOrEmpty(idr["Downline_B"].ToString()))
                        {
                            double pbv = Convert.ToDouble(idr["Downline_B_Monthly_Group_Sales_BV"].ToString());
                            int pbv_value = (int)Math.Floor(pbv);
                            string formatted_pbv = pbv_value.ToString("N0"); // Formats the number with thousand separators

                            double daily_pbv = Convert.ToDouble(idr["Downline_B_Daily_Group_Sales_BV"].ToString());
                            int daily_pbv_value = (int)Math.Floor(daily_pbv);
                            string formatted_daily_pbv = daily_pbv_value.ToString("N0"); // Formats the number with thousand separators

                            lbl_right_member_id.InnerText = idr["Downline_B"].ToString();
                            lbl_right_downline_point.InnerHtml = String.Format("<b>{0}</b> BV", formatted_pbv);
                            lbl_right_member_point.InnerHtml = String.Format("<b>{0}</b> BV", formatted_daily_pbv);
                        }
                        else
                        {
                            string value = "";
                            bool cookieExists = HttpContext.Current.Request.Cookies["language"] != null;
                            if (cookieExists == true)
                            {
                                if (Request.Cookies["language"].Value != null && Request.Cookies["language"].Value != "")
                                {
                                    if (Request.Cookies["language"].Value == "Chinese")
                                    {
                                        value = "新注册会员";
                                    }
                                    else
                                    {
                                        value = "New Recruit";
                                    }
                                }
                                else
                                {
                                    value = "New Recruit";
                                }
                            }
                            else
                            {
                                value = "New Recruit";
                            }
                            lbl_right_member_id.InnerText = value;
                            lbl_right_downline_point.InnerHtml = "<b>0</b> BV";
                            lbl_right_member_point.InnerHtml = "<b>0</b> BV";
                        }

                        lbl_total_payout.InnerText = (Convert.ToDecimal(idr["Total Direct Profit"].ToString()) + Convert.ToDecimal(idr["Total BB Cycle 1"].ToString()) + Convert.ToDecimal(idr["Total BB Cycle 2"].ToString()) + Convert.ToDecimal(idr["Total Income Booster"].ToString()) + Convert.ToDecimal(idr["Total SDB"].ToString()))
                                                    == 0
                                                    ? "RM 0.00"
                                                    : "RM " + (Convert.ToDecimal(idr["Total Direct Profit"].ToString()) + Convert.ToDecimal(idr["Total BB Cycle 1"].ToString()) + Convert.ToDecimal(idr["Total BB Cycle 2"].ToString()) + Convert.ToDecimal(idr["Total Income Booster"].ToString()) + Convert.ToDecimal(idr["Total SDB"].ToString())).ToString("###,###,##0.00");;

                        if(idr["Status"].ToString() == "Active" && Convert.ToDecimal(lbl_total_payout.InnerText.Replace("RM ", "")) >= 200)
                        {
                            if (idr["First_Month_Free_Maintain"].ToString() == "Y" && Convert.ToDateTime(idr["First_Month_Free_Maintain_Expired_Date"].ToString()) > DateTime.UtcNow.AddHours(8))
                            {
                                div_bonus.Visible = false;
                            }
                            else
                            {
                                if (idr["This_Month_Success_Maintain_pBV"].ToString() == "Y")
                                {
                                    div_bonus.Visible = false;
                                }
                                else if (Convert.ToDecimal(idr["Maintain_pBV"].ToString()) >= Convert.ToDecimal(idr["Minimum_Pbv"].ToString()))
                                {
                                    div_bonus.Visible = false;
                                }
                                else
                                {
                                    div_bonus.Visible = true;
                                }
                            }
                        }
                        else
                        {
                            div_bonus.Visible = false;
                        }

                        if (idr["AgentLevel"].ToString() == "Shopper")
                        {
                            if(DateTime.UtcNow.AddHours(8) > Convert.ToDateTime(idr["Upgrade_To_BO_Expired_Date"].ToString()))
                            {
                                div_shopper.Visible = false;
                                div_bo.Visible = false;
                            }
                            else
                            {
                                div_bo.Visible = false;
                                div_shopper.Visible = true;
                                lbl_shopper_upgrade.InnerText = " RM " + idr["Upgrade_To_BO"].ToString();
                                decimal total_collect = 100 - ((Convert.ToDecimal(idr["Upgrade_To_BO"].ToString()) / Convert.ToDecimal(1000)) * 100);
                                div_voucher_progress_bar.Attributes.Add("style", "width:" + Convert.ToString(total_collect) + "%;");
                                lbl_voucher_collected.Text = "RM " + (Convert.ToDecimal(1000) - Convert.ToDecimal(idr["Upgrade_To_BO"].ToString())).ToString();
                            }
                        }
                        else if(idr["AgentLevel"].ToString() == "BO")
                        {
                            decimal downlineA_BV = Convert.ToDecimal(idr["Downline_A_Monthly_Group_Sales_BV"].ToString());
                            decimal downlineB_BV = Convert.ToDecimal(idr["Downline_B_Monthly_Group_Sales_BV"].ToString());
                            decimal member_total_bv = downlineA_BV + downlineB_BV;
                            if (downlineA_BV > 25000 && downlineB_BV > 25000)
                            {
                                // Both values are greater than 100000, fill all circles
                                GenerateProgressBar_Bronze(0);
                            }
                            else if (downlineA_BV > 25000 && downlineB_BV < 25000)
                            {
                                // Downline A is greater than 100000, calculate remaining percentage based on Downline B
                                decimal remaining_bv = 25000 - downlineB_BV;
                                decimal final_percentage = (remaining_bv / 50000) * 100;
                                GenerateProgressBar_Bronze(100 - final_percentage);
                            }
                            else if (downlineA_BV < 25000 && downlineB_BV > 25000)
                            {
                                // Downline B is greater than 100000, calculate remaining percentage based on Downline A
                                decimal remaining_bv = 25000 - downlineA_BV;
                                decimal final_percentage = (remaining_bv / 50000) * 100;
                                GenerateProgressBar_Bronze(100 - final_percentage);
                            }
                            else
                            {
                                // Neither value is greater than 100000, calculate percentage normally
                                if (member_total_bv <= 50000)
                                {
                                    decimal final_percentage = (member_total_bv / 50000) * 100;
                                    GenerateProgressBar_Bronze(final_percentage);
                                }
                            }
                        }
                        else if (idr["AgentLevel"].ToString() == "BBO")
                        {
                            decimal downlineA_BV = Convert.ToDecimal(idr["Downline_A_Monthly_Group_Sales_BV"].ToString());
                            decimal downlineB_BV = Convert.ToDecimal(idr["Downline_B_Monthly_Group_Sales_BV"].ToString());
                            decimal member_total_bv = downlineA_BV + downlineB_BV;
                            if (downlineA_BV > 100000 && downlineB_BV > 100000)
                            {
                                // Both values are greater than 100000, fill all circles
                                GenerateProgressBar_Silver(0);
                            }
                            else if (downlineA_BV > 100000 && downlineB_BV < 100000)
                            {
                                // Downline A is greater than 100000, calculate remaining percentage based on Downline B
                                decimal remaining_bv = 100000 - downlineB_BV;
                                decimal final_percentage = (remaining_bv / 200000) * 100;
                                GenerateProgressBar_Silver(100 - final_percentage);
                            }
                            else if (downlineA_BV < 100000 && downlineB_BV > 100000)
                            {
                                // Downline B is greater than 100000, calculate remaining percentage based on Downline A
                                decimal remaining_bv = 100000 - downlineA_BV;
                                decimal final_percentage = (remaining_bv / 200000) * 100;
                                GenerateProgressBar_Silver(100 - final_percentage);
                            }
                            else
                            {
                                // Neither value is greater than 100000, calculate percentage normally
                                if (member_total_bv <= 200000)
                                {
                                    decimal final_percentage = (member_total_bv / 200000) * 100;
                                    GenerateProgressBar_Silver(final_percentage);
                                }
                            }
                        }
                        else
                        {
                            decimal downlineA_BV = Convert.ToDecimal(idr["Downline_A_Monthly_Group_Sales_BV"].ToString());
                            decimal downlineB_BV = Convert.ToDecimal(idr["Downline_B_Monthly_Group_Sales_BV"].ToString());
                            decimal member_total_bv = downlineA_BV + downlineB_BV;
                            if (downlineA_BV > 1000000 && downlineB_BV > 1000000)
                            {
                                // Both values are greater than 100000, fill all circles
                                GenerateProgressBar_Gold(0);
                            }
                            else if (downlineA_BV > 1000000 && downlineB_BV < 1000000)
                            {
                                // Downline A is greater than 100000, calculate remaining percentage based on Downline B
                                decimal remaining_bv = 1000000 - downlineB_BV;
                                decimal final_percentage = (remaining_bv / 2000000) * 100;
                                GenerateProgressBar_Gold(100 - final_percentage);
                            }
                            else if (downlineA_BV < 1000000 && downlineB_BV > 1000000)
                            {
                                // Downline B is greater than 100000, calculate remaining percentage based on Downline A
                                decimal remaining_bv = 1000000 - downlineA_BV;
                                decimal final_percentage = (remaining_bv / 2000000) * 100;
                                GenerateProgressBar_Gold(100 - final_percentage);
                            }
                            else
                            {
                                // Neither value is greater than 100000, calculate percentage normally
                                if (member_total_bv <= 2000000)
                                {
                                    decimal final_percentage = (member_total_bv / 2000000) * 100;
                                    GenerateProgressBar_Gold(final_percentage);
                                }
                            }
                        }

                        #region Member Card

                        lbl_user_id.InnerText = idr["cardno"].ToString();
                        if (string.IsNullOrEmpty(idr["First_Name"].ToString()))
                        {
                            lbl_user_name.InnerText = idr["Company_Name"].ToString();
                        }
                        else
                        {
                            lbl_user_name.InnerText = idr["First_Name"].ToString();
                        }
                        DateTime createdDate = Convert.ToDateTime(idr["Created_DT"]); // Assuming Created_DT is a DateTime field

                        // Format the DateTime to get only MM/dd
                        string formattedDate = createdDate.ToString("MM/dd/yyyy");

                        lbl_user_join_date.InnerText = formattedDate;

                        string url = "";
                        url = "https://ecentra.com.my/Register?referral_id=" + Request.Cookies["userid"].Value + "&id=";

                        string code = url;
                        QRCodeGenerator qrGenerator = new QRCodeGenerator();
                        QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.L);
                        System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
                        imgBarCode.Height = 80;
                        imgBarCode.Width = 80;
                        using (Bitmap bitMap = qrCode.GetGraphic(20))
                        {
                            using (MemoryStream ms = new MemoryStream())
                            {
                                bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                                byte[] byteImage = ms.ToArray();
                                imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                            }
                            PlaceHolder1.Controls.Add(imgBarCode);
                        }

                        #endregion
                    }
                }

                idr.Close();
                con.Close();

            }
        }
    }

    protected void Load_EO_Mission(string memberid)
    {
        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("Load_Member_Details_EO_Mission", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Member_ID", memberid);
                con.Open();
                SqlDataReader idr = cmd.ExecuteReader();

                if (idr.HasRows == true)
                {
                    while (idr.Read())
                    {
                        hdn_countdown_value.Value = idr["SecondsDifference"].ToString();

                        if (idr["Campaign_ID"].ToString() == "OENVUS" && idr["Status"].ToString() == "Completed")
                        {
                            img_list_eo.Visible = true;
                        }

                        if (idr["Campaign_ID"].ToString() == "TYFBFD6" && idr["Status"].ToString() == "Completed")
                        {
                            img_list_bo.Visible = true;
                        }

                        if (idr["Campaign_ID"].ToString() == "OENVUS" && Convert.ToDecimal(idr["Successful_Sign_Up_EO"].ToString()) >= 2)
                        {
                            img_left_eo.Visible = true;
                            img_right_eo.Visible = true;
                        }
                        else if (idr["Campaign_ID"].ToString() == "OENVUS" && Convert.ToDecimal(idr["Successful_Sign_Up_EO"].ToString()) >= 1)
                        {
                            img_left_eo.Visible = true;
                        }

                        if (idr["Campaign_ID"].ToString() == "TYFBFD6" && Convert.ToDecimal(idr["Successful_Sign_Up_BO"].ToString()) >= 2)
                        {
                            img_left_bo.Visible = true;
                            img_right_bo.Visible = true;
                        }
                        else if (idr["Campaign_ID"].ToString() == "TYFBFD6" && Convert.ToDecimal(idr["Successful_Sign_Up_BO"].ToString()) >= 1)
                        {
                            img_left_bo.Visible = true;
                        }
                    }
                }

                idr.Close();
                con.Close();

            }
        }
    }

    private void GenerateProgressBar_Bronze(decimal percentage)
    {
        int totalCircles = 20;
        int silver_totalCircles = 5;
        int fullColoredCircles = (int)(percentage / (100 / silver_totalCircles));
        decimal partialCirclePercentage = (percentage % (100 / silver_totalCircles)) / (100 / silver_totalCircles) * 100;

        List<string> colors = new List<string>
        {
            "#A4DDED", "#9ACFE4", "#91C2DB", "#87B4D1", "#7DA6C7",
            "#7498BD", "#6A8AB4", "#617DAA", "#617DAA", "#6A8AB4",
            "#7498BD", "#7DA6C7", "#87B4D1", "#91C2DB", "#9ACFE4",
            "#A4DDED", "#C3C8A7", "#D5CC88", "#E8D06A", "#FBD44B"
        };

        StringBuilder progressBarHtml = new StringBuilder();
        progressBarHtml.Append("<div class='rank_header'>");
        progressBarHtml.Append("<span class='label header_1'><img src='img/MyProfile/ranking.png'/></span>");
        progressBarHtml.Append("<span class='label header_4'><img src='img/MyProfile/Bronze.png'/></span>");
        progressBarHtml.Append("<span class='label header_2'><img src='img/MyProfile/Silver.png'/></span>");
        progressBarHtml.Append("<span class='label header_3'><img src='img/MyProfile/Gold.png'/></span>");
        progressBarHtml.Append("</div>");
        progressBarHtml.Append("<div class='progress-bar'>");

        for (int i = 0; i < totalCircles; i++)
        {
            string color = "#2c1919ad"; // Default color for unfilled circles
            string gradient = "";

            if (i < 10)
            {
                if (i < fullColoredCircles)
                {
                    color = colors[i];
                }
                else if (i == fullColoredCircles && partialCirclePercentage > 0)
                {
                    color = colors[i];
                    gradient = String.Format("background: linear-gradient(to right, {0} {1}%, #2c1919ad {1}%);", color, partialCirclePercentage);
                }
            }

            string style = string.IsNullOrEmpty(gradient) ? string.Format("background-color: {0};", color) : gradient;
            progressBarHtml.Append(String.Format("<div class='circle' style='{0}'></div>", style));
        }

        progressBarHtml.Append("</div>");
        progressBarHtml.Append("<div class='labels'>");
        progressBarHtml.Append("<span class='line' style='left: 22%;'></span>");
        progressBarHtml.Append("<span class='line' style='left: 47.5%;'></span>");
        progressBarHtml.Append("<span class='line' style='left: 98.5%;'></span>");
        progressBarHtml.Append("<span class='label' style='left: 22.5%;'>25%</span>");
        progressBarHtml.Append("<span class='label' style='left: 48%;'>50%</span>");
        progressBarHtml.Append("<span class='label' style='left: 99%;'>100%</span>");
        progressBarHtml.Append("</div>");

        // Assign the generated HTML to a literal control
        ProgressBarLiteral.Text = progressBarHtml.ToString();
    }

    private void GenerateProgressBar_Silver(decimal percentage)
    {
        int totalCircles = 20;
        int silver_totalCircles = 10;
        int fullColoredCircles = (int)(percentage / (100 / silver_totalCircles));
        decimal partialCirclePercentage = (percentage % (100 / silver_totalCircles)) / (100 / silver_totalCircles) * 100;

        List<string> colors = new List<string>
    {
        "#A4DDED", "#9ACFE4", "#91C2DB", "#87B4D1", "#7DA6C7",
        "#7498BD", "#6A8AB4", "#617DAA", "#617DAA", "#6A8AB4",
        "#7498BD", "#7DA6C7", "#87B4D1", "#91C2DB", "#9ACFE4",
        "#A4DDED", "#C3C8A7", "#D5CC88", "#E8D06A", "#FBD44B"
    };

        StringBuilder progressBarHtml = new StringBuilder();
        progressBarHtml.Append("<div class='rank_header'>");
        progressBarHtml.Append("<span class='label header_1'><img src='img/MyProfile/ranking.png'/></span>");
        progressBarHtml.Append("<span class='label header_4'><img src='img/MyProfile/Bronze.png'/></span>");
        progressBarHtml.Append("<span class='label header_2'><img src='img/MyProfile/Silver.png'/></span>");
        progressBarHtml.Append("<span class='label header_3'><img src='img/MyProfile/Gold.png'/></span>");
        progressBarHtml.Append("</div>");
        progressBarHtml.Append("<div class='progress-bar'>");

        for (int i = 0; i < totalCircles; i++)
        {
            string color = "#2c1919ad"; // Default color for unfilled circles
            string gradient = "";

            if (i < 10)
            {
                if (i < fullColoredCircles)
                {
                    color = colors[i];
                }
                else if (i == fullColoredCircles && partialCirclePercentage > 0)
                {
                    color = colors[i];
                    gradient = String.Format("background: linear-gradient(to right, {0} {1}%, #2c1919ad {1}%);", color, partialCirclePercentage);
                }
            }

            string style = string.IsNullOrEmpty(gradient) ? string.Format("background-color: {0};", color) : gradient;
            progressBarHtml.Append(String.Format("<div class='circle' style='{0}'></div>", style));
        }

        progressBarHtml.Append("</div>");
        progressBarHtml.Append("<div class='labels'>");
        progressBarHtml.Append("<span class='line' style='left: 22%;'></span>");
        progressBarHtml.Append("<span class='line' style='left: 47.5%;'></span>");
        progressBarHtml.Append("<span class='line' style='left: 98.5%;'></span>");
        progressBarHtml.Append("<span class='label' style='left: 22.5%;'>25%</span>");
        progressBarHtml.Append("<span class='label' style='left: 48%;'>50%</span>");
        progressBarHtml.Append("<span class='label' style='left: 99%;'>100%</span>");
        progressBarHtml.Append("</div>");

        // Assign the generated HTML to a literal control
        ProgressBarLiteral.Text = progressBarHtml.ToString();
    }

    private void GenerateProgressBar_Gold(decimal percentage)
    {
        int full_silver_totalCircles = 10;
        int totalCircles = 10;
        int gold_totalCircles = 10;
        int fullColoredCircles = (int)(percentage / (100 / gold_totalCircles));
        decimal partialCirclePercentage = (percentage % (100 / gold_totalCircles)) / (100 / gold_totalCircles) * 100;

        List<string> colors = new List<string>
        {
            "#A4DDED", "#9ACFE4", "#91C2DB", "#87B4D1", "#7DA6C7",
            "#7498BD", "#6A8AB4", "#617DAA", "#617DAA", "#6A8AB4",
            "#7498BD", "#7DA6C7", "#87B4D1", "#91C2DB", "#9ACFE4",
            "#A4DDED", "#C3C8A7", "#D5CC88", "#E8D06A", "#FBD44B"
        };

        StringBuilder progressBarHtml = new StringBuilder();
        progressBarHtml.Append("<div class='rank_header'>");
        progressBarHtml.Append("<span class='label header_1'><img src='img/MyProfile/ranking.png'/></span>");
        progressBarHtml.Append("<span class='label header_4'><img src='img/MyProfile/Bronze.png'/></span>");
        progressBarHtml.Append("<span class='label header_2'><img src='img/MyProfile/Silver.png'/></span>");
        progressBarHtml.Append("<span class='label header_3'><img src='img/MyProfile/Gold.png'/></span>");
        progressBarHtml.Append("</div>");
        progressBarHtml.Append("<div class='progress-bar'>");

        for (int i = 0; i < full_silver_totalCircles; i++)
        {
            string color = "#2c1919ad"; // Default color for unfilled circles
            string gradient = "";

            color = colors[i];
            gradient = String.Format("background: linear-gradient(to right, {0} {1}%, #2c1919ad {1}%);", color, "100");
            //gradient = String.Format("background: conic-gradient({0} {1}%, #2c1919ad {1}%);", color, "100");

            string style = string.IsNullOrEmpty(gradient) ? string.Format("background-color: {0};", color) : gradient;
            progressBarHtml.Append(String.Format("<div class='circle' style='{0}'></div>", style));
        }

        for (int i = 0; i < totalCircles; i++)
        {
            string color = "#2c1919ad"; // Default color for unfilled circles
            string gradient = "";

            if (i < fullColoredCircles)
            {
                color = colors[i + 10];
            }
            else if (i == fullColoredCircles)
            {
                if (partialCirclePercentage > 0)
                {
                    color = colors[i + 10];
                    gradient = String.Format("background: linear-gradient(to right, {0} {1}%, #2c1919ad {1}%);", color, partialCirclePercentage);
                    //gradient = String.Format("background: conic-gradient({0} {1}%, #2c1919ad {1}%);", color, partialCirclePercentage);
                }
            }

            string style = string.IsNullOrEmpty(gradient) ? string.Format("background-color: {0};", color) : gradient;
            progressBarHtml.Append(String.Format("<div class='circle' style='{0}'></div>", style));
        }

        progressBarHtml.Append("</div>");
        progressBarHtml.Append("<div class='labels'>");
        progressBarHtml.Append("<span class='line' style='left: 22%;'></span>");
        progressBarHtml.Append("<span class='line' style='left: 47.5%;'></span>");
        progressBarHtml.Append("<span class='line' style='left: 98.5%;'></span>");
        progressBarHtml.Append("<span class='label' style='left: 22.5%;'>25%</span>");
        progressBarHtml.Append("<span class='label' style='left: 48%;'>50%</span>");
        progressBarHtml.Append("<span class='label' style='left: 99%;'>100%</span>");
        progressBarHtml.Append("</div>");

        // Assign the generated HTML to a literal control
        ProgressBarLiteral.Text = progressBarHtml.ToString();
    }

}