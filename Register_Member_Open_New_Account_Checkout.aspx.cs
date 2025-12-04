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
using System.Threading;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


public partial class Register_Member_Open_New_Account_Checkout : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    public enum MessageType { Success, Error, Info, Warning };
    bool GotItem = false;
    bool ItemNotAvailable = false;
    bool Correct_Package = false;
    bool Correct_Member_Details = false;
    bool Complete_Member_Details = false;
    bool Country_Available = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["id"].ToString()))
            {
                Load_Registration_State();
                Load_Nationality();
                Load_Country();

                Load_Cart_Item(Request.QueryString["id"].ToString());
                Load_Member_Details(Request.QueryString["id"].ToString());
                Load_Member_Address(Request.QueryString["id"].ToString());
                Calculate_Shipping_Fees(Request.QueryString["id"].ToString());
                Load_Shipping_Discount(Request.QueryString["id"].ToString());
                Load_Delivery_Option();
                Check_Delivery_Option();
                Load_Cart_Note();
                Load_Checkout_Note();
            }
        }
    }

    #region Registration
    protected void Load_Registration_State()
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
                    ddl_Member_State.Items.Clear();
                    DataTable v = new DataTable();

                    v.Load(idr);
                    ddl_Member_State.DataSource = v;
                    ddl_Member_State.DataTextField = "Statename";
                    ddl_Member_State.DataValueField = "Statecode";
                    ddl_Member_State.DataBind();
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
                            if (Request.Cookies["language"].Value == "Chinese")
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
                    lbl_member_id.Text = idr["cardno"].ToString();
                    if(!string.IsNullOrEmpty(idr["First_Name"].ToString()))
                    {
                        lbl_member_name.Text = idr["First_Name"].ToString();
                    }
                    else
                    {
                        lbl_member_name.Text = idr["Company_Name"].ToString();

                    }
                    lbl_member_contact_no.Text = idr["Contact_No"].ToString();

                    #region Registration Details
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
                    ddl_Member_State.SelectedValue = idr["State"].ToString();
                    ddl_package.Value = idr["Profit_Center"].ToString();
                    txt_registration_city.Value = idr["City"].ToString();
                    txt_address1.Value = idr["Add1"].ToString();
                    txt_address2.Value = idr["Add2"].ToString();
                    txt_registration_postocde.Value = idr["Postcode"].ToString();

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

                    txt_referal_id.Disabled = true;
                    ddl_gender.Disabled = true;
                    txt_phonenumber.Disabled = true;
                    txt_email.Disabled = true;
                    ddl_nationality.Enabled = false;
                    ddl_country.Enabled = false;
                    ddl_Member_State.Enabled = false;
                    ddl_type.Enabled = false;
                    ddl_package.Disabled = true;
                    txt_registration_city.Disabled = true;
                    txt_address1.Disabled = true;
                    txt_address2.Disabled = true;
                    txt_registration_postocde.Disabled = true;
                    txt_password.Disabled = true;
                    txt_confirmpassword.Disabled = true;
                    txt_fullname.Disabled = true;
                    txt_nric.Enabled = false;
                    txt_passport.Enabled = false;
                    txt_companyname.Disabled = true;
                    txt_companyno.Disabled = true;
                    ddl_member_placement.Enabled = false;
                    #endregion

                }
            }

            idr.Close();
            con.Close();

        }
    }

    protected void btn_edit_Click(object sender, EventArgs e)
    {
        ddl_gender.Disabled = false;
        txt_phonenumber.Disabled = false;
        txt_email.Disabled = false;
        ddl_nationality.Enabled = true;
        ddl_country.Enabled = true;
        ddl_Member_State.Enabled = true;
        ddl_type.Enabled = true;
        ddl_package.Disabled = true;
        txt_registration_city.Disabled = false;
        txt_address1.Disabled = false;
        txt_address2.Disabled = false;
        txt_registration_postocde.Disabled = false;
        txt_password.Disabled = false;
        txt_confirmpassword.Disabled = false;
        txt_fullname.Disabled = false;
        txt_nric.Enabled = true;
        txt_passport.Enabled = true;
        txt_companyname.Disabled = false;
        txt_companyno.Disabled = false;
        ddl_member_placement.Enabled = true;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "Set_Up_Language_and_btn_placement();", true);
    }
    #endregion

    protected void Load_Cart_Note()
    {
        using (SqlCommand cmd = new SqlCommand("Load_Policy", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("id", "8");
            con.Open();
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows == true)
            {
                DataTable v = new DataTable();
                v.Load(idr);

                lbl_cart_note.Text = v.Rows[0]["Content"].ToString();
            }

            idr.Close();
            con.Close();
        }
    }

    protected void Load_Checkout_Note()
    {
        using (SqlCommand cmd = new SqlCommand("Load_Policy", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("id", "7");
            con.Open();
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows == true)
            {
                DataTable v = new DataTable();
                v.Load(idr);

                lbl_checkout_note.Text = v.Rows[0]["Content"].ToString();
            }

            idr.Close();
            con.Close();
        }
    }

    #region Delivery

    protected void Load_Delivery_Option()
    {
        using (SqlCommand cmd = new SqlCommand("Load_Delivery_Option", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows == true)
            {
                DataTable v = new DataTable();
                v.Load(idr);

                ddl_delivery_option.DataSource = v;
                ddl_delivery_option.DataTextField = "Delivery_Option_Value";
                ddl_delivery_option.DataValueField = "Delivery_Option_Value";
                ddl_delivery_option.DataBind();
                ddl_delivery_option.SelectedValue = "Delivery";
            }

            idr.Close();
            con.Close();
        }
    }

    protected void Calculate_Shipping_Fees(string memberid)
    {
        DataTable cart = new DataTable();
        DataTable dt_shipping_fees = new DataTable();
        decimal total_weight = 0;
        decimal total_shipping_fees = 0;
        decimal default_shipping_fees = 0;

        if (ddl_delivery_option.SelectedValue == "Delivery")
        {
            using (SqlCommand cmd = new SqlCommand("Load_Registration_Member_Cart", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Memberid", memberid);
                con.Open();

                SqlDataReader idr = cmd.ExecuteReader();

                if (idr.HasRows == true)
                {
                    cart.Load(idr);
                }

                idr.Close();
                con.Close();
            }

            foreach (DataRow dr in cart.Rows)
            {
                if (string.IsNullOrEmpty(dr["Variation"].ToString()))
                {
                    total_weight = total_weight + (Convert.ToDecimal(dr["Product_Weight"].ToString()) * (Convert.ToDecimal(dr["Normal_Quantity"].ToString()) + Convert.ToDecimal(dr["DC_Quantity"].ToString())));
                }
                else
                {
                    total_weight = total_weight + (Convert.ToDecimal(dr["Variation_Weight"].ToString()) * (Convert.ToDecimal(dr["Normal_Quantity"].ToString()) + Convert.ToDecimal(dr["DC_Quantity"].ToString())));
                }
            }
            total_weight = Math.Ceiling(total_weight / 1000) * 1000;

            using (SqlCommand cmd = new SqlCommand("Load_Registration_Member_Shipping_Fees", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Member_ID", memberid);
                con.Open();

                SqlDataReader idr = cmd.ExecuteReader();

                if (idr.HasRows == true)
                {
                    dt_shipping_fees.Load(idr);
                }

                idr.Close();
                con.Close();
            }

            foreach (DataRow dr in dt_shipping_fees.Rows)
            {
                decimal shipping_fees = 0;
                decimal first_kg = Convert.ToDecimal(dr["First_kg"].ToString());
                decimal First_kg_price = Convert.ToDecimal(dr["ShippingFeesFirst_kg"].ToString());
                decimal Sub_1kg_price = Convert.ToDecimal(dr["ShippingFeesSub_1kg"].ToString());

                if ((total_weight / 1000) <= first_kg)
                {
                    shipping_fees = First_kg_price;
                }
                else
                {
                    decimal second_kg = (Math.Ceiling(total_weight / 1000) - first_kg);
                    shipping_fees = First_kg_price + (Sub_1kg_price * second_kg);
                }

                if (ddl_package.Value == "3" && dr["Region"].ToString() != "EM")
                {
                    total_shipping_fees = 0;
                }
                else
                {
                    total_shipping_fees = total_shipping_fees + shipping_fees;
                }
            }
        }
        else if (ddl_delivery_option.SelectedValue == "Self Pickup")
        {
            total_shipping_fees = 0;
        }

        default_shipping_fees = Convert.ToDecimal(lbl_total_shipping.Text.Replace("RM ", ""));
        lbl_total_shipping.Text = "RM " + total_shipping_fees.ToString("###,###,##0.00");
    }

    protected void ddl_delivery_option_SelectedIndexChanged(object sender, EventArgs e)
    {
        Check_Delivery_Option();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "Set_Up_Language();", true);
    }

    protected void Check_Delivery_Option()
    {
        if (ddl_delivery_option.SelectedValue == "Delivery")
        {
            Calculate_Shipping_Fees(Request.QueryString["id"].ToString());
            Load_Shipping_Discount(Request.QueryString["id"].ToString());
            div_delivery.Visible = true;
            div_self_pickup.Visible = false;
        }
        else if (ddl_delivery_option.SelectedValue == "Self Pickup")
        {
            Load_Pickup_Store_Information();
            Calculate_Shipping_Fees(Request.QueryString["id"].ToString());
            Load_Shipping_Discount(Request.QueryString["id"].ToString());
            div_delivery.Visible = false;
            div_self_pickup.Visible = true;
        }
    }

    protected void Load_Pickup_Store_Information()
    {
        using (SqlCommand cmd = new SqlCommand("Load_Pickup_Store_Information", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows == true)
            {
                while (idr.Read())
                {
                    lbl_person_in_charge.Text = idr["PIC"].ToString();
                    lbl_person_in_charge_phone_number.Text = idr["PhoneNumber"].ToString();
                    lbl_pickup_address.Text = idr["Address"].ToString();
                    lbl_operation_time.Text = idr["Operation_Time"].ToString();
                }
            }

            idr.Close();
            con.Close();

        }
    }

    protected void Load_Shipping_Discount(string memberid)
    {
        DataTable cart = new DataTable();
        DataTable dt_shipping_fees = new DataTable();
        decimal discount_shipping_fees = 0;
        decimal minimal_purchase = 0;
        decimal default_shipping_fees = 0;

        if (ddl_delivery_option.SelectedValue == "Delivery")
        {
            using (SqlCommand cmd = new SqlCommand("Load_Registration_Member_Shipping_Fees_Discount", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Member_ID", memberid);
                con.Open();

                SqlDataReader idr = cmd.ExecuteReader();

                if (idr.HasRows == true)
                {
                    dt_shipping_fees.Load(idr);
                }

                idr.Close();
                con.Close();
            }

            if (dt_shipping_fees.Rows.Count > 0)
            {
                foreach (DataRow dr in dt_shipping_fees.Rows)
                {
                    discount_shipping_fees = Convert.ToDecimal(dr["Shipping_Fees_Discount"].ToString());
                    minimal_purchase = Convert.ToDecimal(dr["Minimal_Purchase_Amount"].ToString());
                }
            }
            else
            {
                discount_shipping_fees = 0;
            }
            decimal sub_total = 0;
            sub_total = Convert.ToDecimal(lbl_sub_total.Text.Replace("RM ", "").Replace(",", ""));
            if (sub_total >= minimal_purchase)
            {
                default_shipping_fees = Convert.ToDecimal(lbl_total_shipping.Text.Replace("RM ", ""));
                if (default_shipping_fees > discount_shipping_fees)
                {
                    lbl_total_shipping_discount.Text = "RM " + discount_shipping_fees.ToString("###,###,##0.00");
                }
                else
                {
                    lbl_total_shipping_discount.Text = "RM " + default_shipping_fees.ToString("###,###,##0.00");
                }
            }
            else
            {
                discount_shipping_fees = 0;
                lbl_total_shipping_discount.Text = "RM " + discount_shipping_fees.ToString("###,###,##0.00");
            }
        }
        else if (ddl_delivery_option.SelectedValue == "Self Pickup")
        {
            discount_shipping_fees = 0;
            lbl_total_shipping_discount.Text = "RM " + discount_shipping_fees.ToString("###,###,##0.00");
        }
        decimal shipping_fees = Convert.ToDecimal(lbl_total_shipping.Text.Replace("RM ", ""));
        decimal shipping_fees_discount = Convert.ToDecimal(lbl_total_shipping_discount.Text.Replace("RM ", ""));
        lbl_total_amout.Text = "RM " + ((Convert.ToDecimal(lbl_sub_total.Text.Replace("RM ", "").Replace(",", "")) + shipping_fees) - shipping_fees_discount).ToString("###,###,##0.00");

    }

    #endregion

    #region Cart

    protected void Load_Cart_Item(string memberid)
    {
        lbl_total_qty.Text = "0";
        lbl_total_bv.Text = "0";
        lbl_total_dc_used.Text = "0";
        lbl_total_dc_earn.Text = "0";
        lbl_sub_total.Text = "RM 0.00";
        lbl_total_shipping_discount.Text = "RM 0.00";
        lbl_total_amout.Text = "RM 0.00";

        using (SqlCommand cmd = new SqlCommand("Load_Registration_Member_Cart", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Memberid", memberid);
            con.Open();

            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows == true)
            {
                rpt_cart.Visible = true;
                tr_no_record.Visible = false;
                DataTable v = new DataTable();

                v.Load(idr);
                rpt_cart.DataSource = v;
                rpt_cart.DataBind();
            }
            else
            {
                rpt_cart.Visible = false;
                tr_no_record.Visible = true;
                div_customer_info.Visible = false;
            }

            idr.Close();
            con.Close();

        }
    }

    protected void rpt_cart_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        HtmlGenericControl div_mask = (HtmlGenericControl)e.Item.FindControl("div_mask");
        HtmlGenericControl lbl_mask_title = (HtmlGenericControl)e.Item.FindControl("lbl_mask_title");
        HtmlImage img_product = (HtmlImage)e.Item.FindControl("img_product");
        Label lbl_product_name = (Label)e.Item.FindControl("lbl_product_name");
        Label lbl_product_variation = (Label)e.Item.FindControl("lbl_product_variation");
        Label lbl_product_price = (Label)e.Item.FindControl("lbl_product_price");
        Label lbl_qty = (Label)e.Item.FindControl("lbl_qty");
        Label lbl_dc_used = (Label)e.Item.FindControl("lbl_dc_used");
        Label lbl_dc_price = (Label)e.Item.FindControl("lbl_dc_price");
        Label lbl_dc_qty = (Label)e.Item.FindControl("lbl_dc_qty");
        Label lbl_variation = (Label)e.Item.FindControl("lbl_variation");
        Label lbl_total_weight = (Label)e.Item.FindControl("lbl_total_weight");
        Label lbl_total_amount = (Label)e.Item.FindControl("lbl_total_amount");
        Label lbl_bv_earn = (Label)e.Item.FindControl("lbl_bv_earn");
        Label lbl_dc_earn = (Label)e.Item.FindControl("lbl_dc_earn");
        Button btn_edit = (Button)e.Item.FindControl("btn_edit");
        Button btn_delete = (Button)e.Item.FindControl("btn_delete");

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView drv = (DataRowView)(e.Item.DataItem);
            bool package_quantity_available = true;
            bool package_available = true;
            bool package_publish = true;
            bool package_not_deleted = true;

            lbl_product_name.Text = drv.Row["Product_Name"].ToString();

            if (string.IsNullOrEmpty(drv.Row["Variation"].ToString()))
            {
                lbl_total_weight.Text = ((Convert.ToDecimal(drv.Row["Normal_Quantity"].ToString()) + Convert.ToDecimal(drv.Row["DC_Quantity"].ToString())) * Convert.ToDecimal(drv.Row["Product_Weight"].ToString())).ToString("###,###,##0.00");

                img_product.Src = "https://ecentra.com.my/Backoffice/" + drv["Product_Image"].ToString();

                decimal normal_bv = 0;
                decimal promotion_bv = 0;
                lbl_product_variation.Text = "-";

                if (drv.Row["Normal_Quantity"].ToString() == "0")
                {
                    lbl_product_price.Text = "RM 0.00";
                    lbl_qty.Text = "0";
                    normal_bv = 0;
                }
                else
                {
                    lbl_product_price.Text = "RM " + (Convert.ToDecimal(drv.Row["Product_Retail_Price"].ToString()) * Convert.ToDecimal(drv.Row["Normal_Quantity"].ToString())).ToString("###,###,###.00");
                    lbl_qty.Text = drv.Row["Normal_Quantity"].ToString();
                    normal_bv = Convert.ToDecimal(drv.Row["Product_Retail_Price_BV_Points"].ToString()) * Convert.ToDecimal(drv.Row["Normal_Quantity"].ToString());
                }

                if (drv.Row["DC_Quantity"].ToString() == "0")
                {
                    lbl_dc_used.Text = "0";
                    lbl_dc_price.Text = "RM 0.00";
                    lbl_dc_qty.Text = "0";
                    promotion_bv = 0;
                }
                else
                {
                    lbl_dc_used.Text = (Convert.ToDecimal(drv.Row["Product_Promotion_Price_EC_Points"].ToString()) * Convert.ToDecimal(drv.Row["DC_Quantity"].ToString())).ToString("###,###,###.00");
                    lbl_dc_price.Text = "RM " + (Convert.ToDecimal(drv.Row["Product_Promotion_Price_Selling_Price"].ToString()) * Convert.ToDecimal(drv.Row["DC_Quantity"].ToString())).ToString("###,###,##0.00");
                    lbl_dc_qty.Text = drv.Row["DC_Quantity"].ToString();
                    promotion_bv = Convert.ToDecimal(drv.Row["Product_Promotion_Price_BV_Points"].ToString()) * Convert.ToDecimal(drv.Row["DC_Quantity"].ToString());
                }

                lbl_total_amount.Text = "RM " + (Convert.ToDecimal(lbl_product_price.Text.Replace("RM ", "")) + Convert.ToDecimal(lbl_dc_price.Text.Replace("RM ", ""))).ToString("###,###,##0.00");
                lbl_bv_earn.Text = (normal_bv + promotion_bv).ToString("###,###,##0");

                decimal product_total = Convert.ToDecimal(lbl_product_price.Text.Replace("RM ", ""));
                decimal conversionRate = Convert.ToDecimal(drv.Row["Min_Point_Earn"].ToString());
                decimal pointsEarned = 0;
                if (drv.Row["Fix_DC"].ToString() == "Y")
                {
                    pointsEarned = Convert.ToDecimal(drv.Row["Fix_DC_Amount"].ToString()) * (Convert.ToDecimal(drv.Row["Normal_Quantity"].ToString()) + Convert.ToDecimal(drv.Row["DC_Quantity"].ToString()));
                }
                else
                {
                    pointsEarned = Math.Floor(product_total / Convert.ToDecimal(drv.Row["Min_Spend"].ToString())) * conversionRate;
                }
                lbl_dc_earn.Text = pointsEarned.ToString("###,###,##0");

            }
            else
            {
                lbl_total_weight.Text = ((Convert.ToDecimal(drv.Row["Normal_Quantity"].ToString()) + Convert.ToDecimal(drv.Row["DC_Quantity"].ToString())) * Convert.ToDecimal(drv.Row["Variation_Weight"].ToString())).ToString("###,###,##0.00");

                img_product.Src = "https://ecentra.com.my/Backoffice/" + drv["Variation_Image"].ToString();

                decimal normal_bv = 0;
                decimal promotion_bv = 0;
                lbl_product_variation.Text = drv.Row["Variation_Description"].ToString();

                if (drv.Row["Normal_Quantity"].ToString() == "0")
                {
                    lbl_product_price.Text = "RM 0.00";
                    lbl_qty.Text = "0";
                    normal_bv = 0;
                }
                else
                {
                    lbl_product_price.Text = "RM " + (Convert.ToDecimal(drv.Row["Variation_Retail_Price"].ToString()) * Convert.ToDecimal(drv.Row["Normal_Quantity"].ToString())).ToString("###,###,###.00");
                    lbl_qty.Text = drv.Row["Normal_Quantity"].ToString();
                    normal_bv = Convert.ToDecimal(drv.Row["Variation_Retail_Price_BV_Points"].ToString()) * Convert.ToDecimal(drv.Row["Normal_Quantity"].ToString());
                }

                if (drv.Row["DC_Quantity"].ToString() == "0")
                {
                    lbl_dc_used.Text = "0";
                    lbl_dc_price.Text = "RM 0.00";
                    lbl_dc_qty.Text = "0";
                    promotion_bv = 0;
                }
                else
                {
                    lbl_dc_used.Text = (Convert.ToDecimal(drv.Row["Variation_Promotion_Price_EC_Points"].ToString()) * Convert.ToDecimal(drv.Row["DC_Quantity"].ToString())).ToString("###,###,###.00");
                    lbl_dc_price.Text = "RM " + (Convert.ToDecimal(drv.Row["Variation_Promotion_Price_Selling_Price"].ToString()) * Convert.ToDecimal(drv.Row["DC_Quantity"].ToString())).ToString("###,###,###.00");
                    lbl_dc_qty.Text = drv.Row["DC_Quantity"].ToString();
                    promotion_bv = Convert.ToDecimal(drv.Row["Variation_Promotion_Price_BV_Points"].ToString()) * Convert.ToDecimal(drv.Row["DC_Quantity"].ToString());
                }

                lbl_total_amount.Text = "RM " + (Convert.ToDecimal(lbl_product_price.Text.Replace("RM ", "")) + Convert.ToDecimal(lbl_dc_price.Text.Replace("RM ", ""))).ToString("###,###,###.00");
                lbl_bv_earn.Text = (normal_bv + promotion_bv).ToString("###,###,###");

                decimal product_total = Convert.ToDecimal(lbl_product_price.Text.Replace("RM ", ""));
                decimal conversionRate = Convert.ToDecimal(drv.Row["Min_Point_Earn"].ToString());
                decimal pointsEarned = 0;
                if (drv.Row["Fix_DC"].ToString() == "Y")
                {
                    pointsEarned = Convert.ToDecimal(drv.Row["Fix_DC_Amount"].ToString()) * (Convert.ToDecimal(drv.Row["Normal_Quantity"].ToString()) + Convert.ToDecimal(drv.Row["DC_Quantity"].ToString()));
                }
                else
                {
                    pointsEarned = Math.Floor(product_total / Convert.ToDecimal(drv.Row["Min_Spend"].ToString())) * conversionRate;
                }
                lbl_dc_earn.Text = pointsEarned.ToString("###,###,##0");
            }

            lbl_total_qty.Text = (Convert.ToInt32(lbl_total_qty.Text) + Convert.ToInt32(drv.Row["Normal_Quantity"].ToString()) + Convert.ToInt32(drv.Row["DC_Quantity"].ToString())).ToString();
            lbl_total_bv.Text = (Convert.ToInt32(lbl_total_bv.Text) + Convert.ToInt32(Decimal.Truncate(Convert.ToDecimal(lbl_bv_earn.Text)))).ToString();
            lbl_total_dc_used.Text = (Convert.ToInt32(lbl_total_dc_used.Text) + Convert.ToInt32(Decimal.Truncate(Convert.ToDecimal(lbl_dc_used.Text)))).ToString();
            lbl_total_dc_earn.Text = (Convert.ToInt32(lbl_total_dc_earn.Text.Replace(",", "")) + Convert.ToInt32(lbl_dc_earn.Text.Replace(",", ""))).ToString();
            decimal subTotal = Convert.ToDecimal(lbl_sub_total.Text.Replace("RM ", ""));
            decimal totalAmount = Convert.ToDecimal(lbl_total_amount.Text.Replace("RM ", "").Replace(",", ""));

            lbl_sub_total.Text = "RM " + (subTotal + totalAmount).ToString("###,###,###.00");

            decimal total = Convert.ToDecimal(lbl_sub_total.Text.Replace("RM ", "").Replace(",", ""));

            lbl_total_amout.Text = "RM " + (total).ToString("###,###,###.00");

            DataTable Check_Package_Item_Available = new DataTable();
            if (drv.Row["Package"].ToString() == "Y")
            {
                using (SqlCommand cmd = new SqlCommand("Check_Package_Item", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Product_Code", drv.Row["Product_Code"].ToString());
                    SqlDataAdapter idr = new SqlDataAdapter(cmd);

                    idr.Fill(Check_Package_Item_Available);
                }
            }

            if (Check_Package_Item_Available.Rows.Count > 0)
            {
                foreach (DataRow dt in Check_Package_Item_Available.Rows)
                {
                    if (dt["Package_Item_Quantity_Available_Status"].ToString() == "OUT OF STOCK")
                    {
                        package_quantity_available = false;
                    }
                    if (dt["Package_Item_Sold_Out_Status"].ToString() == "SOLD OUT")
                    {
                        package_available = false;
                    }
                    if (dt["Package_Item_Publish_Status"].ToString() == "UNPUBLISH")
                    {
                        package_publish = false;
                    }
                    if (dt["Package_Item_Deleted_Status"].ToString() == "DELETED")
                    {
                        package_not_deleted = false;
                    }
                }
            }

            bool cookieExists = HttpContext.Current.Request.Cookies["language"] != null;
            if (cookieExists == true)
            {
                if (Request.Cookies["language"].Value != null && Request.Cookies["language"].Value != "")
                {
                    if (Request.Cookies["language"].Value == "Chinese")
                    {
                        btn_edit.Text = "编辑";
                        btn_delete.Text = "删除";

                        if (drv.Row["Product_Deleted"].ToString() == "X")
                        {
                            div_mask.Attributes.Add("style", "display:block");
                            lbl_mask_title.InnerText = "未上架";
                            btn_edit.Enabled = false;
                        }
                        else if (drv.Row["Publish_Status"].ToString() == "Unpublish")
                        {
                            div_mask.Attributes.Add("style", "display:block");
                            lbl_mask_title.InnerText = "未上架";
                        }
                        else if (drv.Row["Variation_Deleted"].ToString() == "X")
                        {
                            div_mask.Attributes.Add("style", "display:block");
                            lbl_mask_title.InnerText = "未上架";
                            btn_edit.Enabled = false;
                        }
                        else if (drv.Row["Sold_Out_Status"].ToString() == "Y")
                        {
                            div_mask.Attributes.Add("style", "display:block");
                            lbl_mask_title.InnerText = "售罄";
                            btn_edit.Enabled = false;
                        }
                        else if (drv.Row["Available_Qty"].ToString() == "0.00" && string.IsNullOrEmpty(drv.Row["Variation"].ToString()))
                        {
                            div_mask.Attributes.Add("style", "display:block");
                            lbl_mask_title.InnerText = "缺货";
                        }
                        else if (drv.Row["Variation_Available_Quantity"].ToString() == "0.00" && !string.IsNullOrEmpty(drv.Row["Variation"].ToString()))
                        {
                            div_mask.Attributes.Add("style", "display:block");
                            lbl_mask_title.InnerText = "缺货";
                        }
                        else if (package_quantity_available == false)
                        {
                            div_mask.Attributes.Add("style", "display:block");
                            lbl_mask_title.InnerText = "缺货";
                            btn_edit.Enabled = false;
                        }
                        else if (package_available == false)
                        {
                            div_mask.Attributes.Add("style", "display:block");
                            lbl_mask_title.InnerText = "缺货";
                            btn_edit.Enabled = false;
                        }
                        else if (package_publish == false)
                        {
                            div_mask.Attributes.Add("style", "display:block");
                            lbl_mask_title.InnerText = "缺货";
                            btn_edit.Enabled = false;
                        }
                        else if (package_not_deleted == false)
                        {
                            div_mask.Attributes.Add("style", "display:block");
                            lbl_mask_title.InnerText = "缺货";
                            btn_edit.Enabled = false;
                        }

                    }
                    else
                    {
                        btn_edit.Text = "Edit";
                        btn_delete.Text = "Delete";

                        if (drv.Row["Product_Deleted"].ToString() == "X")
                        {
                            div_mask.Attributes.Add("style", "display:block");
                            lbl_mask_title.InnerText = "NOT AVAILABLE";
                            btn_edit.Enabled = false;
                        }
                        else if (drv.Row["Publish_Status"].ToString() == "Unpublish")
                        {
                            div_mask.Attributes.Add("style", "display:block");
                            lbl_mask_title.InnerText = "NOT AVAILABLE";
                        }
                        else if (drv.Row["Variation_Deleted"].ToString() == "X")
                        {
                            div_mask.Attributes.Add("style", "display:block");
                            lbl_mask_title.InnerText = "NOT AVAILABLE";
                            btn_edit.Enabled = false;
                        }
                        else if (drv.Row["Sold_Out_Status"].ToString() == "Y")
                        {
                            div_mask.Attributes.Add("style", "display:block");
                            lbl_mask_title.InnerText = "SOLD OUT";
                            btn_edit.Enabled = false;
                        }
                        else if (drv.Row["Available_Qty"].ToString() == "0.00" && string.IsNullOrEmpty(drv.Row["Variation"].ToString()))
                        {
                            div_mask.Attributes.Add("style", "display:block");
                            lbl_mask_title.InnerText = "OUT OF STOCK";
                        }
                        else if (drv.Row["Variation_Available_Quantity"].ToString() == "0.00" && !string.IsNullOrEmpty(drv.Row["Variation"].ToString()))
                        {
                            div_mask.Attributes.Add("style", "display:block");
                            lbl_mask_title.InnerText = "OUT OF STOCK";
                        }
                        else if (package_quantity_available == false)
                        {
                            div_mask.Attributes.Add("style", "display:block");
                            lbl_mask_title.InnerText = "OUT OF STOCK";
                            btn_edit.Enabled = false;
                        }
                        else if (package_available == false)
                        {
                            div_mask.Attributes.Add("style", "display:block");
                            lbl_mask_title.InnerText = "OUT OF STOCK";
                            btn_edit.Enabled = false;
                        }
                        else if (package_publish == false)
                        {
                            div_mask.Attributes.Add("style", "display:block");
                            lbl_mask_title.InnerText = "OUT OF STOCK";
                            btn_edit.Enabled = false;
                        }
                        else if (package_not_deleted == false)
                        {
                            div_mask.Attributes.Add("style", "display:block");
                            lbl_mask_title.InnerText = "OUT OF STOCK";
                            btn_edit.Enabled = false;
                        }
                    }
                }
                else
                {
                    btn_edit.Text = "Edit";
                    btn_delete.Text = "Delete";

                    if (drv.Row["Product_Deleted"].ToString() == "X")
                    {
                        div_mask.Attributes.Add("style", "display:block");
                        lbl_mask_title.InnerText = "NOT AVAILABLE";
                        btn_edit.Enabled = false;
                    }
                    else if (drv.Row["Publish_Status"].ToString() == "Unpublish")
                    {
                        div_mask.Attributes.Add("style", "display:block");
                        lbl_mask_title.InnerText = "NOT AVAILABLE";
                    }
                    else if (drv.Row["Variation_Deleted"].ToString() == "X")
                    {
                        div_mask.Attributes.Add("style", "display:block");
                        lbl_mask_title.InnerText = "NOT AVAILABLE";
                        btn_edit.Enabled = false;
                    }
                    else if (drv.Row["Sold_Out_Status"].ToString() == "Y")
                    {
                        div_mask.Attributes.Add("style", "display:block");
                        lbl_mask_title.InnerText = "SOLD OUT";
                        btn_edit.Enabled = false;
                    }
                    else if (drv.Row["Available_Qty"].ToString() == "0.00" && string.IsNullOrEmpty(drv.Row["Variation"].ToString()))
                    {
                        div_mask.Attributes.Add("style", "display:block");
                        lbl_mask_title.InnerText = "OUT OF STOCK";
                    }
                    else if (drv.Row["Variation_Available_Quantity"].ToString() == "0.00" && !string.IsNullOrEmpty(drv.Row["Variation"].ToString()))
                    {
                        div_mask.Attributes.Add("style", "display:block");
                        lbl_mask_title.InnerText = "OUT OF STOCK";
                    }
                    else if (package_quantity_available == false)
                    {
                        div_mask.Attributes.Add("style", "display:block");
                        lbl_mask_title.InnerText = "OUT OF STOCK";
                        btn_edit.Enabled = false;
                    }
                    else if (package_available == false)
                    {
                        div_mask.Attributes.Add("style", "display:block");
                        lbl_mask_title.InnerText = "OUT OF STOCK";
                        btn_edit.Enabled = false;
                    }
                    else if (package_publish == false)
                    {
                        div_mask.Attributes.Add("style", "display:block");
                        lbl_mask_title.InnerText = "OUT OF STOCK";
                        btn_edit.Enabled = false;
                    }
                    else if (package_not_deleted == false)
                    {
                        div_mask.Attributes.Add("style", "display:block");
                        lbl_mask_title.InnerText = "OUT OF STOCK";
                        btn_edit.Enabled = false;
                    }
                }
            }
            else
            {
                btn_edit.Text = "Edit";
                btn_delete.Text = "Delete";

                if (drv.Row["Product_Deleted"].ToString() == "X")
                {
                    div_mask.Attributes.Add("style", "display:block");
                    lbl_mask_title.InnerText = "NOT AVAILABLE";
                    btn_edit.Enabled = false;
                }
                else if (drv.Row["Publish_Status"].ToString() == "Unpublish")
                {
                    div_mask.Attributes.Add("style", "display:block");
                    lbl_mask_title.InnerText = "NOT AVAILABLE";
                }
                else if (drv.Row["Variation_Deleted"].ToString() == "X")
                {
                    div_mask.Attributes.Add("style", "display:block");
                    lbl_mask_title.InnerText = "NOT AVAILABLE";
                    btn_edit.Enabled = false;
                }
                else if (drv.Row["Sold_Out_Status"].ToString() == "Y")
                {
                    div_mask.Attributes.Add("style", "display:block");
                    lbl_mask_title.InnerText = "SOLD OUT";
                    btn_edit.Enabled = false;
                }
                else if (drv.Row["Available_Qty"].ToString() == "0.00" && string.IsNullOrEmpty(drv.Row["Variation"].ToString()))
                {
                    div_mask.Attributes.Add("style", "display:block");
                    lbl_mask_title.InnerText = "OUT OF STOCK";
                }
                else if (drv.Row["Variation_Available_Quantity"].ToString() == "0.00" && !string.IsNullOrEmpty(drv.Row["Variation"].ToString()))
                {
                    div_mask.Attributes.Add("style", "display:block");
                    lbl_mask_title.InnerText = "OUT OF STOCK";
                }
                else if (package_quantity_available == false)
                {
                    div_mask.Attributes.Add("style", "display:block");
                    lbl_mask_title.InnerText = "OUT OF STOCK";
                    btn_edit.Enabled = false;
                }
                else if (package_available == false)
                {
                    div_mask.Attributes.Add("style", "display:block");
                    lbl_mask_title.InnerText = "OUT OF STOCK";
                    btn_edit.Enabled = false;
                }
                else if (package_publish == false)
                {
                    div_mask.Attributes.Add("style", "display:block");
                    lbl_mask_title.InnerText = "OUT OF STOCK";
                    btn_edit.Enabled = false;
                }
                else if (package_not_deleted == false)
                {
                    div_mask.Attributes.Add("style", "display:block");
                    lbl_mask_title.InnerText = "OUT OF STOCK";
                    btn_edit.Enabled = false;
                }
            }
        }
    }

    protected void rpt_cart_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            // Get the Product_Code from the CommandArgument
            string productCode = e.CommandArgument.ToString();

            // Find the TextBox within the RepeaterItem
            Label lbl_qty = e.Item.FindControl("lbl_qty") as Label;

            Load_Product_Variation(e.CommandArgument.ToString(), lbl_qty.Text);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "Show_Variation();", true);
        }

        if (e.CommandName == "Delete")
        {
            using (SqlCommand cmd = new SqlCommand("Delete_Registration_Cart_Item", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("ID", e.CommandArgument.ToString());
                cmd.Parameters.AddWithValue("Member_ID", Request.QueryString["id"].ToString());

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            Load_Cart_Item(Request.QueryString["id"].ToString());
            Calculate_Shipping_Fees(Request.QueryString["id"].ToString());
            Load_Shipping_Discount(Request.QueryString["id"].ToString());
            ShowMessage("Successful delete cart", MessageType.Success);
        }

    }

    protected void Load_Product_Variation(string Cart_ID, string quantity)
    {
        bool variation = false;
        string variation_value = "";
        string variation_name = "";
        using (SqlCommand cmd = new SqlCommand("Load_Front_End_Registration_Checkout_Product_Details", con))
        {
            con.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Cart_ID", Cart_ID);
            cmd.Parameters.AddWithValue("@Member_ID", Request.QueryString["id"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                hdn_item_code.Value = dt.Rows[0]["Product_Code"].ToString();
                hdn_cart_ids.Value = Cart_ID;
                lbl_user_dc.Text = Convert.ToDecimal(dt.Rows[0]["DC_Point"].ToString()).ToString("###,###,##0");
                if (string.IsNullOrEmpty(dt.Rows[0]["Cart_Variation_Value"].ToString()))
                {
                    lbl_retail_price.Text = "RM " + Convert.ToDecimal(dt.Rows[0]["Product_Retail_Price"]).ToString("###,###,###.00");
                    lbl_retail_price_bv.Text = Convert.ToDecimal(dt.Rows[0]["Product_Retail_Price_BV_Points"]).ToString("###,###,##0");
                    if (dt.Rows[0]["Promotion"].ToString() == "Y")
                    {
                        div_promotion.Attributes.Add("style", "display: block");
                        lbl_point_balance.Attributes.Add("style", "display: contents");
                        lbl_promotion_price.Text = "RM " + Convert.ToDecimal(dt.Rows[0]["Product_Promotion_Price_Selling_Price"]).ToString("###,###,###.00") + " + " + Convert.ToInt32(dt.Rows[0]["Product_Promotion_Price_EC_Points"]).ToString() + "DC";
                        lbl_promotion_price_bv.Text = Convert.ToDecimal(dt.Rows[0]["Product_Promotion_Price_BV_Points"]).ToString("###,###,##0");
                    }
                    else
                    {
                        div_promotion.Attributes.Add("style", "display: none");
                        lbl_point_balance.Attributes.Add("style", "display: none");
                    }
                    int total_qty = 0;
                    total_qty = (Convert.ToInt32(dt.Rows[0]["Normal_Quantity"].ToString()) + Convert.ToInt32(dt.Rows[0]["DC_Quantity"].ToString()));
                    lbl_variation_name.Text = dt.Rows[0]["Variation_Name"].ToString();
                    txtQuantity1.Text = dt.Rows[0]["Normal_Quantity"].ToString();
                    txtQuantity2.Text = dt.Rows[0]["DC_Quantity"].ToString();

                    hdn_promotion.Value = dt.Rows[0]["Promotion"].ToString();
                    hdn_retail_price.Value = dt.Rows[0]["Product_Retail_Price"].ToString();
                    hdn_promotion_price.Value = dt.Rows[0]["Product_Promotion_Price_Selling_Price"].ToString();
                    hdn_promotion_dc.Value = dt.Rows[0]["Product_Promotion_Price_EC_Points"].ToString();
                    hdn_total_dc.Value = dt.Rows[0]["DC_Point"].ToString();
                    hdn_total_quantity.Value = total_qty.ToString();

                    variation_name = dt.Rows[0]["Variation_Name"].ToString();
                    variation_value = dt.Rows[0]["Cart_Variation_Value"].ToString();
                    if (dt.Rows[0]["Variation"].ToString() == "Y")
                    {
                        variation = true;
                    }
                }
                else
                {
                    lbl_retail_price.Text = "RM " + Convert.ToDecimal(dt.Rows[0]["Variation_Retail_Price"]).ToString("###,###,###.00");
                    lbl_retail_price_bv.Text = Convert.ToDecimal(dt.Rows[0]["Variation_Retail_Price_BV_Points"]).ToString("###,###,##0");
                    if (dt.Rows[0]["Variation_Promotion"].ToString() == "Y")
                    {
                        div_promotion.Attributes.Add("style", "display: block");
                        lbl_promotion_price.Text = "RM " + Convert.ToDecimal(dt.Rows[0]["Variation_Promotion_Price_Selling_Price"]).ToString("###,###,###.00") + " + " + Convert.ToInt32(dt.Rows[0]["Variation_Promotion_Price_EC_Points"]).ToString() + "DC";
                        lbl_promotion_price_bv.Text = Convert.ToDecimal(dt.Rows[0]["Variation_Promotion_Price_BV_Points"]).ToString("###,###,##0");
                        lbl_point_balance.Attributes.Add("style", "display: contents");
                    }
                    else
                    {
                        div_promotion.Attributes.Add("style", "display: none");
                        lbl_point_balance.Attributes.Add("style", "display: none");
                    }
                    int total_qty = 0;
                    total_qty = (Convert.ToInt32(dt.Rows[0]["Normal_Quantity"].ToString()) + Convert.ToInt32(dt.Rows[0]["DC_Quantity"].ToString()));
                    lbl_variation_name.Text = dt.Rows[0]["Variation_Name"].ToString();
                    txtQuantity1.Text = dt.Rows[0]["Normal_Quantity"].ToString();
                    txtQuantity2.Text = dt.Rows[0]["DC_Quantity"].ToString();

                    hdn_promotion.Value = dt.Rows[0]["Variation_Promotion"].ToString();
                    hdn_retail_price.Value = dt.Rows[0]["Variation_Retail_Price"].ToString();
                    hdn_promotion_price.Value = dt.Rows[0]["Variation_Promotion_Price_Selling_Price"].ToString();
                    hdn_promotion_dc.Value = dt.Rows[0]["Variation_Promotion_Price_EC_Points"].ToString();
                    hdn_total_dc.Value = dt.Rows[0]["DC_Point"].ToString();
                    hdn_total_quantity.Value = total_qty.ToString();

                    variation_name = dt.Rows[0]["Variation_Name"].ToString();
                    variation_value = dt.Rows[0]["Cart_Variation_Value"].ToString();
                    variation = true;
                }

                if (dt.Rows[0]["Package"].ToString() == "Y")
                {
                    div_dc.Visible = false;
                    div_package.Visible = true;
                    Load_Package_Item(dt.Rows[0]["Product_Code"].ToString());
                }
                else
                {
                    div_dc.Visible = true;
                    div_package.Visible = false;
                }
            }
            con.Close();
        }

        if (variation)
        {
            div_variation.Visible = true;
            LoadVariation(hdn_item_code.Value, variation_name, variation_value);
        }
        else
        {
            div_variation.Visible = false;
        }
    }

    protected void Load_Package_Item(string product_code)
    {
        using (SqlCommand cmd = new SqlCommand("Load_Front_End_Package_Item", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Product_Code", product_code);
            //con.Open();
            SqlDataAdapter idr = new SqlDataAdapter(cmd);

            DataTable v = new DataTable();
            idr.Fill(v);

            rpt_package_item.DataSource = v;
            rpt_package_item.DataBind();

            //con.Close();
        }
    }

    protected void rpt_package_item_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Label lb_item_name = (Label)e.Item.FindControl("lb_item_name");

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView drv = (DataRowView)(e.Item.DataItem);

            lb_item_name.Text = drv.Row["Package_Product_Quantity"].ToString().Substring(0, drv.Row["Package_Product_Quantity"].ToString().Length - 3) + " X " + drv.Row["Product_Name"].ToString();
            if (!string.IsNullOrEmpty(drv.Row["Product_Variation_Name"].ToString()))
            {
                lb_item_name.Text += " (" + drv.Row["Product_Variation_Name"].ToString() + ") ";
            }
        }
    }

    protected void LoadVariation(string product_code, string variation_name, string variation_value)
    {
        using (SqlCommand cmd = new SqlCommand("Load_Front_End_Product_Variation_Value", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Product_Code", product_code);
            cmd.Parameters.AddWithValue("@Variation_Name", variation_name);
            con.Open();
            SqlDataAdapter idr = new SqlDataAdapter(cmd);

            DataTable v = new DataTable();
            idr.Fill(v);

            rbtnlist_variation_value.DataSource = v;
            rbtnlist_variation_value.DataTextField = "Variation_Content";
            rbtnlist_variation_value.DataValueField = "Variation_Code";
            rbtnlist_variation_value.DataBind();

            // Loop through each item in the RadioButtonList
            foreach (ListItem item in rbtnlist_variation_value.Items)
            {
                int quantity = Convert.ToInt32(v.Rows[rbtnlist_variation_value.Items.IndexOf(item)]["Variation_Available_Quantity"]);

                // Check if the value of the item matches the variation_value
                if (item.Value == variation_value)
                {
                    if (quantity != 0)
                    {
                        item.Selected = true;
                    }
                    else
                    {
                        item.Enabled = false; // Disable the individual radio button if quantity is 0
                    }
                }
                else
                {
                    if (quantity == 0)
                    {
                        item.Enabled = false; // Disable the individual radio button if quantity is 0
                    }
                }
            }

            rbtnlist_variation_value.Attributes.Add("onclick", "VariationSeleted()");

            con.Close();
        }
    }

    [WebMethod]
    public static string ProcessArray(string[] myArray, string id)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

        string productid = id;

        DataTable variation_details = new DataTable();

        foreach (string element in myArray)
        {
            using (SqlCommand cmd = new SqlCommand("Load_Product_Variation_Details", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductID", productid);
                cmd.Parameters.AddWithValue("@Variation", element);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(variation_details);
            }
        }

        var jsonObject = new JObject();
        jsonObject.Add("variation_details", JToken.FromObject(variation_details));

        string jsonDetails = jsonObject.ToString();
        return jsonDetails;

    }

    protected void btn_final_update_cart_Click(object sender, EventArgs e)
    {
        bool cookieExists = HttpContext.Current.Request.Cookies["userid"] != null;

        if (cookieExists == true)
        {
            if (Request.Cookies["userid"].Value != null && Request.Cookies["userid"].Value != "")
            {
                bool variationselected = false;
                string variation_value = "";
                if (rbtnlist_variation_value.Items.Count > 0)
                {
                    foreach (ListItem item in rbtnlist_variation_value.Items)
                    {
                        if (item.Selected == true)
                        {
                            variationselected = true;
                            variation_value = item.Value;
                        }
                    }
                }
                else
                {
                    variationselected = true;
                }

                if (variationselected == false)
                {
                    ShowMessage_warning("please select product variation ", MessageType.Warning);
                }
                else
                {
                    AddItemtoCart(hdn_item_code.Value, variation_value);
                    Load_Cart_Item(Request.QueryString["id"].ToString());
                    Calculate_Shipping_Fees(Request.QueryString["id"].ToString());
                    Load_Shipping_Discount(Request.QueryString["id"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "Set_Up_Language();", true);
                    ShowMessage("Successful update cart", MessageType.Success);
                }
            }
            else
            {
                ShowMessage_warning("Please login your account first.", MessageType.Warning);
            }
        }
        else
        {
            ShowMessage_warning("Please login your account first.", MessageType.Warning);
        }
    }

    protected void AddItemtoCart(string itemcode, string variation)
    {
        using (SqlCommand cmdcheckvariation = new SqlCommand("Check_Registration_Cart", con))
        {
            con.Open();

            cmdcheckvariation.Parameters.AddWithValue("@MemberID", Request.QueryString["id"].ToString());
            cmdcheckvariation.Parameters.AddWithValue("@ItemCode", itemcode);
            cmdcheckvariation.Parameters.AddWithValue("@Variation", variation);

            cmdcheckvariation.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter idrcheckvariation = new SqlDataAdapter(cmdcheckvariation);

            DataTable v2 = new DataTable();
            idrcheckvariation.Fill(v2);

            if (v2.Rows.Count > 0)
            {
                using (SqlCommand cmdinsertcart = new SqlCommand("Update_Registration_Cart_Checkout", con))
                {
                    cmdinsertcart.CommandType = CommandType.StoredProcedure;
                    cmdinsertcart.Parameters.AddWithValue("@MemberID", Request.QueryString["id"].ToString());
                    cmdinsertcart.Parameters.AddWithValue("@ItemCode", itemcode);
                    cmdinsertcart.Parameters.AddWithValue("@Variation", variation);
                    cmdinsertcart.Parameters.AddWithValue("@Normal_Quantity", txtQuantity1.Text);
                    cmdinsertcart.Parameters.AddWithValue("@DC_Quantity", txtQuantity2.Text);
                    cmdinsertcart.ExecuteNonQuery();
                }
            }
            else
            {
                using (SqlCommand cmdinsertcart = new SqlCommand("Insert_Registration_Cart_Checkout", con))
                {
                    cmdinsertcart.CommandType = CommandType.StoredProcedure;
                    cmdinsertcart.Parameters.AddWithValue("@MemberID", Request.QueryString["id"].ToString());
                    cmdinsertcart.Parameters.AddWithValue("@ItemCode", itemcode);
                    cmdinsertcart.Parameters.AddWithValue("@Variation", variation);
                    cmdinsertcart.Parameters.AddWithValue("@Normal_Quantity", txtQuantity1.Text);
                    cmdinsertcart.Parameters.AddWithValue("@DC_Quantity", txtQuantity2.Text);
                    cmdinsertcart.Parameters.AddWithValue("@Cart_IDs", hdn_cart_ids.Value);
                    cmdinsertcart.ExecuteNonQuery();
                }
            }
            con.Close();
        }
    }

    #endregion

    #region Address
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
                    ddl_state.Items.Clear();
                    DataTable v = new DataTable();

                    v.Load(idr);
                    ddl_state.DataSource = v;
                    ddl_state.DataTextField = "Statename";
                    ddl_state.DataValueField = "Statecode";
                    ddl_state.DataBind();
                }

                idr.Close();
                con.Close();
            }
        }
    }

    protected void Load_Address_Country()
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

                    ddl_address_country.DataSource = v;
                    ddl_address_country.DataTextField = "Name";
                    ddl_address_country.DataValueField = "Code";
                    ddl_address_country.DataBind();
                }

                idr.Close();
                con.Close();
            }
        }
    }

    protected void Load_Member_Address(string memberid)
    {
        using (SqlCommand cmd = new SqlCommand("Load_Registration_Member_Default_Address", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Memberid", memberid);
            con.Open();
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows == true)
            {
                while (idr.Read())
                {
                    if (!string.IsNullOrEmpty(idr["FullName"].ToString()))
                    {
                        lbl_delivery_name.Text = idr["FullName"].ToString();
                    }
                    else
                    {
                        lbl_delivery_name.Text = idr["CompanyName"].ToString();
                    }
                    lbl_delivery_contact_no.Text = idr["PhoneNumber"].ToString();
                    string address1 = idr["Add1"].ToString();
                    string address2 = idr["Add2"].ToString();

                    // Check if address1 ends with a comma
                    if (!address1.EndsWith(","))
                    {
                        address1 += ",";
                    }

                    lbl_delivery_address.Text = address1 + " " + address2;
                    lbl_delivery_postcode.Text = idr["Postcode"].ToString();
                    lbl_delivery_city.Text = idr["City"].ToString();
                    lbl_delivery_state.Text = idr["State_Name"].ToString();
                    lbl_delivery_country.Text = idr["Country_Name"].ToString();
                }
            }

            idr.Close();
            con.Close();

        }
    }

    protected void btn_edit_address_Click(object sender, EventArgs e)
    {
        Open_Address_Modal();
    }

    protected void Open_Address_Modal()
    {
        txt_full_name.Value = "";
        txt_phone_number.Value = "";
        txt_address_1.Value = "";
        txt_address_2.Value = "";
        txt_postcode.Value = "";
        txt_city.Value = "";
        btn_submit.Visible = true;
        btn_update.Visible = false;
        Load_Member_All_Address();
        Load_State();
        Load_Address_Country();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "Show_All_Address_Modal();", true);
    }

    protected void Load_Member_All_Address()
    {
        string member_id = "";
        member_id = Request.QueryString["id"].ToString();

        using (SqlCommand cmd = new SqlCommand("Load_Registration_Member_All_Address", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Memberid", member_id);
            con.Open();

            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows == true)
            {
                rpt_address.Visible = true;
                div_norecord.Visible = false;
                DataTable v = new DataTable();

                v.Load(idr);
                rpt_address.DataSource = v;
                rpt_address.DataBind();
            }
            else
            {
                rpt_address.Visible = false;
                div_norecord.Visible = true;
            }

            idr.Close();
            con.Close();

        }
    }

    protected void rpt_address_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Label lbl_receiver_name = (Label)e.Item.FindControl("lbl_receiver_name");
        Label lbl_receiver_phone = (Label)e.Item.FindControl("lbl_receiver_phone");
        Label lbl_receiver_address = (Label)e.Item.FindControl("lbl_receiver_address");
        Label lbl_receiver_postcode = (Label)e.Item.FindControl("lbl_receiver_postcode");
        Label lbl_receiver_city = (Label)e.Item.FindControl("lbl_receiver_city");
        Label lbl_receiver_state = (Label)e.Item.FindControl("lbl_receiver_state");
        Label lbl_receiver_country = (Label)e.Item.FindControl("lbl_receiver_country");
        HtmlGenericControl div_default = (HtmlGenericControl)e.Item.FindControl("div_default");

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView drv = (DataRowView)(e.Item.DataItem);

            if (!string.IsNullOrEmpty(drv.Row["FullName"].ToString()))
            {
                lbl_receiver_name.Text = drv.Row["FullName"].ToString();
            }
            else
            {
                lbl_receiver_name.Text = drv.Row["CompanyName"].ToString();
            }
            lbl_receiver_phone.Text = drv.Row["PhoneNumber"].ToString();
            string address1 = drv.Row["Add1"].ToString();
            string address2 = drv.Row["Add2"].ToString();

            // Check if address1 ends with a comma
            if (!address1.EndsWith(","))
            {
                address1 += ",";
            }
            lbl_receiver_address.Text = address1 + " " + address2;
            lbl_receiver_postcode.Text = drv.Row["Postcode"].ToString();
            lbl_receiver_city.Text = drv.Row["City"].ToString();
            lbl_receiver_state.Text = drv.Row["State_Name"].ToString();
            lbl_receiver_country.Text = drv.Row["Country_Name"].ToString();

            if (drv.Row["defaultIND"].ToString() == "Yes")
            {
                div_default.Visible = true;
            }
            else
            {
                div_default.Visible = false;
            }
        }
    }

    protected void rpt_address_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            Load_Edit_Address(e.CommandArgument.ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "Show_Edit_Address_Modal();", true);
        }
    }

    protected void Load_Edit_Address(string id)
    {
        using (SqlCommand cmd = new SqlCommand("Load_Registration_Member_Edit_Address", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows == true)
            {
                while (idr.Read())
                {
                    hdn_ids.Value = id;
                    if (!string.IsNullOrEmpty(idr["FullName"].ToString()))
                    {
                        txt_full_name.Value = idr["FullName"].ToString();
                    }
                    else
                    {
                        txt_full_name.Value = idr["CompanyName"].ToString();
                    }
                    txt_phone_number.Value = idr["PhoneNumber"].ToString();
                    txt_address_1.Value = idr["Add1"].ToString();
                    txt_address_2.Value = idr["Add2"].ToString();
                    txt_postcode.Value = idr["Postcode"].ToString();
                    txt_city.Value = idr["City"].ToString();
                    if (!string.IsNullOrEmpty(idr["State"].ToString()))
                    {
                        ddl_state.SelectedValue = idr["State"].ToString();
                    }
                    ddl_address_country.SelectedValue = idr["Country"].ToString();
                    if (idr["defaultIND"].ToString() == "Yes")
                    {
                        chk_default.Checked = true;
                    }
                    else
                    {
                        chk_default.Checked = false;
                    }
                    btn_submit.Visible = false;
                    btn_update.Visible = true;
                }
            }

            idr.Close();
            con.Close();

        }
    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txt_full_name.Value))
        {
            ShowMessage_warning("Please key in receive name", MessageType.Warning);
            return;
        }
        else if (string.IsNullOrEmpty(txt_phone_number.Value))
        {
            ShowMessage_warning("Please key in receive phone number", MessageType.Warning);
            return;
        }
        else if (string.IsNullOrEmpty(txt_address_1.Value))
        {
            ShowMessage_warning("Please key in receive address", MessageType.Warning);
            return;
        }
        else if (string.IsNullOrEmpty(txt_postcode.Value))
        {
            ShowMessage_warning("Please key in receive address postcode", MessageType.Warning);
            return;
        }
        else if (string.IsNullOrEmpty(txt_city.Value))
        {
            ShowMessage_warning("Please key in receive address city", MessageType.Warning);
            return;
        }
        else
        {
            using (SqlCommand cmd = new SqlCommand("Insert_Registration_New_Address", con))
            {
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MemberID", Request.QueryString["id"].ToString());
                cmd.Parameters.AddWithValue("@Receiver_Name", txt_full_name.Value);
                cmd.Parameters.AddWithValue("@Receiver_Phone_Number", txt_phone_number.Value);
                cmd.Parameters.AddWithValue("@Receiver_Address_1", txt_address_1.Value);
                cmd.Parameters.AddWithValue("@Receiver_Address_2", txt_address_2.Value);
                cmd.Parameters.AddWithValue("@Receiver_Postcode", txt_postcode.Value);
                cmd.Parameters.AddWithValue("@Receiver_City", txt_city.Value);
                cmd.Parameters.AddWithValue("@Receiver_State", ddl_state.SelectedValue);
                cmd.Parameters.AddWithValue("@Receiver_Country", ddl_address_country.SelectedValue);
                if (chk_default.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@Default_Address", "Yes");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Default_Address", "No");
                }
                cmd.ExecuteNonQuery();
                con.Close();
            }
            Calculate_Shipping_Fees(Request.QueryString["id"].ToString());
            Open_Address_Modal();
            Load_Member_All_Address();
            Load_Member_Address(Request.QueryString["id"].ToString());
            Load_Shipping_Discount(Request.QueryString["id"].ToString());
        }

    }

    protected void btn_update_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txt_full_name.Value))
        {
            ShowMessage_warning("Please key in receive name", MessageType.Warning);
            return;
        }
        else if (string.IsNullOrEmpty(txt_phone_number.Value))
        {
            ShowMessage_warning("Please key in receive phone number", MessageType.Warning);
            return;
        }
        else if (string.IsNullOrEmpty(txt_address_1.Value))
        {
            ShowMessage_warning("Please key in receive address", MessageType.Warning);
            return;
        }
        else if (string.IsNullOrEmpty(txt_postcode.Value))
        {
            ShowMessage_warning("Please key in receive address postcode", MessageType.Warning);
            return;
        }
        else if (string.IsNullOrEmpty(txt_city.Value))
        {
            ShowMessage_warning("Please key in receive address city", MessageType.Warning);
            return;
        }
        else
        {
            using (SqlCommand cmd = new SqlCommand("Update_Registration_Address", con))
            {
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", hdn_ids.Value);
                cmd.Parameters.AddWithValue("@MemberID", Request.QueryString["id"].ToString());
                cmd.Parameters.AddWithValue("@Receiver_Name", txt_full_name.Value);
                cmd.Parameters.AddWithValue("@Receiver_Phone_Number", txt_phone_number.Value);
                cmd.Parameters.AddWithValue("@Receiver_Address_1", txt_address_1.Value);
                cmd.Parameters.AddWithValue("@Receiver_Address_2", txt_address_2.Value);
                cmd.Parameters.AddWithValue("@Receiver_Postcode", txt_postcode.Value);
                cmd.Parameters.AddWithValue("@Receiver_City", txt_city.Value);
                cmd.Parameters.AddWithValue("@Receiver_State", ddl_state.SelectedValue);
                cmd.Parameters.AddWithValue("@Receiver_Country", ddl_address_country.SelectedValue);
                if (chk_default.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@Default_Address", "Yes");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Default_Address", "No");
                }
                cmd.ExecuteNonQuery();
                con.Close();
            }
            Calculate_Shipping_Fees(Request.QueryString["id"].ToString());
            Open_Address_Modal();
            Load_Member_Address(Request.QueryString["id"].ToString());
            Load_Shipping_Discount(Request.QueryString["id"].ToString());
        }
    }

    #endregion

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

    protected void btn_place_order_Click(object sender, EventArgs e)
    {
        bool cookieExists = HttpContext.Current.Request.Cookies["userid"] != null;

        if (cookieExists == true)
        {
            if (Request.Cookies["userid"].Value != null && Request.Cookies["userid"].Value != "")
            {
                Check_Member_Details(Request.QueryString["id"].ToString());
                Check_Product_Available(Request.QueryString["id"].ToString());
                if (ddl_delivery_option.SelectedValue == "Delivery")
                {
                    Check_Country_Available(Request.QueryString["id"].ToString());
                }
                else
                {
                    Country_Available = true;
                }

                if (GotItem == true && ItemNotAvailable == false && Correct_Package == true && Correct_Member_Details == true && Country_Available == true)
                {
                    Update_Member_Details(Request.QueryString["id"].ToString());
                    if (Complete_Member_Details == true)
                    {
                        Insert_Order(Request.QueryString["id"].ToString());
                    }
                }
            }
            else
            {
                ShowMessage_warning("Please login your account", MessageType.Warning);
                return;
            }
        }
        else
        {
            ShowMessage_warning("Please login your account", MessageType.Warning);
            return;
        }
    }

    protected void Check_Member_Details(string member_id){
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
        else if (string.IsNullOrEmpty(txt_registration_city.Value))
        {
            ShowMessage_warning("Please key in your city", MessageType.Warning);
            return;
        }
        else if (string.IsNullOrEmpty(txt_address1.Value))
        {
            ShowMessage_warning("Please key in your address", MessageType.Warning);
            return;
        }
        else if (string.IsNullOrEmpty(txt_registration_postocde.Value))
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
                else if(string.IsNullOrEmpty(txt_password.Value) || !Regex.IsMatch(txt_confirmpassword.Value, @"^[A-Za-z0-9]{6,}$"))
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
                    Correct_Member_Details = true;
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
                else if (string.IsNullOrEmpty(txt_password.Value) || !Regex.IsMatch(txt_confirmpassword.Value, @"^[A-Za-z0-9]{6,}$"))
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
                    Correct_Member_Details = true;
                }
            }
        }
    }

    protected void Check_Product_Available(string member_id)
    {
        DataTable v = new DataTable();
        using (SqlCommand cmd = new SqlCommand("Load_Registration_Member_Checkout_Cart", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Memberid", member_id);
            con.Open();

            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows == true)
            {
                v.Load(idr);
                GotItem = true;
            }
            else
            {
                ShowMessage_warning("Please add package in your cart, before checkout", MessageType.Warning);
                return;
            }

            idr.Close();
            con.Close();
        }

        using (SqlCommand cmd = new SqlCommand("Check_Registration_Member_Checkout_Cart", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Memberid", Request.QueryString["id"].ToString());
            cmd.Parameters.AddWithValue("@Profit_Center", ddl_package.Value);

            // Set Output Paramater
            SqlParameter StatusParam = new SqlParameter("@Status", SqlDbType.VarChar, 200);
            StatusParam.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(StatusParam);

            con.Open();

            cmd.ExecuteNonQuery();

            string Status = cmd.Parameters["@Status"].Value.ToString();

            if (Status == "MINIMUM 1 PACKAGE")
            {
                string message = "";
                message = "Minimum purchase 1 package";
                ShowMessage_warning(message, MessageType.Warning);
                return;
            }
            else if (Status == "MINIMUM 3 PACKAGE")
            {
                string message = "";
                message = "Minimum purchase 3 package";
                ShowMessage_warning(message, MessageType.Warning);
                return;
            }
            else
            {
                Correct_Package = true;
            }

            con.Close();

        }

        foreach (DataRow dr in v.Rows)
        {
            using (SqlCommand cmd = new SqlCommand("Check_Registration_Product_Qty", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Product_ID", dr["product_code"].ToString());
                cmd.Parameters.AddWithValue("@Variation", dr["Variation"].ToString());
                cmd.Parameters.AddWithValue("@Normal_Quantity", dr["Normal_Quantity"].ToString());
                cmd.Parameters.AddWithValue("@DC_Quantity", dr["DC_Quantity"].ToString());

                // Set Output Paramater
                SqlParameter Product_Name = new SqlParameter("@Product_Name", SqlDbType.VarChar, 200);
                Product_Name.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(Product_Name);

                // Set Output Paramater
                SqlParameter Product_Variation_Name = new SqlParameter("@Product_Variation_Name", SqlDbType.VarChar, 200);
                Product_Variation_Name.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(Product_Variation_Name);

                // Set Output Paramater
                SqlParameter Product_Quantity = new SqlParameter("@Product_Quantity", SqlDbType.VarChar, 200);
                Product_Quantity.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(Product_Quantity);

                // Set Output Paramater
                SqlParameter StatusParam = new SqlParameter("@Status", SqlDbType.VarChar, 200);
                StatusParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(StatusParam);

                con.Open();

                cmd.ExecuteNonQuery();

                string result_Product_Name = cmd.Parameters["@Product_Name"].Value.ToString();
                string result_Product_Variation = cmd.Parameters["@Product_Variation_Name"].Value.ToString();
                string result_Product_Quantity = cmd.Parameters["@Product_Quantity"].Value.ToString();
                string Status = cmd.Parameters["@Status"].Value.ToString();

                if (Status == "NOT AVAILABLE")
                {
                    ItemNotAvailable = true;
                    string message = "";
                    message = result_Product_Name + " is currently unavailable";
                    ShowMessage_warning(message, MessageType.Warning);
                    return;
                }
                else if (Status == "PRODUCT OUT OF STOCK NOT AVAILABLE")
                {
                    ItemNotAvailable = true;
                    string message = "";
                    message = result_Product_Name + " is currently out of stock";
                    ShowMessage_warning(message, MessageType.Warning);
                    return;
                }
                else if (Status == "PRODUCT QTY NOT AVAILABLE")
                {
                    ItemNotAvailable = true;
                    string message = "";
                    message = result_Product_Name + " maximum available quantity is " + result_Product_Quantity;
                    ShowMessage_warning(message, MessageType.Warning);
                    return;
                }
                else if (Status == "VARIATION NOT AVAILABLE")
                {
                    ItemNotAvailable = true;
                    string message = "";
                    message = result_Product_Name + " with variation : " + result_Product_Variation + " is currently unavailable ";
                    ShowMessage_warning(message, MessageType.Warning);
                    return;
                }
                else if (Status == "VARIATION OUT OF STOCK NOT AVAILABLE")
                {
                    ItemNotAvailable = true;
                    string message = "";
                    message = result_Product_Name + " with variation : " + result_Product_Variation + " is currently out of stock";
                    ShowMessage_warning(message, MessageType.Warning);
                    return;
                }
                else if (Status == "VARIATION QTY NOT AVAILABLE")
                {
                    ItemNotAvailable = true;
                    string message = "";
                    message = result_Product_Name + " with variation : " + result_Product_Variation + " maximum available quantity is " + Convert.ToDecimal(result_Product_Quantity).ToString("###,###,###");
                    ShowMessage_warning(message, MessageType.Warning);
                    return;
                }

                con.Close();

            }

            if (dr["Package"].ToString() == "Y")
            {
                bool package_quantity_available = true;
                bool package_available = true;
                bool package_publish = true;
                bool package_not_deleted = true;
                string product_name = "";

                DataTable Check_Package_Item_Available = new DataTable();
                using (SqlCommand cmd = new SqlCommand("Check_Registration_Package_Item", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Product_Code", dr["Product_Code"].ToString());
                    SqlDataAdapter idr = new SqlDataAdapter(cmd);

                    idr.Fill(Check_Package_Item_Available);
                }

                if (Check_Package_Item_Available.Rows.Count > 0)
                {
                    foreach (DataRow dt in Check_Package_Item_Available.Rows)
                    {
                        if (dt["Package_Item_Quantity_Available_Status"].ToString() == "OUT OF STOCK")
                        {
                            package_quantity_available = false;
                        }
                        if (dt["Package_Item_Sold_Out_Status"].ToString() == "SOLD OUT")
                        {
                            package_available = false;
                        }
                        if (dt["Package_Item_Publish_Status"].ToString() == "UNPUBLISH")
                        {
                            package_publish = false;
                        }
                        if (dt["Package_Item_Deleted_Status"].ToString() == "DELETED")
                        {
                            package_not_deleted = false;
                        }
                        product_name = dt["Product_Name"].ToString();
                    }
                }

                if (package_quantity_available == false)
                {
                    ItemNotAvailable = true;
                    string message = "";
                    message = product_name + " is currently out of stock";
                    ShowMessage_warning(message, MessageType.Warning);
                    return;
                }
                else if (package_available == false)
                {
                    ItemNotAvailable = true;
                    string message = "";
                    message = product_name + " is currently out of stock";
                    ShowMessage_warning(message, MessageType.Warning);
                    return;
                }
                else if (package_publish == false)
                {
                    ItemNotAvailable = true;
                    string message = "";
                    message = product_name + " is currently out of stock";
                    ShowMessage_warning(message, MessageType.Warning);
                    return;
                }
                else if (package_not_deleted == false)
                {
                    ItemNotAvailable = true;
                    string message = "";
                    message = product_name + " is currently out of stock";
                    ShowMessage_warning(message, MessageType.Warning);
                    return;
                }

            }
        }

    }

    protected void Check_Country_Available(string member_id)
    {
        DataTable v = new DataTable();
        using (SqlCommand cmd = new SqlCommand("Load_Registration_Country_Available", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Memberid", member_id);
            con.Open();

            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows == true)
            {
                v.Load(idr);
                Country_Available = true;
            }
            else
            {
                ShowMessage_warning("Currently delivery service only available within Malaysia", MessageType.Warning);
                return;
            }

            idr.Close();
            con.Close();
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
            cmd.Parameters.AddWithValue("Postcode", txt_registration_postocde.Value.Trim());
            cmd.Parameters.AddWithValue("City", txt_registration_city.Value.Trim());
            cmd.Parameters.AddWithValue("Country", ddl_country.SelectedValue);
            if (ddl_country.SelectedValue == "MY")
            {
                cmd.Parameters.AddWithValue("State", ddl_Member_State.SelectedValue);
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
                Complete_Member_Details = true;
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

    private string GenerateUniqueNumericId()
    {
        Random random = new Random();
        return random.Next(0, 100000000).ToString("D8");
    }

    protected void Insert_Order(string member_id)
    {
        int total_qty = 0;
        decimal total_item_price = 0;
        decimal total_order_weight = 0;
        decimal total_shipping_price = 0;
        decimal total_shipping_price_discount = 0;
        decimal total_bv = 0;
        int final_total_dc_earn = 0;
        int final_total_dc_used = 0;
        decimal total_price = 0;
        string orderid = "";

        while (orderid == "")
        {
            string uniqueId = GenerateUniqueNumericId();
            orderid = "MYSO" + uniqueId;

            using (SqlCommand cmd = new SqlCommand("Check_Order_No", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Order_No", orderid);
                con.Open();

                SqlDataReader idr = cmd.ExecuteReader();

                if (idr.HasRows == true)
                {
                    orderid = "";
                }

                idr.Close();
                con.Close();
            }
        }

        #region Insert Cart Item

        DataTable v = new DataTable();
        using (SqlCommand cmd = new SqlCommand("Load_Registration_Member_Checkout_Cart", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Memberid", member_id);
            con.Open();

            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows == false)
            {
                ShowMessage_warning("Please add package in your cart, before checkout", MessageType.Warning);
                return;
            }

            idr.Close();
            con.Close();
        }

        using (SqlCommand cmd = new SqlCommand("Load_Registration_Member_Cart", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Memberid", member_id);
            con.Open();

            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows == true)
            {
                v.Load(idr);
            }

            idr.Close();
            con.Close();
        }

        foreach (DataRow row in v.Rows)
        {
            decimal total_normal_price = 0;
            decimal total_promotion_price = 0;
            decimal total_weight = 0;
            decimal total_dc_used = 0;
            string total_dc_earn = "0";
            decimal normal_price = 0;
            decimal promotion_price = 0;
            decimal dc_points = 0;
            decimal normal_bv = 0;
            decimal promotion_bv = 0;

            using (SqlCommand cmd = new SqlCommand("Insert_Registration_SOItem", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@OrderNo", orderid);
                cmd.Parameters.AddWithValue("@Product_Code", row["product_code"].ToString());
                cmd.Parameters.AddWithValue("@Variation", row["Variation"].ToString());
                if (row["Package"].ToString() == "Y")
                {
                    cmd.Parameters.AddWithValue("@Package", "Y");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Package", "N");
                }
                if (string.IsNullOrEmpty(row["Variation"].ToString()))
                {
                    total_weight = (Convert.ToDecimal(row["Normal_Quantity"].ToString()) + Convert.ToDecimal(row["DC_Quantity"].ToString())) * Convert.ToDecimal(row["Product_Weight"].ToString());

                    if (row["Normal_Quantity"].ToString() == "0")
                    {
                        total_normal_price = 0;
                        normal_price = 0;
                        normal_bv = 0;
                    }
                    else
                    {
                        normal_price = Convert.ToDecimal(row["Product_Retail_Price"].ToString());
                        total_normal_price = (Convert.ToDecimal(row["Product_Retail_Price"].ToString()) * Convert.ToDecimal(row["Normal_Quantity"].ToString()));
                        normal_bv = Convert.ToDecimal(row["Product_Retail_Price_BV_Points"].ToString()) * Convert.ToDecimal(row["Normal_Quantity"].ToString());
                    }

                    if (row["DC_Quantity"].ToString() == "0")
                    {
                        total_promotion_price = 0;
                        promotion_price = 0;
                        promotion_bv = 0;
                    }
                    else
                    {
                        total_dc_used = Convert.ToDecimal(row["Product_Promotion_Price_EC_Points"].ToString()) * Convert.ToDecimal(row["DC_Quantity"].ToString());
                        dc_points = Convert.ToDecimal(row["Product_Promotion_Price_EC_Points"].ToString());
                        promotion_price = Convert.ToDecimal(row["Product_Promotion_Price_Selling_Price"].ToString());
                        total_promotion_price = (Convert.ToDecimal(row["Product_Promotion_Price_Selling_Price"].ToString()) * Convert.ToDecimal(row["DC_Quantity"].ToString()));
                        promotion_bv = Convert.ToDecimal(row["Product_Promotion_Price_BV_Points"].ToString()) * Convert.ToDecimal(row["DC_Quantity"].ToString());
                    }

                    decimal product_total = total_normal_price;
                    decimal conversionRate = Convert.ToDecimal(row["Min_Point_Earn"].ToString());
                    decimal pointsEarned = 0;
                    if (row["Fix_DC"].ToString() == "Y")
                    {
                        pointsEarned = Convert.ToDecimal(row["Fix_DC_Amount"].ToString()) * (Convert.ToDecimal(row["Normal_Quantity"].ToString()) + Convert.ToDecimal(row["DC_Quantity"].ToString()));
                    }
                    else
                    {
                        pointsEarned = Math.Floor(product_total / Convert.ToDecimal(row["Min_Spend"].ToString())) * conversionRate;
                    }
                    total_dc_earn = pointsEarned.ToString("########0");

                    cmd.Parameters.AddWithValue("@Normal_Price", normal_price);
                    cmd.Parameters.AddWithValue("@DC_Price", promotion_price);
                    cmd.Parameters.AddWithValue("@DC_Points", dc_points);
                    cmd.Parameters.AddWithValue("@Total_Price", total_normal_price + total_promotion_price);
                    cmd.Parameters.AddWithValue("@Total_Weight", total_weight);
                    cmd.Parameters.AddWithValue("@Total_BV_Earn", normal_bv + promotion_bv);
                    cmd.Parameters.AddWithValue("@Total_DC_Used", total_dc_used);
                    cmd.Parameters.AddWithValue("@Total_DC_Earn", total_dc_earn);
                }
                else
                {
                    total_weight = (Convert.ToDecimal(row["Normal_Quantity"].ToString()) + Convert.ToDecimal(row["DC_Quantity"].ToString())) * Convert.ToDecimal(row["Variation_Weight"].ToString());

                    if (row["Normal_Quantity"].ToString() == "0")
                    {
                        total_normal_price = 0;
                        normal_price = 0;
                        normal_bv = 0;
                    }
                    else
                    {
                        normal_price = Convert.ToDecimal(row["Variation_Retail_Price"].ToString());
                        total_normal_price = (Convert.ToDecimal(row["Variation_Retail_Price"].ToString()) * Convert.ToDecimal(row["Normal_Quantity"].ToString()));
                        normal_bv = Convert.ToDecimal(row["Variation_Retail_Price_BV_Points"].ToString()) * Convert.ToDecimal(row["Normal_Quantity"].ToString());
                    }

                    if (row["DC_Quantity"].ToString() == "0")
                    {
                        total_promotion_price = 0;
                        promotion_price = 0;
                        promotion_bv = 0;
                    }
                    else
                    {
                        total_dc_used = Convert.ToDecimal(row["Variation_Promotion_Price_EC_Points"].ToString()) * Convert.ToDecimal(row["DC_Quantity"].ToString());
                        dc_points = Convert.ToDecimal(row["Variation_Promotion_Price_EC_Points"].ToString());
                        promotion_price = Convert.ToDecimal(row["Variation_Promotion_Price_Selling_Price"].ToString());
                        total_promotion_price = (Convert.ToDecimal(row["Variation_Promotion_Price_Selling_Price"].ToString()) * Convert.ToDecimal(row["DC_Quantity"].ToString()));
                        promotion_bv = Convert.ToDecimal(row["Variation_Promotion_Price_BV_Points"].ToString()) * Convert.ToDecimal(row["DC_Quantity"].ToString());
                    }

                    decimal product_total = total_normal_price;
                    decimal conversionRate = Convert.ToDecimal(row["Min_Point_Earn"].ToString());
                    decimal pointsEarned = 0;
                    if (row["Fix_DC"].ToString() == "Y")
                    {
                        pointsEarned = Convert.ToDecimal(row["Fix_DC_Amount"].ToString()) * (Convert.ToDecimal(row["Normal_Quantity"].ToString()) + Convert.ToDecimal(row["DC_Quantity"].ToString()));
                    }
                    else
                    {
                        pointsEarned = Math.Floor(product_total / Convert.ToDecimal(row["Min_Spend"].ToString())) * conversionRate;
                    }
                    total_dc_earn = pointsEarned.ToString("########0");

                    cmd.Parameters.AddWithValue("@Normal_Price", normal_price);
                    cmd.Parameters.AddWithValue("@DC_Price", promotion_price);
                    cmd.Parameters.AddWithValue("@DC_Points", dc_points);
                    cmd.Parameters.AddWithValue("@Total_Price", total_normal_price + total_promotion_price);
                    cmd.Parameters.AddWithValue("@Total_Weight", total_weight);
                    cmd.Parameters.AddWithValue("@Total_BV_Earn", normal_bv + promotion_bv);
                    cmd.Parameters.AddWithValue("@Total_DC_Used", total_dc_used);
                    cmd.Parameters.AddWithValue("@Total_DC_Earn", total_dc_earn);
                }
                cmd.Parameters.AddWithValue("@Qty", row["Normal_Quantity"].ToString());
                cmd.Parameters.AddWithValue("@DC_Qty", row["DC_Quantity"].ToString());
                cmd.Parameters.AddWithValue("@CartID", Convert.ToInt32(row["IDs"].ToString()));
                cmd.ExecuteNonQuery();
                con.Close();
            }

            total_order_weight = total_order_weight + total_weight;
            total_qty = total_qty + Convert.ToInt32(row["Normal_Quantity"].ToString()) + Convert.ToInt32(row["DC_Quantity"].ToString());
            total_item_price = total_item_price + total_normal_price + total_promotion_price;
            total_bv = total_bv + normal_bv + promotion_bv;
            final_total_dc_earn = final_total_dc_earn + Convert.ToInt32(total_dc_earn);
            final_total_dc_used = final_total_dc_used + Convert.ToInt32(total_dc_used);
            total_price = total_item_price;
        }

        #endregion

        #region Calculate Shipping Fees

        if (ddl_delivery_option.SelectedValue == "Delivery")
        {
            DataTable dt_shipping_fees = new DataTable();

            using (SqlCommand cmd = new SqlCommand("Load_Registration_Member_Shipping_Fees", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Member_ID", member_id);
                con.Open();

                SqlDataReader idr = cmd.ExecuteReader();

                if (idr.HasRows == true)
                {
                    dt_shipping_fees.Load(idr);
                }

                idr.Close();
                con.Close();
            }

            decimal new_total_weight = 0;
            new_total_weight = Math.Ceiling(total_order_weight / 1000) * 1000;

            foreach (DataRow dr in dt_shipping_fees.Rows)
            {
                decimal shipping_fees = 0;
                decimal first_kg = Convert.ToDecimal(dr["First_kg"].ToString());
                decimal First_kg_price = Convert.ToDecimal(dr["ShippingFeesFirst_kg"].ToString());
                decimal Sub_1kg_price = Convert.ToDecimal(dr["ShippingFeesSub_1kg"].ToString());

                if ((new_total_weight / 1000) <= first_kg)
                {
                    shipping_fees = First_kg_price;
                }
                else
                {
                    decimal second_kg = (Math.Ceiling(new_total_weight / 1000) - first_kg);
                    shipping_fees = First_kg_price + (Sub_1kg_price * second_kg);
                }

                if (ddl_package.Value == "3" && dr["Region"].ToString() != "EM")
                {
                    total_shipping_price = 0;
                }
                else
                {
                    total_shipping_price = total_shipping_price + shipping_fees;
                }
            }
        }
        else if (ddl_delivery_option.SelectedValue == "Self Pickup")
        {
            total_shipping_price = 0;
        }

        total_price = total_price + total_shipping_price;

        #endregion

        #region Shipping Discount
        if (ddl_delivery_option.SelectedValue == "Delivery")
        {
            DataTable dt_shipping_fees = new DataTable();
            decimal discount_shipping_fees = 0;
            decimal minimal_purchase = 0;
            decimal default_shipping_fees = 0;

            using (SqlCommand cmd = new SqlCommand("Load_Registration_Member_Shipping_Fees_Discount", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Member_ID", member_id);
                con.Open();

                SqlDataReader idr = cmd.ExecuteReader();

                if (idr.HasRows == true)
                {
                    dt_shipping_fees.Load(idr);
                }

                idr.Close();
                con.Close();
            }

            if (dt_shipping_fees.Rows.Count > 0)
            {
                foreach (DataRow dr in dt_shipping_fees.Rows)
                {
                    discount_shipping_fees = Convert.ToDecimal(dr["Shipping_Fees_Discount"].ToString());
                    minimal_purchase = Convert.ToDecimal(dr["Minimal_Purchase_Amount"].ToString());
                }
            }
            else
            {
                discount_shipping_fees = 0;
            }

            decimal sub_total = 0;
            sub_total = total_item_price;
            if (sub_total >= minimal_purchase)
            {
                default_shipping_fees = total_shipping_price;
                if (default_shipping_fees > discount_shipping_fees)
                {
                    total_shipping_price_discount = discount_shipping_fees;
                }
                else
                {
                    total_shipping_price_discount = default_shipping_fees;
                }
            }
            else
            {
                total_shipping_price_discount = 0;
            }
        }
        else if (ddl_delivery_option.SelectedValue == "Self Pickup")
        {
            total_shipping_price_discount = 0;
        }

        total_price = total_price - total_shipping_price_discount;
        #endregion

        #region Insert Order

        using (SqlCommand cmd2 = new SqlCommand("Insert_Registration_SOHeader", con))
        {
            con.Open();
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Connection = con;
            cmd2.Parameters.AddWithValue("@OrderNo", orderid);
            cmd2.Parameters.AddWithValue("@QtyTotal", total_qty);
            cmd2.Parameters.AddWithValue("@ItemTotal", total_item_price);
            cmd2.Parameters.AddWithValue("@ShippingTotal", total_shipping_price);
            cmd2.Parameters.AddWithValue("@Shipping_Discount", total_shipping_price_discount);
            cmd2.Parameters.AddWithValue("@WeightTotal", total_order_weight);
            cmd2.Parameters.AddWithValue("@BVTotal", total_bv);
            cmd2.Parameters.AddWithValue("@DCTotal", final_total_dc_earn);
            cmd2.Parameters.AddWithValue("@Used_DCTotal", final_total_dc_used);
            cmd2.Parameters.AddWithValue("@Total_Price", total_price);
            cmd2.Parameters.AddWithValue("@OrderStatus", "CONFIRM");
            cmd2.Parameters.AddWithValue("@MemberID", member_id);
            cmd2.Parameters.AddWithValue("@DeliveryService", ddl_delivery_option.SelectedValue);
            cmd2.Parameters.AddWithValue("@Address_ID", "Default");
            cmd2.Parameters.AddWithValue("@DeliveryMessage", "");
            cmd2.Parameters.AddWithValue("@PaymentType", "Online Payment");
            cmd2.Parameters.AddWithValue("@PaymentID", "");
            cmd2.Parameters.AddWithValue("@PaymentStatus", "Pending");
            cmd2.Parameters.AddWithValue("@PaymentRef", "");
            cmd2.Parameters.AddWithValue("@PaymentAmt", total_price);
            cmd2.Parameters.AddWithValue("@PaymentDT", "1990-01-01 00:00:00");
            cmd2.ExecuteNonQuery();
            con.Close();
        }


        con.Open();
        SqlCommand cmd_cart = new SqlCommand();
        cmd_cart.CommandText = "DELETE FROM Temporarily_MF_Member_Cart WHERE IDs IN (SELECT CartID FROM Temporarily_SO_Item WHERE Order_No = '" + orderid + "')";
        cmd_cart.Connection = con;
        cmd_cart.ExecuteNonQuery();
        con.Close();
        #endregion

        string payment_refno = "";
        string payment_amount = "";
        string payment_username = "";
        string payment_userphone = "";
        string payment_useremail = "";
        string payment_remark = "";

        using (SqlCommand cmd = new SqlCommand("Checkout_Load_Member_Details", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Member_ID", Request.Cookies["userid"].Value);
            con.Open();
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows == true)
            {
                DataTable member_details = new DataTable();
                member_details.Load(idr);

                payment_username = member_details.Rows[0]["Member_Name"].ToString();
                payment_userphone = member_details.Rows[0]["Contact_No"].ToString();
                payment_useremail = member_details.Rows[0]["Email"].ToString();
            }

            idr.Close();
            con.Close();
        }
        payment_refno = orderid;
        payment_amount = total_price.ToString();
        payment_remark = "";

        ClearCookie("side");
        ClearCookie("uplineid");

        Response.Redirect("Ipay88_Request_Open_New_Account.aspx?RefNo=" + payment_refno + "&Amount=" + payment_amount + "&Username=" + payment_username + "&UserEmail=" + payment_useremail + "&UserContact=" + payment_userphone + "&Remark=" + payment_remark);
    }

    public void ClearCookie(string cookieName)
    {
        if (Request.Cookies[cookieName] != null)
        {
            HttpCookie myCookie = new HttpCookie(cookieName);
            myCookie.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(myCookie);
        }
    }

    public static string RandomString(int length)
    {
        Random random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
        .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    protected void btn_back_Click(object sender, EventArgs e)
    {
        Response.Redirect("Register_Member_Open_New_Account_Product_List.aspx?id=" + Request.QueryString["id"].ToString());
    }
}