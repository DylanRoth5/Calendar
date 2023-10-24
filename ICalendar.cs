using System.Data.SQLite;
using System.Globalization;
using Calendar.Conections;
using Calendar.Controllers;
using Calendar.Entities;
using static Calendar.Seal.Sketch;
using static Calendar.Seal.Tools;

namespace Calendar;

public interface ICalendar
{
    static void MonthlyView()
    {
        var running = true;
        var selectedDay = Program.Date.Day;
        var selectedYear = Program.Date.Year;
        var selectedMonth = Program.Date.Month;
        while (running)
        {
            Clear();
            PrintCalendar(selectedDay, selectedMonth, selectedYear,
                DateTime.Parse($"{selectedDay}/{selectedMonth}/{selectedYear} 00:00:00"));
            Rect(0, 0, 23, 2, '─', '│', "┌┐├┤");
            Rect(0, 2, 23, 6, '─', '│', "├┤└┘");
            SayLine();
            SayLine("  [+] Add Event (space)");
            ShowEvents(DateTime.Parse($"{selectedDay}/{selectedMonth}/{selectedYear} 00:00:00"));
            var k = Catch();
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
                else
                {
                    selectedMonth += 1;
                }

                selectedDay = 1;
            }

            if (selectedDay < 1)
            {
                if (selectedMonth == 1)
                {
                    selectedYear--;
                    selectedMonth = 12;
                }
                else
                {
                    selectedMonth -= 1;
                }

                selectedDay = DateTime.DaysInMonth(selectedYear, selectedMonth);
            }

            if (k.Key == ConsoleKey.Enter) running = false;
            if (k.Key != ConsoleKey.Spacebar) continue;
            Clear();
            IEvent.Create();
        }

        Clear();
        DailyView(DateTime.Parse($"{selectedDay}/{selectedMonth}/{selectedYear} 00:00:00"));
    }

    static void ShowEvents(DateTime currentDate)
    {
        foreach (var event_ in pEvent.getAll().Where(event_ => event_.Start.Year <= currentDate.Year &&
                                                                   event_.Start.Month <= currentDate.Month &&
                                                                   event_.Start.Day <= currentDate.Day &&
                                                                   event_.End.Year >= currentDate.Year &&
                                                                   event_.End.Month >= currentDate.Month &&
                                                                   event_.End.Day >= currentDate.Day))
        {
            SayLine($"│ {event_}", ConsoleColor.Magenta);
            foreach (var contact in from relation in pRelation.getAll()
                     where relation.EventId == event_.Id
                     from contact in pContact.getAll()
                     where contact.Id == relation.ContactId
                     select contact)
                SayLine($"│    -> {contact}", ConsoleColor.Green);
        }
    }

    private static void PrintCalendar(int selectedDay, int selectedMonth, int selectedYear, DateTime currentDate)
    {
        var daysInMonth = DateTime.DaysInMonth(selectedYear, selectedMonth);
        SayAt(5, 1,
            $"< {selectedYear} {CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(selectedMonth)} >\n\n");

        for (var i = 1; i <= daysInMonth; i++)
        {
            var printedDate = DateTime.Parse($"{i}/{selectedMonth}/{selectedYear} 00:00:00");
            if ((i - 1) % 7 == 0)
                Say("\u2502 ");
            else Say($" ");
            if (i < 10) Say(" ");
            if (selectedDay == i) Flip();
            if (Program.Date.Day == i && Program.Date.Month == selectedMonth && Program.Date.Year == selectedYear &&
                selectedDay != i)
            {
                Flip($"{i}", ConsoleColor.Black, ConsoleColor.Red);
            }
            else if (Program.Date.Day == i && Program.Date.Month == selectedMonth &&
                     Program.Date.Year == selectedYear && selectedDay == i)
            {
                Flip();
                Flip($"{i}", ConsoleColor.Red, ConsoleColor.Black);
                Flip();
            }
            else
            {
                var dateflag = false;
                foreach (var evnt in pEvent.getAll().Where(evnt =>
                             evnt.Start.Date == printedDate.Date && evnt.End.Date == printedDate.Date))
                    dateflag = true;
                if (dateflag && selectedDay != i)
                {
                    Flip($"{i}", ConsoleColor.Black, ConsoleColor.Magenta); 
                }
                else if (dateflag && selectedDay == i)
                {
                    Flip($"{i}", ConsoleColor.Magenta, ConsoleColor.Black);
                }
                else
                {
                    Say($"{i}");
                }
            }

            if (i % 7 == 0) SayLine();
            if (selectedDay == i) Flip();
        }

        SayLine("");
    }


    static void DailyView(DateTime currentDate)
    {
        Clear();
        var wide = 100 - currentDate.ToString().Length;
        for (var i = 0; i < 24; i++)
        {
            currentDate = DateTime.Parse($"{currentDate.Day}/{currentDate.Month}/{currentDate.Year} {i}:00:00");
            if (Program.Date.Hour == i)
                Flip(i == 0 ? "┌" : "├", ConsoleColor.Black, ConsoleColor.Red);
            else Say(i == 0 ? "┌" : "├");
            for (var j = 0; j < wide; j++)
                if (Program.Date.Hour == i)
                    Flip("─", ConsoleColor.Black, ConsoleColor.Red);
                else Say("─");
            if (Program.Date.Hour == i)
                Flip("\u253c────██", ConsoleColor.Black, ConsoleColor.Red);
            else Say(i == 0 ? "┐" : "┤");
            Say("\n");
            var nowdate = $"\u2502 {currentDate.Day}/{currentDate.Month}/{currentDate.Year} {i} Hs";
            var padding = wide % 2 + (wide - nowdate.Length);
            for (var j = 0; j < padding; j++) nowdate += " ";
            nowdate += "\u2502\n";
            if (Program.Date.Hour == i) Flip(nowdate, ConsoleColor.Black, ConsoleColor.Red);
            else Say(nowdate);

            foreach (var evnt in from evnt in pEvent.getAll() let startEvent = DateTime.Parse($"{evnt.Start.Day}/{evnt.Start.Month}/{evnt.Start.Year} {evnt.Start.Hour}:00:00") let endEvent = DateTime.Parse($"{evnt.End.Day}/{evnt.End.Month}/{evnt.End.Year} {evnt.End.Hour}:00:00") where startEvent<=currentDate && endEvent>=currentDate select evnt)
            {
                SayLine($"\u2502 {evnt}",ConsoleColor.Yellow);
                foreach (var contact in pRelation.getAll().Where(relation => relation.EventId==evnt.Id).SelectMany(relation => pContact.getAll().Where(contact => contact.Id==relation.ContactId)))
                {
                    SayLine($"\u2502  -> {contact}",ConsoleColor.Green);
                }
            }
        }

        Say("└");
        for (var j = 0; j < wide; j++) Say("─");
        Say("┘\n");
        Catch();
    }
}