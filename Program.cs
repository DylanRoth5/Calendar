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
                //     "ALTER TABLE Contacts RENAME COLUMN Telefono TO Phone";
                // cmd.ExecuteNonQuery();
                // // cmd.CommandText = createQuery;
                // cmd.CommandText =
                //     "INSERT INTO Contacts(Name,LastName,Phone,Email) VALUES('Alex','Malone','3434808642','alexmalone@gmail.com')";
                // cmd.ExecuteNonQuery();
                // cmd.CommandText =
                //     "INSERT INTO Contacts(Name,LastName,Phone,Email) VALUES('Paula','Rios','3434808643','paularios@gmail.com')";
                // cmd.ExecuteNonQuery();
                cmd.CommandText = "select * from Contacts";
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Tools.SayLine($"[{reader["Name"]}] [{reader["LastName"]}] [{reader["Phone"]}] [{reader["Email"]}]");
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