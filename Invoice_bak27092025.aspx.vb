Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Web.Services
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.tool.xml
Imports Newtonsoft.Json

Partial Class Invoice
    Inherits System.Web.UI.Page
    Shared DBCon As String

    Protected Sub form1_Load(sender As Object, e As System.EventArgs) Handles form1.Load
        DBCon = System.Configuration.ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
        Dim orderno As String = ""
        If Not Request.QueryString("id") Is Nothing Then
            orderno = Request.QueryString("id")
        End If

        Using con As SqlConnection = New SqlConnection(DBCon)
            Dim cmd As SqlCommand = New SqlCommand("Backoffice_Load_Order_Details", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@Order_No", orderno)

            Dim dt As DataTable = New DataTable()
            Using adapter As New SqlDataAdapter(cmd)
                adapter.Fill(dt)
            End Using

            If dt.Rows.Count > 0 Then

                lbl_member_id.Text = dt.Rows(0)("MemberID")
                If (String.IsNullOrEmpty(dt.Rows(0)("Member_Name"))) Then
                    lbl_member_name.Text = dt.Rows(0)("Company_Name")
                Else
                    lbl_member_name.Text = dt.Rows(0)("Member_Name")
                End If

                lbl_order_no.Text = dt.Rows(0)("Order_No")
                lbl_order_date.Text = Convert.ToDateTime(dt.Rows(0)("Order_Date")).ToString("dd-MM-yyyy hh:mm:ss tt")
                lbl_order_handle_by.Text = dt.Rows(0)("Order_Handle_By")

                If dt.Rows(0)("Payment_Reference") <> "" Then
                    lbl_payment_reference.Text = dt.Rows(0)("Payment_Reference")
                Else
                    lbl_payment_reference.Text = "-"
                End If
                If dt.Rows(0)("Remark") <> "" Then
                    lbl_payment_remark.Text = dt.Rows(0)("Remark")
                Else
                    lbl_payment_remark.Text = "-"
                End If

                If dt.Rows(0)("Delivery_Service") = "Delivery" Then
                    lbl_receiver_name.Text = dt.Rows(0)("Delivery_Name")
                    lbl_receiver_contact.Text = dt.Rows(0)("Delivery_Phone")
                    lbl_shipping_method.Text = dt.Rows(0)("Delivery_Service")

                    Dim address1 As String = dt.Rows(0)("Delivery_Address").ToString()
                    Dim address2 As String = dt.Rows(0)("Delivery_Address2").ToString()

                    If dt.Rows(0)("Delivery_Service") = "Delivery" Then
                        If Not address1.EndsWith(",") Then
                            address1 += ","
                        End If
                        lbl_shipping_address.Text = address1 + " " + address2 + ", " + dt.Rows(0)("Delivery_Postcode").ToString() + ", " + dt.Rows(0)("Delivery_City").ToString() + ", " + dt.Rows(0)("State_Name").ToString()
                    Else
                        lbl_shipping_address.Text = address1
                    End If
                Else
                    lbl_title_1.InnerText = "Self Pickup Member Name"
                    lbl_title_2.InnerText = "Self Pickup Address"
                    lbl_title_3.InnerText = "Self Pickup Member Contact No."
                    lbl_receiver_name.Text = dt.Rows(0)("Member_Name")
                    lbl_receiver_contact.Text = dt.Rows(0)("Member_Contact")
                    lbl_shipping_method.Text = dt.Rows(0)("Delivery_Service")

                    Dim address1 As String = dt.Rows(0)("Delivery_Address").ToString()
                    Dim address2 As String = dt.Rows(0)("Delivery_Address2").ToString()

                    If dt.Rows(0)("Delivery_Service") = "Delivery" Then
                        If Not address1.EndsWith(",") Then
                            address1 += ","
                        End If
                        lbl_shipping_address.Text = address1 + " " + address2 + ", " + dt.Rows(0)("Delivery_Postcode").ToString() + ", " + dt.Rows(0)("Delivery_City").ToString() + ", " + dt.Rows(0)("State_Name").ToString()
                    Else
                        lbl_shipping_address.Text = address1
                    End If
                End If

                lbl_total_bv.Text = Convert.ToDecimal(dt.Rows(0)("Total_BV_Point")).ToString("N")
                lbl_total_dc_used.Text = Convert.ToDecimal(dt.Rows(0)("Total_DC_Used_Point")).ToString("N")
                lbl_total_dc_earn.Text = Convert.ToDecimal(dt.Rows(0)("Total_DC_Point")).ToString("N")
                lbl_total_rp_used.Text = Convert.ToDecimal(dt.Rows(0)("Total_RP_Used_Point")).ToString("N")
                lbl_total_rp_earn.Text = Convert.ToDecimal(dt.Rows(0)("Total_RP_Earn_Point")).ToString("N")
                lbl_payment_type.Text = dt.Rows(0)("Payment_Type")
                lbl_subtotal.Text = "RM " + Convert.ToDecimal(dt.Rows(0)("Item_Total")).ToString("N")
                lbl_shipping_fees.Text = "RM " + Convert.ToDecimal(dt.Rows(0)("Shipping_Total")).ToString("N")
                lbl_shipping_discount.Text = "RM " + Convert.ToDecimal(dt.Rows(0)("Shipping_Discount")).ToString("N")
                lbl_total.Text = "RM " + Convert.ToDecimal(dt.Rows(0)("Payment_Amt")).ToString("N")

                If dt.Rows(0)("Order_Status") = "To Pay" Then
                    btnsave.Visible = False
                ElseIf dt.Rows(0)("Order_Status") = "Cancelled" Then
                    btnsave.Visible = False
                Else
                    btnsave.Visible = True
                End If
            End If
        End Using

        Using con As SqlConnection = New SqlConnection(DBCon)
            Dim cmd As SqlCommand = New SqlCommand("Backoffice_Load_Order_Item", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@Order_No", orderno)

            Dim dt As DataTable = New DataTable()
            Using adapter As New SqlDataAdapter(cmd)
                adapter.Fill(dt)
            End Using

            If dt.Rows.Count > 0 Then
                Response.Write("<script>;dt = '" & ConvertDataTableToHTMLMobile(dt) & "';</script>")
            End If
        End Using
    End Sub

    Public Shared Function SelectDBDT(selectcmd As String, connetionString As String) As DataTable
        Dim cnn As SqlConnection
        Dim dt As New DataTable
        cnn = New SqlConnection(connetionString)
        Try
            cnn.Open()
            Dim myCommand As New SqlCommand(selectcmd, cnn)
            dt.Load(myCommand.ExecuteReader())
            cnn.Close()
        Catch ex As Exception
        End Try
        Return dt
    End Function

    Public Shared Function ConvertDataTableToHTMLMobile(dt As DataTable) As String
        Dim html As String = ""
        'add header row
        html += "<tr >"

        For i As Integer = 0 To dt.Columns.Count - 1

            html += "<th style=""border-top: 1px solid black !important;border-bottom: 1px solid black !important"">" + dt.Columns(i).ColumnName.Replace(" 12:00:00 AM", "") + "</th>"
        Next

        Dim dtclmncount As Integer = dt.Columns.Count

        html += "</tr>"
        html += "</thead>"

        html += "<tbody>"
        'add rows

        For i As Integer = 0 To dt.Rows.Count - 1
            For j As Integer = 0 To dt.Columns.Count - 1
                ' 
                If dt.Rows(i)(j).ToString().TrimEnd = "To Pay" Then
                    html += "<td><span class=""label label-warning"">" + dt.Rows(i)(j).ToString().TrimEnd + "</span></td>"
                ElseIf dt.Rows(i)(j).ToString().TrimEnd = "To Ship" Then
                    html += "<td><span class=""label label-info"">" + dt.Rows(i)(j).ToString().TrimEnd + "</span></td>"
                ElseIf dt.Rows(i)(j).ToString().TrimEnd = "To Receive" Then
                    html += "<td><span class=""label label-warning"">" + dt.Rows(i)(j).ToString().TrimEnd + "</span></td>"
                ElseIf dt.Rows(i)(j).ToString().TrimEnd = "Completed" Then
                    html += "<td><span class=""label label-success"">" + dt.Rows(i)(j).ToString().TrimEnd + "</span></td>"
                Else
                    html += "<td >" + dt.Rows(i)(j).ToString().TrimEnd + "</td>"
                End If

            Next

            html += "</tr>"

        Next

        html += "</tbody>"
        html += "</table>"

        Return html
    End Function

    Public Function AmountInWords(ByVal nAmount As String, Optional ByVal wAmount _
                 As String = vbNullString, Optional ByVal nSet As Object = Nothing) As String
        Dim currency = " ringgit"
        'Let's make sure entered value is numeric
        If Not IsNumeric(nAmount) Then Return "Please enter numeric values only."

        Dim tempDecValue As String = String.Empty : If InStr(nAmount, ".") Then _
        tempDecValue = nAmount.Substring(nAmount.IndexOf("."))
        nAmount = Replace(nAmount, tempDecValue, String.Empty)

        Try
            Dim intAmount As Long = nAmount
            If intAmount > 0 Then
                nSet = IIf((intAmount.ToString.Trim.Length / 3) _
                > (CLng(intAmount.ToString.Trim.Length / 3)),
              CLng(intAmount.ToString.Trim.Length / 3) + 1,
                  CLng(intAmount.ToString.Trim.Length / 3))
                Dim eAmount As Long = Microsoft.VisualBasic.Left(intAmount.ToString.Trim,
              (intAmount.ToString.Trim.Length - ((nSet - 1) * 3)))
                Dim multiplier As Long = 10 ^ (((nSet - 1) * 3))

                Dim Ones() As String =
            {"", "One", "Two", "Three",
              "Four", "Five",
              "Six", "Seven", "Eight", "Nine"}
                Dim Teens() As String = {"",
            "Eleven", "Twelve", "Thirteen",
              "Fourteen", "Fifteen",
              "Sixteen", "Seventeen", "Eighteen", "Nineteen"}
                Dim Tens() As String = {"", "Ten",
            "Twenty", "Thirty",
              "Forty", "Fifty", "Sixty",
              "Seventy", "Eighty", "Ninety"}
                Dim HMBT() As String = {"", "",
            "Thousand", "Million",
              "Billion", "Trillion",
              "Quadrillion", "Quintillion"}

                intAmount = eAmount

                Dim nHundred As Integer = intAmount \ 100 : intAmount = intAmount Mod 100
                Dim nTen As Integer = intAmount \ 10 : intAmount = intAmount Mod 10
                Dim nOne As Integer = intAmount \ 1

                If nHundred > 0 Then wAmount = wAmount &
            Ones(nHundred) & " Hundred " 'This is for hundreds                
                If nTen > 0 Then 'This is for tens and teens
                    If nTen = 1 And nOne > 0 Then 'This is for teens 
                        wAmount = wAmount & Teens(nOne) & " "
                    Else 'This is for tens, 10 to 90
                        wAmount = wAmount & Tens(nTen) & IIf(nOne > 0, " ", " ")
                        If nOne > 0 Then wAmount = wAmount & Ones(nOne) & " "
                    End If
                Else 'This is for ones, 1 to 9
                    If nOne > 0 Then wAmount = wAmount & Ones(nOne) & " "
                End If
                wAmount = wAmount & HMBT(nSet) & " "
                wAmount = AmountInWords(CStr(CLng(nAmount) -
              (eAmount * multiplier)).Trim & tempDecValue, wAmount, nSet - 1)
            Else
                If Val(nAmount) = 0 Then nAmount = nAmount &
            tempDecValue : tempDecValue = String.Empty
                If (Math.Round(Val(nAmount), 2) * 100) > 0 Then wAmount =
              Trim(AmountInWords(CStr(Math.Round(Val(nAmount), 2) * 100),
              wAmount.Trim & currency & " And ", 1)) & " Cents"
            End If
        Catch ex As Exception
            ' MsgBox("Error Encountered: " & ex.Message)
            Return "!#ERROR_ENCOUNTERED"
        End Try

        'Trap null values
        If IsNothing(wAmount) = True Then wAmount = String.Empty Else wAmount =
      IIf(InStr(wAmount.Trim.ToLower, currency),
      wAmount.Trim, wAmount.Trim & " " & currency)

        'Display the result
        Return UCase(wAmount)
    End Function

End Class
