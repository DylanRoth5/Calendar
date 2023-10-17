using System.Globalization;
using System.Runtime.InteropServices.JavaScript;
using static Calendar.Seal.Sketch;

namespace Calendar.Seal;

public static class Tools
{
    public static DateTime SetDateTime()
    {
        var Year = DateTime.Now.Year;
        var Month = DateTime.Now.Month;
        var Day = 10;
        var hour = DateTime.Now.Hour;
        var minute = DateTime.Now.Minute;
        var second = 0;
        var running = true;
        var date = $"{Day}/{Month}/{Year} {hour}:{minute}";
        var selected = 1;
        while (running)
        {
            var content = new List<string>() { date };
            var title = "Choose Year";
            Clear();
            Console.CursorVisible = false;

            var menuWidth = CalculateMenuWidth(content, 10);
            TitledBoardApprarence(menuWidth, content.Count);
            Spot(menuWidth / 2 - title.Length / 2, 1);
            Say(title);
            Spot(menuWidth / 2 - date.Length / 2, 3);
            if (selected == 5)
            {
                Flip();
                Say(Day.ToString());
                Flip();
            }
            else
            {
                Say(Day.ToString());
            }

            Say("/");
            if (selected == 4)
            {
                Flip();
                Say(Month.ToString());
                Flip();
            }
            else
            {
                Say(Month.ToString());
            }

            Say("/");
            if (selected == 3)
            {
                Flip();
                Say(Year.ToString());
                Flip();
            }
            else
            {
                Say(Year.ToString());
            }

            Say(" ");
            if (selected == 2)
            {
                Flip();
                Say(hour.ToString());
                Flip();
            }
            else
            {
                Say(hour.ToString());
            }

            Say(":");
            if (selected == 1)
            {
                Flip();
                Say(minute.ToString());
                Flip();
            }
            else
            {
                Say(minute.ToString());
            }

            var k = Catch();
            switch (selected)
            {
                case 5:
                    switch (k.Key)
                    {
                        case ConsoleKey.DownArrow:
                            Day--;
                            break;
                        case ConsoleKey.UpArrow:
                            Day++;
                            break;
                        case ConsoleKey.Enter:
                            selected++;
                            break;
                    }

                    break;
                case 4:
                    switch (k.Key)
                    {
                        case ConsoleKey.DownArrow:
                            Month--;
                            break;
                        case ConsoleKey.UpArrow:
                            Month++;
                            break;
                        case ConsoleKey.Enter:
                            selected++;
                            break;
                    }

                    break;
                case 3:
                    switch (k.Key)
                    {
                        case ConsoleKey.DownArrow:
                            Year--;
                            break;
                        case ConsoleKey.UpArrow:
                            Year++;
                            break;
                        case ConsoleKey.Enter:
                            selected++;
                            break;
                    }

                    break;
                case 2:
                    switch (k.Key)
                    {
                        case ConsoleKey.DownArrow:
                            hour--;
                            break;
                        case ConsoleKey.UpArrow:
                            hour++;
                            break;
                        case ConsoleKey.Enter:
                            selected++;
                            break;
                    }

                    break;
                case 1:
                    switch (k.Key)
                    {
                        case ConsoleKey.DownArrow:
                            minute--;
                            break;
                        case ConsoleKey.UpArrow:
                            minute++;
                            break;
                        case ConsoleKey.Enter:
                            selected++;
                            break;
                    }

                    break;
            }

            if (minute > 59) minute = 0;
            if (minute < 0) minute = 59;
            if (hour > 24) hour = 1;
            if (hour < 1) hour = 23;
            if (Month > 12) Month = 1;
            if (Month < 1) Month = 12;
            if (Day > DateTime.DaysInMonth(Year, Month)) Day = 1;
            if (Day < 1) Day = DateTime.DaysInMonth(Year, Month);
            if (k.Key == ConsoleKey.Enter && selected == 6) running = false;
        }

        Clear();
        Spot(0, 0);
        return DateTime.Parse($"{Day}/{Month}/{Year} {hour}:{minute}:{second}");
    }

