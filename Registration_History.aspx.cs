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

public partial class Registration_History : System.Web.UI.Page
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
                    Load_Registration_Order_History(Request.Cookies["userid"].Value, 1);
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
        Load_Registration_Order_History(Request.Cookies["userid"].Value, 1);
    }

    protected void Load_Registration_Order_History(string memberid, int pageIndex)
    {
        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("Load_Member_Registration_History", con))
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
                    // Bind to an empty DataTable to reset the GridView
                    grd_order_history.DataSource = new DataTable();
                    grd_order_history.DataBind();

                    // Ensure the GridView has columns to display the "No Record Found" message correctly
                    if (grd_order_history.HeaderRow != null)
                    {
                        int columnCount = grd_order_history.HeaderRow.Cells.Count;

                        // Create a new empty row with a "No Record Found." message
                        GridViewRow emptyRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
                        TableCell cell = new TableCell
                        {
                            ColumnSpan = columnCount,
                            Text = "No Record Found.",
                            HorizontalAlign = HorizontalAlign.Center
                        };

                        emptyRow.Cells.Add(cell);

                        // Check if Controls[0] exists before adding the empty row
                        if (grd_order_history.Controls.Count > 0)
                        {
                            grd_order_history.Controls[0].Controls.Add(emptyRow);
                        }

                        grd_order_history.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
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
        Load_Registration_Order_History(Request.Cookies["userid"].Value, 1);
    }

    protected void btn_page_Click(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        Load_Registration_Order_History(Request.Cookies["userid"].Value, pageIndex);
    }

    protected void grd_order_history_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#cccccc'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");

            Label lbl_disabled = (Label)e.Row.FindControl("lbl_disabled");
            Label lbl_status = (Label)e.Row.FindControl("lbl_status");
            LinkButton btn_edit = (LinkButton)e.Row.FindControl("btn_edit");

            if (lbl_status.Text == "Completed")
            {
                lbl_disabled.Visible = true;
                btn_edit.Visible = false;
            }
            else
            {
                lbl_disabled.Visible = false;
                btn_edit.Visible= true;
            }

            GridView gvOrders = e.Row.FindControl("gvOrders") as GridView;
            DataRowView rowView = e.Row.DataItem as DataRowView;

            if (rowView != null)
            {
                // Get the value of the "Variation_Code" field
                string member_id = rowView["cardno"].ToString();
                string status = rowView["status"].ToString();
                using (SqlConnection con2 = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Load_Registration_Order", con2))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Member_ID", member_id);
                        con2.Open();
                        SqlDataReader idr = cmd.ExecuteReader();

                        if (idr.HasRows == true)
                        {
                            DataTable v = new DataTable();

                            v.Load(idr);

                            gvOrders.DataSource = v;
                            gvOrders.DataBind();
                        }

                        idr.Close();
                        con2.Close();
                    }
                }
            }
        }
    }

    protected void grd_order_history_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            Response.Redirect("Register_Member.aspx?id=" + e.CommandArgument.ToString());
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

    protected void gvOrders_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            Response.Redirect("Registration_History_Details.aspx?id=" + e.CommandArgument.ToString());
        }

        if (e.CommandName == "Download")
        {
            string target = "Invoice.aspx?id=" + e.CommandArgument.ToString();
            InvoiceFrame.Src = target;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ViewInvoiceModal();", true);
        }
    }
}