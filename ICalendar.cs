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
        PrintCalendar(selectedDay,lookingMonth,lookingYear);
        while (running)
        {
            var k = Tools.Catch();
            if (k.Key == ConsoleKey.DownArrow)
                selectedDay+=7;
            else if (k.Key == ConsoleKey.UpArrow)
                selectedDay-=7;
            else if (k.Key == ConsoleKey.RightArrow)
                selectedDay++;
            else if (k.Key == ConsoleKey.LeftArrow)
                selectedDay--;
            if (selectedDay>DateTime.DaysInMonth(lookingYear, lookingMonth))
            {
                lookingMonth += 1;
                selectedDay = 1;
            }else if (selectedDay<1)
            {
                lookingMonth -= 1;
                selectedDay = DateTime.DaysInMonth(lookingYear, lookingMonth);
            }
            if (lookingMonth>12)
            {
                lookingMonth = 1;
                lookingYear += 1;
            }
            Tools.Clear();
            PrintCalendar(selectedDay,lookingMonth,lookingYear);
            if (k.Key == ConsoleKey.Enter) running = false;    
        }
        Tools.Clear();
        DailyView(DateTime.Parse(
            $"{selectedDay}/{lookingMonth}/{lookingYear} {Program.Date.Hour}:{Program.Date.Minute}:{Program.Date.Hour}"));
    }

    private static void PrintCalendar(int selectedDay, int lookingMonth,int lookingYear)
    {
        var daysInMonth = DateTime.DaysInMonth(lookingYear, lookingMonth);
        Tools.SayLine($"\n       < {Program.Date.Year} {CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(lookingMonth)} > \n");
        for (int i = 1; i <= daysInMonth; i++)
        {
            if (i<10)
            {
                Tools.Say(" ");
            }

            Tools.Say($"  ");
            if (selectedDay==i)
            {
                Tools.Flip();
            }
            if (Program.Date.Day==i && Program.Date.Month==lookingMonth && Program.Date.Year == lookingYear && selectedDay!=i)
            {
                Tools.Flip(ConsoleColor.Black,ConsoleColor.Red);
                Tools.Say($"{i}");
                Tools.Flip(ConsoleColor.Black,ConsoleColor.White);
            }else if (Program.Date.Day==i && Program.Date.Month==lookingMonth && Program.Date.Year == lookingYear && selectedDay==i)
            {
                Tools.Flip();
                Tools.Flip(ConsoleColor.Red,ConsoleColor.Black);
                Tools.Say($"{i}");
                Tools.Flip(ConsoleColor.Black,ConsoleColor.White);
                Tools.Flip();
            }else Tools.Say($"{i}");
            if (i%7==0)
            {
                Tools.SayLine("\n");
            }
            if (selectedDay==i)
            {
                Tools.Flip();
            }
        }
    }

    static void DailyView(DateTime currentDate)
    {
        Tools.Clear();
        for (int i = 0; i < 24; i++)
        {
            Tools.Say(i == 0 ? "┌" : "├");
            for (int j = 0; j < (currentDate.ToString().Length); j++)
            {
                Tools.Say("─");
            }
            Tools.Say("\n\u2502 ");
            if (Program.Date.Hour==i)
            {
                Tools.Flip(ConsoleColor.Black,ConsoleColor.Red);
                Tools.SayLine($"{currentDate.Day}/{currentDate.Month}/{currentDate.Year} {i} Hs  ────██");
                Tools.Flip(ConsoleColor.Black,ConsoleColor.White);
            } else Tools.SayLine($"{currentDate.Day}/{currentDate.Month}/{currentDate.Year} {i} Hs");
        }
        Tools.Say("└");
        for (int j = 0; j < (currentDate.ToString().Length); j++)
        {
            Tools.Say("─");
        }
        Tools.Say("\n");

        Tools.Catch();
    }
}