using System.Data.SQLite;
using Calendar.Entities;
using Calendar.Seal;

namespace Calendar.Controllers;

public interface IEvent
{
    public static List<Event> evt = pEvent.getAll();
    public static void Create()
    {
            Event evt = new Event();

            Console.Write("Ingrese el titulo del nuevo evento: ");
            evt.Title = Console.ReadLine(); //Cambiar a validation
            Console.WriteLine();

            Console.Write("Ingrese la fecha de inicio del nuevo evento: ");
            evt.StartTime = Console.ReadLine(); //Cambiar a validation
            Console.WriteLine();

            Console.Write("Ingrese la fecha final del nuevo evento: ");
            evt.EndTime = Console.ReadLine(); //Cambiar a validation
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
            Console.Write("Ingrese el nuevo nombre del contacto: ");
            contact.Nombre = Console.ReadLine(); //Validar datos
            Console.WriteLine();
            Console.Write("Ingrese el nuevo apellido del contacto: ");
            contact.Apellido = Console.ReadLine(); //Validar datos
            Console.WriteLine();
            Console.Write("Ingrese el nuevo número del contacto: ");
            contact.Telefono = int.Parse(Console.ReadLine()); //Validar datos
            Console.WriteLine();
            Console.Write("Ingrese el nuevo email del contacto: ");
            contact.Email = Console.ReadLine(); //Validar datos
            Console.WriteLine();
            pContact.Update(contact);
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