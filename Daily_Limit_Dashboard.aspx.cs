using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Services;
using System.Web.UI;

public partial class Daily_Limit_Dashboard : System.Web.UI.Page
{
    protected static String ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
    public enum MessageType { Success, Error, Info, Warning };

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            bool cookieExists = HttpContext.Current.Request.Cookies["userid"] != null;
            if (cookieExists == true)
            {
                if (Request.Cookies["userid"].Value != null && Request.Cookies["userid"].Value != "")
                {
                    salesStartDate.Text = DateTime.UtcNow.AddHours(8).AddDays(-6).ToString("yyyy-MM-dd");
                    salesEndDate.Text = DateTime.UtcNow.AddHours(8).ToString("yyyy-MM-dd");

                    Load_Daily_Limit_Details(Request.Cookies["userid"].Value);
                    LoadRequestHistory(Request.Cookies["userid"].Value);
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

    protected void Load_Daily_Limit_Details(string memberid)
    { 
        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("Load_Stockist_Daily_Limit_Details", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Memberid", memberid);
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    lblRemainingDailyLimit.Text = "RM " + Convert.ToDecimal(dt.Rows[0]["Remaining_Daily_Limit"].ToString()).ToString("###,###,##0.00");
                    lblDailyLimit.Text = "RM " + Convert.ToDecimal(dt.Rows[0]["Daily_Limit"].ToString()).ToString("###,###,##0.00");
                    lblcurrentlimit.Text = "RM " + Convert.ToDecimal(dt.Rows[0]["Daily_Limit"].ToString()).ToString("###,###,##0.00");
                }

                con.Close();
            }
        }
    }

    protected void LoadRequestHistory(string memberId)
    {
        if (!string.IsNullOrEmpty(memberId))
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Load_Mobile_Stockist_Daily_Limit_Request_History", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure; // Specify the command as a stored procedure
                    cmd.Parameters.AddWithValue("@MemberID", memberId);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        rptrequesthistory.DataSource = dt;
                        rptrequesthistory.DataBind();
                        norequesthistory.Visible = false;
                    }
                    else
                    {
                        norequesthistory.Visible = true;
                    }

                    con.Close();
                }
            }
        }
    }

    #region BAR CHART

    [WebMethod]
    public static List<Dictionary<string, object>> GetSalesData(string memberid, string startDate, string endDate)
    {
        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("Load_Mobile_Stockist_Chart", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Add parameters
                cmd.Parameters.AddWithValue("@MemberID", memberid);

                // Handle null or empty dates
                if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
                {
                    cmd.Parameters.AddWithValue("@StartDate", DateTime.Parse(startDate));
                    cmd.Parameters.AddWithValue("@EndDate", DateTime.Parse(endDate));
                }
                else
                {
                    // Pass null for startDate and endDate to trigger the default 7-day query in the stored procedure
                    cmd.Parameters.AddWithValue("@StartDate", DBNull.Value);
                    cmd.Parameters.AddWithValue("@EndDate", DBNull.Value);
                }

                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    while (rdr.Read())
                    {
                        Dictionary<string, object> row = new Dictionary<string, object>();
                        row.Add("OrderDate", Convert.ToDateTime(rdr["OrderDate"]).ToString("yyyy-MM-dd"));
                        row.Add("TotalSalesAmount", rdr["TotalSalesAmount"]);
                        rows.Add(row);
                    }
                    return rows;
                }
            }
        }
    }

    #endregion

    #region LIMIT REQUEST

    protected void btn_confirm_request_Click(object sender, EventArgs e)
    {
        bool cookieExists = HttpContext.Current.Request.Cookies["userid"] != null;
        if (cookieExists == true)
        {
            if (txt_request_amount.Text != "")
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    SqlParameter requestCodeParam = new SqlParameter("@RequestCode", SqlDbType.NVarChar, 50);
                    requestCodeParam.Direction = ParameterDirection.Output;
                    
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("Insert_New_Daily_Limit_Request", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Memberid", Request.Cookies["userid"].Value);
                        cmd.Parameters.AddWithValue("@RequestAmount", Convert.ToDecimal(txt_request_amount.Text));
                        cmd.Parameters.Add(requestCodeParam);

                        cmd.ExecuteNonQuery();

                        // Retrieve the generated ContentCode
                        string generatedContentCode = requestCodeParam.Value.ToString();

                    }

                    con.Close();

                }
                LoadRequestHistory(Request.Cookies["userid"].Value);
                ShowMessage("Request sent successfully.", MessageType.Success);
            }
        }
        else
        {
            ShowMessage_warning("Please login your account", MessageType.Warning);
        }
    }

    #endregion

    #region Message

    protected void ShowMessage(string Message, MessageType type)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "sweetalert_success('" + Message + "','" + type + "');", true);
    }

    protected void ShowMessage_Place_Order(string Message, MessageType type)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "sweetalert_success_place_order('" + Message + "','" + type + "');", true);
    }

    protected void ShowMessage_warning(string Message, MessageType type)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "sweetalert_warning('" + Message + "','" + type + "');", true);
    }

    #endregion

    protected void btn_view_history_Click(object sender, EventArgs e)
    {
        Response.Redirect("Daily_Limit_History.aspx");
    }

    protected string FormatRequestAmount(object limitUsedObj, object limitEarnObj)
    {
        decimal limitUsed = Convert.ToDecimal(limitUsedObj);
        decimal limitEarn = Convert.ToDecimal(limitEarnObj);

        if (limitUsed != 0)
        {
            return " - RM " + limitUsed.ToString("N2");
        }
        else
        {
            return " + RM " + limitEarn.ToString("N2");
        }
    }

}
