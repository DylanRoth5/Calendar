using Calendar.Controllers;
using Calendar.Entities;
using Calendar.Seal;
using System.Data.SQLite;
using Calendar.Conections;

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
                    ICalendar.MonthlyView();
                    continue;
                case 0:
                    break;
            }
            break;
        }
    }
}