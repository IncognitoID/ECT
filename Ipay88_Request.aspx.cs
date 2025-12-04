using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Ipay88_Request : System.Web.UI.Page
{
    private static CultureInfo enCulture = new CultureInfo("en-US");
    public static string MerchantCode, PaymentId, RefNo, Amount, Currency, ProdDesc, UserName, UserEmail, UserContact, Remark, Lang, SignatureType, Signature, ResponseURL, BackendURL;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["RefNo"]))
        {
            MerchantCode = "M43879";
            PaymentId = "";
            RefNo = Request.QueryString["RefNo"];
            Amount = Request.QueryString["Amount"];
            Currency = "MYR";
            ProdDesc = "Ecentra Sdn Bhd";
            UserName = Request.QueryString["Username"];
            UserEmail = Request.QueryString["UserEmail"];
            UserContact = Request.QueryString["UserContact"];
            Remark = Request.QueryString["Remark"];
            Lang = "UTF-8";
            SignatureType = "HMACSHA512";
            ResponseURL = "https://ecentra.com.my/Ipay88_Result.aspx";
            BackendURL = "https://ecentra.com.my/Ipay88_Success.aspx";

            ///////////////// call ipay88 page ////////////////

            var summissionContent = GetSummmisionContent();
            Response.Clear();
            Response.Write(summissionContent);

            ///////////////// call ipay88 page ////////////////
        }
    }

    protected string GetSummmisionContent()
    {
        var content = @" <!DOCTYPE html><html><head><title></title></head><body>
                            <form action=""https://payment.ipay88.com.my/ePayment/entry.asp"" method=""post"" name=""f"">
                                <input type=""hidden"" name=""MerchantCode"" value=""" + MerchantCode + @"""/>
                                <input type=""hidden"" name=""PaymentId"" value=""" + PaymentId + @"""/>
                                <input type=""hidden"" name=""RefNo"" value=""" + RefNo + @"""/>
                                <input type=""hidden"" name=""Amount"" value=""" + Amount + @"""/>
                                <input type=""hidden"" name=""Currency"" value=""" + Currency + @"""/>
                                <input type=""hidden"" name=""ProdDesc"" value=""" + ProdDesc + @"""/>
                                <input type=""hidden"" name=""UserName"" value=""" + UserName + @"""/>
                                <input type=""hidden"" name=""UserEmail"" value=""" + UserEmail + @"""/>
                                <input type=""hidden"" name=""UserContact"" value=""" + UserContact + @"""/>
                                <input type=""hidden"" name=""Remark"" value=""" + Remark + @"""/>
                                <input type=""hidden"" name=""Lang"" value=""" + Lang + @"""/>
                                <input type=""hidden"" name=""SignatureType"" value=""" + SignatureType + @"""/>
                                <input type=""hidden"" name=""Signature"" value=""" + GetEncryptedSign2() + @"""/>
                                <input type=""hidden"" name=""ResponseURL"" value=""" + ResponseURL + @"""/>
                                <input type=""hidden"" name=""BackendURL"" value=""" + BackendURL + @"""/>
                            </form></body></html><script>f.submit()</script>";

        return content;
    }

    private string GetEncryptedSign2()
    {
        string SECURITY_CODE = "Atfp0rjUAK";
        string final_result = "";
        final_result = SECURITY_CODE + MerchantCode + RefNo + decimal.Parse(Amount).ToString().Replace(".", "").Replace(",", "") + Currency;
        var result = hmacSHA512(final_result, SECURITY_CODE);
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

    //private string GetEncryptedSign2()
    //{
    //    string SECURITY_CODE = "Atfp0rjUAK";

    //    var result = sha256(SECURITY_CODE,
    //        MerchantCode,
    //        RefNo,
    //        decimal.Parse(Amount).ToString().Replace(".", "").Replace(",", ""),
    //        Currency);
    //    return result;
    //}

    //static string sha256(string aa, string bb, string cc, string dd, string ee)
    //{
    //    string randomString = aa + bb + cc + dd + ee;

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