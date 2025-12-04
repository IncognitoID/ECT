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

public partial class Product_Details : System.Web.UI.Page
{
    public enum MessageType { Success, Error, Info, Warning };
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            Load_Product_Image(Request.QueryString["id"].ToString());
            Load_Product_Details();
        }
    }

    protected void Load_Product_Image(string product_code)
    {
        using (SqlCommand cmd = new SqlCommand("Load_Front_End_Product_Image", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Product_Code", product_code);
            con.Open();
            SqlDataAdapter idr = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            idr.Fill(dt);

            // Group images into sets of 4
            var chunks = new List<List<DataRow>>();
            for (int i = 0; i < dt.Rows.Count; i += 4)
            {
                var chunk = new List<DataRow>();
                for (int j = 0; j < 4 && i + j < dt.Rows.Count; j++)
                {
                    chunk.Add(dt.Rows[i + j]);
                }
                chunks.Add(chunk);
            }

            rptCarousel.DataSource = chunks;
            rptCarousel.DataBind();

            con.Close();
        }
    }

    protected void rptCarousel_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            var rptImages = (Repeater)e.Item.FindControl("rptImages");
            var imageUrls = (List<DataRow>)e.Item.DataItem;

            rptImages.DataSource = imageUrls;
            rptImages.DataBind();
        }
    }

    protected void rptImages_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        HtmlImage product_img = (HtmlImage)e.Item.FindControl("product_img");

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRow drv = (DataRow)(e.Item.DataItem);

            product_img.Src = "https://ecentra.com.my/Backoffice/" + drv["Product_Img"].ToString();

            if (drv["Position"].ToString() == "1")
            {
                string first_image = "https://ecentra.com.my/Backoffice/" + drv["Product_Img"].ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "Set_First_Image('" + first_image + "');", true);
            }
        }
    }

    protected void Load_Product_Details()
    {
        string quantity = "1";
        bool variation = false;
        string variation_name = "";
        string product_code = "";
        bool package_quantity_available = true;
        bool package_available = true;
        bool package_publish = true;
        bool package_not_deleted = true;

        using (SqlCommand cmd = new SqlCommand("Load_Front_End_Product_Details", con))
        {
            con.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Product_Code", Request.QueryString["id"].ToString());
            cmd.Parameters.AddWithValue("@Member_ID", Request.Cookies["userid"].Value);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                product_code = dt.Rows[0]["Product_Code"].ToString();
                lbl_product_name.Text = dt.Rows[0]["Product_Name"].ToString();
                lbl_product_description.Text = dt.Rows[0]["Product_Description"].ToString();
                lbl_ingredients.Text = dt.Rows[0]["Ingredients_Description"].ToString();
                lbl_how_to_use.Text = dt.Rows[0]["How_To_Use_Description"].ToString();
                lbl_more_info.Text = dt.Rows[0]["More_Info_Description"].ToString();
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
                    lbl_point_balance.Attributes.Add("style", "display:content");
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

                hdn_promotion.Value = dt.Rows[0]["Promotion"].ToString();
                hdn_retail_price.Value = dt.Rows[0]["Product_Retail_Price"].ToString();
                hdn_rp_points.Value = dt.Rows[0]["Variation RP"].ToString();
                hdn_promotion_price.Value = dt.Rows[0]["Product_Promotion_Price_Selling_Price"].ToString();
                hdn_promotion_dc.Value = dt.Rows[0]["Product_Promotion_Price_EC_Points"].ToString();
                hdn_total_dc.Value = dt.Rows[0]["DC_Point"].ToString();
                hdn_total_rp.Value = dt.Rows[0]["RP_Point"].ToString();
                hdn_total_quantity.Value = quantity;

                variation_name = dt.Rows[0]["Variation_Name"].ToString();
                if (dt.Rows[0]["Variation"].ToString() == "Y")
                {
                    variation = true;
                }

                DataTable Check_Package_Item_Available = new DataTable();
                if (dt.Rows[0]["Package"].ToString() == "Y")
                {
                    ingredients_tab.Visible = false;
                    how_to_use_tab.Visible = false;
                    more_info_tab.Visible = false;
                    div_package.Visible = true;
                    Load_Package_Item(product_code);
                    using (SqlCommand cmd_package = new SqlCommand("Check_Package_Item", con))
                    {
                        cmd_package.CommandType = CommandType.StoredProcedure;
                        cmd_package.Parameters.AddWithValue("@Product_Code", dt.Rows[0]["Product_Code"].ToString());
                        SqlDataAdapter idr = new SqlDataAdapter(cmd_package);

                        idr.Fill(Check_Package_Item_Available);
                    }
                }
                else
                {
                    div_package.Visible = false;
                }

                if (Check_Package_Item_Available.Rows.Count > 0)
                {
                    foreach (DataRow dt_item in Check_Package_Item_Available.Rows)
                    {
                        if (dt_item["Package_Item_Quantity_Available_Status"].ToString() == "OUT OF STOCK")
                        {
                            package_quantity_available = false;
                        }
                        if (dt_item["Package_Item_Sold_Out_Status"].ToString() == "SOLD OUT")
                        {
                            package_available = false;
                        }
                        if (dt_item["Package_Item_Publish_Status"].ToString() == "UNPUBLISH")
                        {
                            package_publish = false;
                        }
                        if (dt_item["Package_Item_Deleted_Status"].ToString() == "DELETED")
                        {
                            package_not_deleted = false;
                        }
                    }
                }

                if (dt.Rows[0]["Sold_Out_Status"].ToString() == "Y")
                {
                    btn_sold_out.Attributes.Add("class", "not_available w-100 ");
                }
                else if (dt.Rows[0]["Publish_Status"].ToString() == "Unpublish")
                {
                    btn_not_available.Attributes.Add("class", "not_available w-100 ");
                }
                else if (Convert.ToDecimal(dt.Rows[0]["Available_Qty"].ToString()) < 1)
                {
                    btn_sold_out.Attributes.Add("class", "not_available w-100 ");
                }
                else if (dt.Rows[0]["DeleteInd"].ToString() == "X")
                {
                    btn_not_available.Attributes.Add("class", "not_available w-100 ");
                }
                else if (package_quantity_available == false)
                {
                    btn_not_available.Attributes.Add("class", "not_available w-100 ");
                }
                else if (package_available == false)
                {
                    btn_not_available.Attributes.Add("class", "not_available w-100 ");
                }
                else if (package_publish == false)
                {
                    btn_not_available.Attributes.Add("class", "not_available w-100 ");
                }
                else if (package_not_deleted == false)
                {
                    btn_not_available.Attributes.Add("class", "not_available w-100 ");
                }
                else
                {
                    btn_add_to_cart.Attributes.Add("class", "add-to-cart w-100 ");
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
                        AddItemtoCart(Request.QueryString["id"].ToString(), variation_value);
                        UpdateMasterPage();
                        Load_Product_Image(Request.QueryString["id"].ToString());
                        Load_Product_Details();
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