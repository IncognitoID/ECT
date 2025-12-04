using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class View_Order_Details_Stockist : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    public enum MessageType { Success, Error, Info, Warning };

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Load_Order_Details(Request.QueryString["id"].ToString());
            Load_Order_Item(Request.QueryString["id"].ToString());
            Load_Payment_Slips(Request.QueryString["id"].ToString());
        }
    }

    protected void Load_Order_Details(string id)
    {
        using (SqlCommand cmd = new SqlCommand("Load_Order_History_Details", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@OrderNo", id);
            con.Open();
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows == true)
            {
                while (idr.Read())
                {
                    lbl_order_no.Text = idr["Order_No"].ToString();
                    lbl_payment_status.Text = idr["Payment_Status"].ToString();
                    lbl_order_quantity.Text = idr["Qty_Total"].ToString();
                    lbl_order_weight.Text = idr["Weight_Total"].ToString() + " (g)";
                    lbl_total_dc_used.Text = Decimal.Truncate(Convert.ToDecimal(idr["Total_DC_Used_Point"].ToString())).ToString();
                    lbl_dc_earn.Text = Decimal.Truncate(Convert.ToDecimal(idr["Total_DC_Point"].ToString())).ToString();
                    lbl_bv_earn.Text = Decimal.Truncate(Convert.ToDecimal(idr["Total_BV_Point"].ToString())).ToString();
                    lbl_subtotal.Text = "RM " + Convert.ToDecimal(idr["Item_Total"].ToString()).ToString("###,###,##0.00");
                    lbl_shipping_fees.Text = "RM " + Convert.ToDecimal(idr["Shipping_Total"].ToString()).ToString("###,###,##0.00");
                    lbl_shipping_discount.Text = "RM " + Convert.ToDecimal(idr["Shipping_Discount"].ToString()).ToString("###,###,##0.00");
                    lbl_total_amount.Text = "RM " + Convert.ToDecimal(idr["Payment_Amt"].ToString()).ToString("###,###,##0.00");
                    lbl_payment_type.Text = idr["Payment_Type"].ToString();
                    lbl_payment_reference.Text = idr["Payment_Reference"].ToString();

                    if (string.IsNullOrEmpty(idr["Remark"].ToString()))
                    {
                        lbl_remark.Text = "-";
                    }
                    else
                    {
                        lbl_remark.Text = idr["Remark"].ToString();
                    }

                    if (idr["Delivery_Service"].ToString() == "Delivery")
                    {
                        lbl_order_status.Text = idr["Order_Status"].ToString();
                        lbl_delivery_service.Text = idr["Delivery_Service"].ToString();
                        div_delivery.Visible = true;
                        div_self_pickup.Visible = false;
                        lbl_delivery_name.Text = idr["Delivery_Name"].ToString();
                        lbl_delivery_contact_no.Text = idr["Delivery_Phone"].ToString();
                        string address1 = idr["Delivery_Address"].ToString();
                        string address2 = idr["Delivery_Address2"].ToString();

                        // Check if address1 ends with a comma
                        if (!address1.EndsWith(","))
                        {
                            address1 += ",";
                        }
                        lbl_delivery_address.Text = address1 + " " + address2;
                        lbl_delivery_postcode.Text = idr["Delivery_Postcode"].ToString();
                        lbl_delivery_city.Text = idr["Delivery_City"].ToString();
                        lbl_delivery_state.Text = idr["Delivery_State_Name"].ToString();
                    }
                    else
                    {
                        if (idr["Order_Status"].ToString() == "To Ship")
                        {
                            lbl_order_status.Text = "Collect at HQ";
                        }
                        else
                        {
                            lbl_order_status.Text = idr["Order_Status"].ToString();
                        }
                        lbl_delivery_service.Text = "Self Pickup at HQ (The Icon)";
                        div_delivery.Visible = false;
                        div_self_pickup.Visible = true;
                        lbl_person_in_charge.Text = idr["Self_Pickup_PIC"].ToString();
                        lbl_person_in_charge_phone_number.Text = idr["Self_Pickup_Phone"].ToString();
                        lbl_pickup_address.Text = idr["Self_Pickup_Address"].ToString();
                        lbl_operation_time.Text = idr["Self_Pickup_Operation_Time"].ToString();
                    }

                }
            }

            idr.Close();
            con.Close();

        }
    }

    protected void Load_Order_Item(string id)
    {
        using (SqlCommand cmd = new SqlCommand("Load_Order_History_Item", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@OrderNo", id);
            con.Open();
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows == true)
            {
                DataTable v = new DataTable();

                v.Load(idr);
                rpt_item.DataSource = v;
                rpt_item.DataBind();
            }

            idr.Close();
            con.Close();

        }
    }

    protected void Load_Payment_Slips(string id)
    {
        using (SqlCommand cmd = new SqlCommand("Load_SO_Header_Img", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@orderno", id);

            con.Open();
            SqlDataAdapter idr = new SqlDataAdapter(cmd);

            DataTable g = new DataTable();
            idr.Fill(g);

            rpt_payment_slip.DataSource = g;
            rpt_payment_slip.DataBind();

            con.Close();
        }
    }

    protected void rpt_item_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        HtmlImage product_img = (HtmlImage)e.Item.FindControl("product_img");
        Label lbl_product_name = (Label)e.Item.FindControl("lbl_product_name");
        Label lbl_product_variation = (Label)e.Item.FindControl("lbl_product_variation");
        Label lbl_normal_qty = (Label)e.Item.FindControl("lbl_normal_qty");
        Label lbl_normal_price = (Label)e.Item.FindControl("lbl_normal_price");
        Label lbl_dc_qty = (Label)e.Item.FindControl("lbl_dc_qty");
        Label lbl_dc_price = (Label)e.Item.FindControl("lbl_dc_price");
        Label lbl_total_price = (Label)e.Item.FindControl("lbl_total_price");

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView drv = (DataRowView)(e.Item.DataItem);

            lbl_product_name.Text = drv.Row["Product_Description"].ToString();

            if (string.IsNullOrEmpty(drv.Row["Product_Variation"].ToString()))
            {
                product_img.Src = "https://ecentra.com.my/Backoffice/" + drv["Product_Image"].ToString();
                lbl_product_variation.Text = "-";
            }
            else
            {
                product_img.Src = "https://ecentra.com.my/Backoffice/" + drv["Variation_Image"].ToString();
                lbl_product_variation.Text = drv.Row["Variation_Name"].ToString();
            }


            if (drv.Row["Qty"].ToString() == "0")
            {
                lbl_normal_qty.Text = "-";
                lbl_normal_price.Text = "-";
            }
            else
            {
                lbl_normal_qty.Text = drv.Row["Qty"].ToString();
                lbl_normal_price.Text = "RM " + (Convert.ToDecimal(drv.Row["Normal_Price"].ToString()) * Convert.ToDecimal(drv.Row["Qty"].ToString())).ToString("###,###,##0.00");
            }

            if (drv.Row["DC_Qty"].ToString() == "0")
            {
                lbl_dc_qty.Text = "-";
                lbl_dc_price.Text = "-";
            }
            else
            {
                lbl_dc_qty.Text = drv.Row["DC_Qty"].ToString();
                lbl_dc_price.Text = drv.Row["Total_DC_Used"].ToString() + " DC  + " + "RM " + (Convert.ToDecimal(drv.Row["DC_Price"].ToString()) * Convert.ToDecimal(drv.Row["DC_Qty"].ToString())).ToString("###,###,##0.00");
            }
            lbl_total_price.Text = "RM " + Convert.ToDecimal(drv.Row["Total_Price"].ToString()).ToString("###,###,###.00");
        }
    }

    protected void btn_upload_slip_img_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFiles)
        {
            string orderid = Request.QueryString["id"].ToString();

            if (string.IsNullOrEmpty(orderid))
            {
                ShowMessage_warning("Error fetching order id", MessageType.Warning);
            }
            else
            {
                // List of allowed extensions
                List<string> allowedExtensions = new List<string> { ".jpg", ".jpeg", ".png" };

                foreach (HttpPostedFile uploadedFile in FileUpload1.PostedFiles)
                {
                    string fileExtension = Path.GetExtension(uploadedFile.FileName).ToLower();

                    // Validate file extension
                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        ShowMessage_warning("Only allow image format with jpg, png and jpeg.", MessageType.Warning);
                        return; // Stop further execution
                    }

                    string imagelink = "";
                    string random_number = "";
                    random_number = RandomString(4);
                    string extension = Path.GetExtension(uploadedFile.FileName);
                    string FileNameU = "payment_slip" + "_" + random_number + "_" + DateTime.UtcNow.AddHours(8).ToString("MMddHHmmss") + extension;
                    string path = Server.MapPath("PaymentSlips/");
                    string filePath = Path.Combine(path, FileNameU);

                    uploadedFile.SaveAs(filePath);
                    imagelink = "PaymentSlips/" + FileNameU;
                    // Insert the image link into the database
                    SaveImageLinkToDatabase(orderid, imagelink);
                }

                // Reload the payment slips to display the newly uploaded image
                Load_Payment_Slips(orderid);
            }
        }
        else
        {
            ShowMessage_warning("Please upload payment slip.", MessageType.Warning);
        }
    }

    private void SaveImageLinkToDatabase(string orderid, string imgLink)
    {
        using (SqlCommand cmd = new SqlCommand("Insert_SO_Header_Img", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Order_No", orderid);
            cmd.Parameters.AddWithValue("@ImgLink", imgLink);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }

    public static string RandomString(int length)
    {
        Random random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
        .Select(s => s[random.Next(s.Length)]).ToArray());
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