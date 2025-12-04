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

public partial class Resources : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Load_Resource_Tab();
            string selectedFilterCode = Request.QueryString["filterCode"] ?? "All";
            ViewState["SelectedFilterCode"] = selectedFilterCode;
            Load_Content(selectedFilterCode, GetSortBy(), GetSortOrder(), ddlMediaType.SelectedValue);
        }
        else
        {
            string sortBy = hiddenSortBy.Value;
            string sortOrder = hiddenSortOrder.Value;
            string mediaType = ddlMediaType.SelectedValue;
            Load_Content(null, sortBy, sortOrder, mediaType);
        }
    }

    private void Load_Resource_Tab()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        string query = "SELECT Code, Name FROM dbo.Resource WHERE DeleteInd != 'X'";

        var resourceData = new List<ResourceItem>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                resourceData.Add(new ResourceItem { Code = "All", Name = "All" });

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    resourceData.Add(new ResourceItem
                    {
                        Code = reader["Code"].ToString(),
                        Name = reader["Name"].ToString()
                    });
                }
                connection.Close();
            }
        }

        // Sort alphabetically
        resourceData = resourceData.OrderBy(item => item.Name).ToList();

        // Ensure "Others" is at the end
        var others = resourceData.FirstOrDefault(item => item.Name == "Others");
        if (others != null)
        {
            resourceData.Remove(others);
            resourceData.Add(others);
        }

        rptResourceNames.DataSource = resourceData;
        rptResourceNames.DataBind();
    }

    private void Load_Content(string filterCode = null, string sortBy = "CreatedDT", string sortOrder = "ASC", string mediaType = null)
    {
        string sortColumn = GetSortColumn(sortBy);
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        string query = @"
    SELECT DISTINCT rc.IDs, rc.Name, rc.Discription, rc.ImgLink, rc.Code, rc.Created_DT, rc.ContentCode, r.Name as ResourceName, Priority
    FROM dbo.ResContent rc
    LEFT JOIN dbo.ResSubContent rsc ON rc.ContentCode = rsc.ContentCode
    LEFT JOIN dbo.Resource r ON rc.Code = r.Code
    WHERE rc.DeleteIdn != 'X'";

        if (!string.IsNullOrEmpty(filterCode) && filterCode != "All")
        {
            query += " AND rc.Code = @Code";
        }

        if (!string.IsNullOrEmpty(mediaType))
        {
            query += " AND rsc.MediaType = @MediaType";
        }

        // Add sorting to the query
        query += " ORDER BY " + sortColumn + " " + sortOrder;

        List<ContentItem> contentItems = new List<ContentItem>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                if (!string.IsNullOrEmpty(filterCode) && filterCode != "All")
                {
                    command.Parameters.AddWithValue("@Code", filterCode);
                }

                if (!string.IsNullOrEmpty(mediaType))
                {
                    command.Parameters.AddWithValue("@MediaType", mediaType);
                }

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var contentItem = new ContentItem
                    {
                        ID = Convert.ToInt32(reader["IDs"]),
                        Name = reader["Name"].ToString(),
                        Discription = reader["Discription"].ToString(),
                        ImgLink = "https://ecentra.com.my/backoffice/" + reader["ImgLink"].ToString(),
                        Code = reader["Code"].ToString(),
                        Content_Code = reader["ContentCode"].ToString(),
                        Created_DT = Convert.ToDateTime(reader["Created_DT"]).ToString("dd-MM-yyyy"),
                        ResourceName = reader["ResourceName"].ToString() // New property to store the name
                    };

                    contentItem.Media_Types = GetMediaTypes(connectionString, contentItem.Content_Code);
                    contentItems.Add(contentItem);
                }
                connection.Close();
            }
        }

        if(contentItems.Count > 0)
        {
            Repeater1.DataSource = contentItems;
            Repeater1.DataBind();
            div_no_record.Visible = false;
        }
        else
        {
            div_no_record.Visible = true;
        }

    }

    private List<string> GetMediaTypes(string connectionString, string contentCode)
    {
        List<string> mediaTypes = new List<string>();
        string query = "SELECT MediaType FROM dbo.ResSubContent WHERE ContentCode = @ContentCode";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ContentCode", contentCode);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    mediaTypes.Add(reader["MediaType"].ToString());
                }
                connection.Close();
            }
        }

        return mediaTypes.Distinct().ToList(); // Ensure distinct media types
    }

    protected void btnSort_Click(object sender, EventArgs e)
    {
        string sortBy = hiddenSortBy.Value;
        string sortOrder = hiddenSortOrder.Value;
        string mediaType = ddlMediaType.SelectedValue;
        Load_Content(null, sortBy, sortOrder, mediaType);
    }

    private string GetSortColumn(string sortBy)
    {
        switch (sortBy)
        {
            case "Content Name":
                return "Name";
            case "Date Created":
                return "Created_DT";
            default:
                return "Priority"; // Default sorting column
        }
    }

    protected void rptResourceNames_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "FilterContent")
        {
            string selectedCode = e.CommandArgument.ToString();
            Response.Redirect(string.Format("Resources.aspx?filterCode={0}", selectedCode));

        }
    }

    protected void ddlMediaType_SelectedIndexChanged(object sender, EventArgs e)
    {
        string selectedMediaType = ddlMediaType.SelectedValue;
        Load_Content(null, GetSortBy(), GetSortOrder(), selectedMediaType);
        //UpdatePaginationControls();
    }

    private string GetSortBy()
    {
        // Retrieve the sorting criterion (default to "Name")
        var sortCriterion = Request.Form["hiddenSortBy"];
        return string.IsNullOrEmpty(sortCriterion) ? "Name" : sortCriterion;
    }

    private string GetSortOrder()
    {
        // Retrieve the sorting order (default to "ASC")
        var sortOrder = Request.Form["hiddenSortOrder"];
        return string.IsNullOrEmpty(sortOrder) ? "ASC" : sortOrder;
    }

    protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "GoToContentPage")
        {
            string contentCode = e.CommandArgument.ToString();
            Response.Redirect("Resources_Content.aspx?contentCode=" + contentCode);
        }
    }

    protected void rptResourceNames_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ResourceItem resourceItem = e.Item.DataItem as ResourceItem;
            if (resourceItem != null)
            {
                HtmlControl li = (HtmlControl)e.Item.FindControl("li");
                if (li != null)
                {
                    string selectedFilterCode = Request.QueryString["filterCode"] ?? "All";

                    if (resourceItem.Code == selectedFilterCode)
                    {
                        li.Attributes["class"] = "active-tab";
                    }
                    else
                    {
                        li.Attributes["class"] = "inactive-tab";
                    }
                }
            }
        }
    }

}

public class ContentItem
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Discription { get; set; }
    public string ImgLink { get; set; }
    public string Code { get; set; }
    public string Created_DT { get; set; }
    public string Content_Code { get; set; }
    public List<string> Media_Types { get; set; }
    public string Thumbnail { get; set; }
    public string ResourceName { get; set; } // New property
}


public class ResourceItem
{
    public string Code { get; set; }
    public string Name { get; set; }
}
