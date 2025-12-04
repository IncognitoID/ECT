using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Ipay88_Success_Upgrade_Level : System.Web.UI.Page
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
                Update_Profit_Center(Orderid);

                using (SqlCommand cmd = new SqlCommand("Update_Upgrade_Level_Order_Status", con))
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

                DataTable order = new DataTable();
                using (SqlCommand cmd = new SqlCommand("Upgrade_Level_Load_All_Order", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Order_No", Orderid);
                    con.Open();
                    SqlDataReader idr = cmd.ExecuteReader();

                    if (idr.HasRows == true)
                    {
                        order.Load(idr);
                    }

                    idr.Close();
                    con.Close();
                }

                foreach (DataRow row in order.Rows)
                {
                    Insert_Order(row["Order_No"].ToString());
                    Calculate_Direct_Profit(row["Order_No"].ToString());
                }

            }
        }
    }

    protected void Update_Profit_Center(string orderno)
    {
        using (SqlCommand cmd = new SqlCommand("Upgrade_Level_Load_Member_Package", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Order_No", orderno);
            con.Open();
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows == true)
            {
                DataTable v = new DataTable();
                v.Load(idr);

                if (v.Rows[0]["Upgrade_Level"].ToString() == "EO1")
                {
                    con.Close();
                    Upgrade_To_EO(v.Rows[0]["Default_Member_ID"].ToString(), "Main Account", v.Rows[0]["Upgrade_Level"].ToString());
                    Insert_Redemption_Mission(v.Rows[0]["Default_Member_ID"].ToString(), "Main Account");
                }
                else if (v.Rows[0]["Upgrade_Level"].ToString() == "EO5")
                {
                    if (v.Rows[0]["Member_Category"].ToString() == "BO")
                    {
                        if (v.Rows[0]["Profit_Center"].ToString() == "1")
                        {
                            con.Close();
                            Need_Update_Profit_Center(v.Rows[0]["MemberID"].ToString());
                            Upgrade_To_EO(v.Rows[0]["Default_Member_ID"].ToString(), "Main Account", v.Rows[0]["Upgrade_Level"].ToString());
                            Insert_Redemption_Mission(v.Rows[0]["Default_Member_ID"].ToString(), "Main Account");
                            Update_Redemption_Mission(v.Rows[0]["Default_Member_ID"].ToString(), "2 BO");
                        }
                        else if (v.Rows[0]["Profit_Center"].ToString() == "3")
                        {
                            con.Close();
                            Upgrade_To_EO(v.Rows[0]["Default_Member_ID"].ToString(), "Main Account", v.Rows[0]["Upgrade_Level"].ToString());
                            Insert_Redemption_Mission(v.Rows[0]["Default_Member_ID"].ToString(), "Main Account");
                        }
                    }
                    else if (v.Rows[0]["Member_Category"].ToString() == "EO")
                    {
                        if (v.Rows[0]["Profit_Center"].ToString() == "1")
                        {
                            con.Close();
                            Need_Update_Profit_Center(v.Rows[0]["Default_Member_ID"].ToString());
                            Insert_Redemption_Mission(v.Rows[0]["Default_Member_ID"].ToString(), "Main Account");
                            Update_Redemption_Mission(v.Rows[0]["Default_Member_ID"].ToString(), "2 BO");
                        }
                    }
                }
                else if (v.Rows[0]["Upgrade_Level"].ToString() == "EO9")
                {
                    if (v.Rows[0]["Member_Category"].ToString() == "BO")
                    {
                        if (v.Rows[0]["Profit_Center"].ToString() == "1")
                        {
                            con.Close();
                            Need_Update_Profit_Center(v.Rows[0]["Default_Member_ID"].ToString());
                            Upgrade_To_EO(v.Rows[0]["Default_Member_ID"].ToString(), "All Account", v.Rows[0]["Upgrade_Level"].ToString());
                            Insert_Redemption_Mission(v.Rows[0]["Default_Member_ID"].ToString(), "All Account");
                            Update_Redemption_Mission(v.Rows[0]["Default_Member_ID"].ToString(), "2 EO");
                        }
                        else if (v.Rows[0]["Profit_Center"].ToString() == "3")
                        {
                            con.Close();
                            Upgrade_To_EO(v.Rows[0]["Default_Member_ID"].ToString(), "All Account", v.Rows[0]["Upgrade_Level"].ToString());
                            Insert_Redemption_Mission(v.Rows[0]["Default_Member_ID"].ToString(), "All Account");
                        }
                    }
                    else if (v.Rows[0]["Member_Category"].ToString() == "EO")
                    {
                        if (v.Rows[0]["Profit_Center"].ToString() == "1")
                        {
                            con.Close();
                            Need_Update_Profit_Center(v.Rows[0]["Default_Member_ID"].ToString());
                            Upgrade_To_EO(v.Rows[0]["Default_Member_ID"].ToString(), "All Account", v.Rows[0]["Upgrade_Level"].ToString());
                            Insert_Redemption_Mission(v.Rows[0]["Default_Member_ID"].ToString(), "All Account");
                            Update_Redemption_Mission(v.Rows[0]["Default_Member_ID"].ToString(), "2 EO");
                        }
                        else if (v.Rows[0]["Profit_Center"].ToString() == "3")
                        {
                            con.Close();
                            Upgrade_To_EO(v.Rows[0]["Default_Member_ID"].ToString(), "All Account", v.Rows[0]["Upgrade_Level"].ToString());
                            Insert_Redemption_Mission(v.Rows[0]["Default_Member_ID"].ToString(), "All Account");
                        }
                    }
                }
            }

            idr.Close();
            con.Close();
        }
    }

    protected void Need_Update_Profit_Center(string memberid)
    {
        using (SqlCommand cmd = new SqlCommand("Update_Profit_Center", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("Member_Code", memberid);
            cmd.Parameters.AddWithValue("Profit_Center", "3");
            cmd.Parameters.AddWithValue("Action", "Update Profit Center");
            cmd.Parameters.AddWithValue("User", memberid);

            // Set Output Paramater
            SqlParameter MemberParam = new SqlParameter("@MemberNotExist", SqlDbType.Bit);
            MemberParam.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(MemberParam);

            // Set Output Paramater
            SqlParameter StatusParam = new SqlParameter("@Status", SqlDbType.VarChar, 200);
            StatusParam.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(StatusParam);

            con.Open();
            cmd.ExecuteNonQuery();
            bool MemberNotExist = Convert.ToBoolean(cmd.Parameters["@MemberNotExist"].Value);
            string StatusExists = cmd.Parameters["@Status"].Value.ToString();
            con.Close();
        }
    }

    protected void Upgrade_To_EO(string memberid, string account_upgrade_to_eo, string package)
    {
        using (SqlCommand cmd = new SqlCommand("Update_To_EO", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("Member_Code", memberid);
            cmd.Parameters.AddWithValue("Upgrade_To_EO", account_upgrade_to_eo);
            cmd.Parameters.AddWithValue("Package", package);
            cmd.Parameters.AddWithValue("Action", "Update To EO");
            cmd.Parameters.AddWithValue("User", memberid);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }

    protected void Insert_Redemption_Mission(string memberid, string account_upgrade_to_eo)
    {
        if (account_upgrade_to_eo == "Main Account")
        {
            using (SqlCommand cmd = new SqlCommand("Insert_Member_Redemption_Point_Mission", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Member_ID", memberid);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        else if (account_upgrade_to_eo == "All Account")
        {
            using (SqlCommand cmd = new SqlCommand("Insert_Member_Redemption_Point_Mission", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Member_ID", memberid);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            using (SqlCommand cmd = new SqlCommand("Insert_Member_Redemption_Point_Mission", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Member_ID", memberid + 'A');
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            using (SqlCommand cmd = new SqlCommand("Insert_Member_Redemption_Point_Mission", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Member_ID", memberid + 'B');
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }

    protected void Update_Redemption_Mission(string memberid, string account_upgrade_to_eo)
    {
        if (account_upgrade_to_eo == "2 BO")
        {
            using (SqlCommand cmd = new SqlCommand("Update_Member_Redemption_Point_Mission", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Member_ID", memberid);
                cmd.Parameters.AddWithValue("@Mission", account_upgrade_to_eo);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        else if (account_upgrade_to_eo == "2 EO")
        {
            using (SqlCommand cmd = new SqlCommand("Update_Member_Redemption_Point_Mission", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Member_ID", memberid);
                cmd.Parameters.AddWithValue("@Mission", account_upgrade_to_eo);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
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

        #region Select Buyer Referal one and two

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

        using (SqlCommand cmd = new SqlCommand("Insert_Upgrade_Level_Transaction_BV", con))
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