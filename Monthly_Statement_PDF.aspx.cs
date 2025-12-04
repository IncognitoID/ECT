using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Monthly_Statement_PDF : System.Web.UI.Page
{
    protected static String ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            bool cookieExists = HttpContext.Current.Request.Cookies["userid"] != null;

            if (cookieExists == true)
            {
                if (Request.Cookies["userid"].Value != null && Request.Cookies["userid"].Value != "")
                {
                    Load_Member_Details();
                    Load_Member_Monthly_Statement();
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

    protected void Load_Member_Details()
    {
        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("Statement_Load_Member_Details", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Memberid", Request.Cookies["userid"].Value);
                con.Open();
                SqlDataReader idr = cmd.ExecuteReader();

                if (idr.HasRows == true)
                {
                    while (idr.Read())
                    {
                        if (string.IsNullOrEmpty(idr["First_Name"].ToString()))
                        {
                            lbl_member_name.InnerText = idr["Company_Name"].ToString();
                        }
                        else
                        {
                            lbl_member_name.InnerText = idr["First_Name"].ToString();
                        }
                        lbl_member_id.InnerText = idr["cardno"].ToString();

                        string statement_Date = Request.QueryString["id"];
                        DateTime date;
                        if (!string.IsNullOrEmpty(statement_Date) && DateTime.TryParseExact(statement_Date, "MM/yyyy", null, System.Globalization.DateTimeStyles.None, out date))
                        {
                            // Calculate the start and end dates
                            DateTime startDate = new DateTime(date.Year, date.Month, 1);
                            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

                            // Manually format the dates
                            string formattedStartDate = startDate.ToString("dd/MM/yyyy");
                            string formattedEndDate = endDate.ToString("dd/MM/yyyy");
                            lbl_period.InnerText = formattedStartDate + " - " + formattedEndDate;
                        }
                        else
                        {
                            // Handle the case where the query string parameter is missing or invalid
                            lbl_period.InnerText = "Invalid date format";
                        }
                    }
                }

                idr.Close();
                con.Close();

            }
        }
    }

    protected void Load_Member_Monthly_Statement()
    {
        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            string statement_Date = Request.QueryString["id"];
            DateTime date;
            DateTime startDate = DateTime.UtcNow.AddHours(8);
            DateTime endDate = DateTime.UtcNow.AddHours(8);
            if (!string.IsNullOrEmpty(statement_Date) && DateTime.TryParseExact(statement_Date, "MM/yyyy", null, System.Globalization.DateTimeStyles.None, out date))
            {
                // Calculate the start and end dates
                startDate = new DateTime(date.Year, date.Month, 1);
                endDate = startDate.AddMonths(1).AddDays(-1);

                // Manually format the dates
                string formattedStartDate = startDate.ToString("dd/MM/yyyy");
                string formattedEndDate = endDate.ToString("dd/MM/yyyy");
            }

            using (SqlCommand cmd = new SqlCommand("Statement_Load_Member_Daily_Bonus", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Memberid", Request.Cookies["userid"].Value);
                cmd.Parameters.AddWithValue("@Start_Date", Convert.ToDateTime(startDate));
                cmd.Parameters.AddWithValue("@End_Date", Convert.ToDateTime(endDate).AddDays(1));
                con.Open();

                SqlDataReader idr = cmd.ExecuteReader();

                if (idr.HasRows == true)
                {
                    DataTable v = new DataTable();

                    v.Load(idr);

                    rpt_daily_statement.DataSource = v;
                    rpt_daily_statement.DataBind();
                }
                else
                {
                    DataTable v = new DataTable();

                    v.Load(idr);
                    v.Rows.Add(v.NewRow());

                    rpt_daily_statement.DataSource = v;
                    rpt_daily_statement.DataBind();
                }

                idr.Close();
                con.Close();

            }
        }

    }

    protected void rpt_daily_statement_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Label lbl_date = (Label)e.Item.FindControl("lbl_date");
        Label lbl_direct_profit = (Label)e.Item.FindControl("lbl_direct_profit");
        Label lbl_bb_cycle_1 = (Label)e.Item.FindControl("lbl_bb_cycle_1");
        Label lbl_bb_cycle_2 = (Label)e.Item.FindControl("lbl_bb_cycle_2");
        Label lbl_income_booster = (Label)e.Item.FindControl("lbl_income_booster");
        Label lbl_special_development_bonus = (Label)e.Item.FindControl("lbl_special_development_bonus");
        Label lbl_total_bonus = (Label)e.Item.FindControl("lbl_total_bonus");

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView drv = (DataRowView)(e.Item.DataItem);

            lbl_date.Text = Convert.ToDateTime(drv.Row["ReportDate"].ToString()).ToString("dd-MM-yyyy");
            lbl_direct_profit.Text = "RM " + Convert.ToDecimal(drv.Row["Total Direct Profit"].ToString()).ToString("###,###,##0.00");
            lbl_bb_cycle_1.Text = "RM" + Convert.ToDecimal(drv.Row["Total BB Cycle 1"].ToString()).ToString("###,###,##0.00");
            lbl_bb_cycle_2.Text = "RM" + Convert.ToDecimal(drv.Row["Total BB Cycle 2"].ToString()).ToString("###,###,##0.00");
            lbl_income_booster.Text = "RM" + Convert.ToDecimal(drv.Row["Total Income Booster"].ToString()).ToString("###,###,##0.00");
            lbl_special_development_bonus.Text = "RM" + Convert.ToDecimal(drv.Row["Total SDB"].ToString()).ToString("###,###,##0.00");
            lbl_total_bonus.Text = (Convert.ToDecimal(drv.Row["Total Direct Profit"].ToString()) + Convert.ToDecimal(drv.Row["Total BB Cycle 1"].ToString()) + Convert.ToDecimal(drv.Row["Total BB Cycle 2"].ToString()) + Convert.ToDecimal(drv.Row["Total Income Booster"].ToString()) + Convert.ToDecimal(drv.Row["Total SDB"].ToString()))
                            == 0
                            ? "RM 0.00"
                            : "RM " + (Convert.ToDecimal(drv.Row["Total Direct Profit"].ToString()) + Convert.ToDecimal(drv.Row["Total BB Cycle 1"].ToString()) + Convert.ToDecimal(drv.Row["Total BB Cycle 2"].ToString()) + Convert.ToDecimal(drv.Row["Total Income Booster"].ToString()) + Convert.ToDecimal(drv.Row["Total SDB"].ToString())).ToString("###,###,##0.00");
            lbl_monthly_sales.Text = "RM " + (Convert.ToDecimal(lbl_monthly_sales.Text.Replace("RM ", "")) + Convert.ToDecimal(lbl_total_bonus.Text.Replace("RM ", ""))).ToString("###,###,##0.00");
        }

    }
}