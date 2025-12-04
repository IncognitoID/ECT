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

public partial class View_Order_Stockist : System.Web.UI.Page
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
                    txt_start_date.Value = DateTime.UtcNow.AddHours(8).AddMonths(-1).ToString("yyyy-MM-dd");
                    txt_end_date.Value = DateTime.UtcNow.AddHours(8).ToString("yyyy-MM-dd");
                    Load_Member_Order_History(Request.Cookies["userid"].Value, txt_start_date.Value, txt_end_date.Value, 1);
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

    protected void btn_search_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txt_start_date.Value))
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "AlertCode", "alert('Invalid start date');", true);
            txt_start_date.Focus();
        }
        else if (string.IsNullOrEmpty(txt_start_date.Value))
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "AlertCode", "alert('Invalid end date');", true);
            txt_start_date.Focus();
        }
        else if (Convert.ToDateTime(txt_start_date.Value) > Convert.ToDateTime(txt_end_date.Value))
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "AlertCode", "alert('Filter date range invalid.');", true);
            txt_start_date.Focus();
        }
        else
        {
            Load_Member_Order_History(Request.Cookies["userid"].Value, txt_start_date.Value, txt_end_date.Value, 1);
        }
    }


    protected void Load_Member_Order_History(string memberid, string startdate, string enddate, int pageIndex)
    {
        string filter_text = "";
        if (string.IsNullOrEmpty(txt_search.Value))
        {
            filter_text = "%%";
        }
        else
        {
            filter_text = "%" + txt_search.Value + "%";
        }

        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("Load_Stockist_Sales_Order_History", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Memberid", memberid);
                cmd.Parameters.AddWithValue("@Filter_Text", filter_text);
                cmd.Parameters.AddWithValue("@Delivery_Service", ddl_delivery_service.SelectedValue);
                cmd.Parameters.AddWithValue("@Order_Status", ddl_status.SelectedValue);
                cmd.Parameters.AddWithValue("@Start_Date", Convert.ToDateTime(startdate));
                cmd.Parameters.AddWithValue("@End_Date", Convert.ToDateTime(enddate).AddDays(1));
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
        txt_start_date.Value = DateTime.UtcNow.AddHours(8).AddMonths(-1).ToString("yyyy-MM-dd");
        txt_end_date.Value = DateTime.UtcNow.AddHours(8).ToString("yyyy-MM-dd");
        Load_Member_Order_History(Request.Cookies["userid"].Value, txt_start_date.Value, txt_end_date.Value, 1);
    }

    protected void btn_page_Click(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        txt_start_date.Value = DateTime.UtcNow.AddHours(8).AddMonths(-1).ToString("yyyy-MM-dd");
        txt_end_date.Value = DateTime.UtcNow.AddHours(8).ToString("yyyy-MM-dd");
        Load_Member_Order_History(Request.Cookies["userid"].Value, txt_start_date.Value, txt_end_date.Value, pageIndex);
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
        }
    }


    protected void grd_order_history_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            Response.Redirect("View_Order_Details_Stockist.aspx?id=" + e.CommandArgument.ToString());
        }

        if(e.CommandName == "Download")
        {
            string target = "Invoice.aspx?id=" + e.CommandArgument.ToString();
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