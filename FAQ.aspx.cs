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

public partial class FAQ : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        div_no_record.Visible = false;

        if (!Page.IsPostBack)
        {
            Load_FAQ(1, GetPageSize());
        }
    }

    protected void Load_FAQ(int pageIndex, int pageSize, string searchTerm = null, string sortColumn = "CreatedDT", string sortOrder = "DESC")
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("Load_FAQ", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SearchTerm", string.IsNullOrEmpty(searchTerm) ? (object)DBNull.Value : searchTerm);
                cmd.Parameters.AddWithValue("@PageSize", pageSize);
                cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
                cmd.Parameters.AddWithValue("@SortColumn", sortColumn);
                cmd.Parameters.AddWithValue("@SortOrder", sortOrder);

                con.Open();
                using (SqlDataReader idr = cmd.ExecuteReader())
                {
                    if (idr.HasRows)
                    {
                        DataTable dt = new DataTable();
                        dt.Load(idr);
                        rpt_faq.DataSource = dt;
                        rpt_faq.DataBind();

                        int totalRecords = (int)dt.Rows[0]["TotalRecords"];
                        lbl_Record3.Text = dt.Rows.Count.ToString();
                        lbl_Record2.Text = totalRecords.ToString();

                        PopulatePager(totalRecords, pageIndex, pageSize);
                    }
                    else
                    {
                        rpt_faq.Visible = false;
                        div_no_record.Visible = true;
                    }
                }
            }
        }
    }


    #region Search

    protected void txt_Search_TextChanged(object sender, EventArgs e)
    {
        string searchTerm = txt_Search.Text.Trim();
        int pageSize = int.Parse(ddlPageSize.SelectedValue);
        Load_FAQ(1, pageSize, searchTerm); // Start from the first page for new search
    }

    #endregion

    #region Pagination

    protected int GetPageSize()
    {
        int pageSize = 25; // default page size
        if (ddlPageSize.SelectedItem.Text == "Manual")
        {
            int parsedValue;
            if (int.TryParse(pagesize.Text, out parsedValue) && parsedValue >= 1)
            {
                pageSize = parsedValue;
            }
            else
            {
                pageSize = 25;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please enter a record number with the minimum of 1.')", true);
                pagesize.Text = "";
            }
        }
        else
        {
            int.TryParse(ddlPageSize.SelectedValue, out pageSize);
        }
        return pageSize;
    }

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPageSize.SelectedItem.Text == "Manual")
        {
            pagesize.Visible = true;
            pagesizesearchicon.Visible = true;
        }
        else
        {
            pagesize.Visible = false;
            pagesizesearchicon.Visible = false;
            Load_FAQ(1, GetPageSize());
        }
    }

    protected void ddlPager_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_FAQ(int.Parse(ddlPager.SelectedValue), GetPageSize());
    }

    private void PopulatePager(int recordCount, int currentPage, int pageSize)
    {
        ddlPager.Items.Clear();
        double pageCount = (double)recordCount / pageSize;
        int totalPages = (int)Math.Ceiling(pageCount);

        for (int i = 1; i <= totalPages; i++)
        {
            ddlPager.Items.Add(new ListItem("Page " + i.ToString(), i.ToString()));
        }

        if (ddlPager.Items.FindByValue(currentPage.ToString()) != null)
        {
            ddlPager.SelectedValue = currentPage.ToString();
        }
        else
        {
            ddlPager.SelectedValue = "1";
            Load_FAQ(1, pageSize);
        }
    }

    #endregion Pagination

    #region Sort

    protected void ddlSort_SelectedIndexChanged(object sender, EventArgs e)
    {
        string sortColumn = "CreatedDT";
        string sortOrder = "ASC";
        if (ddlsort.SelectedItem.Text == "Newest")
        {
            sortOrder = "DESC";
        }
        else
        {
            sortOrder = "ASC";
        }
        Load_FAQ(1, GetPageSize(), null, sortColumn, sortOrder);
    }

    //protected void btnSort_Click(object sender, EventArgs e)
    //{
    //    string sortColumn = radsortCriterion.Value;
    //    string sortOrder = radsortOrder.Value;
    //    Load_FAQ(1, GetPageSize(), null, sortColumn, sortOrder);
    //}


    #endregion
}