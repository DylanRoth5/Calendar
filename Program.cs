using Calendar.Controllers;
using Calendar.Entities;

namespace Calendar;

internal static class Program
{
    public static List<Event>? Events;
    public static List<Contact>? Contacts;

    private static void Main()
    {
        Events = new List<Event>();
        Contacts = new List<Contact>();
        Load();
        Menu();
        Save();
    }

    private static void Menu()
    {
        while (true)
        {
            string[] options = { "Event", "Contact", "Monthly view", "Daily view" };
            var result = Seal.Menu("Calendar", options);
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
                case 4:
                    ICalendar.DailyView();
                    continue;
                case 0:
                    break;
            }

            break;
        }
    }

    private static void Save()
    {
        File.WriteAllText(@"Data\\Events.txt", "");
        if (Events == null) return;
        foreach (var item in Events)
            Seal.FileWrite(item.ToString(), @"Data\\Events.txt");
        File.WriteAllText(@"Data\\Contacts.txt", "");
        if (Contacts == null) return;
        foreach (var item in Contacts)
            Seal.FileWrite(item.ToString(), @"Data\\Contacts.txt");
    }

    private static void Load()
    {
        var eventData = File.ReadAllLines(@"Data\\Thing.txt");
        foreach (var info in eventData)
        {
            var data = info.Split('|');
            IEvent.Add(data[1]);
        }

        var contactData = File.ReadAllLines(@"Data\\Thing.txt");
        foreach (var info in contactData)
        {
            var data = info.Split('|');
            IContact.Add(data[1]);
        }
    }
}