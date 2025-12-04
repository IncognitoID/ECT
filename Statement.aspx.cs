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

public partial class Statement : System.Web.UI.Page
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
                    txt_start_date.Text = DateTime.UtcNow.AddHours(8).AddMonths(-1).ToString("yyyy-MM-dd");
                    txt_end_date.Text = DateTime.UtcNow.AddHours(8).ToString("yyyy-MM-dd");
                    Load_Member_Commission_Statement_Daily_Record(Request.Cookies["userid"].Value, txt_start_date.Text, txt_end_date.Text, 1);
                    Load_Member_Commission_Statement_Monthly_Record(Request.Cookies["userid"].Value, txt_start_date.Text, txt_end_date.Text);
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

    protected void Load_Member_Commission_Statement_Monthly_Record(string memberid, string startdate, string enddate)
    {
        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("Load_Member_Monthly_Bonus", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Memberid", memberid);
                cmd.Parameters.AddWithValue("@Start_Date", Convert.ToDateTime(startdate));
                cmd.Parameters.AddWithValue("@End_Date", Convert.ToDateTime(enddate).AddDays(1));
                con.Open();

                SqlDataReader idr = cmd.ExecuteReader();

                if (idr.HasRows == true)
                {
                    DataTable v = new DataTable();

                    v.Load(idr);
                    grd_monthly_record.DataSource = v;
                    grd_monthly_record.DataBind();
                    grd_monthly_record.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                else
                {
                    DataTable v = new DataTable();

                    v.Load(idr);
                    v.Rows.Add(v.NewRow());

                    grd_monthly_record.DataSource = v;
                    grd_monthly_record.DataBind();
                    int columnCount = grd_monthly_record.Rows[0].Cells.Count;

                    grd_monthly_record.Rows[0].Cells.Clear();
                    grd_monthly_record.Rows[0].Cells.Add(new TableCell());
                    grd_monthly_record.Rows[0].Cells[0].ColumnSpan = columnCount;
                    grd_monthly_record.Rows[0].Cells[0].Text = "No Record Found.";
                    grd_monthly_record.HeaderRow.TableSection = TableRowSection.TableHeader;
                }

                idr.Close();
                con.Close();

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

    protected void btn_seach_Click(object sender, EventArgs e)
    {
        if(string.IsNullOrEmpty(txt_start_date.Text))
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "AlertCode", "alert('Invalid start date');", true);
            txt_start_date.Focus();
        }
        else if (string.IsNullOrEmpty(txt_start_date.Text))
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "AlertCode", "alert('Invalid end date');", true);
            txt_start_date.Focus();
        }
        else if (Convert.ToDateTime(txt_start_date.Text) > Convert.ToDateTime(txt_end_date.Text))
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "AlertCode", "alert('Filter date range invalid.');", true);
            txt_start_date.Focus();
        }
        else
        {
            Load_Member_Commission_Statement_Daily_Record(Request.Cookies["userid"].Value, txt_start_date.Text, txt_end_date.Text, 1);
            Load_Member_Commission_Statement_Monthly_Record(Request.Cookies["userid"].Value, txt_start_date.Text, txt_end_date.Text);
        }
    }

    #region Daily

    protected void Load_Member_Commission_Statement_Daily_Record(string memberid, string startdate, string enddate, int pageIndex)
    {
        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("Load_Member_Daily_Bonus", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Memberid", memberid);
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

                    grd_daily_record.DataSource = v;
                    grd_daily_record.DataBind();
                    grd_daily_record.HeaderRow.TableSection = TableRowSection.TableHeader;

                    //int recordCount = Convert.ToInt32(cmd.Parameters["@RecordCount"].Value);
                    //PopulatePager(recordCount, pageIndex);
                    
                    int recordCount = Convert.ToInt32(cmd.Parameters["@RecordCount"].Value);
                    int totalpage = 0;
                    totalpage = (recordCount / int.Parse(ddlPageSize.SelectedValue)) + 1;
                    GeneratePagingNumbers(pageIndex, totalpage);

                }
                else
                {
                    DataTable v = new DataTable();

                    v.Load(idr);
                    v.Rows.Add(v.NewRow());

                    grd_daily_record.DataSource = v;
                    grd_daily_record.DataBind();
                    int columnCount = grd_daily_record.Rows[0].Cells.Count;

                    grd_daily_record.Rows[0].Cells.Clear();
                    grd_daily_record.Rows[0].Cells.Add(new TableCell());
                    grd_daily_record.Rows[0].Cells[0].ColumnSpan = columnCount;
                    grd_daily_record.Rows[0].Cells[0].Text = "No Record Found.";
                    grd_daily_record.HeaderRow.TableSection = TableRowSection.TableHeader;

                    List<PagingItem> pagingItems = new List<PagingItem>();
                    rpt_daily_pager.DataSource = pagingItems;
                    rpt_daily_pager.DataBind();
                }

                idr.Close();
                con.Close();

            }
        }
    }

    protected void GeneratePagingNumbers(int currentPage, int totalPages)
    {
        if (totalPages > 1)
        {
            List<PagingItem> pagingItems = new List<PagingItem>();

            // Add "First" button
            pagingItems.Add(new PagingItem("First", "1"));

            // Add page numbers
            for (int i = 1; i <= totalPages; i++)
            {
                if (i <= 4 || i > totalPages - 4 || Math.Abs(i - currentPage) <= 2)
                {
                    pagingItems.Add(new PagingItem(i.ToString(), i.ToString()));
                }
                else if (pagingItems[pagingItems.Count - 1].Text != "...")
                {
                    pagingItems.Add(new PagingItem("...", ""));
                }
            }

            // Add "Last" button
            pagingItems.Add(new PagingItem("Last", totalPages.ToString()));

            rpt_daily_pager.DataSource = pagingItems;
            rpt_daily_pager.DataBind();
        }
        else
        {
            List<PagingItem> pagingItems = new List<PagingItem>();

            // Add "First" button
            pagingItems.Add(new PagingItem("1", "1"));

            rpt_daily_pager.DataSource = pagingItems;
            rpt_daily_pager.DataBind();
        }
    }

    public class PagingItem
    {
        public string Text { get; set; }
        public string Value { get; set; }

        public PagingItem(string text, string value)
        {
            Text = text;
            Value = value;
        }
    }

    protected void grd_daily_record_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#cccccc'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");

            Label lbl_date = (Label)e.Row.FindControl("lbl_date");
            Label lbl_direct_profit = (Label)e.Row.FindControl("lbl_direct_profit");
            Label lbl_bb_cycle_1 = (Label)e.Row.FindControl("lbl_bb_cycle_1");
            Label lbl_bb_cycle_2 = (Label)e.Row.FindControl("lbl_bb_cycle_2");
            Label lbl_income_booster = (Label)e.Row.FindControl("lbl_income_booster");
            Label lbl_special_development_bonus = (Label)e.Row.FindControl("lbl_special_development_bonus");
            Label lbl_total_bonus = (Label)e.Row.FindControl("lbl_total_bonus");

            lbl_date.Text = Convert.ToDateTime(lbl_date.Text).ToString("dd-MM-yyyy");
            lbl_total_bonus.Text = (Convert.ToDecimal(lbl_direct_profit.Text) + Convert.ToDecimal(lbl_bb_cycle_1.Text) + Convert.ToDecimal(lbl_bb_cycle_2.Text) + Convert.ToDecimal(lbl_income_booster.Text) + Convert.ToDecimal(lbl_special_development_bonus.Text))
                                        == 0
                                        ? "0.00"
                                        : (Convert.ToDecimal(lbl_direct_profit.Text) + Convert.ToDecimal(lbl_bb_cycle_1.Text) + Convert.ToDecimal(lbl_bb_cycle_2.Text) + Convert.ToDecimal(lbl_income_booster.Text) + Convert.ToDecimal(lbl_special_development_bonus.Text)).ToString("###,###,##0.00");

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
        Load_Member_Commission_Statement_Daily_Record(Request.Cookies["userid"].Value, txt_start_date.Text, txt_end_date.Text, 1);
    }

    protected void btn_page_Click(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        Load_Member_Commission_Statement_Daily_Record(Request.Cookies["userid"].Value, txt_start_date.Text, txt_end_date.Text, pageIndex);
    }

    #endregion

    #region Monthly

    protected void grd_monthly_record_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#cccccc'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");

            Label lbl_date = (Label)e.Row.FindControl("lbl_date");
            Label lbl_direct_profit = (Label)e.Row.FindControl("lbl_direct_profit");
            Label lbl_bb_cycle_1 = (Label)e.Row.FindControl("lbl_bb_cycle_1");
            Label lbl_bb_cycle_2 = (Label)e.Row.FindControl("lbl_bb_cycle_2");
            Label lbl_income_booster = (Label)e.Row.FindControl("lbl_income_booster");
            Label lbl_special_development_bonus = (Label)e.Row.FindControl("lbl_special_development_bonus");
            Label lbl_total_bonus = (Label)e.Row.FindControl("lbl_total_bonus");

            lbl_date.Text = Convert.ToDateTime(lbl_date.Text).ToString("MM/yyyy");
            lbl_total_bonus.Text = (Convert.ToDecimal(lbl_direct_profit.Text) + Convert.ToDecimal(lbl_bb_cycle_1.Text) + Convert.ToDecimal(lbl_bb_cycle_2.Text) + Convert.ToDecimal(lbl_income_booster.Text) + Convert.ToDecimal(lbl_special_development_bonus.Text))
                                        == 0
                                        ? "0.00"
                                        : (Convert.ToDecimal(lbl_direct_profit.Text) + Convert.ToDecimal(lbl_bb_cycle_1.Text) + Convert.ToDecimal(lbl_bb_cycle_2.Text) + Convert.ToDecimal(lbl_income_booster.Text) + Convert.ToDecimal(lbl_special_development_bonus.Text)).ToString("###,###,##0.00");

        }
    }

    protected void grd_monthly_record_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    protected void grd_monthly_record_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Download")
        {
            string target = "Monthly_Statement_PDF.aspx?id=" + e.CommandArgument.ToString();
            Statement_Frame.Attributes.Add("src", target);
            ClientScript.RegisterStartupScript(this.GetType(), "Pop", "View_Statemnt_Modal();", true);
        }
    }
    #endregion


}