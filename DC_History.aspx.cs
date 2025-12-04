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

public partial class DC_History : System.Web.UI.Page
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
                    Load_Member_DC_History(Request.Cookies["userid"].Value);
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

    protected void Load_Member_DC_History(string memberid)
    {
        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("Load_Member_DC_History", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Memberid", memberid);
                con.Open();

                SqlDataReader idr = cmd.ExecuteReader();

                if (idr.HasRows == true)
                {
                    rpt_dc_history.Visible = true;
                    tr_no_record.Visible = false;
                    DataTable v = new DataTable();

                    v.Load(idr);
                    rpt_dc_history.DataSource = v;
                    rpt_dc_history.DataBind();
                }
                else
                {
                    rpt_dc_history.Visible = false;
                    tr_no_record.Visible = true;
                }

                idr.Close();
                con.Close();

            }
        }
    }

    protected void rpt_dc_history_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Label lbl_date = (Label)e.Item.FindControl("lbl_date");
        Label lbl_member_id = (Label)e.Item.FindControl("lbl_member_id");
        Label lbl_order_no = (Label)e.Item.FindControl("lbl_order_no");
        Label lbl_dc_cw = (Label)e.Item.FindControl("lbl_dc_cw");
        Label lbl_dc_used = (Label)e.Item.FindControl("lbl_dc_used");
        Label lbl_dc_earn = (Label)e.Item.FindControl("lbl_dc_earn");
        Label lbl_dc_balance = (Label)e.Item.FindControl("lbl_dc_balance");
        Label lbl_status = (Label)e.Item.FindControl("lbl_status");

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView drv = (DataRowView)(e.Item.DataItem);

            lbl_date.Text = Convert.ToDateTime(drv.Row["History_Date"].ToString()).ToString("dd-MM-yyyy");
            lbl_member_id.Text = drv.Row["Member_ID"].ToString();
            lbl_order_no.Text = drv.Row["Order_No"].ToString();
            lbl_dc_cw.Text = drv.Row["DC_C_W"].ToString().Substring(0, drv.Row["DC_C_W"].ToString().Length - 3);
            lbl_dc_used.Text = drv.Row["DC_Used"].ToString().Substring(0, drv.Row["DC_Used"].ToString().Length - 3);
            lbl_dc_earn.Text = drv.Row["DC_Earn"].ToString().Substring(0, drv.Row["DC_Earn"].ToString().Length - 3);
            lbl_dc_balance.Text = drv.Row["DC_Balance"].ToString().Substring(0, drv.Row["DC_Balance"].ToString().Length - 3);

            if(drv.Row["Status"].ToString() == "Success")
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