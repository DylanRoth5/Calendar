using Calendar.Controllers;
using Calendar.Entities;
using Calendar.Seal;
using System.Data.SQLite;

namespace Calendar;

internal static class Program
{
    public static DateTime Date;
    private static void Main()
    {
        Conexion.OpenConnection();
        Date = Tools.ChooseDate();
        Menu();
        Conexion.CloseConnection();
    }

    private static void Menu()
    {
        while (true)
        {
            string[] options = { "Events", "Contacts", "See Calendar" };
            var result = Tools.Menu("Calendar", options);
            switch (result)
            {
                case 1:
                    IEvent.Menu();
                    continue;
                case 2:
                    IContact.Menu();
                    continue;
                case 3:
                    ICalendar.Monthly_View();
                    continue;
                case 0:
                    break;
            }
            break;
        }
    }
}