using System.Data.SQLite;
using Calendar.Conections;
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
            contact.Name = Console.ReadLine(); //Cambiar a validation
            Console.WriteLine();

            Console.Write("Ingrese el apellido del nuevo contacto: ");
            contact.LastName = Console.ReadLine(); //Cambiar a validation
            Console.WriteLine();

            Console.Write("Ingrese el telefono del nuevo contacto: ");
            contact.Phone = int.Parse(Console.ReadLine()); //Cambiar a validation
            Console.WriteLine();

            Console.Write("Ingrese el email del nuevo contacto: ");
            contact.Email = Console.ReadLine(); //Cambiar a validation
            Console.WriteLine();

            pContact.Save(contact);
            contacts = pContact.getAll();
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
        contact.Name = Console.ReadLine(); //Validar datos
        Console.WriteLine();

        Console.Write("Ingrese el nuevo apellido del contacto: ");
        contact.LastName = Console.ReadLine(); //Validar datos
        Console.WriteLine();

        Console.Write("Ingrese el nuevo número del contacto: ");
        contact.Phone = int.Parse(Console.ReadLine()); //Validar datos
        Console.WriteLine();

        Console.Write("Ingrese el nuevo email del contacto: ");
        contact.Email = Console.ReadLine(); //Validar datos
        Console.WriteLine();

        pContact.Update(contact);
    }
    public static Contact Select(int id)
    {
        return pContact.getAll().FirstOrDefault(contact => contact.Id == id) ?? throw new InvalidOperationException();
    }

    public static Contact Select()
    {
        var result = Tools.Select("Choose the contact", pContact.getAll().Select(contact => contact.ToString()).ToList());
        return pContact.getAll().FirstOrDefault(contact => contact.Id == pContact.getAll()[result - 1].Id)!;
    }
    
    static void Menu()
    {
        while (true)
        {
            string[] options = { "Create", "Delete", "Update", "List" };
            var result = Tools.Menu("Contacts", options);
            switch (result)
            {
                case 1:
                    Create();
                    continue;
                case 2:
                    Delete(Select());
                    continue;
                case 3:
                    Update(Select());
                    continue;
                case 4:
                    List();
                    Tools.Catch();
                    continue;
                case 0:
                    break;
            }

            break;
        }
    }

    public static void List()
    {
        var content = new List<string>();
        foreach (var contact in pContact.getAll()) content.Add(contact.ToString());
        Tools.Board(content);
    }
}