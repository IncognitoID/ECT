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

public partial class Ipay88_Success_Redemption_Order : System.Web.UI.Page
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
                using (SqlCommand cmd = new SqlCommand("Update_Redemption_Order_Status", con))
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