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
        // Date = Tools.ChooseDate();
        // Menu();
        // Save();
        DataBase();
    }

    private static void DataBase()
    {
    //     string createQuery = @"
    //     CREATE TABLE IF NOT EXISTS [Contacts] (
	   //  [Id]	    INTEGER NOT NULL UNIQUE,
	   //  [Name]	    CHAR(100) NOT NULL,
	   //  [LastName]	CHAR(100),
	   //  [Phone]	    INTEGER,
	   //  [Email]	    CHAR(100),
	   //  PRIMARY KEY([Id] AUTOINCREMENT))
    // ";
 //    IF OBJECT_ID(N'Events', N'U') IS NULL
 //    CREATE TABLE [Events] (
	// [Id]	INTEGER NOT NULL UNIQUE,
	// [Title]	VARCHAR(100) NOT NULL,
	// [Date]	DATETIME NOT NULL,
	// [Hours]	INTEGER,
	// [Place]	VARCHAR(150),
	// [ContactsId]	INTEGER,
	// PRIMARY KEY([Id] AUTOINCREMENT),
	// FOREIGN KEY([ContactsId]) REFERENCES [Contacts]([Id]));
        // SQLiteConnection.CreateFile("Calendar.db3");
        using (SQLiteConnection conn = new SQLiteConnection(@"Data Source=C:\Users\dylan\BlackBox\Calendar\Calendar.db"))
        {
            using (SQLiteCommand cmd = new SQLiteCommand(conn))
            {
                conn.Open();
                // cmd.CommandText =
                //     "INSERT INTO Events (Title,Date,Hours,Place) VALUES('Coffe','2023-10-10 12:52:00',2,'Guelcom')";
                // cmd.ExecuteNonQuery();
                cmd.CommandText = "select e.*,c.* from Events as e INNER JOIN main.Contacts C on C.Id = e.ContactsId";
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!reader["ContactsId"].Equals(null))
                        {
                            Tools.SayLine(
                                $"[{reader["Title"]}, {reader["Date"]}, {reader["Hours"]}, {reader["Place"]}] \n    [{reader["Name"]}, {reader["LastName"]}, {reader["Phone"]}, {reader["Email"]}]");
                        }
                        else
                        {
                            Tools.SayLine(
                                $"[{reader["Title"]}, {reader["Date"]}, {reader["Hours"]}, {reader["Place"]}]");
                        }
                    }
                    conn.Close();
                }
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
        List<object> lista = new List<object>();

        Contact contacto1 = new Contact("Damian", "Frick", 343123456,"damian.frick@uap.edu.ar" )
        {};

        Event evento1 = new Event("Cumple",DateTime.Now , 4,"la 25")
        {};

        lista.Add(contacto1);
        lista.Add(evento1);

        foreach (var item in lista)
        {
            if (item is Contact)
            {
                Contact contacto = (Contact)item;
                Console.WriteLine($"Contacto: {contacto.Nombre} {contacto.Apellido}");
            }
            else if (item is Event)
            {
                Event evento = (Event)item;
                Console.WriteLine($"Evento: {evento.Title}");
            }
        }
    }    
}