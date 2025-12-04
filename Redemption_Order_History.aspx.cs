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

public partial class Redemption_Order_History : System.Web.UI.Page
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
                    Load_Member_Order_History(Request.Cookies["userid"].Value, 1);
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

    protected void btn_seach_Click(object sender, EventArgs e)
    {
        Load_Member_Order_History(Request.Cookies["userid"].Value, 1);
    }

    protected void Load_Member_Order_History(string memberid, int pageIndex)
    {
        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("Load_Member_Redemption_Order_History", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Memberid", memberid);
                cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
                cmd.Parameters.AddWithValue("@PageSize", int.Parse(ddlPageSize.SelectedValue));
                cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4);
                cmd.Parameters["@RecordCount"].Direction = ParameterDirection.Output;
                con.Open();

                SqlDataReader idr = cmd.ExecuteReader();

                if (idr.HasRows == true)
                {
                    DataTable v = new DataTable();

                    v.Load(idr);

                    grd_order_history.DataSource = v;
                    grd_order_history.DataBind();
                    grd_order_history.HeaderRow.TableSection = TableRowSection.TableHeader;

                    int recordCount = Convert.ToInt32(cmd.Parameters["@RecordCount"].Value);
                    PopulatePager(recordCount, pageIndex);
                }
                else
                {
                    DataTable v = new DataTable();

                    v.Load(idr);
                    v.Rows.Add(v.NewRow());

                    grd_order_history.DataSource = v;
                    grd_order_history.DataBind();
                    int columnCount = grd_order_history.Rows[0].Cells.Count;

                    grd_order_history.Rows[0].Cells.Clear();
                    grd_order_history.Rows[0].Cells.Add(new TableCell());
                    grd_order_history.Rows[0].Cells[0].ColumnSpan = columnCount;
                    grd_order_history.Rows[0].Cells[0].Text = "No Record Found.";
                    grd_order_history.HeaderRow.TableSection = TableRowSection.TableHeader;
                }

                idr.Close();
                con.Close();

            }
        }
    }

    private void PopulatePager(int recordCount, int currentPage)
    {
        double dblPageCount = (double)((decimal)recordCount / decimal.Parse(ddlPageSize.SelectedValue));
        int pageCount = (int)Math.Ceiling(dblPageCount);
        List<ListItem> pages = new List<ListItem>();
        if (pageCount > 0)
        {
            pages.Add(new ListItem("First", "1", currentPage > 1));
            for (int i = 1; i <= pageCount; i++)
            {
                pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
            }
            pages.Add(new ListItem("Last", pageCount.ToString(), currentPage < pageCount));
        }
        rpt_daily_pager.DataSource = pages;
        rpt_daily_pager.DataBind();
    }

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_Member_Order_History(Request.Cookies["userid"].Value, 1);
    }

    protected void btn_page_Click(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        Load_Member_Order_History(Request.Cookies["userid"].Value, pageIndex);
    }

    protected void grd_order_history_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#cccccc'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");

            Label lbl_status = (Label)e.Row.FindControl("lbl_status");

            string statusText = lbl_status.Text;
            string[] statusParts = statusText.Split(new string[] { ", " }, StringSplitOptions.None);

            string orderStatus = statusParts[0];
            string deliveryService = statusParts[1];

            if (deliveryService == "Self Pickup" && orderStatus == "To Ship")
            {
                lbl_status.Text = "Collect at HQ";
            }
            else
            {
                lbl_status.Text = orderStatus;
            }

            Label lbl_disabled = (Label)e.Row.FindControl("lbl_disabled");
            LinkButton btn_download = (LinkButton)e.Row.FindControl("btn_download");

            if (orderStatus != "To Pay" && orderStatus != "Cancelled")
            {
                lbl_disabled.Visible = false;
                btn_download.Visible = true;
            }
            else
            {
                lbl_disabled.Visible = true;
                btn_download.Visible = false;
            }
        }
    }


    protected void grd_order_history_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            Response.Redirect("Redemption_Order_Details.aspx?id=" + e.CommandArgument.ToString());
        }

        if (e.CommandName == "Download")
        {
            string target = "Invoice_Redemption.aspx?id=" + e.CommandArgument.ToString();
            InvoiceFrame.Src = target;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ViewInvoiceModal();", true);
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