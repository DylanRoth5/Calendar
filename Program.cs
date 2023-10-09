﻿using Calendar.Controllers;
using Calendar.Entities;
using Calendar.Seal;

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
        Date = Tools.ChooseDate();
        Menu();
        // Save();
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