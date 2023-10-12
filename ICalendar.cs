using System.Data.SQLite;
using System.Globalization;
using System.Runtime.InteropServices.JavaScript;
using Calendar.Seal;

namespace Calendar;

public interface ICalendar
{
    static void Monthly_View()
    {
        var running = true;
        var selectedDay = Program.Date.Day;
        var lookingYear = Program.Date.Year;
        var lookingMonth = Program.Date.Month;
        Tools.Clear();
        PrintCalendar(selectedDay, lookingMonth, lookingYear);
        while (running)
        {
            var k = Tools.Catch();
            if (k.Key == ConsoleKey.DownArrow) selectedDay += 7;
            else if (k.Key == ConsoleKey.UpArrow) selectedDay -= 7;
            else if (k.Key == ConsoleKey.RightArrow) selectedDay++;
            else if (k.Key == ConsoleKey.LeftArrow) selectedDay--;
            if (selectedDay > DateTime.DaysInMonth(lookingYear, lookingMonth))
            {
                if (lookingMonth == 12)
                {
                    lookingMonth = 1;
                    lookingYear++;
                }
                else lookingMonth += 1;
                selectedDay = 1;
            }
            if (selectedDay < 1)
            {
                if (lookingMonth == 1)
                {
                    lookingYear--;
                    lookingMonth = 12;
                }
                else lookingMonth -= 1;
                selectedDay = DateTime.DaysInMonth(lookingYear, lookingMonth);
            }
            Tools.Clear();
            PrintCalendar(selectedDay, lookingMonth, lookingYear);
            if (k.Key == ConsoleKey.Enter) running = false;
        }
        Tools.Clear();
        DailyView(DateTime.Parse($"{selectedDay}/{lookingMonth}/{lookingYear} 00:00:00"));
    }

    private static void PrintCalendar(int selectedDay, int lookingMonth, int lookingYear)
    {
        var daysInMonth = DateTime.DaysInMonth(lookingYear, lookingMonth);
        Tools.SayLine($"\n       < {lookingYear} {CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(lookingMonth)} > \n");
        for (var i = 1; i <= daysInMonth; i++)
        {
            if (i < 10) Tools.Say(" ");
            Tools.Say($"  ");
            if (selectedDay == i) Tools.Flip();
            if (Program.Date.Day == i && Program.Date.Month == lookingMonth && Program.Date.Year == lookingYear && selectedDay != i)
            {
                Tools.Flip(ConsoleColor.Black, ConsoleColor.Red);
                Tools.Say($"{i}");
                Tools.Flip(ConsoleColor.Black, ConsoleColor.White);
            }
            else if (Program.Date.Day == i && Program.Date.Month == lookingMonth && Program.Date.Year == lookingYear && selectedDay == i)
            {
                Tools.Flip();
                Tools.Flip(ConsoleColor.Red, ConsoleColor.Black);
                Tools.Say($"{i}");
                Tools.Flip(ConsoleColor.Black, ConsoleColor.White);
                Tools.Flip();
            }
            else Tools.Say($"{i}");

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
            {
                Tools.Flip(ConsoleColor.Black, ConsoleColor.Red);
                Tools.Say(i == 0 ? "┌" : "├");
                Tools.Flip(ConsoleColor.Black, ConsoleColor.White);
            }
            else Tools.Say(i == 0 ? "┌" : "├");
            for (var j = 0; j < wide; j++)
                if (Program.Date.Hour == i)
                {
                    Tools.Flip(ConsoleColor.Black, ConsoleColor.Red);
                    Tools.Say("─");
                    Tools.Flip(ConsoleColor.Black, ConsoleColor.White);
                }
                else Tools.Say("─");
            if (Program.Date.Hour == i)
            {
                Tools.Flip(ConsoleColor.Black, ConsoleColor.Red);
                Tools.Say("\u253c────██");
                Tools.Flip(ConsoleColor.Black, ConsoleColor.White);
            }
            else Tools.Say(i == 0 ? "┐" : "┤");
            Tools.Say("\n");
            var nowdate = $"\u2502 {currentDate.Day}/{currentDate.Month}/{currentDate.Year} {i} Hs";
            var padding = (wide % 2)+(wide - nowdate.Length);
            if (Program.Date.Hour == i)
            {
                Tools.Flip(ConsoleColor.Black, ConsoleColor.Red);
                Tools.Say(nowdate);
                for (var j = 0; j < padding; j++) Tools.Say(" ");
                Tools.Say("\u2502\n");
                Tools.Flip(ConsoleColor.Black, ConsoleColor.White);
            }
            else
            {
                Tools.Say(nowdate);
                for (var j = 0; j < padding; j++) Tools.Say(" ");
                Tools.Say("\u2502\n");
            }
            using (var conn = new SQLiteConnection(@"Data Source=Calendar.db"))
            {
                using (var cmd = new SQLiteCommand(conn))
                {
                    conn.Open();
                    cmd.CommandText = "select * from Events E join main.Registered R2 on E.EventId = R2.EventId join main.Contacts C on C.Contactd = R2.ContactId";
                    var title = "";
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            var startDate = DateTime.Parse($"{reader["StartDate"]} {reader["StartTime"]}");
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
                            var endDate = DateTime.Parse($"{reader["EndDate"]} {reader["EndTime"]}");
                            var eventInfo =
                                $"\u2502 [{reader["Title"]}, {reader["StartDate"]}, {reader["StartTime"]}, {reader["EndDate"]}, {reader["EndTime"]}, {reader["Place"]}]";
                            var eventPadding = (wide % 2) + (wide - eventInfo.Length);
                            if (startDate > currentDate || endDate < currentDate) continue;
                            Tools.Say(eventInfo, ConsoleColor.Green);
                            for (var j = 0; j < eventPadding; j++) Tools.Say(" ");
                            Tools.Say("\u2502\n");
                        }
                }
                conn.Close();
            }
        }
        Tools.Say("└");
        for (var j = 0; j < wide; j++) Tools.Say("─");
        Tools.Say("┘\n");
        Tools.Catch();
    }
}