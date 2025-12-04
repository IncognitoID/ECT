using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Text;
using System.Security.Cryptography;

public partial class Ipay88_Result_Redemption_Order : System.Web.UI.Page
{
    private string MERCHANT_CODE = "M43879";
    private string SECURITY_CODE = "Atfp0rjUAK";
    private static CultureInfo enCulture = new CultureInfo("en-US");

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string MerchantKey = "Atfp0rjUAK";
            string MerchantCode = Page.Request["MerchantCode"];
            string PaymentId = Page.Request["PaymentId"];
            string RefNo = Page.Request["RefNo"];
            string Amount = Page.Request["Amount"];
            string Currency = Page.Request["Currency"];
            string Status = Page.Request["Status"];
            string TransId = Page.Request["TransId"];
            string ErrDesc = Page.Request["ErrDesc"];
            string Signature = Page.Request["Signature"];

            if (Status == "1")
            {
                if (Signature == GetEncryptedSign2(MerchantKey, MerchantCode, PaymentId, RefNo, Amount, Currency, Status))
                {
                    div_success.Visible = true;
                    div_failed.Visible = false;
                }
                else
                {
                    div_success.Visible = false;
                    div_failed.Visible = true;
                }
            }
            else
            {
                if(string.IsNullOrEmpty(RefNo))
                {
                    RefNo = "";
                }

                using (SqlCommand cmd = new SqlCommand("Update_Redemption_Order_Status_Failed", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Order_No", RefNo);
                    cmd.Parameters.AddWithValue("@Payment_Status", "Failed");
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                div_success.Visible = false;
                div_failed.Visible = true;
            }
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

}