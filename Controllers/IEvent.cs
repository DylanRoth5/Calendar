using System.Data.SQLite;
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
            evt.FechaHora = DateTime.Parse(Console.ReadLine()); //Validar datos
            Console.WriteLine();

            Console.Write("Ingrese la hora de inicio del evento: ");
            evt.StartTime = Console.ReadLine(); //Validar datos
            Console.WriteLine();

            Console.Write("Ingrese la hora final del evento: ");
            evt.EndTime = Console.ReadLine(); //Validar datos
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

            Console.Write("Ingrese la fecha de inicio del nuevo evento dd/mm/yy 00:00:00 : ");
            evt.FechaHora = DateTime.Parse(Console.ReadLine()); //Validar datos
            Console.WriteLine();

            Console.Write("Ingrese la hora de inicio del nuevo evento: ");
            evt.StartTime = Console.ReadLine(); //Validar datos
            Console.WriteLine();

            Console.Write("Ingrese la hora final del nuevo evento: ");
            evt.EndTime = Console.ReadLine(); //Validar datos
            Console.WriteLine();

            Console.Write("Ingrese el lugar del nuevo evento: ");
            evt.Place = Console.ReadLine(); //Validar datos
            Console.WriteLine();

            Console.WriteLine();
            pEvent.Update(evt);
        }
    static void Menu()
    {
        throw new NotImplementedException();
    }

    static void Add(string? s)
    {
        throw new NotImplementedException();
    }
}