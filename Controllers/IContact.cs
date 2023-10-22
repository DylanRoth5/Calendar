using System.Data.SQLite;
using Calendar.Entities;
using Calendar.Seal;

namespace Calendar.Controllers;

public interface IContact
{
    public static List<Contact> contacts = pContact.getAll();
    public static void Create()
    {
            Contact contact = new Contact();

            Console.Write("Ingrese el nombre del nuevo contacto: ");
            contact.Nombre = Console.ReadLine(); //Cambiar a validation
            Console.WriteLine();

            Console.Write("Ingrese el apellido del nuevo contacto: ");
            contact.Apellido = Console.ReadLine(); //Cambiar a validation
            Console.WriteLine();

            Console.Write("Ingrese el telefono del nuevo contacto: ");
            contact.Telefono = int.Parse(Console.ReadLine()); //Cambiar a validation
            Console.WriteLine();

            Console.Write("Ingrese el email del nuevo contacto: ");
            contact.Email = Console.ReadLine(); //Cambiar a validation
            Console.WriteLine();

            contacts.Add(contact);
            pContact.Save(contact);
    }
    public static void Delete(Contact contact)
    {
        pContact.Delete(contact);
        contacts.Remove(contact);
    }
        public static void Update(Contact contact)
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
    public static Contact Select()
    {
        Console.WriteLine("Ingrese el Nomre del Contacto:");
        string s = Console.ReadLine();
        return contacts[0];
    }
    
    static void Menu()
    {
        int op = Tools.ReadInt(Console.ReadLine());
        Console.WriteLine("Seleccione una opcion");
        switch (op)
        {
            case 1: Console.WriteLine("1. Crear Contacto");
                Create();
                break;

            case 2: Console.WriteLine("2.Actualizar Contacto");
                Update(Select());
                break;
            
            case 3: Console.WriteLine("3.Borrar Contacto");
                Delete(Select());
                break;
        }
    }

    static void Add(string? s)
    {
        throw new NotImplementedException();
    }
}