using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Encrypt_All_Password : System.Web.UI.Page
{
    protected static String ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

        }
    }

    protected void btn_encrypt_Click(object sender, EventArgs e)
    {
        #region Load All Member Password
        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("Load_All_Member_Details", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader idr = cmd.ExecuteReader();

                if (idr.HasRows == true)
                {
                    while (idr.Read())
                    {
                        Encrypt_Password(idr["cardno"].ToString(), idr["password"].ToString());
                    }
                }

                idr.Close();
                con.Close();

            }
        }
        #endregion
    }

    protected void Encrypt_Password(string memberid, string password)
    {
        #region Encrypt

        string encryptdata = "";
        // Combine memberid and todaydate into a single string
        string data = password;

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

        #region Update Encrypt Data
        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("Update_All_Member_Password", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Member_ID", memberid);
                cmd.Parameters.AddWithValue("password", encryptdata);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        #endregion
    }
}