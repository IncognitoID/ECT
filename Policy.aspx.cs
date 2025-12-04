using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Policy : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            Load_Policy_Content(Request.QueryString["id"].ToString());
        }
    }

    protected void Load_Policy_Content(string id)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("Load_Policy", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id", id);
                con.Open();
                SqlDataReader idr = cmd.ExecuteReader();

                if (idr.HasRows == true)
                {
                    DataTable v = new DataTable();
                    v.Load(idr);

                    lbl_policy_header.InnerText = v.Rows[0]["Title"].ToString();
                    lbl_policy_title.InnerText = v.Rows[0]["Title"].ToString();
                    lbl_policy_content.InnerHtml = v.Rows[0]["Content"].ToString();
                }

                idr.Close();
                con.Close();
            }
        }

    }
}