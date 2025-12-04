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
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class My_Info : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

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
                    Load_State();
                    Load_Country();
                    Load_Bank();
                    GetMemberDetails(Request.Cookies["userid"].Value);
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

                    ListItem defaultItem = new ListItem("Select your country", "");
                    defaultItem.Attributes["disabled"] = "disabled";
                    defaultItem.Selected = true; // Set the default item as selected
                    ddl_country.Items.Insert(0, defaultItem);
                }

                idr.Close();
                con.Close();
            }
        }
    }

    protected void Load_Bank()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("Load_Bank", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader idr = cmd.ExecuteReader();

                if (idr.HasRows == true)
                {
                    ddl_bank.Items.Clear();
                    DataTable v = new DataTable();

                    v.Load(idr);
                    ddl_bank.DataSource = v;
                    ddl_bank.DataTextField = "Bankname";
                    ddl_bank.DataValueField = "Bankcode";
                    ddl_bank.DataBind();

                    ddl_bank.Items.Insert(0, new ListItem("Select Bank", ""));
                    ddl_bank.Items[0].Attributes["disabled"] = "disabled";

                }

                idr.Close();
                con.Close();
            }
        }
    }

    protected void GetMemberDetails(string id)
    {
        using (SqlCommand cmd = new SqlCommand("Load_Member_Details_My_Info", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Memberid", id);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                DateTime joindate;
                DateTime bday;
                string showjoindate;
                string birthdaydate;

                joindate = Convert.ToDateTime(dt.Rows[0]["Created_DT"]);
                showjoindate = joindate.ToString("d MMMM yyyy");

                lbl_nationality.Text = dt.Rows[0]["Nationality_Name"].ToString();
                lbl_member_id.Text = dt.Rows[0]["cardno"].ToString();
                lbl_member_join_date.Text = showjoindate;

                if(dt.Rows[0]["Member_Type"].ToString() == "Personal")
                {
                    div_name.Style["display"] = "block";
                    div_identity.Style["display"] = "block";
                    div_gender.Style["display"] = "block";
                    div_company_name.Style["display"] = "none";
                    div_company_no.Style["display"] = "none";

                    lbl_member_name.Text = dt.Rows[0]["First_Name"].ToString();
                    
                    if(!string.IsNullOrEmpty(dt.Rows[0]["NRIC"].ToString()))
                    {
                        lbl_member_identity.Text = dt.Rows[0]["NRIC"].ToString();
                    }
                    else
                    {
                        lbl_member_identity.Text = dt.Rows[0]["Passport"].ToString();
                    }
                }
                else
                {
                    div_name.Style["display"] = "none";
                    div_identity.Style["display"] = "none";
                    div_gender.Style["display"] = "none";
                    div_company_name.Style["display"] = "block";
                    div_company_no.Style["display"] = "block";

                    lbl_company_name.Text = dt.Rows[0]["Company_Name"].ToString();
                    lbl_company_no.Text = dt.Rows[0]["Company_No"].ToString();
                }

                //bday = Convert.ToDateTime(dt.Rows[0]["DOB"]);
                //birthdaydate = bday.ToString("d MMMM yyyy");

                //lbl_member_dob.Text = birthdaydate;
                lbl_member_gender.Text = dt.Rows[0]["Gender"].ToString();
                txt_member_mobile.Text = dt.Rows[0]["Contact_No"].ToString();
                txt_member_email.Text = dt.Rows[0]["Email"].ToString();
                
                ddl_country.Text = dt.Rows[0]["Country"].ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "toggleCountry();", true);
                ddl_State.Text = dt.Rows[0]["State"].ToString();
                txt_city.Text = dt.Rows[0]["City"].ToString();
                txt_address_1.Text = dt.Rows[0]["Add1"].ToString();
                txt_address_2.Text = dt.Rows[0]["Add2"].ToString();
                txt_postcode.Text = dt.Rows[0]["Postcode"].ToString();

                if(string.IsNullOrEmpty(dt.Rows[0]["Bank_Code"].ToString()) && string.IsNullOrEmpty(dt.Rows[0]["Bank_Account"].ToString()) && string.IsNullOrEmpty(dt.Rows[0]["Account_Name"].ToString()))
                {
                    txt_account_name.Text = "";
                    ddl_bank.SelectedValue = "";
                    txt_account_no.Text = "";
                }
                else
                {
                    txt_account_name.Enabled = false;
                    ddl_bank.Enabled = false;
                    txt_account_no.Enabled = false;
                    txt_account_name.Text = dt.Rows[0]["Account_Name"].ToString();
                    ddl_bank.SelectedValue = dt.Rows[0]["Bank_Code"].ToString();
                    txt_account_no.Text = dt.Rows[0]["Bank_Account"].ToString();
                }

                if (string.IsNullOrEmpty(dt.Rows[0]["E_Wallet_Password"].ToString()))
                {
                    div_e_wallet_password.Visible = true;
                }
                else
                {
                    div_e_wallet_password.Visible = false;
                }
            }

            con.Close();

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

    protected void btn_update_Click(object sender, EventArgs e)
    {
        if(string.IsNullOrEmpty(txt_member_mobile.Text))
        {
            ShowMessage_warning("Please key in mobile number", MessageType.Warning);
            return;
        } 
        else if (string.IsNullOrEmpty(txt_member_email.Text))
        {
            ShowMessage_warning("Please key in email address", MessageType.Warning);
            return;
        }
        else if (string.IsNullOrEmpty(txt_city.Text))
        {
            ShowMessage_warning("Please key in city", MessageType.Warning);
            return;
        }
        else if (string.IsNullOrEmpty(txt_address_1.Text))
        {
            ShowMessage_warning("Please key in address", MessageType.Warning);
            return;
        }
        else if (string.IsNullOrEmpty(txt_postcode.Text))
        {
            ShowMessage_warning("Please key in postcode", MessageType.Warning);
            return;
        }
        else if (string.IsNullOrEmpty(txt_account_name.Text) && (!string.IsNullOrEmpty(ddl_bank.SelectedValue) || !string.IsNullOrEmpty(txt_account_no.Text)))
        {
            ShowMessage_warning("Please key in bank account owner name", MessageType.Warning);
            return;
        }
        else if (string.IsNullOrEmpty(ddl_bank.SelectedValue) && (!string.IsNullOrEmpty(txt_account_name.Text) || !string.IsNullOrEmpty(txt_account_no.Text)))
        {
            ShowMessage_warning("Please key in bank name", MessageType.Warning);
            return;
        }
        else if (string.IsNullOrEmpty(txt_account_no.Text) && (!string.IsNullOrEmpty(txt_account_name.Text) || !string.IsNullOrEmpty(ddl_bank.SelectedValue)))
        {
            ShowMessage_warning("Please key in bank account number", MessageType.Warning);
            return;
        }
        else if (!string.IsNullOrEmpty(txt_account_no.Text) && !string.IsNullOrEmpty(txt_account_name.Text) && !string.IsNullOrEmpty(ddl_bank.SelectedValue) && div_e_wallet_password.Visible == true && string.IsNullOrEmpty(txt_ewallet_password.Text))
        {
            ShowMessage_warning("Please key in eWallet password", MessageType.Warning);
            return;
        }
        else if (!string.IsNullOrEmpty(txt_account_no.Text) && !string.IsNullOrEmpty(txt_account_name.Text) && !string.IsNullOrEmpty(ddl_bank.SelectedValue) && div_e_wallet_password.Visible == true && string.IsNullOrEmpty(txt_confirm_ewallet_password.Text))
        {
            ShowMessage_warning("Please key in eWallet confirm password", MessageType.Warning);
            return;
        }
        else if (div_e_wallet_password.Visible == true && !string.IsNullOrEmpty(txt_ewallet_password.Text) && string.IsNullOrEmpty(txt_confirm_ewallet_password.Text))
        {
            ShowMessage_warning("Please key in eWallet confirm password", MessageType.Warning);
            return;
        }
        else if (div_e_wallet_password.Visible == true && !string.IsNullOrEmpty(txt_confirm_ewallet_password.Text) && string.IsNullOrEmpty(txt_ewallet_password.Text))
        {
            ShowMessage_warning("Please key in eWallet password", MessageType.Warning);
            return;
        }
        else if (div_e_wallet_password.Visible == true && (txt_confirm_ewallet_password.Text != txt_ewallet_password.Text))
        {
            ShowMessage_warning("Please make sure eWallet password and confirm eWallet password is same", MessageType.Warning);
            return;
        }
        else if (div_e_wallet_password.Visible == true && !string.IsNullOrEmpty(txt_confirm_ewallet_password.Text) && !Regex.IsMatch(txt_confirm_ewallet_password.Text, @"^[A-Za-z0-9]{6,}$"))
        {
            ShowMessage_warning("eWallet password should be a minimum of 6 characters", MessageType.Warning);
            return;
        }
        else
        {
            using (SqlCommand cmd = new SqlCommand("Update_Member_Detials", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Member_ID", Request.Cookies["userid"].Value);
                cmd.Parameters.AddWithValue("Mobile_Number", txt_member_mobile.Text);
                cmd.Parameters.AddWithValue("Email", txt_member_email.Text.Trim());
                cmd.Parameters.AddWithValue("Add1", txt_address_1.Text.Trim());
                cmd.Parameters.AddWithValue("Add2", txt_address_2.Text.Trim());
                cmd.Parameters.AddWithValue("Postcode", txt_postcode.Text.Trim());
                cmd.Parameters.AddWithValue("City", txt_city.Text.Trim());
                cmd.Parameters.AddWithValue("State", ddl_State.SelectedValue);
                cmd.Parameters.AddWithValue("Country", ddl_country.SelectedValue);

                if(ddl_bank.Enabled == true && txt_account_name.Enabled == true && txt_account_no.Enabled == true)
                {
                    cmd.Parameters.AddWithValue("Account_Name", txt_account_name.Text.Trim());
                    cmd.Parameters.AddWithValue("Bank_Name", ddl_bank.SelectedValue);
                    cmd.Parameters.AddWithValue("Account_Number", txt_account_no.Text.Trim());
                }
                else
                {
                    cmd.Parameters.AddWithValue("Account_Name", "");
                    cmd.Parameters.AddWithValue("Bank_Name", "");
                    cmd.Parameters.AddWithValue("Account_Number", "");
                }

                if (div_e_wallet_password.Visible == true && !string.IsNullOrEmpty(txt_ewallet_password.Text) && !string.IsNullOrEmpty(txt_confirm_ewallet_password.Text))
                {
                    #region Encrypt

                    string encryptdata = "";
                    // Combine memberid and todaydate into a single string
                    string data = txt_confirm_ewallet_password.Text.Trim();

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

                    cmd.Parameters.AddWithValue("E_Wallet_Password", encryptdata);
                }
                else
                {
                    cmd.Parameters.AddWithValue("E_Wallet_Password", "");
                }

                // Set Output Paramater
                SqlParameter StatusParam = new SqlParameter("@Status", SqlDbType.VarChar, 200);
                StatusParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(StatusParam);

                con.Open();
                cmd.ExecuteNonQuery();

                string StatusExists = cmd.Parameters["@Status"].Value.ToString();

                con.Close();

                if (StatusExists == "success")
                {
                    SendEmail(Request.Cookies["userid"].Value, txt_confirm_ewallet_password.Text);
                    GetMemberDetails(Request.Cookies["userid"].Value);
                    ShowMessage("Update Member Details Successful.", MessageType.Success);
                }
                else if (StatusExists == "failed")
                {

                }
                con.Close();
            }
        }
    }

    protected void SendEmail(string memberid, string walletpassword)
    {
        string phonenumber = "";
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
                    phonenumber = idr["Contact_No"].ToString();
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
        content += "<div class='container'><h1>Update Member Details Successful!</h1>";
        content += "<p>Your updated information are as follows:</p>";
        content += "<div class='login-info'>";
        content += "<p><strong>Phone Number :</strong> " + phonenumber + "</p><br/>";
        content += "<p><strong>Email :</strong> " + email + "</p>";
        if (!string.IsNullOrEmpty(walletpassword))
        {
            content += "<br/><p><strong>New Wallet Password :</strong> " + walletpassword + "</p>";
        }
        if (ddl_bank.Enabled == true && txt_account_name.Enabled == true && txt_account_no.Enabled == true && !string.IsNullOrEmpty(txt_account_no.Text))
        {
            content += "<br/><p><strong>Bank Account Name :</strong> " + txt_account_name.Text.Trim() + "</p>";
            content += "<br/><p><strong>Bank_Name :</strong> " + ddl_bank.SelectedItem.Text + "</p>";
            content += "<br/><p><strong>Bank Account Number :</strong> " + txt_account_no.Text.Trim() + "</p>";
        }
        content += "</div>";
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

}