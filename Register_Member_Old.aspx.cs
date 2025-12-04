using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Register_Member_Old : System.Web.UI.Page
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
                    id = Request.Cookies["userid"].Value;
                    GetReferal(id);
                }
            }
            
            GetPolicy();
            Load_State();
            GetProduct();
            GetPackage();
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

    protected void GetReferal(string id)
    {
        using (SqlCommand cmd = new SqlCommand("SELECT First_Name FROM MF_CRM_Members WHERE cardno = @userid", con))
        {
            cmd.Parameters.AddWithValue("@userid", id);
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    txt_referal_id.Value = id;
                    txt_referal_id1.Value = id;
                    txt_referal_name.Value = sdr["First_Name"].ToString();
                    txt_referal_name1.Value = sdr["First_Name"].ToString();
                    txt_referal_id.Disabled = true;
                    txt_referal_name.Disabled = true;
                    txt_referal_id1.Disabled = true;
                    txt_referal_name1.Disabled = true;
                }
            }
            con.Close();
        }
    }

    protected void GetPolicy()
    {
        SqlCommand cmd = new SqlCommand("SELECT * FROM PolicyList", con);
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
    }

    protected void GetPackage()
    {
        SqlCommand cmd = new SqlCommand("Get_Package", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sda.Fill(dt);


        if (dt.Rows.Count > 0)
        {
            CheckPackage = dt.Rows[0]["Package"].ToString();
            hfCheck.Value = CheckPackage;
            if (CheckPackage == "N")
            {
                //col_package.Style["display"] = "none";
                Next.Text = "Register";
            }
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

    protected void Next_Click(object sender, EventArgs e)
    {

        string pattern = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";


        System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(txt_email.Value, pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

        if (string.IsNullOrEmpty(ddl_nationality.SelectedValue))
        {
            ShowMessage_warning("Please select your nationality.", MessageType.Warning);
            return;
        }
        else if (string.IsNullOrEmpty(txt_dob.Value))
        {
            ShowMessage_warning("Please key in your date of birth.", MessageType.Warning);
            return;
        }
        else if (string.IsNullOrEmpty(txt_phonenumber.Value))
        {
            ShowMessage_warning("Please key in your mobile number.", MessageType.Warning);
            return;
        }
        else if (string.IsNullOrEmpty(txt_email.Value))
        {
            ShowMessage_warning("Please key in your email.", MessageType.Warning);
            return;
        }
        else if (string.IsNullOrEmpty(txt_city.Value))
        {
            ShowMessage_warning("Please key in your city.", MessageType.Warning);
            return;
        }
        else if (string.IsNullOrEmpty(txt_address1.Value))
        {
            ShowMessage_warning("Please key in your address.", MessageType.Warning);
            return;
        }
        else if (string.IsNullOrEmpty(txt_postcode.Value))
        {
            ShowMessage_warning("Please key in your postcode.", MessageType.Warning);
            return;
        }
        else if (txt_email.Value != "" && !match.Success)
        {
            ShowMessage_warning("Please enter a valid email address.", MessageType.Warning);
            txt_email.Focus();
        }
        else if (string.IsNullOrEmpty(ddl_package.Value))
        {
            ShowMessage_warning("Please select your profit center.", MessageType.Warning);
            return;
        }
        else
        {

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
                else
                {
                    if (hfCheck.Value == "N")
                    {
                        string isMarried = "";
                        if (btn_married.Checked == true)
                        {
                            isMarried = "Y";
                        }
                        else
                        {
                            isMarried = "N";
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
                            using (SqlCommand cmd = new SqlCommand("Insert_New_Member_v2", con))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("Referal_ID", txt_referal_id.Value);
                                cmd.Parameters.AddWithValue("Phone_Number", txt_phonenumber.Value);
                                cmd.Parameters.AddWithValue("Full_Name", txt_fullname.Value);
                                cmd.Parameters.AddWithValue("Company_Name", txt_companyname.Value);
                                cmd.Parameters.AddWithValue("Company_No", txt_companyno.Value);
                                cmd.Parameters.AddWithValue("Email", txt_email.Value);
                                cmd.Parameters.AddWithValue("Nationality", ddl_nationality.SelectedValue);
                                cmd.Parameters.AddWithValue("Gender", ddl_gender.Value);
                                cmd.Parameters.AddWithValue("Married", isMarried);
                                cmd.Parameters.AddWithValue("Add1", txt_address1.Value);
                                cmd.Parameters.AddWithValue("Add2", txt_address2.Value);
                                cmd.Parameters.AddWithValue("Postcode", txt_postcode.Value);
                                cmd.Parameters.AddWithValue("City", txt_city.Value);
                                cmd.Parameters.AddWithValue("Country", ddl_country.SelectedValue);
                                cmd.Parameters.AddWithValue("State", ddl_State.SelectedValue);
                                cmd.Parameters.AddWithValue("DOB", txt_dob.Value);
                                cmd.Parameters.AddWithValue("Password", txt_confirmpassword.Value);
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

                                // Set Output Paramater
                                SqlParameter MobileParam = new SqlParameter("@MobileExist", SqlDbType.Bit);
                                MobileParam.Direction = ParameterDirection.Output;
                                cmd.Parameters.Add(MobileParam);

                                // Set Output Paramater
                                SqlParameter ReferalParam = new SqlParameter("@ReferalExist", SqlDbType.Bit);
                                ReferalParam.Direction = ParameterDirection.Output;
                                cmd.Parameters.Add(ReferalParam);

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

                                bool MobileExist = Convert.ToBoolean(cmd.Parameters["@MobileExist"].Value);
                                bool ReferalExists = Convert.ToBoolean(cmd.Parameters["@ReferalExist"].Value);
                                string userid = cmd.Parameters["@Userid"].Value.ToString();
                                string StatusExists = cmd.Parameters["@Status"].Value.ToString();

                                con.Close();

                                if (StatusExists == "success")
                                {
                                    ShowMessage("Registration Successful.", MessageType.Success);
                                }
                                else if (StatusExists == "failed")
                                {
                                    if (MobileExist == true)
                                    {
                                        ShowMessage_warning("Phone number already registered, please use another phone number to register", MessageType.Warning);
                                        txt_phonenumber.Focus();
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
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "packagePage()", true);
                    }

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
                else
                {
                    if (hfCheck.Value == "N")
                    {
                        string isMarried = "";
                        if (btn_married.Checked == true)
                        {
                            isMarried = "Y";
                        }
                        else
                        {
                            isMarried = "N";
                        }

                        //if (string.IsNullOrEmpty(txt_password.Value) || !Regex.IsMatch(txt_confirmpassword.Value, "^(?=.*?[A-Za-z])(?=.*?[0-9])[A-Za-z0-9$@$!%*?&]{6,}$"))
                        //{
                        //    ShowMessage_warning("Password should be at least 6 letters and should include at least 1 alphabet and 1 number. Special characters only allow are !@%*&?", MessageType.Warning);
                        //    return;
                        //}
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
                            using (SqlCommand cmd = new SqlCommand("Insert_New_Member", con))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("Referal_ID", txt_referal_id.Value);
                                cmd.Parameters.AddWithValue("Phone_Number", txt_phonenumber.Value);
                                cmd.Parameters.AddWithValue("Full_Name", txt_fullname.Value);
                                cmd.Parameters.AddWithValue("Company_Name", txt_companyname.Value);
                                cmd.Parameters.AddWithValue("Company_No", txt_companyno.Value);
                                cmd.Parameters.AddWithValue("Email", txt_email.Value);
                                cmd.Parameters.AddWithValue("Nationality", ddl_nationality.SelectedValue);
                                cmd.Parameters.AddWithValue("Gender", ddl_gender.Value);
                                cmd.Parameters.AddWithValue("Married", isMarried);
                                cmd.Parameters.AddWithValue("Add1", txt_address1.Value);
                                cmd.Parameters.AddWithValue("Add2", txt_address2.Value);
                                cmd.Parameters.AddWithValue("Postcode", txt_postcode.Value);
                                cmd.Parameters.AddWithValue("City", txt_city.Value);
                                cmd.Parameters.AddWithValue("Country", ddl_country.SelectedValue);
                                cmd.Parameters.AddWithValue("State", ddl_State.SelectedValue);
                                cmd.Parameters.AddWithValue("DOB", txt_dob.Value);
                                cmd.Parameters.AddWithValue("Password", txt_confirmpassword.Value);
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

                                // Set Output Paramater
                                SqlParameter MobileParam = new SqlParameter("@MobileExist", SqlDbType.Bit);
                                MobileParam.Direction = ParameterDirection.Output;
                                cmd.Parameters.Add(MobileParam);

                                // Set Output Paramater
                                SqlParameter ReferalParam = new SqlParameter("@ReferalExist", SqlDbType.Bit);
                                ReferalParam.Direction = ParameterDirection.Output;
                                cmd.Parameters.Add(ReferalParam);

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

                                bool MobileExist = Convert.ToBoolean(cmd.Parameters["@MobileExist"].Value);
                                bool ReferalExists = Convert.ToBoolean(cmd.Parameters["@ReferalExist"].Value);
                                string userid = cmd.Parameters["@Userid"].Value.ToString();
                                string StatusExists = cmd.Parameters["@Status"].Value.ToString();

                                con.Close();

                                if (StatusExists == "success")
                                {
                                    ShowMessage("Registration Successful.", MessageType.Success);
                                }
                                else if (StatusExists == "failed")
                                {
                                    if (MobileExist == true)
                                    {
                                        ShowMessage_warning("Phone number already registered, please use another phone number to register", MessageType.Warning);
                                        txt_phonenumber.Focus();
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
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "packagePage()", true);
                    }
                }
            }
        }

    }

    protected void selectAll_Click(object sender, EventArgs e)
    {
        bool checkAll = chkall.Checked;

        foreach (RepeaterItem item in rpt_policy.Items)
        {
            HtmlInputCheckBox chkPolicy = (HtmlInputCheckBox)item.FindControl("chkPolicy");
            chkPolicy.Checked = checkAll;
            System.Diagnostics.Debug.WriteLine("Checkbox ID: " + chkPolicy.ID + ", Checked: " + chkPolicy.Checked);
        }
    }

    protected void chkall_CheckedChanged(object sender, EventArgs e)
    {
        bool checkAll = chkall.Checked;

        foreach (RepeaterItem item in rpt_policy.Items)
        {
            HtmlInputCheckBox chkPolicy = (HtmlInputCheckBox)item.FindControl("chkPolicy");
            chkPolicy.Checked = checkAll;
        }

    }

    protected void GetProduct()
    {
        SqlCommand cmdproduct = new SqlCommand("Load_Product", con);
        cmdproduct.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter sda = new SqlDataAdapter(cmdproduct);
        DataTable tbl = new DataTable();
        sda.Fill(tbl);

        if (tbl.Rows.Count > 0)
        {
            rpt_product.DataSource = tbl;
            rpt_product.DataBind();
            rpt_alacarte.DataSource = tbl;
            rpt_alacarte.DataBind();
        }
    }

    protected void rpt_product_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        HiddenField hfProductCode = (HiddenField)e.Item.FindControl("hfProductCode");

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView drv = (DataRowView)(e.Item.DataItem);

            hfProductCode.Value = drv.Row["Product_Code"].ToString();
        }
    }

    protected void btnIncrement_Click(object sender, EventArgs e)
    {

        if (ddl_package.SelectedIndex == 1)
        {
            hfQuantity.Value = "1";
        }

        if (title.InnerText == "Package 1000BV")
        {
            hfQuantity.Value = "1";
        }

        string value = hfQuantity.Value;

        LinkButton btnIncrement = (LinkButton)sender;
        string productCode = btnIncrement.CommandArgument;
        //if(package1.Value == "Package 1000BV")
        //{
        //    package1.Checked = true;
        //    package2.Checked = false;
        //}
        //else if (package2.Value == "Package 3000BV")
        //{
        //    package1.Checked = false;
        //    package2.Checked = true;
        //}


        RepeaterItem item = (RepeaterItem)btnIncrement.NamingContainer;
        TextBox txtQuantity = (TextBox)item.FindControl("txtQuantity");

        // Check if the product code matches
        if (productCode != null && txtQuantity != null)
        {
            int quantity;



            if (int.TryParse(txtQuantity.Text, out quantity))
            {
                if (quantity < 1)
                {
                    quantity++;
                }
            }
            txtQuantity.Text = quantity.ToString();

            if (int.TryParse(txtQuantity.Text, out quantity))
            {
                if (quantity < 3)
                {
                    quantity++;
                }
            }
            txtQuantity.Text = quantity.ToString();

        }
    }

    protected void btnDecrement_Click(object sender, EventArgs e)
    {
        LinkButton btnDecrement = (LinkButton)sender;
        string productCode = btnDecrement.CommandArgument;

        RepeaterItem item = (RepeaterItem)btnDecrement.NamingContainer;
        TextBox txtQuantity = (TextBox)item.FindControl("txtQuantity");

        // Check if the product code matches
        if (productCode != null && txtQuantity != null)
        {
            int quantity;
            // Increment the quantity
            if (int.TryParse(txtQuantity.Text, out quantity))
            {
                if (quantity > 0)
                {
                    quantity--;
                }
            }
            txtQuantity.Text = quantity.ToString();
        }
    }

    protected void btn_continue_Click(object sender, EventArgs e)
    {
        ddl_nationality1.SelectedValue = ddl_nationality.SelectedValue;
        ddl_nationality1.Enabled = false;
        txt_dob1.Value = txt_dob.Value;
        txt_dob1.Disabled = true;
        ddl_gender1.Value = ddl_gender.Value;
        ddl_gender1.Disabled = true;
        txt_phonenumber1.Value = txt_phonenumber.Value;
        txt_phonenumber1.Disabled = true;
        txt_email1.Value = txt_email.Value;
        txt_email1.Disabled = true;
        bool married = btn_married.Checked;
        bool placement = btn_placement.Checked;
        if (married == false)
        {
            btn_married1.Checked = false;
        }
        else
        {
            btn_married1.Checked = true;
        }
        if (placement == false)
        {
            btn_placement1.Checked = false;
        }
        else
        {
            btn_placement1.Checked = true;
        }
        btn_married1.Disabled = true;
        btn_placement1.Disabled = true;
        txt_country1.Value = txt_country.Value;
        txt_country1.Disabled = true;
        txt_state1.Value = txt_state.Value;
        txt_state1.Disabled = true;
        txt_city1.Value = txt_city.Value;
        txt_city1.Disabled = true;
        txt_password1.Value = txt_password.Value;
        txt_password1.Disabled = true;
        txt_confirmpassword1.Value = txt_confirmpassword.Value;
        txt_confirmpassword1.Disabled = true;
        txtAddress1.Value = txt_address1.Value;
        txtAddress1.Disabled = true;
        txtAddress2.Value = txt_address2.Value;
        txtAddress2.Disabled = true;
        txt_postcode1.Value = txt_postcode.Value;
        txt_postcode1.Disabled = true;
        btn_company1.Disabled = true;
        btn_name1.Disabled = true;
        ddl_ref1.SelectedValue = ddl_ref.SelectedValue;
        ddl_ref1.Enabled = false;
        ddl_package1.InnerText = ddl_package.Value;
        if (ddl_package.Value == "Package 1000BV")
        {
            lbl_package.Text = "1000BV";
            lbl_bv.Text = "1000BV";
        }
        else
        {
            lbl_package.Text = "3000BV";
            lbl_bv.Text = "3000BV";
        }

        if (divNameVisible.Value == "false")
        {
            divName1.Style["display"] = "none";
            div_nricpass1.Style["display"] = "none";
            div_nric1.Style["display"] = "none";
            div_cmpname1.Style["display"] = "block";
            div_cmpno1.Style["display"] = "block";
            txt_companyname1.Value = txt_companyname.Value;
            txt_companyname1.Disabled = true;
            txt_companyno1.Value = txt_companyno.Value;
            txt_companyno1.Disabled = true;
            btn_company1.Style["border-color"] = "black";
            btn_company1.Style["color"] = "black";
            btn_company1.Style["font-weight"] = "bold";
            btn_name1.Style["border-color"] = "#777777";
            btn_name1.Style["color"] = "#777777";
            btn_name1.Style["font-weight"] = "400";
        }
        else
        {
            txt_fullname1.Value = txt_fullname.Value;
            txt_fullname1.Disabled = true;
            ddl_type1.SelectedValue = ddl_type.SelectedValue;
            ddl_type1.Enabled = false;
            txt_nric1.Text = txt_nric.Text;
            txt_nric1.Enabled = false;
        }





        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "summaryPage()", true);
    }

    protected void btn_back_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "registrationPage()", true);
    }

    protected void btn_edit_Click(object sender, EventArgs e)
    {
        ddl_nationality1.Enabled = true;
        txt_dob1.Disabled = false;
        ddl_gender1.Disabled = false;
        txt_phonenumber1.Disabled = false;
        txt_email1.Disabled = false;
        btn_married1.Disabled = false;
        txt_country1.Disabled = false;
        txt_state1.Disabled = false;
        txt_city1.Disabled = false;
        txtAddress1.Disabled = false;
        txtAddress2.Disabled = false;
        txt_postcode1.Disabled = false;
        btn_company1.Disabled = false;
        btn_name1.Disabled = false;
        txt_companyname1.Disabled = false;
        txt_companyno1.Disabled = false;
        txt_fullname1.Disabled = false;
        ddl_type1.Enabled = true;
        txt_nric1.Enabled = true;
        btn_placement1.Disabled = false;
        ddl_ref1.Enabled = true;
    }

    protected void btn_continue1_Click(object sender, EventArgs e)
    {
        string isMarried = "";
        if (btn_married1.Checked == true)
        {
            isMarried = "Yes";
        }
        else
        {
            isMarried = "No";
        }

        string pattern = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";

        System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(txt_email1.Value, pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

        if (string.IsNullOrEmpty(txt_password1.Value) || !Regex.IsMatch(txt_confirmpassword1.Value, "^(?=.*?[A-Za-z])(?=.*?[0-9])[A-Za-z0-9$@$!%*?&]{6,}$"))
        {
            ShowMessage_warning("Password should be at least 6 letters and should include at least 1 alphabet and 1 number. Special characters only allow are !@%*&?", MessageType.Warning);
            return;
        }
        else if (txt_password1.Value != txt_confirmpassword1.Value)
        {
            ShowMessage_warning("Passwords not match.", MessageType.Warning);
            return;
        }
        else
        {
            using (SqlCommand cmd = new SqlCommand("Insert_New_Member", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Referal_ID", txt_referal_id1.Value);
                cmd.Parameters.AddWithValue("Phone_Number", txt_phonenumber1.Value);
                cmd.Parameters.AddWithValue("Full_Name", txt_fullname1.Value);
                cmd.Parameters.AddWithValue("Company_Name", txt_companyname1.Value);
                cmd.Parameters.AddWithValue("Company_No", txt_companyno1.Value);
                cmd.Parameters.AddWithValue("Email", txt_email1.Value);
                cmd.Parameters.AddWithValue("Nationality", ddl_nationality1.SelectedValue);
                cmd.Parameters.AddWithValue("Gender", ddl_gender1.Value);
                cmd.Parameters.AddWithValue("Married", isMarried);
                cmd.Parameters.AddWithValue("Add1", txtAddress1.Value);
                cmd.Parameters.AddWithValue("Add2", txtAddress2.Value);
                cmd.Parameters.AddWithValue("Postcode", txt_postcode1.Value);
                cmd.Parameters.AddWithValue("City", txt_city1.Value);
                cmd.Parameters.AddWithValue("Country", ddl_country.SelectedValue);
                cmd.Parameters.AddWithValue("State", ddl_State.SelectedValue);
                cmd.Parameters.AddWithValue("DOB", txt_dob1.Value);
                cmd.Parameters.AddWithValue("Password", txt_confirmpassword1.Value);
                if (ddl_type1.SelectedValue == "NRIC")
                {
                    cmd.Parameters.AddWithValue("NRIC", txt_nric1.Text);
                    cmd.Parameters.AddWithValue("Passport", "");

                }
                else if (ddl_type.SelectedValue == "Passport")
                {
                    cmd.Parameters.AddWithValue("NRIC", "");
                    cmd.Parameters.AddWithValue("Passport", txt_passport1.Text);
                }

                // Set Output Paramater
                SqlParameter MobileParam = new SqlParameter("@MobileExist", SqlDbType.Bit);
                MobileParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(MobileParam);

                // Set Output Paramater
                SqlParameter ReferalParam = new SqlParameter("@ReferalExist", SqlDbType.Bit);
                ReferalParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(ReferalParam);

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

                bool MobileExist = Convert.ToBoolean(cmd.Parameters["@MobileExist"].Value);
                bool ReferalExists = Convert.ToBoolean(cmd.Parameters["@ReferalExist"].Value);
                string userid = cmd.Parameters["@Userid"].Value.ToString();
                string StatusExists = cmd.Parameters["@Status"].Value.ToString();

                con.Close();

                if (StatusExists == "success")
                {
                    ShowMessage("Registration Successful.", MessageType.Success);
                }
                else if (StatusExists == "failed")
                {
                    if (MobileExist == true)
                    {
                        ShowMessage_warning("Phone number already registered, please use another phone number to sign up", MessageType.Warning);
                        txt_phonenumber.Focus();
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

    protected void btn_back1_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "productPage()", true);
    }
}