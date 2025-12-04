using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Announcement : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["PageNumber"] = 1;
            Load_Ann("", "Newest");
        }
    }

    private void Load_Ann(string searchTerm = "", string sortOrder = "Newest", int pageNumber = 1, int pageSize = 10)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        string query = @"
        SELECT Ids, Title, PubStartDT
        FROM dbo.Announcement
        WHERE DeleteInd <> 'X'
        AND GETDATE() BETWEEN ISNULL(PubStartDT, '1900-01-01') AND ISNULL(PubEndDT, '9999-12-31')";

        string countQuery = @"
        SELECT COUNT(*)
        FROM dbo.Announcement
        WHERE DeleteInd <> 'X'
        AND GETDATE() BETWEEN ISNULL(PubStartDT, '1900-01-01') AND ISNULL(PubEndDT, '9999-12-31')";

        // Add search term filter if provided
        if (!string.IsNullOrEmpty(searchTerm))
        {
            query += " AND Title LIKE @SearchTerm";
            countQuery += " AND Title LIKE @SearchTerm";
        }

        // Apply sorting based on the sortOrder parameter
        if (sortOrder == "Newest")
        {
            query += " ORDER BY PubStartDT DESC";
        }
        else if (sortOrder == "Oldest")
        {
            query += " ORDER BY PubStartDT ASC";
        }

        // Apply pagination
        int offset = (pageNumber - 1) * pageSize;
        query += " OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            // Count total rows
            SqlCommand countCmd = new SqlCommand(countQuery, conn);
            if (!string.IsNullOrEmpty(searchTerm))
            {
                countCmd.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
            }

            conn.Open();
            int totalRows = (int)countCmd.ExecuteScalar();

            // Load paged data
            SqlCommand cmd = new SqlCommand(query, conn);
            if (!string.IsNullOrEmpty(searchTerm))
            {
                cmd.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
            }
            cmd.Parameters.AddWithValue("@Offset", offset);
            cmd.Parameters.AddWithValue("@PageSize", pageSize);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            gvAnnouncements.DataSource = dt;
            gvAnnouncements.DataBind();

            // Setup pagination
            SetupPagination(totalRows, pageSize, pageNumber);
        }
    }



    #region Search

    protected void txt_Search_TextChanged(object sender, EventArgs e)
    {
        string sortOrder = ddlsort.SelectedItem.Text;
        string searchTerm = txt_Search.Text.Trim();
        Load_Ann(searchTerm, sortOrder);
    }

    #endregion

    #region Sort

    protected void ddlSort_SelectedIndexChanged(object sender, EventArgs e)
    {
        string sortOrder = ddlsort.SelectedItem.Text;
        string searchTerm = txt_Search.Text.Trim();
        Load_Ann(searchTerm, sortOrder);
    }


    #endregion

    #region Pagination

    private void SetupPagination(int totalRows, int pageSize, int pageNumber)
    {
        int totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

        lblPageInfo.Text = string.Format("Page {0} of {1}", pageNumber, totalPages);
        btnPrevious.Enabled = pageNumber > 1;
        btnNext.Enabled = pageNumber < totalPages;
    }

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        int pageNumber = int.Parse(ViewState["PageNumber"].ToString());
        if (pageNumber > 1)
        {
            pageNumber--;
            ViewState["PageNumber"] = pageNumber;
            Load_Ann(txt_Search.Text.Trim(), ddlsort.SelectedItem.Text, pageNumber);
        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        int pageNumber = int.Parse(ViewState["PageNumber"].ToString());
        int totalPages = int.Parse(lblPageInfo.Text.Split(' ')[3]);

        if (pageNumber < totalPages)
        {
            pageNumber++;
            ViewState["PageNumber"] = pageNumber;
            Load_Ann(txt_Search.Text.Trim(), ddlsort.SelectedItem.Text, pageNumber);
        }
    }

    #endregion
}