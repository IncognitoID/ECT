using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Text;
using System.Security.Cryptography;
using System.Web;

public partial class Payment_Success : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            bool cookieExists = HttpContext.Current.Request.Cookies["userid"] != null;
            bool cookieExists2 = HttpContext.Current.Request.Cookies["order_no"] != null;

            if (cookieExists == true)
            {
                if (Request.Cookies["userid"].Value != null && Request.Cookies["userid"].Value != "")
                {
                    if (cookieExists2 == true)
                    {
                        if (Request.Cookies["order_no"].Value != null && Request.Cookies["order_no"].Value != "")
                        {
                            div_success.Visible = true;
                            decimal itemTotal = 0;

                            using (SqlCommand cmd = new SqlCommand("Update_Order_Status_RP_Order", con))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@Order_No", Request.Cookies["order_no"].Value);
                                SqlParameter outputParam = new SqlParameter("@ItemTotal", SqlDbType.Decimal);
                                outputParam.Direction = ParameterDirection.Output;
                                cmd.Parameters.Add(outputParam);
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                                itemTotal = Convert.ToDecimal(outputParam.Value);

                            }

                            if (itemTotal > 0)
                            {
                                Calculate_Direct_Profit(Request.Cookies["order_no"].Value);
                            }

                            System.Web.HttpCookie myCookie = new System.Web.HttpCookie("order_no");
                            myCookie.Expires = DateTime.Now.AddMonths(-1);
                            myCookie.Value = string.Empty;
                            Response.Cookies.Add(myCookie);
                        }
                    }
                }
            }
        }
    }

    protected void Calculate_Direct_Profit(string orderno)
    {

        DataTable user_details = new DataTable();
        DataTable member_upline = new DataTable();
        DataTable member_upline3 = new DataTable();
        DataTable member_upline4 = new DataTable();
        DataTable member_upline5 = new DataTable();
        string first_referral_shopper = "";

        #region Load_Order_Details

        using (SqlCommand cmd = new SqlCommand("Load_Order_Details", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@OrderNo", orderno);
            con.Open();
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows == true)
            {
                user_details.Load(idr);
            }

            idr.Close();
            con.Close();
        }

        #endregion

        #region Select Buyer Referal 1, 2, 3, 4, 5

        if (user_details.Rows.Count > 0)
        {
            using (SqlCommand cmd = new SqlCommand("Load_Member_Referal", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MemberID", user_details.Rows[0]["First_Referal_ID"].ToString());
                con.Open();
                SqlDataReader idr = cmd.ExecuteReader();

                if (idr.HasRows == true)
                {
                    member_upline.Load(idr);
                }

                idr.Close();
                con.Close();
            }
        }

        if (member_upline.Rows.Count > 0)
        {
            using (SqlCommand cmd = new SqlCommand("Load_Member_Referal", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MemberID", member_upline.Rows[0]["Second_Referal_ID"].ToString());
                con.Open();
                SqlDataReader idr = cmd.ExecuteReader();

                if (idr.HasRows == true)
                {
                    member_upline3.Load(idr);
                }

                idr.Close();
                con.Close();
            }
        }

        if (member_upline3.Rows.Count > 0)
        {
            using (SqlCommand cmd = new SqlCommand("Load_Member_Referal", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MemberID", member_upline3.Rows[0]["Second_Referal_ID"].ToString());
                con.Open();
                SqlDataReader idr = cmd.ExecuteReader();

                if (idr.HasRows == true)
                {
                    member_upline4.Load(idr);
                }

                idr.Close();
                con.Close();
            }
        }

        if (member_upline4.Rows.Count > 0)
        {
            using (SqlCommand cmd = new SqlCommand("Load_Member_Referal", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MemberID", member_upline4.Rows[0]["Second_Referal_ID"].ToString());
                con.Open();
                SqlDataReader idr = cmd.ExecuteReader();

                if (idr.HasRows == true)
                {
                    member_upline5.Load(idr);
                }

                idr.Close();
                con.Close();
            }
        }

        #endregion

        #region Calculate Direct Profit

        if (user_details.Rows.Count > 0)
        {
            foreach (DataRow dr in user_details.Rows)
            {
                first_referral_shopper = user_details.Rows[0]["First_Referal_Shopper"].ToString();

                if (dr["First_Referal_ID"].ToString() != "-")
                {
                    if (first_referral_shopper == "N")
                    {
                        decimal first_referal_direct_profit = 0;
                        decimal second_referal_direct_profit = 0;
                        decimal normal_item_total_price = 0;
                        decimal promotion_item_total_price = 0;
                        decimal percentage = 0;

                        normal_item_total_price = (Convert.ToDecimal(dr["Normal_BV"].ToString()) * Convert.ToDecimal(dr["Qty"].ToString())) * (Convert.ToDecimal(dr["Product_Direct_Profit_Level_1"].ToString()) / Convert.ToDecimal(100));
                        promotion_item_total_price = (Convert.ToDecimal(dr["DC_BV"].ToString()) * Convert.ToDecimal(dr["DC_Qty"].ToString())) * (Convert.ToDecimal(dr["Product_Promotion_Direct_Profit_Level_1"].ToString()) / Convert.ToDecimal(100));
                        percentage = Convert.ToDecimal(dr["Product_Direct_Profit_Level_1"].ToString());

                        first_referal_direct_profit = normal_item_total_price + promotion_item_total_price;

                        using (SqlCommand cmd = new SqlCommand("Insert_Direct_Profit", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Type", "Direct Profit");
                            cmd.Parameters.AddWithValue("@Description", orderno + " Direct Profit Level 1");
                            cmd.Parameters.AddWithValue("@OrderNo", orderno);
                            cmd.Parameters.AddWithValue("@Transaction_Amount", dr["Total_BV_Earn"].ToString());
                            cmd.Parameters.AddWithValue("@Transaction_Rate", percentage);
                            cmd.Parameters.AddWithValue("@Paid_Amount", first_referal_direct_profit);
                            cmd.Parameters.AddWithValue("@Paid_To", dr["First_Referal_ID"].ToString());
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }

                        if (member_upline.Rows.Count > 0)
                        {
                            if (member_upline.Rows[0]["Second_Referal_ID"].ToString() != "-")
                            {
                                if (member_upline.Rows[0]["Shopper"].ToString() == "N")
                                {
                                    normal_item_total_price = (Convert.ToDecimal(dr["Normal_BV"].ToString()) * Convert.ToDecimal(dr["Qty"].ToString())) * (Convert.ToDecimal(dr["Product_Direct_Profit_Level_2"].ToString()) / Convert.ToDecimal(100));
                                    promotion_item_total_price = (Convert.ToDecimal(dr["DC_BV"].ToString()) * Convert.ToDecimal(dr["DC_Qty"].ToString())) * (Convert.ToDecimal(dr["Product_Promotion_Direct_Profit_Level_2"].ToString()) / Convert.ToDecimal(100));
                                    percentage = Convert.ToDecimal(dr["Product_Direct_Profit_Level_2"].ToString());

                                    second_referal_direct_profit = normal_item_total_price + promotion_item_total_price;

                                    using (SqlCommand cmd = new SqlCommand("Insert_Direct_Profit", con))
                                    {
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@Type", "Direct Profit");
                                        cmd.Parameters.AddWithValue("@Description", orderno + " Direct Profit Level 2");
                                        cmd.Parameters.AddWithValue("@OrderNo", orderno);
                                        cmd.Parameters.AddWithValue("@Transaction_Amount", dr["Total_BV_Earn"].ToString());
                                        cmd.Parameters.AddWithValue("@Transaction_Rate", percentage);
                                        cmd.Parameters.AddWithValue("@Paid_Amount", second_referal_direct_profit);
                                        cmd.Parameters.AddWithValue("@Paid_To", member_upline.Rows[0]["Second_Referal_ID"].ToString());
                                        con.Open();
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                    }
                                }
                                else
                                {
                                    DataTable no_shopper = new DataTable();
                                    string last_member = member_upline.Rows[0]["Second_Referal_ID"].ToString();

                                    while (true)
                                    {
                                        using (SqlCommand cmd = new SqlCommand("Load_Member_Referal", con))
                                        {
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.Parameters.AddWithValue("@MemberID", last_member);
                                            con.Open();
                                            using (SqlDataReader idr = cmd.ExecuteReader())
                                            {
                                                no_shopper.Clear();
                                                no_shopper.Load(idr);
                                            }
                                            con.Close();
                                        }

                                        if (no_shopper.Rows.Count > 0)
                                        {
                                            if (no_shopper.Rows[0]["Shopper"].ToString() == "N")
                                            {
                                                last_member = no_shopper.Rows[0]["Second_Referal_ID"].ToString();
                                                break;
                                            }
                                            else
                                            {
                                                last_member = no_shopper.Rows[0]["Second_Referal_ID"].ToString();
                                            }
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }

                                    if (last_member != "-")
                                    {
                                        normal_item_total_price = (Convert.ToDecimal(dr["Normal_BV"].ToString()) * Convert.ToDecimal(dr["Qty"].ToString())) * (Convert.ToDecimal(dr["Product_Direct_Profit_Level_2"].ToString()) / Convert.ToDecimal(100));
                                        promotion_item_total_price = (Convert.ToDecimal(dr["DC_BV"].ToString()) * Convert.ToDecimal(dr["DC_Qty"].ToString())) * (Convert.ToDecimal(dr["Product_Promotion_Direct_Profit_Level_2"].ToString()) / Convert.ToDecimal(100));
                                        percentage = Convert.ToDecimal(dr["Product_Direct_Profit_Level_2"].ToString());

                                        second_referal_direct_profit = normal_item_total_price + promotion_item_total_price;

                                        using (SqlCommand cmd = new SqlCommand("Insert_Direct_Profit", con))
                                        {
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.Parameters.AddWithValue("@Type", "Direct Profit");
                                            cmd.Parameters.AddWithValue("@Description", orderno + " Direct Profit Level 2");
                                            cmd.Parameters.AddWithValue("@OrderNo", orderno);
                                            cmd.Parameters.AddWithValue("@Transaction_Amount", dr["Total_BV_Earn"].ToString());
                                            cmd.Parameters.AddWithValue("@Transaction_Rate", percentage);
                                            cmd.Parameters.AddWithValue("@Paid_Amount", second_referal_direct_profit);
                                            cmd.Parameters.AddWithValue("@Paid_To", last_member);
                                            con.Open();
                                            cmd.ExecuteNonQuery();
                                            con.Close();
                                        }
                                    }
                                }
                            }
                        }

                        if (member_upline3.Rows.Count > 0)
                        {
                            if (member_upline3.Rows[0]["Second_Referal_ID"].ToString() != "-")
                            {
                                if (member_upline3.Rows[0]["Member_Category"].ToString() == "EO")
                                {
                                    normal_item_total_price = (Convert.ToDecimal(dr["Normal_BV"].ToString()) * Convert.ToDecimal(dr["Qty"].ToString())) * (Convert.ToDecimal(dr["Product_Direct_Profit_Level_3"].ToString()) / Convert.ToDecimal(100));
                                    promotion_item_total_price = (Convert.ToDecimal(dr["DC_BV"].ToString()) * Convert.ToDecimal(dr["DC_Qty"].ToString())) * (Convert.ToDecimal(dr["Product_Promotion_Direct_Profit_Level_3"].ToString()) / Convert.ToDecimal(100));
                                    percentage = Convert.ToDecimal(dr["Product_Direct_Profit_Level_3"].ToString());

                                    second_referal_direct_profit = normal_item_total_price + promotion_item_total_price;

                                    using (SqlCommand cmd = new SqlCommand("Insert_Direct_Profit", con))
                                    {
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@Type", "Direct Profit");
                                        cmd.Parameters.AddWithValue("@Description", orderno + " Direct Profit Level 3");
                                        cmd.Parameters.AddWithValue("@OrderNo", orderno);
                                        cmd.Parameters.AddWithValue("@Transaction_Amount", dr["Total_BV_Earn"].ToString());
                                        cmd.Parameters.AddWithValue("@Transaction_Rate", percentage);
                                        cmd.Parameters.AddWithValue("@Paid_Amount", second_referal_direct_profit);
                                        cmd.Parameters.AddWithValue("@Paid_To", member_upline3.Rows[0]["Second_Referal_ID"].ToString());
                                        con.Open();
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                    }
                                }
                            }
                        }

                        if (member_upline4.Rows.Count > 0)
                        {
                            if (member_upline4.Rows[0]["Second_Referal_ID"].ToString() != "-")
                            {
                                if (member_upline4.Rows[0]["Member_Category"].ToString() == "EO")
                                {
                                    normal_item_total_price = (Convert.ToDecimal(dr["Normal_BV"].ToString()) * Convert.ToDecimal(dr["Qty"].ToString())) * (Convert.ToDecimal(dr["Product_Direct_Profit_Level_4"].ToString()) / Convert.ToDecimal(100));
                                    promotion_item_total_price = (Convert.ToDecimal(dr["DC_BV"].ToString()) * Convert.ToDecimal(dr["DC_Qty"].ToString())) * (Convert.ToDecimal(dr["Product_Promotion_Direct_Profit_Level_4"].ToString()) / Convert.ToDecimal(100));
                                    percentage = Convert.ToDecimal(dr["Product_Direct_Profit_Level_4"].ToString());

                                    second_referal_direct_profit = normal_item_total_price + promotion_item_total_price;

                                    using (SqlCommand cmd = new SqlCommand("Insert_Direct_Profit", con))
                                    {
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@Type", "Direct Profit");
                                        cmd.Parameters.AddWithValue("@Description", orderno + " Direct Profit Level 4");
                                        cmd.Parameters.AddWithValue("@OrderNo", orderno);
                                        cmd.Parameters.AddWithValue("@Transaction_Amount", dr["Total_BV_Earn"].ToString());
                                        cmd.Parameters.AddWithValue("@Transaction_Rate", percentage);
                                        cmd.Parameters.AddWithValue("@Paid_Amount", second_referal_direct_profit);
                                        cmd.Parameters.AddWithValue("@Paid_To", member_upline4.Rows[0]["Second_Referal_ID"].ToString());
                                        con.Open();
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                    }
                                }
                            }
                        }

                        if (member_upline5.Rows.Count > 0)
                        {
                            if (member_upline5.Rows[0]["Second_Referal_ID"].ToString() != "-")
                            {
                                if (member_upline5.Rows[0]["Member_Category"].ToString() == "EO")
                                {
                                    normal_item_total_price = (Convert.ToDecimal(dr["Normal_BV"].ToString()) * Convert.ToDecimal(dr["Qty"].ToString())) * (Convert.ToDecimal(dr["Product_Direct_Profit_Level_5"].ToString()) / Convert.ToDecimal(100));
                                    promotion_item_total_price = (Convert.ToDecimal(dr["DC_BV"].ToString()) * Convert.ToDecimal(dr["DC_Qty"].ToString())) * (Convert.ToDecimal(dr["Product_Promotion_Direct_Profit_Level_5"].ToString()) / Convert.ToDecimal(100));
                                    percentage = Convert.ToDecimal(dr["Product_Direct_Profit_Level_5"].ToString());

                                    second_referal_direct_profit = normal_item_total_price + promotion_item_total_price;

                                    using (SqlCommand cmd = new SqlCommand("Insert_Direct_Profit", con))
                                    {
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@Type", "Direct Profit");
                                        cmd.Parameters.AddWithValue("@Description", orderno + " Direct Profit Level 5");
                                        cmd.Parameters.AddWithValue("@OrderNo", orderno);
                                        cmd.Parameters.AddWithValue("@Transaction_Amount", dr["Total_BV_Earn"].ToString());
                                        cmd.Parameters.AddWithValue("@Transaction_Rate", percentage);
                                        cmd.Parameters.AddWithValue("@Paid_Amount", second_referal_direct_profit);
                                        cmd.Parameters.AddWithValue("@Paid_To", member_upline5.Rows[0]["Second_Referal_ID"].ToString());
                                        con.Open();
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        decimal first_referal_direct_profit = 0;
                        decimal second_referal_direct_profit = 0;
                        decimal normal_item_total_price = 0;
                        decimal promotion_item_total_price = 0;
                        decimal total_percentage = 0;
                        decimal total_promotion_percentage = 0;
                        decimal percentage = 0;

                        if (dr["Buyer_Shopper"].ToString() == "Y")
                        {
                            total_percentage = Convert.ToDecimal(dr["Shopper_First_Level_Percentage"].ToString());
                            total_promotion_percentage = Convert.ToDecimal(dr["Shopper_First_Level_Percentage"].ToString());
                        }
                        else
                        {
                            total_percentage = Convert.ToDecimal(dr["Product_Direct_Profit_Level_1"].ToString());
                            total_promotion_percentage = Convert.ToDecimal(dr["Product_Promotion_Direct_Profit_Level_1"].ToString());
                        }

                        if (total_percentage > Convert.ToDecimal(dr["Shopper_First_Level_Percentage"].ToString()))
                        {
                            normal_item_total_price = (Convert.ToDecimal(dr["Normal_BV"].ToString()) * Convert.ToDecimal(dr["Qty"].ToString())) * (Convert.ToDecimal(dr["Shopper_First_Level_Percentage"].ToString()) / Convert.ToDecimal(100));
                            percentage = Convert.ToDecimal(dr["Shopper_First_Level_Percentage"].ToString());
                        }
                        else
                        {
                            normal_item_total_price = (Convert.ToDecimal(dr["Normal_BV"].ToString()) * Convert.ToDecimal(dr["Qty"].ToString())) * (Convert.ToDecimal(dr["Product_Direct_Profit_Level_1"].ToString()) / Convert.ToDecimal(100));
                            percentage = Convert.ToDecimal(dr["Product_Direct_Profit_Level_1"].ToString());
                        }

                        if (total_promotion_percentage > Convert.ToDecimal(dr["Shopper_First_Level_Percentage"].ToString()))
                        {
                            promotion_item_total_price = (Convert.ToDecimal(dr["DC_BV"].ToString()) * Convert.ToDecimal(dr["DC_Qty"].ToString())) * (Convert.ToDecimal(dr["Shopper_First_Level_Percentage"].ToString()) / Convert.ToDecimal(100));
                        }
                        else
                        {
                            promotion_item_total_price = (Convert.ToDecimal(dr["DC_BV"].ToString()) * Convert.ToDecimal(dr["DC_Qty"].ToString())) * (Convert.ToDecimal(dr["Product_Promotion_Direct_Profit_Level_1"].ToString()) / Convert.ToDecimal(100));
                        }

                        first_referal_direct_profit = normal_item_total_price + promotion_item_total_price;

                        using (SqlCommand cmd = new SqlCommand("Insert_Direct_Profit", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Type", "Direct Profit");
                            cmd.Parameters.AddWithValue("@Description", orderno + " Direct Profit Level 1");
                            cmd.Parameters.AddWithValue("@OrderNo", orderno);
                            cmd.Parameters.AddWithValue("@Transaction_Amount", dr["Total_BV_Earn"].ToString());
                            cmd.Parameters.AddWithValue("@Transaction_Rate", percentage);
                            cmd.Parameters.AddWithValue("@Paid_Amount", first_referal_direct_profit);
                            cmd.Parameters.AddWithValue("@Paid_To", dr["First_Referal_ID"].ToString());
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }

                        if (member_upline.Rows.Count > 0)
                        {
                            if (member_upline.Rows[0]["Second_Referal_ID"].ToString() != "-")
                            {
                                if (member_upline.Rows[0]["Shopper"].ToString() == "N")
                                {
                                    decimal remaining_percentage = 0;
                                    decimal remaining_promotion_percentage = 0;
                                    remaining_percentage = total_percentage - percentage;
                                    remaining_promotion_percentage = total_promotion_percentage - percentage;

                                    if (remaining_percentage > 0 || remaining_promotion_percentage > 0)
                                    {
                                        normal_item_total_price = (Convert.ToDecimal(dr["Normal_BV"].ToString()) * Convert.ToDecimal(dr["Qty"].ToString())) * (remaining_percentage / Convert.ToDecimal(100));
                                        promotion_item_total_price = (Convert.ToDecimal(dr["DC_BV"].ToString()) * Convert.ToDecimal(dr["DC_Qty"].ToString())) * (remaining_promotion_percentage / Convert.ToDecimal(100));

                                        second_referal_direct_profit = normal_item_total_price + promotion_item_total_price;

                                        using (SqlCommand cmd = new SqlCommand("Insert_Direct_Profit", con))
                                        {
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.Parameters.AddWithValue("@Type", "Direct Profit");
                                            cmd.Parameters.AddWithValue("@Description", orderno + " Direct Profit Level 1");
                                            cmd.Parameters.AddWithValue("@OrderNo", orderno);
                                            cmd.Parameters.AddWithValue("@Transaction_Amount", dr["Total_BV_Earn"].ToString());
                                            cmd.Parameters.AddWithValue("@Transaction_Rate", remaining_percentage);
                                            cmd.Parameters.AddWithValue("@Paid_Amount", second_referal_direct_profit);
                                            cmd.Parameters.AddWithValue("@Paid_To", member_upline.Rows[0]["Second_Referal_ID"].ToString());
                                            con.Open();
                                            cmd.ExecuteNonQuery();
                                            con.Close();
                                        }

                                        DataTable no_shopper = new DataTable();
                                        string last_member = member_upline.Rows[0]["Second_Referal_ID"].ToString();

                                        while (true)
                                        {
                                            using (SqlCommand cmd = new SqlCommand("Load_Member_Referal", con))
                                            {
                                                cmd.CommandType = CommandType.StoredProcedure;
                                                cmd.Parameters.AddWithValue("@MemberID", last_member);
                                                con.Open();
                                                using (SqlDataReader idr = cmd.ExecuteReader())
                                                {
                                                    no_shopper.Clear();
                                                    no_shopper.Load(idr);
                                                }
                                                con.Close();
                                            }

                                            if (no_shopper.Rows.Count > 0)
                                            {
                                                if (no_shopper.Rows[0]["Shopper"].ToString() == "N")
                                                {
                                                    last_member = no_shopper.Rows[0]["Second_Referal_ID"].ToString();
                                                    break;
                                                }
                                                else
                                                {
                                                    last_member = no_shopper.Rows[0]["Second_Referal_ID"].ToString();
                                                }
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }

                                        if (last_member != "-")
                                        {
                                            normal_item_total_price = (Convert.ToDecimal(dr["Normal_BV"].ToString()) * Convert.ToDecimal(dr["Qty"].ToString())) * (Convert.ToDecimal(dr["Product_Direct_Profit_Level_2"].ToString()) / Convert.ToDecimal(100));
                                            promotion_item_total_price = (Convert.ToDecimal(dr["DC_BV"].ToString()) * Convert.ToDecimal(dr["DC_Qty"].ToString())) * (Convert.ToDecimal(dr["Product_Promotion_Direct_Profit_Level_2"].ToString()) / Convert.ToDecimal(100));
                                            percentage = Convert.ToDecimal(dr["Product_Direct_Profit_Level_2"].ToString());

                                            second_referal_direct_profit = normal_item_total_price + promotion_item_total_price;

                                            using (SqlCommand cmd = new SqlCommand("Insert_Direct_Profit", con))
                                            {
                                                cmd.CommandType = CommandType.StoredProcedure;
                                                cmd.Parameters.AddWithValue("@Type", "Direct Profit");
                                                cmd.Parameters.AddWithValue("@Description", orderno + " Direct Profit Level 2");
                                                cmd.Parameters.AddWithValue("@OrderNo", orderno);
                                                cmd.Parameters.AddWithValue("@Transaction_Amount", dr["Total_BV_Earn"].ToString());
                                                cmd.Parameters.AddWithValue("@Transaction_Rate", percentage);
                                                cmd.Parameters.AddWithValue("@Paid_Amount", second_referal_direct_profit);
                                                cmd.Parameters.AddWithValue("@Paid_To", last_member);
                                                con.Open();
                                                cmd.ExecuteNonQuery();
                                                con.Close();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        normal_item_total_price = (Convert.ToDecimal(dr["Normal_BV"].ToString()) * Convert.ToDecimal(dr["Qty"].ToString())) * (Convert.ToDecimal(dr["Product_Direct_Profit_Level_2"].ToString()) / Convert.ToDecimal(100));
                                        promotion_item_total_price = (Convert.ToDecimal(dr["DC_BV"].ToString()) * Convert.ToDecimal(dr["DC_Qty"].ToString())) * (Convert.ToDecimal(dr["Product_Promotion_Direct_Profit_Level_2"].ToString()) / Convert.ToDecimal(100));
                                        percentage = Convert.ToDecimal(dr["Product_Direct_Profit_Level_2"].ToString());

                                        second_referal_direct_profit = normal_item_total_price + promotion_item_total_price;

                                        using (SqlCommand cmd = new SqlCommand("Insert_Direct_Profit", con))
                                        {
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.Parameters.AddWithValue("@Type", "Direct Profit");
                                            cmd.Parameters.AddWithValue("@Description", orderno + " Direct Profit Level 2");
                                            cmd.Parameters.AddWithValue("@OrderNo", orderno);
                                            cmd.Parameters.AddWithValue("@Transaction_Amount", dr["Total_BV_Earn"].ToString());
                                            cmd.Parameters.AddWithValue("@Transaction_Rate", percentage);
                                            cmd.Parameters.AddWithValue("@Paid_Amount", second_referal_direct_profit);
                                            cmd.Parameters.AddWithValue("@Paid_To", member_upline.Rows[0]["Second_Referal_ID"].ToString());
                                            con.Open();
                                            cmd.ExecuteNonQuery();
                                            con.Close();
                                        }
                                    }

                                }
                                else
                                {
                                    DataTable no_shopper = new DataTable();
                                    string last_member = member_upline.Rows[0]["Second_Referal_ID"].ToString();

                                    while (true)
                                    {
                                        using (SqlCommand cmd = new SqlCommand("Load_Member_Referal", con))
                                        {
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.Parameters.AddWithValue("@MemberID", last_member);
                                            con.Open();
                                            using (SqlDataReader idr = cmd.ExecuteReader())
                                            {
                                                no_shopper.Clear();
                                                no_shopper.Load(idr);
                                            }
                                            con.Close();
                                        }

                                        if (no_shopper.Rows.Count > 0)
                                        {
                                            if (no_shopper.Rows[0]["Shopper"].ToString() == "N")
                                            {
                                                last_member = no_shopper.Rows[0]["Second_Referal_ID"].ToString();
                                                break;
                                            }
                                            else
                                            {
                                                last_member = no_shopper.Rows[0]["Second_Referal_ID"].ToString();
                                            }
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }

                                    if (last_member != "-")
                                    {
                                        decimal remaining_percentage = 0;
                                        decimal remaining_promotion_percentage = 0;
                                        remaining_percentage = total_percentage - percentage;
                                        remaining_promotion_percentage = total_promotion_percentage - percentage;

                                        normal_item_total_price = (Convert.ToDecimal(dr["Normal_BV"].ToString()) * Convert.ToDecimal(dr["Qty"].ToString())) * (remaining_percentage / Convert.ToDecimal(100));
                                        promotion_item_total_price = (Convert.ToDecimal(dr["DC_BV"].ToString()) * Convert.ToDecimal(dr["DC_Qty"].ToString())) * (remaining_promotion_percentage / Convert.ToDecimal(100));

                                        second_referal_direct_profit = normal_item_total_price + promotion_item_total_price;

                                        using (SqlCommand cmd = new SqlCommand("Insert_Direct_Profit", con))
                                        {
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.Parameters.AddWithValue("@Type", "Direct Profit");
                                            cmd.Parameters.AddWithValue("@Description", orderno + " Direct Profit Level 1");
                                            cmd.Parameters.AddWithValue("@OrderNo", orderno);
                                            cmd.Parameters.AddWithValue("@Transaction_Amount", dr["Total_BV_Earn"].ToString());
                                            cmd.Parameters.AddWithValue("@Transaction_Rate", remaining_percentage);
                                            cmd.Parameters.AddWithValue("@Paid_Amount", second_referal_direct_profit);
                                            cmd.Parameters.AddWithValue("@Paid_To", last_member);
                                            con.Open();
                                            cmd.ExecuteNonQuery();
                                            con.Close();
                                        }
                                    }

                                    DataTable no_shopper_2 = new DataTable();
                                    string last_member_2 = last_member;

                                    while (true)
                                    {
                                        using (SqlCommand cmd = new SqlCommand("Load_Member_Referal", con))
                                        {
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.Parameters.AddWithValue("@MemberID", last_member_2);
                                            con.Open();
                                            using (SqlDataReader idr = cmd.ExecuteReader())
                                            {
                                                no_shopper_2.Clear();
                                                no_shopper_2.Load(idr);
                                            }
                                            con.Close();
                                        }

                                        if (no_shopper_2.Rows.Count > 0)
                                        {
                                            if (no_shopper_2.Rows[0]["Shopper"].ToString() == "N")
                                            {
                                                last_member_2 = no_shopper_2.Rows[0]["Second_Referal_ID"].ToString();
                                                break;
                                            }
                                            else
                                            {
                                                last_member_2 = no_shopper_2.Rows[0]["Second_Referal_ID"].ToString();
                                            }
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }

                                    if (last_member != "-")
                                    {
                                        if (dr["Buyer_Shopper"].ToString() == "Y")
                                        {
                                            normal_item_total_price = (Convert.ToDecimal(dr["Normal_BV"].ToString()) * Convert.ToDecimal(dr["Qty"].ToString())) * (Convert.ToDecimal(dr["Shopper_Total_Second_Level_Percentage"].ToString()) / Convert.ToDecimal(100));
                                            promotion_item_total_price = (Convert.ToDecimal(dr["DC_BV"].ToString()) * Convert.ToDecimal(dr["DC_Qty"].ToString())) * (Convert.ToDecimal(dr["Shopper_Total_Second_Level_Percentage"].ToString()) / Convert.ToDecimal(100));
                                            percentage = Convert.ToDecimal(dr["Shopper_Total_Second_Level_Percentage"].ToString());
                                        }
                                        else
                                        {
                                            normal_item_total_price = (Convert.ToDecimal(dr["Normal_BV"].ToString()) * Convert.ToDecimal(dr["Qty"].ToString())) * (Convert.ToDecimal(dr["Product_Direct_Profit_Level_2"].ToString()) / Convert.ToDecimal(100));
                                            promotion_item_total_price = (Convert.ToDecimal(dr["DC_BV"].ToString()) * Convert.ToDecimal(dr["DC_Qty"].ToString())) * (Convert.ToDecimal(dr["Product_Promotion_Direct_Profit_Level_2"].ToString()) / Convert.ToDecimal(100));
                                            percentage = Convert.ToDecimal(dr["Product_Direct_Profit_Level_2"].ToString());
                                        }

                                        second_referal_direct_profit = normal_item_total_price + promotion_item_total_price;

                                        using (SqlCommand cmd = new SqlCommand("Insert_Direct_Profit", con))
                                        {
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.Parameters.AddWithValue("@Type", "Direct Profit");
                                            cmd.Parameters.AddWithValue("@Description", orderno + " Direct Profit Level 2");
                                            cmd.Parameters.AddWithValue("@OrderNo", orderno);
                                            cmd.Parameters.AddWithValue("@Transaction_Amount", dr["Total_BV_Earn"].ToString());
                                            cmd.Parameters.AddWithValue("@Transaction_Rate", percentage);
                                            cmd.Parameters.AddWithValue("@Paid_Amount", second_referal_direct_profit);
                                            cmd.Parameters.AddWithValue("@Paid_To", last_member_2);
                                            con.Open();
                                            cmd.ExecuteNonQuery();
                                            con.Close();
                                        }
                                    }
                                }
                            }
                        }

                        if (member_upline3.Rows.Count > 0)
                        {
                            if (member_upline3.Rows[0]["Second_Referal_ID"].ToString() != "-")
                            {
                                if (member_upline3.Rows[0]["Member_Category"].ToString() == "EO")
                                {
                                    normal_item_total_price = (Convert.ToDecimal(dr["Normal_BV"].ToString()) * Convert.ToDecimal(dr["Qty"].ToString())) * (Convert.ToDecimal(dr["Product_Direct_Profit_Level_3"].ToString()) / Convert.ToDecimal(100));
                                    promotion_item_total_price = (Convert.ToDecimal(dr["DC_BV"].ToString()) * Convert.ToDecimal(dr["DC_Qty"].ToString())) * (Convert.ToDecimal(dr["Product_Promotion_Direct_Profit_Level_3"].ToString()) / Convert.ToDecimal(100));
                                    percentage = Convert.ToDecimal(dr["Product_Direct_Profit_Level_3"].ToString());

                                    second_referal_direct_profit = normal_item_total_price + promotion_item_total_price;

                                    using (SqlCommand cmd = new SqlCommand("Insert_Direct_Profit", con))
                                    {
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@Type", "Direct Profit");
                                        cmd.Parameters.AddWithValue("@Description", orderno + " Direct Profit Level 3");
                                        cmd.Parameters.AddWithValue("@OrderNo", orderno);
                                        cmd.Parameters.AddWithValue("@Transaction_Amount", dr["Total_BV_Earn"].ToString());
                                        cmd.Parameters.AddWithValue("@Transaction_Rate", percentage);
                                        cmd.Parameters.AddWithValue("@Paid_Amount", second_referal_direct_profit);
                                        cmd.Parameters.AddWithValue("@Paid_To", member_upline3.Rows[0]["Second_Referal_ID"].ToString());
                                        con.Open();
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                    }
                                }
                            }
                        }

                        if (member_upline4.Rows.Count > 0)
                        {
                            if (member_upline4.Rows[0]["Second_Referal_ID"].ToString() != "-")
                            {
                                if (member_upline4.Rows[0]["Member_Category"].ToString() == "EO")
                                {
                                    normal_item_total_price = (Convert.ToDecimal(dr["Normal_BV"].ToString()) * Convert.ToDecimal(dr["Qty"].ToString())) * (Convert.ToDecimal(dr["Product_Direct_Profit_Level_4"].ToString()) / Convert.ToDecimal(100));
                                    promotion_item_total_price = (Convert.ToDecimal(dr["DC_BV"].ToString()) * Convert.ToDecimal(dr["DC_Qty"].ToString())) * (Convert.ToDecimal(dr["Product_Promotion_Direct_Profit_Level_4"].ToString()) / Convert.ToDecimal(100));
                                    percentage = Convert.ToDecimal(dr["Product_Direct_Profit_Level_4"].ToString());

                                    second_referal_direct_profit = normal_item_total_price + promotion_item_total_price;

                                    using (SqlCommand cmd = new SqlCommand("Insert_Direct_Profit", con))
                                    {
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@Type", "Direct Profit");
                                        cmd.Parameters.AddWithValue("@Description", orderno + " Direct Profit Level 4");
                                        cmd.Parameters.AddWithValue("@OrderNo", orderno);
                                        cmd.Parameters.AddWithValue("@Transaction_Amount", dr["Total_BV_Earn"].ToString());
                                        cmd.Parameters.AddWithValue("@Transaction_Rate", percentage);
                                        cmd.Parameters.AddWithValue("@Paid_Amount", second_referal_direct_profit);
                                        cmd.Parameters.AddWithValue("@Paid_To", member_upline4.Rows[0]["Second_Referal_ID"].ToString());
                                        con.Open();
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                    }
                                }
                            }
                        }

                        if (member_upline5.Rows.Count > 0)
                        {
                            if (member_upline5.Rows[0]["Second_Referal_ID"].ToString() != "-")
                            {
                                if (member_upline5.Rows[0]["Member_Category"].ToString() == "EO")
                                {
                                    normal_item_total_price = (Convert.ToDecimal(dr["Normal_BV"].ToString()) * Convert.ToDecimal(dr["Qty"].ToString())) * (Convert.ToDecimal(dr["Product_Direct_Profit_Level_5"].ToString()) / Convert.ToDecimal(100));
                                    promotion_item_total_price = (Convert.ToDecimal(dr["DC_BV"].ToString()) * Convert.ToDecimal(dr["DC_Qty"].ToString())) * (Convert.ToDecimal(dr["Product_Promotion_Direct_Profit_Level_5"].ToString()) / Convert.ToDecimal(100));
                                    percentage = Convert.ToDecimal(dr["Product_Direct_Profit_Level_5"].ToString());

                                    second_referal_direct_profit = normal_item_total_price + promotion_item_total_price;

                                    using (SqlCommand cmd = new SqlCommand("Insert_Direct_Profit", con))
                                    {
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@Type", "Direct Profit");
                                        cmd.Parameters.AddWithValue("@Description", orderno + " Direct Profit Level 5");
                                        cmd.Parameters.AddWithValue("@OrderNo", orderno);
                                        cmd.Parameters.AddWithValue("@Transaction_Amount", dr["Total_BV_Earn"].ToString());
                                        cmd.Parameters.AddWithValue("@Transaction_Rate", percentage);
                                        cmd.Parameters.AddWithValue("@Paid_Amount", second_referal_direct_profit);
                                        cmd.Parameters.AddWithValue("@Paid_To", member_upline5.Rows[0]["Second_Referal_ID"].ToString());
                                        con.Open();
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        using (SqlCommand cmd = new SqlCommand("Insert_Transaction_BV", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@OrderNo", orderno);
            cmd.Parameters.AddWithValue("@Split_Bill", "N");
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        #endregion
    }

}