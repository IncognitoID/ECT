using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Register_Member_Share_Link_Product_List : System.Web.UI.Page
{
    public enum MessageType { Success, Error, Info, Warning };
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["id"].ToString()))
            {
                Load_Member_BV(Request.QueryString["id"].ToString());
                Load_All_Package();
                Load_All_Product();
                Load_Cart_Note();
            }
        }
    }

    protected void Load_Member_BV(string id)
    {
        using (SqlCommand cmd = new SqlCommand("Load_Temporarily_Member_Details", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Memberid", id);
            con.Open();
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows == true)
            {
                DataTable v = new DataTable();
                v.Load(idr);
                
                if (v.Rows[0]["Shopper"].ToString() == "Y")
                {
                    bool cookieExists = HttpContext.Current.Request.Cookies["language"] != null;
                    if (cookieExists == true)
                    {
                        if (Request.Cookies["language"].Value != null && Request.Cookies["language"].Value != "")
                        {
                            if (Request.Cookies["language"].Value == "Chinese")
                            {
                                lbl_346.InnerText = "购物者 - ";
                                lbl_pacakge.InnerText = "最低购买金额 RM 100";
                                lbl_package_2.InnerText = "最低购买金额 RM 100";
                                ListItem newItem2 = new ListItem("单品", "Single");
                                newItem2.Selected = true;
                                rbtn_package.Items.Add(newItem2);
                            }
                            else
                            {
                                lbl_346.InnerText = "Shopper - ";
                                lbl_pacakge.InnerText = "Minimum Purchase RM 100";
                                lbl_package_2.InnerText = "Minimum Purchase RM 100";
                                ListItem newItem2 = new ListItem("A La Carte", "Single");
                                newItem2.Selected = true;
                                rbtn_package.Items.Add(newItem2);
                            }
                        }
                        else
                        {
                            lbl_346.InnerText = "Shopper - ";
                            lbl_pacakge.InnerText = "Minimum Purchase RM 100";
                            lbl_package_2.InnerText = "Minimum Purchase RM 100";
                            ListItem newItem2 = new ListItem("A La Carte", "Single");
                            newItem2.Selected = true;
                            rbtn_package.Items.Add(newItem2);
                        }
                    }
                    else
                    {
                        lbl_346.InnerText = "Shopper - ";
                        lbl_pacakge.InnerText = "Minimum Purchase RM 100";
                        lbl_package_2.InnerText = "Minimum Purchase RM 100";
                        ListItem newItem2 = new ListItem("A La Carte", "Single");
                        newItem2.Selected = true;
                        rbtn_package.Items.Add(newItem2);
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "Show_Shopper();", true);
                }
                else if (v.Rows[0]["Minimum_Package"].ToString() == "1")
                {
                    hdn_minimum_package.Value = "1";

                    bool cookieExists = HttpContext.Current.Request.Cookies["language"] != null;
                    string language = cookieExists ? Request.Cookies["language"].Value : string.Empty;

                    // Default values for package
                    string lblPackageText = "Package 1,000BV";
                    string compulsoryText = "Package 1,000BV (Compulsory)";
                    string singleText = "A La Carte";

                    // Update values for Chinese language
                    if (language == "Chinese")
                    {
                        lblPackageText = "配套 1,000BV";
                        compulsoryText = "配套 1,000BV (必须)";
                        singleText = "单品";
                    }

                    // Set labels
                    lbl_pacakge.InnerText = lblPackageText;
                    lbl_package_2.InnerText = lblPackageText;

                    // Add items to radio button list
                    ListItem newItem = new ListItem(compulsoryText, "Package") { Selected = true };
                    ListItem newItem2 = new ListItem(singleText, "Single");
                    rbtn_package.Items.Add(newItem);
                    rbtn_package.Items.Add(newItem2);
                }
                else if (v.Rows[0]["Minimum_Package"].ToString() == "3")
                {
                    hdn_minimum_package.Value = "3";

                    bool cookieExists = HttpContext.Current.Request.Cookies["language"] != null;
                    string language = cookieExists ? Request.Cookies["language"].Value : string.Empty;

                    // Default values for package
                    string lblPackageText = "Package 3,000BV";
                    string compulsoryText = "Package 3,000BV (Compulsory)";
                    string singleText = "A La Carte";

                    // Update values for Chinese language
                    if (language == "Chinese")
                    {
                        lblPackageText = "配套 3,000BV";
                        compulsoryText = "配套 3,000BV (必须)";
                        singleText = "单品";
                    }

                    // Set labels
                    lbl_pacakge.InnerText = lblPackageText;
                    lbl_package_2.InnerText = lblPackageText;

                    // Add items to radio button list
                    ListItem newItem = new ListItem(compulsoryText, "Package") { Selected = true };
                    ListItem newItem2 = new ListItem(singleText, "Single");
                    rbtn_package.Items.Add(newItem);
                    rbtn_package.Items.Add(newItem2);
                }
                else if (v.Rows[0]["Minimum_Package"].ToString() == "5")
                {
                    hdn_minimum_package.Value = "5";

                    bool cookieExists = HttpContext.Current.Request.Cookies["language"] != null;
                    string language = cookieExists ? Request.Cookies["language"].Value : string.Empty;

                    // Default values for package
                    string lblPackageText = "Package 5,000BV";
                    string compulsoryText = "Package 5,000BV (Compulsory)";
                    string singleText = "A La Carte";

                    // Update values for Chinese language
                    if (language == "Chinese")
                    {
                        lblPackageText = "配套 5,000BV";
                        compulsoryText = "配套 5,000BV (必须)";
                        singleText = "单品";
                    }

                    // Set labels
                    lbl_pacakge.InnerText = lblPackageText;
                    lbl_package_2.InnerText = lblPackageText;

                    // Add items to radio button list
                    ListItem newItem = new ListItem(compulsoryText, "Package") { Selected = true };
                    ListItem newItem2 = new ListItem(singleText, "Single");
                    rbtn_package.Items.Add(newItem);
                    rbtn_package.Items.Add(newItem2);
                }
                else if (v.Rows[0]["Minimum_Package"].ToString() == "9")
                {
                    hdn_minimum_package.Value = "9";

                    bool cookieExists = HttpContext.Current.Request.Cookies["language"] != null;
                    string language = cookieExists ? Request.Cookies["language"].Value : string.Empty;

                    // Default values for package
                    string lblPackageText = "Package 9,000BV";
                    string compulsoryText = "Package 9,000BV (Compulsory)";
                    string singleText = "A La Carte";

                    // Update values for Chinese language
                    if (language == "Chinese")
                    {
                        lblPackageText = "配套 9,000BV";
                        compulsoryText = "配套 9,000BV (必须)";
                        singleText = "单品";
                    }

                    // Set labels
                    lbl_pacakge.InnerText = lblPackageText;
                    lbl_package_2.InnerText = lblPackageText;

                    // Add items to radio button list
                    ListItem newItem = new ListItem(compulsoryText, "Package") { Selected = true };
                    ListItem newItem2 = new ListItem(singleText, "Single");
                    rbtn_package.Items.Add(newItem);
                    rbtn_package.Items.Add(newItem2);
                }
            }

            idr.Close();
            con.Close();
        }
    }

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

    #region Package

    protected void Load_All_Package()
    {
        SqlCommand cmdproduct = new SqlCommand("Load_Front_End_Registration_All_Package", con);
        cmdproduct.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter sda = new SqlDataAdapter(cmdproduct);
        DataTable dt = new DataTable();
        sda.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            rpt_product.DataSource = dt;
            rpt_product.DataBind();
            div_no_package.Visible = false;
        }
        else
        {
            div_no_package.Visible = true;
        }
    }

    protected void rpt_product_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        HtmlGenericControl div_mask = (HtmlGenericControl)e.Item.FindControl("div_mask");
        HtmlGenericControl lbl_mask_title = (HtmlGenericControl)e.Item.FindControl("lbl_mask_title");
        HtmlGenericControl div_product_img = (HtmlGenericControl)e.Item.FindControl("div_product_img");
        HtmlGenericControl div_product_details = (HtmlGenericControl)e.Item.FindControl("div_product_details");
        HtmlImage img_product = (HtmlImage)e.Item.FindControl("img_product");
        Label lbl_product_name = (Label)e.Item.FindControl("lbl_product_name");
        Label lbl_product_price = (Label)e.Item.FindControl("lbl_product_price");
        Label lbl_product_bv = (Label)e.Item.FindControl("lbl_product_bv");
        LinkButton btn_view_cart = (LinkButton)e.Item.FindControl("btn_view_cart");

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView drv = (DataRowView)(e.Item.DataItem);
            bool package_quantity_available = true;
            bool package_available = true;
            bool package_publish = true;
            bool package_not_deleted = true;

            img_product.Src = "https://ecentra.com.my/Backoffice/" + drv.Row["Product_Image"].ToString();
            lbl_product_name.Text = drv.Row["Product_Name"].ToString();
            lbl_product_price.Text = "<b>RM " + Convert.ToDecimal(drv.Row["Product_Retail_Price"]).ToString("###,###,###.00") + "</b>";
            lbl_product_bv.Text = "BV : <b>" + Convert.ToDecimal(drv.Row["Product_Retail_Price_BV_Points"].ToString()).ToString("###,###,##0") + "</b>";

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
                        lbl_product_price.Text = "会员价 : <b>RM " + Convert.ToDecimal(drv.Row["Product_Retail_Price"]).ToString("###,###,###.00") + "</b>";

                        if (drv.Row["Sold_Out_Status"].ToString() == "Y")
                        {
                            div_mask.Attributes.Add("style", "display:block");
                            lbl_mask_title.InnerText = "售罄";
                            btn_view_cart.Enabled = false;
                        }
                        else if (drv.Row["Available_Qty"].ToString() == "0.00")
                        {
                            div_mask.Attributes.Add("style", "display:block");
                            lbl_mask_title.InnerText = "缺货";
                            btn_view_cart.Enabled = false;
                        }
                        else if (package_quantity_available == false)
                        {
                            div_mask.Attributes.Add("style", "display:block");
                            lbl_mask_title.InnerText = "缺货";
                            btn_view_cart.Enabled = false;
                        }
                        else if (package_available == false)
                        {
                            div_mask.Attributes.Add("style", "display:block");
                            lbl_mask_title.InnerText = "缺货";
                            btn_view_cart.Enabled = false;
                        }
                        else if (package_publish == false)
                        {
                            div_mask.Attributes.Add("style", "display:block");
                            lbl_mask_title.InnerText = "缺货";
                            btn_view_cart.Enabled = false;
                        }
                        else if (package_not_deleted == false)
                        {
                            div_mask.Attributes.Add("style", "display:block");
                            lbl_mask_title.InnerText = "缺货";
                            btn_view_cart.Enabled = false;
                        }
                        else
                        {
                            div_product_img.Attributes.Add("onclick", "redirectToProduct('" + drv.Row["Product_Code"].ToString() + "')");
                            div_product_details.Attributes.Add("onclick", "redirectToProduct('" + drv.Row["Product_Code"].ToString() + "')");
                        }
                    }
                    else
                    {
                        lbl_product_price.Text = "Member : <b>RM " + Convert.ToDecimal(drv.Row["Product_Retail_Price"]).ToString("###,###,###.00") + "</b>";

                        if (drv.Row["Sold_Out_Status"].ToString() == "Y")
                        {
                            div_mask.Attributes.Add("style", "display:block");
                            lbl_mask_title.InnerText = "SOLD OUT";
                            btn_view_cart.Enabled = false;
                        }
                        else if (drv.Row["Available_Qty"].ToString() == "0.00")
                        {
                            div_mask.Attributes.Add("style", "display:block");
                            lbl_mask_title.InnerText = "OUT OF STOCK";
                            btn_view_cart.Enabled = false;
                        }
                        else if (package_quantity_available == false)
                        {
                            div_mask.Attributes.Add("style", "display:block");
                            lbl_mask_title.InnerText = "OUT OF STOCK";
                            btn_view_cart.Enabled = false;
                        }
                        else if (package_available == false)
                        {
                            div_mask.Attributes.Add("style", "display:block");
                            lbl_mask_title.InnerText = "OUT OF STOCK";
                            btn_view_cart.Enabled = false;
                        }
                        else if (package_publish == false)
                        {
                            div_mask.Attributes.Add("style", "display:block");
                            lbl_mask_title.InnerText = "OUT OF STOCK";
                            btn_view_cart.Enabled = false;
                        }
                        else if (package_not_deleted == false)
                        {
                            div_mask.Attributes.Add("style", "display:block");
                            lbl_mask_title.InnerText = "OUT OF STOCK";
                            btn_view_cart.Enabled = false;
                        }
                        else
                        {
                            div_product_img.Attributes.Add("onclick", "redirectToProduct('" + drv.Row["Product_Code"].ToString() + "')");
                            div_product_details.Attributes.Add("onclick", "redirectToProduct('" + drv.Row["Product_Code"].ToString() + "')");
                        }
                    }
                }
                else
                {
                    lbl_product_price.Text = "Member : <b>RM " + Convert.ToDecimal(drv.Row["Product_Retail_Price"]).ToString("###,###,###.00") + "</b>";

                    if (drv.Row["Sold_Out_Status"].ToString() == "Y")
                    {
                        div_mask.Attributes.Add("style", "display:block");
                        lbl_mask_title.InnerText = "SOLD OUT";
                        btn_view_cart.Enabled = false;
                    }
                    else if (drv.Row["Available_Qty"].ToString() == "0.00")
                    {
                        div_mask.Attributes.Add("style", "display:block");
                        lbl_mask_title.InnerText = "OUT OF STOCK";
                        btn_view_cart.Enabled = false;
                    }
                    else if (package_quantity_available == false)
                    {
                        div_mask.Attributes.Add("style", "display:block");
                        lbl_mask_title.InnerText = "OUT OF STOCK";
                        btn_view_cart.Enabled = false;
                    }
                    else if (package_available == false)
                    {
                        div_mask.Attributes.Add("style", "display:block");
                        lbl_mask_title.InnerText = "OUT OF STOCK";
                        btn_view_cart.Enabled = false;
                    }
                    else if (package_publish == false)
                    {
                        div_mask.Attributes.Add("style", "display:block");
                        lbl_mask_title.InnerText = "OUT OF STOCK";
                        btn_view_cart.Enabled = false;
                    }
                    else if (package_not_deleted == false)
                    {
                        div_mask.Attributes.Add("style", "display:block");
                        lbl_mask_title.InnerText = "OUT OF STOCK";
                        btn_view_cart.Enabled = false;
                    }
                    else
                    {
                        div_product_img.Attributes.Add("onclick", "redirectToProduct('" + drv.Row["Product_Code"].ToString() + "')");
                        div_product_details.Attributes.Add("onclick", "redirectToProduct('" + drv.Row["Product_Code"].ToString() + "')");
                    }
                }
            }
            else
            {
                lbl_product_price.Text = "Member : <b>RM " + Convert.ToDecimal(drv.Row["Product_Retail_Price"]).ToString("###,###,###.00") + "</b>";

                if (drv.Row["Sold_Out_Status"].ToString() == "Y")
                {
                    div_mask.Attributes.Add("style", "display:block");
                    lbl_mask_title.InnerText = "SOLD OUT";
                    btn_view_cart.Enabled = false;
                }
                else if (drv.Row["Available_Qty"].ToString() == "0.00")
                {
                    div_mask.Attributes.Add("style", "display:block");
                    lbl_mask_title.InnerText = "OUT OF STOCK";
                    btn_view_cart.Enabled = false;
                }
                else if (package_quantity_available == false)
                {
                    div_mask.Attributes.Add("style", "display:block");
                    lbl_mask_title.InnerText = "OUT OF STOCK";
                    btn_view_cart.Enabled = false;
                }
                else if (package_available == false)
                {
                    div_mask.Attributes.Add("style", "display:block");
                    lbl_mask_title.InnerText = "OUT OF STOCK";
                    btn_view_cart.Enabled = false;
                }
                else if (package_publish == false)
                {
                    div_mask.Attributes.Add("style", "display:block");
                    lbl_mask_title.InnerText = "OUT OF STOCK";
                    btn_view_cart.Enabled = false;
                }
                else if (package_not_deleted == false)
                {
                    div_mask.Attributes.Add("style", "display:block");
                    lbl_mask_title.InnerText = "OUT OF STOCK";
                    btn_view_cart.Enabled = false;
                }
                else
                {
                    div_product_img.Attributes.Add("onclick", "redirectToProduct('" + drv.Row["Product_Code"].ToString() + "')");
                    div_product_details.Attributes.Add("onclick", "redirectToProduct('" + drv.Row["Product_Code"].ToString() + "')");
                }
            }
        }
    }

    protected void rpt_product_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "View_Product")
        {
            // Get the Product_Code from the CommandArgument
            string productCode = e.CommandArgument.ToString();

            // Find the TextBox within the RepeaterItem
            TextBox txtQuantity = e.Item.FindControl("txtQuantity") as TextBox;
            div_dc.Visible = false;
            Load_Product_Variation(e.CommandArgument.ToString(), txtQuantity.Text);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "Show_Variation();", true);
        }
    }

    #endregion

    #region Product

    protected void Load_All_Product()
    {
        SqlCommand cmdproduct = new SqlCommand("Load_Front_End_Registration_All_Product", con);
        cmdproduct.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter sda = new SqlDataAdapter(cmdproduct);
        DataTable dt = new DataTable();
        sda.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            rpt_single_product.DataSource = dt;
            rpt_single_product.DataBind();
            div_no_product.Visible = false;
        }
        else
        {
            div_no_product.Visible = true;
        }
    }

    protected void rpt_single_product_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        HtmlGenericControl div_mask = (HtmlGenericControl)e.Item.FindControl("div_mask");
        HtmlGenericControl lbl_mask_title = (HtmlGenericControl)e.Item.FindControl("lbl_mask_title");
        HtmlGenericControl div_product_img = (HtmlGenericControl)e.Item.FindControl("div_product_img");
        HtmlGenericControl div_product_details = (HtmlGenericControl)e.Item.FindControl("div_product_details");
        HtmlImage img_product = (HtmlImage)e.Item.FindControl("img_product");
        Label lbl_product_name = (Label)e.Item.FindControl("lbl_product_name");
        Label lbl_product_price = (Label)e.Item.FindControl("lbl_product_price");
        Label lbl_product_bv = (Label)e.Item.FindControl("lbl_product_bv");
        LinkButton btn_view_cart = (LinkButton)e.Item.FindControl("btn_view_cart");

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView drv = (DataRowView)(e.Item.DataItem);
            bool package_quantity_available = true;
            bool package_available = true;
            bool package_publish = true;
            bool package_not_deleted = true;

            img_product.Src = "https://ecentra.com.my/Backoffice/" + drv.Row["Product_Image"].ToString();
            lbl_product_name.Text = drv.Row["Product_Name"].ToString();
            lbl_product_price.Text = "<b>RM " + Convert.ToDecimal(drv.Row["Product_Retail_Price"]).ToString("###,###,###.00") + "</b>";
            lbl_product_bv.Text = "BV : <b>" + Convert.ToDecimal(drv.Row["Product_Retail_Price_BV_Points"].ToString()).ToString("###,###,##0") + "</b>";

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
                        lbl_product_price.Text = "会员价 : <b>RM " + Convert.ToDecimal(drv.Row["Product_Retail_Price"]).ToString("###,###,###.00") + "</b>";

                        if (drv.Row["Sold_Out_Status"].ToString() == "Y")
                        {
                            div_mask.Attributes.Add("style", "display:block");
                            lbl_mask_title.InnerText = "售罄";
                            btn_view_cart.Enabled = false;
                        }
                        else if (drv.Row["Available_Qty"].ToString() == "0.00")
                        {
                            div_mask.Attributes.Add("style", "display:block");
                            lbl_mask_title.InnerText = "缺货";
                            btn_view_cart.Enabled = false;
                        }
                        else if (package_quantity_available == false)
                        {
                            div_mask.Attributes.Add("style", "display:block");
                            lbl_mask_title.InnerText = "缺货";
                            btn_view_cart.Enabled = false;
                        }
                        else if (package_available == false)
                        {
                            div_mask.Attributes.Add("style", "display:block");
                            lbl_mask_title.InnerText = "缺货";
                            btn_view_cart.Enabled = false;
                        }
                        else if (package_publish == false)
                        {
                            div_mask.Attributes.Add("style", "display:block");
                            lbl_mask_title.InnerText = "缺货";
                            btn_view_cart.Enabled = false;
                        }
                        else if (package_not_deleted == false)
                        {
                            div_mask.Attributes.Add("style", "display:block");
                            lbl_mask_title.InnerText = "缺货";
                            btn_view_cart.Enabled = false;
                        }
                        else
                        {
                            div_product_img.Attributes.Add("onclick", "redirectToProduct('" + drv.Row["Product_Code"].ToString() + "')");
                            div_product_details.Attributes.Add("onclick", "redirectToProduct('" + drv.Row["Product_Code"].ToString() + "')");
                        }
                    }
                    else
                    {
                        lbl_product_price.Text = "Member : <b>RM " + Convert.ToDecimal(drv.Row["Product_Retail_Price"]).ToString("###,###,###.00") + "</b>";

                        if (drv.Row["Sold_Out_Status"].ToString() == "Y")
                        {
                            div_mask.Attributes.Add("style", "display:block");
                            lbl_mask_title.InnerText = "SOLD OUT";
                            btn_view_cart.Enabled = false;
                        }
                        else if (drv.Row["Available_Qty"].ToString() == "0.00")
                        {
                            div_mask.Attributes.Add("style", "display:block");
                            lbl_mask_title.InnerText = "OUT OF STOCK";
                            btn_view_cart.Enabled = false;
                        }
                        else if (package_quantity_available == false)
                        {
                            div_mask.Attributes.Add("style", "display:block");
                            lbl_mask_title.InnerText = "OUT OF STOCK";
                            btn_view_cart.Enabled = false;
                        }
                        else if (package_available == false)
                        {
                            div_mask.Attributes.Add("style", "display:block");
                            lbl_mask_title.InnerText = "OUT OF STOCK";
                            btn_view_cart.Enabled = false;
                        }
                        else if (package_publish == false)
                        {
                            div_mask.Attributes.Add("style", "display:block");
                            lbl_mask_title.InnerText = "OUT OF STOCK";
                            btn_view_cart.Enabled = false;
                        }
                        else if (package_not_deleted == false)
                        {
                            div_mask.Attributes.Add("style", "display:block");
                            lbl_mask_title.InnerText = "OUT OF STOCK";
                            btn_view_cart.Enabled = false;
                        }
                        else
                        {
                            div_product_img.Attributes.Add("onclick", "redirectToProduct('" + drv.Row["Product_Code"].ToString() + "')");
                            div_product_details.Attributes.Add("onclick", "redirectToProduct('" + drv.Row["Product_Code"].ToString() + "')");
                        }
                    }
                }
                else
                {
                    lbl_product_price.Text = "Member : <b>RM " + Convert.ToDecimal(drv.Row["Product_Retail_Price"]).ToString("###,###,###.00") + "</b>";

                    if (drv.Row["Sold_Out_Status"].ToString() == "Y")
                    {
                        div_mask.Attributes.Add("style", "display:block");
                        lbl_mask_title.InnerText = "SOLD OUT";
                        btn_view_cart.Enabled = false;
                    }
                    else if (drv.Row["Available_Qty"].ToString() == "0.00")
                    {
                        div_mask.Attributes.Add("style", "display:block");
                        lbl_mask_title.InnerText = "OUT OF STOCK";
                        btn_view_cart.Enabled = false;
                    }
                    else if (package_quantity_available == false)
                    {
                        div_mask.Attributes.Add("style", "display:block");
                        lbl_mask_title.InnerText = "OUT OF STOCK";
                        btn_view_cart.Enabled = false;
                    }
                    else if (package_available == false)
                    {
                        div_mask.Attributes.Add("style", "display:block");
                        lbl_mask_title.InnerText = "OUT OF STOCK";
                        btn_view_cart.Enabled = false;
                    }
                    else if (package_publish == false)
                    {
                        div_mask.Attributes.Add("style", "display:block");
                        lbl_mask_title.InnerText = "OUT OF STOCK";
                        btn_view_cart.Enabled = false;
                    }
                    else if (package_not_deleted == false)
                    {
                        div_mask.Attributes.Add("style", "display:block");
                        lbl_mask_title.InnerText = "OUT OF STOCK";
                        btn_view_cart.Enabled = false;
                    }
                    else
                    {
                        div_product_img.Attributes.Add("onclick", "redirectToProduct('" + drv.Row["Product_Code"].ToString() + "')");
                        div_product_details.Attributes.Add("onclick", "redirectToProduct('" + drv.Row["Product_Code"].ToString() + "')");
                    }
                }
            }
            else
            {
                lbl_product_price.Text = "Member : <b>RM " + Convert.ToDecimal(drv.Row["Product_Retail_Price"]).ToString("###,###,###.00") + "</b>";

                if (drv.Row["Sold_Out_Status"].ToString() == "Y")
                {
                    div_mask.Attributes.Add("style", "display:block");
                    lbl_mask_title.InnerText = "SOLD OUT";
                    btn_view_cart.Enabled = false;
                }
                else if (drv.Row["Available_Qty"].ToString() == "0.00")
                {
                    div_mask.Attributes.Add("style", "display:block");
                    lbl_mask_title.InnerText = "OUT OF STOCK";
                    btn_view_cart.Enabled = false;
                }
                else if (package_quantity_available == false)
                {
                    div_mask.Attributes.Add("style", "display:block");
                    lbl_mask_title.InnerText = "OUT OF STOCK";
                    btn_view_cart.Enabled = false;
                }
                else if (package_available == false)
                {
                    div_mask.Attributes.Add("style", "display:block");
                    lbl_mask_title.InnerText = "OUT OF STOCK";
                    btn_view_cart.Enabled = false;
                }
                else if (package_publish == false)
                {
                    div_mask.Attributes.Add("style", "display:block");
                    lbl_mask_title.InnerText = "OUT OF STOCK";
                    btn_view_cart.Enabled = false;
                }
                else if (package_not_deleted == false)
                {
                    div_mask.Attributes.Add("style", "display:block");
                    lbl_mask_title.InnerText = "OUT OF STOCK";
                    btn_view_cart.Enabled = false;
                }
                else
                {
                    div_product_img.Attributes.Add("onclick", "redirectToProduct('" + drv.Row["Product_Code"].ToString() + "')");
                    div_product_details.Attributes.Add("onclick", "redirectToProduct('" + drv.Row["Product_Code"].ToString() + "')");
                }
            }
        }
    }

    protected void rpt_single_product_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "View_Product")
        {
            string productCode = e.CommandArgument.ToString();
            TextBox txtQuantity = e.Item.FindControl("txtQuantity") as TextBox;
            div_dc.Visible = true;
            Load_Product_Variation(e.CommandArgument.ToString(), txtQuantity.Text);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "Show_Variation();", true);
        }
    }

    #endregion

    protected void Load_Product_Variation(string product_code, string quantity)
    {
        bool variation = false;
        string variation_name = "";
        using (SqlCommand cmd = new SqlCommand("Load_Front_End_Registration_Product_Details", con))
        {
            con.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Product_Code", product_code);
            cmd.Parameters.AddWithValue("@Member_ID", Request.QueryString["id"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                hdn_item_code.Value = product_code;
                lbl_user_dc.Text = dt.Rows[0]["DC_Point"].ToString();
                lbl_retail_price.Text = "RM " + Convert.ToDecimal(dt.Rows[0]["Product_Retail_Price"]).ToString("###,###,###.00");
                lbl_retail_price_bv.Text = Convert.ToDecimal(dt.Rows[0]["Product_Retail_Price_BV_Points"]).ToString("###,###,##0");
                if (dt.Rows[0]["Promotion"].ToString() == "Y" &&
                    (DateTime.UtcNow.AddHours(8) > Convert.ToDateTime(dt.Rows[0]["Product_Promotion_Duration_From"])) &&
                    (DateTime.UtcNow.AddHours(8) < Convert.ToDateTime(dt.Rows[0]["Product_Promotion_Duration_To"])))
                {
                    div_promotion.Attributes.Add("style", "display:block");
                    lbl_point_balance.Attributes.Add("style", "display:unset");
                    lbl_promotion_price.Text = "RM " + Convert.ToDecimal(dt.Rows[0]["Product_Promotion_Price_Selling_Price"]).ToString("###,###,###.00") + " + " + Convert.ToInt32(dt.Rows[0]["Product_Promotion_Price_EC_Points"]).ToString();
                    lbl_promotion_price_bv.Text = Convert.ToDecimal(dt.Rows[0]["Product_Promotion_Price_BV_Points"]).ToString("###,###,##0");
                }
                else
                {
                    div_promotion.Attributes.Add("style", "display:none");
                    lbl_point_balance.Attributes.Add("style", "display:none");
                }
                lbl_variation_name.Text = dt.Rows[0]["Variation_Name"].ToString();
                lbl_total_quantity.Text = quantity;
                lbl_total_amount.Text = "RM " + (Convert.ToDecimal(dt.Rows[0]["Product_Retail_Price"].ToString()) * Convert.ToDecimal(quantity)).ToString("###,###,###.00");
                lbl_point_balance.Text = dt.Rows[0]["DC_Point"].ToString();
                txtQuantity1.Text = quantity;
                txtQuantity2.Text = "0";

                hdn_promotion.Value = dt.Rows[0]["Promotion"].ToString();
                hdn_retail_price.Value = dt.Rows[0]["Product_Retail_Price"].ToString();
                hdn_promotion_price.Value = dt.Rows[0]["Product_Promotion_Price_Selling_Price"].ToString();
                hdn_promotion_dc.Value = dt.Rows[0]["Product_Promotion_Price_EC_Points"].ToString();
                hdn_total_dc.Value = dt.Rows[0]["DC_Point"].ToString();
                hdn_total_quantity.Value = quantity;

                variation_name = dt.Rows[0]["Variation_Name"].ToString();
                if (dt.Rows[0]["Variation"].ToString() == "Y")
                {
                    variation = true;
                }

                if (dt.Rows[0]["Package"].ToString() == "Y")
                {
                    div_package.Visible = true;
                    Load_Package_Item(product_code);
                }
                else
                {
                    div_package.Visible = false;
                }
            }
            con.Close();
        }

        if (variation)
        {
            div_variation.Visible = true;
            LoadVariation(product_code, variation_name);
        }
        else
        {
            rbtnlist_variation_value.Items.Clear();
            div_variation.Visible = false;
        }
    }

    protected void LoadVariation(string product_code, string variation_name)
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

                if (quantity == 0)
                {
                    item.Enabled = false; // Disable the individual radio button if quantity is 0
                }
            }

            rbtnlist_variation_value.Attributes.Add("onclick", "VariationSeleted()");

            con.Close();
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

    protected void btn_final_add_to_cart_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtQuantity1.Text))
        {
            ShowMessage_warning("please select product quantity ", MessageType.Warning);
            txtQuantity1.Focus();
        }
        else
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
                ShowMessage("Successful add to cart", MessageType.Success);
            }
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
                using (SqlCommand cmdinsertcart = new SqlCommand("Update_Registration_Cart", con))
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
                using (SqlCommand cmdinsertcart = new SqlCommand("Insert_Registration_Cart", con))
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

    protected void btn_back_Click(object sender, EventArgs e)
    {
        Response.Redirect("Register_Member_Share_Link.aspx?referral_id=" + Request.QueryString["referral_id"].ToString() + "&id=" + Request.QueryString["id"].ToString());
    }

    protected void btn_continue_Click(object sender, EventArgs e)
    {
        DataTable v = new DataTable();
        using (SqlCommand cmd = new SqlCommand("Load_Registration_Member_Checkout_Cart", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Memberid", Request.QueryString["id"].ToString());
            con.Open();

            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows == true)
            {
                v.Load(idr);
            }
            else
            {
                ShowMessage_warning("Please add package in your cart, before checkout", MessageType.Warning);
                return;
            }

            idr.Close();
            con.Close();
        }

        if (v.Rows.Count > 0)
        {
            using (SqlCommand cmd = new SqlCommand("Check_Regsitration_Member_Cart", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Memberid", Request.QueryString["id"].ToString());

                // Set Output Paramater
                SqlParameter StatusParam = new SqlParameter("@Status", SqlDbType.VarChar, 200);
                StatusParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(StatusParam);

                con.Open();

                cmd.ExecuteNonQuery();

                string Status = cmd.Parameters["@Status"].Value.ToString();
                
                if (Status == "MINIMUM RM 150")
                {
                    string message = "";
                    message = "Minimum purchase RM 150 product";
                    ShowMessage_warning(message, MessageType.Warning);
                    return;
                }
                else if (Status == "MINIMUM 1 PACKAGE")
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
                else if (Status == "MINIMUM 5 PACKAGE")
                {
                    string message = "";
                    message = "Minimum purchase 5 package";
                    ShowMessage_warning(message, MessageType.Warning);
                    return;
                }
                else if (Status == "MINIMUM 9 PACKAGE")
                {
                    string message = "";
                    message = "Minimum purchase 9 package";
                    ShowMessage_warning(message, MessageType.Warning);
                    return;
                }
                else
                {
                    Response.Redirect("Register_Member_Share_Link_Checkout.aspx?referral_id=" + Request.QueryString["referral_id"].ToString() + "&id=" + Request.QueryString["id"].ToString());
                }

                con.Close();

            }
        }
    }
}