    public static DateTime SetDate()
    {
        var Year = DateTime.Now.Year;
        var Month = DateTime.Now.Month;
        var Day = 10;
        var running = true;
        var date = $"{Day}/{Month}/{Year}";
        var selected = 1;
        while (running)
        {
            var content = new List<string>() { date };
            var title = "Choose Year";
            Clear();
            Console.CursorVisible = false;

            var menuWidth = CalculateMenuWidth(content, 10);
            TitledBoardApprarence(menuWidth, content.Count);
            Spot(menuWidth / 2 - title.Length / 2, 1);
            Say(title);
            Spot(menuWidth / 2 - date.Length / 2, 3);
            if (selected == 3)
            {
                Flip();
                Say(Day.ToString());
                Flip();
            }
            else
            {
                Say(Day.ToString());
            }

            Say("/");
            if (selected == 2)
            {
                Flip();
                Say(Month.ToString());
                Flip();
            }
            else
            {
                Say(Month.ToString());
            }

            Say("/");
            if (selected == 1)
            {
                Flip();
                Say(Year.ToString());
                Flip();
            }
            else
            {
                Say(Year.ToString());
            }

            var k = Catch();
            switch (selected)
            {
                case 3:
                    switch (k.Key)
                    {
                        case ConsoleKey.DownArrow:
                            Day--;
                            break;
                        case ConsoleKey.UpArrow:
                            Day++;
                            break;
                        case ConsoleKey.Enter:
                            selected++;
                            break;
                    }

                    break;
                case 2:
                    switch (k.Key)
                    {
                        case ConsoleKey.DownArrow:
                            Month--;
                            break;
                        case ConsoleKey.UpArrow:
                            Month++;
                            break;
                        case ConsoleKey.Enter:
                            selected++;
                            break;
                    }

                    break;
                case 1:
                    switch (k.Key)
                    {
                        case ConsoleKey.DownArrow:
                            Year--;
                            break;
                        case ConsoleKey.UpArrow:
                            Year++;
                            break;
                        case ConsoleKey.Enter:
                            selected++;
                            break;
                    }

                    break;
            }

            if (k.Key == ConsoleKey.Enter && selected == 4) running = false;
        }

        Clear();
        Spot(0, 0);
        return DateTime.Parse($"{Day}/{Month}/{Year} {DateTime.Now:HH:mm:ss}");
    }

    public static string? FileReadAll(string filepath)
    {
        if (!File.Exists(filepath)) return null;
        var file = File.ReadAllText(filepath);
        return file;
    }

    public static DateTime ChooseDate(string? message)
    {
        string[] options = { "Use current time", "Write down the time" };
        message ??= "DateTime Select";
        var selection = Menu(message, options);
        DateTime currentTime = default;
        switch (selection)
        {
            case 1:
                currentTime = DateTime.Now;
                break;
            case 2:
                currentTime = ReadDate("Enter the date and time");
                break;
            case 0:
                break;
        }

        return currentTime;
    }

    public static DateTime ChooseDate()
    {
        string[] options = { "Use current time", "Write down the time" };
        var message = "DateTime Select";
        var selection = Select(message, options);
        DateTime currentTime = default;
        switch (selection)
        {
            case 1:
                currentTime = DateTime.Now;
                break;
            case 2:
                currentTime = SetDateTime();
                break;
        }

        return currentTime;
    }


    public static void EraseLine(int index, string filepath)
    {
        var file = File.ReadAllLines(filepath);
        for (var i = 0; i < file.Length; i++)
        {
            var data = file[i].Split(',');
            if (RecordMatches(index, data, 0)) LineChanger("", filepath, i);
        }
    }

    public static void LineChanger(string newText, string fileName, int lineToEdit)
    {
        var arrLine = File.ReadAllLines(fileName);
        arrLine[lineToEdit - 1] = newText;
        File.WriteAllLines(fileName, arrLine);
    }

    public static string[]? FileRead(int id, string filepath)
    {
        if (File.Exists(filepath))
        {
            var file = File.ReadAllLines(filepath);
            foreach (var t in file)
            {
                var data = t.Split(',');
                if (!RecordMatches(id, data, 0)) continue;
                SayLine("Record Found");
                return data;
            }
        }

        Console.WriteLine("Record not Found");
        return null;
    }

    public static bool Confirm(string message)
    {
        string[] options = { "Do it" };
        return Menu(message, options) == 1;
    }

    public static bool RecordMatches(int id, string[]? record, int position)
    {
        return record != null && record[position].Equals($"{id}");
    }

    public static void FileWrite(string content, string filepath)
    {
        var info = FileReadAll(filepath);
        if (info == null || info.Contains($"{content}")) return;
        var writer = new StreamWriter(filepath, true);
        writer.WriteLine($"{content}");
        writer.Close();
    }

    public static DateTime ReadDate(string message)
    {
        DateTime result;
        var isValid = false;
        do
        {
            Console.WriteLine(message);
            var input = Console.ReadLine();
            if (DateTime.TryParseExact(input, "dd/MM/yyyy HH:mm:ss", null, DateTimeStyles.None, out result))
                isValid = true;
            else
                Console.WriteLine("Format incorrect, try again...");
        } while (!isValid);

        return result;
    }

