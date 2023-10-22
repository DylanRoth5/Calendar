namespace Calendar.Controllers;

public interface IEvent
{
    public static List<Event> contacts = pEvent.getAll();
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
    static void Menu()
    {
        throw new NotImplementedException();
    }

    static void Add(string? s)
    {
        throw new NotImplementedException();
    }
}