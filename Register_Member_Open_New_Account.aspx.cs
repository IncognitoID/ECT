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

public partial class Register_Member_Open_New_Account : System.Web.UI.Page
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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "Member_Type('Personal');", true);
                }
            }

            GetPolicy();
            Load_State();
            Load_Nationality();
            Load_Country();
            ddl_package.Disabled = true;
            btn_placement.Checked = true;
            btn_placement.Disabled = true;


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
                    ddl_package.Value = idr["Profit_Center"].ToString();
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
                cmd.Parameters.AddWithValue("@CountryCode", "%" + ddl_country.SelectedValue + "%");
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

        if (string.IsNullOrEmpty(ddl_nationality.SelectedValue))
        {
            ShowMessage_warning("Please select your nationality", MessageType.Warning);
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
                    upline = Request.Cookies["userid"].Value;
                    side = "Left";
                }

                if (btn_placement.Checked == true && ddl_member_placement.SelectedValue == "Right")
                {
                    upline = Request.Cookies["userid"].Value;
                    side = "Right";
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
                    using (SqlCommand cmd = new SqlCommand("Insert_New_Member_Temporarily_Purchase_New_Account", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("Type", Type);
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
                            cmd.Parameters.AddWithValue("State", "");
                        }
                        cmd.Parameters.AddWithValue("Password", encryptdata);
                        cmd.Parameters.AddWithValue("Profit_Center", ddl_package.Value);
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

                        cmd.Parameters.AddWithValue("Binary_Placement", "Y");
                        cmd.Parameters.AddWithValue("Upline", upline);
                        cmd.Parameters.AddWithValue("Side", side);
                        cmd.Parameters.AddWithValue("Placement_Value", ddl_member_placement.SelectedValue);
                        cmd.Parameters.AddWithValue("Placement_Key_In_ID", "");

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
                        bool ReferalExists = Convert.ToBoolean(cmd.Parameters["@ReferalExist"].Value);
                        string userid = cmd.Parameters["@Userid"].Value.ToString();
                        string StatusExists = cmd.Parameters["@Status"].Value.ToString();

                        con.Close();

                        if (StatusExists == "success")
                        {
                            Response.Cookies["Profit_Center"].Value = ddl_package.Value;
                            Response.Redirect("Register_Member_Open_New_Account_Product_List.aspx?id=" + userid);
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
            upline = Request.Cookies["userid"].Value;
            side = "Left";
        }

        if (btn_placement.Checked == true && ddl_member_placement.SelectedValue == "Right")
        {
            upline = Request.Cookies["userid"].Value;
            side = "Right";
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
            cmd.Parameters.AddWithValue("Profit_Center", ddl_package.Value);
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

            cmd.Parameters.AddWithValue("Binary_Placement", "Y");
            cmd.Parameters.AddWithValue("Upline", upline);
            cmd.Parameters.AddWithValue("Side", side);
            cmd.Parameters.AddWithValue("Placement_Value", ddl_member_placement.SelectedValue);
            cmd.Parameters.AddWithValue("Placement_Key_In_ID", "");

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
            bool ReferalExists = Convert.ToBoolean(cmd.Parameters["@ReferalExist"].Value);
            string userid = cmd.Parameters["@Userid"].Value.ToString();
            string StatusExists = cmd.Parameters["@Status"].Value.ToString();

            con.Close();

            if (StatusExists == "success")
            {
                Response.Cookies["Profit_Center"].Value = ddl_package.Value;
                Response.Redirect("Register_Member_Open_New_Account_Product_List.aspx?id=" + userid);
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

    protected void ddl_member_placement_SelectedIndexChanged(object sender, EventArgs e)
    {
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

}