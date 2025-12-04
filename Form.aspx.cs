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

public partial class Form : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadForm();
        }
    }

    #region Fetching and Loading Media

    private void LoadForm()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        string query = "SELECT Ids, Title, FormType, FormLink FROM dbo.Form WHERE DeleteInd != 'X'";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                rptForm.DataSource = reader;
                rptForm.DataBind();

                connection.Close();
            }
        }
    }

    protected string GetMediaHtml(string mediaType, string mediaLink)
    {
        string resolvedLink = "https://ecentra.com.my/Backoffice/" + ResolveUrl(mediaLink);

        switch (mediaType)
        {
            case "Image":
                return "<img class='card_image' src='" + resolvedLink + "' onerror=\"this.src='img/ContentImg/imagenotfound.png'\" alt='Image' />";
            case "Video":
                return "<video class='card_image' controls><source src='" + resolvedLink + "' type='video/mp4'>Your browser does not support the video tag.</video>";
            case "PDF":
                return "<iframe class='card_image' src='" + resolvedLink + "' frameborder='0'></iframe>";
            default:
                return "<p>Unsupported media type</p>";
        }
    }

    #endregion
}

