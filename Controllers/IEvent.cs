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
            evt.StartDate = DateTime.Parse(Console.ReadLine()); //Validar datos
            Console.WriteLine();

            Console.Write("Ingrese la hora final del evento: ");
            evt.EndDate = DateTime.Parse(Console.ReadLine()); //Validar datos
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
        evt.StartDate = DateTime.Parse(Console.ReadLine()); //Validar datos
        Console.WriteLine();

        Console.Write("Ingrese la hora final del nuevo evento: ");
        evt.EndDate = DateTime.Parse(Console.ReadLine()); //Validar datos
        Console.WriteLine();

        Console.Write("Ingrese el lugar del nuevo evento: ");
        evt.Place = Console.ReadLine(); //Validar datos
        Console.WriteLine();

        Console.WriteLine();
        pEvent.Update(evt);
    }

    public static Event Select()
    {
        return events[0];
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
        foreach (var evnt in events)
        {
            Tools.SayLine($"{evnt}");
        }
    }
}