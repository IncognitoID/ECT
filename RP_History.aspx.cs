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

public partial class RP_History : System.Web.UI.Page
{
    protected static String ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
    public enum MessageType { Success, Error, Info, Warning };

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bool cookieExists = HttpContext.Current.Request.Cookies["userid"] != null;
            if (cookieExists == true)
            {
                if (Request.Cookies["userid"].Value != null && Request.Cookies["userid"].Value != "")
                {
                    Load_Member_RP_History(Request.Cookies["userid"].Value);
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

    protected void Load_Member_RP_History(string memberid)
    {
        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("Load_Member_RP_History", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Memberid", memberid);
                con.Open();

                SqlDataReader idr = cmd.ExecuteReader();

                if (idr.HasRows == true)
                {
                    rpt_rp_history.Visible = true;
                    tr_no_record.Visible = false;
                    DataTable v = new DataTable();

                    v.Load(idr);
                    rpt_rp_history.DataSource = v;
                    rpt_rp_history.DataBind();
                }
                else
                {
                    rpt_rp_history.Visible = false;
                    tr_no_record.Visible = true;
                }

                idr.Close();
                con.Close();

            }
        }
    }

    protected void rpt_rp_history_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Label lbl_date = (Label)e.Item.FindControl("lbl_date");
        Label lbl_member_id = (Label)e.Item.FindControl("lbl_member_id");
        Label lbl_description = (Label)e.Item.FindControl("lbl_description");
        Label lbl_rp_total = (Label)e.Item.FindControl("lbl_rp_total");
        Label lbl_rp_used = (Label)e.Item.FindControl("lbl_rp_used");
        Label lbl_rp_earn = (Label)e.Item.FindControl("lbl_rp_earn");
        Label lbl_rp_balance = (Label)e.Item.FindControl("lbl_rp_balance");
        Label lbl_status = (Label)e.Item.FindControl("lbl_status");

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView drv = (DataRowView)(e.Item.DataItem);

            lbl_date.Text = Convert.ToDateTime(drv.Row["History_Date"].ToString()).ToString("dd-MM-yyyy");
            lbl_member_id.Text = drv.Row["Member_ID"].ToString();
            lbl_description.Text = drv.Row["Order_No"].ToString();
            lbl_rp_total.Text = Convert.ToDecimal(drv.Row["Redemption_Point"]).ToString("N0");
            lbl_rp_used.Text = Convert.ToDecimal(drv.Row["Redemption_Point_Used"]).ToString("N0");
            lbl_rp_earn.Text = Convert.ToDecimal(drv.Row["Redemption_Point_Earn"]).ToString("N0");
            lbl_rp_balance.Text = Convert.ToDecimal(drv.Row["Redemption_Point_Balance"]).ToString("N0");

            if (drv.Row["Status"].ToString() == "Success")
            {
                lbl_status.Text = drv.Row["Status"].ToString();
            }
            else
            {
                lbl_status.Text = "Failed";
            }
        }

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