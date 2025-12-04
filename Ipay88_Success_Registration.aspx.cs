using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Ipay88_Success_Registration : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string MerchantKey = "Atfp0rjUAK";
            string MerchantCode = Page.Request["MerchantCode"];
            string PaymentId = Page.Request["PaymentId"];
            string Orderid = Page.Request["RefNo"];
            string Amount = Page.Request["Amount"];
            string Currency = Page.Request["Currency"];
            string Status = Page.Request["Status"];
            string TransId = Page.Request["TransId"];
            string ErrDesc = Page.Request["ErrDesc"];
            string Signature = Page.Request["Signature"];
            string StatusFinal = "";

            if (Status == "1")
            {
                if (Signature == GetEncryptedSign2(MerchantKey, MerchantCode, PaymentId, Orderid, Amount, Currency, Status))
                {
                    Response.Write("RECEIVEOK");
                    StatusFinal = "RECEIVEOK";
                }
                else
                {
                    StatusFinal = "RECEIVENOTOK";
                }
            }
            else
            {
                StatusFinal = "RECEIVENOTOK";
            }

            if (StatusFinal == "RECEIVEOK")
            {
                DataTable user_details = new DataTable();

                using (SqlCommand cmd = new SqlCommand("Upgrade_Level_Load_Order_Details_Temporarily", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@OrderNo", Orderid);
                    con.Open();
                    SqlDataReader idr = cmd.ExecuteReader();

                    if (idr.HasRows == true)
                    {
                        user_details.Load(idr);
                    }

                    idr.Close();
                    con.Close();
                }

                string member_id = "";
                if (user_details.Rows.Count > 0)
                {
                    member_id = user_details.Rows[0]["MemberID"].ToString();
                }

                Insert_Member(member_id);

                using (SqlCommand cmd = new SqlCommand("Update_Order_Status_Temporarily", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Order_No", Orderid);
                    cmd.Parameters.AddWithValue("@Order_Status", "To Ship");
                    cmd.Parameters.AddWithValue("@Payment_Status", "Paid");
                    cmd.Parameters.AddWithValue("@Payment_Reference", TransId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                foreach (DataRow row in user_details.Rows)
                {
                    Insert_Order(row["Order_No"].ToString());
                    Calculate_Direct_Profit(row["Order_No"].ToString());
                    Calculate_RP(row["Order_No"].ToString());
                }

                SendEmail(member_id);
            }
        }
    }

    protected void Insert_Member(string memberid)
    {
        using (SqlCommand cmd = new SqlCommand("Insert_New_Member_Successful_Register", con))
        {
            con.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Member_ID", memberid);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }

    protected void Insert_Order(string order_no)
    {
        using (SqlCommand cmd = new SqlCommand("Upgrade_Level_Insert_Order", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Order_No", order_no);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }

    protected void Calculate_Direct_Profit(string orderno)
    {
        string member_id = "";
        string profit_center = "";
        DataTable user_details = new DataTable();
        DataTable package_item = new DataTable();
        DataTable normal_item = new DataTable();
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
            profit_center = user_details.Rows[0]["Profit_Center"].ToString();
            member_id = user_details.Rows[0]["MemberID"].ToString();
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

                        if (dr["Buyer_Shopper"].ToString() == "Y")
                        {
                            normal_item_total_price = (Convert.ToDecimal(dr["Normal_BV"].ToString()) * Convert.ToDecimal(dr["Qty"].ToString())) * (Convert.ToDecimal(dr["Shopper_Total_First_Level_Percentage"].ToString()) / Convert.ToDecimal(100));
                            promotion_item_total_price = (Convert.ToDecimal(dr["DC_BV"].ToString()) * Convert.ToDecimal(dr["DC_Qty"].ToString())) * (Convert.ToDecimal(dr["Shopper_Total_First_Level_Percentage"].ToString()) / Convert.ToDecimal(100));
                            percentage = Convert.ToDecimal(dr["Shopper_Total_First_Level_Percentage"].ToString());
                        }
                        else
                        {
                            normal_item_total_price = (Convert.ToDecimal(dr["Normal_BV"].ToString()) * Convert.ToDecimal(dr["Qty"].ToString())) * (Convert.ToDecimal(dr["Product_Direct_Profit_Level_1"].ToString()) / Convert.ToDecimal(100));
                            promotion_item_total_price = (Convert.ToDecimal(dr["DC_BV"].ToString()) * Convert.ToDecimal(dr["DC_Qty"].ToString())) * (Convert.ToDecimal(dr["Product_Promotion_Direct_Profit_Level_1"].ToString()) / Convert.ToDecimal(100));
                            percentage = Convert.ToDecimal(dr["Product_Direct_Profit_Level_1"].ToString());
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
                            total_percentage = Convert.ToDecimal(dr["Shopper_Total_First_Level_Percentage"].ToString());
                            total_promotion_percentage = Convert.ToDecimal(dr["Shopper_Total_First_Level_Percentage"].ToString());
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
                                            cmd.Parameters.AddWithValue("@Paid_To", last_member);
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

        using (SqlCommand cmd = new SqlCommand("Insert_Registration_Transaction_BV", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@OrderNo", orderno);
            cmd.Parameters.AddWithValue("@Member_ID", member_id);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        #endregion
    }

    protected void Calculate_RP(string orderno)
    {
        string recruiter_id = "";
        string recruiter_category = "";
        string member_category = "";
        string member_id = "";
        DataTable rp_mission = new DataTable();

        using (SqlCommand cmd = new SqlCommand("Upgrade_Level_Load_Order_Recruiter", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@OrderNo", orderno);
            con.Open();
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows == true)
            {
                DataTable recruiter_details = new DataTable();
                recruiter_details.Load(idr);
                if (recruiter_details.Rows.Count > 0)
                {
                    recruiter_id = recruiter_details.Rows[0]["Action_By"].ToString();
                    recruiter_category = recruiter_details.Rows[0]["Recruiter_Category"].ToString();
                    member_category = recruiter_details.Rows[0]["Member_Category"].ToString();
                    member_id = recruiter_details.Rows[0]["MemberID"].ToString();
                }
            }

            idr.Close();
            con.Close();
        }

        using (SqlCommand cmd = new SqlCommand("Upgrade_Level_Load_Redemption_Point_Mission", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows == true)
            {
                rp_mission.Load(idr);
            }

            idr.Close();
            con.Close();
        }

        if (member_category == "EO")
        {
            using (SqlCommand cmd = new SqlCommand("Insert_Member_Redemption_Point_Mission", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Member_ID", member_id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //if (!string.IsNullOrEmpty(recruiter_id))
        //{
        //    if (rp_mission.Rows.Count > 0)
        //    {
        //        foreach (DataRow row in rp_mission.Rows)
        //        {
        //            string signUpBO = row["Sign_Up_BO"].ToString();
        //            string signUpEO = row["Sign_Up_EO"].ToString();
        //            string redemptionPoint = row["Redemption_Point"].ToString();

        //            if (signUpBO == "1.00" && member_category == "BO")
        //            {
        //                using (SqlCommand cmd = new SqlCommand("Insert_Redemption_Point", con))
        //                {
        //                    cmd.CommandType = CommandType.StoredProcedure;
        //                    cmd.Parameters.AddWithValue("@Description", "Recruit " + member_id);
        //                    cmd.Parameters.AddWithValue("@Redemption_Point", redemptionPoint);
        //                    cmd.Parameters.AddWithValue("@Receiver", recruiter_id);
        //                    con.Open();
        //                    cmd.ExecuteNonQuery();
        //                    con.Close();
        //                }
        //            }

        //            if (signUpEO == "1.00" && member_category == "EO")
        //            {
        //                using (SqlCommand cmd = new SqlCommand("Insert_Redemption_Point", con))
        //                {
        //                    cmd.CommandType = CommandType.StoredProcedure;
        //                    cmd.Parameters.AddWithValue("@Description", "Recruit " + member_id);
        //                    cmd.Parameters.AddWithValue("@Redemption_Point", redemptionPoint);
        //                    cmd.Parameters.AddWithValue("@Receiver", recruiter_id);
        //                    con.Open();
        //                    cmd.ExecuteNonQuery();
        //                    con.Close();
        //                }
        //            }

        //            if (member_category == "EO")
        //            {
        //                using (SqlCommand cmd = new SqlCommand("Insert_Member_Redemption_Point_Mission", con))
        //                {
        //                    cmd.CommandType = CommandType.StoredProcedure;
        //                    cmd.Parameters.AddWithValue("@Member_ID", member_id);
        //                    con.Open();
        //                    cmd.ExecuteNonQuery();
        //                    con.Close();
        //                }
        //            }
        //        }
        //    }
        //}
    }

    protected void SendEmail(string memberid)
    {
        string login_id = "";
        string login_password = "";
        string referral_id = "";
        string referral_name = "";
        string profit_center = "";
        string email = "";
        using (SqlCommand cmd = new SqlCommand("Load_Member_Details_Send_Email", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Memberid", memberid);
            con.Open();
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows == true)
            {
                while (idr.Read())
                {
                    login_id = idr["cardno"].ToString();
                    referral_id = idr["referal_id"].ToString();
                    if (!string.IsNullOrEmpty(idr["Referral_Personal_Name"].ToString()))
                    {
                        referral_name = idr["Referral_Personal_Name"].ToString();
                    }
                    else
                    {
                        referral_name = idr["Referral_Company_Name"].ToString();
                    }
                    profit_center = idr["Profit_Center"].ToString();
                    email = idr["Email"].ToString();

                    byte[] encryptedBytes = Convert.FromBase64String(idr["password"].ToString());
                    string encryptionKey = "Ecentra207007PASSWORD888"; // Replace with your encryption key
                    byte[] keyBytes = Encoding.UTF8.GetBytes(encryptionKey);

                    byte[] iv = new byte[16];
                    byte[] cipherText = new byte[encryptedBytes.Length - iv.Length];

                    Array.Copy(encryptedBytes, 0, iv, 0, iv.Length);
                    Array.Copy(encryptedBytes, iv.Length, cipherText, 0, cipherText.Length);

                    using (Aes aesAlg = Aes.Create())
                    {
                        aesAlg.Key = keyBytes;
                        aesAlg.IV = iv;

                        ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                        using (var msDecrypt = new System.IO.MemoryStream())
                        {
                            using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Write))
                            {
                                csDecrypt.Write(cipherText, 0, cipherText.Length);
                                csDecrypt.FlushFinalBlock();

                                byte[] decryptedBytes = msDecrypt.ToArray();
                                string decryptedText = Encoding.UTF8.GetString(decryptedBytes);
                                login_password = decryptedText;
                            }
                        }
                    }

                }
            }

            idr.Close();
            con.Close();
        }

        string content = "";
        content = "<html><head>";
        content += "<style>body { font-family: Arial, sans-serif; background-color: #f4f4f4; padding: 20px; } .frame{ max-width: 100%; background-color: #efefef; padding: 10px;} .container { max-width: 600px; margin: 0 auto; background-color: #ffffff; padding: 20px; border-radius: 10px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);}";
        content += "h1 {color: #333333;} p {color: #666666;} .login-info { background-color: #f0f0f0; padding: 10px; border-radius: 5px; margin-top: 20px;} .login-info p {margin: 0;} .redirect{ background-color: #fde293;} </style></head>";
        content += "<body>";
        content += "<div class='frame'>";
        content += "<div class='container'><h1>Registration Successful!</h1><p>Thank you for registering with our platform.</p>";
        content += "<p>Your login credentials are as follows:</p>";
        content += "<div class='login-info'>";
        content += "<p><strong>Login ID :</strong> " + login_id + "</p><br/>";
        content += "<p><strong>Password :</strong> " + login_password + "</p><br/>";
        content += "<p><strong>Profit Center :</strong> " + profit_center + " Profit Center</p>";
        content += "</div>";
        content += "<p>Your referral details are as follows:</p>";
        content += "<div class='login-info'>";
        content += "<p><strong>Referral ID :</strong> " + referral_id + "</p><br/>";
        content += "<p><strong>Referral Name :</strong> " + referral_name + "</p>";
        content += "</div>";
        content += "<p>Please keep your login credentials secure and do not share them with anyone.</p>";
        content += "<p>You can now log in to our platform by " + "<a class='redirect' href='https://ecentra.com.my/'>click here</a>" + " and start exploring our services.</p>";
        content += "<p>Regards,<br>Ecentra Customer Service</p>";
        content += "</div>";
        content += "</div>";
        content += "</body>";
        content += "</html>";

        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
        mail.To.Add(email);
        mail.From = new MailAddress("noreply@ecentra.com.my", "ECENTRA");
        mail.SubjectEncoding = System.Text.Encoding.UTF8;

        string htmlc;

        mail.Subject = "ECENTRA";
        htmlc = content;

        mail.Body = htmlc;
        mail.BodyEncoding = System.Text.Encoding.UTF8;
        mail.Priority = MailPriority.High;
        mail.IsBodyHtml = true;

        SmtpClient client = new SmtpClient();
        client.UseDefaultCredentials = false;
        client.Credentials = new System.Net.NetworkCredential("noreply@ecentra.com.my", "ECentra123!");
        client.Port = 587;
        client.Host = "smtppro55.mschosting.network";
        client.EnableSsl = false;

        try
        {
            client.Send(mail);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(''" + ex + "'');", true);
        }
    }

    private string GetEncryptedSign2(string key, string merchant_code, string p_id, string order_no, string amt, string currency, string status)
    {
        string final_result = "";
        final_result = key + merchant_code + p_id + order_no + decimal.Parse(amt).ToString().Replace(".", "").Replace(",", "") + currency + status;
        var result = hmacSHA512(final_result, key);
        return result;
    }

    static string hmacSHA512(string result, string key)
    {
        byte[] keyBytes = Encoding.UTF8.GetBytes(key);
        using (HMACSHA512 hmacSHA512 = new HMACSHA512(keyBytes))
        {
            byte[] hashBytes = hmacSHA512.ComputeHash(Encoding.UTF8.GetBytes(result));
            StringBuilder hash = new StringBuilder();
            foreach (byte theByte in hashBytes)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }
    }

    //private string GetEncryptedSign2(string aa, string bb, string cc, string dd, string ee, string ff, string gg)
    //{
    //    string SECURITY_CODE = "Atfp0rjUAK";

    //    var result = sha256(aa,
    //        bb,
    //        cc,
    //        dd,
    //        decimal.Parse(ee).ToString().Replace(".", "").Replace(",", ""),
    //        ff,
    //        gg);
    //    return result;
    //}

    //static string sha256(string aa, string bb, string cc, string dd, string ee, string ff, string gg)
    //{
    //    string randomString = aa + bb + cc + dd + ee + ff + gg;

    //    System.Security.Cryptography.SHA256Managed crypt = new System.Security.Cryptography.SHA256Managed();
    //    System.Text.StringBuilder hash = new System.Text.StringBuilder();
    //    byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(randomString), 0, Encoding.UTF8.GetByteCount(randomString));
    //    foreach (byte theByte in crypto)
    //    {
    //        hash.Append(theByte.ToString("x2"));
    //    }
    //    return hash.ToString();
    //}
}