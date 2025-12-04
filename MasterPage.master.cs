using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected static String ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            bool Page_Type_cookieExists = HttpContext.Current.Request.Cookies["Page_Type"] != null;

            if (Page_Type_cookieExists == true)
            {
                if (Request.Cookies["Page_Type"].Value != null && Request.Cookies["Page_Type"].Value != "")
                {
                    if (Request.Cookies["Page_Type"].Value == "Shopping")
                    {
                        div_shopping_menu.Visible = true;
                        div_myoffice_menu.Visible = false;

                        div_mobile_shopping.Visible = true;
                        div_mobile_office.Visible = false;
                    }
                    else
                    {
                        div_shopping_menu.Visible = false;
                        div_myoffice_menu.Visible = true;

                        div_mobile_shopping.Visible = false;
                        div_mobile_office.Visible = true;
                    }
                }
                else
                {
                    div_shopping_menu.Visible = true;
                    div_myoffice_menu.Visible = false;

                    div_mobile_shopping.Visible = false;
                    div_mobile_office.Visible = true;
                }
            }
            else
            {
                div_shopping_menu.Visible = true;
                div_myoffice_menu.Visible = false;

                div_mobile_shopping.Visible = false;
                div_mobile_office.Visible = true;
            }

            bool cookieExists = HttpContext.Current.Request.Cookies["userid"] != null;

            if (cookieExists == true)
            {
                if (Request.Cookies["userid"].Value != null && Request.Cookies["userid"].Value != "")
                {
                    Load_Member_Details(Request.Cookies["userid"].Value);
                    div_userdetails.Visible = true;
                    div_login.Visible = false;
                }
                else
                {
                    div_userdetails.Visible = false;
                    div_login.Visible = true;
                }
            }
            else
            {
                div_userdetails.Visible = false;
                div_login.Visible = true;
            }

            string currentUrl = HttpContext.Current.Request.Url.AbsoluteUri;

            // Create a Uri object
            Uri uri = new Uri(currentUrl);

            if (Path.GetFileName(uri.LocalPath.Substring(1)) == "Home.aspx")
            {
                div_shopping_menu.Visible = true;
                div_myoffice_menu.Visible = false;

                div_mobile_shopping.Visible = true;
                div_mobile_office.Visible = false;
            }

            if (Path.GetFileName(uri.LocalPath.Substring(1)) == "Register_Member_Product_List.aspx" ||
                Path.GetFileName(uri.LocalPath.Substring(1)) == "Register_Member_Product_Details.aspx" ||
                Path.GetFileName(uri.LocalPath.Substring(1)) == "Register_Member_Checkout.aspx" ||
                Path.GetFileName(uri.LocalPath.Substring(1)) == "Upgrade_To_EO_Product_List.aspx" ||
                Path.GetFileName(uri.LocalPath.Substring(1)) == "Upgrade_To_EO_Product_Details.aspx" ||
                Path.GetFileName(uri.LocalPath.Substring(1)) == "Upgrade_To_EO_Checkout.aspx" ||
                Path.GetFileName(uri.LocalPath.Substring(1)) == "Register_Member_Stockist_Product_List.aspx" ||
                Path.GetFileName(uri.LocalPath.Substring(1)) == "Register_Member_Stockist_Product_Details.aspx" ||
                Path.GetFileName(uri.LocalPath.Substring(1)) == "Register_Member_Stockist_Checkout.aspx" ||
                Path.GetFileName(uri.LocalPath.Substring(1)) == "Mobile_Stockist_Product_Listing.aspx" ||
                Path.GetFileName(uri.LocalPath.Substring(1)) == "Mobile_Stockist_Product_Details.aspx" ||
                Path.GetFileName(uri.LocalPath.Substring(1)) == "Mobile_Stockist_Checkout.aspx")
            {
                footer.Visible = false;
                header.Visible = false;
            }
            else
            {
                footer.Visible = true;
                header.Visible = true;
            }

            LoadCartNo();
            LoadAccountStatus();

        }
    }

    protected void LoadCartNo()
    {
        bool cookieExists = HttpContext.Current.Request.Cookies["userid"] != null;

        if (cookieExists == true)
        {
            if (Request.Cookies["userid"].Value != null && Request.Cookies["userid"].Value != "")
            {
                cartNo.Visible = true;

                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Get_User_CartItem", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("user_id", Request.Cookies["userid"].Value);
                        con.Open();

                        SqlDataReader idr = cmd.ExecuteReader();

                        if (idr.HasRows == true)
                        {
                            DataTable g = new DataTable();
                            g.Load(idr);

                            cartNo.Visible = true;
                            cartNo.InnerText = g.Rows[0]["TotalQty"].ToString();

                            mobile_cart.Visible = true;
                            mobile_cart.InnerText = g.Rows[0]["TotalQty"].ToString();

                            mobile_cart_2.Visible = true;
                            mobile_cart_2.InnerText = g.Rows[0]["TotalQty"].ToString();
                        }
                        else
                        {
                            cartNo.Visible = false;
                            cartNo.InnerText = "0";

                            mobile_cart.Visible = false;
                            mobile_cart.InnerText = "0";

                            mobile_cart_2.Visible = false;
                            mobile_cart_2.InnerText = "0";
                        }
                        con.Close();
                    }
                }
            }
            else
            {
                cartNo.Visible = false;
            }
        }
        else
        {
            cartNo.Visible = false;
        }
    }

    private void LoadAccountStatus()
    {
        using (SqlConnection con = new SqlConnection(ConnectionString))
        using (SqlCommand cmd = new SqlCommand("Load_Netwotk_Tree_Member_Details", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Memberid", Request.Cookies["userid"].Value);

            con.Open();
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.Read())
            {
                string A = idr["Downline_A"].ToString().Trim();
                string B = idr["Downline_B"].ToString().Trim();

                if (!string.IsNullOrEmpty(A)) lbl_OpenA.Visible = false;
                if (!string.IsNullOrEmpty(B)) lbl_OpenB.Visible = false;
            }
        }
    }

    protected void Load_Member_Details(string memberid)
    {
        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("Load_Member_Details", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Memberid", memberid);
                con.Open();
                SqlDataReader idr = cmd.ExecuteReader();

                if (idr.HasRows == true)
                {
                    while (idr.Read())
                    {
                        if(idr["Status"].ToString() == "Active")
                        {
                            lbl_341.Visible = false;
                            lbl_356.Visible = true;
                            li_activate_account.Visible = false;
                        }
                        if (idr["Purchase_New_Account"].ToString() == "N")
                        {
                            lbl_344.Visible = false;
                            li_open_new_account.Visible = false;
                        }

                        if (idr["Success_Purchase_New_Account"].ToString() == "Y")
                        {
                            lbl_344.Visible = false;
                            li_open_new_account.Visible = false;
                        }

                        if (idr["AgentLevel"].ToString() == "Shopper" && idr["Upgrade_To_BO_Package_Expired_Date"].ToString() != null)
                        {
                            if (Convert.ToDateTime(idr["Upgrade_To_BO_Package_Expired_Date"].ToString()) > DateTime.UtcNow.AddHours(8))
                            {
                                lbl_354.Visible = true;
                                li_upgrade_bo.Visible = true;
                            }
                        }

                        if (!string.IsNullOrEmpty(idr["First_Name"].ToString()))
                        {
                            lbl_username.InnerText = idr["First_Name"].ToString() + " (ID : " + idr["cardno"].ToString() + ")";

                        }
                        else
                        {
                            lbl_username.InnerText = idr["Company_Name"].ToString() + " (ID : " + idr["cardno"].ToString() + ")";

                        }


                        if (idr["Status"].ToString() == "Active" && idr["Member_Category"].ToString() == "BO" && !(idr["cardno"].ToString().EndsWith("A") || idr["cardno"].ToString().EndsWith("B")))
                        {
                            lbl_356.Visible = true;
                            li_upgrade_eo.Visible = true;
                        }

                        if (idr["Status"].ToString() == "Active" && idr["Member_Category"].ToString() == "EO" && idr["Minimum_Package"].ToString() != "9" && !(idr["cardno"].ToString().EndsWith("A") || idr["cardno"].ToString().EndsWith("B")))
                        {
                            lbl_356.Visible = true;
                            li_upgrade_eo.Visible = true;
                        }

                        if (idr["Member_Category"].ToString() == "BO")
                        {
                            if (idr["Profit_Center"].ToString() == "1")
                            {
                                ddl_profit_center.Items.Clear();
                                ddl_profit_center.Items.Add(new ListItem("Entrepreneur Owner (2,000 BV)", "EO1"));
                                /*ddl_profit_center.Items.Add(new ListItem("1 x EO & 2 x BO (5,000BV)", "EO5"));
                                ddl_profit_center.Items.Add(new ListItem("3 x EO (9,000BV)", "EO9"));*/
                            }
                            else if (idr["Profit_Center"].ToString() == "3")
                            {
                                ddl_profit_center.Items.Clear();
                                ddl_profit_center.Items.Add(new ListItem("1 x EO & 2 x BO (5,000BV)", "EO5"));
                                ddl_profit_center.Items.Add(new ListItem("3 x EO (9,000BV)", "EO9"));
                            }
                        }
                        else if (idr["Member_Category"].ToString() == "EO")
                        {
                            if (idr["Profit_Center"].ToString() == "1")
                            {
                                ddl_profit_center.Items.Clear();
                                ddl_profit_center.Items.Add(new ListItem("1 x EO & 2 x BO (5,000BV)", "EO5"));
                                ddl_profit_center.Items.Add(new ListItem("3 x EO (9,000BV)", "EO9"));
                            }
                            else if (idr["Profit_Center"].ToString() == "3" && idr["Minimum_Package"].ToString() == "5")
                            {
                                ddl_profit_center.Items.Clear();
                                ddl_profit_center.Items.Add(new ListItem("3 x EO (9,000BV)", "EO9"));
                            }
                        }

                        if (idr["Mobile_Stockist"].ToString() == "Y")
                        {
                            div_mobile_stockist.Visible = true;
                            li_mobile_stockist.Visible = true;
                        }
                    }
                }

                idr.Close();
                con.Close();

            }
        }
    }

    protected void btn_Product_Click(object sender, EventArgs e)
    {
        bool cookieExists = HttpContext.Current.Request.Cookies["userid"] != null;

        if (cookieExists == true)
        {
            if (Request.Cookies["userid"].Value != null && Request.Cookies["userid"].Value != "")
            {
                Response.Cookies["Page_Type"].Value = "Shopping";
                Response.Redirect("Product.aspx");
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

    protected void btn_Launch_Click(object sender, EventArgs e)
    {
        bool cookieExists = HttpContext.Current.Request.Cookies["userid"] != null;

        if (cookieExists == true)
        {
            if (Request.Cookies["userid"].Value != null && Request.Cookies["userid"].Value != "")
            {
                Response.Cookies["Page_Type"].Value = "Shopping";
                Response.Redirect("Dermafirm_Introduction.aspx");
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

    protected void btn_My_Office_Click(object sender, EventArgs e)
    {
        bool cookieExists = HttpContext.Current.Request.Cookies["userid"] != null;

        if (cookieExists == true)
        {
            if (Request.Cookies["userid"].Value != null && Request.Cookies["userid"].Value != "")
            {
                Response.Cookies["Page_Type"].Value = "MyOffice";
                Response.Redirect("MyProfile.aspx");
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

    protected void btn_confirm_change_bo_level_Click(object sender, EventArgs e)
    {
        bool cookieExists = HttpContext.Current.Request.Cookies["userid"] != null;

        if (cookieExists == true)
        {
            if (Request.Cookies["userid"].Value != null && Request.Cookies["userid"].Value != "")
            {
                Response.Redirect("Upgrade_To_EO_Product_List.aspx?id=" + Request.Cookies["userid"].Value + "&Agent_Level=EO&Package=" + ddl_profit_center.SelectedValue);
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
