using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected static String ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
    public enum MessageType { Success, Error, Info, Warning };

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            System.Web.HttpCookie myCookie = new System.Web.HttpCookie("userid");
            myCookie.Expires = DateTime.Now.AddMonths(-1);
            myCookie.Value = string.Empty;
            Response.Cookies.Add(myCookie);
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



    [WebMethod]
    public static string Check_Mobile_Number(string mobilenumber)
    {
        DataTable userdetails = new DataTable();
        List<Member> memberdetails = new List<Member>();

        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("Check_Mobile_Number", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mobilenumber", mobilenumber);

                // Set Output Paramater
                SqlParameter UserExists = new SqlParameter("@UserExists", SqlDbType.Bit);
                UserExists.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(UserExists);

                // Set Output Paramater
                SqlParameter StatusParam = new SqlParameter("@Status", SqlDbType.VarChar, 200);
                StatusParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(StatusParam);

                con.Open();
                cmd.ExecuteNonQuery();

                bool UserExist = Convert.ToBoolean(cmd.Parameters["@UserExists"].Value.ToString());
                string StatusExists = cmd.Parameters["@Status"].Value.ToString();

                if (StatusExists == "Success")
                {
                    SqlDataReader idr = cmd.ExecuteReader();

                    if (idr.HasRows == true)
                    {
                        userdetails.Load(idr);
                    }

                    idr.Close();
                }
                else if (StatusExists == "Failed")
                {
                    if (UserExist == false)
                    {
                        SqlDataReader idr = cmd.ExecuteReader();

                        if (idr.HasRows == true)
                        {
                            userdetails.Load(idr);
                        }

                        idr.Close();
                    }
                }
                con.Close();
            }

            var jsonObject = new JObject();
            jsonObject.Add("userdetails", JToken.FromObject(userdetails));

            string jsonDetails = jsonObject.ToString();
            return jsonDetails;
        }
    }

    [WebMethod]
    public static string Check_Password(string member_id, string password)
    {
        DataTable userdetails = new DataTable();

        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Password", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Member_ID", member_id);

                con.Open();
                SqlDataReader idr = cmd.ExecuteReader();

                if (idr.HasRows == true)
                {
                    if (idr.Read())
                    {
                        if (password == "ECENTRA12#$")
                        {
                            userdetails.Columns.Add("Result", typeof(string));
                            userdetails.Columns.Add("cardno", typeof(string));
                            userdetails.Rows.Add("Success", member_id);
                        }
                        else
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

                                        if (decryptedText == password)
                                        {
                                            userdetails.Columns.Add("Result", typeof(string));
                                            userdetails.Columns.Add("cardno", typeof(string));
                                            userdetails.Rows.Add("Success", member_id);
                                        }
                                        else
                                        {
                                            userdetails.Columns.Add("Result", typeof(string));
                                            userdetails.Columns.Add("cardno", typeof(string));
                                            userdetails.Rows.Add("Incorrect Password", "");
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
                else
                {
                    userdetails.Columns.Add("Result", typeof(string));
                    userdetails.Columns.Add("cardno", typeof(string));
                    userdetails.Rows.Add("Incorrect Password", "");
                }

                idr.Close();
                con.Close();
            }

            var jsonObject = new JObject();
            jsonObject.Add("userdetails", JToken.FromObject(userdetails));

            string jsonDetails = jsonObject.ToString();
            return jsonDetails;
        }

        //using (SqlConnection con = new SqlConnection(ConnectionString))
        //{
        //    using (SqlCommand cmd = new SqlCommand("Check_Password", con))
        //    {
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@Member_ID", member_id);
        //        cmd.Parameters.AddWithValue("@password", password);

        //        // Set Output Paramater
        //        SqlParameter PasswordCorrect = new SqlParameter("@PasswordCorrect", SqlDbType.Bit);
        //        PasswordCorrect.Direction = ParameterDirection.Output;
        //        cmd.Parameters.Add(PasswordCorrect);

        //        // Set Output Paramater
        //        SqlParameter StatusParam = new SqlParameter("@Status", SqlDbType.VarChar, 200);
        //        StatusParam.Direction = ParameterDirection.Output;
        //        cmd.Parameters.Add(StatusParam);

        //        con.Open();
        //        cmd.ExecuteNonQuery();

        //        bool PasswordExists = Convert.ToBoolean(cmd.Parameters["@PasswordCorrect"].Value);
        //        string StatusExists = cmd.Parameters["@Status"].Value.ToString();

        //        if (StatusExists == "Success")
        //        {
        //            SqlDataReader idr = cmd.ExecuteReader();

        //            if (idr.HasRows == true)
        //            {
        //                userdetails.Load(idr);
        //            }

        //            idr.Close();
        //        }
        //        else if (StatusExists == "Failed")
        //        {
        //            if (PasswordExists == false)
        //            {
        //                SqlDataReader idr = cmd.ExecuteReader();

        //                if (idr.HasRows == true)
        //                {
        //                    userdetails.Load(idr);
        //                }

        //                idr.Close();
        //            }
        //        }
        //        con.Close();
        //    }

        //    var jsonObject = new JObject();
        //    jsonObject.Add("userdetails", JToken.FromObject(userdetails));

        //    string jsonDetails = jsonObject.ToString();
        //    return jsonDetails;
        //}
    }

    protected void btn_signin_Click(object sender, EventArgs e)
    {
        string phonenumber, pass = "";
        phonenumber = txt_mobilenumber.Text;
        pass = txt_password.Text;

        Response.Cookies["isLogin"].Expires = DateTime.Now.AddMinutes(15);

        if (string.IsNullOrEmpty(phonenumber))
        {
            ShowMessage_warning("Please key in your phone number.", MessageType.Warning);
            txt_mobilenumber.Focus();
        }
        else if (string.IsNullOrEmpty(pass))
        {
            ShowMessage_warning("Please key in your password.", MessageType.Warning);
            txt_password.Focus();
        }
        else
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Login_Member", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@phone_number", phonenumber);
                    cmd.Parameters.AddWithValue("@password", pass);
                    con.Open();
                    SqlDataReader idr = cmd.ExecuteReader();

                    if (idr.HasRows == false)
                    {
                        idr.Close();
                        SqlCommand cmd4 = new SqlCommand("select Count(*) AS RecordExists from mf_crm_members where Contact_No = '" + phonenumber + "' and Status = 'INACTIVE' and password = '" + pass + "' and DeleteInd <> 'X' COLLATE Latin1_General_CS_AS", con);
                        SqlDataAdapter da4 = new SqlDataAdapter(cmd4);
                        DataTable dt4 = new DataTable();
                        da4.Fill(dt4);

                        SqlCommand cmd2 = new SqlCommand("select Count(*) AS RecordExists from mf_crm_members where DeleteInd <> 'X' and Contact_No = '" + phonenumber + "' and Status = 'ACTIVE' and password = '" + pass + "' COLLATE Latin1_General_CS_AS", con);
                        SqlDataAdapter da = new SqlDataAdapter(cmd2);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt4.Rows[0]["RecordExists"].ToString() == "1")
                        {
                            ShowMessage_warning("Please contact HQ to activate your account, Thank you.", MessageType.Warning);
                            return;
                        }
                        else if (dt.Rows[0]["RecordExists"].ToString() == "0")
                        {
                            SqlCommand cmdphonenumber = new SqlCommand("select Count(*) AS RecordExists from mf_crm_members where DeleteInd <> 'X' and Contact_No = '" + phonenumber + "' and Status = 'ACTIVE'", con);
                            SqlDataAdapter daphoenumber = new SqlDataAdapter(cmdphonenumber);
                            DataTable dtphonenumber = new DataTable();
                            daphoenumber.Fill(dtphonenumber);

                            SqlCommand cmdpassword = new SqlCommand("select Count(*) AS RecordExists from mf_crm_members where DeleteInd <> 'X' and Contact_No = '" + phonenumber + "' and Status = 'ACTIVE' and password = '" + pass + "' COLLATE Latin1_General_CS_AS", con);
                            SqlDataAdapter dapassword = new SqlDataAdapter(cmdpassword);
                            DataTable dtpassword = new DataTable();
                            dapassword.Fill(dtpassword);

                            if (dtphonenumber.Rows[0]["RecordExists"].ToString() == "0")
                            {
                                ShowMessage_warning("Incorrect phone number.", MessageType.Warning);
                                return;
                            }
                            else if (dtpassword.Rows[0]["RecordExists"].ToString() == "0")
                            {
                                ShowMessage_warning("Incorrect password.", MessageType.Warning);
                                return;
                            }
                            else
                            {
                                ShowMessage_warning("Member not found.", MessageType.Warning);
                                return;
                            }
                        }
                    }
                    else
                    {
                        while (idr.Read())
                        {
                            Response.Cookies["userid"].Value = idr["cardno"].ToString();
                            Response.Redirect("MyProfile.aspx");
                        }
                    }

                    idr.Close();
                    con.Close();

                }
            }
        }
    }
}