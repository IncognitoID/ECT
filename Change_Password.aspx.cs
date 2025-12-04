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

public partial class Change_Password : System.Web.UI.Page
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

    protected void GetMemberDetails(string id)
    {
        using (SqlCommand cmd = new SqlCommand("Load_Member_Details", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Memberid", id);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            if (dt.Rows.Count > 0)
            {

                if (string.IsNullOrEmpty(dt.Rows[0]["E_Wallet_Password"].ToString()))
                {
                    div_change_e_wallet_password.Visible = false;
                }
                else
                {
                    div_change_e_wallet_password.Visible = true;
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
        if (string.IsNullOrEmpty(txt_old_password.Text) && (!string.IsNullOrEmpty(txt_new_password.Text) || !string.IsNullOrEmpty(txt_confirm_new_password.Text)))
        {
            ShowMessage_warning("Please key in old password", MessageType.Warning);
            return;
        }
        else if (string.IsNullOrEmpty(txt_new_password.Text) && (!string.IsNullOrEmpty(txt_old_password.Text) || !string.IsNullOrEmpty(txt_confirm_new_password.Text)))
        {
            ShowMessage_warning("Please key in new password", MessageType.Warning);
            return;
        }
        else if (string.IsNullOrEmpty(txt_confirm_new_password.Text) && (!string.IsNullOrEmpty(txt_old_password.Text) || !string.IsNullOrEmpty(txt_new_password.Text)))
        {
            ShowMessage_warning("Please key in new confirm password", MessageType.Warning);
            return;
        }
        else if ((!string.IsNullOrEmpty(txt_new_password.Text) && !string.IsNullOrEmpty(txt_confirm_new_password.Text)) && (txt_new_password.Text != txt_confirm_new_password.Text))
        {
            ShowMessage_warning("Please make sure new password and confirm password is same", MessageType.Warning);
            return;
        }
        else if (!string.IsNullOrEmpty(txt_confirm_new_password.Text) && !Regex.IsMatch(txt_confirm_new_password.Text, @"^[A-Za-z0-9]{6,}$"))
        {
            ShowMessage_warning("New password should be a minimum of 6 characters", MessageType.Warning);
            return;
        }
        else if (div_change_e_wallet_password.Visible == true && string.IsNullOrEmpty(txt_old_e_wallet_password.Text) && (!string.IsNullOrEmpty(txt_new_e_wallet_password.Text) || !string.IsNullOrEmpty(txt_new_confirm_e_wallet_password.Text)))
        {
            ShowMessage_warning("Please key in old eWallet password", MessageType.Warning);
            return;
        }
        else if (div_change_e_wallet_password.Visible == true && string.IsNullOrEmpty(txt_new_e_wallet_password.Text) && (!string.IsNullOrEmpty(txt_old_e_wallet_password.Text) || !string.IsNullOrEmpty(txt_new_confirm_e_wallet_password.Text)))
        {
            ShowMessage_warning("Please key in new eWallet password", MessageType.Warning);
            return;
        }
        else if (div_change_e_wallet_password.Visible == true && string.IsNullOrEmpty(txt_new_confirm_e_wallet_password.Text) && (!string.IsNullOrEmpty(txt_old_e_wallet_password.Text) || !string.IsNullOrEmpty(txt_new_e_wallet_password.Text)))
        {
            ShowMessage_warning("Please key in new eWallet confirm password", MessageType.Warning);
            return;
        }
        else if (div_change_e_wallet_password.Visible == true && (!string.IsNullOrEmpty(txt_new_e_wallet_password.Text) && !string.IsNullOrEmpty(txt_new_confirm_e_wallet_password.Text)) && (txt_new_e_wallet_password.Text != txt_new_confirm_e_wallet_password.Text))
        {
            ShowMessage_warning("Please make sure new eWallet password and eWallet confirm password is same", MessageType.Warning);
            return;
        }
        else if (div_change_e_wallet_password.Visible == true && !string.IsNullOrEmpty(txt_new_confirm_e_wallet_password.Text) && !Regex.IsMatch(txt_new_confirm_e_wallet_password.Text, @"^[A-Za-z0-9]{6,}$"))
        {
            ShowMessage_warning("New eWallet password should be a minimum of 6 characters", MessageType.Warning);
            return;
        }
        else
        {
            using (SqlCommand cmd = new SqlCommand("Load_Member_Password", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Member_ID", Request.Cookies["userid"].Value);

                con.Open();
                SqlDataReader idr = cmd.ExecuteReader();

                if (idr.HasRows == true)
                {
                    if (idr.Read())
                    {
                        if (!string.IsNullOrEmpty(txt_old_password.Text) && !string.IsNullOrEmpty(txt_new_password.Text) && !string.IsNullOrEmpty(txt_confirm_new_password.Text))
                        {
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

                                        if (decryptedText != txt_old_password.Text.Trim())
                                        {
                                            ShowMessage_warning("Old password is not same with current password", MessageType.Warning);
                                            txt_old_password.Focus();
                                            return;
                                        }
                                    }
                                }
                            }
                        }

                        if (div_change_e_wallet_password.Visible == true && !string.IsNullOrEmpty(txt_old_e_wallet_password.Text) && !string.IsNullOrEmpty(txt_new_e_wallet_password.Text) && !string.IsNullOrEmpty(txt_new_confirm_e_wallet_password.Text))
                        {
                            string member_e_wallet_password = "";
                            member_e_wallet_password = idr["E_Wallet_Password"].ToString();
                            byte[] encryptedBytes = Convert.FromBase64String(member_e_wallet_password);
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

                                        if (decryptedText != txt_old_e_wallet_password.Text.Trim())
                                        {
                                            ShowMessage_warning("Old eWallet password is not same with current eWallet password", MessageType.Warning);
                                            txt_old_e_wallet_password.Focus();
                                            return;
                                        }
                                    }
                                }
                            }
                        }

                    }
                }
                else
                {
                    ShowMessage_warning("Something went wrong, please try again later.", MessageType.Warning);
                    txt_old_e_wallet_password.Focus();
                    return;
                }

                idr.Close();
                con.Close();
            }

            using (SqlCommand cmd = new SqlCommand("Update_Member_Password_v2", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Member_ID", Request.Cookies["userid"].Value);

                if (!string.IsNullOrEmpty(txt_old_password.Text) && !string.IsNullOrEmpty(txt_new_password.Text) && !string.IsNullOrEmpty(txt_confirm_new_password.Text))
                {
                    #region Encrypt

                    string encryptdata = "";
                    // Combine memberid and todaydate into a single string
                    string data = txt_confirm_new_password.Text.Trim();

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

                    cmd.Parameters.AddWithValue("New_Password", encryptdata);
                }
                else
                {
                    cmd.Parameters.AddWithValue("New_Password", "");
                }

                if (div_change_e_wallet_password.Visible == true && !string.IsNullOrEmpty(txt_old_e_wallet_password.Text) && !string.IsNullOrEmpty(txt_new_e_wallet_password.Text) && !string.IsNullOrEmpty(txt_new_confirm_e_wallet_password.Text))
                {
                    #region Encrypt

                    string encryptdata = "";
                    // Combine memberid and todaydate into a single string
                    string data = txt_new_e_wallet_password.Text.Trim();

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

                    cmd.Parameters.AddWithValue("New_E_Wallet_Password", encryptdata);
                }
                else
                {
                    cmd.Parameters.AddWithValue("New_E_Wallet_Password", "");
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
                    SendEmail(Request.Cookies["userid"].Value, txt_confirm_new_password.Text, txt_new_e_wallet_password.Text);
                    GetMemberDetails(Request.Cookies["userid"].Value);
                    ShowMessage("Update Member Password Successful.", MessageType.Success);
                }
                else if (StatusExists == "failed")
                {
                    ShowMessage_warning("Something went wrong, please try again later.", MessageType.Warning);
                    txt_old_password.Focus();
                    return;
                }
                con.Close();
            }
        }
    }

    protected void SendEmail(string memberid, string newpassword, string newewalletpassword)
    {
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
        content += "<div class='container'><h1>Change Password Successful!</h1>";
        content += "<p>Your new password credentials are as follows:</p>";
        content += "<div class='login-info'>";
        if (!string.IsNullOrEmpty(newpassword))
        {
            content += "<p><strong>New Password :</strong> " + newpassword + "</p>";
        }
        if (!string.IsNullOrEmpty(newewalletpassword))
        {
            content += "<br/><p><strong>New Wallet Password :</strong> " + newewalletpassword + "</p>";
        }
        content += "</div>";
        content += "<p>Please keep your password credentials secure and do not share them with anyone.</p>";
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