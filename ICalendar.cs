using System.Globalization;
using Calendar.Seal;

namespace Calendar;

public interface ICalendar
{
    static void Monthly_View()
    {
        var selectedDay = Program.Date.Day;
        var running = true;
        var lookingYear = Program.Date.Year;
        var lookingMonth = Program.Date.Month;
        Tools.Clear();
        PrintCalendar(selectedDay, lookingMonth, lookingYear);
        while (running)
        {
            var k = Tools.Catch();
            if (k.Key == ConsoleKey.DownArrow)
                selectedDay += 7;
            else if (k.Key == ConsoleKey.UpArrow)
                selectedDay -= 7;
            else if (k.Key == ConsoleKey.RightArrow)
                selectedDay++;
            else if (k.Key == ConsoleKey.LeftArrow)
                selectedDay--;
            if (selectedDay > DateTime.DaysInMonth(lookingYear, lookingMonth))
            {
                if (lookingMonth == 12)
                {
                    lookingMonth = 1;
                    lookingYear++;
                }
                else
                {
                    lookingMonth += 1;
                }

                selectedDay = 1;
            }

            if (selectedDay < 1)
            {
                if (lookingMonth == 1)
                {
                    lookingYear--;
                    lookingMonth = 12;
                }
                else
                {
                    lookingMonth -= 1;
                }

                selectedDay = DateTime.DaysInMonth(lookingYear, lookingMonth);
            }

            Tools.Clear();
            PrintCalendar(selectedDay, lookingMonth, lookingYear);
            if (k.Key == ConsoleKey.Enter) running = false;
        }

        Tools.Clear();
        DailyView(DateTime.Parse(
            $"{selectedDay}/{lookingMonth}/{lookingYear} {Program.Date.Hour}:{Program.Date.Minute}:{Program.Date.Hour}"));
    }

    private static void PrintCalendar(int selectedDay, int lookingMonth, int lookingYear)
    {
        var daysInMonth = DateTime.DaysInMonth(lookingYear, lookingMonth);
        Tools.SayLine(
            $"\n       < {lookingYear} {CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(lookingMonth)} > \n");
        for (var i = 1; i <= daysInMonth; i++)
        {
            if (i < 10) Tools.Say(" ");

            Tools.Say($"  ");
            if (selectedDay == i) Tools.Flip();
            if (Program.Date.Day == i && Program.Date.Month == lookingMonth && Program.Date.Year == lookingYear &&
                selectedDay != i)
            {
                Tools.Flip(ConsoleColor.Black, ConsoleColor.Red);
                Tools.Say($"{i}");
                Tools.Flip(ConsoleColor.Black, ConsoleColor.White);
            }
            else if (Program.Date.Day == i && Program.Date.Month == lookingMonth && Program.Date.Year == lookingYear &&
                     selectedDay == i)
            {
                Tools.Flip();
                Tools.Flip(ConsoleColor.Red, ConsoleColor.Black);
                Tools.Say($"{i}");
                Tools.Flip(ConsoleColor.Black, ConsoleColor.White);
                Tools.Flip();
            }
            else
            {
                Tools.Say($"{i}");
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
        for (var i = 0; i < 24; i++)
        {
            if (Program.Date.Hour == i)
            {
                Tools.Flip(ConsoleColor.Black, ConsoleColor.Red);
                Tools.Say(i == 0 ? "┌" : "├");
                Tools.Flip(ConsoleColor.Black, ConsoleColor.White);
            }
            else
            {
                Tools.Say(i == 0 ? "┌" : "├");
            }

            for (var j = 0; j < currentDate.ToString().Length + 20; j++)
                if (Program.Date.Hour == i)
                {
                    Tools.Flip(ConsoleColor.Black, ConsoleColor.Red);
                    Tools.Say("─");
                    Tools.Flip(ConsoleColor.Black, ConsoleColor.White);
                }
                else
                {
                    Tools.Say("─");
                }

            if (Program.Date.Hour == i)
            {
                Tools.Flip(ConsoleColor.Black, ConsoleColor.Red);
                Tools.Say("\u253c────██");
                Tools.Flip(ConsoleColor.Black, ConsoleColor.White);
            }
            else
            {
                Tools.Say(i == 0 ? "┐" : "┤");
            }

            Tools.Say("\n");
            if (Program.Date.Hour == i)
            {
                Tools.Flip(ConsoleColor.Black, ConsoleColor.Red);
                if (i < 10)
                    Tools.SayLine(
                        $"\u2502 {currentDate.Day}/{currentDate.Month}/{currentDate.Year} {i} Hs                       \u2502");
                else
                    Tools.SayLine(
                        $"\u2502 {currentDate.Day}/{currentDate.Month}/{currentDate.Year} {i} Hs                      \u2502");
                Tools.Flip(ConsoleColor.Black, ConsoleColor.White);
            }
            else
            {
                if (i < 10)
                    Tools.SayLine(
                        $"\u2502 {currentDate.Day}/{currentDate.Month}/{currentDate.Year} {i} Hs                       \u2502");
                else
                    Tools.SayLine(
                        $"\u2502 {currentDate.Day}/{currentDate.Month}/{currentDate.Year} {i} Hs                      \u2502");
            }
        }

        Tools.Say("└");
        for (var j = 0; j < currentDate.ToString().Length + 20; j++) Tools.Say("─");
        Tools.Say("┘\n");

        Tools.Catch();
    }
}