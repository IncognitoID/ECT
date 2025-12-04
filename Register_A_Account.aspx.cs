using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Register_A_Account : System.Web.UI.Page
{
    string id = "";
    string CheckPackage = "";
    public enum MessageType { Success, Error, Info, Warning };
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bool cookieExists = HttpContext.Current.Request.Cookies["userid"] != null;
            if (cookieExists == true)
            {
                if (Request.Cookies["userid"].Value != null && Request.Cookies["userid"].Value != "")
                {
                    btn_verify.Visible = false;
                    btn_remove.Visible = true;
                    GetReferal(Request.Cookies["userid"].Value);
                    //Load_All_Downline(Request.Cookies["userid"].Value);
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

            bool uplineid_cookieExists = HttpContext.Current.Request.Cookies["uplineid"] != null;
            if (uplineid_cookieExists == true)
            {
                if (Request.Cookies["uplineid"].Value != null && Request.Cookies["uplineid"].Value != "")
                {
                    btn_placement.Checked = true;
                    ddl_member_placement.SelectedValue = "KeyinID";
                    txt_keyinid.Text = Request.Cookies["uplineid"].Value;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "Member_Type('Personal');", true);
                }
            }
            btn_shopper.Checked = false;

            GetPolicy();
            Load_State();
            Load_Nationality();
            Load_Country();

            if (!string.IsNullOrEmpty(Request.QueryString["id"].ToString()))
            {
                Load_Member_Details(Request.QueryString["id"].ToString());
            }
        }
    }

    protected void Load_Member_Details(string memberid)
    {
        using (SqlCommand cmd = new SqlCommand("Load_Registration_Member_Details", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Memberid", memberid);
            con.Open();
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows == true)
            {
                while (idr.Read())
                {
                    txt_referal_id.Value = idr["referal_id"].ToString();
                    if (!string.IsNullOrEmpty(idr["Referal_Name"].ToString()))
                    {
                        txt_referal_name.Value = idr["Referal_Name"].ToString();
                    }
                    else
                    {
                        txt_referal_name.Value = idr["Referal_Company_Name"].ToString();
                    }
                    ddl_gender.Value = idr["Gender"].ToString();
                    txt_phonenumber.Value = idr["Contact_No"].ToString();
                    txt_email.Value = idr["Email"].ToString();
                    ddl_nationality.SelectedValue = idr["Nationality"].ToString();
                    ddl_country.SelectedValue = idr["Country"].ToString();
                    ddl_State.SelectedValue = idr["State"].ToString();
                    ddl_package.Value = idr["Package_Value"].ToString();
                    txt_city.Value = idr["City"].ToString();
                    txt_address1.Value = idr["Add1"].ToString();
                    txt_address2.Value = idr["Add2"].ToString();
                    txt_postcode.Value = idr["Postcode"].ToString();

                    #region Decrypt Password
                    string member_password = "";
                    member_password = idr["password"].ToString();
                    byte[] encryptedBytes = Convert.FromBase64String(member_password);
                    string encryptionKey = "Ecentra207007PASSWORD888"; // Replace with your encryption key
                    byte[] keyBytes = Encoding.UTF8.GetBytes(encryptionKey);

                    byte[] iv = new byte[16];
                    byte[] cipherText = new byte[encryptedBytes.Length - iv.Length];

                    Array.Copy(encryptedBytes, 0, iv, 0, iv.Length);
                    Array.Copy(encryptedBytes, iv.Length, cipherText, 0, cipherText.Length);

                    using (Aes aesAlg = Aes.Create())
                    {
                        aesAlg.Key = keyBytes;
                        aesAlg.IV = iv;

                        ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                        using (var msDecrypt = new System.IO.MemoryStream())
                        {
                            using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Write))
                            {
                                csDecrypt.Write(cipherText, 0, cipherText.Length);
                                csDecrypt.FlushFinalBlock();

                                byte[] decryptedBytes = msDecrypt.ToArray();
                                string decryptedText = Encoding.UTF8.GetString(decryptedBytes);
                                txt_password.Attributes["type"] = "text"; // Change the input type to text temporarily
                                txt_confirmpassword.Attributes["type"] = "text"; // Change the input type to text temporarily
                                txt_password.Value = decryptedText;
                                txt_confirmpassword.Value = decryptedText;
                                txt_password.Attributes["type"] = "password";
                                txt_confirmpassword.Attributes["type"] = "password";
                            }
                        }
                    }
                    #endregion

                    foreach (RepeaterItem item in rpt_policy.Items)
                    {
                        HtmlInputCheckBox chkPolicy = item.FindControl("chkPolicy") as HtmlInputCheckBox;
                        chkPolicy.Checked = true;   
                    }

                    if (idr["Binary_Placement_Yes_No"].ToString() == "Y")
                    {
                        btn_placement.Checked = true;
                        ddl_member_placement.SelectedValue = idr["Binary_Placement"].ToString();
                        txt_keyinid.Text = idr["Binary_Placement_Key_In_ID"].ToString();
                        ddl_default_placement.SelectedValue = idr["Binary_Placement_Left_or_Right"].ToString();
                    }

                    if (idr["Shopper"].ToString() == "Y")
                    {
                        btn_shopper.Checked = true;
                    }

                    if (idr["Member_Type"].ToString() == "Personal")
                    {
                        txt_fullname.Value = idr["First_Name"].ToString();
                        txt_nric.Text = idr["NRIC"].ToString();
                        txt_passport.Text = idr["Passport"].ToString();
                        divNameVisible.Value = "true";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "Member_Type('Personal');", true);
                    }
                    else
                    {
                        txt_companyname.Value = idr["Company_Name"].ToString();
                        txt_companyno.Value = idr["Company_No"].ToString();
                        divNameVisible.Value = "false";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "Member_Type('Company');", true);
                    }

                }
            }

            idr.Close();
            con.Close();

        }
    }

    protected void Load_State()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("Load_State", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CountryCode", "%" +  ddl_country.SelectedValue + "%");
                con.Open();
                SqlDataReader idr = cmd.ExecuteReader();

                if (idr.HasRows == true)
                {
                    ddl_State.Items.Clear();
                    DataTable v = new DataTable();

                    v.Load(idr);
                    ddl_State.DataSource = v;
                    ddl_State.DataTextField = "Statename";
                    ddl_State.DataValueField = "Statecode";
                    ddl_State.DataBind();

                    string value = "";
                    bool cookieExists = HttpContext.Current.Request.Cookies["language"] != null;
                    if (cookieExists == true)
                    {
                        if (Request.Cookies["language"].Value != null && Request.Cookies["language"].Value != "")
                        {
                            if (Request.Cookies["language"].Value == "Chinese")
                            {
                                value = "选择您的州属";
                            }
                            else
                            {
                                value = "Select your state";
                            }
                        }
                        else
                        {
                            value = "Select your state";
                        }
                    }
                    else
                    {
                        value = "Select your state";
                    }

                    ListItem defaultItem = new ListItem(value, "");
                    defaultItem.Attributes["disabled"] = "disabled";
                    defaultItem.Selected = true; // Set the default item as selected
                    ddl_State.Items.Insert(0, defaultItem);
                }
                else
                {
                    string value = "";
                    bool cookieExists = HttpContext.Current.Request.Cookies["language"] != null;
                    if (cookieExists == true)
                    {
                        if (Request.Cookies["language"].Value != null && Request.Cookies["language"].Value != "")
                        {
                            if (Request.Cookies["language"].Value == "Chinese")
                            {
                                value = "选择您的州属";
                            }
                            else
                            {
                                value = "Select your state";
                            }
                        }
                        else
                        {
                            value = "Select your state";
                        }
                    }
                    else
                    {
                        value = "Select your state";
                    }

                    ddl_State.Items.Clear();
                    ListItem defaultItem = new ListItem(value, "");
                    ddl_State.Items.Insert(0, defaultItem);
                }

                    idr.Close();
                con.Close();
            }
        }
    }

    protected void Load_Nationality()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("Load_Nationality", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader idr = cmd.ExecuteReader();

                if (idr.HasRows == true)
                {
                    DataTable v = new DataTable();

                    v.Load(idr);

                    ddl_nationality.DataSource = v;
                    ddl_nationality.DataTextField = "Nationality_Name";
                    ddl_nationality.DataValueField = "Nationality_Code";
                    ddl_nationality.DataBind();

                    string value = "";
                    bool cookieExists = HttpContext.Current.Request.Cookies["language"] != null;
                    if (cookieExists == true)
                    {
                        if (Request.Cookies["language"].Value != null && Request.Cookies["language"].Value != "")
                        {
                            if(Request.Cookies["language"].Value == "Chinese")
                            {
                                value = "选择您的国籍";
                            }
                            else
                            {
                                value = "Select your nationality";
                            }
                        }
                        else
                        {
                            value = "Select your nationality";
                        }
                    }
                    else
                    {
                        value = "Select your nationality";
                    }

                    ListItem defaultItem = new ListItem(value, "");
                    defaultItem.Attributes["disabled"] = "disabled";
                    defaultItem.Selected = true; // Set the default item as selected
                    ddl_nationality.Items.Insert(0, defaultItem);
                }

                idr.Close();
                con.Close();
            }
        }
    }

    protected void Load_Country()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("Load_Country", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader idr = cmd.ExecuteReader();

                if (idr.HasRows == true)
                {
                    DataTable v = new DataTable();

                    v.Load(idr);

                    ddl_country.DataSource = v;
                    ddl_country.DataTextField = "Name";
                    ddl_country.DataValueField = "Code";
                    ddl_country.DataBind();

                    string value = "";
                    bool cookieExists = HttpContext.Current.Request.Cookies["language"] != null;
                    if (cookieExists == true)
                    {
                        if (Request.Cookies["language"].Value != null && Request.Cookies["language"].Value != "")
                        {
                            if (Request.Cookies["language"].Value == "Chinese")
                            {
                                value = "选择您的国家";
                            }
                            else
                            {
                                value = "Select your country";
                            }
                        }
                        else
                        {
                            value = "Select your country";
                        }
                    }
                    else
                    {
                        value = "Select your country";
                    }

                    ListItem defaultItem = new ListItem(value, "");
                    defaultItem.Attributes["disabled"] = "disabled";
                    defaultItem.Selected = true; // Set the default item as selected
                    ddl_country.Items.Insert(0, defaultItem);
                }

                idr.Close();
                con.Close();
            }
        }
    }

    protected void GetReferal(string id)
    {
        using (SqlCommand cmd = new SqlCommand("SELECT First_Name, Company_Name, Default_Assign_Member_Side FROM MF_CRM_Members WHERE cardno = @userid", con))
        {
            cmd.Parameters.AddWithValue("@userid", id);
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    txt_referal_id.Value = id;
                    if(!string.IsNullOrEmpty(sdr["First_Name"].ToString()))
                    {
                        txt_referal_name.Value = sdr["First_Name"].ToString();
                    }
                    else
                    {
                        txt_referal_name.Value = sdr["Company_Name"].ToString();
                    }
                    txt_referal_id.Disabled = true;
                    txt_referal_name.Disabled = true;

                    ddl_member_placement.SelectedValue = sdr["Default_Assign_Member_Side"].ToString(); 
                }
            }
            con.Close();
        }
    }

    //protected void Load_All_Downline(string memberid)
    //{
    //    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
    //    {
    //        using (SqlCommand cmd = new SqlCommand("Load_Member_Downline", con))
    //        {
    //            cmd.CommandType = CommandType.StoredProcedure;
    //            cmd.Parameters.AddWithValue("@Memberid", memberid);
    //            con.Open();
    //            SqlDataReader idr = cmd.ExecuteReader();

    //            if (idr.HasRows == true)
    //            {
    //                ddl_downline.Items.Clear();
    //                DataTable v = new DataTable();

    //                v.Load(idr);
    //                ddl_downline.DataSource = v;
    //                ddl_downline.DataTextField = "Username";
    //                ddl_downline.DataValueField = "Member_ID";
    //                ddl_downline.DataBind();

    //                ListItem newItem = new ListItem("Select Downline", "");
    //                ddl_downline.Items.Insert(0, newItem);
    //                ddl_downline.Items[0].Attributes.Add("disabled", "disabled");
    //                ddl_downline.SelectedValue = "";

    //            }

    //            idr.Close();
    //            con.Close();
    //        }
    //    }
    //}

    protected void GetPolicy()
    {
        SqlCommand cmd = new SqlCommand("SELECT TOP 1 * FROM Website_Policy WHERE Ids = '5'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable v = new DataTable();
        da.Fill(v);

        con.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        if (reader.HasRows)
        {
            rpt_policy.DataSource = reader;
            rpt_policy.DataBind();
        }
        reader.Close();
        con.Close();
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

    protected void Register_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowLoading();", true);

        string pattern = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";

        System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(txt_email.Value, pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

        if (string.IsNullOrEmpty(txt_referal_id.Value))
        {
            ShowMessage_warning("Please key in referral ID.", MessageType.Warning);
            return;
        }
        else if (string.IsNullOrEmpty(txt_referal_name.Value))
        {
            ShowMessage_warning("Please key in correct referral ID.", MessageType.Warning);
            return;
        }
        else if (string.IsNullOrEmpty(ddl_nationality.SelectedValue))
        {
            ShowMessage_warning("Please select your nationality", MessageType.Warning);
            return;
        }
        else if (string.IsNullOrEmpty(ddl_country.SelectedValue))
        {
            ShowMessage_warning("Please select your country", MessageType.Warning);
            return;
        }
        else if (string.IsNullOrEmpty(ddl_State.SelectedValue))
        {
            ShowMessage_warning("Please select your state", MessageType.Warning);
            return;
        }
        else if (string.IsNullOrEmpty(txt_phonenumber.Value))
        {
            ShowMessage_warning("Please key in your mobile number", MessageType.Warning);
            return;
        }
        else if (string.IsNullOrEmpty(txt_email.Value))
        {
            ShowMessage_warning("Please key in your email", MessageType.Warning);
            return;
        }
        else if (string.IsNullOrEmpty(txt_city.Value))
        {
            ShowMessage_warning("Please key in your city", MessageType.Warning);
            return;
        }
        else if (string.IsNullOrEmpty(txt_address1.Value))
        {
            ShowMessage_warning("Please key in your address", MessageType.Warning);
            return;
        }
        else if (string.IsNullOrEmpty(txt_postcode.Value))
        {
            ShowMessage_warning("Please key in your postcode", MessageType.Warning);
            return;
        }
        else if (txt_email.Value != "" && !match.Success)
        {
            ShowMessage_warning("Please enter a valid email address", MessageType.Warning);
            txt_email.Focus();
        }
        else if (string.IsNullOrEmpty(ddl_package.Value))
        {
            ShowMessage_warning("Please select your profit center", MessageType.Warning);
            return;
        }
        else if (btn_placement.Checked != true)
        {
            ShowMessage_warning("Please set the binary placement", MessageType.Warning);
            return;
        }
        else if (btn_placement.Checked == true && ddl_member_placement.SelectedValue == "KeyinID" && string.IsNullOrEmpty(txt_keyinid.Text))
        {
            ShowMessage_warning("Please key in downline member id", MessageType.Warning);
            return;
        }
        else
        {
            string Type = "Personal";
            foreach (RepeaterItem item in rpt_policy.Items)
            {
                HtmlInputCheckBox chkPolicy = item.FindControl("chkPolicy") as HtmlInputCheckBox;
                if (!chkPolicy.Checked)
                {
                    ShowMessage_warning("Please check all the T&C.", MessageType.Warning);
                    return;
                }
            }
            if (divNameVisible.Value == "true")
            {
                if (string.IsNullOrEmpty(txt_fullname.Value))
                {
                    ShowMessage_warning("Please key in your name.", MessageType.Warning);
                    return;
                }
                else if (ddl_type.SelectedValue == "NRIC" && string.IsNullOrEmpty(txt_nric.Text))
                {
                    ShowMessage_warning("Please key in your NRIC number.", MessageType.Warning);
                    return;
                }
                else if (ddl_type.SelectedValue == "Passport" && string.IsNullOrEmpty(txt_passport.Text))
                {
                    ShowMessage_warning("Please key in your Passport number.", MessageType.Warning);
                    return;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(txt_companyname.Value))
                {
                    ShowMessage_warning("Please key in your company name.", MessageType.Warning);
                    return;
                }
                else if (string.IsNullOrEmpty(txt_companyno.Value))
                {
                    ShowMessage_warning("Please key in your company number.", MessageType.Warning);
                    return;
                }
                Type = "Company";
            }

            if (string.IsNullOrEmpty(txt_password.Value) || !Regex.IsMatch(txt_confirmpassword.Value, @"^[A-Za-z0-9]{6,}$"))
            {
                ShowMessage_warning("Password should be a minimum of 6 characters", MessageType.Warning);
                return;
            }
            else if (txt_password.Value != txt_confirmpassword.Value)
            {
                ShowMessage_warning("Passwords not match.", MessageType.Warning);
                return;
            }
            else
            {
                if (divNameVisible.Value == "true")
                {
                    txt_companyname.Value = "";
                    txt_companyno.Value = "";
                }
                else
                {
                    txt_fullname.Value = "";
                    txt_nric.Text = "";
                    txt_passport.Text = "";
                }

                string upline = "";
                string side = "";

                if (btn_placement.Checked == true && ddl_member_placement.SelectedValue == "Left")
                {
                    string default_assign_member_id = "";
                    string downline = "";

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
                        side = "Left";
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
                                break;
                            }
                            else
                            {
                                upline = downline;
                            }
                        }
                    }
                }

                if (btn_placement.Checked == true && ddl_member_placement.SelectedValue == "Right")
                {
                    string default_assign_member_id = "";
                    string downline = "";

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
                        side = "Right";
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
                                break;
                            }
                            else
                            {
                                upline = downline;
                            }
                        }
                    }
                }

                if (btn_placement.Checked == true && ddl_member_placement.SelectedValue == "Always Left")
                {
                    string default_assign_member_id = "";
                    string downline = "";

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
                        side = "Left";
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
                                break;
                            }
                            else
                            {
                                upline = downline;
                            }
                        }
                    }
                }

                if (btn_placement.Checked == true && ddl_member_placement.SelectedValue == "Always Right")
                {
                    string default_assign_member_id = "";
                    string downline = "";

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
                        side = "Right";
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
                                break;
                            }
                            else
                            {
                                upline = downline;
                            }
                        }
                    }
                }

                if (btn_placement.Checked == true && ddl_member_placement.SelectedValue == "KeyinID" && !string.IsNullOrEmpty(txt_keyinid.Text))
                {
                    #region Check Key in ID Under Me

                    //if (ddl_member_placement.SelectedValue == "KeyinID")
                    //{
                    //    using (SqlCommand cmd = new SqlCommand("Check_Key_In_ID_Valid", con))
                    //    {
                    //        cmd.CommandType = CommandType.StoredProcedure;
                    //        cmd.Parameters.AddWithValue("Key_In_ID", txt_keyinid.Text);
                    //        cmd.Parameters.AddWithValue("My_MemberID", Request.Cookies["userid"].Value);

                    //        con.Open();
                    //        SqlDataReader idr = cmd.ExecuteReader();

                    //        if (idr.HasRows == false)
                    //        {
                    //            ShowMessage_warning("User not found", MessageType.Warning);
                    //            return;
                    //        }

                    //        idr.Close();
                    //        con.Close();
                    //    }
                    //}

                    #endregion

                    string downline = "";

                    bool uplineid_cookieExists = HttpContext.Current.Request.Cookies["side"] != null;
                    if (uplineid_cookieExists == true)
                    {
                        if (Request.Cookies["side"].Value != null && Request.Cookies["side"].Value != "")
                        {
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
                                        if(Request.Cookies["side"].Value == "Left")
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
                                else
                                {
                                    ShowMessage_warning("Unable to assign member", MessageType.Warning);
                                    return;
                                }

                                idr.Close();
                                con.Close();
                            }
                        }
                        else
                        {
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
                        }
                    }
                    else
                    {
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
                    }

                    if (string.IsNullOrEmpty(downline))
                    {
                        upline = txt_keyinid.Text;
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
                                break;
                            }
                            else
                            {
                                upline = downline;
                            }
                        }
                    }
                }

                #region Encrypt

                string encryptdata = "";
                // Combine memberid and todaydate into a single string
                string data = txt_confirmpassword.Value.Trim();

                // Encrypt the data
                string encryptionKey = "Ecentra207007PASSWORD888"; // Replace with your encryption key
                byte[] keyBytes = Encoding.UTF8.GetBytes(encryptionKey);
                byte[] textBytes = Encoding.UTF8.GetBytes(data);

                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = keyBytes;
                    aesAlg.GenerateIV();

                    ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                    using (var msEncrypt = new System.IO.MemoryStream())
                    {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            csEncrypt.Write(textBytes, 0, textBytes.Length);
                            csEncrypt.FlushFinalBlock();

                            byte[] encryptedBytes = msEncrypt.ToArray();

                            byte[] resultBytes = new byte[aesAlg.IV.Length + encryptedBytes.Length];
                            Array.Copy(aesAlg.IV, 0, resultBytes, 0, aesAlg.IV.Length);
                            Array.Copy(encryptedBytes, 0, resultBytes, aesAlg.IV.Length, encryptedBytes.Length);

                            encryptdata = Convert.ToBase64String(resultBytes);
                        }
                    }
                }

                #endregion

                if (!string.IsNullOrEmpty(Request.QueryString["id"].ToString()))
                {
                    Update_Member_Details(Request.QueryString["id"].ToString());
                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand("Insert_New_Member_Temporarily", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("Type", Type);
                        cmd.Parameters.AddWithValue("Action_By", Request.Cookies["userid"].Value);
                        cmd.Parameters.AddWithValue("Member_ID", "");
                        cmd.Parameters.AddWithValue("Referal_ID", txt_referal_id.Value.Trim());
                        cmd.Parameters.AddWithValue("Phone_Number", txt_phonenumber.Value);
                        cmd.Parameters.AddWithValue("Full_Name", txt_fullname.Value.Trim());
                        cmd.Parameters.AddWithValue("Company_Name", txt_companyname.Value.Trim());
                        cmd.Parameters.AddWithValue("Company_No", txt_companyno.Value.Trim());
                        cmd.Parameters.AddWithValue("Email", txt_email.Value.Trim());
                        cmd.Parameters.AddWithValue("Nationality", ddl_nationality.SelectedValue);
                        cmd.Parameters.AddWithValue("Gender", ddl_gender.Value);
                        cmd.Parameters.AddWithValue("Add1", txt_address1.Value.Trim());
                        cmd.Parameters.AddWithValue("Add2", txt_address2.Value.Trim());
                        cmd.Parameters.AddWithValue("Postcode", txt_postcode.Value.Trim());
                        cmd.Parameters.AddWithValue("City", txt_city.Value.Trim());
                        cmd.Parameters.AddWithValue("Country", ddl_country.SelectedValue);
                        if (ddl_country.SelectedValue == "MY")
                        {
                            cmd.Parameters.AddWithValue("State", ddl_State.SelectedValue);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("State", ddl_State.SelectedValue);
                        }
                        cmd.Parameters.AddWithValue("Password", encryptdata);
                        if (ddl_type.SelectedValue == "NRIC")
                        {
                            cmd.Parameters.AddWithValue("NRIC", txt_nric.Text);
                            cmd.Parameters.AddWithValue("Passport", "");

                        }
                        else if (ddl_type.SelectedValue == "Passport")
                        {
                            cmd.Parameters.AddWithValue("NRIC", "");
                            cmd.Parameters.AddWithValue("Passport", txt_passport.Text);
                        }

                        if (btn_placement.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("Binary_Placement", "Y");
                            cmd.Parameters.AddWithValue("Upline", upline);
                            cmd.Parameters.AddWithValue("Side", side);
                            cmd.Parameters.AddWithValue("Placement_Value", ddl_member_placement.SelectedValue);
                            if (ddl_member_placement.SelectedValue == "KeyinID")
                            {
                                cmd.Parameters.AddWithValue("Placement_Key_In_ID", txt_keyinid.Text);
                                cmd.Parameters.AddWithValue("Placement_Left_or_Right", ddl_default_placement.SelectedValue);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("Placement_Key_In_ID", "");
                                cmd.Parameters.AddWithValue("Placement_Left_or_Right", "");
                            }
                        }
                        else if (btn_placement.Checked == false)
                        {
                            cmd.Parameters.AddWithValue("Binary_Placement", "N");
                            cmd.Parameters.AddWithValue("Upline", "");
                            cmd.Parameters.AddWithValue("Side", "");
                            cmd.Parameters.AddWithValue("Placement_Value", "");
                            cmd.Parameters.AddWithValue("Placement_Key_In_ID", "");
                            cmd.Parameters.AddWithValue("Placement_Left_or_Right", "");
                        }

                        if (btn_shopper.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("Shopper", "Y");
                            cmd.Parameters.AddWithValue("Profit_Center", "1");
                            cmd.Parameters.AddWithValue("Agent_Level", "Shopper");
                            cmd.Parameters.AddWithValue("Member_Category", "BO");
                            cmd.Parameters.AddWithValue("Minimum_Package", "1");
                            cmd.Parameters.AddWithValue("Package_Value", "1");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("Shopper", "N");
                            cmd.Parameters.AddWithValue("Agent_Level", "BO");
                            if(ddl_package.Value == "1" || ddl_package.Value == "3")
                            {
                                cmd.Parameters.AddWithValue("Member_Category", "BO");
                                cmd.Parameters.AddWithValue("Profit_Center", ddl_package.Value);
                                cmd.Parameters.AddWithValue("Minimum_Package", ddl_package.Value);
                                cmd.Parameters.AddWithValue("Package_Value", ddl_package.Value);
                            }
                            else
                            {
                                if(ddl_package.Value == "EO1")
                                {
                                    cmd.Parameters.AddWithValue("Profit_Center", "1");
                                    cmd.Parameters.AddWithValue("Minimum_Package", "3");
                                    cmd.Parameters.AddWithValue("Package_Value", "EO1");
                                }
                                else if (ddl_package.Value == "EO5")
                                {
                                    cmd.Parameters.AddWithValue("Profit_Center", "3");
                                    cmd.Parameters.AddWithValue("Minimum_Package", "5");
                                    cmd.Parameters.AddWithValue("Package_Value", "EO5");
                                }
                                else if (ddl_package.Value == "EO9")
                                {
                                    cmd.Parameters.AddWithValue("Profit_Center", "3");
                                    cmd.Parameters.AddWithValue("Minimum_Package", "9");
                                    cmd.Parameters.AddWithValue("Package_Value", "EO9");
                                }
                                cmd.Parameters.AddWithValue("Member_Category", "EO");
                            }
                        }

                        // Set Output Paramater
                        SqlParameter ReferalParam = new SqlParameter("@ReferalExist", SqlDbType.Bit);
                        ReferalParam.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(ReferalParam);

                        // Set Output Paramater
                        SqlParameter NRICParam = new SqlParameter("@NRICExist", SqlDbType.Bit);
                        NRICParam.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(NRICParam);

                        // Set Output Paramater
                        SqlParameter Passportaram = new SqlParameter("@PassportExist", SqlDbType.Bit);
                        Passportaram.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(Passportaram);

                        // Set Output Paramater
                        SqlParameter MaximumIdentityparam = new SqlParameter("@MaximumIdentityExist", SqlDbType.Bit);
                        MaximumIdentityparam.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(MaximumIdentityparam);

                        // Set Output Paramater
                        SqlParameter UseridParam = new SqlParameter("@Userid", SqlDbType.VarChar, 200);
                        UseridParam.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(UseridParam);

                        // Set Output Paramater
                        SqlParameter StatusParam = new SqlParameter("@Status", SqlDbType.VarChar, 200);
                        StatusParam.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(StatusParam);

                        con.Open();
                        cmd.ExecuteNonQuery();

                        bool NRICExist = Convert.ToBoolean(cmd.Parameters["@NRICExist"].Value);
                        bool PassportExist = Convert.ToBoolean(cmd.Parameters["@PassportExist"].Value);
                        bool MaximumIdentityExist = Convert.ToBoolean(cmd.Parameters["@MaximumIdentityExist"].Value);
                        bool ReferalExists = Convert.ToBoolean(cmd.Parameters["@ReferalExist"].Value);
                        string userid = cmd.Parameters["@Userid"].Value.ToString();
                        string StatusExists = cmd.Parameters["@Status"].Value.ToString();

                        con.Close();

                        if (StatusExists == "success")
                        {
                            Response.Cookies["Profit_Center"].Value = ddl_package.Value;
                            Response.Redirect("Register_Member_Product_List.aspx?id=" + userid);
                        }
                        else if (StatusExists == "failed")
                        {
                            if (NRICExist == true)
                            {
                                ShowMessage_warning("NRIC already registered, please use another NRIC to register", MessageType.Warning);
                                txt_nric.Focus();
                            }
                            else if (PassportExist == true)
                            {
                                ShowMessage_warning("Passport already registered, please use another Passport to register", MessageType.Warning);
                                txt_passport.Focus();
                            }
                            else if (ReferalExists == false)
                            {
                                ShowMessage_warning("Referral ID not found!", MessageType.Warning);
                                txt_referal_id.Focus();
                            }
                        }
                        con.Close();
                    }
                }

            }
        }
    }

    protected void Update_Member_Details(string member_id)
    {
        string Type = "Personal";
        if (divNameVisible.Value == "true")
        {
            txt_companyname.Value = "";
            txt_companyno.Value = "";
        }
        else
        {
            txt_fullname.Value = "";
            txt_nric.Text = "";
            txt_passport.Text = "";
            Type = "Company";
        }

        string upline = "";
        string side = "";

        if (btn_placement.Checked == true && ddl_member_placement.SelectedValue == "Left")
        {
            string default_assign_member_id = "";
            string downline = "";

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
                side = "Left";
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
                        break;
                    }
                    else
                    {
                        upline = downline;
                    }
                }
            }
        }

        if (btn_placement.Checked == true && ddl_member_placement.SelectedValue == "Right")
        {
            string default_assign_member_id = "";
            string downline = "";

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
                side = "Right";
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
                        break;
                    }
                    else
                    {
                        upline = downline;
                    }
                }
            }
        }

        if (btn_placement.Checked == true && ddl_member_placement.SelectedValue == "KeyinID" && !string.IsNullOrEmpty(txt_keyinid.Text))
        {
            #region Check Key in ID Under Me

            //if (ddl_member_placement.SelectedValue == "KeyinID")
            //{
            //    using (SqlCommand cmd = new SqlCommand("Check_Key_In_ID_Valid", con))
            //    {
            //        cmd.CommandType = CommandType.StoredProcedure;
            //        cmd.Parameters.AddWithValue("Key_In_ID", txt_keyinid.Text);
            //        cmd.Parameters.AddWithValue("My_MemberID", Request.Cookies["userid"].Value);

            //        con.Open();
            //        SqlDataReader idr = cmd.ExecuteReader();

            //        if (idr.HasRows == false)
            //        {
            //            ShowMessage_warning("User not found", MessageType.Warning);
            //            return;
            //        }

            //        idr.Close();
            //        con.Close();
            //    }
            //}

            #endregion

            string downline = "";

            bool uplineid_cookieExists = HttpContext.Current.Request.Cookies["side"] != null;
            if (uplineid_cookieExists == true)
            {
                if (Request.Cookies["side"].Value != null && Request.Cookies["side"].Value != "")
                {
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
                                if (Request.Cookies["side"].Value == "Left")
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
                        else
                        {
                            ShowMessage_warning("Unable to assign member", MessageType.Warning);
                            return;
                        }

                        idr.Close();
                        con.Close();
                    }
                }
                else
                {
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
                }
            }
            else
            {
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
            }

            if (string.IsNullOrEmpty(downline))
            {
                upline = txt_keyinid.Text;
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
                        break;
                    }
                    else
                    {
                        upline = downline;
                    }
                }
            }
        }

        #region Encrypt

        string encryptdata = "";
        // Combine memberid and todaydate into a single string
        string data = txt_confirmpassword.Value.Trim();

        // Encrypt the data
        string encryptionKey = "Ecentra207007PASSWORD888"; // Replace with your encryption key
        byte[] keyBytes = Encoding.UTF8.GetBytes(encryptionKey);
        byte[] textBytes = Encoding.UTF8.GetBytes(data);

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = keyBytes;
            aesAlg.GenerateIV();

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (var msEncrypt = new System.IO.MemoryStream())
            {
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    csEncrypt.Write(textBytes, 0, textBytes.Length);
                    csEncrypt.FlushFinalBlock();

                    byte[] encryptedBytes = msEncrypt.ToArray();

                    byte[] resultBytes = new byte[aesAlg.IV.Length + encryptedBytes.Length];
                    Array.Copy(aesAlg.IV, 0, resultBytes, 0, aesAlg.IV.Length);
                    Array.Copy(encryptedBytes, 0, resultBytes, aesAlg.IV.Length, encryptedBytes.Length);

                    encryptdata = Convert.ToBase64String(resultBytes);
                }
            }
        }

        #endregion

        using (SqlCommand cmd = new SqlCommand("Update_Member_Details_Temporarily", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("Type", Type);
            cmd.Parameters.AddWithValue("Member_ID", member_id);
            cmd.Parameters.AddWithValue("Referal_ID", txt_referal_id.Value.Trim());
            cmd.Parameters.AddWithValue("Phone_Number", txt_phonenumber.Value.Trim());
            cmd.Parameters.AddWithValue("Full_Name", txt_fullname.Value.Trim());
            cmd.Parameters.AddWithValue("Company_Name", txt_companyname.Value.Trim());
            cmd.Parameters.AddWithValue("Company_No", txt_companyno.Value.Trim());
            cmd.Parameters.AddWithValue("Email", txt_email.Value.Trim());
            cmd.Parameters.AddWithValue("Nationality", ddl_nationality.SelectedValue);
            cmd.Parameters.AddWithValue("Gender", ddl_gender.Value);
            cmd.Parameters.AddWithValue("Add1", txt_address1.Value.Trim());
            cmd.Parameters.AddWithValue("Add2", txt_address2.Value.Trim());
            cmd.Parameters.AddWithValue("Postcode", txt_postcode.Value.Trim());
            cmd.Parameters.AddWithValue("City", txt_city.Value.Trim());
            cmd.Parameters.AddWithValue("Country", ddl_country.SelectedValue);
            if (ddl_country.SelectedValue == "MY")
            {
                cmd.Parameters.AddWithValue("State", ddl_State.SelectedValue);
            }
            else
            {
                cmd.Parameters.AddWithValue("State", "");
            }
            cmd.Parameters.AddWithValue("Password", encryptdata);
            if (ddl_type.SelectedValue == "NRIC")
            {
                cmd.Parameters.AddWithValue("NRIC", txt_nric.Text.Trim());
                cmd.Parameters.AddWithValue("Passport", "");

            }
            else if (ddl_type.SelectedValue == "Passport")
            {
                cmd.Parameters.AddWithValue("NRIC", "");
                cmd.Parameters.AddWithValue("Passport", txt_passport.Text.Trim());
            }

            if (btn_placement.Checked == true)
            {
                cmd.Parameters.AddWithValue("Binary_Placement", "Y");
                cmd.Parameters.AddWithValue("Upline", upline);
                cmd.Parameters.AddWithValue("Side", side);
                cmd.Parameters.AddWithValue("Placement_Value", ddl_member_placement.SelectedValue);
                if(ddl_member_placement.SelectedValue == "KeyinID")
                {
                    cmd.Parameters.AddWithValue("Placement_Key_In_ID", txt_keyinid.Text);
                    cmd.Parameters.AddWithValue("Placement_Left_or_Right", ddl_default_placement.SelectedValue);
                }
                else
                {
                    cmd.Parameters.AddWithValue("Placement_Key_In_ID", "");
                    cmd.Parameters.AddWithValue("Placement_Left_or_Right", "");
                }
            }
            else if (btn_placement.Checked == false)
            {
                cmd.Parameters.AddWithValue("Binary_Placement", "N");
                cmd.Parameters.AddWithValue("Upline", "");
                cmd.Parameters.AddWithValue("Side", "");
                cmd.Parameters.AddWithValue("Placement_Value", "");
                cmd.Parameters.AddWithValue("Placement_Key_In_ID", "");
                cmd.Parameters.AddWithValue("Placement_Left_or_Right", "");
            }

            if (btn_shopper.Checked == true)
            {
                cmd.Parameters.AddWithValue("Shopper", "Y");
                cmd.Parameters.AddWithValue("Profit_Center", "1");
                cmd.Parameters.AddWithValue("Agent_Level", "Shopper");
                cmd.Parameters.AddWithValue("Member_Category", "BO");
                cmd.Parameters.AddWithValue("Minimum_Package", "1");
                cmd.Parameters.AddWithValue("Package_Value", "1");
            }
            else
            {
                cmd.Parameters.AddWithValue("Shopper", "N");
                cmd.Parameters.AddWithValue("Agent_Level", "BO");
                if (ddl_package.Value == "1" || ddl_package.Value == "3")
                {
                    cmd.Parameters.AddWithValue("Member_Category", "BO");
                    cmd.Parameters.AddWithValue("Profit_Center", ddl_package.Value);
                    cmd.Parameters.AddWithValue("Minimum_Package", ddl_package.Value);
                    cmd.Parameters.AddWithValue("Package_Value", ddl_package.Value);
                }
                else
                {
                    if (ddl_package.Value == "EO1")
                    {
                        cmd.Parameters.AddWithValue("Profit_Center", "1");
                        cmd.Parameters.AddWithValue("Minimum_Package", "3");
                        cmd.Parameters.AddWithValue("Package_Value", "EO1");
                    }
                    else if (ddl_package.Value == "EO5")
                    {
                        cmd.Parameters.AddWithValue("Profit_Center", "3");
                        cmd.Parameters.AddWithValue("Minimum_Package", "5");
                        cmd.Parameters.AddWithValue("Package_Value", "EO5");
                    }
                    else if (ddl_package.Value == "EO9")
                    {
                        cmd.Parameters.AddWithValue("Profit_Center", "3");
                        cmd.Parameters.AddWithValue("Minimum_Package", "9");
                        cmd.Parameters.AddWithValue("Package_Value", "EO9");
                    }
                    cmd.Parameters.AddWithValue("Member_Category", "EO");
                }
            }

            // Set Output Paramater
            SqlParameter ReferalParam = new SqlParameter("@ReferalExist", SqlDbType.Bit);
            ReferalParam.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(ReferalParam);

            // Set Output Paramater
            SqlParameter NRICParam = new SqlParameter("@NRICExist", SqlDbType.Bit);
            NRICParam.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(NRICParam);

            // Set Output Paramater
            SqlParameter Passportaram = new SqlParameter("@PassportExist", SqlDbType.Bit);
            Passportaram.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(Passportaram);

            // Set Output Paramater
            SqlParameter MaximumIdentityparam = new SqlParameter("@MaximumIdentityExist", SqlDbType.Bit);
            MaximumIdentityparam.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(MaximumIdentityparam);

            // Set Output Paramater
            SqlParameter UseridParam = new SqlParameter("@Userid", SqlDbType.VarChar, 200);
            UseridParam.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(UseridParam);

            // Set Output Paramater
            SqlParameter StatusParam = new SqlParameter("@Status", SqlDbType.VarChar, 200);
            StatusParam.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(StatusParam);

            con.Open();
            cmd.ExecuteNonQuery();

            bool NRICExist = Convert.ToBoolean(cmd.Parameters["@NRICExist"].Value);
            bool PassportExist = Convert.ToBoolean(cmd.Parameters["@PassportExist"].Value);
            bool MaximumIdentityExist = Convert.ToBoolean(cmd.Parameters["@MaximumIdentityExist"].Value);
            bool ReferalExists = Convert.ToBoolean(cmd.Parameters["@ReferalExist"].Value);
            string userid = cmd.Parameters["@Userid"].Value.ToString();
            string StatusExists = cmd.Parameters["@Status"].Value.ToString();

            con.Close();

            if (StatusExists == "success")
            {
                Response.Cookies["Profit_Center"].Value = ddl_package.Value;
                Response.Redirect("Register_Member_Product_List.aspx?id=" + userid);
            }
            else if (StatusExists == "failed")
            {
                if (NRICExist == true)
                {
                    ShowMessage_warning("NRIC already registered, please use another NRIC to register", MessageType.Warning);
                    txt_nric.Focus();
                }
                else if (PassportExist == true)
                {
                    ShowMessage_warning("Passport already registered, please use another Passport to register", MessageType.Warning);
                    txt_passport.Focus();
                }
                else if (MaximumIdentityExist == true)
                {
                    ShowMessage_warning("Each NRIC / Passport maximum register 3 account.", MessageType.Warning);
                    txt_passport.Focus();
                }
                else if (ReferalExists == false)
                {
                    ShowMessage_warning("Referral ID not found!", MessageType.Warning);
                    txt_referal_id.Focus();
                }
            }
            con.Close();
        }
    }

    protected void ddl_member_placement_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_member_placement.SelectedValue == "KeyinID")
        {
            div_id.Visible = true;
        }
        else
        {
            div_id.Visible = false;
        }
    }

    protected void SendEmail(string memberid)
    {
        string login_id = "";
        string login_password = "";
        string referral_id = "";
        string referral_name = "";
        string profit_center = "";
        string email = "";
        using (SqlCommand cmd = new SqlCommand("Load_Member_Details_Send_Email", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Memberid", memberid);
            con.Open();
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows == true)
            {
                while (idr.Read())
                {
                    login_id = idr["cardno"].ToString();
                    login_password = txt_confirmpassword.Value.Trim();
                    referral_id = idr["referal_id"].ToString();
                    if(!string.IsNullOrEmpty(idr["Referral_Personal_Name"].ToString()))
                    {
                        referral_name = idr["Referral_Personal_Name"].ToString();
                    }
                    else
                    {
                        referral_name = idr["Referral_Company_Name"].ToString();
                    }
                    profit_center = idr["Profit_Center"].ToString();
                    email = idr["Email"].ToString();
                }
            }

            idr.Close();
            con.Close();
        }

        string content = "";
        content = "<html><head>";
        content += "<style>body { font-family: Arial, sans-serif; background-color: #f4f4f4; padding: 20px; } .frame{ max-width: 100%; background-color: #efefef; padding: 10px;} .container { max-width: 600px; margin: 0 auto; background-color: #ffffff; padding: 20px; border-radius: 10px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);}";
        content += "h1 {color: #333333;} p {color: #666666;} .login-info { background-color: #f0f0f0; padding: 10px; border-radius: 5px; margin-top: 20px;} .login-info p {margin: 0;} .redirect{ background-color: #fde293;} </style></head>";
        content += "<body>";
        content += "<div class='frame'>";
        content += "<div class='container'><h1>Registration Successful!</h1><p>Thank you for registering with our platform.</p>";
        content += "<p>Your login credentials are as follows:</p>";
        content += "<div class='login-info'>";
        content += "<p><strong>Login ID :</strong> " + login_id + "</p><br/>";
        content += "<p><strong>Password :</strong> " + login_password + "</p><br/>";
        content += "<p><strong>Profit Center :</strong> " + profit_center + " Profit Center</p>";
        content += "</div>";
        content += "<p>Your referral details are as follows:</p>";
        content += "<div class='login-info'>";
        content += "<p><strong>Referral ID :</strong> " + referral_id + "</p><br/>";
        content += "<p><strong>Referral Name :</strong> " + referral_name + "</p>";
        content += "</div>";
        content += "<p>Please keep your login credentials secure and do not share them with anyone.</p>";
        content += "<p>You can now log in to our platform by " + "<a class='redirect' href='https://ecentra.com.my/'>click here</a>" + " and start exploring our services.</p>";
        content += "<p>Regards,<br>Ecentra Customer Service</p>";
        content += "</div>";
        content += "</div>";
        content += "</body>";
        content += "</html>";

        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
        mail.To.Add(email);
        mail.From = new MailAddress("noreply@ecentra.com.my", "ECENTRA");
        mail.SubjectEncoding = System.Text.Encoding.UTF8;

        string htmlc;

        mail.Subject = "ECENTRA";
        htmlc = content;

        mail.Body = htmlc;
        mail.BodyEncoding = System.Text.Encoding.UTF8;
        mail.Priority = MailPriority.High;
        mail.IsBodyHtml = true;

        SmtpClient client = new SmtpClient();
        client.UseDefaultCredentials = false;
        client.Credentials = new System.Net.NetworkCredential("noreply@ecentra.com.my", "ECentra123!");
        client.Port = 587;
        client.Host = "smtppro55.mschosting.network";
        client.EnableSsl = false;

        try
        {
            client.Send(mail);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(''" + ex + "'');", true);
        }
    }

    [WebMethod]
    public static string GetDownlineDetails(string memberid)
    {
        DataTable memberdetails = new DataTable();

        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("Load_Member_Downline", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Memberid", memberid);
                con.Open();
                SqlDataReader idr = cmd.ExecuteReader();

                if (idr.HasRows == true)
                {
                    memberdetails.Load(idr);
                }

                idr.Close();
                con.Close();
            }

            var jsonObject = new JObject();
            jsonObject.Add("memberdetails", JToken.FromObject(memberdetails));

            string jsonDetails = jsonObject.ToString();
            return jsonDetails;
        }
    }


    protected void btn_verify_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txt_referal_id.Value))
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "AlertCode", "alert('Please key in referral id');", true);
            txt_referal_id.Focus();
        }
        else
        {
            using (SqlCommand cmd = new SqlCommand("Verify_Referral_ID", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Referral_ID", txt_referal_id.Value);
                con.Open();
                SqlDataReader idr = cmd.ExecuteReader();

                if (idr.HasRows == true)
                {
                    DataTable data = new DataTable();
                    data.Load(idr);

                    txt_referal_id.Disabled = true;
                    if (!string.IsNullOrEmpty(data.Rows[0]["First_Name"].ToString()))
                    {
                        txt_referal_name.Value = data.Rows[0]["First_Name"].ToString();
                    }
                    else
                    {
                        txt_referal_name.Value = data.Rows[0]["Company_Name"].ToString();
                    }
                    btn_verify.Visible = false;
                    btn_remove.Visible = true;
                    if (divNameVisible.Value == "true")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "Member_Type('Personal');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "Member_Type('Company');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "AlertCode", "alert('Referral ID not found');", true);
                    txt_referal_id.Focus();
                }

                idr.Close();
                con.Close();
            }

        }
    }

    protected void btn_remove_Click(object sender, EventArgs e)
    {
        txt_referal_id.Disabled = false;
        txt_referal_id.Value = "";
        txt_referal_name.Value = "";
        btn_verify.Visible = true;
        btn_remove.Visible = false;
        txt_referal_id.Focus();
        if (divNameVisible.Value == "true")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "Member_Type('Personal');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "Member_Type('Company');", true);
        }

    }

    protected void ddl_country_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_State();
    }
}