    public static char ValidateLetter()
    {
        while (true)
        {
            var letter = ' ';
            do
            {
                var consoleKeyInfo = Console.ReadKey(true);
                var ascii = Convert.ToInt32(consoleKeyInfo.KeyChar);
                if (ascii is >= 97 and <= 122 or >= 65 and <= 90) letter = consoleKeyInfo.KeyChar;
            } while (letter == ' ');

            if (letter != ' ') return char.ToLower(letter);
        }
    }

    public static int GetX()
    {
        var x = Console.CursorLeft;
        return x;
    }

    public static int GetY()
    {
        var y = Console.CursorTop;
        return y;
    }

    public static void SayLine(string? word = "")
    {
        Console.WriteLine("" + word);
    }

    public static void Spot(int x, int y)
    {
        Console.SetCursorPosition(x, y);
    }

    public static void Print(string @filepath, List<string>? options)
    {
        var content = "";
        var menuWidth = 0;
        if (options != null)
        {
            foreach (var item in options.Where(item => item.Length >= menuWidth)) menuWidth = item.Length;
            if (menuWidth % 2 != 0) menuWidth++;
            menuWidth += 10;
            var space = " ";
            content += '\u250c';
            for (var i = 0; i < menuWidth; i++) content += '\u2500';

            content += '\u2510';
            content += "\n";
            foreach (var item in options)
            {
                content += '│';
                var itemLenght = item.Length;
                if (itemLenght % 2 != 0) itemLenght++;
                for (var i = 0; i < menuWidth / 2 - itemLenght / 2; i++) content += space;

                content += item;
                for (var i = 0; i < menuWidth / 2 - item.Length / 2; i++) content += space;

                content += "│\n";
            }

            content += '\u2514';
            for (var i = 0; i < menuWidth; i++) content += '\u2500';
        }

        content += '\u2518';
        File.WriteAllText(filepath, content);
    }

    public static void Print(string @filepath, string[] options)
    {
        var content = "";
        var menuWidth = 0;
        foreach (var item in options.Where(item => item.Length >= menuWidth)) menuWidth = item.Length;
        if (menuWidth % 2 != 0) menuWidth++;
        menuWidth += 10;
        var space = " ";
        content += '\u250c';
        for (var i = 0; i < menuWidth; i++) content += '\u2500';
        content += '\u2510';
        content += "\n";
        foreach (var item in options)
        {
            content += '│';
            var itemLenght = item.Length;
            if (itemLenght % 2 != 0) itemLenght++;
            for (var i = 0; i < menuWidth / 2 - itemLenght / 2; i++) content += space;
            content += item;
            for (var i = 0; i < menuWidth / 2 - item.Length / 2; i++) content += space;
            content += "│\n";
        }

        content += '\u2514';
        for (var i = 0; i < menuWidth; i++) content += '\u2500';
        content += '\u2518';
        File.WriteAllText(filepath, content);
    }

    public static void SpotX(int x)
    {
        Console.CursorLeft = x;
    }

    public static void SpotY(int y)
    {
        Console.CursorTop = y;
    }

    public static void Say(string? word = "")
    {
        Console.Write("" + word);
    }

    public static void Say(string? word, ConsoleColor foreground)
    {
        var temp = Console.ForegroundColor;
        Console.ForegroundColor = foreground;
        Say(word);
        Console.ForegroundColor = temp;
    }

    public static void SayLine(string? word, ConsoleColor foreground)
    {
        var temp = Console.ForegroundColor;
        Console.ForegroundColor = foreground;
        SayLine(word);
        Console.ForegroundColor = temp;
    }

    public static string? Read()
    {
        return Console.ReadLine();
    }

    public static void Clear()
    {
        Console.Clear();
    }

    public static ConsoleKeyInfo Catch()
    {
        return Console.ReadKey(true);
    }

    public static void SayAt(int x, int y, string? word)
    {
        Spot(x, y);
        Say(word);
    }

    public static void SayAt(int x, int y, string? word, bool comeback)
    {
        var nx = GetX();
        var ny = GetY();
        Spot(x, y);
        Say(word);
        if (comeback) Spot(nx, ny);
    }

    public static string? Read(string? word)
    {
        if (!string.IsNullOrEmpty(word)) SayLine(word);
        return Read();
    }

    public static int ReadInt()
    {
        return int.Parse(Read() ?? string.Empty);
    }

    public static char ReadChar()
    {
        return ValidateLetter();
    }

    public static char ReadChar(string? word)
    {
        if (!string.IsNullOrEmpty(word)) SayLine(word);
        return ReadChar();
    }

    public static float ReadFloat()
    {
        var culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
        culture.NumberFormat.NumberDecimalSeparator = ".";
        return float.Parse(Read() ?? string.Empty, culture);
    }

    public static bool Judge()
    {
        var answer = Read();
        return answer is "yes" or "si";
    }

