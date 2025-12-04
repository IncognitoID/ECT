using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;

public partial class Event : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Read month and year from query string
            int month = DateTime.Now.Month - 1; // Default to current month (zero-based index)
            int year = DateTime.Now.Year; // Default to current year

            // Read month from query string
            if (Request.QueryString["month"] != null)
            {
                int tempMonth;
                if (int.TryParse(Request.QueryString["month"], out tempMonth))
                {
                    // Check if month is valid (0-based index)
                    if (tempMonth >= 0 && tempMonth < 12)
                    {
                        month = tempMonth;
                    }
                }
            }

            // Read year from query string
            if (Request.QueryString["year"] != null)
            {
                int tempYear;
                if (int.TryParse(Request.QueryString["year"], out tempYear))
                {
                    // Check if year is valid (positive value)
                    if (tempYear > 0)
                    {
                        year = tempYear;
                    }
                }
            }

            // Generate calendar HTML and load events
            string calendarHtml = GenerateCalendarHtml(year, month);
            calendarContainer.InnerHtml = calendarHtml; // Assuming you have a container to hold the calendar
        }
    }

    private string GenerateCalendarHtml(int year, int month)
    {
        // Ensure month is in range
        if (month < 1)
        {
            month = (month % 11 + 11) % 11; // This wraps around to 12 (December)
            year--; // Decrease year because month is in the previous year
        }
        else if (month > 11)
        {
            month = (month - 1) % 11 + 1; // This wraps around to January
            year++; // Increase year because month is in the next year
        }

        // Ensure year is positive
        if (year <= 0)
        {
            throw new ArgumentOutOfRangeException("year", "Year must be positive.");
        }

        var events = FetchEvents(year, month + 1); // Fetch events for the given month

        var months = new[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

        // Check for valid month index
        if (month < 0 || month >= months.Length)
        {
            month = DateTime.Now.Month - 1; // Fallback to current month
        }

        DateTime firstDayOfMonth = new DateTime(year, month + 1, 1);
        int dayOfWeek = (int)firstDayOfMonth.DayOfWeek;
        int lastDate = DateTime.DaysInMonth(year, month + 1);

        // Adjust dayOfWeek to start from Monday (1) instead of Sunday (0)
        dayOfWeek = (dayOfWeek + 6) % 7; // This converts Sunday=0 to Monday=0

        StringBuilder calendarHtml = new StringBuilder();

        // Add previous month days
        for (int i = 0; i < dayOfWeek; i++)
        {
            calendarHtml.AppendFormat("<li class='inactive'>{0}</li>", (DateTime.DaysInMonth(year, month) - dayOfWeek + i + 1));
        }

        // Add current month days
        for (int i = 1; i <= lastDate; i++)
        {
            string isToday = (i == DateTime.Now.Day && month == DateTime.Now.Month - 1 && year == DateTime.Now.Year) ? "active" : "";
            calendarHtml.AppendFormat("<li class='{0}'>{1}", isToday, i);
            var eventOnDate = events.Find(e =>
                e.PubStartDT.HasValue &&
                e.PubStartDT.Value.Day <= i && // Event starts on or before this date
                e.PubEndDT.HasValue &&
                e.PubEndDT.Value.Day >= i &&   // Event ends on or after this date
                e.PubStartDT.Value.Month == month + 1 &&
                e.PubEndDT.Value.Month == month + 1);

            if (eventOnDate != null)
            {
                string buttonColor;

                if (eventOnDate.ColID == "1")
                {
                    buttonColor = eventOnDate.Colour;
                }
                else if (eventOnDate.ColID == "2")
                {
                    buttonColor = eventOnDate.Colour;
                }
                else
                {
                    buttonColor = eventOnDate.Colour;
                }

                calendarHtml.AppendFormat("<div class='button-container'><div class='eventbtn' style='background-color:{0};'><a class='calendar-button' href='Event_Details.aspx?Ids={2}'>{1}</a></div></div>", buttonColor, eventOnDate.Title, eventOnDate.Ids);
            }
            else
            {
                calendarHtml.Append("<div class='button-container'><div class='eventbtn' style='display:none;'><button class='calendar-button'></button></div></div>");
            }
            calendarHtml.Append("</li>");
        }

        // Add next month days
        int lastDayOfWeek = (int)new DateTime(year, month + 1, lastDate).DayOfWeek;
        lastDayOfWeek = (lastDayOfWeek + 6) % 7; // Adjust to match Monday start

        for (int i = lastDayOfWeek + 1; i < 7; i++)
        {
            calendarHtml.AppendFormat("<li class='inactive'>{0}</li>", (i - lastDayOfWeek));
        }

        return string.Format(@"
            <header class='calendar-header'>
                <div class='calendar-month-text'>
                    <div class='calendar-currmonth-cotainer'>
                        <p class='calendar-current-month' data-month='{0}'>{1}</p>
                    </div>
                    <div class='calendar-curryear-cotainer'>
                        <p class='calendar-current-year' data-year='{2}'>{3}</p>
                    </div>
                </div>
            </header>
            <div class='calendar-body'>
                <ul class='calendar-weekdays'>
                    <li>MON</li>
                    <li>TUE</li>
                    <li>WED</li>
                    <li>THU</li>
                    <li>FRI</li>
                    <li>SAT</li>
                    <li style='background: crimson;'>SUN</li>
                </ul>
                <ul class='calendar-dates'>
                    {4}
                </ul>
            </div>",
        month, months[month], year, year, calendarHtml.ToString()
        );
    }

    private List<EventData> FetchEvents(int year, int month)
    {
        var events = new List<EventData>();

        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = @"
                SELECT Ids, Title, Dsc, ColID, Colour, PubStartDT, PubEndDT
                FROM dbo.Event
                WHERE YEAR(PubStartDT) = @Year AND MONTH(PubStartDT) = @Month
                AND DeleteInd <> 'X'";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Year", year);
                cmd.Parameters.AddWithValue("@Month", month);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        events.Add(new EventData
                        {
                            Ids = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                            Title = reader.IsDBNull(1) ? null : reader.GetString(1),
                            Dsc = reader.IsDBNull(2) ? null : reader.GetString(2),
                            ColID = reader.IsDBNull(3) ? null : reader.GetString(3),
                            Colour = reader.IsDBNull(4) ? null : reader.GetString(4),
                            PubStartDT = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5),
                            PubEndDT = reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6),
                        });
                    }
                }
            }
        }

        return events;
    }
}

public class EventData
{
    public int Ids { get; set; }
    public string Title { get; set; }
    public string Dsc { get; set; }
    public string ColID { get; set; }
    public string Colour { get; set; }
    public DateTime? PubStartDT { get; set; } // Nullable DateTime
    public DateTime? PubEndDT { get; set; } // Nullable DateTime
}

