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
        Date = Tools.ChooseDate();
        Menu();
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