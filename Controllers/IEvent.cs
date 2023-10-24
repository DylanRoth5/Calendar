using System.Data.SQLite;
using Calendar.Conections;
using Calendar.Entities;
using Calendar.Seal;

namespace Calendar.Controllers;

public interface IEvent
{
    public static List<Event> events = pEvent.getAll();
    public static void Create()
    {
            Event evt = new Event();

            Console.Write("Ingrese el titulo del evento: ");
            evt.Title = Console.ReadLine(); //Validar datos
            Console.WriteLine();

            Console.Write("Ingrese la fecha de inicio del evento dd/mm/yy 00:00:00 : ");
            evt.Start = DateTime.Parse(Console.ReadLine()); //Validar datos
            Console.WriteLine();

            Console.Write("Ingrese la hora final del evento: ");
            evt.End = DateTime.Parse(Console.ReadLine()); //Validar datos
            Console.WriteLine();

            Console.Write("Ingrese el lugar del evento: ");
            evt.Place = Console.ReadLine(); //Validar datos
            Console.WriteLine();

            events.Add(evt);
            pEvent.Save(evt);
    }
    public static void Delete(Event evt)
    {
        pEvent.Delete(evt);
        events.Remove(evt);
    }
    public static void Update(Event evt)
    {
        Console.WriteLine();
        Console.Write("Ingrese el nuevo titulo del evento: ");
        evt.Title = Console.ReadLine(); //Validar datos
        Console.WriteLine();

        Console.Write("Ingrese la hora de inicio del nuevo evento: ");
        evt.Start = DateTime.Parse(Console.ReadLine()); //Validar datos
        Console.WriteLine();

        Console.Write("Ingrese la hora final del nuevo evento: ");
        evt.End = DateTime.Parse(Console.ReadLine()); //Validar datos
        Console.WriteLine();

        Console.Write("Ingrese el lugar del nuevo evento: ");
        evt.Place = Console.ReadLine(); //Validar datos
        Console.WriteLine();

        Console.WriteLine();
        pEvent.Update(evt);
    }

    public static Event Select(int id)
    {
        return pEvent.getAll().FirstOrDefault(event_ => event_.Id == id) ?? throw new InvalidOperationException();
    }

    public static Event Select()
    {
        List();
        var id = Tools.ReadInt("Enter the Id: ");
        return pEvent.getAll().FirstOrDefault(event_ => event_.Id == id) ?? throw new InvalidOperationException();
    }
    static void Menu()
    {
        var options = new List<string>() { "create", "Update", "Delete", "list"};
        int op = Tools.Menu("Event Menu",options);
        switch (op)
        {
           case 1: Create();
                break;
            case 2: Update(Select());
                break;
            case 3: Delete(Select());
                break;
            case 4: List();
                Tools.Catch();
                break;
        }
    }

    static void List()
    {
        foreach (var event_ in events)
        {
            Tools.SayLine($"{event_}",ConsoleColor.DarkYellow);
            foreach (var contact in from relation in pRelation.getAll()
                     where relation.EventId == event_.Id
                     from contact in IContact.contacts
                     
                     where contact.Id == relation.ContactId
                     select contact)
                Tools.SayLine($" -> {contact}",ConsoleColor.Green);
        }
    }
}