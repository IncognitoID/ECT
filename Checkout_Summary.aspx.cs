using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Checkout_Summary : System.Web.UI.Page
{
    public enum MessageType { Success, Error, Info, Warning };
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    protected static String ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
    bool Payment_Method_Success = true;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            bool cookieExists = HttpContext.Current.Request.Cookies["Payment_order_no"] != null;

            if (cookieExists == true)
            {
                if (Request.Cookies["Payment_order_no"].Value != null && Request.Cookies["Payment_order_no"].Value != "")
                {

                    Load_Ecentra_Bank_Details();
                    //Load_Order_Details(Request.Cookies["Payment_order_no"].Value);
                    string type = Request.QueryString["type"];

                    if (type == "Upgrade_EO")
                    {
                        Load_UTEOC_Order(Request.Cookies["Payment_order_no"].Value);
                    }
                    else
                    {
                        Load_Order_Details(Request.Cookies["Payment_order_no"].Value);
                    }

                    string value = "Select your payment method";
                    string value1 = "Online Payment";
                    string value2 = "Wallet";
                    string value4 = "Product Wallet";
                    string value3 = "Manual Payment";
                    string fullPaymentText = "Full Payment";
                    string partialPaymentText = "Partial Payment";
                    string placeholderText = "Enter password";

                    bool language_cookieExists = HttpContext.Current.Request.Cookies["language"] != null;
                    if (language_cookieExists == true)
                    {
                        if (Request.Cookies["language"].Value != null && Request.Cookies["language"].Value != "")
                        {
                            if (Request.Cookies["language"].Value == "Chinese")
                            {
                                value = "选择您的付款方式";
                                value1 = "网上支付";
                                value2 = "钱包";
                                value4 = "产品钱包";
                                value3 = "手动付款";
                                fullPaymentText = "全额付款";
                                partialPaymentText = "部分付款";
                                placeholderText = "请输入密码"; // Chinese translation
                            }
                        }
                    }

                    ListItem defaultItem = new ListItem(value, "");
                    ListItem defaultItem1 = new ListItem(value1, "online");
                    ListItem defaultItem2 = new ListItem(value2, "ewallet");
                    ListItem defaultItem4 = new ListItem(value4, "pwallet");
                    ListItem defaultItem3 = new ListItem(value3, "manual");
                    defaultItem.Attributes["disabled"] = "disabled";
                    defaultItem.Selected = true; // Set the default item as selected
                    ddlPaymentMethod.Items.Insert(0, defaultItem);
                    ddlPaymentMethod.Items.Insert(1, defaultItem1);
                    ddlPaymentMethod.Items.Insert(2, defaultItem2);
                    ddlPaymentMethod.Items.Insert(3, defaultItem4);
                    ddlPaymentMethod.Items.Insert(3, defaultItem3);

                    rbrn_payment_option.Items.Clear(); // Optional: reset items before adding
                    rbrn_payment_option.Items.Add(new ListItem(fullPaymentText, "fullewallet", true));
                    rbrn_payment_option.Items.Add(new ListItem(partialPaymentText, "partialewallet"));
                    rbrn_payment_option.SelectedValue = "fullewallet"; // Default selection

                    txt_ewallet_password.Attributes["placeholder"] = placeholderText;
                }
                else
                {
                    Response.Redirect("Landing.aspx");
                }
            }
            else
            {
                Response.Redirect("Landing.aspx");
            }
        }
    }

    protected void Load_Ecentra_Bank_Details()
    {
        using (SqlCommand cmd = new SqlCommand("Front_End_Load_Ecentra_Bank_Details", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();

            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows)
            {
                DataTable g = new DataTable();
                g.Load(idr);

                lbl_bank.InnerText = g.Rows[0]["Bank_Name"].ToString();
                lbl_account_name.InnerText = g.Rows[0]["Account_Name"].ToString();
                lbl_account_number.InnerText = g.Rows[0]["Bank_Account"].ToString();
            }
            con.Close();
        }
    }

    protected void Load_Order_Details(string order_no)
    {
        using (SqlCommand cmd = new SqlCommand("Load_Order_History_Details", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@OrderNo", order_no);
            con.Open();

            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows)
            {
                DataTable g = new DataTable();
                g.Load(idr);

                itemSubtotal.InnerText = "RM " + g.Rows[0]["Item_Total"].ToString();
                shippingtotal.InnerText = "RM " + g.Rows[0]["Shipping_Total"].ToString();
                shippingdiscount.InnerText = "- RM " + g.Rows[0]["Shipping_Discount"].ToString();
                totalPayment.InnerText = "RM " + g.Rows[0]["Total_Amount"].ToString();
            }
            con.Close();
        }
    }


    private void Load_UTEOC_Order(string default_order_no)
    {
        using (SqlCommand cmd = new SqlCommand("Load_Upgrade_Level_SO_Header", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@OrderNo", default_order_no);
            con.Open();
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows)
            {
                DataTable header = new DataTable();
                header.Load(idr);

                itemSubtotal.InnerText = "RM " + header.Rows[0]["Item_Total"].ToString();
                shippingtotal.InnerText = "RM " + header.Rows[0]["Shipping_Total"].ToString();
                shippingdiscount.InnerText = "- RM " + header.Rows[0]["Shipping_Discount"].ToString();
                totalPayment.InnerText = "RM " + header.Rows[0]["Grand_Total"].ToString();
            }

            con.Close();
        }

        /*using (SqlCommand cmd = new SqlCommand("Load_Upgrade_Level_SO_Item", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Order_No", orderNo);
            con.Open();
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows)
            {
                // RepeaterItems.DataSource = idr;
                // RepeaterItems.DataBind();
            }

            con.Close();
        }*/
    }


    protected void btn_online_payment_Click(object sender, EventArgs e)
    {
        walletamount.InnerText = "- RM 0.00";
        hdn_wallet_deduct_amount.Value = "0";
        rbeWallet.Value = "online";

        string type = Request.QueryString["type"];

        if (type == "Upgrade_EO")
        {
            Load_UTEOC_Order(Request.Cookies["Payment_order_no"].Value);
        }
        else
        {
            Load_Order_Details(Request.Cookies["Payment_order_no"].Value);
        }

        if (ddlPaymentMethod.SelectedValue == "manual")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "Show_Payment_Slip();", true);
        }
    }

    #region ewallet

    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public string MemberName { get; set; }
        public string MemberID { get; set; }
        public string WalletBalance { get; set; }
        public string PWalletBalance { get; set; }
        public string TotalAmount { get; set; }

    }

    [System.Web.Services.WebMethod]
    public static ValidationResult ValidatePassword(string password)
    {
        if (HttpContext.Current.Request.Cookies["userid"].Value != "")
        {
            ValidationResult result = new ValidationResult();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Load_Member_Details", con))
                {
                    string memberid = "";
                    memberid = HttpContext.Current.Request.Cookies["userid"].Value;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Memberid", memberid);
                    con.Open();

                    SqlDataReader idr = cmd.ExecuteReader();
                    if (idr.HasRows)
                    {
                        while (idr.Read())
                        {
                            string member_password = "";
                            member_password = idr["E_Wallet_Password"].ToString();
                            byte[] encryptedBytes = Convert.FromBase64String(member_password);
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

                                        // Check the password
                                        if (password == decryptedText)
                                        {
                                            result.IsValid = true; // Password is correct
                                            result.MemberName = idr["First_Name"].ToString();
                                            result.MemberID = HttpContext.Current.Request.Cookies["userid"].Value;
                                            result.WalletBalance = idr["Wallet_Balance"].ToString();
                                            result.PWalletBalance = idr["Product_Wallet_Balance"].ToString();
                                            result.TotalAmount = idr["Wallet_Balance"].ToString();
                                            return result; // Return the result
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
            }
        }

        return new ValidationResult { IsValid = false }; // Password is incorrect

    }

    protected void btn_cancel_ewallet_Click(object sender, EventArgs e)
    {
        ddlPaymentMethod.SelectedValue = "online";
        hdn_wallet_deduct_amount.Value = "0";
        rbeWallet.Value = "online";
    }

    protected void btn_confirm_password_Click(object sender, EventArgs e)
    {
        using (SqlCommand cmd = new SqlCommand("Load_Member_Details", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Memberid", Request.Cookies["userid"].Value);
            con.Open();

            SqlDataReader idr = cmd.ExecuteReader();
            if (idr.HasRows)
            {
                if (idr.Read()) // Move to the first row
                {
                    // Now you can safely access the column data
                    if (txt_ewallet_password.Text == idr["E_Wallet_Password"].ToString())
                    {
                        ShowMessage_warning("Correct password.", MessageType.Warning);
                    }
                    else
                    {
                        ShowMessage_warning("Incorrect password.", MessageType.Warning);

                        // First hide the modal and then show it again
                        ScriptManager.RegisterStartupScript(this, GetType(), "keepModalOpen", "$('#ewalletModal').modal('hide'); setTimeout(function() { $('#ewalletModal').modal('show'); }, 500);", true);
                    }
                }
            }
            else
            {
                ShowMessage_warning("Please login your account", MessageType.Warning);

                // Hide the modal first and reopen it to avoid double backdrops
                ScriptManager.RegisterStartupScript(this, GetType(), "keepModalOpen", "$('#ewalletModal').modal('hide'); setTimeout(function() { $('#ewalletModal').modal('show'); }, 500);", true);
            }
            con.Close();
        }
    }

    protected void btn_confirm_ewallet_Click(object sender, EventArgs e)
    {
        if (rbrn_payment_option.SelectedValue == "fullewallet")
        {
            if (string.IsNullOrEmpty(hdn_wallet_deduct_amount.Value))
            {
                ShowMessage_warning("Please select payment method.", MessageType.Warning);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showEwalletModalPayment", "showEwalletModalPayment();", true);
                return;
            }
            else if (Convert.ToDecimal(hdn_wallet_deduct_amount.Value) >= Convert.ToDecimal(totalPayment.InnerText.Replace("RM ", "")))
            {
                walletamount.InnerText = "- RM " + Convert.ToDecimal(hdn_wallet_deduct_amount.Value).ToString("N2");
                totalPayment.InnerText = "RM " + (Convert.ToDecimal(totalPayment.InnerText.Replace("RM ", "")) - Convert.ToDecimal(hdn_wallet_deduct_amount.Value)).ToString("N2");
            }
            else
            {
                ShowMessage_warning("Insufficient wallet balance to place order.", MessageType.Warning);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showEwalletModalPayment", "showEwalletModalPayment();", true);
                return;
            }
        }
        else if (rbrn_payment_option.SelectedValue == "partialewallet")
        {
            if (string.IsNullOrEmpty(hdn_wallet_deduct_amount.Value))
            {
                ShowMessage_warning("Please key in partial payment amount.", MessageType.Warning);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showEwalletModalPayment", "showEwalletModalPayment();", true);
                return;
            }
            else
            {
                if (Convert.ToDecimal(hdn_wallet_deduct_amount.Value) == 0)
                {
                    ShowMessage_warning("Please key in partial payment amount.", MessageType.Warning);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "showEwalletModalPayment", "showEwalletModalPayment();", true);
                    return;
                }
                else
                {
                    walletamount.InnerText = "- RM " + Convert.ToDecimal(hdn_wallet_deduct_amount.Value).ToString("N2");
                    totalPayment.InnerText = "RM " + (Convert.ToDecimal(totalPayment.InnerText.Replace("RM ", "")) - Convert.ToDecimal(hdn_wallet_deduct_amount.Value)).ToString("N2");

                    string script = "document.getElementById('partialewallet').checked = true;";
                    ClientScript.RegisterStartupScript(this.GetType(), "SelectPartialPayment", script, true);
                }
            }

        }
        else
        {
            ShowMessage_warning("Something went wrong, please try again later.", MessageType.Warning);
        }
    }


    #endregion

    protected void btn_check_fileupload_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFiles)
        {
            //Insert_Order(Request.Cookies["userid"].Value);
        }
        else
        {
            ShowMessage_warning("Please upload payment slip.", MessageType.Warning);
        }
    }


    protected void btn_submit_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(ddlPaymentMethod.SelectedValue))
        {
            ShowMessage_warning("Please select payment method.", MessageType.Warning);
            return;
        }
        else
        {
            bool cookieExists = HttpContext.Current.Request.Cookies["Payment_order_no"] != null;

            if (cookieExists == true)
            {
                if (Request.Cookies["Payment_order_no"].Value != null && Request.Cookies["Payment_order_no"].Value != "")
                {
                    decimal order_total_amount = 0;
                    string type = Request.QueryString["type"];

                    if (type == "Upgrade_EO")
                    {
                        using (SqlCommand cmd = new SqlCommand("Load_Upgrade_Level_SO_Header", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@OrderNo", Request.Cookies["Payment_order_no"].Value);

                            con.Open();
                            SqlDataReader idr = cmd.ExecuteReader();

                            if (idr.HasRows)
                            {
                                DataTable header = new DataTable();
                                header.Load(idr);
                                order_total_amount = Convert.ToDecimal(header.Rows[0]["Grand_Total"]);
                            }

                            con.Close();
                        }
                    }
                    else
                    {
                        using (SqlCommand cmd = new SqlCommand("Load_Checkout_Summary_Order_Details", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@OrderNo", Request.Cookies["Payment_order_no"].Value);

                            con.Open();
                            SqlDataReader idr = cmd.ExecuteReader();

                            if (idr.HasRows)
                            {
                                DataTable order_details = new DataTable();
                                order_details.Load(idr);

                                order_total_amount = Convert.ToDecimal(order_details.Rows[0]["Total_Amount"]);
                            }

                            con.Close();
                        }
                    }

                    /*using (SqlCommand cmd = new SqlCommand("Load_Checkout_Summary_Order_Details", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@OrderNo", Request.Cookies["Payment_order_no"].Value);
                        con.Open();
                        SqlDataReader idr = cmd.ExecuteReader();

                        if (idr.HasRows == true)
                        {
                            DataTable order_details = new DataTable();
                            order_details.Load(idr);

                            order_total_amount = Convert.ToDecimal(order_details.Rows[0]["Total_Amount"].ToString());
                        }

                        idr.Close();
                        con.Close();
                    }*/

                    if (ddlPaymentMethod.SelectedValue == "online")
                    {
                        string payment_refno = "";
                        string payment_amount = "";
                        string payment_username = "";
                        string payment_userphone = "";
                        string payment_useremail = "";
                        string payment_remark = "";

                        using (SqlCommand cmd = new SqlCommand("Checkout_Load_Member_Details", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Member_ID", Request.Cookies["userid"].Value);
                            con.Open();
                            SqlDataReader idr = cmd.ExecuteReader();

                            if (idr.HasRows == true)
                            {
                                DataTable member_details = new DataTable();
                                member_details.Load(idr);

                                payment_username = member_details.Rows[0]["Member_Name"].ToString();
                                payment_userphone = member_details.Rows[0]["Contact_No"].ToString();
                                payment_useremail = member_details.Rows[0]["Email"].ToString();
                            }

                            idr.Close();
                            con.Close();
                        }
                        payment_refno = Request.Cookies["Payment_order_no"].Value;
                        payment_amount = order_total_amount.ToString();
                        payment_remark = "";

                        if (order_total_amount > 0)
                        {
                            Response.Cookies["Payment_order_no"].Expires = DateTime.Now.AddMonths(-1);
                            Response.Cookies["Payment_order_no"].Value = string.Empty;
                            Response.Cookies.Add(new HttpCookie("Payment_order_no", string.Empty));
                            Response.Redirect("Ipay88_Request.aspx?RefNo=" + payment_refno + "&Amount=" + payment_amount + "&Username=" + payment_username + "&UserEmail=" + payment_useremail + "&UserContact=" + payment_userphone + "&Remark=" + payment_remark);
                        }
                    }
                    else if (ddlPaymentMethod.SelectedValue == "pwallet")
                    {
                        Check_Member_Wallet(Request.Cookies["userid"].Value);

                        if (Payment_Method_Success)
                        {
                            string payment_method = "";
                            string payment_status = "";
                            string order_status = "";
                            string wallet_amount = "";
                            string order_no = "";
                            order_no = Request.Cookies["Payment_order_no"].Value;

                            payment_method = "Product Wallet Payment";
                            if (rbrn_payment_option.SelectedValue == "fullewallet")
                            {
                                payment_status = "Pending";
                                order_status = "To Pay";
                                wallet_amount = order_total_amount.ToString();
                                order_total_amount = 0;
                            }
                            else if (rbrn_payment_option.SelectedValue == "partialewallet")
                            {
                                payment_status = "Pending";
                                order_status = "To Pay";
                                wallet_amount = hdn_wallet_deduct_amount.Value;
                                order_total_amount = order_total_amount - Convert.ToDecimal(hdn_wallet_deduct_amount.Value);
                            }

                            using (SqlCommand cmd = new SqlCommand("Update_ProductWallet_Payment_Order_Details", con))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@Order_No", order_no);
                                cmd.Parameters.AddWithValue("@Order_Status", order_status);
                                cmd.Parameters.AddWithValue("@Payment_Status", payment_status);
                                cmd.Parameters.AddWithValue("@Payment_Method", payment_method);
                                cmd.Parameters.AddWithValue("@Wallet_Amount", wallet_amount);
                                cmd.Parameters.AddWithValue("@Payment_Amount", order_total_amount);
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }

                            if (order_total_amount > 0)
                            {
                                string payment_refno = "";
                                string payment_amount = "";
                                string payment_username = "";
                                string payment_userphone = "";
                                string payment_useremail = "";
                                string payment_remark = "";

                                using (SqlCommand cmd = new SqlCommand("Checkout_Load_Member_Details", con))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@Member_ID", Request.Cookies["userid"].Value);
                                    con.Open();
                                    SqlDataReader idr = cmd.ExecuteReader();

                                    if (idr.HasRows == true)
                                    {
                                        DataTable member_details = new DataTable();
                                        member_details.Load(idr);

                                        payment_username = member_details.Rows[0]["Member_Name"].ToString();
                                        payment_userphone = member_details.Rows[0]["Contact_No"].ToString();
                                        payment_useremail = member_details.Rows[0]["Email"].ToString();
                                    }

                                    idr.Close();
                                    con.Close();
                                }
                                payment_refno = Request.Cookies["Payment_order_no"].Value;
                                payment_amount = order_total_amount.ToString();
                                payment_remark = "";
                                Response.Cookies["Payment_order_no"].Expires = DateTime.Now.AddMonths(-1);
                                Response.Cookies["Payment_order_no"].Value = string.Empty;
                                Response.Cookies.Add(new HttpCookie("Payment_order_no", string.Empty));
                                Response.Redirect("Ipay88_Request.aspx?RefNo=" + payment_refno + "&Amount=" + payment_amount + "&Username=" + payment_username + "&UserEmail=" + payment_useremail + "&UserContact=" + payment_userphone + "&Remark=" + payment_remark);
                            }
                            else if (order_total_amount == 0 && Convert.ToDecimal(wallet_amount) > 0)
                            {
                                Response.Cookies["order_no"].Value = order_no;
                                Response.Cookies["Payment_order_no"].Expires = DateTime.Now.AddMonths(-1);
                                Response.Cookies["Payment_order_no"].Value = string.Empty;
                                Response.Cookies.Add(new HttpCookie("Payment_order_no", string.Empty));
                                Response.Redirect("Payment_Success.aspx");
                            }
                        }

                    }
                    else if (ddlPaymentMethod.SelectedValue == "ewallet")
                    {
                        Check_Member_Wallet(Request.Cookies["userid"].Value);

                        if (Payment_Method_Success)
                        {
                            string payment_method = "";
                            string payment_status = "";
                            string order_status = "";
                            string wallet_amount = "";
                            string order_no = "";
                            order_no = Request.Cookies["Payment_order_no"].Value;

                            payment_method = "Wallet Payment";
                            if (rbrn_payment_option.SelectedValue == "fullewallet")
                            {
                                payment_status = "Pending";
                                order_status = "To Pay";
                                wallet_amount = order_total_amount.ToString();
                                order_total_amount = 0;
                            }
                            else if (rbrn_payment_option.SelectedValue == "partialewallet")
                            {
                                payment_status = "Pending";
                                order_status = "To Pay";
                                wallet_amount = hdn_wallet_deduct_amount.Value;
                                order_total_amount = order_total_amount - Convert.ToDecimal(hdn_wallet_deduct_amount.Value);
                            }

                            using (SqlCommand cmd = new SqlCommand("Update_Payment_Order_Details", con))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@Order_No", order_no);
                                cmd.Parameters.AddWithValue("@Order_Status", order_status);
                                cmd.Parameters.AddWithValue("@Payment_Status", payment_status);
                                cmd.Parameters.AddWithValue("@Payment_Method", payment_method);
                                cmd.Parameters.AddWithValue("@Wallet_Amount", wallet_amount);
                                cmd.Parameters.AddWithValue("@Payment_Amount", order_total_amount);
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }

                            if (order_total_amount > 0)
                            {
                                string payment_refno = "";
                                string payment_amount = "";
                                string payment_username = "";
                                string payment_userphone = "";
                                string payment_useremail = "";
                                string payment_remark = "";

                                using (SqlCommand cmd = new SqlCommand("Checkout_Load_Member_Details", con))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@Member_ID", Request.Cookies["userid"].Value);
                                    con.Open();
                                    SqlDataReader idr = cmd.ExecuteReader();

                                    if (idr.HasRows == true)
                                    {
                                        DataTable member_details = new DataTable();
                                        member_details.Load(idr);

                                        payment_username = member_details.Rows[0]["Member_Name"].ToString();
                                        payment_userphone = member_details.Rows[0]["Contact_No"].ToString();
                                        payment_useremail = member_details.Rows[0]["Email"].ToString();
                                    }

                                    idr.Close();
                                    con.Close();
                                }
                                payment_refno = Request.Cookies["Payment_order_no"].Value;
                                payment_amount = order_total_amount.ToString();
                                payment_remark = "";
                                Response.Cookies["Payment_order_no"].Expires = DateTime.Now.AddMonths(-1);
                                Response.Cookies["Payment_order_no"].Value = string.Empty;
                                Response.Cookies.Add(new HttpCookie("Payment_order_no", string.Empty));
                                Response.Redirect("Ipay88_Request.aspx?RefNo=" + payment_refno + "&Amount=" + payment_amount + "&Username=" + payment_username + "&UserEmail=" + payment_useremail + "&UserContact=" + payment_userphone + "&Remark=" + payment_remark);
                            }
                            else if (order_total_amount == 0 && Convert.ToDecimal(wallet_amount) > 0)
                            {
                                Response.Cookies["order_no"].Value = order_no;
                                Response.Cookies["Payment_order_no"].Expires = DateTime.Now.AddMonths(-1);
                                Response.Cookies["Payment_order_no"].Value = string.Empty;
                                Response.Cookies.Add(new HttpCookie("Payment_order_no", string.Empty));
                                Response.Redirect("Payment_Success.aspx");
                            }
                        }

                    }
                    else if (ddlPaymentMethod.SelectedValue == "manual")
                    {
                        if (FileUpload1.HasFiles)
                        {
                            string order_no = "";
                            order_no = Request.Cookies["Payment_order_no"].Value;

                            #region Insert Payment Slip

                            if (ddlPaymentMethod.SelectedValue == "manual")
                            {
                                foreach (HttpPostedFile uploadedFile in FileUpload1.PostedFiles)
                                {
                                    string imagelink = "";
                                    string random_number = "";
                                    random_number = Guid.NewGuid().ToString("N").Substring(0, 8);
                                    string extension = Path.GetExtension(uploadedFile.FileName);
                                    string FileNameU = "payment_slip" + "_" + random_number + "_" + DateTime.UtcNow.AddHours(8).ToString("ss") + extension;
                                    string path = Server.MapPath("PaymentSlips/");
                                    string filePath = Path.Combine(path, FileNameU);

                                    uploadedFile.SaveAs(filePath);
                                    imagelink = "PaymentSlips/" + FileNameU;
                                    SaveImageLinkToDatabase(order_no, imagelink);
                                }
                            }

                            #endregion

                            using (SqlCommand cmd = new SqlCommand("Update_Payment_Order_Details", con))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@Order_No", order_no);
                                cmd.Parameters.AddWithValue("@Order_Status", "Waiting For Approval");
                                cmd.Parameters.AddWithValue("@Payment_Status", "Pending");
                                cmd.Parameters.AddWithValue("@Payment_Method", "Manual Payment");
                                cmd.Parameters.AddWithValue("@Wallet_Amount", "0");
                                cmd.Parameters.AddWithValue("@Payment_Amount", order_total_amount);
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }

                            Response.Cookies["Payment_order_no"].Expires = DateTime.Now.AddMonths(-1);
                            Response.Cookies["Payment_order_no"].Value = string.Empty;
                            Response.Cookies.Add(new HttpCookie("Payment_order_no", string.Empty));
                            ShowMessage("Place order successful, please wait for approval.", MessageType.Success);
                        }
                        else
                        {
                            ShowMessage_warning("Please upload payment slip.", MessageType.Warning);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "Show_Payment_Slip();", true);
                        }
                    }
                }
                else
                {
                    Response.Redirect("Landing.aspx");
                }
            }
            else
            {
                Response.Redirect("Landing.aspx");
            }
        }

    }

    protected void Check_Member_Wallet(string member_id)
    {
        con.Close();
        DataTable v = new DataTable();

      
            using (SqlCommand cmd = new SqlCommand("Load_Member_Details", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Memberid", member_id);
                con.Open();
                SqlDataReader idr = cmd.ExecuteReader();

                if (idr.HasRows == true)
                {
                    v.Load(idr);
                }
                else
                {
                    Payment_Method_Success = false;
                }

                idr.Close();
                con.Close();
            }

        foreach (DataRow row in v.Rows)
        {
            decimal member_wallet = 0;
            if (ddlPaymentMethod.SelectedValue == "pwallet")
            {
                member_wallet = Convert.ToDecimal(row["Product_Wallet_Balance"].ToString());
            }
            else if (ddlPaymentMethod.SelectedValue == "ewallet")
            {
                member_wallet = Convert.ToDecimal(row["Wallet_Balance"].ToString());
            }
            if (string.IsNullOrEmpty(hdn_wallet_deduct_amount.Value))
            {
                Payment_Method_Success = false;
                ShowMessage_warning("Please key in partial payment amount.", MessageType.Warning);
                return;
            }
            else if (Convert.ToDecimal(hdn_wallet_deduct_amount.Value) == 0)
            {
                Payment_Method_Success = false;
                ShowMessage_warning("Please key in partial payment amount.", MessageType.Warning);
                return;
            }
            else if (member_wallet >= Convert.ToDecimal(hdn_wallet_deduct_amount.Value))
            {
                Payment_Method_Success = true;
            }
            else
            {
                Payment_Method_Success = false;
                ShowMessage_warning("Insufficient wallet balance to place order.", MessageType.Warning);
                return;
            }
        }
    }

    private void SaveImageLinkToDatabase(string orderid, string imgLink)
    {
        using (SqlCommand cmd = new SqlCommand("Insert_SO_Header_Img", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Order_No", orderid);
            cmd.Parameters.AddWithValue("@ImgLink", imgLink);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }

    public static string RandomString(int length)
    {
        Random random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
        .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    #region Message
    protected void ShowMessage(string Message, MessageType type)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "sweetalert_success('" + Message + "','" + type + "');", true);
    }

    protected void ShowMessageRedirect(string Message, MessageType type)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "sweetalert_success_redirect('" + Message + "','" + type + "');", true);
    }

    protected void ShowMessage_warning(string Message, MessageType type)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "sweetalert_warning('" + Message + "','" + type + "');", true);
    }

    #endregion

}