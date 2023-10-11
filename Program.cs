using Calendar.Controllers;
using Calendar.Entities;
using Calendar.Seal;
using System.Data.SQLite;

namespace Calendar;

internal static class Program
{
    public static List<Event>? Events;
    public static List<Contact>? Contacts;
    public static DateTime Date;

    private static void Main()
    {
        Events = new List<Event>();
        Contacts = new List<Contact>();
        // Load();
        ShowDataBase();
        Date = Tools.ChooseDate();
        Menu();
        // Save();
    }

    private static void ShowDataBase()
    {
        using (var conn = new SQLiteConnection(@"Data Source=Calendar.db"))
        {
            using (var cmd = new SQLiteCommand(conn))
            {
                conn.Open();
                Tools.SayLine("Contacts table:", ConsoleColor.Red);
                cmd.CommandText = "select * from Contacts";
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        Tools.SayLine(
                            $"[{reader["Name"]}, {reader["LastName"]}, {reader["Phone"]}, {reader["Email"]}]",
                            ConsoleColor.Green);
                }

                Tools.SayLine();
                Tools.SayLine("Events table:", ConsoleColor.Red);
                cmd.CommandText = "select * from Events";
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        Tools.SayLine(
                            $"[{reader["Title"]}, {reader["StartDate"]}, {reader["StartTime"]}, {reader["EndDate"]}, {reader["EndTime"]}, {reader["Place"]}]",
                            ConsoleColor.Green);
                }

                Tools.SayLine();
                Tools.SayLine("Conections:", ConsoleColor.Red);
                cmd.CommandText =
                    "select * from Events E join main.Registered R2 on E.EventId = R2.EventId join main.Contacts C on C.Contactd = R2.ContactId";
                var title = "";
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (title.Equals(reader["Title"].ToString()))
                        {
                            Tools.SayLine(
                                $"   \u2192 [{reader["Name"]}, {reader["LastName"]}, {reader["Phone"]}, {reader["Email"]}]",
                                ConsoleColor.DarkGreen);
                        }
                        else
                        {
                            Tools.SayLine(
                                $"[{reader["Title"]}, {reader["StartDate"]}, {reader["StartTime"]}, {reader["EndDate"]}, {reader["EndTime"]}, {reader["Place"]}]",
                                ConsoleColor.Green);
                            Tools.SayLine(
                                $"   \u2192 [{reader["Name"]}, {reader["LastName"]}, {reader["Phone"]}, {reader["Email"]}]",
                                ConsoleColor.DarkGreen);
                        }

                        title = reader["Title"].ToString();
                    }
                }

                Tools.SayLine();
                conn.Close();
            }
        }

        Console.ReadLine();
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

    private static void Save()
    {
        File.WriteAllText(@"Data\\Events.txt", "");
        if (Events == null) return;
        foreach (var item in Events)
            Tools.FileWrite(item.ToString(), @"Data\\Events.txt");
        File.WriteAllText(@"Data\\Contacts.txt", "");
        if (Contacts == null) return;
        foreach (var item in Contacts)
            Tools.FileWrite(item.ToString(), @"Data\\Contacts.txt");
    }

    private static void Load()
    {
        var eventData = File.ReadAllLines(@"Data\\Events.txt");
        foreach (var info in eventData)
        {
            var data = info.Split('|');
            IEvent.Add(data[1]);
        }

        var contactData = File.ReadAllLines(@"Data\\Contacts.txt");
        foreach (var info in contactData)
        {
            var data = info.Split('|');
            IContact.Add(data[1]);
        }
    }

    public static void ListaEventos()
    {
        List<object> lista = new();

        var contacto1 = new Contact("Damian", "Frick", 343123456, "damian.frick@uap.edu.ar")
            { };

        var evento1 = new Event("Cumple", DateTime.Now, 4, "la 25")
            { };

        lista.Add(contacto1);
        lista.Add(evento1);

        foreach (var item in lista)
            if (item is Contact)
            {
                var contacto = (Contact)item;
                Console.WriteLine($"Contacto: {contacto.Nombre} {contacto.Apellido}");
            }
            else if (item is Event)
            {
                var evento = (Event)item;
                Console.WriteLine($"Evento: {evento.Title}");
            }
    }
}