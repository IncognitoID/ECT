using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Web;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web.UI;

public partial class CP58_Listing : System.Web.UI.Page
{
    protected static String ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
    public enum MessageType { Success, Error, Info, Warning };

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsPostBack)
            {
                bool cookieExists = HttpContext.Current.Request.Cookies["userid"] != null;
                if (cookieExists == true)
                {
                    if (Request.Cookies["userid"].Value != null && Request.Cookies["userid"].Value != "")
                    {
                        Load_Member_Report_Year(Request.Cookies["userid"].Value);
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
    }

    protected void Load_Member_Report_Year(string memberid)
    {
        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("Load_Member_CP58_Report", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Memberid", memberid);
                con.Open();

                SqlDataReader idr = cmd.ExecuteReader();

                if (idr.HasRows == true)
                {
                    rpt_year.Visible = true;
                    tr_no_record.Visible = false;
                    DataTable v = new DataTable();

                    v.Load(idr);
                    rpt_year.DataSource = v;
                    rpt_year.DataBind();

                    txt_income_tax_number.Text = v.Rows[0]["Income_Tax_Number"].ToString();
                    ddl_stay.SelectedValue = v.Rows[0]["Stay_In_Malaysia"].ToString();
                }
                else
                {
                    rpt_year.Visible = false;
                    tr_no_record.Visible = true;
                }

                idr.Close();
                con.Close();

            }
        }
    }

    protected void rpt_year_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView drv = (DataRowView)(e.Item.DataItem);

        }
    }

    protected void rpt_year_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Download")
        {
            GeneratePDF(Request.Cookies["userid"].Value, e.CommandArgument.ToString());
        }
    }

    private void GeneratePDF(string member_id, string year)
    {
        // Retrieve values from the database
        string bahagian_a_name_value = "", bahagian_a_address_value = "", bahagian_a_identity_value = "", bahagian_a_income_tax_value = "";
        string bahagian_b_name_value = "", bahagian_b_address_value = "", bahagian_b_identity_value = "", bahagian_b_income_tax_value = "", bahagian_b_stay_in_malaysia_value = "";
        string bahagian_c_commission_value = "", bahagian_c_others_1_value = "", bahagian_c_others_2_value = "", bahagian_c_total_1_value = "";
        string bahagian_c_car_value = "", bahagian_c_house_value = "", bahagian_c_travel_value = "", bahagian_c_others_3_value = "", bahagian_c_others_4_value = "", bahagian_c_total_2_value = "";
        string bahagian_c_final_total_value = "", bahagian_c_tax_deduction_value = "";
        string bahagian_d_name_value = "", bahagian_d_identity_value = "", bahagian_d_position_value = "";

        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("Load_Member_Report_CP58_Bahagian_B", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Member_ID", member_id);
                cmd.Parameters.AddWithValue("@Year", year);

                con.Open();
                SqlDataReader idr = cmd.ExecuteReader();
                if (idr.HasRows && idr.Read())
                {
                    bahagian_a_name_value = idr["Bahagian_A_Name"].ToString();
                    bahagian_a_address_value = idr["Bahagian_A_Address"].ToString();
                    bahagian_a_identity_value = idr["Bahagian_A_Registration_No"].ToString();
                    bahagian_a_income_tax_value = idr["Bahagian_A_Income_Tax"].ToString();
                    bahagian_b_name_value = idr["Bahagian_B_Name"].ToString();
                    bahagian_b_address_value = idr["Bahagian_B_Address"].ToString();
                    bahagian_b_identity_value = idr["Bahagian_B_Identity_No"].ToString();
                    bahagian_b_income_tax_value = idr["Bahagian_B_Income_Tax_No"].ToString();
                    bahagian_b_stay_in_malaysia_value = idr["Bahagian_B_Stay_In_Malaysia"].ToString();
                    bahagian_c_commission_value = idr["Bahagian_B_Commission"].ToString();
                    bahagian_c_others_1_value = idr["Bahagian_B_Others_1"].ToString();
                    bahagian_c_others_2_value = idr["Bahagian_B_Others_2"].ToString();
                    bahagian_c_total_1_value = idr["Bahagian_B_Total_1"].ToString();
                    bahagian_c_car_value = idr["Bahagian_B_Car"].ToString();
                    bahagian_c_house_value = idr["Bahagian_B_House"].ToString();
                    bahagian_c_travel_value = idr["Bahagian_B_Travel"].ToString();
                    bahagian_c_others_3_value = idr["Bahagian_B_Others_3"].ToString();
                    bahagian_c_others_4_value = idr["Bahagian_B_Others_4"].ToString();
                    bahagian_c_total_2_value = idr["Bahagian_B_Total_2"].ToString();
                    bahagian_c_final_total_value = idr["Bahagian_B_Final_Total"].ToString();
                    bahagian_c_tax_deduction_value = idr["Bahagian_B_Tax_Deduction"].ToString();
                    bahagian_d_name_value = idr["Bahagian_D_Name"].ToString();
                    bahagian_d_identity_value = idr["Bahagian_D_Registration_No"].ToString();
                    bahagian_d_position_value = idr["Bahagian_D_Position"].ToString();
                }
                idr.Close();
                con.Close();
            }
        }

        // Generate PDF with custom text
        string templatePath = Server.MapPath("~/CP58/Borang_CP58.pdf");
        byte[] outputPdf;
        using (PdfReader reader = new PdfReader(templatePath))
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (PdfStamper stamper = new PdfStamper(reader, ms))
                {
                    PdfContentByte cb = stamper.GetOverContent(1);
                    BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 12);
                    cb.SetTextMatrix(348, 710);
                    cb.ShowText(year);
                    cb.EndText();

                    // BAHAGIAN A: Payer Info (Top-left, starting ~100 points from top)
                    cb.BeginText();
                    cb.SetFontAndSize(bf, 12);
                    cb.SetTextMatrix(130, 673); // Nama (left margin, near top)
                    cb.ShowText(bahagian_a_name_value);
                    cb.EndText();

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 9);
                    cb.SetTextMatrix(130, 655); // Alamat (below Nama)
                    cb.ShowText(bahagian_a_address_value.Length > 85 ? bahagian_a_address_value.Substring(0, 85) : bahagian_a_address_value);
                    // For long addresses, add more lines if needed
                    if (bahagian_a_address_value.Length > 85)
                    {
                        cb.EndText();
                        cb.BeginText();
                        cb.SetFontAndSize(bf, 9);
                        cb.SetTextMatrix(130, 645);
                        cb.ShowText(bahagian_a_address_value.Substring(85, bahagian_a_address_value.Length - 85 > 85 ? 85 : bahagian_a_address_value.Length - 85));
                    }
                    cb.EndText();

                    // No. Rujukan (split into individual characters for boxes)
                    float startX = 285; // Starting x-coordinate for the first box
                    float y = 631;     // y-coordinate for the boxes
                    float boxWidth = 12; // Estimated width of each box (adjust based on your template)
                    if (!string.IsNullOrEmpty(bahagian_a_identity_value))
                    {
                        for (int i = 0; i < bahagian_a_identity_value.Length && i < 22; i++) // Limit to 10 boxes if applicable
                        {
                            cb.BeginText();
                            cb.SetFontAndSize(bf, 9);
                            cb.SetTextMatrix(startX + (i * boxWidth), y);
                            cb.ShowText(bahagian_a_identity_value[i].ToString());
                            cb.EndText();
                        }
                    }

                    startX = 285; // Starting x-coordinate for the first box
                    y = 620;     // y-coordinate for the boxes
                    boxWidth = 12; // Estimated width of each box (adjust based on your template)
                    if (!string.IsNullOrEmpty(bahagian_a_income_tax_value))
                    {
                        for (int i = 0; i < bahagian_a_income_tax_value.Length && i < 11; i++) // Limit to 10 boxes if applicable
                        {
                            cb.BeginText();
                            cb.SetFontAndSize(bf, 9);
                            cb.SetTextMatrix(startX + (i * boxWidth), y);
                            cb.ShowText(bahagian_a_income_tax_value[i].ToString());
                            cb.EndText();
                        }
                    }

                    // BAHAGIAN B: Recipient Info (Below BAHAGIAN A, ~200 points lower)
                    cb.BeginText();
                    cb.SetFontAndSize(bf, 12);
                    cb.SetTextMatrix(130, 572); // Nama
                    cb.ShowText(bahagian_b_name_value);
                    cb.EndText();

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 9);
                    cb.SetTextMatrix(130, 554); // Alamat
                    cb.ShowText(bahagian_b_address_value.Length > 85 ? bahagian_b_address_value.Substring(0, 85) : bahagian_b_address_value);
                    if (bahagian_b_address_value.Length > 85)
                    {
                        cb.EndText();
                        cb.BeginText();
                        cb.SetFontAndSize(bf, 9);
                        cb.SetTextMatrix(130, 545);
                        cb.ShowText(bahagian_b_address_value.Substring(85, bahagian_b_address_value.Length - 85 > 85 ? 85 : bahagian_b_address_value.Length - 85));
                    }
                    cb.EndText();

                    startX = 285; // Starting x-coordinate for the first box
                    y = 528;     // y-coordinate for the boxes
                    boxWidth = 12; // Estimated width of each box (adjust based on your template)
                    if (!string.IsNullOrEmpty(bahagian_b_identity_value))
                    {
                        for (int i = 0; i < bahagian_b_identity_value.Length && i < 22; i++) // Limit to 10 boxes if applicable
                        {
                            cb.BeginText();
                            cb.SetFontAndSize(bf, 9);
                            cb.SetTextMatrix(startX + (i * boxWidth), y);
                            cb.ShowText(bahagian_b_identity_value[i].ToString());
                            cb.EndText();
                        }
                    }

                    // BAHAGIAN B: Income Tax Number (split into prefix and numbers)
                    startX = 200; // Starting x-coordinate for the first box
                    y = 502;     // y-coordinate for the boxes
                    float number_x = 224;     // y-coordinate for the boxes
                    boxWidth = 12; // Width of each box
                    if (!string.IsNullOrEmpty(bahagian_b_income_tax_value))
                    {
                        // Remove spaces from the value
                        string cleanedValue = bahagian_b_income_tax_value.Replace(" ", "");

                        // Separate prefix (alphabets) and numbers
                        string prefix = "";
                        string numbers = "";
                        int i = 0;
                        while (i < cleanedValue.Length && char.IsLetter(cleanedValue[i]))
                        {
                            prefix += cleanedValue[i];
                            i++;
                        }
                        numbers = cleanedValue.Substring(i); // Remaining part is numbers

                        // Write prefix characters
                        for (int j = 0; j < prefix.Length && j < 2; j++) // Limit prefix to 2 characters (e.g., "SG")
                        {
                            cb.BeginText();
                            cb.SetFontAndSize(bf, 9);
                            cb.SetTextMatrix(startX + (j * boxWidth), y);
                            cb.ShowText(prefix[j].ToString());
                            cb.EndText();
                        }

                        if(prefix.Length == 2)
                        {
                            number_x = 213;
                        }

                        // Write numbers (up to 11 digits)
                        for (int k = 0; k < numbers.Length && k < 11; k++)
                        {
                            cb.BeginText();
                            cb.SetFontAndSize(bf, 9);
                            cb.SetTextMatrix(number_x + ((k + prefix.Length) * boxWidth), y); // Offset by prefix length
                            cb.ShowText(numbers[k].ToString());
                            cb.EndText();
                        }
                    }

                    string stay = "";
                    if (bahagian_b_stay_in_malaysia_value == "Yes")
                    {
                        stay = "1";
                    }
                    else
                    {
                        stay = "2";
                    }

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 10);
                    cb.SetTextMatrix(235, 478); // Mastautin di Malaysia (Yes/No)
                    cb.ShowText(stay);
                    cb.EndText();

                    // BAHAGIAN C: Incentive Payment (Middle, table-like structure)
                    cb.BeginText();
                    cb.SetFontAndSize(bf, 9);
                    cb.SetTextMatrix(458, 427); // Komisen/bonus
                    cb.ShowText(bahagian_c_commission_value);
                    cb.EndText();

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 9);
                    cb.SetTextMatrix(140, 416); // Lain-lain (1)
                    cb.ShowText(bahagian_c_others_1_value);
                    cb.EndText();

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 9);
                    cb.SetTextMatrix(458, 415); // Lain-lain (RM)
                    cb.ShowText(bahagian_c_others_2_value);
                    cb.EndText();

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 9);
                    cb.SetTextMatrix(458, 397); // Lain-lain (RM)
                    cb.ShowText(bahagian_c_total_1_value);
                    cb.EndText();

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 9);
                    cb.SetTextMatrix(458, 365); // Kenderaan
                    cb.ShowText(bahagian_c_car_value);
                    cb.EndText();

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 9);
                    cb.SetTextMatrix(458, 355); // Rumah
                    cb.ShowText(bahagian_c_house_value);
                    cb.EndText();

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 9);
                    cb.SetTextMatrix(458, 344); // Pakej pelancongan
                    cb.ShowText(bahagian_c_travel_value);
                    cb.EndText();

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 9);
                    cb.SetTextMatrix(140, 345); // Lain-lain (3)
                    cb.ShowText(bahagian_c_others_3_value);
                    cb.EndText();

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 9);
                    cb.SetTextMatrix(458, 332); // Lain-lain (RM)
                    cb.ShowText(bahagian_c_others_4_value);
                    cb.EndText();

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 9);
                    cb.SetTextMatrix(458, 312); // Jumlah B
                    cb.ShowText(bahagian_c_total_2_value);
                    cb.EndText();

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 9);
                    cb.SetTextMatrix(458, 288); // Jumlah Keseluruhan
                    cb.ShowText(bahagian_c_final_total_value);
                    cb.EndText();

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 9);
                    cb.SetTextMatrix(458, 260); // Jumlah potongan cukai
                    cb.ShowText(bahagian_c_tax_deduction_value);
                    cb.EndText();

                    // BAHAGIAN D: Payer Declaration (Bottom)
                    cb.BeginText();
                    cb.SetFontAndSize(bf, 12);
                    cb.SetTextMatrix(100, 218); // Saya
                    cb.ShowText(bahagian_d_name_value);
                    cb.EndText();

                    // No. Rujukan (split into individual characters for boxes)
                    startX = 273; // Starting x-coordinate for the first box
                    y = 197;     // y-coordinate for the boxes
                    boxWidth = 12; // Estimated width of each box (adjust based on your template)
                    if (!string.IsNullOrEmpty(bahagian_d_identity_value))
                    {
                        for (int i = 0; i < bahagian_d_identity_value.Length && i < 22; i++) // Limit to 10 boxes if applicable
                        {
                            cb.BeginText();
                            cb.SetFontAndSize(bf, 9);
                            cb.SetTextMatrix(startX + (i * boxWidth), y);
                            cb.ShowText(bahagian_d_identity_value[i].ToString());
                            cb.EndText();
                        }
                    }

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 9);
                    cb.SetTextMatrix(140, 168); // Jawatan
                    cb.ShowText(bahagian_d_position_value);
                    cb.EndText();

                    startX = 444; // Starting x-coordinate for the first box
                    y = 68;     // y-coordinate for the boxes
                    boxWidth = 12; // Estimated width of each box (adjust based on your template)
                    if (!string.IsNullOrEmpty(DateTime.Now.ToString("dd/MM/yy")))
                    {
                        for (int i = 0; i < DateTime.Now.ToString("dd/MM/yy").Length && i < 9; i++) // Limit to 10 boxes if applicable
                        {
                            cb.BeginText();
                            cb.SetFontAndSize(bf, 9);
                            cb.SetTextMatrix(startX + (i * boxWidth), y);
                            cb.ShowText(DateTime.Now.ToString("dd/MM/yy")[i].ToString());
                            cb.EndText();
                        }
                    }

                    stamper.FormFlattening = true;
                }
                outputPdf = ms.ToArray();
            }
            reader.Close();
        }

        // Handle preview or download
        Response.Clear();
        Response.ContentType = "application/pdf";
        Response.AddHeader("Content-Disposition", "attachment; filename=Generated_CP58_" + year + ".pdf");
        Response.BinaryWrite(outputPdf);
        Response.Flush();
        Response.End();
    }

    protected void btn_final_update_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("Update_Tax_Information", con))
            {
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Member_ID", Request.Cookies["userid"].Value);
                cmd.Parameters.AddWithValue("@Tax_Number", txt_income_tax_number.Text);
                cmd.Parameters.AddWithValue("@Stay_In_Malaysia", ddl_stay.SelectedValue);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        ShowMessage("Successful update tax information", MessageType.Success);
    }

    #region Message
    protected void ShowMessage(string Message, MessageType type)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "sweetalert_success('" + Message + "','" + type + "');", true);
    }

    protected void ShowMessage_Place_Order(string Message, MessageType type)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "sweetalert_success_place_order('" + Message + "','" + type + "');", true);
    }

    protected void ShowMessage_warning(string Message, MessageType type)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "sweetalert_warning('" + Message + "','" + type + "');", true);
    }
    #endregion

}