    public static bool Judge(string? word)
    {
        if (!string.IsNullOrEmpty(word)) SayLine(word);
        return Judge();
    }

    public static int ReadInt(string? word)
    {
        if (!string.IsNullOrEmpty(word)) SayLine(word);
        return ReadInt();
    }

    public static float ReadFloat(string? word)
    {
        if (!string.IsNullOrEmpty(word)) SayLine(word);
        return ReadFloat();
    }

    public static void CatchClear()
    {
        Catch();
        Clear();
    }

    public static void Flip()
    {
        (Console.BackgroundColor, Console.ForegroundColor) = (Console.ForegroundColor, Console.BackgroundColor);
    }
    public static void Flip(string message, ConsoleColor background, ConsoleColor foreground)
    {
        var back = Console.BackgroundColor;
        var fore =Console.ForegroundColor;
        Flip(background,foreground);
        Say(message);
        Flip(back,fore);
    }

    public static void Flip(ConsoleColor background, ConsoleColor foreground)
    {
        Console.BackgroundColor = background;
        Console.ForegroundColor = foreground;
    }

    public static void Board(List<string> content)
    {
        var menuWidth = CalculateMenuWidth(content, 10);
        BoardApprarence(menuWidth, content.Count);
        var y = 1;
        foreach (var item in content)
        {
            SayAt(menuWidth / 2 - item.Length / 2, y, item);
            y++;
        }
    }

    public static int CalculateMenuWidth(List<string> content, int padding)
    {
        return CalculateMenuWidth(content.ToArray(), padding);
    }

    public static int CalculateMenuWidth(string[] content, int padding)
    {
        var menuWidth = 0;
        foreach (var t in content)
            if (t.Length >= menuWidth)
                menuWidth = t.Length;
        if (menuWidth % 2 != 0)
            menuWidth++;
        menuWidth += padding;
        return menuWidth;
    }

    public static void TitledBoard(string title, List<string>? content)
    {
        Clear();
        Console.CursorVisible = false;

        if (content != null)
        {
            var menuWidth = CalculateMenuWidth(content, 10);
            TitledBoardApprarence(menuWidth, content.Count);
            var x = menuWidth / 2 - title.Length / 2;
            var y = 1;
            Spot(x, y);
            foreach (var t in title)
            {
                Say("" + t);
                x++;
                SpotX(x);
            }

            y++;
            Spot(x, y);
            foreach (var t in content)
            {
                y++;
                x = menuWidth / 2 - t.Length / 2;
                Spot(x, y);
                foreach (var t1 in t)
                {
                    Say("" + t1);
                    x++;
                    SpotX(x);
                }
            }
        }

        Catch();
    }

    public static int Menu(string title, string[] options)
    {
        options = options.Append("Exit").ToArray();
        var result = Select(title, options);
        return result == options.Length ? 0 : result;
    }

    public static int Select(string title, string[] options)
    {
        Clear();
        var background = ConsoleColor.Black;
        var foreground = ConsoleColor.White;
        Console.CursorVisible = false;
        var running = true;
        var result = 1;
        var color = 0;
        var appearance = 0;
        var word = "(a) Appearance";
        Flip(background, foreground);
        var menuWidth = CalculateMenuWidth(options, 20);
        Spot(0, 0);
        while (running)
        {
            foreground = SealPulse[color];
            MenuAppearence(0, 0, menuWidth, options.Length, appearance);
            SketchSeal(menuWidth + 2, 0);
            SayAt(menuWidth / 2 - word.Length / 2, 4 + options.Length, word);
            SayAt(menuWidth / 2 - title.Length / 2, 1, title);
            Spot(0, 2);
            for (var i = 0; i < options.Length; i++)
            {
                Spot(menuWidth / 2 - options[i].Length / 2, GetY() + 1);
                if (result == i + 1)
                {
                    Flip(foreground, background);
                    Say(options[i]);
                    Flip(background, foreground);
                }
                else
                {
                    Say(options[i]);
                }
            }

            Spot(0, 0);
            if (Console.KeyAvailable)
            {
                var k = Catch();
                if (k.Key == ConsoleKey.DownArrow) result++;
                if (k.Key == ConsoleKey.UpArrow) result--;
                if (k.Key == ConsoleKey.Enter) running = false;
                if (k.Key == ConsoleKey.A) appearance++;
            }
            else
            {
                Thread.Sleep(100);
                color++;
            }

            if (result < 1) result = options.Length;
            if (result > options.Length) result = 1;
            if (color >= SealPulse.Length) color = 0;
            if (appearance >= 3) appearance = 0;
        }

        Flip(ConsoleColor.Black, ConsoleColor.White);
        Clear();
        return result;
    }

    public static int Menu(string title, List<string> options)
    {
        return Menu(title, options.ToArray());
    }
}