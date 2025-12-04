using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Resources_Content : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string contentCode = Request.QueryString["contentCode"];
            if (!string.IsNullOrEmpty(contentCode))
            {
                LoadContentDetails(contentCode);
                LoadSubContent(contentCode);
            }
        }
    }

    private void LoadContentDetails(string contentCode)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        string query = "SELECT Name, Discription, ImgLink FROM dbo.ResContent WHERE ContentCode = @ContentCode";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ContentCode", contentCode);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    content_name.InnerText = reader["Name"].ToString();
                    content_desc.InnerText = reader["Discription"].ToString();
                    content_image.Src = "https://ecentra.com.my/backoffice/" + reader["ImgLink"].ToString();
                }
                connection.Close();
            }
        }
    }

    private void LoadSubContent(string contentCode)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        string query = "SELECT SubCode, Name, MediaType, MediaLink, Thumbnail FROM dbo.ResSubContent WHERE ContentCode = @ContentCode and DeleteInd != 'X'";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ContentCode", contentCode);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                // Assuming you have already set up the Repeater in your .aspx file
                rpt_content.DataSource = reader;
                rpt_content.DataBind();

                connection.Close();
            }
        }
    }

    protected string GetMediaHtml(string mediaType, string mediaLink)
    {
        string resolvedLink = ResolveUrl(mediaLink);

        switch (mediaType)
        {
            case "Image":
                return "<img class='card_image' src='https://ecentra.com.my/backoffice/" + resolvedLink + "' onerror=\"this.src='img/ContentImg/imagenotfound.png'\" alt='Image' />";
            case "Video":
                return "<video class='card_image' controls><source src='https://ecentra.com.my/backoffice/" + resolvedLink + "' type='video/mp4'>Your browser does not support the video tag.</video>";
            case "PDF":
                return "<iframe class='card_image' src='https://ecentra.com.my/backoffice/" + resolvedLink + "' frameborder='0'></iframe>";
            default:
                return "<p>Unsupported media type</p>";
        }
    }


}