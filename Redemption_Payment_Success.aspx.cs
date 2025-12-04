using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Text;
using System.Security.Cryptography;
using System.Web;

public partial class Redemption_Payment_Success : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            bool cookieExists = HttpContext.Current.Request.Cookies["userid"] != null;
            bool cookieExists2 = HttpContext.Current.Request.Cookies["redemption_order_no"] != null;

            if (cookieExists == true)
            {
                if (Request.Cookies["userid"].Value != null && Request.Cookies["userid"].Value != "")
                {
                    if (cookieExists2 == true)
                    {
                        if (Request.Cookies["redemption_order_no"].Value != null && Request.Cookies["redemption_order_no"].Value != "")
                        {
                            div_success.Visible = true;

                            using (SqlCommand cmd = new SqlCommand("Update_Redemption_Order_Status_RP_Order", con))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@Order_No", Request.Cookies["redemption_order_no"].Value);
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                            System.Web.HttpCookie myCookie = new System.Web.HttpCookie("redemption_order_no");
                            myCookie.Expires = DateTime.Now.AddMonths(-1);
                            myCookie.Value = string.Empty;
                            Response.Cookies.Add(myCookie);
                        }
                    }
                }
            }
        }
    }

}