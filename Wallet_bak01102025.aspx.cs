using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Wallet : System.Web.UI.Page
{
    protected static String ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
    public enum MessageType { Success, Error, Info, Warning };

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            bool cookieExists = HttpContext.Current.Request.Cookies["userid"] != null;
            if (cookieExists == true)
            {
                if (Request.Cookies["userid"].Value != null && Request.Cookies["userid"].Value != "")
                {
                    Load_Member_Wallet_Details(Request.Cookies["userid"].Value);
                    Load_Member_Bonus_Details(Request.Cookies["userid"].Value);
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

    protected void Load_Member_Wallet_Details(string memberid)
    {
        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("Load_Member_Wallet_Details", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Memberid", memberid);
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    lbl_wallet_amount.Text = "RM " + dt.Rows[0]["Wallet_Balance"].ToString();
                    rbtn_withdrawal_setting.SelectedValue = dt.Rows[0]["Wallet_Withdrawal_Setting"].ToString();

                    if(dt.Rows[0]["Wallet_Withdrawal_Setting"].ToString() == "Amount_Per_Transaction")
                    {
                        txt_withdrawal_amount.Text = dt.Rows[0]["Wallet_Withdrawal_Amount"].ToString();
                    }
                }

                con.Close();
            }
        }
    }

    protected void Load_Member_Bonus_Details(string memberid)
    {
        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("Load_Member_Bonus_Details", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Memberid", memberid);
                cmd.Parameters.AddWithValue("@Start_Date", DateTime.UtcNow.AddHours(8).AddDays(-14).ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@End_Date", DateTime.UtcNow.AddHours(8).AddDays(-1).ToString("yyyy-MM-dd"));
                con.Open();

                SqlDataReader idr = cmd.ExecuteReader();

                if (idr.HasRows == true)
                {
                    DataTable v = new DataTable();

                    v.Load(idr);

                    rpt_bonus.DataSource = v;
                    rpt_bonus.DataBind();
                }

                idr.Close();
                con.Close();

            }
        }
    }

    protected void rpt_bonus_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        HtmlGenericControl lbl_day = (HtmlGenericControl)e.Item.FindControl("lbl_day");

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView drv = (DataRowView)(e.Item.DataItem);
            lbl_day.InnerText = drv.Row["ReportDate"].ToString() + " - RM " + Convert.ToDecimal(drv.Row["Daily Total Bonus"].ToString()).ToString("###,###,##0.00");

            decimal new_bonus = 0;
            decimal total_bonus = 0;
            new_bonus = Convert.ToDecimal(drv.Row["Daily Total Bonus"].ToString());
            total_bonus = Convert.ToDecimal(lbl_total_14days_amount.Text.Replace("RM ", ""));
            lbl_total_14days_amount.Text = "RM " + Convert.ToDecimal(new_bonus + total_bonus).ToString("###,###,##0.00");
        }
    }

    protected void btn_Update_Click(object sender, EventArgs e)
    {
        bool cookieExists = HttpContext.Current.Request.Cookies["userid"] != null;
        if (cookieExists == true)
        {
            if (Request.Cookies["userid"].Value != null && Request.Cookies["userid"].Value != "")
            {
                if(rbtn_withdrawal_setting.SelectedValue == "")
                {
                    ShowMessage_warning("Please select your withdrawal setting", MessageType.Warning);
                    return;
                }
                else if (rbtn_withdrawal_setting.SelectedValue == "Amount_Per_Transaction" && string.IsNullOrEmpty(txt_withdrawal_amount.Text))
                {
                    ShowMessage_warning("Please key in withdrawal amount", MessageType.Warning);
                    return;
                }
                else if (rbtn_withdrawal_setting.SelectedValue == "Amount_Per_Transaction" && Convert.ToDecimal(txt_withdrawal_amount.Text) < 50)
                {
                    ShowMessage_warning("Minimum withdrawal amount is RM 50 ", MessageType.Warning);
                    return;
                }
                else
                {
                    Update_Member_Withdrawal_Setting(Request.Cookies["userid"].Value);
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

    protected void Update_Member_Withdrawal_Setting(string memberid)
    {
        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("Update_Member_Wallet_Details", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Member_ID", Request.Cookies["userid"].Value);
                cmd.Parameters.AddWithValue("Withdrawal_setting", rbtn_withdrawal_setting.SelectedValue);

                if (rbtn_withdrawal_setting.SelectedValue == "Amount_Per_Transaction")
                {
                    cmd.Parameters.AddWithValue("Withdrawal_amount", txt_withdrawal_amount.Text);
                }
                else
                {
                    cmd.Parameters.AddWithValue("Withdrawal_amount", "0");
                }

                // Set Output Paramater
                SqlParameter StatusParam = new SqlParameter("@Status", SqlDbType.VarChar, 200);
                StatusParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(StatusParam);

                con.Open();
                cmd.ExecuteNonQuery();

                string StatusExists = cmd.Parameters["@Status"].Value.ToString();

                con.Close();

                if (StatusExists == "success")
                {
                    ShowMessage("Update Withdrawal Setting Successful.", MessageType.Success);
                }
                else if (StatusExists == "failed")
                {
                    ShowMessage_warning("Failed to update withdrawal setting, please try again later.", MessageType.Warning);
                }
                con.Close();
            }
        }
    }

    protected void btn_view_transaction_Click(object sender, EventArgs e)
    {
        Response.Redirect("Transaction_History.aspx");
    }

    #region Message
    protected void ShowMessage(string Message, MessageType type)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "sweetalert_success('" + Message + "','" + type + "');", true);
    }

    protected void ShowMessage_warning(string Message, MessageType type)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "sweetalert_warning('" + Message + "','" + type + "');", true);
    }
    #endregion

}