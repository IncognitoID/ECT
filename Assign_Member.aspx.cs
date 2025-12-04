using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Assign_Member : System.Web.UI.Page
{
    protected static String ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
    public enum MessageType { Success, Error, Info, Warning };

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bool cookieExists = HttpContext.Current.Request.Cookies["userid"] != null;
            if (cookieExists == true)
            {
                if (Request.Cookies["userid"].Value != null && Request.Cookies["userid"].Value != "")
                {
                    Load_Member_To_Be_Assign(Request.Cookies["userid"].Value);
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

    protected void Load_Member_To_Be_Assign(string memberid)
    {
        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("Load_All_Member_No_Upline", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Memberid", memberid);
                con.Open();

                SqlDataReader idr = cmd.ExecuteReader();

                if (idr.HasRows == true)
                {
                    rpt_assign_member.Visible = true;
                    tr_no_record.Visible = false;
                    DataTable v = new DataTable();

                    v.Load(idr);
                    rpt_assign_member.DataSource = v;
                    rpt_assign_member.DataBind();
                }
                else
                {
                    rpt_assign_member.Visible = false;
                    tr_no_record.Visible = true;
                }

                idr.Close();
                con.Close();

            }
        }
    }

    protected void rpt_assign_member_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Label lbl_member_name = (Label)e.Item.FindControl("lbl_member_name");
        Label lbl_member_join_date = (Label)e.Item.FindControl("lbl_member_join_date");
        Label lbl_member_package = (Label)e.Item.FindControl("lbl_member_package");
        Label lbl_member_bv = (Label)e.Item.FindControl("lbl_member_bv");
        LinkButton btn_confirm = (LinkButton)e.Item.FindControl("btn_confirm");
        DropDownList ddl_member_placement = (DropDownList)e.Item.FindControl("ddl_member_placement");
        HtmlTableRow tr_no_record = (HtmlTableRow)e.Item.FindControl("tr_no_record");

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView drv = (DataRowView)(e.Item.DataItem);

            if (!string.IsNullOrEmpty(drv.Row["Username"].ToString()))
            {
                lbl_member_name.Text = drv.Row["Username"].ToString() + "<br/>" + "(" + drv.Row["UserID"].ToString() + ")";
            }
            else
            {
                lbl_member_name.Text = drv.Row["Company_Name"].ToString() + "<br/>" + "(" + drv.Row["UserID"].ToString() + ")";
            }
            lbl_member_join_date.Text = Convert.ToDateTime(drv.Row["Joined Date"].ToString()).ToString("dd MMM yyyy hh:mm:ss tt");
            lbl_member_package.Text = drv.Row["Package"].ToString();
            lbl_member_bv.Text = Convert.ToDecimal(drv.Row["BV"]).ToString("###,###,###");

            string value = "";
            bool cookieExists = HttpContext.Current.Request.Cookies["language"] != null;
            if (cookieExists == true)
            {
                if (Request.Cookies["language"].Value != null && Request.Cookies["language"].Value != "")
                {
                    if (Request.Cookies["language"].Value == "Chinese")
                    {
                        value = "确认";
                        ListItem defaultItem1 = new ListItem(drv.Row["Left_Downline"].ToString(), "Left");
                        ListItem defaultItem2 = new ListItem(drv.Row["Right_Downline"].ToString(), "Right");
                        ListItem defaultItem3 = new ListItem("输入账号", "KeyinID");
                        ListItem defaultItem4 = new ListItem("向左直到尽头", "Always Left");
                        ListItem defaultItem5 = new ListItem("向右直到尽头", "Always Right");
                        ddl_member_placement.Items.Insert(0, defaultItem1);
                        ddl_member_placement.Items.Insert(1, defaultItem2);
                        ddl_member_placement.Items.Insert(2, defaultItem4);
                        ddl_member_placement.Items.Insert(3, defaultItem5);
                        ddl_member_placement.Items.Insert(4, defaultItem3);
                    }
                    else
                    {
                        value = "Confirm";
                        ListItem defaultItem1 = new ListItem(drv.Row["Left_Downline"].ToString(), "Left");
                        ListItem defaultItem2 = new ListItem(drv.Row["Right_Downline"].ToString(), "Right");
                        ListItem defaultItem3 = new ListItem("Key in ID", "KeyinID");
                        ListItem defaultItem4 = new ListItem("Left toward end", "Always Left");
                        ListItem defaultItem5 = new ListItem("Right toward end", "Always Right");
                        ddl_member_placement.Items.Insert(0, defaultItem1);
                        ddl_member_placement.Items.Insert(1, defaultItem2);
                        ddl_member_placement.Items.Insert(2, defaultItem4);
                        ddl_member_placement.Items.Insert(3, defaultItem5);
                        ddl_member_placement.Items.Insert(4, defaultItem3);
                    }
                }
                else
                {
                    value = "Confirm";
                    ListItem defaultItem1 = new ListItem(drv.Row["Left_Downline"].ToString(), "Left");
                    ListItem defaultItem2 = new ListItem(drv.Row["Right_Downline"].ToString(), "Right");
                    ListItem defaultItem3 = new ListItem("Key in ID", "KeyinID");
                    ListItem defaultItem4 = new ListItem("Left toward end", "Always Left");
                    ListItem defaultItem5 = new ListItem("Right toward end", "Always Right");
                    ddl_member_placement.Items.Insert(0, defaultItem1);
                    ddl_member_placement.Items.Insert(1, defaultItem2);
                    ddl_member_placement.Items.Insert(2, defaultItem4);
                    ddl_member_placement.Items.Insert(3, defaultItem5);
                    ddl_member_placement.Items.Insert(4, defaultItem3);
                }
            }
            else
            {
                value = "Confirm";
                ListItem defaultItem1 = new ListItem(drv.Row["Left_Downline"].ToString(), "Left");
                ListItem defaultItem2 = new ListItem(drv.Row["Right_Downline"].ToString(), "Right");
                ListItem defaultItem3 = new ListItem("Key in ID", "KeyinID");
                ListItem defaultItem4 = new ListItem("Left toward end", "Always Left");
                ListItem defaultItem5 = new ListItem("Right toward end", "Always Right");
                ddl_member_placement.Items.Insert(0, defaultItem1);
                ddl_member_placement.Items.Insert(1, defaultItem2);
                ddl_member_placement.Items.Insert(2, defaultItem4);
                ddl_member_placement.Items.Insert(3, defaultItem5);
                ddl_member_placement.Items.Insert(4, defaultItem3);
            }

            btn_confirm.Text = value;

            ddl_member_placement.SelectedValue = drv.Row["Default_Assign_Member_Side"].ToString();
        }
    }

    protected void ddl_member_placement_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlMemberPlacement = (DropDownList)sender;
        RepeaterItem item = (RepeaterItem)ddlMemberPlacement.NamingContainer;

        HtmlGenericControl div_id = (HtmlGenericControl)item.FindControl("div_id");

        if (ddlMemberPlacement.SelectedValue == "KeyinID")
        {
            div_id.Visible = true;
        }
        else
        {
            div_id.Visible = false;
        }
    }

    protected void rpt_assign_member_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Confirm")
        {
            RepeaterItem item = e.Item;
            DropDownList ddlMemberPlacement = (DropDownList)item.FindControl("ddl_member_placement");
            DropDownList ddl_default_placement = (DropDownList)item.FindControl("ddl_default_placement");
            TextBox txt_keyinid = (TextBox)item.FindControl("txt_keyinid");

            if (ddlMemberPlacement.SelectedValue == "KeyinID" && string.IsNullOrEmpty(txt_keyinid.Text))
            {
                ShowMessage_warning("Please key in ID", MessageType.Warning);
                return;
            }
            else
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    string upline = "";

                    if (ddlMemberPlacement.SelectedValue == "Left")
                    {
                        string default_assign_member_id = "";
                        string downline = "";
                        string side = "";

                        using (SqlCommand cmd = new SqlCommand("Get_Member_Default_Assign", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("MemberID", Request.Cookies["userid"].Value);

                            con.Open();
                            SqlDataReader idr = cmd.ExecuteReader();

                            if (idr.HasRows == true)
                            {
                                while (idr.Read())
                                {
                                    default_assign_member_id = idr["Default_Assign_Member"].ToString();
                                    downline = idr["Downline_A"].ToString();
                                }
                            }
                            else
                            {
                                ShowMessage_warning("Unable to assign member", MessageType.Warning);
                                return;
                            }

                            idr.Close();
                            con.Close();
                        }
                        if (string.IsNullOrEmpty(downline))
                        {
                            upline = Request.Cookies["userid"].Value;
                            Update_Member_Upline(upline, e.CommandArgument.ToString(), "Left", "Left");
                        }
                        else
                        {
                            upline = downline;
                            while (!string.IsNullOrEmpty(upline))
                            {
                                using (SqlCommand cmd = new SqlCommand("Get_Member_Default_Assign", con))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("MemberID", upline);

                                    con.Open();
                                    SqlDataReader idr = cmd.ExecuteReader();

                                    if (idr.HasRows == true)
                                    {
                                        while (idr.Read())
                                        {
                                            if (idr["Default_Assign_Member_Side"].ToString() == "Left")
                                            {
                                                side = "Left";
                                                downline = idr["Downline_A"].ToString();
                                            }
                                            else
                                            {
                                                side = "Right";
                                                downline = idr["Downline_B"].ToString();
                                            }
                                        }
                                    }

                                    idr.Close();
                                    con.Close();
                                }

                                if (string.IsNullOrEmpty(downline))
                                {
                                    Update_Member_Upline(upline, e.CommandArgument.ToString(), side, "Left");
                                    break;
                                }
                                else
                                {
                                    upline = downline;
                                }
                            }
                        }
                    }

                    if (ddlMemberPlacement.SelectedValue == "Right")
                    {
                        string default_assign_member_id = "";
                        string downline = "";
                        string side = "";

                        using (SqlCommand cmd = new SqlCommand("Get_Member_Default_Assign", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("MemberID", Request.Cookies["userid"].Value);

                            con.Open();
                            SqlDataReader idr = cmd.ExecuteReader();

                            if (idr.HasRows == true)
                            {
                                while (idr.Read())
                                {
                                    default_assign_member_id = idr["Default_Assign_Member"].ToString();
                                    downline = idr["Downline_B"].ToString();
                                }
                            }
                            else
                            {
                                ShowMessage_warning("Unable to assign member", MessageType.Warning);
                                return;
                            }

                            idr.Close();
                            con.Close();
                        }
                        if (string.IsNullOrEmpty(downline))
                        {
                            upline = Request.Cookies["userid"].Value;
                            Update_Member_Upline(upline, e.CommandArgument.ToString(), "Right", "Right");
                        }
                        else
                        {
                            upline = downline;
                            while (!string.IsNullOrEmpty(upline))
                            {
                                using (SqlCommand cmd = new SqlCommand("Get_Member_Default_Assign", con))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("MemberID", upline);

                                    con.Open();
                                    SqlDataReader idr = cmd.ExecuteReader();

                                    if (idr.HasRows == true)
                                    {
                                        while (idr.Read())
                                        {
                                            if (idr["Default_Assign_Member_Side"].ToString() == "Left")
                                            {
                                                side = "Left";
                                                downline = idr["Downline_A"].ToString();
                                            }
                                            else
                                            {
                                                side = "Right";
                                                downline = idr["Downline_B"].ToString();
                                            }
                                        }
                                    }

                                    idr.Close();
                                    con.Close();
                                }

                                if (string.IsNullOrEmpty(downline))
                                {
                                    Update_Member_Upline(upline, e.CommandArgument.ToString(), side, "Right");
                                    break;
                                }
                                else
                                {
                                    upline = downline;
                                }
                            }
                        }
                    }

                    if (ddlMemberPlacement.SelectedValue == "Always Left")
                    {
                        string default_assign_member_id = "";
                        string downline = "";
                        string side = "";

                        using (SqlCommand cmd = new SqlCommand("Get_Member_Default_Assign", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("MemberID", Request.Cookies["userid"].Value);

                            con.Open();
                            SqlDataReader idr = cmd.ExecuteReader();

                            if (idr.HasRows == true)
                            {
                                while (idr.Read())
                                {
                                    default_assign_member_id = idr["Default_Assign_Member"].ToString();
                                    downline = idr["Downline_A"].ToString();
                                }
                            }
                            else
                            {
                                ShowMessage_warning("Unable to assign member", MessageType.Warning);
                                return;
                            }

                            idr.Close();
                            con.Close();
                        }
                        if (string.IsNullOrEmpty(downline))
                        {
                            upline = Request.Cookies["userid"].Value;
                            Update_Member_Upline(upline, e.CommandArgument.ToString(), "Left", "Left");
                        }
                        else
                        {
                            upline = downline;
                            while (!string.IsNullOrEmpty(upline))
                            {
                                using (SqlCommand cmd = new SqlCommand("Get_Member_Default_Assign", con))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("MemberID", upline);

                                    con.Open();
                                    SqlDataReader idr = cmd.ExecuteReader();

                                    if (idr.HasRows == true)
                                    {
                                        while (idr.Read())
                                        {
                                            side = "Left";
                                            downline = idr["Downline_A"].ToString();
                                        }
                                    }

                                    idr.Close();
                                    con.Close();
                                }

                                if (string.IsNullOrEmpty(downline))
                                {
                                    Update_Member_Upline(upline, e.CommandArgument.ToString(), side, "Left");
                                    break;
                                }
                                else
                                {
                                    upline = downline;
                                }
                            }
                        }
                    }

                    if (ddlMemberPlacement.SelectedValue == "Always Right")
                    {
                        string default_assign_member_id = "";
                        string downline = "";
                        string side = "";

                        using (SqlCommand cmd = new SqlCommand("Get_Member_Default_Assign", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("MemberID", Request.Cookies["userid"].Value);

                            con.Open();
                            SqlDataReader idr = cmd.ExecuteReader();

                            if (idr.HasRows == true)
                            {
                                while (idr.Read())
                                {
                                    default_assign_member_id = idr["Default_Assign_Member"].ToString();
                                    downline = idr["Downline_B"].ToString();
                                }
                            }
                            else
                            {
                                ShowMessage_warning("Unable to assign member", MessageType.Warning);
                                return;
                            }

                            idr.Close();
                            con.Close();
                        }
                        if (string.IsNullOrEmpty(downline))
                        {
                            upline = Request.Cookies["userid"].Value;
                            Update_Member_Upline(upline, e.CommandArgument.ToString(), "Right", "Right");
                        }
                        else
                        {
                            upline = downline;
                            while (!string.IsNullOrEmpty(upline))
                            {
                                using (SqlCommand cmd = new SqlCommand("Get_Member_Default_Assign", con))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("MemberID", upline);

                                    con.Open();
                                    SqlDataReader idr = cmd.ExecuteReader();

                                    if (idr.HasRows == true)
                                    {
                                        while (idr.Read())
                                        {
                                            side = "Right";
                                            downline = idr["Downline_B"].ToString();
                                        }
                                    }

                                    idr.Close();
                                    con.Close();
                                }

                                if (string.IsNullOrEmpty(downline))
                                {
                                    Update_Member_Upline(upline, e.CommandArgument.ToString(), side, "Right");
                                    break;
                                }
                                else
                                {
                                    upline = downline;
                                }
                            }
                        }
                    }

                    if (ddlMemberPlacement.SelectedValue == "KeyinID" && !string.IsNullOrEmpty(txt_keyinid.Text))
                    {

                        #region Check ID Under Left or Right

                        string upline_id = "";
                        string upline_side = "";
                        string downline_a = "";
                        string downline_b = "";
                        string memberid = Request.Cookies["userid"].Value; // Current user
                        string current_id = txt_keyinid.Text; // Starting member (keyed in)
                        string previous_id = current_id; // To keep track of where we came from

                        // Traverse up until we find the logged-in user (memberid)
                        while (true)
                        {
                            using (SqlCommand cmd = new SqlCommand("Get_Member_Upline", con))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("MemberID", current_id);

                                con.Open();
                                SqlDataReader idr = cmd.ExecuteReader();

                                if (idr.HasRows)
                                {
                                    while (idr.Read())
                                    {
                                        upline_id = idr["upline_id"].ToString();
                                        downline_a = idr["Downline_A"].ToString();
                                        downline_b = idr["Downline_B"].ToString();
                                    }
                                }
                                else
                                {
                                    idr.Close();
                                    con.Close();
                                    ShowMessage_warning("Unable to assign member", MessageType.Warning);
                                    return;
                                }

                                idr.Close();
                                con.Close();
                            }

                            if (string.IsNullOrEmpty(upline_id))
                            {
                                ShowMessage_warning("Unable to assign member", MessageType.Warning);
                                return;
                            }

                            // If we found the logged-in user, determine left/right
                            if (upline_id == memberid)
                            {
                                if (downline_a == current_id)
                                {
                                    upline_side = "Left";
                                }
                                else if (downline_b == current_id)
                                {
                                    upline_side = "Right";
                                }
                                else
                                {
                                    ShowMessage_warning("Unable to assign member", MessageType.Warning);
                                    return;
                                }

                                break;
                            }

                            // Move one level up in the tree
                            previous_id = current_id;
                            current_id = upline_id;
                        }

                        #endregion

                        string downline = "";
                        string side = "";

                        using (SqlCommand cmd = new SqlCommand("Get_Member_Default_Assign", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("MemberID", txt_keyinid.Text);

                            con.Open();
                            SqlDataReader idr = cmd.ExecuteReader();

                            if (idr.HasRows == true)
                            {
                                while (idr.Read())
                                {
                                    if (ddl_default_placement.SelectedValue == "Default")
                                    {
                                        if (idr["Default_Assign_Member_Side"].ToString() == "Left")
                                        {
                                            side = "Left";
                                            downline = idr["Downline_A"].ToString();
                                        }
                                        else
                                        {
                                            side = "Right";
                                            downline = idr["Downline_B"].ToString();
                                        }
                                    }
                                    else if (ddl_default_placement.SelectedValue == "Left")
                                    {
                                        side = "Left";
                                        downline = idr["Downline_A"].ToString();
                                    }
                                    else if (ddl_default_placement.SelectedValue == "Right")
                                    {
                                        side = "Right";
                                        downline = idr["Downline_B"].ToString();
                                    }
                                    //if (idr["Default_Assign_Member_Side"].ToString() == "Left")
                                    //{
                                    //    side = "Left";
                                    //    downline = idr["Downline_A"].ToString();
                                    //}
                                    //else
                                    //{
                                    //    side = "Right";
                                    //    downline = idr["Downline_B"].ToString();
                                    //}
                                }
                            }
                            else
                            {
                                ShowMessage_warning("Unable to assign member", MessageType.Warning);
                                return;
                            }

                            idr.Close();
                            con.Close();
                        }
                        if (string.IsNullOrEmpty(downline))
                        {
                            upline = txt_keyinid.Text;
                            Update_Member_Upline(upline, e.CommandArgument.ToString(), side, upline_side);
                        }
                        else
                        {
                            upline = downline;
                            while (!string.IsNullOrEmpty(upline))
                            {
                                using (SqlCommand cmd = new SqlCommand("Get_Member_Default_Assign", con))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("MemberID", upline);

                                    con.Open();
                                    SqlDataReader idr = cmd.ExecuteReader();

                                    if (idr.HasRows == true)
                                    {
                                        while (idr.Read())
                                        {
                                            if (idr["Default_Assign_Member_Side"].ToString() == "Left")
                                            {
                                                side = "Left";
                                                downline = idr["Downline_A"].ToString();
                                            }
                                            else
                                            {
                                                side = "Right";
                                                downline = idr["Downline_B"].ToString();
                                            }
                                        }
                                    }

                                    idr.Close();
                                    con.Close();
                                }

                                if (string.IsNullOrEmpty(downline))
                                {
                                    Update_Member_Upline(upline, e.CommandArgument.ToString(), side, upline_side);
                                    break;
                                }
                                else
                                {
                                    upline = downline;
                                }
                            }
                        }
                    }

                    ShowMessage("Assign Member Successful.", MessageType.Success);
                    Load_Member_To_Be_Assign(Request.Cookies["userid"].Value);
                }
            }
        }

    }

    protected void Update_Member_Upline(string upline, string member_to_assign, string side, string mission_side)
    {
        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("Assign_Member_v2", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Upline", upline.ToUpper());
                cmd.Parameters.AddWithValue("Member_Need_To_Assign", member_to_assign);
                cmd.Parameters.AddWithValue("Side", side);
                cmd.Parameters.AddWithValue("Assign_By", Request.Cookies["userid"].Value);
                cmd.Parameters.AddWithValue("Mission_side", mission_side);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        Assing_Member_Pending_BV(member_to_assign);
    }

    protected void Assing_Member_Pending_BV(string assing_memberid)
    {
        DataTable order_bv = new DataTable();

        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("Load_All_Pending_BV_Order_Details", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Member_ID", assing_memberid);
                con.Open();
                SqlDataReader idr = cmd.ExecuteReader();

                if (idr.HasRows == true)
                {
                    order_bv.Load(idr);
                }

                idr.Close();
                con.Close();
            }
        }

        if (order_bv.Rows.Count > 0)
        {
            foreach (DataRow dr in order_bv.Rows)
            {
                int count = 0;
                string memberid = "";
                string downline = "";

                if (!string.IsNullOrEmpty(dr["UplineID"].ToString()))
                {
                    while (memberid != "-")
                    {
                        using (SqlConnection con = new SqlConnection(ConnectionString))
                        {
                            using (SqlCommand cmd = new SqlCommand("Auto_Insert_Member_Pending_BV_Points", con))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@Order_No", dr["Order_No"].ToString());

                                if (count == 0)
                                    cmd.Parameters.AddWithValue("@Member_ID", dr["MEMBERID"].ToString());
                                else
                                    cmd.Parameters.AddWithValue("@Member_ID", memberid);

                                cmd.Parameters.AddWithValue("@Downline", downline);
                                cmd.Parameters.AddWithValue("@BV_Amount", dr["BV_Amount"].ToString());
                                cmd.Parameters.AddWithValue("@Member_Count", count);
                                SqlParameter UplineParam = new SqlParameter("@Upline_ID", SqlDbType.VarChar, 200);
                                UplineParam.Direction = ParameterDirection.Output;
                                cmd.Parameters.Add(UplineParam);
                                con.Open();
                                cmd.ExecuteNonQuery();

                                if (count == 0)
                                    downline = dr["MEMBERID"].ToString();
                                else
                                    downline = memberid;

                                memberid = cmd.Parameters["@Upline_ID"].Value.ToString();
                                count += 1;
                                con.Close();
                                Thread.Sleep(100); // Add a delay of 1000 milliseconds (1 second)
                            }
                        }
                    }
                }

                count += 1;
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

    protected void txt_keyinid_TextChanged(object sender, EventArgs e)
    {
        // Get the TextBox that raised the event
        TextBox txtKeyInId = (TextBox)sender;

        // Find the RepeaterItem containing this TextBox
        RepeaterItem item = (RepeaterItem)txtKeyInId.NamingContainer;

        TextBox txt_member_name = (TextBox)item.FindControl("txt_member_name");

        // Example: Display the new value of the TextBox
        string key_in_id = txtKeyInId.Text;
        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Member_Name", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("MemberID", key_in_id);

                con.Open();
                SqlDataReader idr = cmd.ExecuteReader();

                if (idr.HasRows == true)
                {
                    while (idr.Read())
                    {
                        txt_member_name.Text = idr["Member_Name"].ToString();
                    }
                }
                else
                {
                    txt_member_name.Text = "Member Not Found !";
                }

                idr.Close();
                con.Close();
            }
        }
    }
}