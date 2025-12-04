using QRCoder;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Membership_Card : System.Web.UI.Page
{
    protected static String ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            bool cookieExists = HttpContext.Current.Request.Cookies["userid"] != null;

            if (cookieExists == true)
            {
                if (Request.Cookies["userid"].Value != null && Request.Cookies["userid"].Value != "")
                {
                    Load_Member_Details(Request.Cookies["userid"].Value);

                    string url = "";
                    url = "https://ecentra.com.my/Register?referral_id=" + Request.Cookies["userid"].Value + "&id=";
                    lbl_referralid.Value = url;
                    lbl_referal_link.Value = url;

                    string code = url;
                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                    QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.L);
                    System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
                    imgBarCode.Height = 100;
                    imgBarCode.Width = 100;
                    imgBarCode.CssClass = "mobile_qr";
                    using (Bitmap bitMap = qrCode.GetGraphic(20))
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                            byte[] byteImage = ms.ToArray();
                            imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                        }
                        PlaceHolder1.Controls.Add(imgBarCode);
                    }
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
                        lbl_user_id.InnerText = idr["cardno"].ToString();
                        if (string.IsNullOrEmpty(idr["First_Name"].ToString()))
                        {
                            lbl_user_name.InnerText = idr["Company_Name"].ToString();
                        }
                        else
                        {
                            lbl_user_name.InnerText = idr["First_Name"].ToString();
                        }
                        DateTime createdDate = Convert.ToDateTime(idr["Created_DT"]); // Assuming Created_DT is a DateTime field

                        // Format the DateTime to get only MM/dd
                        string formattedDate = createdDate.ToString("MM/dd/yyyy");

                        lbl_user_join_date.InnerText = formattedDate;
                    }
                }

                idr.Close();
                con.Close();

            }
        }
    }

}