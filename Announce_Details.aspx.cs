using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Announce_Details : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["Ids"] != null)
            {
                int id = Convert.ToInt32(Request.QueryString["Ids"]);
                LoadAnnouncementDetails(id);
            }
        }
    }

    private void LoadAnnouncementDetails(int id, int pageNumber = 1, int pageSize = 10)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        string announcementQuery = "SELECT Title, Dsc, PubStartDT FROM dbo.Announcement WHERE Ids = @Ids";
        string postersQuery = "SELECT PosterLink, Caption FROM dbo.AnnPoster WHERE AnnID = @AnnID AND DeleteInd <> 'X'";

        // Updated PDF query with ROW_NUMBER()
        string pdfQuery = @"
    ;WITH PagedPDF AS
    (
        SELECT 
            PdfLink, 
            Caption, 
            ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS RowNum
        FROM dbo.AnnPDF
        WHERE AnnID = @AnnID AND DeleteInd <> 'X'
    )
    SELECT PdfLink, Caption
    FROM PagedPDF
    WHERE RowNum BETWEEN @Offset + 1 AND @Offset + @PageSize";

        string pdfcountQuery = "SELECT COUNT(*) FROM dbo.AnnPDF WHERE AnnID = @AnnID AND DeleteInd <> 'X'";

        int offset = (pageNumber - 1) * pageSize;

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            #region TITLE, PUB DATE & DESCRIPTION

            // Fetch announcement details
            SqlCommand announcementCmd = new SqlCommand(announcementQuery, conn);
            announcementCmd.Parameters.AddWithValue("@Ids", id);

            conn.Open();
            SqlDataReader announcementReader = announcementCmd.ExecuteReader();

            if (announcementReader.Read())
            {
                lblTitle.Text = announcementReader["Title"].ToString();
                lblDescription.Text = announcementReader["Dsc"].ToString();
            }
            announcementReader.Close();

            #endregion

            #region POSTER

            // Fetch and bind poster details
            SqlCommand postersCmd = new SqlCommand(postersQuery, conn);
            postersCmd.Parameters.AddWithValue("@AnnID", id);

            SqlDataAdapter posterda = new SqlDataAdapter(postersCmd);
            DataTable posterdt = new DataTable();
            posterda.Fill(posterdt);

            rptPoster.DataSource = posterdt;
            rptPoster.DataBind();

            #endregion

            #region PDF

            // Set up command for pagination
            SqlCommand pdfCmd = new SqlCommand(pdfQuery, conn);
            pdfCmd.Parameters.AddWithValue("@AnnID", id); // Ensure @AnnID is added here
            pdfCmd.Parameters.AddWithValue("@Offset", offset);
            pdfCmd.Parameters.AddWithValue("@PageSize", pageSize);

            SqlDataAdapter pdfda = new SqlDataAdapter(pdfCmd);
            DataTable pdfdt = new DataTable();
            pdfda.Fill(pdfdt);

            gvPdf.DataSource = pdfdt;
            gvPdf.DataBind();

            // Check if GridView has any data and update UpdatePanel visibility
            UpdatePanel1.Visible = gvPdf.Rows.Count > 0;

            #endregion

            #region PAGINATION

            // Count total rows
            SqlCommand countCmd = new SqlCommand(pdfcountQuery, conn);
            countCmd.Parameters.AddWithValue("@AnnID", id); // Ensure @AnnID is added here
            int totalRows = (int)countCmd.ExecuteScalar();

            // Setup pagination
            SetupPagination(totalRows, pageSize, pageNumber);

            #endregion
        }
    }



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
            LoadAnnouncementDetails(int.Parse(ViewState["AnnID"].ToString()), pageNumber);
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
            LoadAnnouncementDetails(int.Parse(ViewState["AnnID"].ToString()), pageNumber);
        }
    }

}
