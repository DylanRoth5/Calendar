using System.Data.SQLite;
using System.Globalization;
using Calendar.Seal;

namespace Calendar;

public interface ICalendar
{
    static void Monthly_View()
    {
        var running = true;
        var selectedDay = Program.Date.Day;
        var selectedYear = Program.Date.Year;
        var selectedMonth = Program.Date.Month;
        Tools.Clear();
        PrintCalendar(selectedDay, selectedMonth, selectedYear);
        while (running)
        {
            var k = Tools.Catch();
            if (k.Key == ConsoleKey.DownArrow) selectedDay += 7;
            else if (k.Key == ConsoleKey.UpArrow) selectedDay -= 7;
            else if (k.Key == ConsoleKey.RightArrow) selectedDay++;
            else if (k.Key == ConsoleKey.LeftArrow) selectedDay--;
            if (selectedDay > DateTime.DaysInMonth(selectedYear, selectedMonth))
            {
                if (selectedMonth == 12)
                {
                    selectedMonth = 1;
                    selectedYear++;
                }
                else selectedMonth += 1;
                selectedDay = 1;
            }
            if (selectedDay < 1)
            {
                if (selectedMonth == 1)
                {
                    selectedYear--;
                    selectedMonth = 12;
                }
                else selectedMonth -= 1;
                selectedDay = DateTime.DaysInMonth(selectedYear, selectedMonth);
            }
            Tools.Clear();
            PrintCalendar(selectedDay, selectedMonth, selectedYear);
            if (k.Key == ConsoleKey.Enter) running = false;
        }
        Tools.Clear();
        DailyView(DateTime.Parse($"{selectedDay}/{selectedMonth}/{selectedYear} 00:00:00"));
    }

