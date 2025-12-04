using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Event_Details : System.Web.UI.Page
{
    protected static String ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
    int slidercount = 0;
    int licount = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["Ids"] != null)
            {
                int id = Convert.ToInt32(Request.QueryString["Ids"]);
                LoadAnnouncementDetails(id);
                LoadCarouselImages(id);
            }
        }
    }

    private void LoadCarouselImages(int id)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        string query = "SELECT PosterLink FROM dbo.EventPoster WHERE DeleteInd <> 'X' AND EventID = @AnnID ";

        StringBuilder carouselItems = new StringBuilder();

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@AnnID", id);
                SqlDataReader reader = cmd.ExecuteReader();
                int index = 0;

                while (reader.Read())
                {
                    string posterLink = "https://ecentra.com.my/Backoffice/" + reader["PosterLink"].ToString();
                    string activeClass = index == 0 ? "active" : "";

                    carouselItems.Append(string.Format(@"
                    <div class='carousel-item {0}'>
                        <img src='{1}' class='d-block w-100 img-fluid' alt='Image {2}'>
                    </div>", activeClass, posterLink, index + 1));

                    index++;
                }
            }
        }

        // Insert the generated carousel items into a placeholder control in your .aspx page
        CarouselPlaceholder.Controls.Add(new Literal { Text = carouselItems.ToString() });
    }


    private void LoadAnnouncementDetails(int id)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        string announcementQuery = "SELECT * FROM dbo.Event WHERE Ids = @Ids";
        string updateViewsQuery = "UPDATE dbo.Event SET Views = Views + 1 WHERE Ids = @Ids";

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();

            #region TITLE, PUB DATE & DESCRIPTION

            // Fetch announcement details
            SqlCommand announcementCmd = new SqlCommand(announcementQuery, conn);
            announcementCmd.Parameters.AddWithValue("@Ids", id);
            SqlDataReader announcementReader = announcementCmd.ExecuteReader();

            if (announcementReader.Read())
            {
                lblTitle.Text = announcementReader["Title"].ToString();
                lblDescription.Text = announcementReader["Dsc"].ToString();
            }
            announcementReader.Close();

            #endregion

            #region UPDATE VIEWS

            // Update views count
            SqlCommand updateViewsCmd = new SqlCommand(updateViewsQuery, conn);
            updateViewsCmd.Parameters.AddWithValue("@Ids", id);
            updateViewsCmd.ExecuteNonQuery();

            #endregion

            //#region POSTER

            //// Fetch and bind poster details
            //SqlCommand postersCmd = new SqlCommand(postersQuery, conn);
            //postersCmd.Parameters.AddWithValue("@AnnID", id);

            //SqlDataAdapter posterda = new SqlDataAdapter(postersCmd);
            //DataTable posterdt = new DataTable();
            //posterda.Fill(posterdt);

            //rptPoster.DataSource = posterdt;
            //rptPoster.DataBind();

            //#endregion
        }
    }
}
