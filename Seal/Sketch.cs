using static Calendar.Seal.Tools;

namespace Calendar.Seal;

public class Sketch
{
    public static readonly string[] SealSketch =
    {
        "    ████████",
        "  ██░░░░░░░░██",
        "██░░    ░░  ░░██",
        "██  ████░░██  ██",
        "██  ████░░██  ██",
        "██▒▒░░░░▓▓▓▓░░██",
        "██▒▒▒▒░░░░░░▒▒▒▒██",
        "██▒▒▒▒░░▒▒▒▒▒▒▒▒░░██    ████",
        "  ██░░░░▒▒░░▒▒▒▒░░▒▒██████▒▒████",
        "    ██▒▒░░▒▒░░▒▒░░▒▒░░▒▒▒▒▒▒██▒▒██",
        "    ████░░██▒▒▒▒▒▒░░██░░▒▒▒▒░░██",
        "  ██▒▒▒▒▒▒██▒▒░░░░░░██░░▒▒░░██",
        "  ██████████░░░░░░░░████████",
        "          ██░░░░░░░░██",
        "            ████████"
    };

    public static void SketchSeal()
    {
        foreach (var line in SealSketch) SayLine(line);
    }

    public static void SketchSeal(int x, int y)
    {
        foreach (var line in SealSketch)
        {
            SayAt(x, y, line);
            y++;
        }
    }

    public static readonly ConsoleColor[] SealPulse =
    {
        ConsoleColor.Blue,
        ConsoleColor.Blue,
        ConsoleColor.Blue,
        ConsoleColor.Blue,
        ConsoleColor.Blue,
        ConsoleColor.Blue,
        ConsoleColor.Blue,
        ConsoleColor.Cyan,
        ConsoleColor.Cyan,
        ConsoleColor.Cyan,
        ConsoleColor.Cyan,
        ConsoleColor.Cyan,
        ConsoleColor.Cyan,
        ConsoleColor.Cyan,
        ConsoleColor.White,
        ConsoleColor.White,
        ConsoleColor.White,
        ConsoleColor.White,
        ConsoleColor.White,
        ConsoleColor.White,
        ConsoleColor.White
    };

    public static void TitledBoardApprarence(int menuWidth, int countContent)
    {
        Rect(0, 0, menuWidth, 2, '─', '│', "┌┐├┤");
        Rect(0, 2, menuWidth, countContent + 1, '─', '│', "├┤└┘");
    }

    public static void BoardApprarence(int menuWidth, int countContent)
    {
        Rect(0, 0, menuWidth, countContent + 1, '─', '│', "┌┐└┘");
    }

    public static void MenuAppearence(int x, int y, int menuWidth, int numOfOptions, int appearance)
    {
        if (appearance == 1)
        {
            Rect(x, y, menuWidth, 2, '═', '║', "╔╗╠╣");
            Rect(x, y + 2, menuWidth, numOfOptions + 1, '═', '║', "╠╣╠╣");
            Rect(x, y + 3 + numOfOptions, menuWidth, 2, '═', '║', "╠╣╚╝");
        }
        else if (appearance == 0)
        {
            Rect(x, y, menuWidth, 2, '─', '│', "┌┐├┤");
            Rect(x, y + 2, menuWidth, numOfOptions + 1, '─', '│', "├┤├┤");
            Rect(x, y + 3 + numOfOptions, menuWidth, 2, '─', '│', "├┤└┘");
        }
        else if (appearance == 2)
        {
            Rect(x, y, menuWidth, 2, '█', '█', "████");
            Rect(x, y + 2, menuWidth, numOfOptions + 1, '█', '█', "████");
            Rect(x, y + 3 + numOfOptions, menuWidth, 2, '█', '█', "████");
        }
    }

    public static void Point(int x, int y, char symbol)
    {
        Spot(x, y);
        Say("" + symbol);
    }

    public static void Line(int x, int y, int lenght, bool horizontal, char symbol)
    {
        Spot(x, y);
        //If the orientation is horizontal, the "horizontal" parameter will be true and the line will be drawn
        if (horizontal)
            for (var i = 0; i < lenght; i++)
                Say("" + symbol);
        //If the orientation is vertical, said parameter would be false, therefore the following code would be executed
        if (!horizontal)
            for (var i = 0; i < lenght; i++)
            {
                Say("" + symbol);
                Spot(GetX() - 1, GetY() + 1);
            }
    }

    public static void Rect(int x, int y, int width, int height, char horizontal, char vertical, string corners)
    {
        Line(x + 1, y, width - 1, true, horizontal);
        Line(x + 1, y + height, width - 1, true, horizontal);
        Line(x, y + 1, height - 1, false, vertical);
        Line(x + width, y + 1, height - 1, false, vertical);
        Point(x, y, corners[0]);
        Point(x + width, y, corners[1]);
        Point(x, y + height, corners[2]);
        Point(x + width, y + height, corners[3]);
    }

    //This method draws a grid of cells at a specific location in x and y
    public static void Cell(int x, int y, int width, int height, int rows, int columns)
    {
        for (var i = 0; i < columns; i++)
        for (var j = 0; j < rows; j++)
            Rect(x + i * width, y + j * height, width, height, '█', '█', "████");
    }

    public static void Up(int steps, char? type, char? start, char? end)
    {
        if (start.HasValue)
        {
            Say("" + start);
            steps--;
            Spot(GetX() - 1, GetY() - 1);
        }

        if (end.HasValue) steps--;
        for (var i = 0; i < steps; i++)
        {
            Say("" + type);
            Spot(GetX() - 1, GetY() - 1);
        }

        if (end.HasValue) Say("" + end);
    }

    public static void Down(int steps, char? type, char? start, char? end)
    {
        if (start.HasValue)
        {
            Say("" + start);
            steps--;
            Spot(GetX() - 1, GetY() + 1);
        }

        if (end.HasValue) steps--;
        for (var i = 0; i < steps; i++)
        {
            Say("" + type);
            Spot(GetX() - 1, GetY() + 1);
        }

        if (end.HasValue) Say("" + end);
    }

    public static void Right(int steps, char? type, char? start, char? end)
    {
        if (start.HasValue)
        {
            Say("" + start);
            steps--;
        }

        if (end.HasValue) steps--;
        for (var i = 0; i < steps; i++) Say("" + type);
        if (end.HasValue) Say("" + end);
    }

    public static void Left(int steps, char? type, char? start, char? end)
    {
        if (start.HasValue)
        {
            Console.CursorLeft -= 2;
            Say("" + start);
            steps--;
        }

        if (end.HasValue) steps--;
        for (var i = 0; i < steps; i++)
        {
            Console.CursorLeft -= 2;
            Say("" + type);
        }

        if (end.HasValue) Say("" + end);
    }
}