    private static void PrintCalendar(int selectedDay, int selectedMonth, int selectedYear)
    {
        var daysInMonth = DateTime.DaysInMonth(selectedYear, selectedMonth);
        Tools.SayLine($"\n         < {selectedYear} {CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(selectedMonth)} > \n");

        var eventDates = new List<DateTime>();
        using (var conn = new SQLiteConnection(@"Data Source=Calendar.db"))
        {
            using (var cmd = new SQLiteCommand(conn))
            {
                conn.Open();
                cmd.CommandText = "select * from Events";
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        eventDates.Add(DateTime.Parse($"{reader["StartDate"]} 00:00:00"));
                        eventDates.Add(DateTime.Parse($"{reader["EndDate"]} 00:00:00"));
                    }
                }
            }
            conn.Close();
        }
        
        for (var i = 1; i <= daysInMonth; i++)
        {
            if (i < 10) Tools.Say(" ");
            Tools.Say($"  ");
            if (selectedDay == i) Tools.Flip();
            if (Program.Date.Day == i && Program.Date.Month == selectedMonth && Program.Date.Year == selectedYear &&
                selectedDay != i)
                Tools.Flip($"{i}", ConsoleColor.Black, ConsoleColor.Red);
            else if (Program.Date.Day == i && Program.Date.Month == selectedMonth && Program.Date.Year == selectedYear && selectedDay == i)
            {
                Tools.Flip();
                Tools.Flip($"{i}",ConsoleColor.Red, ConsoleColor.Black);
                Tools.Flip();
            }
            else
            {
                var dateflag = false;
                foreach (var date in eventDates.Where(date => date.Day==i && date.Month==selectedMonth && date.Year==selectedYear))
                    dateflag = true;
                if (dateflag && selectedDay != i)
                    Tools.Flip($"{i}", ConsoleColor.Black, ConsoleColor.Magenta);
                else if (dateflag && selectedDay == i)
                {
                    Tools.Flip();
                    Tools.Flip($"{i}",ConsoleColor.Magenta, ConsoleColor.Black);
                    Tools.Flip();
                }
                else Tools.Say($"{i}");
            }
            if (i % 7 == 0) Tools.SayLine("\n");
            if (selectedDay == i) Tools.Flip();
        }
        Sketch.Rect(0, 0, 29, 2, '─', '│', "┌┐├┤");
        Sketch.Rect(0, 2, 29, 10, '─', '│', "├┤└┘");
    }

    static void DailyView(DateTime currentDate)
    {
        Tools.Clear();
        var wide = 100 - currentDate.ToString().Length;
        for (var i = 0; i < 24; i++)
        {
            currentDate = DateTime.Parse($"{currentDate.Day}/{currentDate.Month}/{currentDate.Year} {i}:00:00");
            if (Program.Date.Hour == i)
                Tools.Flip(i == 0 ? "┌" : "├", ConsoleColor.Black, ConsoleColor.Red);
            else Tools.Say(i == 0 ? "┌" : "├");
            for (var j = 0; j < wide; j++)
                if (Program.Date.Hour == i)
                    Tools.Flip("─", ConsoleColor.Black, ConsoleColor.Red);
                else Tools.Say("─");
            if (Program.Date.Hour == i)
                Tools.Flip("\u253c────██", ConsoleColor.Black, ConsoleColor.Red);
            else Tools.Say(i == 0 ? "┐" : "┤");
            Tools.Say("\n");
            var nowdate = $"\u2502 {currentDate.Day}/{currentDate.Month}/{currentDate.Year} {i} Hs";
            var padding = (wide % 2)+(wide - nowdate.Length);
            for (var j = 0; j < padding; j++) nowdate+=" ";
            nowdate += "\u2502\n";
            if (Program.Date.Hour == i) Tools.Flip(nowdate, ConsoleColor.Black, ConsoleColor.Red);
            else Tools.Say(nowdate);

            using var conn = new SQLiteConnection(@"Data Source=Calendar.db");
            using (var cmd = new SQLiteCommand(conn))
            {
                conn.Open();
                cmd.CommandText = "select * from Events E join main.Registered R2 on E.EventId = R2.EventId join main.Contacts C on C.Contactd = R2.ContactId";
                var title = "";
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        var startDate = DateTime.Parse($"{reader["StartDate"]} {reader["StartTime"]}");
                        startDate = DateTime.Parse($"{reader["StartDate"]} {startDate.Hour}:00:00");
                        var endDate = DateTime.Parse($"{reader["EndDate"]} {reader["EndTime"]}");
                        if (startDate <= currentDate && endDate >= currentDate)
                        {
                            var eventInfo =
                                $"\u2502 [{reader["Title"]}, {reader["StartDate"]}, {reader["StartTime"]}, {reader["EndDate"]}, {reader["EndTime"]}, {reader["Place"]}]";
                            var contactInfo =
                                $"\u2502   \u2192 [{reader["Name"]}, {reader["LastName"]}, {reader["Phone"]}, {reader["Email"]}]";
                            var contactPadding = (wide % 2) + (wide - contactInfo.Length);
                            var eventPadding = (wide % 2) + (wide - eventInfo.Length);
                            if (title.Equals(reader["Title"].ToString()))
                            {
                                Tools.Say(contactInfo, ConsoleColor.DarkGreen);
                                for (var j = 0; j < contactPadding; j++) Tools.Say(" ");
                                Tools.Say("\u2502\n");
                            }
                            else
                            {
                                Tools.Say(eventInfo, ConsoleColor.Green);
                                for (var j = 0; j < eventPadding; j++) Tools.Say(" ");
                                Tools.Say("\u2502\n");
                                Tools.Say(contactInfo, ConsoleColor.DarkGreen);
                                for (var j = 0; j < contactPadding; j++) Tools.Say(" ");
                                Tools.Say("\u2502\n");
                            }
                        }
                        title = reader["Title"].ToString();
                    }
                cmd.CommandText = "select * from Events E where E.EventId NOT IN (SELECT R.EventId FROM Registered R) ";
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        var startDate = DateTime.Parse($"{reader["StartDate"]} {reader["StartTime"]}");
                        startDate = DateTime.Parse($"{reader["StartDate"]} {startDate.Hour}:00:00");
                        var endDate = DateTime.Parse($"{reader["EndDate"]} {reader["EndTime"]}");
                        var eventInfo = $"\u2502 [{reader["Title"]}, {reader["StartDate"]}, {reader["StartTime"]}, {reader["EndDate"]}, {reader["EndTime"]}, {reader["Place"]}]";
                        var eventPadding = (wide % 2) + (wide - eventInfo.Length);
                        if (startDate > currentDate || endDate < currentDate) continue;
                        Tools.Say(eventInfo, ConsoleColor.Green);
                        for (var j = 0; j < eventPadding; j++) Tools.Say(" ");
                        Tools.Say("\u2502\n");
                    }
            }
            conn.Close();
        }
        Tools.Say("└");
        for (var j = 0; j < wide; j++) Tools.Say("─");
        Tools.Say("┘\n");
        Tools.Catch();
    }
}