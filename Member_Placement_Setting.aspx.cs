using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Member_Placement_Setting : System.Web.UI.Page
{
    protected static String ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
    public enum MessageType { Success, Error, Info, Warning };

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            bool cookieExists = HttpContext.Current.Request.Cookies["userid"] != null;
            if (cookieExists == true)
            {
                if (Request.Cookies["userid"].Value != null && Request.Cookies["userid"].Value != "")
                {
                    GetMemberDefaultAssign(Request.Cookies["userid"].Value);
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

    protected void GetMemberDefaultAssign(string memberid)
    {
        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("Load_Member_Default_Assign_Memeber_Side", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Memberid", memberid);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (reader["Default_Assign_Member_Side"].ToString() == "Left")
                        {
                            btn_left.Checked = true;
                        }
                        else
                        {
                            btn_right.Checked = true;
                        }
                    }
                }

                reader.Close();
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

    protected void btn_confirm_Click(object sender, EventArgs e)
    {
        if (btn_left.Checked == false && btn_right.Checked == false)
        {
            ShowMessage_warning("Please select your placement setting.", MessageType.Warning);
            return;
        }
        else
        {
            string member_side = "Left";
            
            if(btn_left.Checked == true)
            {
                member_side = "Left";
            }
            else if (btn_right.Checked == true)
            {
                member_side = "Right";
            }

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Update_Member_Default_Assign_Member_Side", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Default_Assign_Member_Side", member_side);
                    cmd.Parameters.AddWithValue("Member", Request.Cookies["userid"].Value);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            ShowMessage("Successful setup placement setting.", MessageType.Success);

        }
    }

    protected void btn_left_CheckedChanged(object sender, EventArgs e)
    {
        btn_left.Checked = true;
        btn_right.Checked = false;
    }

    protected void btn_right_CheckedChanged(object sender, EventArgs e)
    {
        btn_left.Checked = false;
        btn_right.Checked = true;
    }
}