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

public partial class Product : System.Web.UI.Page
{
    public enum MessageType { Success, Error, Info, Warning };
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            bool cookieExists = HttpContext.Current.Request.Cookies["userid"] != null;

            if (cookieExists == true)
            {
                if (Request.Cookies["userid"].Value != null && Request.Cookies["userid"].Value != "")
                {
                    Load_All_Product();
                    Load_Cart_Note();
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

    protected void Load_All_Product()
    {
        SqlCommand cmdproduct = new SqlCommand("Load_Front_End_All_Product_v2", con);
        cmdproduct.CommandType = CommandType.StoredProcedure;
        cmdproduct.Parameters.AddWithValue("Member_ID", Request.Cookies["userid"].Value);
        SqlDataAdapter sda = new SqlDataAdapter(cmdproduct);
        DataTable dt = new DataTable();
        sda.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            rpt_product.DataSource = dt;
            rpt_product.DataBind();
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
        Label lbl_product_RP = (Label)e.Item.FindControl("lbl_product_RP");
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
            if (drv.Row["Variation"].ToString() == "Y")
            {
                lbl_product_price.Text = "<b>RM " + Convert.ToDecimal(drv.Row["Variation Price"]).ToString("###,###,###.00") + "</b>";
                lbl_product_bv.Text = "BV : <b>" + Convert.ToDecimal(drv.Row["Variation BV"].ToString()).ToString("###,###,##0") + "</b>";
                lbl_product_RP.Text = "RP : <b>" + Convert.ToDecimal(drv.Row["Variation RP"].ToString()).ToString("###,###,##0") + "</b>";
            }
            else
            {
                lbl_product_price.Text = "<b>RM " + Convert.ToDecimal(drv.Row["Product_Retail_Price"]).ToString("###,###,###.00") + "</b>";
                lbl_product_bv.Text = "BV : <b>" + Convert.ToDecimal(drv.Row["Product_Retail_Price_BV_Points"].ToString()).ToString("###,###,##0") + "</b>";
                lbl_product_RP.Text = "RP : <b>" + Convert.ToDecimal(drv.Row["Product_RP_Points"].ToString()).ToString("###,###,##0") + "</b>";
            }

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

            if(Check_Package_Item_Available.Rows.Count > 0)
            {
                foreach (DataRow dt in Check_Package_Item_Available.Rows)
                {
                    if(dt["Package_Item_Quantity_Available_Status"].ToString() == "OUT OF STOCK")
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
        if(e.CommandName == "View_Product")
        {
            // Get the Product_Code from the CommandArgument
            string productCode = e.CommandArgument.ToString();

            // Find the TextBox within the RepeaterItem
            TextBox txtQuantity = e.Item.FindControl("txtQuantity") as TextBox;

            Load_Product_Variation(e.CommandArgument.ToString(), txtQuantity.Text);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "Show_Variation();", true);
        }
    }

    protected void Load_Product_Variation(string product_code, string quantity)
    {
        bool variation = false;
        string variation_name = "";
        using (SqlCommand cmd = new SqlCommand("Load_Front_End_Product_Details", con))
        {
            con.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Product_Code", product_code);
            cmd.Parameters.AddWithValue("@Member_ID", Request.Cookies["userid"].Value);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                hdn_item_code.Value = product_code;
                lbl_user_dc.Text = Convert.ToDecimal(dt.Rows[0]["DC_Point"].ToString()).ToString("###,###,##0");
                lbl_user_rp.Text = Convert.ToDecimal(dt.Rows[0]["RP_Point"].ToString()).ToString("###,###,##0");
                lbl_retail_price.Text = "RM " + Convert.ToDecimal(dt.Rows[0]["Product_Retail_Price"]).ToString("###,###,###.00");
                lbl_retail_price_bv.Text = Convert.ToDecimal(dt.Rows[0]["Product_Retail_Price_BV_Points"]).ToString("###,###,##0");
                lbl_rp.Text = Convert.ToDecimal(dt.Rows[0]["Variation RP"]).ToString("###,###,##0");
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
                lbl_point_balance.Text = Convert.ToDecimal(dt.Rows[0]["DC_Point"].ToString()).ToString("###,###,##0");
                lbl_rp_balance.Text = Convert.ToDecimal(dt.Rows[0]["RP_Point"].ToString()).ToString("###,###,##0");
                txtQuantity1.Text = quantity;
                txtQuantity2.Text = "0";
                txtQuantity3.Text = "0";

                hdn_promotion.Value = dt.Rows[0]["Promotion"].ToString();
                hdn_retail_price.Value = dt.Rows[0]["Product_Retail_Price"].ToString();
                hdn_rp_points.Value = dt.Rows[0]["Variation RP"].ToString();
                hdn_promotion_price.Value = dt.Rows[0]["Product_Promotion_Price_Selling_Price"].ToString();
                hdn_promotion_dc.Value = dt.Rows[0]["Product_Promotion_Price_EC_Points"].ToString();
                hdn_total_dc.Value = dt.Rows[0]["DC_Point"].ToString();
                hdn_total_rp.Value = dt.Rows[0]["RP_Point"].ToString();
                hdn_total_quantity.Value = quantity;
                
                variation_name = dt.Rows[0]["Variation_Name"].ToString();
                if(dt.Rows[0]["Variation"].ToString() == "Y")
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

        if(variation)
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
            if(!string.IsNullOrEmpty(drv.Row["Product_Variation_Name"].ToString()))
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
        bool cookieExists = HttpContext.Current.Request.Cookies["userid"] != null;

        if (cookieExists == true)
        {
            if (Request.Cookies["userid"].Value != null && Request.Cookies["userid"].Value != "")
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
                    if(rbtnlist_variation_value.Items.Count > 0)
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

                    if(variationselected == false)
                    {
                        ShowMessage_warning("please select product variation ", MessageType.Warning);
                    }
                    else
                    {
                        AddItemtoCart(hdn_item_code.Value, variation_value);
                        UpdateMasterPage();
                        ShowMessage("Successful add to cart", MessageType.Success);
                    }
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
        using (SqlCommand cmdcheckvariation = new SqlCommand("Check_Cart", con))
        {
            con.Open();

            cmdcheckvariation.Parameters.AddWithValue("@MemberID", Request.Cookies["userid"].Value);
            cmdcheckvariation.Parameters.AddWithValue("@ItemCode", itemcode);
            cmdcheckvariation.Parameters.AddWithValue("@Variation", variation);

            cmdcheckvariation.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter idrcheckvariation = new SqlDataAdapter(cmdcheckvariation);

            DataTable v2 = new DataTable();
            idrcheckvariation.Fill(v2);

            if (v2.Rows.Count > 0)
            {
                using (SqlCommand cmdinsertcart = new SqlCommand("Update_Cart", con))
                {
                    cmdinsertcart.CommandType = CommandType.StoredProcedure;
                    cmdinsertcart.Parameters.AddWithValue("@MemberID", Request.Cookies["userid"].Value);
                    cmdinsertcart.Parameters.AddWithValue("@ItemCode", itemcode);
                    cmdinsertcart.Parameters.AddWithValue("@Variation", variation);
                    cmdinsertcart.Parameters.AddWithValue("@Normal_Quantity", txtQuantity1.Text);
                    cmdinsertcart.Parameters.AddWithValue("@DC_Quantity", txtQuantity2.Text);
                    cmdinsertcart.Parameters.AddWithValue("@RP_Quantity", txtQuantity3.Text);
                    cmdinsertcart.ExecuteNonQuery();
                }
            }
            else
            {
                using (SqlCommand cmdinsertcart = new SqlCommand("Insert_Cart", con))
                {
                    cmdinsertcart.CommandType = CommandType.StoredProcedure;
                    cmdinsertcart.Parameters.AddWithValue("@MemberID", Request.Cookies["userid"].Value);
                    cmdinsertcart.Parameters.AddWithValue("@ItemCode", itemcode);
                    cmdinsertcart.Parameters.AddWithValue("@Variation", variation);
                    cmdinsertcart.Parameters.AddWithValue("@Normal_Quantity", txtQuantity1.Text);
                    cmdinsertcart.Parameters.AddWithValue("@DC_Quantity", txtQuantity2.Text);
                    cmdinsertcart.Parameters.AddWithValue("@RP_Quantity", txtQuantity3.Text);
                    cmdinsertcart.ExecuteNonQuery();
                }
            }
            con.Close();
        }
    }

    protected void UpdateMasterPage()
    {
        HtmlGenericControl cartNo = (HtmlGenericControl)Page.Master.FindControl("cartNo");
        HtmlGenericControl mobile_cart = (HtmlGenericControl)Page.Master.FindControl("mobile_cart");

        bool cookieExists = HttpContext.Current.Request.Cookies["userid"] != null;

        if (cookieExists == true)
        {
            if (Request.Cookies["userid"].Value != null && Request.Cookies["userid"].Value != "")
            {
                using (SqlCommand cmd = new SqlCommand("Get_User_CartItem", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("user_id", Request.Cookies["userid"].Value);
                    con.Open();

                    SqlDataReader idr = cmd.ExecuteReader();

                    if (idr.HasRows == true)
                    {
                        DataTable g = new DataTable();
                        g.Load(idr);

                        mobile_cart.Visible = true;
                        mobile_cart.InnerText = g.Rows[0]["TotalQty"].ToString();
                        cartNo.Visible = true;
                        cartNo.InnerText = g.Rows[0]["TotalQty"].ToString();
                    }
                    else
                    {

                        mobile_cart.Visible = false;
                        mobile_cart.InnerText = "0";
                        cartNo.Visible = false;
                        cartNo.InnerText = "0";
                    }
                    con.Close();
                }

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

}