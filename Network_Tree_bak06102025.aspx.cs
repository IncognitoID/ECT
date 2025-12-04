using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Network_Tree : System.Web.UI.Page
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
                        if(string.IsNullOrEmpty(Request.QueryString["id"].ToString()))
                        {
                            Load_Member_Details(Request.Cookies["userid"].Value);
                            div_back.Visible = false;
                        }
                        else
                        {
                            Load_Member_Details(Request.QueryString["id"].ToString());
                            div_back.Visible = true;
                        }

                        if (!string.IsNullOrEmpty(hdn_first_level_left_id.Value))
                        {
                            div_first_level_left_1.Attributes.Add("onclick", "View_Member_Downline('" + hdn_first_level_left_id.Value + "')");
                            Load_First_Level_Left_Member_Details(hdn_first_level_left_id.Value);
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(Request.QueryString["id"].ToString()))
                            {
                                div_first_level_left_1.Attributes.Add("onclick", "Register_Member_Downline('" + Request.Cookies["userid"].Value + "', '" + Request.Cookies["userid"].Value + "', 'Left')");
                            }
                            else
                            {
                                div_first_level_left_1.Attributes.Add("onclick", "Register_Member_Downline('" + Request.Cookies["userid"].Value + "', '" + Request.QueryString["id"].ToString() + "', 'Left')");
                            }

                            div_first_level_left_1.Attributes.Remove("class");
                            div_first_level_left_1.Attributes.Add("class", "col");
                            div_second_level_left_1.Attributes.Add("style", "visibility:hidden;");
                            div_second_level_left_2.Attributes.Add("style", "visibility:hidden;");

                        }

                        if (!string.IsNullOrEmpty(hdn_first_level_right_id.Value))
                        {
                            div_first_level_right_1.Attributes.Add("onclick", "View_Member_Downline('" + hdn_first_level_right_id.Value + "')");
                            Load_First_Level_Right_Member_Details(hdn_first_level_right_id.Value);
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(Request.QueryString["id"].ToString()))
                            {
                                div_first_level_right_1.Attributes.Add("onclick", "Register_Member_Downline('" + Request.Cookies["userid"].Value + "', '" + Request.Cookies["userid"].Value + "', 'Right')");
                            }
                            else
                            {
                                div_first_level_right_1.Attributes.Add("onclick", "Register_Member_Downline('" + Request.Cookies["userid"].Value + "', '" + Request.QueryString["id"].ToString() + "', 'Right')");
                            }
                            div_first_level_right_1.Attributes.Remove("class");
                            div_first_level_right_1.Attributes.Add("class", "col");
                            div_second_level_right_1.Attributes.Add("style", "visibility:hidden;");
                            div_second_level_right_2.Attributes.Add("style", "visibility:hidden;");  
                        }

                        if (!string.IsNullOrEmpty(hdn_second_level_1_left_id.Value))
                        {
                            div_second_level_left_1.Attributes.Add("onclick", "View_Member_Downline('" + hdn_second_level_1_left_id.Value + "')");
                            Load_Second_Level_Left_1_Member_Details(hdn_second_level_1_left_id.Value);
                        }
                        else
                        {
                            div_second_level_left_1.Attributes.Add("onclick", "Register_Member_Downline('" + Request.Cookies["userid"].Value + "', '" + hdn_first_level_left_id.Value + "', 'Left')");
                            img_second_level_1_left.Src = "img/NetworkTree/Icon_add_new.png";
                            img_second_level_1_left.Attributes.Add("class", "w-75");
                            div_second_level_1_left.Visible = false;
                        }

                        if (!string.IsNullOrEmpty(hdn_second_level_1_right_id.Value))
                        {
                            div_second_level_left_2.Attributes.Add("onclick", "View_Member_Downline('" + hdn_second_level_1_right_id.Value + "')");
                            Load_Second_Level_Right_1_Member_Details(hdn_second_level_1_right_id.Value);
                        }
                        else
                        {
                            div_second_level_left_2.Attributes.Add("onclick", "Register_Member_Downline('" + Request.Cookies["userid"].Value + "', '" + hdn_first_level_left_id.Value + "', 'Right')");
                            img_second_level_1_right.Src = "img/NetworkTree/Icon_add_new.png";
                            img_second_level_1_right.Attributes.Add("class", "w-75");
                            div_second_level_1_right.Visible = false;
                        }

                        if (!string.IsNullOrEmpty(hdn_second_level_2_left_id.Value))
                        {
                            div_second_level_right_1.Attributes.Add("onclick", "View_Member_Downline('" + hdn_second_level_2_left_id.Value + "')");
                            Load_Second_Level_Left_2_Member_Details(hdn_second_level_2_left_id.Value);
                        }
                        else
                        {
                            div_second_level_right_1.Attributes.Add("onclick", "Register_Member_Downline('" + Request.Cookies["userid"].Value + "', '" + hdn_first_level_right_id.Value + "', 'Left')");
                            img_second_level_2_left.Src = "img/NetworkTree/Icon_add_new.png";
                            img_second_level_2_left.Attributes.Add("class", "w-75");
                            div_second_level_2_left.Visible = false;
                        }

                        if (!string.IsNullOrEmpty(hdn_second_level_2_right_id.Value))
                        {
                            div_second_level_right_2.Attributes.Add("onclick", "View_Member_Downline('" + hdn_second_level_2_right_id.Value + "')");
                            Load_Second_Level_Right_2_Member_Details(hdn_second_level_2_right_id.Value);
                        }
                        else
                        {
                            div_second_level_right_2.Attributes.Add("onclick", "Register_Member_Downline('" + Request.Cookies["userid"].Value + "', '" + hdn_first_level_right_id.Value + "', 'Right')");
                            img_second_level_2_right.Src = "img/NetworkTree/Icon_add_new.png";
                            img_second_level_2_right.Attributes.Add("class", "w-75");
                            div_second_level_2_right.Visible = false;
                        }
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
            using (SqlCommand cmd = new SqlCommand("Load_Netwotk_Tree_Member_Details", con))
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
                            lbl_selected_member_name.Text = idr["Company_Name"].ToString();
                        }
                        else
                        {
                            lbl_selected_member_name.Text = idr["First_Name"].ToString();
                        }
                        if(idr["Status"].ToString() == "Inactive")
                        {
                            img_main.Src = "img/NetworkTree/Icon2_Red.png";
                        }
                        else
                        {
                            img_main.Src = "img/NetworkTree/Icon2.png";
                        }
                        if(idr["Maintain"].ToString() == "Failed")
                        {
                            img_main.Src = "img/NetworkTree/Icon2_Red.png";
                        }
                        else
                        {
                            img_main.Src = "img/NetworkTree/Icon2.png";
                        }
                        lbl_selected_member_id.Text = idr["cardno"].ToString();
                        lbl_selected_member_rank.Text = "<b>" + idr["Rank"].ToString() + "</b>";

                        if (idr["Member_Category"].ToString() == "EO")
                        {
                            if (idr["AgentLevel"].ToString() == "BO")
                            {
                                lbl_selected_member_rank.Text = "<b> Entrepreneur Owner </b>";
                            }
                            else if (idr["AgentLevel"].ToString() == "BBO")
                            {
                                lbl_selected_member_rank.Text = "<b> BEO </b>";
                            }
                            else if (idr["AgentLevel"].ToString() == "BBO")
                            {
                                lbl_selected_member_rank.Text = "<b> Bronze EO </b>";
                            }
                            else if (idr["AgentLevel"].ToString() == "SBO")
                            {
                                lbl_selected_member_rank.Text = "<b> Silver EO </b>";
                            }
                            else if (idr["AgentLevel"].ToString() == "GBO")
                            {
                                lbl_selected_member_rank.Text = "<b> Gold EO </b>";
                            }
                        }

                        if (!string.IsNullOrEmpty(idr["Downline_A"].ToString()))
                        {
                            hdn_first_level_left_id.Value = idr["Downline_A"].ToString();
                        }
                        else
                        {
                            img_first_level_left.Src = "img/NetworkTree/Icon_add_new.png";
                            img_first_level_left.Attributes.Add("class", "w-75");
                            div_first_level_left.Visible = false;
                        } 
                        
                        if (!string.IsNullOrEmpty(idr["Downline_B"].ToString()))
                        {
                            hdn_first_level_right_id.Value = idr["Downline_B"].ToString();
                        }
                        else
                        {
                            img_first_level_right.Src = "img/NetworkTree/Icon_add_new.png";
                            img_first_level_right.Attributes.Add("class", "w-75");
                            div_first_level_right.Visible = false;
                        }

                        if (idr["Downline_A_Daily_Group_Sales_BV"].ToString() != "0")
                        {
                            double pbv = Convert.ToDouble(idr["Downline_A_Daily_Group_Sales_BV"].ToString());
                            int pbv_value = (int)Math.Floor(pbv);
                            string formatted_pbv = pbv_value.ToString("N0"); // Formats the number with thousand separators

                            lbl_selected_member_left_point.Text = String.Format("<b>{0}</b> BV", formatted_pbv);
                        }
                        else
                        {
                            lbl_selected_member_left_point.Text = "<b>0</b> BV";
                        }

                        if (idr["Downline_B_Daily_Group_Sales_BV"].ToString() != "0")
                        {
                            double pbv = Convert.ToDouble(idr["Downline_B_Daily_Group_Sales_BV"].ToString());
                            int pbv_value = (int)Math.Floor(pbv);
                            string formatted_pbv = pbv_value.ToString("N0"); // Formats the number with thousand separators

                            lbl_selected_member_right_point.Text = String.Format("<b>{0}</b> BV", formatted_pbv);
                        }
                        else
                        {
                            lbl_selected_member_right_point.Text = "<b>0</b> BV";
                        }
                    }
                }

                idr.Close();
                con.Close();

            }
        }
    }

    protected void Load_First_Level_Left_Member_Details(string memberid)
    {
        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("Load_Netwotk_Tree_Member_Details", con))
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
                            lbl_first_level_left_member_name.Text = idr["Company_Name"].ToString();
                        }
                        else
                        {
                            lbl_first_level_left_member_name.Text = idr["First_Name"].ToString();
                        }
                        if (idr["Status"].ToString() == "Inactive")
                        {
                            img_first_level_left.Src = "img/NetworkTree/Icon_Red.png";
                        }
                        else
                        {
                            img_first_level_left.Src = "img/NetworkTree/Icon.png";
                        }
                        if (idr["Maintain"].ToString() == "Failed")
                        {
                            img_first_level_left.Src = "img/NetworkTree/Icon_Red.png";
                        }
                        else
                        {
                            img_first_level_left.Src = "img/NetworkTree/Icon.png";
                        }
                        lbl_first_level_left_member_id.Text = idr["cardno"].ToString();
                        lbl_first_level_left_member_rank.Text = "<b>" + idr["Rank"].ToString() + "</b>";
                        if (idr["Member_Category"].ToString() == "EO")
                        {
                            if (idr["AgentLevel"].ToString() == "BO")
                            {
                                lbl_first_level_left_member_rank.Text = "<b> Entrepreneur Owner </b>";
                            }
                            else if (idr["AgentLevel"].ToString() == "BBO")
                            {
                                lbl_first_level_left_member_rank.Text = "<b> BEO </b>";
                            }
                            else if (idr["AgentLevel"].ToString() == "BBO")
                            {
                                lbl_first_level_left_member_rank.Text = "<b> Bronze EO </b>";
                            }
                            else if (idr["AgentLevel"].ToString() == "SBO")
                            {
                                lbl_first_level_left_member_rank.Text = "<b> Silver EO </b>";
                            }
                            else if (idr["AgentLevel"].ToString() == "GBO")
                            {
                                lbl_first_level_left_member_rank.Text = "<b> Gold EO </b>";
                            }
                        }

                        if (!string.IsNullOrEmpty(idr["Downline_A"].ToString()))
                        {
                            hdn_second_level_1_left_id.Value = idr["Downline_A"].ToString();
                        }

                        if (!string.IsNullOrEmpty(idr["Downline_B"].ToString()))
                        {
                            hdn_second_level_1_right_id.Value = idr["Downline_B"].ToString();
                        }

                        if (idr["Downline_A_Daily_Group_Sales_BV"].ToString() != "0")
                        {
                            double pbv = Convert.ToDouble(idr["Downline_A_Daily_Group_Sales_BV"].ToString());
                            int pbv_value = (int)Math.Floor(pbv);
                            string formatted_pbv = pbv_value.ToString("N0"); // Formats the number with thousand separators

                            lbl_first_level_left_left_point.Text = String.Format("<b>{0}</b> BV", formatted_pbv);
                        }
                        else
                        {
                            lbl_first_level_left_left_point.Text = "<b>0</b> BV";
                        }

                        if (idr["Downline_B_Daily_Group_Sales_BV"].ToString() != "0")
                        {
                            double pbv = Convert.ToDouble(idr["Downline_B_Daily_Group_Sales_BV"].ToString());
                            int pbv_value = (int)Math.Floor(pbv);
                            string formatted_pbv = pbv_value.ToString("N0"); // Formats the number with thousand separators

                            lbl_first_level_left_right_point.Text = String.Format("<b>{0}</b> BV", formatted_pbv);
                        }
                        else
                        {
                            lbl_first_level_left_right_point.Text = "<b>0</b> BV";
                        }
                    }
                }

                idr.Close();
                con.Close();

            }
        }
    }

    protected void Load_First_Level_Right_Member_Details(string memberid)
    {
        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("Load_Netwotk_Tree_Member_Details", con))
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
                            lbl_first_level_right_member_name.Text = idr["Company_Name"].ToString();
                        }
                        else
                        {
                            lbl_first_level_right_member_name.Text = idr["First_Name"].ToString();
                        }
                        if (idr["Status"].ToString() == "Inactive")
                        {
                            img_first_level_right.Src = "img/NetworkTree/Icon_Red.png";
                        }
                        else
                        {
                            img_first_level_right.Src = "img/NetworkTree/Icon.png";
                        }
                        if (idr["Maintain"].ToString() == "Failed")
                        {
                            img_first_level_right.Src = "img/NetworkTree/Icon_Red.png";
                        }
                        else
                        {
                            img_first_level_right.Src = "img/NetworkTree/Icon.png";
                        }
                        lbl_first_level_right_member_id.Text = idr["cardno"].ToString();
                        lbl_first_level_right_member_rank.Text = "<b>" + idr["Rank"].ToString() + "</b>";
                        if (idr["Member_Category"].ToString() == "EO")
                        {
                            if (idr["AgentLevel"].ToString() == "BO")
                            {
                                lbl_first_level_right_member_rank.Text = "<b> Entrepreneur Owner </b>";
                            }
                            else if (idr["AgentLevel"].ToString() == "BBO")
                            {
                                lbl_first_level_right_member_rank.Text = "<b> BEO </b>";
                            }
                            else if (idr["AgentLevel"].ToString() == "BBO")
                            {
                                lbl_first_level_right_member_rank.Text = "<b> Bronze EO </b>";
                            }
                            else if (idr["AgentLevel"].ToString() == "SBO")
                            {
                                lbl_first_level_right_member_rank.Text = "<b> Silver EO </b>";
                            }
                            else if (idr["AgentLevel"].ToString() == "GBO")
                            {
                                lbl_first_level_right_member_rank.Text = "<b> Gold EO </b>";
                            }
                        }

                        if (!string.IsNullOrEmpty(idr["Downline_A"].ToString()))
                        {
                            hdn_second_level_2_left_id.Value = idr["Downline_A"].ToString();
                        }

                        if (!string.IsNullOrEmpty(idr["Downline_B"].ToString()))
                        {
                            hdn_second_level_2_right_id.Value = idr["Downline_B"].ToString();
                        }

                        if (idr["Downline_A_Daily_Group_Sales_BV"].ToString() != "0")
                        {
                            double pbv = Convert.ToDouble(idr["Downline_A_Daily_Group_Sales_BV"].ToString());
                            int pbv_value = (int)Math.Floor(pbv);
                            string formatted_pbv = pbv_value.ToString("N0"); // Formats the number with thousand separators

                            lbl_first_level_right_left_point.Text = String.Format("<b>{0}</b> BV", formatted_pbv);
                        }
                        else
                        {
                            lbl_first_level_right_left_point.Text = "<b>0</b> BV";
                        }

                        if (idr["Downline_B_Daily_Group_Sales_BV"].ToString() != "0")
                        {
                            double pbv = Convert.ToDouble(idr["Downline_B_Daily_Group_Sales_BV"].ToString());
                            int pbv_value = (int)Math.Floor(pbv);
                            string formatted_pbv = pbv_value.ToString("N0"); // Formats the number with thousand separators

                            lbl_first_level_right_right_point.Text = String.Format("<b>{0}</b> BV", formatted_pbv);
                        }
                        else
                        {
                            lbl_first_level_right_right_point.Text = "<b>0</b> BV";
                        }
                    }
                }

                idr.Close();
                con.Close();

            }
        }
    }

    protected void Load_Second_Level_Left_1_Member_Details(string memberid)
    {
        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("Load_Netwotk_Tree_Member_Details", con))
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
                            lbl_second_level_1_left_member_name.Text = idr["Company_Name"].ToString();
                        }
                        else
                        {
                            lbl_second_level_1_left_member_name.Text = idr["First_Name"].ToString();
                        }
                        if (idr["Status"].ToString() == "Inactive")
                        {
                            img_second_level_1_left.Src = "img/NetworkTree/Icon_Red.png";
                        }
                        else
                        {
                            img_second_level_1_left.Src = "img/NetworkTree/Icon.png";
                        }
                        if (idr["Maintain"].ToString() == "Failed")
                        {
                            img_second_level_1_left.Src = "img/NetworkTree/Icon_Red.png";
                        }
                        else
                        {
                            img_second_level_1_left.Src = "img/NetworkTree/Icon.png";
                        }
                        lbl_second_level_1_left_member_id.Text = idr["cardno"].ToString();
                        lbl_second_level_1_left_member_rank.Text = "<b>" + idr["Rank"].ToString() + "</b>";
                        if (idr["Member_Category"].ToString() == "EO")
                        {
                            if (idr["AgentLevel"].ToString() == "BO")
                            {
                                lbl_second_level_1_left_member_rank.Text = "<b> Entrepreneur Owner </b>";
                            }
                            else if (idr["AgentLevel"].ToString() == "BBO")
                            {
                                lbl_second_level_1_left_member_rank.Text = "<b> BEO </b>";
                            }
                            else if (idr["AgentLevel"].ToString() == "BBO")
                            {
                                lbl_second_level_1_left_member_rank.Text = "<b> Bronze EO </b>";
                            }
                            else if (idr["AgentLevel"].ToString() == "SBO")
                            {
                                lbl_second_level_1_left_member_rank.Text = "<b> Silver EO </b>";
                            }
                            else if (idr["AgentLevel"].ToString() == "GBO")
                            {
                                lbl_second_level_1_left_member_rank.Text = "<b> Gold EO </b>";
                            }
                        }

                        if (idr["Downline_A_Daily_Group_Sales_BV"].ToString() != "0")
                        {
                            double pbv = Convert.ToDouble(idr["Downline_A_Daily_Group_Sales_BV"].ToString());
                            int pbv_value = (int)Math.Floor(pbv);
                            string formatted_pbv = pbv_value.ToString("N0"); // Formats the number with thousand separators

                            lbl_second_level_1_left_left_point.Text = String.Format("<b>{0}</b> BV", formatted_pbv);
                        }
                        else
                        {
                            lbl_second_level_1_left_left_point.Text = "<b>0</b> BV";
                        }

                        if (idr["Downline_B_Daily_Group_Sales_BV"].ToString() != "0")
                        {
                            double pbv = Convert.ToDouble(idr["Downline_B_Daily_Group_Sales_BV"].ToString());
                            int pbv_value = (int)Math.Floor(pbv);
                            string formatted_pbv = pbv_value.ToString("N0"); // Formats the number with thousand separators

                            lbl_second_level_1_left_right_point.Text = String.Format("<b>{0}</b> BV", formatted_pbv);
                        }
                        else
                        {
                            lbl_second_level_1_left_right_point.Text = "<b>0</b> BV";
                        }
                    }
                }

                idr.Close();
                con.Close();

            }
        }
    }

    protected void Load_Second_Level_Right_1_Member_Details(string memberid)
    {
        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("Load_Netwotk_Tree_Member_Details", con))
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
                            lbl_second_level_1_right_member_name.Text = idr["Company_Name"].ToString();
                        }
                        else
                        {
                            lbl_second_level_1_right_member_name.Text = idr["First_Name"].ToString();
                        }
                        if (idr["Status"].ToString() == "Inactive")
                        {
                            img_second_level_1_right.Src = "img/NetworkTree/Icon_Red.png";
                        }
                        else
                        {
                            img_second_level_1_right.Src = "img/NetworkTree/Icon.png";
                        }
                        if (idr["Maintain"].ToString() == "Failed")
                        {
                            img_second_level_1_right.Src = "img/NetworkTree/Icon_Red.png";
                        }
                        else
                        {
                            img_second_level_1_right.Src = "img/NetworkTree/Icon.png";
                        }
                        lbl_second_level_1_right_member_id.Text = idr["cardno"].ToString();
                        lbl_second_level_1_right_member_rank.Text = "<b>" + idr["Rank"].ToString() + "</b>";
                        if (idr["Member_Category"].ToString() == "EO")
                        {
                            if (idr["AgentLevel"].ToString() == "BO")
                            {
                                lbl_second_level_1_right_member_rank.Text = "<b> Entrepreneur Owner </b>";
                            }
                            else if (idr["AgentLevel"].ToString() == "BBO")
                            {
                                lbl_second_level_1_right_member_rank.Text = "<b> BEO </b>";
                            }
                            else if (idr["AgentLevel"].ToString() == "BBO")
                            {
                                lbl_second_level_1_right_member_rank.Text = "<b> Bronze EO </b>";
                            }
                            else if (idr["AgentLevel"].ToString() == "SBO")
                            {
                                lbl_second_level_1_right_member_rank.Text = "<b> Silver EO </b>";
                            }
                            else if (idr["AgentLevel"].ToString() == "GBO")
                            {
                                lbl_second_level_1_right_member_rank.Text = "<b> Gold EO </b>";
                            }
                        }

                        if (idr["Downline_A_Daily_Group_Sales_BV"].ToString() != "0")
                        {
                            double pbv = Convert.ToDouble(idr["Downline_A_Daily_Group_Sales_BV"].ToString());
                            int pbv_value = (int)Math.Floor(pbv);
                            string formatted_pbv = pbv_value.ToString("N0"); // Formats the number with thousand separators

                            lbl_second_level_1_right_left_point.Text = String.Format("<b>{0}</b> BV", formatted_pbv);
                        }
                        else
                        {
                            lbl_second_level_1_right_left_point.Text = "<b>0</b> BV";
                        }

                        if (idr["Downline_B_Daily_Group_Sales_BV"].ToString() != "0")
                        {
                            double pbv = Convert.ToDouble(idr["Downline_B_Daily_Group_Sales_BV"].ToString());
                            int pbv_value = (int)Math.Floor(pbv);
                            string formatted_pbv = pbv_value.ToString("N0"); // Formats the number with thousand separators

                            lbl_second_level_1_right_right_point.Text = String.Format("<b>{0}</b> BV", formatted_pbv);
                        }
                        else
                        {
                            lbl_second_level_1_right_right_point.Text = "<b>0</b> BV";
                        }
                    }
                }

                idr.Close();
                con.Close();

            }
        }
    }

    protected void Load_Second_Level_Left_2_Member_Details(string memberid)
    {
        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("Load_Netwotk_Tree_Member_Details", con))
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
                            lbl_second_level_2_left_member_name.Text = idr["Company_Name"].ToString();
                        }
                        else
                        {
                            lbl_second_level_2_left_member_name.Text = idr["First_Name"].ToString();
                        }
                        if (idr["Status"].ToString() == "Inactive")
                        {
                            img_second_level_2_left.Src = "img/NetworkTree/Icon_Red.png";
                        }
                        else
                        {
                            img_second_level_2_left.Src = "img/NetworkTree/Icon.png";
                        }
                        if (idr["Maintain"].ToString() == "Failed")
                        {
                            img_second_level_2_left.Src = "img/NetworkTree/Icon_Red.png";
                        }
                        else
                        {
                            img_second_level_2_left.Src = "img/NetworkTree/Icon.png";
                        }
                        lbl_second_level_2_left_member_id.Text = idr["cardno"].ToString();
                        lbl_second_level_2_left_member_rank.Text = "<b>" + idr["Rank"].ToString() + "</b>";
                        if (idr["Member_Category"].ToString() == "EO")
                        {
                            if (idr["AgentLevel"].ToString() == "BO")
                            {
                                lbl_second_level_2_left_member_rank.Text = "<b> Entrepreneur Owner </b>";
                            }
                            else if (idr["AgentLevel"].ToString() == "BBO")
                            {
                                lbl_second_level_2_left_member_rank.Text = "<b> BEO </b>";
                            }
                            else if (idr["AgentLevel"].ToString() == "BBO")
                            {
                                lbl_second_level_2_left_member_rank.Text = "<b> Bronze EO </b>";
                            }
                            else if (idr["AgentLevel"].ToString() == "SBO")
                            {
                                lbl_second_level_2_left_member_rank.Text = "<b> Silver EO </b>";
                            }
                            else if (idr["AgentLevel"].ToString() == "GBO")
                            {
                                lbl_second_level_2_left_member_rank.Text = "<b> Gold EO </b>";
                            }
                        }

                        if (idr["Downline_A_Daily_Group_Sales_BV"].ToString() != "0")
                        {
                            double pbv = Convert.ToDouble(idr["Downline_A_Daily_Group_Sales_BV"].ToString());
                            int pbv_value = (int)Math.Floor(pbv);
                            string formatted_pbv = pbv_value.ToString("N0"); // Formats the number with thousand separators

                            lbl_second_level_2_left_left_point.Text = String.Format("<b>{0}</b> BV", formatted_pbv);
                        }
                        else
                        {
                            lbl_second_level_2_left_left_point.Text = "<b>0</b> BV";
                        }

                        if (idr["Downline_B_Daily_Group_Sales_BV"].ToString() != "0")
                        {
                            double pbv = Convert.ToDouble(idr["Downline_B_Daily_Group_Sales_BV"].ToString());
                            int pbv_value = (int)Math.Floor(pbv);
                            string formatted_pbv = pbv_value.ToString("N0"); // Formats the number with thousand separators

                            lbl_second_level_2_left_right_point.Text = String.Format("<b>{0}</b> BV", formatted_pbv);
                        }
                        else
                        {
                            lbl_second_level_2_left_right_point.Text = "<b>0</b> BV";
                        }
                    }
                }

                idr.Close();
                con.Close();

            }
        }
    }

    protected void Load_Second_Level_Right_2_Member_Details(string memberid)
    {
        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("Load_Netwotk_Tree_Member_Details", con))
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
                            lbl_second_level_2_right_member_name.Text = idr["Company_Name"].ToString();
                        }
                        else
                        {
                            lbl_second_level_2_right_member_name.Text = idr["First_Name"].ToString();
                        }
                        if (idr["Status"].ToString() == "Inactive")
                        {
                            img_second_level_2_right.Src = "img/NetworkTree/Icon_Red.png";
                        }
                        else
                        {
                            img_second_level_2_right.Src = "img/NetworkTree/Icon.png";
                        }
                        if (idr["Maintain"].ToString() == "Failed")
                        {
                            img_second_level_2_right.Src = "img/NetworkTree/Icon_Red.png";
                        }
                        else
                        {
                            img_second_level_2_right.Src = "img/NetworkTree/Icon.png";
                        }
                        lbl_second_level_2_right_member_id.Text = idr["cardno"].ToString();
                        lbl_second_level_2_right_member_rank.Text = "<b>" + idr["Rank"].ToString() + "</b>";
                        if (idr["Member_Category"].ToString() == "EO")
                        {
                            if (idr["AgentLevel"].ToString() == "BO")
                            {
                                lbl_second_level_2_right_member_rank.Text = "<b> Entrepreneur Owner </b>";
                            }
                            else if (idr["AgentLevel"].ToString() == "BBO")
                            {
                                lbl_second_level_2_right_member_rank.Text = "<b> BEO </b>";
                            }
                            else if (idr["AgentLevel"].ToString() == "BBO")
                            {
                                lbl_second_level_2_right_member_rank.Text = "<b> Bronze EO </b>";
                            }
                            else if (idr["AgentLevel"].ToString() == "SBO")
                            {
                                lbl_second_level_2_right_member_rank.Text = "<b> Silver EO </b>";
                            }
                            else if (idr["AgentLevel"].ToString() == "GBO")
                            {
                                lbl_second_level_2_right_member_rank.Text = "<b> Gold EO </b>";
                            }
                        }

                        if (idr["Downline_A_Daily_Group_Sales_BV"].ToString() != "0")
                        {
                            double pbv = Convert.ToDouble(idr["Downline_A_Daily_Group_Sales_BV"].ToString());
                            int pbv_value = (int)Math.Floor(pbv);
                            string formatted_pbv = pbv_value.ToString("N0"); // Formats the number with thousand separators

                            lbl_second_level_2_right_left_point.Text = String.Format("<b>{0}</b> BV", formatted_pbv);
                        }
                        else
                        {
                            lbl_second_level_2_right_left_point.Text = "<b>0</b> BV";
                        }

                        if (idr["Downline_B_Daily_Group_Sales_BV"].ToString() != "0")
                        {
                            double pbv = Convert.ToDouble(idr["Downline_B_Daily_Group_Sales_BV"].ToString());
                            int pbv_value = (int)Math.Floor(pbv);
                            string formatted_pbv = pbv_value.ToString("N0"); // Formats the number with thousand separators

                            lbl_second_level_2_right_right_point.Text = String.Format("<b>{0}</b> BV", formatted_pbv);
                        }
                        else
                        {
                            lbl_second_level_2_right_right_point.Text = "<b>0</b> BV";
                        }
                    }
                }

                idr.Close();
                con.Close();

            }
        }
    }


}